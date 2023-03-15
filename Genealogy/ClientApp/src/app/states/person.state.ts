import { State, Selector, StateContext, Action } from '@ngxs/store';
import { Injectable } from '@angular/core';
import { FetchPersonList, AddPerson, UpdatePerson, ClearPersonList, GetPersonsCount, FetchPerson, FetchAllPersons } from '@actions';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { HttpParams } from '@angular/common/http';
import { PersonDto, Person, PersonOutDto, CountInDto } from '@models';
import { ApiService } from '@core';
import { first } from 'lodash';

export interface PersonStateModel {
  personList: Array<PersonDto>;
  person: PersonDto;
  count: number;
}

const apiUrl = 'persons';

@State<PersonStateModel>({
  name: 'person',
  defaults: { personList: [], person: null, count: 0 },
})
@Injectable()
export class PersonState {
  constructor(private apiService: ApiService) {}

  @Selector()
  static person({ person }: PersonStateModel): Person {
    return person as Person;
  }

  @Selector()
  static personList({ personList }: PersonStateModel): Array<Person> {
    return personList as Array<Person>;
  }

  @Selector()
  static allPersonsCount({ count }: PersonStateModel): number {
    return count;
  }

  @Action(FetchPersonList)
  fetchPersonList(ctx: StateContext<PersonStateModel>, { payload: filter }): Observable<Array<PersonDto>> {
    const params: HttpParams = filter;
    return this.apiService.get<Array<PersonDto>>(apiUrl, params).pipe(tap(personList => ctx.patchState({ personList })));
  }

  @Action(ClearPersonList)
  clearPersonList(ctx: StateContext<PersonStateModel>) {
    ctx.patchState({ personList: [] });
  }

  @Action(FetchPerson)
  getPerson(ctx: StateContext<PersonStateModel>, { payload: filter }): Observable<Array<PersonDto>> {
    const params: HttpParams = filter;
    return this.apiService.get<Array<PersonDto>>(apiUrl, params).pipe(tap(personList => ctx.patchState({ person: first(personList) })));
  }

  @Action(FetchAllPersons)
  getAllPersons(ctx: StateContext<PersonStateModel>, { payload: filter }): Observable<Array<PersonDto>> {
    const params: HttpParams = filter;
    const url = apiUrl + '/all';

    return this.apiService.get<Array<PersonDto>>(url, params).pipe(tap(personList => ctx.patchState({ personList   })));
  }

  @Action(AddPerson)
  addPerson(ctx: StateContext<PersonStateModel>, { payload: person }: AddPerson): Observable<any> {
    return this.apiService.post<PersonDto>(apiUrl, person);
  }

  @Action(UpdatePerson)
  updatePerson(ctx: StateContext<PersonStateModel>, { payload: person }: UpdatePerson): Observable<any> {
    return this.apiService.put<PersonDto>(apiUrl, person);
  }

  @Action(GetPersonsCount)
  getPersonsCount(ctx: StateContext<PersonStateModel>): Observable<CountInDto> {
    return this.apiService.get<CountInDto>(`${apiUrl}/count`, null).pipe(tap(res => ctx.patchState({ count: res.count })));
  }
}
