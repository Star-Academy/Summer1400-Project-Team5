import ActionItem, { ActionConfig, ActionType, AggregateActionConfig, FilterActionConfig, JoinActionConfig } from "./action";
import Data from "./data";

export default class Pipe {
  id: number;
  name: string;
  sourceData: Data | null = null;
  destinationData: Data | null = null;
  actions: ActionItem[] = [];/*[ // TODO: Change default actions to...
    new ActionItem(ActionType.join, new JoinActionConfig()),
    new ActionItem(ActionType.aggregate, new AggregateActionConfig()),
    new ActionItem(ActionType.join, new JoinActionConfig()),
    new ActionItem(ActionType.filter, new FilterActionConfig()),
  ];*/


  constructor(id: number, name: string) {
    this.id = id;
    this.name = name;
  }
}
