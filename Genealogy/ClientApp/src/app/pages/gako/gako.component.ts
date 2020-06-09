import { Component, OnInit } from '@angular/core';
import { Store, Select } from '@ngxs/store';
import { FetchPersonList, GetPerson } from '@act/person.actions';
import { Person } from '@mdl/person';
import { PersonState } from '@state/person.state';
import { Observable } from 'rxjs';
import { Validators, FormControl, FormGroup } from '@angular/forms';
import { PersonFilter } from '@mdl/filters/person.filter';
import { PersonDto } from '@mdl/dtos/person.dto';

@Component({
  selector: 'app-gako',
  templateUrl: './gako.component.html',
  styleUrls: ['./gako.component.css'],
})
export class GakoComponent implements OnInit {
  @Select(PersonState.personList) personList$: Observable<Array<Person>>;

  searchForm: FormGroup;

  constructor(private store: Store) {}

  ngOnInit() {
    this.searchForm = new FormGroup({
      fio: new FormControl(null, [Validators.required]),
    });
  }

  search() {
    const fio = <string>this.searchForm.value.fio.toLowerCase();
    const filter: PersonFilter = { fio };
    console.log(fio, filter)
    this.store.dispatch(new FetchPersonList(filter)).subscribe(() => {
      console.log('GET')
    });
  }
}
