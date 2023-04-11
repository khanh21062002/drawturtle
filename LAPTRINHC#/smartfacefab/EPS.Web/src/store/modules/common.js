import Services from '@/utils/services'

const state = {
    isInitialized: false,
    privileges: [],
    unitTree: [],
    sidebarShow: 'responsive',
    sidebarMinimize: false,
    totalNotifications: 0
}

const getters = {
    setting: state => property => {
        return state.appSettings[property];
    },
    unitById: state => id => {
        return findUnitById(state.unitTree, id);
    }
}

const actions = {   
    loadPrivileges(context) {
        return Services.get('lookup/privileges').done((data) => {
            context.commit('setPrivileges', data);
        });
    },
    loadUnitTree(context) {
        return Services.get('lookup/unit-tree').done((data) => {
            context.commit('setUnitTree', data);
        });
    },
    loadDepartmentTree(context) {
        return Services.get('lookup/department-tree').done((data) => {
            context.commit('setDepartmentTree', data);
        });
    },
    initializeSettings(context) {
        if (!context.state.isInitialized) {
            context.dispatch('loadUnitTree');
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
    setUnitTree(state, data) {
        
        standardlizeUnitTree(data);
        state.unitTree = data;
    },
    setTotalNotifications(state, data) {
        state.totalNotifications = data;
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

function standardlizeUnitTree(units) {
    for (var i = 0; i < units.length; i++) {
        if (units[i].children === null || units[i].children.length == 0) {
            units[i].children = undefined;
        } else {
            standardlizeUnitTree(units[i].children);
        }
    }
}

function findUnitById(units, id) {
    for (var i = 0; i < units.length; i++) {
        if (units[i].id == id) {
            return units[i];
        } else if (units[i].children && units[i].children.length > 0) {
            var childMatched = findUnitById(units[i].children, id);
            if (childMatched) {
                return childMatched;
            }
        }
    }

    return null;
}