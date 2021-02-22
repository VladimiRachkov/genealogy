import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { ComponentsModule } from './components/components.module';
import { AssetsModule } from './assets/assets.module';
import { UserStatusPipe, CurrencyPipe } from './pipes';

@NgModule({
  imports: [CommonModule, ReactiveFormsModule, ComponentsModule, AssetsModule],
  exports: [CommonModule, ReactiveFormsModule, ComponentsModule, AssetsModule, UserStatusPipe, CurrencyPipe],
  declarations: [UserStatusPipe, CurrencyPipe],
})
export class SharedModule {}
