/*
 ** TailwindCSS Configuration File
 **
 ** Docs: https://tailwindcss.com/docs/configuration
 ** Default: https://github.com/tailwindcss/tailwindcss/blob/master/stubs/defaultConfig.stub.js
 */
const plugin = require('tailwindcss/plugin')

module.exports = {
  theme: {
    extend: {
      colors: {
        theme: {
          1: '#1C3FAA',
          2: '#F1F5F8',
          3: '#2e51bb',
          4: '#3151BC',
          5: '#dee7ef',
          6: '#D32929',
          7: '#365A74',
          8: '#D2DFEA',
          9: '#91C714',
          10: '#3160D8',
          11: '#F78B00',
          12: '#FBC500',
          13: '#7F9EB9',
          14: '#E6F3FF',
          15: '#8DA9BE',
          16: '#607F96',
          17: '#FFEFD9',
          18: '#D8F8BC',
          19: '#E6F3FF',
          20: '#2449AF',
          21: '#284EB2',
          22: '#395EC1',
          23: '#D6E1FF',
          24: '#2e51bb',
          25: '#C6D4FD',
          26: '#E8EEFF',
          27: '#98AFF5',
          28: '#1A389F',
          29: '#142C91',
          30: '#8da3e6',
          31: '#ffd8d8',
          32: '#3b5998',
          33: '#4ab3f4',
          34: '#517fa4',
          35: '#0077b5',
          36: '#d18d96',
          37: '#c7d2ff',
          38: '#15329A',
          40: '#203FAD',
          41: '#BBC8FD',
        },
        dark: {
          1: '#293145',
          2: '#232a3b',
          3: '#313a55',
          4: '#1e2533',
          5: '#3f4865',
          6: '#2b3348',
          7: '#181f29',
        },
      },
      spacing: {
        26: '6.5rem',
        80: '20rem',
      },
      maxWidth: {
        '1/4': '25%',
        '1/2': '50%',
        '3/4': '75%',
      },
      borderRadius: {
        xl: '1rem',
        '2xl': '2rem',
        '3xl': '4rem',
      },
      fontFamily: {
        roboto: ['Roboto'],
      },
      fontSize: {
        '2xs': '0.65rem',
        '3xs': '0.5rem',
      },
      zIndex: {
        1: '1',
      },
      strokeWidth: {
        0.5: 0.5,
        1.5: 1.5,
        2.5: 2.5,
      },
      screens: {
        sm: '640px',
        md: '768px',
        lg: '1024px',
        xl: '1280px',
        xxl: '1600px',
      },
    },
    container: {
      center: true,
    },
  },
  variants: {
    zIndex: ['responsive', 'hover'],
    position: ['responsive', 'hover'],
    padding: ['responsive', 'last'],
    margin: ['responsive', 'last'],
    borderWidth: ['responsive', 'last'],
    backgroundColor: [
      'last',
      'first',
      'odd',
      'responsive',
      'hover',
      'dark',
      'dark-hover',
    ],
    borderColor: [
      'last',
      'first',
      'odd',
      'responsive',
      'hover',
      'dark',
      'dark-hover',
    ],
    textColor: [
      'last',
      'first',
      'odd',
      'responsive',
      'hover',
      'dark',
      'dark-hover',
    ],
  },
  plugins: [
    require('@tailwindcss/custom-forms'),
    plugin(function ({ addVariant, e }) {
      addVariant('dark', ({ modifySelectors, separator }) => {
        modifySelectors(({ className }) => {
          return `.dark .${e(`dark${separator}${className}`)}`
        })
      })
      addVariant('dark-hover', ({ modifySelectors, separator }) => {
        modifySelectors(({ className }) => {
          return `.dark .${e(
            `dark${separator}hover${separator}${className}`
          )}:hover`
        })
      })
    }),
  ],
  purge: {
    // Learn more on https://tailwindcss.com/docs/controlling-file-size/#removing-unused-css
    enabled: process.env.NODE_ENV === 'production',
    content: [
      'components/**/*.vue',
      'layouts/**/*.vue',
      'pages/**/*.vue',
      'plugins/**/*.js',
      'nuxt.config.js',
    ],
  },
}
