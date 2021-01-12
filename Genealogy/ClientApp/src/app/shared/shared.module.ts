import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { ComponentsModule } from './components/components.module';
import { AssetsModule } from './assets/assets.module';
import { UserStatusPipe } from './pipes/user-status.pipe';

@NgModule({
  imports: [CommonModule, ReactiveFormsModule, ComponentsModule, AssetsModule],
  exports: [CommonModule, ReactiveFormsModule, ComponentsModule, AssetsModule, UserStatusPipe],
  declarations: [UserStatusPipe],
})
export class SharedModule {}
