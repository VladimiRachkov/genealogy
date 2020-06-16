import { Component, OnInit } from '@angular/core';
import { Store } from '@ngxs/store';
import { FetchPersonList, ClearPersonList } from '@act/person.actions';
import { Person } from '@mdl/person';
import { PersonState } from '@state/person.state';
import { Validators, FormControl, FormGroup } from '@angular/forms';
import { PersonFilter } from '@mdl/filters/person.filter';
import { NotifierService } from 'angular-notifier';

@Component({
  selector: 'app-necropolis',
  templateUrl: './necropolis.component.html',
  styleUrls: ['./necropolis.component.scss'],
})
export class NecropolisComponent implements OnInit {
  personList: Array<Person> = null;
  searchForm: FormGroup;
  hasAuth: boolean = false;

  constructor(private store: Store) {}

  ngOnInit() {
    this.searchForm = new FormGroup({
      fio: new FormControl(null, [Validators.required]),
    });
    this.store.dispatch(new ClearPersonList());
  }

  search() {
    const fio = <string>this.searchForm.value.fio.toLowerCase();
    if (fio.trim() !== '') {
      const filter: PersonFilter = { fio };
      console.log(fio, filter);
      this.store.dispatch(new FetchPersonList(filter)).subscribe(data => {
        this.personList = this.store.selectSnapshot<Array<Person>>(PersonState.personList);
        console.log('PERSON LIST', this.personList);
      });
    } else {
      //Предупреждение
    }
  }
  open() {
    this.hasAuth = true;
  }
}
