import { Component, OnInit } from '@angular/core';
import { AuthenticationService, ApiService } from '@core';
import { Store } from '@ngxs/store';
import { FetchActiveSubscribe, FetchPurchases, GetUser } from '@actions';
import { UserState } from 'app/states/user.state';
import { BusinessObject, User } from '@models';
import { environment } from '@env/environment';
import { MainState } from '@states';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  user: User;
  subscription: BusinessObject;
  hasSubscription = false;
  purchases: BusinessObject[];

  constructor(private authenticationService: AuthenticationService, private store: Store, private apiService: ApiService) {}

  ngOnInit() {
    const currentUser = this.authenticationService.currentUserValue;
    this.store.dispatch(new GetUser({ id: currentUser.id })).subscribe(() => {
      this.user = this.store.selectSnapshot<User>(UserState.user);
    });

    this.store.dispatch(new FetchActiveSubscribe()).subscribe(() => {
      this.subscription = this.store.selectSnapshot(MainState.subscription);
      this.hasSubscription = this.store.selectSnapshot(MainState.hasSubscription);
    });

    this.store.dispatch(new FetchPurchases()).subscribe(() => {
      this.purchases = this.store.selectSnapshot(MainState.purchases);
    });
  }

  onLogout() {
    this.authenticationService.logout();
  }

  onPayment() {
    const params: any = { returnUrl: environment.apiUrl + '/shop/success' };
    this.apiService.get<string>('/shop/payment', params).subscribe(res => window.open(res as string));
  }
}
