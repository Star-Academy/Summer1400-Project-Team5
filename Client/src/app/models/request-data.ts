export enum RequestMethod {
  GET,
  POST
}

export default class RequestData {
  static server = "";

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
