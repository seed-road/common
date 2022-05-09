import {SnackbarProps} from "@mui/material/Snackbar/Snackbar";
import {CustomAlertProps, SnackBar, UniqSnackBar} from "./definitions";
import {OverridableStringUnion} from "@mui/types";
import {AlertColor, AlertPropsColorOverrides} from "@mui/material/Alert/Alert";
import {v4 as uuid} from "uuid";
export const errorAlert = (text: string, props?: Partial<CustomAlertProps>): CustomAlertProps => ({
    ...defaultAlert(text),
    color: "error",
    severity: "error",
    ...props
})
export const successAlert = (text: string, props?: Partial<CustomAlertProps>): CustomAlertProps => ({
    ...defaultAlert(text),
    color: "success",
    severity: "success",
    ...props
})
export const warningAlert = (text: string, props?: Partial<CustomAlertProps>): CustomAlertProps => ({
    ...defaultAlert(text),
    color: "warning",
    severity: "warning",
    ...props
})
export const infoAlert = (text: string, props?: Partial<CustomAlertProps>): CustomAlertProps => ({
    ...defaultAlert(text),
    color: "info",
    severity: "info",
    ...props
})

export const errorSnackbar = (text: string, config?: Partial<UniqSnackBar>): UniqSnackBar => snackBar(text, "error", config)
export const successSnackbar = (text: string, config?: Partial<UniqSnackBar>): UniqSnackBar => snackBar(text, "success", config)
export const warningSnackbar = (text: string, config?: Partial<UniqSnackBar>): UniqSnackBar => snackBar(text, "warning", config)
export const infoSnackbar = (text: string, config?: Partial<UniqSnackBar>): UniqSnackBar => snackBar(text, "info", config)

function snackBar(text: string, color: OverridableStringUnion<AlertColor, AlertPropsColorOverrides>, config?: Partial<SnackBar>): UniqSnackBar {
    return {
        ...config,
        id: uuid(),
        alert: alert(text, color, config?.alert),
        snackbar: defaultSnackBarProps(config?.snackbar),
    }
}

function alert(text: string, color: OverridableStringUnion<AlertColor, AlertPropsColorOverrides>, config?: Partial<CustomAlertProps>): CustomAlertProps {
    return {
        text,
        color,
        severity: color,
        ...config
    }
}

export function defaultAlert(text: string, config?: Partial<CustomAlertProps>): CustomAlertProps {
    return {
        text,
        ...config
    }
}

export function defaultSnackBarProps(config?: Partial<SnackbarProps>): SnackbarProps {
    return {
        autoHideDuration: 2000,
        open: true,
        ...config
    }
}

