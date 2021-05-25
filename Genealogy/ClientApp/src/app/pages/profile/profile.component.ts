import { Component, OnInit } from '@angular/core';
import { AuthenticationService, ApiService } from '@core';
import { Store, Select } from '@ngxs/store';
import { GetUser } from '@actions';
import { UserState } from 'app/states/user.state';
import { Observable } from 'rxjs';
import { User } from '@models';
import { HttpParams } from '@angular/common/http';
import { environment } from '@env/environment';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  user: User;

  constructor(private authenticationService: AuthenticationService, private store: Store, private apiService: ApiService) {}

  ngOnInit() {
    const currentUser = this.authenticationService.currentUserValue;
    this.store.dispatch(new GetUser({ id: currentUser.id })).subscribe(() => {
      this.user = this.store.selectSnapshot<User>(UserState.user);
      console.log(this.user);
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
