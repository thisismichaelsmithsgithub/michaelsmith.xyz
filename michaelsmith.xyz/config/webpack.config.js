const { merge } = require('webpack-merge');

const commonConfig = require('./webpack.config.common');
const prodConfig = require('./webpack.config.prod');
const devConfig = require('./webpack.config.dev');

module.exports = (env, argv) => {
    const isProduction = argv.mode === 'production';

    return isProduction
        ? merge(commonConfig, prodConfig)
        : merge(commonConfig, devConfig);
};