import React, {ReactElement} from "react";
import {Alert, Snackbar, SnackbarCloseReason} from "@mui/material";
import {useCommonSelector} from "../../hooks";
import {closeSnackBarById, selectFirstSnackBar, selectSnackBars} from "./slice";
import {useDispatch} from "react-redux";


export function SnackBarsHandler(): ReactElement {
    const snackBars = useCommonSelector(selectSnackBars);
    const snackBar = useCommonSelector(selectFirstSnackBar)
    const onCloseSnackBar = !!snackBar ? {
        ...snackBar,
        snackbar: {
            onClose: (event: React.SyntheticEvent | Event, reason: SnackbarCloseReason) => {
                if (reason === 'clickaway') {
                    return;
                }
                if (!!snackBar?.snackbar?.onClose) {
                    snackBar.snackbar.onClose(event, reason)
                }
                dispatch(closeSnackBarById(snackBar.id));
            },
            ...snackBar.snackbar
        }
    } : snackBar
    const dispatch = useDispatch();
    const isOpen = (snackbarId: string) => snackBars.snackBars.some(s => s.id === snackbarId)
    return (
        <>
            {
                onCloseSnackBar &&
                <Snackbar {...onCloseSnackBar.snackbar} key={onCloseSnackBar.id} open={isOpen(onCloseSnackBar.id)}>
                    <Alert {...onCloseSnackBar.alert}>{onCloseSnackBar.alert.text}</Alert>
                </Snackbar>
            }
        </>

    )
}
