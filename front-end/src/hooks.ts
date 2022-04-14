import {CommonState} from "./reducers";
import {TypedUseSelectorHook, useSelector} from 'react-redux';

export const useCommonSelector: TypedUseSelectorHook<CommonState> = useSelector;