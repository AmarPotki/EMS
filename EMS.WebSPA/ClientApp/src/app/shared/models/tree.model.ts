
export class Tree {
    constructor(
        public id: string,
        public name: string,
        public parentId: string
    ) { }
}

export enum TreeActionType{
    AddNode,
    EditNode,
    RemoveNode
}