import Services from '@/utils/services'

const state = {
    isInitialized: false,
    privileges: [],
    companyTree: [],
    sidebarShow: 'responsive',
    sidebarMinimize: false
}

const getters = {
    setting: state => property => {
        return state.appSettings[property];
    },
    companyById: state => id => {
        return findCompanyById(state.companyTree, id);
    }
}

const actions = {
    loadPrivileges(context) {
        return Services.get('lookup/privileges').done((data) => {
            context.commit('setPrivileges', data);
        });
    },
    loadCompanyTree(context) {
        return Services.get('lookup/company-tree').done((data) => {
            context.commit('setCompanyTree', data);
        });
    },
    initializeSettings(context) {
        if (!context.state.isInitialized) {
            context.dispatch('loadCompanyTree');
            context.dispatch('loadPrivileges');
            context.commit('initialize');
        }
    }
}

// mutations
const mutations = {
    initialize(state) {
        state.isInitialized = true;
    },
    setPrivileges(state, privileges) {
        state.privileges = privileges;
    },
    setCompanyTree(state, data) {
        standardlizeCompanyTree(data);
        state.companyTree = data;
    },
    toggleSidebarDesktop(state) {
        const sidebarOpened = [true, 'responsive'].includes(state.sidebarShow)
        state.sidebarShow = sidebarOpened ? false : 'responsive'
    },
    toggleSidebarMobile(state) {
        const sidebarClosed = [false, 'responsive'].includes(state.sidebarShow)
        state.sidebarShow = sidebarClosed ? true : 'responsive'
    },
    set(state, [variable, value]) {
        state[variable] = value
    },
}

export default {
    state,
    getters,
    actions,
    mutations
}

function standardlizeCompanyTree(companys) {
    for (var i = 0; i < companys.length; i++) {
        if (companys[i]._children === null || companys[i]._children.length == 0) {
            companys[i]._children = undefined;
        } else {
            standardlizeCompanyTree(companys[i]._children);
        }
    }
}

function findCompanyById(companys, id) {
    for (var i = 0; i < companys.length; i++) {
        if (companys[i].id == id) {
            return companys[i];
        } else if (companys[i]._children && companys[i]._children.length > 0) {
            var childMatched = findCompanyById(companys[i]._children, id);
            if (childMatched) {
                return childMatched;
            }
        }
    }

    return null;
}