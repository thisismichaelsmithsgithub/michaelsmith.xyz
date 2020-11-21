const glob = require('glob');

const CssMinimizerPlugin = require('css-minimizer-webpack-plugin');
const PurgeCssPlugin = require('purgecss-webpack-plugin')

const { srcDir } = require('./paths');


module.exports = {
    optimization: {
        minimize: true,
        minimizer: [
            new CssMinimizerPlugin()
        ]
    },

    plugins: [
        new PurgeCssPlugin({
            paths: glob.sync(`${srcDir}/**/*`, { nodir: true })
        })
    ]
};