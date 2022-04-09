import {HttpError} from "./HttpError";
import {InnerError} from "./InnerError";

export function getErrorByCode<TCode, TReason = string, TTarget = string>(error?: HttpError, code?: TCode | string): InnerError<TReason, TTarget> | undefined {
    return error?.errors?.find(innerError => innerError.code === code);
}

export function getErrorsByCodes<TCode, TReason = string, TTarget = string>(error?: HttpError, ...codes: TCode []): InnerError<TReason, TTarget>[] {
    return error?.errors?.filter(innerError => codes.some(c => c === innerError.code)) ?? [];
}


