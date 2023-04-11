import { mapState } from 'vuex';
export const authorizationMixin = {
    computed: {
        ...mapState({
            currentPrivileges: (state, getters) => state.identity.privileges
        })
    },
    methods: {
        authorize(requiredPrivileges) {
            return intersect(this.currentPrivileges, requiredPrivileges).length > 0;
        }
    }
};
