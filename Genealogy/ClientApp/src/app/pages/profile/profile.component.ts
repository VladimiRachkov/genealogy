import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '@core';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  constructor(private authenticationService: AuthenticationService) {}

  ngOnInit() {}

  onLogout() {
    this.authenticationService.logout();
  }
}
