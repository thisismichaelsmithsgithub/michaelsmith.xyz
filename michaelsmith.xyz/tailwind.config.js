module.exports = {
  purge: {
    mode: 'all',
    content: [
      './src/**/*.html'
    ]
  },
  darkMode: false,
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
