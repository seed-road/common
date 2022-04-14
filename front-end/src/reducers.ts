import {combineReducers} from "@reduxjs/toolkit";
import {snackBar} from "./material/snackbar/slice";

export const commonReducer = combineReducers({snackBar})
export type CommonState = ReturnType<typeof commonReducer>;

