import React, {createContext, ReactNode, useState} from "react";
import {SnackBarsHandler} from "./snackbar/SnackBarsHandler";

const MaterialContext = createContext<undefined>(undefined);

export interface MaterialContextProviderProb {
    children: ReactNode
}

export function MaterialContextProvider({children}: MaterialContextProviderProb) {
    return (
        <MaterialContext.Provider value={undefined}>
            {children}
            <SnackBarsHandler/>
        </MaterialContext.Provider>
    )
}
