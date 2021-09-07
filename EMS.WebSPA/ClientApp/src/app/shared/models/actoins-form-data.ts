import { TreeActionType } from 'app/shared/models/tree.model';

export class ActionsFormData {
    constructor(
        public actionType: TreeActionType,
        public selectedItem: any,
        public showActionsForm: boolean
    ) { }
}