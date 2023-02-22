import {
  ActivatePurchase,
  AddCatalogItem,
  FetchCatalogItem,
  FetchCatalogList,
  FetchPurchaseList,
  GetCatalogItemsCount,
  RemovePurchase,
  UpdateCatalogItem,
} from '@actions';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { BusinessObject, BusinessObjectFilter, BusinessObjectOutDto, Paginator, Table } from '@models';
import { Select, Store } from '@ngxs/store';
import { CatalogState } from '@states';
import { METATYPE_ID } from 'app/enums/metatype';
import { Observable } from 'rxjs';
import { map, switchMap, tap } from 'rxjs/operators';
import { Pick } from 'app/helpers/json-parse';
import { UntilDestroy } from '@ngneat/until-destroy';
import { ModalComponent } from '@shared';
import { DatePipe } from '@angular/common';
import { isNil} from 'lodash';

const metatypeId = METATYPE_ID.PRODUCT;

@UntilDestroy()
@Component({
  selector: 'dashboard-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.scss'],
  providers: [DatePipe],
})
export class CatalogComponent implements OnInit, OnDestroy {
  @ViewChild(ModalComponent, { static: false }) purchaseModal: ModalComponent;

  @Select(CatalogState.item)
  item$: Observable<BusinessObject>;

  @Select(CatalogState.list)
  list$: Observable<BusinessObject[]>;

  @Select(CatalogState.purchaseList)
  purchaseList$: Observable<BusinessObject[]>;

  tableData$: Observable<Table.Data>;
  purchaseTableData$: Observable<Table.Data>;

  catalogForm: FormGroup;

  paginatorOptions: Paginator = {
    index: 0,
    step: 10,
    count: null,
  };

  pageIndex: number = 0;
  startIndex: number = 0;

  showUnprocessedPurchases: boolean = false;

  constructor(private store: Store, private datePipe: DatePipe) {}

  ngOnInit() {
    this.list$.pipe(tap(() => this.updatePaginator()));

    this.tableData$ = this.list$.pipe(
      map<BusinessObject[], Table.Item[]>(list =>
        list.map(({ id, title, data, isRemoved }) => {
          const props = this.parseJSON(data);
          return { id, values: [title, props.price], isRemoved };
        })
      ),
      map<Table.Item[], Table.Data>(items => ({
        fields: ['Название', 'Цена'],
        items,
      }))
    );

    this.purchaseTableData$ = this.purchaseList$.pipe(
      map<BusinessObject[], Table.Item[]>(list =>
        list.map(({ id, title, data, isRemoved, startDate }) => {
          const props = this.parsePurchaseDataJSON(data);

          const date = this.datePipe.transform(startDate.toString(), 'dd.MM.yyyy');
          const time = this.datePipe.transform(startDate.toString(), 'HH:mm');

          return { id, values: [title, props.username, props.email, date, time], isRemoved, status: props.status };
        }).filter(item => !isNil(item))
      ),
      map<Table.Item[], Table.Data>(items => ({
        fields: ['Название', 'Покупатель', 'Почта', 'Дата', 'Время'],
        items
      }))
    );

    this.catalogForm = new FormGroup({
      id: new FormControl(null),
      title: new FormControl(null, null),
      imageUrl: new FormControl(null, null),
      price: new FormControl(null, null),
      description: new FormControl(null, null),
      message: new FormControl(null, null),
    });

    this.updatePaginator();
  }

  ngOnDestroy() {}

  onAdd() {
    const { title, imageUrl, price, description } = this.catalogForm.value;

    const props = { imageUrl, price, description };
    const data = JSON.stringify(props);
    const body = { title, data, metatypeId };

    this.store.dispatch(new AddCatalogItem(body)).subscribe(() => this.fetchList());
  }

  onSelect(id: string) {
    const params: BusinessObjectFilter = { id };
    this.store.dispatch(new FetchCatalogItem(params)).subscribe(() => {
      const item = this.store.selectSnapshot<BusinessObject>(CatalogState.item);
      const { id, title, data } = item;
      const { imageUrl, price, description, message } = this.parseJSON(data);

      this.catalogForm.setValue({ id, title, imageUrl, price, description, message });
    });
  }

  onRemove(id: string) {
    this.updateItem({ id, isRemoved: true });
  }

  onRestore(id: string) {
    this.updateItem({ id, isRemoved: false });
  }

  onUpdate() {
    const { title, imageUrl, price, description, message } = this.catalogForm.value;
    const { id } = this.store.selectSnapshot<BusinessObject>(CatalogState.item);
    const data = JSON.stringify({ imageUrl, price, description, message });

    this.updateItem({ id, title, data });
  }

  onChangePage(pageIndex: number) {
    const { step } = this.paginatorOptions;

    this.pageIndex = pageIndex;
    this.startIndex = pageIndex * step;
    this.fetchList(pageIndex);
  }

  onClickPurchaseButton() {
    this.showUnprocessedPurchases = false;
    this.showPurchases();
  }

  onClickUnprocessedPurchaseButton() {
    this.showUnprocessedPurchases = true;
    this.showPurchases();
  }

  private showPurchases() {
    const isRemoved = !this.showUnprocessedPurchases
    this.fetchPurchaseList(isRemoved).subscribe(() => this.purchaseModal.open());
  }

  onActivatePurchase(id: string) {
    const isRemoved = !this.showUnprocessedPurchases;
    const params: BusinessObjectFilter = { id };
    this.store.dispatch(new ActivatePurchase(params)).pipe(switchMap(() => this.fetchPurchaseList(isRemoved))).subscribe();
  }

  onRemovePurchase(id: string) {
    const isRemoved = !this.showUnprocessedPurchases
    this.store.dispatch(new RemovePurchase(id)).pipe(switchMap(() => this.fetchPurchaseList(isRemoved))).subscribe();
  }

  private fetchPurchaseList(isRemoved: boolean): Observable<void> {
    console.log(isRemoved)
    const params: BusinessObjectFilter = { metatypeId: METATYPE_ID.PURCHASE, isRemoved: isRemoved };
    return this.store.dispatch(new FetchPurchaseList(params));
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
      price: String,
      description: String,
      message: String,
    });
  }

  private parsePurchaseDataJSON(data: string) {
    return Pick(JSON.parse(data), {
      username: String,
      email: String,
      status: String,
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
