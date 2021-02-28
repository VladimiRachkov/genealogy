import { Component, OnInit } from '@angular/core';
import { ApiService } from '@core';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.scss'],
})
export class PaymentComponent implements OnInit {
  constructor(private apiService: ApiService) {}

  ngOnInit() {

  }
}
