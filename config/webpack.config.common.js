const path = require('path');

const HTMLWebpackPlugin = require('html-webpack-plugin');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");

const { srcDir, outputDir } = require('./paths');


module.exports = {
    entry: [path.resolve(srcDir, 'index.js')],

    output: {
        path: outputDir,
        publicPath: '/',
        filename: 'app.bundle.js'
    },

    module: {
        rules: [
            {
                test: /\.html$/,
                loader: 'html-loader'
            },
            {
                test: /\.(pdf|gif|png|jpe?g|svg|eot|woff|woff2|ttf)$/,
                use: [
                    {
                        loader: 'file-loader',
                        options: {
                            outputPath: 'static/',
                            name: '[name].[ext]?[hash]'
                        },
                    },
                ],
            },
            {
                test: /\.css$/,
                use: [
                    MiniCssExtractPlugin.loader,
                    'css-loader',
                    'postcss-loader'
                ],
            },
        ]
    },

    plugins: [
        new MiniCssExtractPlugin(),

        new HTMLWebpackPlugin({
            filename: 'index.html',
            template: path.resolve(srcDir, 'index.html')
        })
    ]
};