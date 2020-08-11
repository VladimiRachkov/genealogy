export interface Link {
  id?: string;
  caption?: string;
  pageId?: string;
  targetPageId?: string;
  order?: number;
  route?: string;
}

export interface ShortLink {
  caption: string;
  route: string;
  order: number;
}
