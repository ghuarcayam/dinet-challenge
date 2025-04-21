import { ChangeDetectionStrategy, Component } from '@angular/core';
import { SharedModule } from '../../../shared/shared-module';
import { OrderItem, PaginationOrder } from '../../../core/models/order-model';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { OrderService } from '../../../core/services/order.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmationService, MessageService } from 'primeng/api';

@Component({
  selector: 'app-order-form-page',
  imports: [SharedModule],
  templateUrl: './order-form-page.component.html',
  styleUrl: './order-form-page.component.scss',
  changeDetection: ChangeDetectionStrategy.Default,
})
export class OrderFormPageComponent 
{ 
  orderForm: FormGroup;
  isEditMode = false;
  orderId: string | null = null;
  loading = false;
  submitting = false;
  
  constructor(
    private fb: FormBuilder,
    private orderService: OrderService,
    private route: ActivatedRoute,
    private router: Router,
    private messageService: MessageService
  ) {
    this.orderForm = this.createOrderForm();
  }
  
  ngOnInit(): void {
    this.orderId = this.route.snapshot.paramMap.get('id');
    this.isEditMode = !!this.orderId;
    
    if (this.isEditMode && this.orderId) {
      this.loadOrder(this.orderId);
    }
  }
  
  createOrderForm(): FormGroup {
    return this.fb.group({
      cliente: ['', [Validators.required]],
      fechaCreacion: [new Date(), [Validators.required]],
      items: this.fb.array([this.createItemForm()])
    });
  }
  
  createItemForm(item?: OrderItem): FormGroup {
    return this.fb.group({
      id: [item?.id || null],
      producto: [item?.producto || '', [Validators.required]],
      cantidad: [item?.cantidad || 1, [Validators.required, Validators.min(1)]],
      precioUnitario: [item?.precioUnitario || 0, [Validators.required, Validators.min(0.01)]],
      subTotal: [{ value: item?.subTotal || 0, disabled: true }]
    });
  }
  
  get items(): FormArray {
    return this.orderForm.get('items') as FormArray;
  }
  
  addItem(): void {
    this.items.push(this.createItemForm());
  }
  
  removeItem(index: number): void {
    if (this.items.length > 1) {
      this.items.removeAt(index);
      this.calculateTotals();
    } else {
      this.messageService.add({
        severity: 'info',
        summary: 'Info',
        detail: 'At least one item is required'
      });
    }
  }
  
  loadOrder(orderId: string): void {
    this.loading = true;
    this.orderService.getOrderById(orderId).subscribe({
      next: (response) => {
        if (response.success) {
          const order = response.data;
          
          // Clear existing form array
          while (this.items.length) {
            this.items.removeAt(0);
          }
          
          // Set basic order data
          this.orderForm.patchValue({
            cliente: order.cliente,
            fechaCreacion: new Date(order.fechaCreacion)
          });
          
          // Add items
          order.items.forEach(item => {
            this.items.push(this.createItemForm({
              id: item.id,
              producto: item.product,
              cantidad: item.cantidad,
              precioUnitario: item.precioUnitario,
              subTotal: item.subTotal
            }));
          });
          
          this.loading = false;
        } else {
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: response.message || 'Failed to load order'
          });
          this.router.navigate(['/']);
        }
      },
      error: () => {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'Failed to load order'
        });
        this.loading = false;
        this.router.navigate(['/']);
      }
    });
  }
  
  calculateItemTotal(index: number): void {
    const item = this.items.at(index) as FormGroup;
    const cantidad = item.get('cantidad')?.value || 0;
    const precioUnitario = item.get('precioUnitario')?.value || 0;
    const subTotal = cantidad * precioUnitario;
    
    item.get('subTotal')?.setValue(subTotal);
    this.calculateTotals();
  }
  
  calculateTotals(): void {
    // This function would calculate totals for display purposes
    // The API calculates the actual totals on the server
  }
  
  onSubmit(): void {
    if (this.orderForm.invalid) {
      this.orderForm.markAllAsTouched();
      this.messageService.add({
        severity: 'error',
        summary: 'Validation Error',
        detail: 'Please fill all required fields correctly'
      });
      return;
    }
    
    this.submitting = true;
    
    const formValue = this.orderForm.value;
    
    // Format the request based on mode (create/update)
    if (this.isEditMode && this.orderId) {
      const updateRequest = {
        cliente: formValue.cliente,
        fechaCreacion: formValue.fechaCreacion,
        items: formValue.items.map((item: any) => ({
          id: item.id,
          producto: item.producto,
          cantidad: item.cantidad,
          precioUnitario: item.precioUnitario
        }))
      };
      
      this.orderService.updateOrder(this.orderId, updateRequest).subscribe({
        next: (response) => {
          if (response.success) {
            this.messageService.add({
              severity: 'success',
              summary: 'Success',
              detail: 'Order updated successfully'
            });
            this.router.navigate(['/']);
          } else {
            this.messageService.add({
              severity: 'error',
              summary: 'Error',
              detail: response.message || 'Failed to update order'
            });
          }
          this.submitting = false;
        },
        error: () => {
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: 'Failed to update order'
          });
          this.submitting = false;
        }
      });
    } else {
      const createRequest = {
        cliente: formValue.cliente,
        fechaCreacion: formValue.fechaCreacion,
        items: formValue.items.map((item: any) => ({
          producto: item.producto,
          cantidad: item.cantidad,
          precioUnitario: item.precioUnitario
        }))
      };
      
      this.orderService.createOrder(createRequest).subscribe({
        next: (response) => {
          if (response.success) {
            this.messageService.add({
              severity: 'success',
              summary: 'Success',
              detail: 'Order created successfully'
            });
            this.router.navigate(['/']);
          } else {
            this.messageService.add({
              severity: 'error',
              summary: 'Error',
              detail: response.message || 'Failed to create order'
            });
          }
          this.submitting = false;
        },
        error: () => {
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: 'Failed to create order'
          });
          this.submitting = false;
        }
      });
    }
  }
  
  cancel(): void {
    this.router.navigate(['/']);
  }
}
