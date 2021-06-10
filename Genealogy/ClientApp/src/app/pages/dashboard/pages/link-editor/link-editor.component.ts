import { Component, OnInit, ViewChild } from '@angular/core';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { Store, Select } from '@ngxs/store';
import { FetchFreePageList, AddLink, FetchLinkList, UpdateLinkList } from '@actions';
import { Page, Link, LinkDto } from '@models';
import { PageState } from '@states';
import { Observable, combineLatest } from 'rxjs';
import { filter } from 'rxjs/operators';
import { LinkState } from 'app/states/link.state';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-link-editor',
  templateUrl: './link-editor.component.html',
  styleUrls: ['./link-editor.component.scss'],
})
export class LinkEditorComponent implements OnInit {
  @ViewChild('content', { static: false }) content;

  @Select(PageState.freePageList) freePageList$: Observable<Array<Page>>;
  @Select(LinkState.linkList) linkList$: Observable<Array<Link>>;

  pages: Array<any> = [];
  links: Array<Link> = [];
  closeResult: string = null;
  pageId: string = null;

  constructor(private modalService: NgbModal, private store: Store) {}

  ngOnInit() {
    combineLatest([this.freePageList$, this.linkList$])
      .pipe(filter(data => data[0] != null && data[1] != null))
      .subscribe(data => {
        this.pages = [];
        this.links = [];
        data[0].forEach(page => {
          let location = '';
          data[1].forEach(link => {
            if (this.pageId === link.pageId && page.id === link.targetPageId) {
              location = 'this';
              this.links.push({ ...link, caption: page.title, route: decodeURI(page.name) });
            }
          });
          this.pages.push({ ...page, location });
        });
        this.links = this.links.sort((a, b) => a.order - b.order);
      });
  }

  public open(pageId: string) {
    this.pageId = pageId;
    this.store.dispatch([new FetchFreePageList(this.pageId), new FetchLinkList(null)]).subscribe(() => {
      const options: NgbModalOptions = {
        ariaLabelledBy: 'modal-basic-title',
        size: 'xl',
        windowClass: 'your-custom-dialog-class',
      };

      this.modalService.open(this.content, options).result.then(
        result => {},
        reason => {
          this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
        }
      );
    });
  }

  onSelect(targetPageId: string) {
    const link: LinkDto = {
      pageId: this.pageId,
      targetPageId,
    };
    this.store.dispatch(new AddLink(link)).subscribe();
  }

  drop(event: CdkDragDrop<string[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
      this.reorderLinkList(event.container.data as Array<Link>);
    } else {
      transferArrayItem(event.previousContainer.data, event.container.data, event.previousIndex, event.currentIndex);
    }
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

  private reorderLinkList(linkList: Array<Link>) {
    this.links = linkList.map((link, index) => ({ ...link, order: index }));
    let newList = this.links.map(link => link as LinkDto);
    this.store.dispatch(new UpdateLinkList(newList));
  }
}
