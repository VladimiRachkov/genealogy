import { Component, OnInit, ViewChild } from '@angular/core';
import { Store } from '@ngxs/store';
import { Page, PageFilter } from '@models';
import { GetPage } from '@actions';
import { FeedbackComponent } from '@shared';

@Component({
  selector: 'app-start',
  templateUrl: './start.component.html',
  styleUrls: ['./start.component.scss'],
  providers: [],
})
export class StartComponent implements OnInit {
  @ViewChild(FeedbackComponent, { static: false }) feedbackModal: FeedbackComponent;
  
  page: Page;
  constructor(private store: Store) {}

  ngOnInit() {
    const pageFilter: PageFilter = { name: 'index' };
    this.store.dispatch(new GetPage(pageFilter)).subscribe(data => (this.page = data.page.page));
  }

  onOrderButtonClick() {
    this.feedbackModal.open('Генеалогическое исследование');
  }
}
