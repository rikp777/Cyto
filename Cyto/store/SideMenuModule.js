const state = {
  menuItems: [
    {
      title: 'Dashboard',
      page: 'Dashboard',
    },
    {
      title: 'Experiment',
      page: 'Experiment-ExperimentDashboard',
      subMenu: [
        {
          title: 'Create',
          page: 'Experiment-ExperimentCreate',
        },
        {
          title: 'Overview',
          page: 'Experiment-ExperimentOverview',
        },
      ],
    },
    {
      title: 'Projects',
      page: 'project-index',
      subMenu: [
        {
          title: 'Create',
          page: 'project-create',
        },
        {
          title: 'Overview',
          page: 'project-overview',
        },
      ],
    },
  ],
}

const getters = {
  menuItems: (state) => state.menuItems,
}

const actions = {}

const mutations = {}

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations,
}
