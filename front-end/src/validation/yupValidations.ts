import * as yup from "yup"
import Reference from "yup/es/Reference";
import {MixedSchema} from "yup/lib/mixed";

export function equalTo(this: MixedSchema, ref: Reference<any>, msg: string): MixedSchema {
    return this.test({
        name: 'equalTo',
        exclusive: false,
        message: msg || '${path} must be the same as ${reference}',
        params: {
            reference: ref.path
        },
        test: function (value: any) {
            return value === this.resolve(ref)
        }
    })
}
yup.addMethod(yup.mixed, 'equalTo', equalTo);
