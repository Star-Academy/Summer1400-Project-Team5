export enum RequestMethod {
  GET,
  POST
}

export default class RequestData {
  static server = "http://localhost:5000";

  endpoint: string;
  method: RequestMethod;
  body: any;
  callback: any | null;

  constructor(endpoint: string, method: RequestMethod, body: any, callback: any | null) {
    this.endpoint = endpoint;
    this.method = method;
    this.body = body;
    this.callback = callback
  }
}
