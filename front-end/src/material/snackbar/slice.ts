import {createSlice, Draft, PayloadAction} from "@reduxjs/toolkit";
import {CommonState} from "../../reducers";
import {SnackBar, SnackBarSlice, UniqSnackBar} from "./definitions";
import {v4 as uuid} from "uuid";

const initialState: SnackBarSlice = {
    snackBars: [],
};
const snackBarSlice = createSlice({
    name: 'snackBar',
    initialState,
    reducers: {
        openSnackBar: (state, action: PayloadAction<SnackBar>) => {
            const snackBar: UniqSnackBar = {id: uuid(), ...action.payload};
            state.snackBars.push(snackBar as Draft<UniqSnackBar>);
            if (state.snackBars.length > 0) {
                state.snackBarOpenedId = state.snackBars[0].id;
            }
        },
        openSnackBars: (state, action: PayloadAction<SnackBar[]>) => {
            const snackBars: UniqSnackBar[] = action.payload.map(sb => ({id: uuid(), ...sb}));
            state.snackBars.push(...snackBars as Draft<UniqSnackBar>[]);
        },
        closeSnackBarById: (state, action: PayloadAction<string>) => {
            const snackBarIdx = state.snackBars.findIndex(snackBar => snackBar.id === action.payload);
            if (snackBarIdx >= 0) {
                state.snackBars.splice(snackBarIdx, 1);
                state.snackBarClosedId = action.payload
            }
        }
    }
});

export const {openSnackBar, openSnackBars, closeSnackBarById} = snackBarSlice.actions;
export const snackBar = snackBarSlice.reducer;

export const selectSnackBars = (state: CommonState) => state.snackBar;
export const selectFirstSnackBar = (state: CommonState) => state.snackBar.snackBars?.length > 0 ? state.snackBar.snackBars[0] : undefined;
export const selectOpenedSnackBarId = (state: CommonState) => state.snackBar.snackBarOpenedId;
export const selectClosedSnackBarId = (state: CommonState) => state.snackBar.snackBarClosedId;
