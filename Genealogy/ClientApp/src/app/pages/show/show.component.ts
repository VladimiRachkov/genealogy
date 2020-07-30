import { Component, OnInit, OnDestroy } from '@angular/core';
import { Store } from '@ngxs/store';
import { GetPage } from '@actions';
import { PageFilter } from '@models';
import { ActivatedRoute } from '@angular/router';
import { untilDestroyed } from 'ngx-take-until-destroy';

@Component({
  selector: 'app-show',
  templateUrl: './show.component.html',
  styleUrls: ['./show.component.scss'],
})
export class ShowComponent implements OnInit, OnDestroy {
  pageName = '';
  content: string = null;
  constructor(private store: Store, private activateRoute: ActivatedRoute) {}

  ngOnInit() {
    this.activateRoute.params.pipe(untilDestroyed(this)).subscribe(params => {
      const name = params['name'];
      this.fetchPage(name);
    });
    const name = this.activateRoute.snapshot.params['name'];
    this.fetchPage(name);
  }

  ngOnDestroy() {}

  fetchPage(name: string) {
    const filter: PageFilter = { name };
    this.store.dispatch(new GetPage(filter)).subscribe(data => {
      const { page } = data.page;
      this.content = page.content;
      const { id, title, name } = page;
      this.content = page.content;
      console.log('PAGE', page);
    });
  }
}
