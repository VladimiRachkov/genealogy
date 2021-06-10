import { EventEmitter, Input } from '@angular/core';
import { Component, OnInit, Output, ViewChild } from '@angular/core';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss'],
})
export class ModalComponent implements OnInit {
  @ViewChild('content', { static: false }) content;

  @Input() size: string = 'md';
  @Input() ariaLabelledBy: string;
  @Input() header: string;

  @Output() apply: EventEmitter<void> = new EventEmitter();

  closeResult: string = null;

  constructor(private modalService: NgbModal) {}

  ngOnInit() {}

  open() {
    const options: NgbModalOptions = {
      ariaLabelledBy: this.ariaLabelledBy,
      size: this.size,
    };

    this.modalService.open(this.content, options).result.then(
      result => this.apply.emit(),
      reason => {
        this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
      }
    );
  }

  close() {
    this.modalService.dismissAll();
  }

  onCloseButtonClick() {
    this.close();
  }

  private getDismissReason(reason: any) {}
}
