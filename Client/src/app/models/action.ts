import {faAngleLeft, faCalculator, faDatabase, faFilter, faLink} from "@fortawesome/free-solid-svg-icons";

export enum ActionType {
    join = 0,
    filter = 1,
    aggregate = 2,
    calculate = 3,
}
  
export class ActionConfig {
}

export class JoinActionConfig extends ActionConfig {
    
}
export class FilterActionConfig extends ActionConfig {
    columnName = "";
    columnEqual = "";
    columnMore = "";
    columnLess = "";
}
export class AggregateActionConfig extends ActionConfig {
    groupColumn = "";
    addColumn = "";
}
export class CalculateActionConfig extends ActionConfig {
    type = CalculateType.add;
    firstOperand = "";
    secondOperand = "";
}

export enum CalculateType {
    add = 0,
    subtract = 1,
    multiply = 2,
    divide = 3
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
            case ActionType.calculate: return "محاسبه";
            default: return "";
        }
    }

    getColor(): string {
        switch (this.type) {
            case ActionType.join: return "green";
            case ActionType.filter: return "orangered";
            case ActionType.aggregate: return "blue";
            case ActionType.calculate: return "purple";
            default: return "gray";
        }
    }

    getIcon(): any {
        switch (this.type) {
            case ActionType.join: return faLink;
            case ActionType.filter: return faFilter;
            case ActionType.aggregate: return faDatabase;
            case ActionType.calculate: return faCalculator;
            default: return faDatabase;
        }
    }
}