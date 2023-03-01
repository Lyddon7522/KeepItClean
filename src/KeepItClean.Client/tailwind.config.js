/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './Pages/**/*.razor'
  ],
  theme: {
    colors: {
      'blue': '#4f4cb0',
      'blue-light': '#6b93d6',
      'white': '#e9eff9',
      'green': '#9fc164',
      'brown': 'd8c596'
    },
    extend: {
      fontFamily: {
        'eczar': ['Eczar'],
        'earthis': ['Earthis']
      }
    },
  },
  plugins: [],
}
