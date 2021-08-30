export enum DataType {
  csv,
  sqlServer
}

interface DataConfig {
}

class CSVDataConfig implements DataConfig {
  fileAddress = "";
}

class SQLServerDataConfig implements DataConfig {
  server = "";
  username = "";
  password = "";
}

export default class Data {
  id: number;
  name: string;
  type: DataType;
  config: DataConfig;


  constructor(id: number, name: string, type: DataType) {
    this.id = id;
    this.name = name;
    this.type = type;
    switch (type) {
      case DataType.csv:
        this.config = new CSVDataConfig();
        break;
      case DataType.sqlServer:
        this.config = new SQLServerDataConfig();
        break;
    }
  }

  getTypeName(): string {
    switch (this.type) {
      case DataType.csv:
        return "CSV";
      case DataType.sqlServer:
        return "SQL Server";
    }
  }
}
