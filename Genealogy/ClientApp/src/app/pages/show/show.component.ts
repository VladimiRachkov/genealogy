import { Component, OnInit } from '@angular/core';
import { Store } from '@ngxs/store';
import { GetPage } from '@actions';
import { PageDto, PageFilter } from '@models';
import { ActivatedRoute } from '@angular/router';
import { PageState } from '@states';

@Component({
  selector: 'app-show',
  templateUrl: './show.component.html',
  styleUrls: ['./show.component.scss'],
})
export class ShowComponent implements OnInit {
  pageName = '';
  content: string = null;
  constructor(private store: Store, private activateRoute: ActivatedRoute) {}

  ngOnInit() {
    this.activateRoute.params.subscribe(params => {
      const name = params['name'];
      this.fetchPage(name);
    });
    const name = this.activateRoute.snapshot.params['name'];
    this.fetchPage(name);
  }

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
