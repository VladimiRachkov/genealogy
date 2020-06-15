import { Component, OnInit } from '@angular/core';
import { Store, Select } from '@ngxs/store';
import { FetchPersonList, ClearPersonList } from '@act/person.actions';
import { Person } from '@mdl/person';
import { PersonState } from '@state/person.state';
import { Observable } from 'rxjs';
import { Validators, FormControl, FormGroup } from '@angular/forms';
import { PersonFilter } from '@mdl/filters/person.filter';

@Component({
  selector: 'app-gako',
  templateUrl: './gako.component.html',
  styleUrls: ['./gako.component.css'],
})
export class GakoComponent implements OnInit {
  personList: Array<Person> = null;
  searchForm: FormGroup;

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
}
