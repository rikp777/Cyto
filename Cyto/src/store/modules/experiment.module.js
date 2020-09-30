import { experimentService } from "@/services/experiment.service";

// Action Names
const FETCH_START = "setExperimentLoading";
const FETCH_END = "resetExperimentLoading";

// mutation Names
const SET_EXPERIMENT = "setExperiment";
const SET_EXPERIMENTS = "setExperiments";
const SET_PAGINATION = "setExperimentPagination";

// Initial State
const state = {
    experiment: [],
    experiments: {},
    pagination: {},
    perPage: 6,
    isLoading: true,
    totalAmount: 0
};

// Getters
const getters = {
    getAll(state) {
        return state.experiments;
    },
    getById: state => id => {
        return state.experiments.find(experiment => (experiment.id = id));
    },
    totalAmount(state) {
        return state.totalAmount;
    },
    isLoading(state) {
        return state.isLoading;
    }
};

const mutations = {
    [FETCH_START](state) {
        state.isLoading = true;
    },
    [FETCH_END](state) {
        state.isLoading = false;
    },
    [SET_EXPERIMENTS](state, experiments) {
        state.experiments = experiments;
        state.totalAmount = experiments.length;
    },
    [SET_PAGINATION](state, data) {
        state.pagination = data;
    },
    [SET_EXPERIMENT](state, article) {
        state.experiment = article;
    }
};

const actions = {
    // eslint-disable-next-line no-unused-vars
    fetchExperiments({ commit, dispatch, state }, { page }) {
        return experimentService.getAll(state.perPage, page).then(response => {
            commit("SET_EXPERIMENTS", response.data);
        });
    },
    fetchExperiment({ commit, getters, state }, id) {
        //if experiment already fetched show that
        if (id === state.experiment.id) return state.experiment;

        //if experiment is in list show that
        const experiment = getters.getById(id);

        if (experiment) {
            commit("SET_EXPERIMENT", experiment);
            return experiment;
        } else {
            // if experiment not fetched yet fetch
            return experimentService.getById(id).then(response => {
                commit("SET_EXPERIMENT", response.data);
                return response.data;
            });
        }
    }
};

export default {
    state,
    getters,
    actions,
    mutations
};
