import {createTheme, Theme} from "@mui/material";
import {amber, brown, green} from "@mui/material/colors";

export const getThemeBy = (prefersDarkMode: boolean): Theme => {
    return createTheme({
        palette: prefersDarkMode ?
            {
                mode: 'dark',
                primary: amber,
                secondary: green,
            } :
            {
                mode: 'light',
                primary: brown,
                secondary: green,
            }
    });
}