import { defineConfig } from 'vite'

export default defineConfig({
    root: ".",
    publicDir: "assets",
    build:{
        outDir: "dist",
        rollupOptions: {
            input: "./main.fs.js",
            output: {
                entryFileNames: `main.js`,
                chunkFileNames: `[name].js`,
                assetFileNames: `[name].[ext]`
            }
        },
        // minify: false,
    },
})
