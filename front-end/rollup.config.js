import typescript from 'rollup-plugin-typescript2'
import babel from '@rollup/plugin-babel'
import { terser } from 'rollup-plugin-terser'
const INPUT_ENTRY = 'src/index.ts'
export default [
    // ES
    {
        input: INPUT_ENTRY,
        output: {
            file: 'dist/index.es.js', format: 'es',
        },
        plugins: [
            typescript(),
            babel({
                extensions: ['.ts'],
            }),
        ],
    },

    // UMD
    {
        input: INPUT_ENTRY,
        output: {
            file: 'dist/index.umd.min.js',
            format: 'umd',
            name: 'reactUseThrottle',
            indent: false,
        },
        plugins: [
            typescript(),
            babel({
                extensions: ['.ts'],
                exclude: 'node_modules/**',
            }),
            terser(),
        ],
    },
]