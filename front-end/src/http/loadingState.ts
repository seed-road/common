import {HttpError} from "../errors/HttpError";
import {getErrorsByCodes} from "../errors/codes";

export declare type loadingStatus = 'idle' | 'loading' | 'failed' | 'completed';
export interface LoadingState<ErrorType>{
    status: loadingStatus;
    errors: ErrorType[];
}
export function idleState(): LoadingState<any> {
    return {
        status: "idle",
        errors: []
    };
}

export function loadingState(): LoadingState<any> {
    return {
        status: "loading",
        errors: []
    };
}

export function failedState<ErrorType>(...errors: ErrorType[]): LoadingState<Exclude<ErrorType, null | undefined>> {
    return {
        status: "failed",
        errors: errors?.filter(e => !!e).map(e => e as Exclude<ErrorType, null | undefined>) ?? []
    };
}

export function httpFailedState<TCode = string>(httpError?: HttpError, ...errorCodes: TCode[]): LoadingState<Exclude<TCode, null | undefined>> {
    const codes = getErrorsByCodes(httpError, ...errorCodes).map(e => e.code);
    return {
        status: "failed",
        errors: codes
    };
}

export function completedState(): LoadingState<any> {
    return {
        status: "completed",
        errors: []
    };
}