import { Component, OnInit } from '@angular/core';
import { Store } from '@ngxs/store';
import { GetPage } from '@act/page.actions';
import { PageFilter } from '@mdl/filters/page.filter';
import { Page } from '@mdl/page';

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
    const pageFilter: PageFilter = { id: '97df9d46-37a8-4983-a679-7fd407245ca0' };
    this.store.dispatch(new GetPage(pageFilter)).subscribe(data => {
      this.page = data.page.page;
      console.log('PAGE', this.page);
    });
  }
}
