import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService, AuthenticationService, UserService } from '@core';
import { NOTIFICATIONS } from '@enums';
import { BusinessObject, CatalogItem, PaymentOutDto } from '@models';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { Store } from '@ngxs/store';
import { NotifierService } from 'angular-notifier';
import { isNil } from 'lodash';
import { environment } from '@env/environment';
import { FeedbackComponent } from '@shared';

@Component({
  selector: 'app-purchase',
  templateUrl: './purchase.component.html',
  styleUrls: ['./purchase.component.scss'],
})
export class PurchaseComponent implements OnInit {
  @ViewChild('content', { static: false }) content;
  @ViewChild(FeedbackComponent, { static: false }) feedbackModal: FeedbackComponent;

  closeResult: string = null;
  item: CatalogItem;

  constructor(
    private modalService: NgbModal,
    private apiService: ApiService,
    private authService: AuthenticationService,
    private router: Router,
    private notifierService: NotifierService
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
    if (isNil(userId)) {
      this.router.navigate(['/login']);
      this.modalService.dismissAll();
      this.notifierService.notify('error', NOTIFICATIONS.NOT_AUTHORIZED, 'NOT_AUTHORIZED');
      return;
    }
    const hostname = environment.apiUrl;
    const body: PaymentOutDto = { returnUrl: hostname + '/catalog/info', productId: this.item.id, userId };

    this.apiService.post<string>('catalog/payment', body).subscribe(res => window.open(res as string, '_self'));
  }

  onCloseButtonClick() {
    this.modalService.dismissAll();
  }

  onMessageButtonClick() {
    this.feedbackModal.open('Вопрос о продукте: ' + this.item.title);
  }

  private getDismissReason(reason: any) {}
}
