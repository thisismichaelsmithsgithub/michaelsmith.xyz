const fs = require('fs');
const path = require('path')

const baseDir = fs.realpathSync(process.cwd());

module.exports = {
    baseDir,
    outputDir: path.resolve(baseDir, 'dist'),
    srcDir: path.resolve(baseDir, 'src')
};