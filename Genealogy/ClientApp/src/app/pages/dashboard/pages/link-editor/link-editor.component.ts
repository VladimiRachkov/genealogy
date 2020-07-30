import { Component, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Store, Select } from '@ngxs/store';
import { FetchFreePageList, AddLink } from '@actions';
import { Page, Link, LinkDto } from '@models';
import { PageState } from '@states';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-link-editor',
  templateUrl: './link-editor.component.html',
  styleUrls: ['./link-editor.component.scss'],
})
export class LinkEditorComponent implements OnInit {
  @ViewChild('content', { static: false }) content;
  @Select(PageState.freePageList) freePageList$: Observable<Array<Page>>;

  closeResult: string = null;
  pageId: string = null;

  constructor(private modalService: NgbModal, private store: Store) {}

  ngOnInit() {}

  public open(id: string) {
    this.pageId = id;
    this.store.dispatch(new FetchFreePageList()).subscribe(data => {
      console.log('LIST', data);
    });
    //   this.page = data.page.page;
    //   this.htmlContent = this.page.content;
    // });
    this.modalService.open(this.content, { ariaLabelledBy: 'modal-basic-title', size: 'xl' }).result.then(
      result => {},
      reason => {
        this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
      }
    );
  }

  onSelect(targetPageId: string) {
    console.log(targetPageId);
    const link: LinkDto = {
      pageId: this.pageId,
      targetPageId,
    };
    this.store.dispatch(new AddLink(link)).subscribe(data => console.log(data));
  }

  private getDismissReason(reason: any) {
    // if (reason === ModalDismissReasons.ESC) {
    //   return 'by pressing ESC';
    // } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
    //   return 'by clicking on a backdrop';
    // } else {
    //   return `with: ${reason}`;
    // }
  }
}
