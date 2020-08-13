import { Component, OnInit, OnDestroy } from '@angular/core';
import { Store, Select } from '@ngxs/store';
import { FetchPageList, SetAuthorization } from '@actions';
import { PageFilter, Section, Page } from '@models';
import { PageState, MainState } from '@states';
import { AuthenticationService } from '@core';
import { untilDestroyed } from 'ngx-take-until-destroy';
import { Observable } from 'rxjs';
import { isNil } from 'lodash'; 

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent implements OnInit, OnDestroy {
  sections: Array<Section>;
  isAdmin = false;
  currentUser: any = null;

  @Select(MainState.adminMode) adminMode$: Observable<boolean>;
  @Select(MainState.hasAuth) hasAuth$: Observable<boolean>;

  constructor(private store: Store, private authenticationService: AuthenticationService) {}

  ngOnInit() {
    const filter: PageFilter = { isSection: true };
    this.store.dispatch(new FetchPageList(filter)).subscribe(
      () =>
        (this.sections = this.store
          .selectSnapshot<Array<Page>>(PageState.pageList)
          .filter(item => item.name !== 'index')
          .map<Section>(item => item))
    );
    const currentUser = this.authenticationService.currentUserValue;
    this.store.dispatch(new SetAuthorization(!isNil(currentUser)));


    if (currentUser) {
      this.authenticationService
        .checkAdmin()
        .pipe(untilDestroyed(this))
        .subscribe(value => (this.isAdmin = value));
    }
  }

  ngOnDestroy() {}
}
