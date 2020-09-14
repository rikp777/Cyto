import * as types from '~/store/MutationTypes'

const state = {
  darkMode: false,
}

const getters = {
  darkMode: (state) => state.darkMode,
}

const actions = {
  setDarkMode({ commit }, darkMode) {
    commit(types.SET_DARK_MODE, { darkMode })
  },
}

const mutations = {
  [types.SET_DARK_MODE](state, { darkMode }) {
    state.darkMode = darkMode
  },
}

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations,
}
