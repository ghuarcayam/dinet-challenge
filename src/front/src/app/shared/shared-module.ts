// src/app/app.module.ts
import { NgModule } from '@angular/core';


import { ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS } from '@angular/common/http';



// PrimeNG Modules
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { CardModule } from 'primeng/card';
import { ConfirmPopupModule } from 'primeng/confirmpopup';
import { FieldsetModule } from 'primeng/fieldset';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { MessageModule } from 'primeng/message';
import { MessagesModule } from 'primeng/messages';
import { PasswordModule } from 'primeng/password';
import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';

// PrimeNG Services
import { ConfirmationService, MessageService } from 'primeng/api';
import { CommonModule } from '@angular/common';

const modules = [
  ReactiveFormsModule,
  CommonModule,
  ConfirmDialogModule,
  // PrimeNG Modules
  ButtonModule,
  CalendarModule,
  CardModule,
  ConfirmPopupModule,
  FieldsetModule,
  InputNumberModule,
  InputTextModule,
  MessageModule,
  MessagesModule,
  PasswordModule,
  TableModule,
  ToastModule]
@NgModule({
  declarations: [
   ],
  imports: [
    ...modules
  ],
  exports:[...modules],
  providers: [
    // Services
    MessageService,
    ConfirmationService,
    
    // Interceptors
    
  ],
})
export class SharedModule { }