import { Component, OnInit } from '@angular/core';
import { Store } from '@ngxs/store';
import { Page, PageFilter } from '@models';
import { GetPage } from '@actions';

@Component({
  selector: 'app-start',
  templateUrl: './start.component.html',
  styleUrls: ['./start.component.scss'],
  providers: [],
})
export class StartComponent implements OnInit {
  page: Page;
  constructor(private store: Store) {}

  ngOnInit() {
    const pageFilter: PageFilter = { name: 'start' };
    this.store.dispatch(new GetPage(pageFilter)).subscribe(data => (this.page = data.page.page));
  }
}
