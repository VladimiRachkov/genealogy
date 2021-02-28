import { Component, OnInit, ViewChild } from '@angular/core';
import { ApiService, AuthenticationService, UserService } from '@core';
import { BusinessObject, CatalogItem, PaymentOutDto } from '@models';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { Store } from '@ngxs/store';

@Component({
  selector: 'app-purchase',
  templateUrl: './purchase.component.html',
  styleUrls: ['./purchase.component.scss'],
})
export class PurchaseComponent implements OnInit {
  @ViewChild('content', { static: false }) content;

  closeResult: string = null;
  item: CatalogItem;

  constructor(
    private modalService: NgbModal,
    private store: Store,
    private apiService: ApiService,
    private authService: AuthenticationService
  ) {}

  ngOnInit() {}

  open(item: CatalogItem) {
    const options: NgbModalOptions = {
      ariaLabelledBy: 'modal-basic-title',
      size: 'md',
      windowClass: 'your-custom-dialog-class',
    };

    this.modalService.open(this.content, options).result.then(
      result => {},
      reason => {
        this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
      }
    );

    this.item = item;
  }

  onPayButtonClick() {
    const userId = this.authService.getUserId();
    const body: PaymentOutDto = { returnUrl: 'http://localhost:5000/api/payment', productId: this.item.id, userId };
    this.apiService.post<string>('payment', body).subscribe(res => window.open(res as string));
  }

  onCloseButtonClick() {
    this.modalService.dismissAll();
  }

  private getDismissReason(reason: any) {}
}
