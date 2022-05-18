import {FormikContextType} from "formik/dist/types";
import {ReactElement} from "react";
import {TextField, TextFieldProps} from "@mui/material";
import _ from "lodash";

export interface FieldProps<TValue> {
    formKey: keyof TValue,
    label?: string,
    formik: FormikContextType<TValue>
}

export function Field<TValue>({formik, formKey, label, ...props}: FieldProps<TValue> & TextFieldProps): ReactElement {
    return (
        <TextField
            id={`${formKey}`}
            name={`${formKey}`}
            label={label ?? _.capitalize(`${formKey}`)}
            fullWidth
            value={formik.values[formKey]}
            onChange={formik.handleChange}
            error={formik.touched[formKey] && Boolean(formik.errors[formKey])}
            helperText={formik.touched[formKey] && formik.errors[formKey]}
            {...props}
        />
    );
}