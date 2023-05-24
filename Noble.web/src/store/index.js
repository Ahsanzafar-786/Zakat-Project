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
        
        GetCustomerList: function (commit, customerList) {
            store.state.customerList = customerList;
        },
  },
  modules: {
  }
})
