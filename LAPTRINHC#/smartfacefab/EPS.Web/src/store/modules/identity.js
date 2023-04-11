import Services from '@/utils/services'

const state = {
    username: '',
    fullName: '',
    unitId: null,
    privileges: [],
    isAuthenticated: false,
};
window.svc = Services;
const actions = {
    loadIdentity(context) {
        var accessToken = Services.getAccessToken();
        if (accessToken) {
            context.commit("setIdentity", accessToken);
            context.dispatch('initializeSettings');
        }
    },
    authenticate(context, { username, password }) {
        return Services.login(username, password).done(function (accessToken) {
            context.commit("setIdentity", accessToken);
            context.dispatch('initializeSettings');
        });
    },
    checkIdentity(context, identity) {
        if (identity) {
            if (new Date(identity['expires']) <= new Date() && identity.refresh_token) {
                Services.refreshToken(identity.refresh_token).done(function (accessToken) {
                    context.commit("setIdentity", accessToken);
                });
            } else {
                context.commit("setIdentity", identity);
            }
        }
    },
    deauthenticate(context) {
        Services.logout();
        context.commit('removeIdentity');
        //return Services.logout().done(function () {
        //    context.commit('removeIdentity');
        //});
    }
};

// mutations
const mutations = {
    setIdentity(state, identity) {
        state.username = identity.username;
        state.fullName = identity.fullName;
        state.unitId = identity.unitId;
        state.privileges = identity.privileges;
        state.isAuthenticated = true;
    },
    removeIdentity(state) {
        state.username = '';
        state.fullName = '';
        state.unitId = null;
        state.privileges = [];
        state.isAuthenticated = false;
    }
};

export default {
    state,
    actions,
    mutations
};
