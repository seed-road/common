import Reference from "yup/es/Reference";

declare module 'yup' {
    interface StringSchema {
        equalTo(ref: Reference<any>, msg: string): StringSchema
    }
}