import {InnerError} from "./InnerError";

export interface HttpErrorResponse {
    url: string;
    errors: InnerError[]
}
export interface HttpError {
    url: string;
    code: number;
    errors: InnerError[]
}
export function isHttpErrorResponse(value: any): value is HttpErrorResponse{
    return (value as HttpErrorResponse).url !== undefined;
}
export function isHttpError(value: any) : value is HttpError{
    return (value as HttpError).url !== undefined && (value as HttpError).code !== undefined ;
}
