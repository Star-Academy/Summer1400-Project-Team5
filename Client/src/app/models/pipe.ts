import Data from "./data";

export default class Pipe {
  id: number;
  name: string;
  sourceData: Data | null = null;
  destinationData: Data | null = null;


  constructor(id: number, name: string) {
    this.id = id;
    this.name = name;
  }
}
