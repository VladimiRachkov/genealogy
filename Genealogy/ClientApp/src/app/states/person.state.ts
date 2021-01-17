import { State, Selector, StateContext, Action } from '@ngxs/store';
import { Injectable } from '@angular/core';
import { FetchPersonList, GetPerson, AddPerson, MarkAsRemovedPerson, UpdatePerson, ClearPersonList, GetPersonsCount } from '@actions';
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

  @Action(FetchPersonList)
  fetchPersonList(ctx: StateContext<PersonStateModel>, { payload: filter }): Observable<Array<PersonDto>> {
    const params: HttpParams = filter;
    return this.apiService.get<Array<PersonDto>>('person', params).pipe(tap(personList => ctx.patchState({ personList })));
  }

  @Action(ClearPersonList)
  clearPersonList(ctx: StateContext<PersonStateModel>) {
    ctx.patchState({ personList: [] });
  }

  @Action(GetPerson)
  getPerson(ctx: StateContext<PersonStateModel>, { payload: filter }): Observable<Array<PersonDto>> {
    const params: HttpParams = filter;
    return this.apiService.get<Array<PersonDto>>('person', params).pipe(tap(personList => ctx.patchState({ person: first(personList) })));
  }

  @Action(AddPerson)
  addPerson(ctx: StateContext<PersonStateModel>, { payload: person }: AddPerson): Observable<any> {
    return this.apiService.post<PersonDto>('person', person).pipe(tap(data => console.log('ADD', data)));
  }

  @Action(MarkAsRemovedPerson)
  markAsRemovedPerson(ctx: StateContext<PersonStateModel>, { payload: id }: MarkAsRemovedPerson): Observable<any> {
    const personDto: PersonOutDto = { id };
    return this.apiService.post<PersonDto>('person/remove', personDto);
  }

  @Action(UpdatePerson)
  updatePerson(ctx: StateContext<PersonStateModel>, { payload: person }: UpdatePerson): Observable<any> {
    console.log('PERSON', person);
    return this.apiService.put<PersonDto>('person', person);
  }

  @Action(GetPersonsCount)
  getPersonsCount(ctx: StateContext<PersonStateModel>): Observable<CountInDto> {
    return this.apiService.get<CountInDto>('person/count', null).pipe(tap(res => ctx.patchState({ count: res.count })));
  }
}
