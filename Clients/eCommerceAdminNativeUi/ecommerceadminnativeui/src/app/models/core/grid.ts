export class FilterModel {
  propertyName!: string;
  filterType!: string;
  filterValue!: string;
  jsonOrXml!: boolean;
  andOrOperation: string = 'And';
}
export class ValueText {
  value!: string;
  text!: string;
}
export class GridSettingsDTO {
  id!: string;
  path!: string;
  propertyInfo!: string;
}
export class GridPropertyInfo {
  propertyName!: string;
  propertyType!: string;
  checked!: boolean;
}
export class GridPostData {
  constructor(PageIndex: number, PageSize: number) {
    this.pageIndex = PageIndex;
    this.pageSize = PageSize;
  }
  pageIndex: number;
  pageSize: number;
  orderByColumnName: string = '';
  filterModelList!: FilterModel[];
}
export enum FilterOperators {
  Equals = 1,
  NotEquals = 2,
  Contains = 3,
  GreaterThan = 5,
  LessThan = 6,
}
export class PagedList<T> {
  data: T[] = [];
  pageIndex!: number;
  pageSize!: number;
  totalCount!: number;
  totalPages!: number;
  propertyInfos: GridPropertyInfo[] = [];
}
