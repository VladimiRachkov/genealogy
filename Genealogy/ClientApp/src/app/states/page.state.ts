import { Action, StateContext, State, Selector } from '@ngxs/store';
import { HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { tap, map } from 'rxjs/operators';
import { PageDto, Page, Section, PageWithLinksDto, PageWithLinks } from '@models';
import { FetchPageList, GetPage, AddPage, MarkAsRemovedPage, UpdatePage, AddLink, FetchFreePageList, GetPageWithLinks } from '@actions';
import { ApiService } from '@core';

export interface PageStateModel {
  pageList: Array<PageDto>;
  page: PageDto;
  sections: Array<Section>;
  freePageList: Array<PageDto>;
  pageWithLinks: PageWithLinks;
}

@State<PageStateModel>({
  name: 'page',
  defaults: { pageList: [], page: null, sections: [], freePageList: [], pageWithLinks: null },
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
    return pageList.filter(item => !item.isRemoved) as Array<Page>;
  }

  @Selector()
  static freePageList({ freePageList }: PageStateModel): Array<Page> {
    return freePageList.filter(item => !item.isRemoved) as Array<Page>;
  }

  @Selector()
  static pageWithLinks({ pageWithLinks }: PageStateModel): PageWithLinks {
    return pageWithLinks;
  }

  @Action(FetchPageList)
  fetchPageList(ctx: StateContext<PageStateModel>, { payload: filter }): Observable<Array<PageDto>> {
    const params: HttpParams = filter;
    return this.apiService.get<Array<PageDto>>('page/list', params).pipe(
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
    return this.apiService.post<PageDto>('page', page);
  }

  @Action(MarkAsRemovedPage)
  markAsRemovedPage(ctx: StateContext<PageStateModel>, { payload: id }: MarkAsRemovedPage): Observable<any> {
    const pageDto: PageDto = { id };
    return this.apiService.post<PageDto>('page/remove', pageDto);
  }

  @Action(UpdatePage)
  updatePage(ctx: StateContext<PageStateModel>, { payload: Page }: UpdatePage): Observable<any> {
    return this.apiService.put<PageDto>('page', Page);
  }

  @Action(FetchFreePageList)
  fetchFreePageList(ctx: StateContext<PageStateModel>, { payload: pageId }: FetchFreePageList): Observable<any> {
    return this.apiService.get<Array<PageDto>>('page/list', null).pipe(
      map(pageList => pageList.filter(page => page.id !== pageId)),
      tap(pageList => ctx.patchState({ freePageList: pageList }))
    );
  }

  @Action(GetPageWithLinks)
  getPageWithLinks(ctx: StateContext<PageStateModel>, { payload: filter }): Observable<PageWithLinksDto> {
    const params: HttpParams = filter;
    return this.apiService.get<PageWithLinksDto>('page/withlinks', params).pipe(
      map(page => page as PageWithLinks),
      tap(pageWithLinks => ctx.patchState({ pageWithLinks }))
    );
  }
}
