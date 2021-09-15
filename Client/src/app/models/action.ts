import {faAngleLeft, faDatabase, faFilter, faLink} from "@fortawesome/free-solid-svg-icons";

export enum ActionType {
    join = 0,
    filter = 1,
    aggregate = 2
}
  
export class ActionConfig {
}

export class JoinActionConfig extends ActionConfig {

}
export class FilterActionConfig extends ActionConfig {
    
}
export class AggregateActionConfig extends ActionConfig {
    
}

export default class ActionItem {
    type: ActionType;
    config: ActionConfig;

    constructor(type: ActionType, config: ActionConfig) {
        this.type = type;
        this.config = config;
    }

    getPersianName(): string {
        switch (this.type) {
            case ActionType.join: return "الحاق";
            case ActionType.filter: return "فیلتر";
            case ActionType.aggregate: return "جمع‌آوری";
            default: return "";
        }
    }

    getColor(): string {
        switch (this.type) {
            case ActionType.join: return "green";
            case ActionType.filter: return "red";
            case ActionType.aggregate: return "blue";
            default: return "gray";
        }
    }

    getIcon(): any {
        switch (this.type) {
            case ActionType.join: return faLink;
            case ActionType.filter: return faFilter;
            case ActionType.aggregate: return faDatabase;
            default: return faDatabase;
        }
    }
}