export class AddFaultDto{
  title: string;
  description: string;
  itemTypeId: number;
  faultTypeId: number;
  locationId: number;
}

export class FaultDto{
  title: string;
  id:number;
  faultType: string;
  location: string;
  itemType: string;
  createdTime:Date;
  assingTime: Date;
  assignUser: string;
  faultStatus: string
}

export class FaultListDto{
  data:FaultDto[];
  count: number;
  pageIndex: number;
  pageSize: number;
}
