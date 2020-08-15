import { ShortLink } from "@models";

export interface Page {
  id: string;
  name: string;
  title: string;
  content: string;
  isRemoved: boolean;
  isSection: boolean;
}

export interface PageWithLinks {
  name: string;
  title: string;
  content: string;
  linkList: Array<ShortLink>;
  isSection: boolean;
}
