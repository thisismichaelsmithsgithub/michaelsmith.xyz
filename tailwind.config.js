module.exports = {
  purge: [
    './src/**/*.html',
  ],
  darkMode: false, // or 'media' or 'class'
  theme: {
    extend: {},
    fontFamily: {
      'sans': ['ui-sans-serif', 'system-ui'],
      'display': ['Lato', 'ui-sans-serif', 'system-ui']
    }
  },
  variants: {
    extend: {},
  },
  plugins: [],
}
