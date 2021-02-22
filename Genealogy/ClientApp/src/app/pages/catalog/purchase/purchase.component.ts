import { Component, OnInit, ViewChild } from '@angular/core';
import { ApiService } from '@core';
import { BusinessObject, CatalogItem } from '@models';
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

  constructor(private modalService: NgbModal, private store: Store, private apiService: ApiService) {}

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
    const params: any = { returnUrl: 'http://37.230.116.107/payment' };
    this.apiService.get<string>('payment', params).subscribe(res => window.open(res as string));
  }

  private getDismissReason(reason: any) {}
}
