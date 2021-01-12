import { ShortLink } from "@models";

export interface PageDto {
  id?: string;
  name?: string;
  title?: string;
  content?: string;
  isRemoved?: boolean;
  isSection?: boolean;
}

export interface PageWithLinksDto {
  name: string;
  title: string;
  content: string;
  linkList: Array<ShortLink>;
  isSection: boolean;
  links: Array<ShortLink>;
}
