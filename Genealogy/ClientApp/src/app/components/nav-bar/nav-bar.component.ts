import { Component, OnInit } from '@angular/core';
import { Store } from '@ngxs/store';
import { FetchPageList } from '@actions';
import { PageFilter, Section, Page } from '@models';
import { PageState } from '@states';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent implements OnInit {
  sections: Array<Section>;

  constructor(private store: Store) {}

  ngOnInit() {
    const filter: PageFilter = { isSection: true };
    this.store.dispatch(new FetchPageList(filter)).subscribe(
      () =>
        (this.sections = this.store
          .selectSnapshot<Array<Page>>(PageState.pageList)
          .filter(item => item.name !== 'start')
          .map<Section>(item => item))
    );
  }
}
