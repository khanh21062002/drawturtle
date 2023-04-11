const path = require('path')
var jquery = require('jquery')
const webpack = require('webpack')

const isDevEnv = process.env.NODE_ENV == "development";

module.exports = {
    //outputDir: 'wwwroot',
    publicPath: isDevEnv ? '/' : '/',
    lintOnSave: false,
    runtimeCompiler: true,
    configureWebpack: {
        //Necessary to run npm link https://webpack.js.org/configuration/resolve/#resolve-symlinks
        resolve: {
            symlinks: false,
            alias: {
                '@': path.resolve(__dirname, 'src/')
            }
        },
        plugins: [
            new webpack.ProvidePlugin({
                $: "jquery",
                jQuery: "jquery"
            })
        ]
    },
    transpileDependencies: [
        '@coreui/utils'
    ]
}
