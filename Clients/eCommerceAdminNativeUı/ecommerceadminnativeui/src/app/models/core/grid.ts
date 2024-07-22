export class FilterModel {
  propertyName!: string;
  filterType!: string;
  filter!: string;
  jsonOrXml!: boolean;
  andOrOperation!: string;
}
export class ValueText {
  value!: string;
  text!: string;
}
export class GridSettingsDTO {
  id!: number;
  path!: string;
  propertyInfo!: string;
}
export class GridPropertyInfo {
  propertyName!: string;
  propertyType!: string;
  checked!: boolean;
  attrFilterName!: string;
}
export class GridPostData {
  constructor(PageIndex: number, PageSize: number) {
    this.pageIndex = PageIndex;
    this.pageSize = PageSize;
  }
  pageIndex: number;
  pageSize: number;
  orderByColumnName!: string;
  filterModelList!: FilterModel[];
}
