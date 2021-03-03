import { Component, OnInit, OnDestroy } from '@angular/core';
import { Store } from '@ngxs/store';
import { GetPageWithLinks, GetPage } from '@actions';
import { PageWithLinks, Page, ShortLink } from '@models';
import { ActivatedRoute, ActivatedRouteSnapshot } from '@angular/router';
import { untilDestroyed } from 'ngx-take-until-destroy';
import { PageState } from '@states';
import { StateReset } from 'ngxs-reset-plugin';
import { isEmpty } from 'lodash';

@Component({
  selector: 'app-viewer',
  templateUrl: './page-viewer.component.html',
  styleUrls: ['./page-viewer.component.scss'],
})
export class PageViewerComponent implements OnInit, OnDestroy {
  isSection: boolean;
  title: string;
  links: Array<ShortLink>;
  content: string;
  parent: string;
  child: string;

  page: PageWithLinks;

  constructor(private store: Store, private activateRoute: ActivatedRoute) {}

  ngOnInit() {
    this.store.dispatch(new StateReset(PageState));

    this.activateRoute.params.pipe(untilDestroyed(this)).subscribe(params => {
      const { parent, child } = params;

      if (this.parent !== parent) {
        this.parent = parent;
        this.fetchParent();
      }

      if (this.child !== child) {
        this.child = child;
        if (isEmpty(child)) {
          this.fetchParent();
        } else {
          this.fetchChild();
        }
      }
    });
  }

  private fetchParent() {
    this.store.dispatch(new GetPageWithLinks({ name: this.parent })).subscribe(() => {
      this.page = this.store.selectSnapshot<PageWithLinks>(PageState.pageWithLinks);
    });
  }

  private fetchChild() {
    this.store.dispatch(new GetPage({ name: this.child })).subscribe(() => {
      const page = this.store.selectSnapshot<Page>(PageState.page);
      this.page = { ...this.page, content: page.content, title: page.title };
    });
  }

  ngOnDestroy() {}
}
