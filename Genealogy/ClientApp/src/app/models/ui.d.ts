export declare namespace Table {
  export interface Data {
    fields: Array<string>;
    items: Array<Item>;
  }

  export interface Item {
    id: string;
    values: Array<string>;
    isRemoved?: boolean;
    status?: string;
  }
}
