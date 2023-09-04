export class ResponseDto {
    isSuccess: boolean = true;
    result: any;
    displayMessage: string = "";
    errorMessages: string[] | null = null;
  }
  