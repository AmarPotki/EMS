
export class ItemTypeDto {
    id: number;
    name: string;
    ParentId: number;
    hasChildren: boolean;
}

export class CreateItemTypeCommand {
    Name: string;
    ParentId: number;
}


 