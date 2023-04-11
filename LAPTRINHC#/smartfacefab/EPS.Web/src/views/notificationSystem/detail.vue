<template>

    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div v-html="notificationSystem.noContent"></div>
                </b-card>
            </b-col>
        </b-row>

    </div>
</template>

<script>
    export default {
        name: 'NotificationSystemDetail',
        data() {
            return {
                notificationSystem: {
                    title: null,
                    noContent: null,
                    readed: null
                },
            }
        },
        computed: {
            notificationSystemId() {
                return this.$route.params.notificationSystemId;
            },
        },
        created() {
            this.loadNotifiDetail();
            this.updateStatus();
        },
        methods: {
            loadNotifiDetail() {
                return this.$services.get(`notificationSystem/${this.notificationSystemId}`).done(res => {
                    this.notificationSystem = res;
                });
            },
            updateStatus() {
                this.$services.put(`notificationSystem/${this.notificationSystemId}`).done(res => {
                    if (res)
                        this.$store.commit('setTotalNotifications', this.$store.state.common.totalNotifications - 1);
                });
            }
        }
    }

</script>
<style scoped>
</style>
