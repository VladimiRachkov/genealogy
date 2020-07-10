import { Action, StateContext, State, Selector } from '@ngxs/store';
import { MarkAsRemovedPage, AddPage, GetPage, FetchPageList, UpdatePage } from '@act/page.actions';
import { HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PageDto } from '@mdl/dtos/page.dto';
import { Page } from '@mdl/page';
import { Injectable } from '@angular/core';
import { ApiService } from '@srv/api.service';
import { tap } from 'rxjs/operators';

export interface PageStateModel {
  pageList: Array<PageDto>;
  page: PageDto;
}

@State<PageStateModel>({
  name: 'page',
  defaults: { pageList: [], page: null },
})
@Injectable()
export class PageState {
  constructor(private apiService: ApiService) {}

  @Selector()
  static page({ page }: PageStateModel): Page {
    return page as Page;
  }

  @Selector()
  static pageList({ pageList }: PageStateModel): Array<Page> {
    return pageList.filter(item => !item.removed) as Array<Page>;
  }

  @Action(FetchPageList)
  fetchPageList(ctx: StateContext<PageStateModel>, { payload: filter }): Observable<Array<PageDto>> {
    const params: HttpParams = filter;
    return this.apiService.get<Array<PageDto>>('page', params).pipe(
      tap(data => console.log('FETCH', data)),
      tap(pageList => ctx.patchState({ pageList }))
    );
  }

  @Action(GetPage)
  getPage(ctx: StateContext<PageStateModel>, { payload: filter }): Observable<Array<PageDto>> {
    const params: HttpParams = filter;
    return this.apiService.get<Array<PageDto>>('page', params).pipe(tap(pageList => ctx.patchState({ page: pageList[0] })));
  }

  @Action(AddPage)
  addPage(ctx: StateContext<PageStateModel>, { payload: page }: AddPage): Observable<any> {
    return this.apiService.post<PageDto>('page', page).pipe(tap(data => console.log('ADD', data)));
  }

  @Action(MarkAsRemovedPage)
  markAsRemovedPage(ctx: StateContext<PageStateModel>, { payload: id }: MarkAsRemovedPage): Observable<any> {
    const pageDto: PageDto = { id };
    return this.apiService.post<PageDto>('page/markasremoved', pageDto);
  }

  @Action(UpdatePage)
  updatePage(ctx: StateContext<PageStateModel>, { payload: Page }: UpdatePage): Observable<any> {
    console.log('Page', Page);
    return this.apiService.put<PageDto>('page', Page);
  }
}
