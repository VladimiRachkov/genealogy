import { Link, LinkDto } from '@models';
import { State, Action, StateContext, Selector } from '@ngxs/store';
import { Injectable } from '@angular/core';
import { AddLink, FetchLinkList, UpdateLinkList } from '@actions';
import { Observable } from 'rxjs';
import { tap, map } from 'rxjs/operators';
import { HttpParams } from '@angular/common/http';
import { ApiService } from '@core';

export interface LinkStateModel {
  linkList: Array<Link>;
}

@State<LinkStateModel>({
  name: 'link',
  defaults: { linkList: [] },
})
@Injectable()
export class LinkState {
  constructor(private apiService: ApiService) {}

  @Selector()
  static linkList({ linkList }: LinkStateModel): Array<Link> {
    return linkList as Array<Link>;
  }

  @Action(AddLink)
  addLink(ctx: StateContext<LinkStateModel>, { payload: link }: AddLink): Observable<any> {
    return this.apiService.post<LinkDto>('link', link).pipe(
      map<LinkDto, Link>(item => item),
      tap(linkList => ctx.patchState({ linkList }))
    );
  }

  @Action(FetchLinkList)
  fetchLinkList(ctx: StateContext<LinkStateModel>, { payload: filter }): Observable<any> {
    const params: HttpParams = filter;
    return this.apiService.get<LinkDto>('link', params).pipe(
      map(item => item as Link),
      tap(linkList => ctx.patchState({ linkList }))
    );
  }

  @Action(UpdateLinkList)
  updateLinkList(ctx: StateContext<LinkStateModel>, { payload: links }): Observable<any> {
    return this.apiService
      .post<{ links: Array<LinkDto> }>('link/list', { links })
      .pipe(
        map(item => item as Link),
        tap(linkList => ctx.patchState({ linkList }))
      );
  }
}
