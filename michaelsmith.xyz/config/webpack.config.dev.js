const path = require('path');


module.exports = {
    devtool: 'inline-source-map',

    devServer: {
        watchContentBase: true,
        contentBase: path.join(__dirname, 'dist'),
        compress: true,
        port: 9000,
        open: true
    }
};
