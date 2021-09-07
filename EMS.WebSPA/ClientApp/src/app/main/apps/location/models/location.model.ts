
export class LocationDto {
    id: number;
    name: string;
    ParentId: number;
    hasChildren: boolean;
}

export class CreateLocationCommand {
    Name: string;
    ParentId: number;
}


 