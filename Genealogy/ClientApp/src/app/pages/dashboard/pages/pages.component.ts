import { Component, OnInit, ViewChild } from '@angular/core';
import { Store } from '@ngxs/store';
import { Page } from '@mdl/page';
import { FetchPageList, AddPage, MarkAsRemovedPage, GetPage, UpdatePage } from '@act/page.actions';
import { PageState } from '@state/page.state';
import { Table } from '@shared/components/dashboard/table/table';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { PageDto } from '@mdl/dtos/page.dto';
import { PageFilter } from '@mdl/filters/page.filter';
import { EditorComponent } from './editor/editor.component';

@Component({
  selector: 'dashboard-pages',
  templateUrl: './pages.component.html',
  styleUrls: ['./pages.component.scss'],
})
export class PagesComponent implements OnInit {
  @ViewChild(EditorComponent, { static: true }) editor: EditorComponent;

  pageForm: FormGroup;
  selectedPage: Page;
  tableData: Table.Data;

  closeResult: string = null;

  constructor(private store: Store) {}

  ngOnInit() {
    this.updateList();
    this.pageForm = new FormGroup({
      id: new FormControl(null),
      name: new FormControl(null, [Validators.required]),
      title: new FormControl(null, [Validators.required]),
    });
    this.selectedPage = null;
  }

  addPage() {
    const page = this.pageForm.value as PageDto;
    this.store.dispatch(new AddPage(page)).subscribe(() => this.updateList());
  }

  markAsRemovedPage(id: string) {
    this.store.dispatch(new MarkAsRemovedPage(id)).subscribe(() => this.updateList());
  }

  changeCemetery(id: string) {
    const filter: PageFilter = { id };
    this.store.dispatch(new GetPage(filter)).subscribe(() => {
      const page = this.store.selectSnapshot<Page>(PageState.page);
      const { id, name, title } = page;
      this.selectedPage = page;
      this.pageForm.setValue({ id, name, title });
    });
  }

  updatePage() {
    const page = this.pageForm.value;
    this.store.dispatch(new UpdatePage(page)).subscribe(() => this.updateList());
  }

  resetForm() {
    //this.pageForm.getRawValue();
    this.selectedPage = null;
  }

  onChange(pageId: string) {
    this.editor.open(pageId);
  }

  onEditorClose(result: string) {
    console.log(result);
  }

  private updateList() {
    this.store.dispatch(new FetchPageList({})).subscribe(() => {
      const pageList: Array<Page> = this.store.selectSnapshot<Array<Page>>(PageState.pageList);
      const items = pageList && pageList.map<Table.Item>(item => ({ id: item.id, values: [item.name, item.title] }));
      this.tableData = {
        fields: ['Имя', 'Название'],
        items,
      };
      this.resetForm();
    });
  }
}
