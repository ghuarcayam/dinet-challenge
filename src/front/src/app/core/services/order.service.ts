// src/app/services/order.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { 
  Order, RequestOrderCreate, RequestOrderUpdate, 
  GetOrdersResultOperationResult, GetOrderResultOperationResult,
  GuidOperationResult, OperationResult 
} from '../models/order-model';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private baseUrl = '/api';

  constructor(private http: HttpClient) {}

  getOrders(
    startAt: number = 0, 
    rowCount: number = 10, 
    cliente?: string, 
    from?: Date, 
    to?: Date, 
    fieldOrder: string = 'FechaCreacion', 
    orderDesc: boolean = true
  ): Observable<GetOrdersResultOperationResult> {
    let params = new HttpParams()
      .set('startAt', startAt.toString())
      .set('rowCount', rowCount.toString())
      .set('fieldOrder', fieldOrder)
      .set('orderDesc', orderDesc.toString());
    
    if (cliente) {
      params = params.set('cliente', cliente);
    }
    
    if (from) {
      params = params.set('from', from.toISOString());
    }
    
    if (to) {
      params = params.set('to', to.toISOString());
    }
    
    return this.http.get<GetOrdersResultOperationResult>(this.baseUrl + "/order", { params });
  }

  getOrderById(orderId: string): Observable<GetOrderResultOperationResult> {
    return this.http.get<GetOrderResultOperationResult>(`${this.baseUrl}/order/${orderId}`);
  }

  createOrder(order: RequestOrderCreate): Observable<GuidOperationResult> {
    return this.http.post<GuidOperationResult>(this.baseUrl + "/order", order);
  }

  updateOrder(orderId: string, order: RequestOrderUpdate): Observable<OperationResult> {
    return this.http.put<OperationResult>(`${this.baseUrl}/order/${orderId}`, order);
  }

  deleteOrder(orderId: string): Observable<OperationResult> {
    return this.http.delete<OperationResult>(`${this.baseUrl}/order/${orderId}`);
  }
}