import {Action, AsyncThunk, AsyncThunkPayloadCreator, createAsyncThunk, ThunkAction} from "@reduxjs/toolkit";

export type AppThunk<TState, TExtra, ReturnType = void> = ThunkAction<ReturnType,
    TState,
    TExtra,
    Action<string>>;
export type ThunkAPI<TState, TExtra, Rejected = unknown> = { state: TState, extra: TExtra, rejectValue: Rejected }
export type AppAsyncThunkPayloadCreator<TState, TExtra, Returned, ThunkArg, Rejected> = AsyncThunkPayloadCreator<Returned, ThunkArg, ThunkAPI<TState, TExtra, Rejected>>

export function createAppAsyncThunk<TState, TExtra, Returned, ThunkArg, Rejected = unknown>(
    typePrefix: string,
    payloadCreator: AppAsyncThunkPayloadCreator<TState, TExtra, Returned, ThunkArg, Rejected>): AsyncThunk<Returned, ThunkArg, ThunkAPI<TState, TExtra, Rejected>> {
    return createAsyncThunk<Returned, ThunkArg, ThunkAPI<TState, TExtra, Rejected>>(typePrefix, payloadCreator);
}

export function createRejectAppAsyncThunk<TState, TExtra, Returned, ThunkArg, Rejected>(typePrefix: string, payloadCreator: AppAsyncThunkPayloadCreator<TState, TExtra, Returned, ThunkArg, Rejected>): AsyncThunk<Returned, ThunkArg, ThunkAPI<TState, TExtra, Rejected>> {
    const errorWrapper: AppAsyncThunkPayloadCreator<TState, TExtra, Returned, ThunkArg, Rejected> = async (arg, thunkAPI) => {
        try {
            return await payloadCreator(arg, thunkAPI);
        } catch (e) {
            return thunkAPI.rejectWithValue(e as Rejected);
        }
    };
    return createAppAsyncThunk<TState, TExtra, Returned, ThunkArg, Rejected>(typePrefix, errorWrapper);
}
