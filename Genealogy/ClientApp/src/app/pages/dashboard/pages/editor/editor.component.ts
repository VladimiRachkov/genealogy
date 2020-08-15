import { Component, OnInit, ViewChild, TemplateRef, ContentChild, Output, EventEmitter } from '@angular/core';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { EditorConfig } from './editor.config';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { Store } from '@ngxs/store';
import { Page, PageFilter, PageDto } from '@models';
import { GetPage, UpdatePage } from '@actions';

@Component({
  selector: 'app-page-editor',
  templateUrl: './editor.component.html',
  styleUrls: ['./editor.component.scss'],
})
export class EditorComponent implements OnInit {
  @ViewChild('content', { static: false }) content;
  @Output() result = new EventEmitter<string>();
  Editor = ClassicEditor;
  editorConfig = {
    toolbar: [
      'undo',
      'redo',
      'bold',
      'italic',
      'blockQuote',
      'ckfinder',
      'imageTextAlternative',
      'imageUpload',
      'heading',
      'imageStyle:full',
      'imageStyle:side',
      'link',
      'numberedList',
      'bulletedList',
      'mediaEmbed',
      'insertTable',
      'tableColumn',
      'tableRow',
      'mergeTableCells',
    ],
  };

  closeResult: string = null;
  htmlContent: string = '';
  page: Page;
  
  constructor(private modalService: NgbModal, private store: Store) {}

  ngOnInit() {}

  public open(id: string) {
    const pageFilter: PageFilter = { id };
    this.store.dispatch(new GetPage(pageFilter)).subscribe(data => {
      this.page = data.page.page;
      this.htmlContent = this.page.content;
    });
    this.modalService.open(this.content, { ariaLabelledBy: 'modal-basic-title', size: 'xl' }).result.then(
      result => {
        if (result === 'save') {
          const resultPage: PageDto = { ...this.page, content: this.htmlContent };
          this.store.dispatch(new UpdatePage(resultPage));
        }
      },
      reason => {
        this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
      }
    );
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }
}
