import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export const store= new Vuex.Store({
    state: {
        companyList: [],
  },
    mutations: {
        setAuthentication(state, status) {
            state.authenticated = status;
        }
  },
    actions: {
        
        GetCompanyList: function (commit, companyList) {
            store.state.companyList = companyList;
        },
  },
  modules: {
  }
})
