import {
    Action,
    AnyAction,
    Dispatch,
    isAsyncThunkAction,
    isRejectedWithValue,
    Middleware,
    MiddlewareAPI
} from '@reduxjs/toolkit';
import {HttpError, isHttpError} from "./HttpError";

export interface ErrorsHandler<TCode = unknown, TError = unknown, TState = unknown> {
    codes: TCode[];
    handle: (errors: TError[], storeApi: MiddlewareAPI<Dispatch<AnyAction>, TState>) => void;
}

export const errorsMiddleware: <TCode = unknown, TError = unknown, TState = unknown>(handlers: ErrorsHandler<TCode, TError, TState>[]) => Middleware<{}, TState> = handlers => storeApi => next => (action: Action) => {
    if (isRejectedWithValue(action) && isHttpError(action.payload)) {
        const httpError = action.payload as HttpError;
        handlers.forEach(handler => {
            const innerErrors = httpError.errors
                .filter(innerError => handler.codes.includes(innerError.code));
            if (innerErrors.length > 0) {
                handler.handle(innerErrors as any, storeApi);
            }
        });
    }
    next(action);
}