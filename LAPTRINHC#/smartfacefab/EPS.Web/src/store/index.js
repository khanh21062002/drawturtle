import Vue from 'vue'
import Vuex from 'vuex'
import common from './modules/common'
import identity from './modules/identity'
//import createLogger from 'vuex/dist/logger'

Vue.use(Vuex)

const debug = process.env.NODE_ENV !== 'production'

export default new Vuex.Store({
    modules: {
        common,
        identity
    },
    strict: debug
    //plugins: debug ? [createLogger()] : []
})
