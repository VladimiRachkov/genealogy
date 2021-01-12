import { Component, OnInit } from '@angular/core';
import { FetchUserList } from '@actions';
import { Store } from '@ngxs/store';
import { User } from '@models';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit {
  ngOnInit() {}
}
