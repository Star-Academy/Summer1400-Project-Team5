export enum RequestMethod {
  GET,
  POST
}

export default class RequestData {
  static server = "";

  endpoint: string;
  method: RequestMethod;
  body: any;

  constructor(endpoint: string, method: RequestMethod, body: any) {
    this.endpoint = endpoint;
    this.method = method;
    this.body = body;
  }
}
