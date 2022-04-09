import _ from "lodash"

export const getRoute = (...path: string[]) => {
    return path.map(p => _.trim(p, '/')).join('/');
}
export const getAbsoluteUrlByWindow = (path: string) => getAbsoluteUrl(window.location.protocol, window.location.hostname, path, parseInt(window.location.port, 10))

export const getAbsoluteUrl = (protocol: string, hostname: string, path: string, port: number = 80) =>
    `${protocol}//${hostname}:${port}/${_.trimStart(path, '/')}`;
