import { FetchMessage, FetchMessageList, GetMessagesCount, UpdateMessage } from '@actions';
import { DatePipe } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { BusinessObject, BusinessObjectFilter, BusinessObjectOutDto, Paginator, Table } from '@models';
import { Select, Store } from '@ngxs/store';
import { ModalComponent } from '@shared';
import { METATYPE_ID } from 'app/enums/metatype';
import { Pick } from 'app/helpers/json-parse';
import { MailState } from 'app/states/mail.state';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

const metatypeId = METATYPE_ID.MESSAGE;

@Component({
  selector: 'dashboard-mail',
  templateUrl: './mail.component.html',
  styleUrls: ['./mail.component.scss'],
  providers: [DatePipe],
})
export class MailComponent implements OnInit {
  @ViewChild(ModalComponent, { static: false }) purchaseModal: ModalComponent;

  @Select(MailState.item)
  item$: Observable<BusinessObject>;

  messageText: string;

  @Select(MailState.list)
  list$: Observable<BusinessObject[]>;

  tableData$: Observable<Table.Data>;

  paginatorOptions: Paginator = {
    index: 0,
    step: 10,
    count: null,
  };

  pageIndex: number = 0;
  startIndex: number = 0;

  constructor(private store: Store, private datePipe: DatePipe) {}

  ngOnInit() {
    this.updatePaginator();

    this.tableData$ = this.list$.pipe(
      map<BusinessObject[], Table.Item[]>(list =>
        list.map(({ id, title, data, isRemoved, startDate }) => {
          const props = this.parseJSON(data);

          const date = this.datePipe.transform(startDate.toString(), 'dd.MM.yyyy');
          const time = this.datePipe.transform(startDate.toString(), 'HH:mm');

          return { id, values: [title, props.username, props.email, date, time], isRemoved };
        })
      ),
      map<Table.Item[], Table.Data>(items => ({
        fields: ['Сообщение', 'От кого', 'Email', 'Дата', 'Время'],
        items,
      }))
    );
  }

  onSelect(id: string) {
    const filter: BusinessObjectFilter = { id };
    this.store.dispatch(new FetchMessage(filter)).subscribe(() => {
      const message = this.store.selectSnapshot(MailState.item);
      const props = this.parseJSON(message.data);
      this.messageText = props.message;
      this.purchaseModal.open();
    });
  }

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

  private updateItem(body: BusinessObjectOutDto) {
    this.store.dispatch(new UpdateMessage(body)).subscribe(() => this.fetchList());
  }

  private parseJSON(data: string) {
    return Pick(JSON.parse(data), {
      username: String,
      email: String,
      message: String,
    });
  }

  private fetchList(index: number = this.pageIndex) {
    const { step } = this.paginatorOptions;
    const params: BusinessObjectFilter = { metatypeId, index, step };

    this.paginatorOptions.index = index;

    this.store.dispatch(new FetchMessageList(params)).subscribe();
  }

  private updatePaginator() {
    const { step } = this.paginatorOptions;

    this.store.dispatch(new GetMessagesCount({ metatypeId })).subscribe(() => {
      const count = this.store.selectSnapshot<number>(MailState.count);

      this.paginatorOptions = {
        index: 0,
        step,
        count,
      };

      this.fetchList();
    });
  }
}
