import { ChangeDetectionStrategy, Component } from '@angular/core';
import { PaginationOrder } from '../../../core/models/order-model';
import { FormBuilder, FormGroup } from '@angular/forms';
import { OrderService } from '../../../core/services/order.service';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Router } from '@angular/router';
import { SharedModule } from '../../../shared/shared-module';

@Component({
  selector: 'app-order-list-page',
  imports: [SharedModule],
  templateUrl: './order-list-page.component.html',
  styleUrl: './order-list-page.component.scss',
  changeDetection: ChangeDetectionStrategy.Default,
})
export class OrderListPageComponent 
{ 

  
  orders: PaginationOrder[] = [];
  totalRecords = 0;
  loading = false;
  first = 0;
  rows = 10;
  
  // Agregar propiedades para el ordenamiento
  sortField = 'FechaCreacion';
  sortOrder = -1; // -1 para descendente (desc), 1 para ascendente (asc)
  
  filterForm: FormGroup;
  
  constructor(
    private orderService: OrderService,
    private fb: FormBuilder,
    private router: Router,
    private confirmationService: ConfirmationService,
    private messageService: MessageService
  ) {
    this.filterForm = this.fb.group({
      cliente: [''],
      fromDate: [null],
      toDate: [null]
    });
  }
  
  ngOnInit(): void {
    this.loadOrders();
  }
  
  loadOrders(event?: any): void {
    this.loading = true;
    
    const startAt = event ? event.first : this.first;
    const rowCount = event ? event.rows : this.rows;
    
    if (event) {
      this.first = event.first;
      this.rows = event.rows;
      
      // Si el evento incluye información de ordenamiento, actualizar las propiedades
      if (event.sortField) {
        this.sortField = event.sortField;
        this.sortOrder = event.sortOrder;
      }
    }
    
    const cliente = this.filterForm.get('cliente')?.value;
    const fromDate = this.filterForm.get('fromDate')?.value;
    const toDate = this.filterForm.get('toDate')?.value;
    
    // Enviar los parámetros de ordenamiento al servicio
    const orderDesc = this.sortOrder === -1;
    
    this.orderService.getOrders(
      startAt,
      rowCount,
      cliente,
      fromDate,
      toDate,
      this.sortField,  // Campo por el cual ordenar
      orderDesc        // Indica si es descendente u ascendente
    ).subscribe({
      next: (response) => {
        
        if (response.success) {
          this.orders = response.data.orders;
          this.totalRecords = response.data.totalRows;
        } else {
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: response.message || 'Failed to load orders'
          });
        }
        this.loading = false;
      },
      error: (error) => {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'Failed to load orders'
        });
        this.loading = false;
      }
    });
  }
  
  // Método para manejar el cambio de ordenamiento
  onSort(event: any): void {
    this.loadOrders(event);
  }
  
  applyFilters(): void {
    this.first = 0;
    this.loadOrders();
  }
  
  resetFilters(): void {
    this.filterForm.reset();
    this.first = 0;
    // Resetear también el ordenamiento a los valores iniciales
    this.sortField = 'FechaCreacion';
    this.sortOrder = -1;
    this.loadOrders();
  }
  
  createOrder(): void {
    this.router.navigate(['/create']);
  }
  
  editOrder(orderId: string): void {
    this.router.navigate(['/', orderId]);
  }
  
  confirmDelete(orderId: string, event: Event): void {
    this.confirmationService.confirm({
      target: event.target as EventTarget,
      message: 'Are you sure you want to delete this order?',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.deleteOrder(orderId);
      }
    });
  }
  
  deleteOrder(orderId: string): void {
    this.loading = true;
    this.orderService.deleteOrder(orderId).subscribe({
      next: (response) => {
        if (response.success) {
          this.messageService.add({
            severity: 'success',
            summary: 'Success',
            detail: 'Order deleted successfully'
          });
          this.loadOrders();
        } else {
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: response.message || 'Failed to delete order'
          });
        }
        this.loading = false;
      },
      error: () => {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'Failed to delete order'
        });
        this.loading = false;
      }
    });
  }


}
