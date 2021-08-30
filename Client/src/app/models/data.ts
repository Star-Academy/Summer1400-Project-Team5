enum DataType {
  csv,
  sqlServer
}

interface DataConfig {
}

class CSVDataConfig implements DataConfig {
}

class SQLServerDataConfig implements DataConfig {
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
}
