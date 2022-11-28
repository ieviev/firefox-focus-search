import { defineConfig } from 'vite'

export default defineConfig({
    root: ".",
    // publicDir: "assets",
    
    
    build:{
        outDir: "dist",
        // emptyOutDir: true,
        rollupOptions: {
            input: "./main.fs.js",
            output: {
                // entryFileNames: `[name].js`,
                entryFileNames: `main.js`,
                chunkFileNames: `[name].js`,
                assetFileNames: `[name].[ext]`
            }
        }
    }   
})
