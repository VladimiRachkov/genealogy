import { FetchCatalogItem, FetchCatalogList, GetCatalogItemsCount, UpdateCatalogItem } from '@actions';
import { BusinessObject, BusinessObjectFilter, BusinessObjectOutDto, CatalogItem, Paginator } from '@models';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Select, Store } from '@ngxs/store';
import { CatalogState } from '@states';
import { METATYPE_ID } from 'app/enums/metatype';
import { Pick } from 'app/helpers/json-parse';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { PurchaseComponent } from './purchase/purchase.component';
import { ApiService } from '@core';

const metatypeId = METATYPE_ID.PRODUCT;

@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.scss'],
})
export class CatalogComponent implements OnInit {
  @Select(CatalogState.item)
  item$: Observable<BusinessObject>;

  @Select(CatalogState.list)
  list$: Observable<BusinessObject[]>;

  @ViewChild(PurchaseComponent, { static: false }) purchaseModal: PurchaseComponent;

  products$: Observable<CatalogItem[]>;

  paginatorOptions: Paginator = {
    index: 0,
    step: 10,
    count: null,
  };

  pageIndex: number = 0;
  startIndex: number = 0;

  constructor(private store: Store, private apiService: ApiService) {}

  ngOnInit() {
    this.list$.pipe(tap(() => this.updatePaginator()));

    this.products$ = this.list$.pipe(
      map<BusinessObject[], CatalogItem[]>(list => list.map(({ id, title, data }) => ({ id, title, ...this.parseJSON(data) })))
    );

    this.updatePaginator();
  }

  ngOnDestroy() {}

  onRemove(id: string) {
    this.updateItem({ id, isRemoved: true });
  }

  onRestore(id: string) {
    this.updateItem({ id, isRemoved: false });
  }

  onChangePage(pageIndex: number) {
    const { step } = this.paginatorOptions;

    this.pageIndex = pageIndex;
    this.startIndex = pageIndex * step;
    this.fetchList(pageIndex);
  }

  onCardClick(item: CatalogItem) {
    this.purchaseModal.open(item);
  }

  private fetchList(index: number = this.pageIndex) {
    const { step } = this.paginatorOptions;
    const params: BusinessObjectFilter = { metatypeId, index, step };

    this.paginatorOptions.index = index;

    this.store.dispatch(new FetchCatalogList(params)).subscribe();
  }

  private updateItem(body: BusinessObjectOutDto) {
    this.store.dispatch(new UpdateCatalogItem(body)).subscribe(() => this.fetchList());
  }

  private parseJSON(data: string) {
    return Pick(JSON.parse(data), {
      imageUrl: String,
      price: Number,
      description: String,
    });
  }

  private updatePaginator() {
    const { step } = this.paginatorOptions;

    this.store.dispatch(new GetCatalogItemsCount({ metatypeId })).subscribe(() => {
      const count = this.store.selectSnapshot<number>(CatalogState.count);

      this.paginatorOptions = {
        index: 0,
        step,
        count,
      };

      this.fetchList();
    });
  }
}
