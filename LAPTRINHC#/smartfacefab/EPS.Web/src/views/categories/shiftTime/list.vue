<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>Danh sách ca làm việc</strong>
                    </div>
                    <b-form @submit.prevent="search">
                        <b-row>
                            <b-col md="4">
                                <b-form-group label="Đơn vị:"
                                              label-for="unit"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :options="lstcompany"
                                             :disabled="true"
                                             class="col-10"
                                             placeholder="Chọn giá trị"
                                             :settings="{allowClear: true}"
                                             v-model="searchForm.compId">
                                    </select2>
                                </b-form-group>
                            </b-col>
                        </b-row>

                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group label="" :label-cols="4">
                                    <b-button type="submit" variant="primary">Tìm kiếm</b-button>
                                </b-form-group>
                            </b-col>
                        </b-row>
                    </b-form>
                    <b-grid :searchForm="searchForm"
                            :gridOptions="gridOptions"
                            dataUrl="categories/shifttime"
                            gridName="Danh sách khai báo ca làm việc"
                            ref="shifttimeTable">
                        <template v-slot:actions>
                            <router-link to="/categories/shifttime/create"
                                         tag="button"
                                         class="btn btn-primary"
                                         size="md"
                                         v-if="authorize(['ManageShiftTime']) && (isAdmin || ($refs.shifttimeTable && $refs.shifttimeTable.options.totalRows < 1))">
                                Thêm mới
                            </router-link>
                        </template>

                        <template v-slot:cell(_actions)="row">
                            <b-button-group>
                                <b-button size="sm"
                                          :to="{ path: `/categories/shifttime/detail/${row.item.id}` }"
                                          :title="$t('Button.View')">
                                    <i class="fa fa-eye" aria-hidden="true"></i>
                                </b-button>
                                <b-button size="sm"
                                          @click.stop="remove(row.item, row.index, $event.target)"
                                          class="mr-1"
                                          :title="$t('Button.Delete')"
                                          v-if="authorize(['ManageShiftTime'])">
                                    <i class="fa fa-trash-o" aria-hidden="true"></i>
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
    import { authorizationMixin } from '@/shared/mixins'
    import Services from '@/utils/services'
    export default {
        name: 'ListGroupAccess',
        mixins: [authorizationMixin],
        data() {
            return {
                searchForm: {
                    filterText: null,
                    compId: null,
                },
                gridOptions: {
                    fields: [
                        {
                            key: 'compName',
                            label: 'Đơn vị',
                            sortable: false,
                        },
                        {
                            key: 'shiftName',
                            label: 'Tên Ca Làm Việc',
                            sortable: false,
                        },
                        {
                            key: 'beginShiftTime',
                            label: 'Thời gian bắt đầu làm việc',
                            sortable: false,
                        },
                        {
                            key: 'endShiftTime',
                            label: 'Thời gian kết thúc làm việc',
                            sortable: false,
                        },
                        {
                            key: 'beginFreeShiftTime',
                            label: 'Thời gian bắt đầu giải lao',
                            sortable: false,
                        },
                        {
                            key: 'endFreeShiftTime',
                            label: 'Thời gian kết thúc giải lao',
                            sortable: false,
                        },
                    ],
                    sortBy: 'id',
                    sortDesc: true,
                },
                lstcompany: [],
                lstmachine: [],
                lstdepartment: [],
                lstgroup: [],
                lstaccesstimeseg: [],
                isAdmin: false,
                isEmpty: false,
            }
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.searchForm.compId = accessToken.comId;
            await this.loadCompany();
        },
        methods: {
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(res => {
                    vm.lstcompany = res;
                });
            },
            search() {
                this.$refs.shifttimeTable.refresh();
            },
            remove(item, index, button) {
                var vm = this;
                if (confirm('Bạn có chắc chắn muốn xóa ca làm việc này?')) {
                    this.$services.delete(`categories/shifttime/${item.id}`).done(() => {
                        vm.$toastr.s('Xóa ca làm việc thành công');
                        vm.search();
                    });
                }
            }
        }
    }
</script>
<style scoped>
</style>
