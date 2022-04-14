import {AlertProps} from "@mui/material/Alert/Alert";
import {SnackbarProps} from "@mui/material/Snackbar/Snackbar";

export  type CustomAlertProps = AlertProps & {
    text: string;
};

export interface SnackBar {
    snackbar: SnackbarProps
    alert: CustomAlertProps
}

export interface SnackBarSlice {
    snackBars: UniqSnackBar[];
    snackBarClosedId?: string;
    snackBarOpenedId?: string;
}

export type UniqSnackBar = SnackBar & { id: string }