export interface OrderItem {
  id?: string;  // Optional for creation
  producto: string;
  cantidad: number;
  precioUnitario: number;
  subTotal?: number; // Calculated field
}

export interface Order {
  id?: string;
  cliente: string;
  fechaCreacion: Date | string;
  total?: number;
  items: OrderItem[];
}

export interface RequestOrderCreate {
  cliente: string;
  fechaCreacion: Date | string;
  items: RequestOrderCreateItem[];
}

export interface RequestOrderCreateItem {
  producto: string;
  cantidad: number;
  precioUnitario: number;
}

export interface RequestOrderUpdate {
  cliente: string;
  fechaCreacion: Date | string;
  items: RequestOrderUpdateItem[];
}

export interface RequestOrderUpdateItem {
  id?: string;
  producto: string;
  cantidad: number;
  precioUnitario: number;
}

export interface PaginationOrder {
  id: string;
  fechaCreacion: Date | string;
  cliente: string;
  total: number;
  items: PaginationOrderItem[];
}

export interface PaginationOrderItem {
  id: string;
  producto: string;
  cantidad: number;
  precioUnitario: number;
  subTotal: number;
}

export interface GetOrdersResult {
  totalRows: number;
  orders: PaginationOrder[];
}

export interface OperationResult {
  message: string;
  success: boolean;
}

export interface GuidOperationResult extends OperationResult {
  data: string; // UUID
}

export interface GetOrdersResultOperationResult extends OperationResult {
  data: GetOrdersResult;
}

export interface GetOrderResult {
  orderId: string;
  fechaCreacion: Date | string;
  cliente: string;
  total: number;
  items: GetOrderResultItem[];
}

export interface GetOrderResultItem {
  id: string;
  product: string;
  cantidad: number;
  precioUnitario: number;
  subTotal: number;
}

export interface GetOrderResultOperationResult extends OperationResult {
  data: GetOrderResult;
}