<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <b-grid :searchForm="searchForm" class="grid-border-top"
                            :gridOptions="gridOptions"
                            dataUrl="notificationSystem"
                            gridName="Warning.List.Table.Header"
                            ref="notificationSystemTable">
                        <template v-slot:cell(title)="row">
                            <strong v-if="row.item.readed == 0">{{ row.item.title }}</strong>
                            <div v-if="row.item.readed == 1">{{ row.item.title }}</div>
                        </template>
                        <template v-slot:cell(_actions)="row">
                            <b-button-group>
                                <b-button size="sm" :to="{ path: `/notificationSystem/detail/${row.item.id}` }" :title="$t('Button.View')">
                                    <i class="fa fa-eye" aria-hidden="true"></i>
                                </b-button>
                            </b-button-group>
                        </template>
                    </b-grid>
                </b-card>
            </b-col>
        </b-row>
    </div>
</template>

<script>
    import Services from '@/utils/services'

    export default {
        name: 'ListNotificationSystem',
        data() {
            return {
                searchForm: {
                    userId: null,
                },
                gridOptions: {
                    fields: [
                        { key: 'title', label: this.$t('Warning.List.Table.Content'), sortable: false, },
                    ],
                    sortBy: 'createDate',
                    sortDesc: true,
                },
            };
        },        
        created() {
            var accessToken = Services.getAccessToken();
            this.searchForm.userId = accessToken.userId;
        },
        
        methods: {
           
        }
    }
</script>
