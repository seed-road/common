
export interface InnerError<TReason = any, TTarget = any, TCode = any> {
    message?: string;
    code: TCode;
    reason?: TReason;
    target?: TTarget;
    stackTrace?: string;
}