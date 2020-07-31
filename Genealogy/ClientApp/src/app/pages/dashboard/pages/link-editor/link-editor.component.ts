import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Store, Select } from '@ngxs/store';
import { FetchFreePageList, AddLink, FetchLinkList } from '@actions';
import { Page, Link, LinkDto, LinkFilter } from '@models';
import { PageState } from '@states';
import { Observable, combineLatest } from 'rxjs';
import { switchMapTo, filter } from 'rxjs/operators';
import { LinkState } from 'app/states/link.state';
import { untilDestroyed } from 'ngx-take-until-destroy';

@Component({
  selector: 'app-link-editor',
  templateUrl: './link-editor.component.html',
  styleUrls: ['./link-editor.component.scss'],
})
export class LinkEditorComponent implements OnInit, OnDestroy {
  @ViewChild('content', { static: false }) content;

  @Select(PageState.freePageList) freePageList$: Observable<Array<Page>>;
  @Select(LinkState.linkList) linkList$: Observable<Array<Link>>;

  pages: Array<any> = [];
  links: Array<Link> = [];
  closeResult: string = null;
  pageId: string = null;

  constructor(private modalService: NgbModal, private store: Store) {}

  ngOnInit() {
    combineLatest(this.freePageList$, this.linkList$)
      .pipe(
        filter(data => data[0] != null && data[1] != null),
        untilDestroyed(this)
      )
      .subscribe(data => {
        this.pages = [];
        this.links = [];
        data[0].forEach(page => {
          let location = '';
          data[1].forEach(link => {
            if (this.pageId === link.pageId && page.id === link.targetPageId) {
              location = 'this';
              this.links.push({ caption: page.title, name: page.name, order: link.order });
            }
          });
          this.pages.push({ ...page, location });
        });
        this.links = this.links.sort((a, b) => a.order - b.order);
        console.log('LINKS', this.links);
      });
  }

  ngOnDestroy() {}

  public open(pageId: string) {
    this.pageId = pageId;
    this.store
      .dispatch(new FetchFreePageList(pageId))
      .pipe(switchMapTo(this.store.dispatch(new FetchLinkList(null))))
      .subscribe(data => {
        console.log('LIST', data);
      });

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
    this.store.dispatch(new AddLink(link)).subscribe();
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
