import {Action, isRejectedWithValue, Middleware} from '@reduxjs/toolkit';
import {isHttpError} from "./HttpError";

export interface ErrorHandler<TCode, TError = unknown > {
    code: TCode;
    handle: (error: TError) => void;
}

export const errorsMiddleware: <TCode, TState>(handlers: ErrorHandler<TCode>[]) => Middleware<{}, TState> = handlers => storeApi => next => (action: Action) => {
    if (isRejectedWithValue(action) && isHttpError(action.payload)) {
        action.payload.errors
            .forEach(error => handlers
                .filter(handler => handler.code === error.code)
                .forEach(handler => handler.handle(error)
                )
            );
    }
    next(action);
}