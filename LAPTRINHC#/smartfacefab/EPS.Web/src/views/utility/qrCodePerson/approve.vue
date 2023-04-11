<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("QRCodePerson.Approve.Header")}}</strong>
                    </div>
                    <b-form class="form-search" @submit.prevent="search">
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('QRCodePerson.Approve.Form.CodeName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text" v-model="searchForm.filterPerson">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('QRCodePerson.Approve.Form.Department')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <treeselect :multiple="true"
                                                :limit="3"
                                                :flat="true"
                                                :autoSelectDescendants="true"
                                                :autoDeselectDescendants="true"
                                                :options="treeselect.options"
												:noChildrenText="this.$t('TreeSelect.noChildrenText')"
                                                :placeholder="this.$t('PlaceHolder.Select')"
                                                v-model="deptId" />
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('QRCodePerson.Approve.Form.DateFrom')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <date-picker style="width: 100%;" v-model="searchForm.filterDateFrom" valueType="YYYY-MM-DD" format="DD-MM-YYYY">
                                    </date-picker>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('QRCodePerson.Approve.Form.DateTo')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <date-picker style="width: 100%;" v-model="searchForm.filterDateTo" valueType="YYYY-MM-DD" format="DD-MM-YYYY">
                                    </date-picker>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('QRCodePerson.Approve.Form.Status')"
                                              label-for="code"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :options="lstStatus" :placeholder="this.$t('PlaceHolder.Select')" :settings="{allowClear: true}"
                                             v-model="searchForm.filterStatus">
                                    </select2>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="7" class="text-right">
                                <b-form-group style="margin-bottom: 0px !important;" label="" :label-cols="4" label-align-md="right">
                                    <b-button type="submit" variant="primary">{{$t("Button.Search")}}</b-button>
                                </b-form-group>
                            </b-col>
                        </b-row>
                    </b-form>
                    <b-grid class="grid-border-top" :searchForm="searchForm"
                            :gridOptions="gridOptions"
                            :multiLanguage="true"
                            dataUrl="qrCodePerson"
                            :selectable="false"
                            :actionable="false"
                            gridName="QRCodePerson.Approve.GridName"
                            ref="qrCodePersonTable">
                        <template v-slot:actions>
                            <b-button type="button" variant="primary" @click="approve" v-if="authorize(['ManageApproveCodeQR'])">{{$t("QRCodePerson.Approve.Button.Approve")}}</b-button>
                            <b-button type="button" variant="danger" @click="reject" v-if="authorize(['ManageApproveCodeQR'])">{{$t("QRCodePerson.Approve.Button.Reject")}}</b-button>
                        </template>
                        <template v-slot:cell(checkBox)="row">
                            <b-form-checkbox v-if="row.item.status == 1" v-model="row.item.checkBox"></b-form-checkbox>
                        </template>
                        <template v-slot:cell(avatar)="row">
                            <img @click="showApproveModal(row.item.id)" v-bind:src="row.item.imageUpdate" alt="No Image" width="60" height="60" style="width: 60px; height: 80px; object-fit: cover; border-radius: 0.3em;">
                        </template>
                        <template v-slot:cell(statusStr)="row">
                            <span class="col-form-label "> {{ $t(row.item.statusStr) }}</span>
                        </template>
                        <template v-slot:cell(personCode)="row">
                            <b-link :to="{path:`/categories/person/detail/${row.item.personId}?isApprove=true`}"> <span>{{ $t(row.item.personCode) }}</span></b-link>
                        </template>
                        <template v-slot:cell(personName)="row">
                            <b-link :to="{path:`/categories/person/detail/${row.item.personId}?isApprove=true`}"> <span>{{ $t(row.item.personName) }}</span></b-link>
                        </template>
                        <template v-slot:cell(gender)="row">
                            <span class="col-form-label "> {{ $t(row.item.gender) }}</span>
                        </template>
                    </b-grid>
                    <confirmModal ref="confirmModal">
                        <label>{{$t('Messages.ConfirmDelete')}}</label>
                    </confirmModal>
                    <CModal :show.sync="modalApprove" v-if="authorize(['ManageApproveCodeQR'])"
                            :no-close-on-backdrop="true"
                            :title="this.$t('QRCodePerson.Approve.CModal.Title')"
                            size="lg"
                            color="dark" >
                        <b-form class="form-horizontal">
                            <div class="d-flex justify-content-center align-items-center">
                                <div>{{$t("QRCodePerson.Approve.CModal.Image")}}</div>
                                <div class="d-flex justify-content-center"
                                     style="margin-left: -5px; position: relative; width: 300px; height: 300px;  ">
                                    <img class="img-fluid" style="max-height: 100%; max-width: 100%" v-bind:src="avatar" />
                                    <img v-bind:src="qrCodePerson.imageUpdate" v-if="qrCodePerson.imageUpdate != null && avatar == null" alt="No Image" style="max-height: 100%; max-width: 100%" />
                                </div>
                            </div>
                            <b-row class="d-flex justify-content-center" style="margin-top:20px;">
                                <b-col md="6">
                                    <b-form-group :label="this.$t('QRCodePerson.Approve.CModal.Note')"
                                                  :label-cols="4"
                                                  :horizontal="true"
                                                  label-align-md="left">
                                        <b-form-textarea type="text" disabled
                                                         v-model="qrCodePerson.note">
                                        </b-form-textarea>
                                    </b-form-group>
                                </b-col>
                            </b-row>
                        </b-form>
                        <template #footer>
                            <b-button type="button" v-if="buttonDetail && authorize(['ManageApproveCodeQR'])" variant="primary" @click="approveDetail" >{{$t("QRCodePerson.Approve.Button.Approve")}}</b-button>
                            <b-button type="button" v-if="buttonDetail && authorize(['ManageApproveCodeQR'])" variant="danger" @click="rejectDetail"  >{{$t("QRCodePerson.Approve.Button.Reject")}}</b-button>
                            <b-button type="button" @click="cancelDetail">{{$t("Button.Cancel")}}</b-button>
                        </template>
                    </CModal>
                </b-card>
            </b-col>
        </b-row>
    </div>
</template>
<script>
    import { authorizationMixin } from '@/shared/mixins'
    import i18n from '@/libs/i18n'

    export default {
        name: 'ListPerson',
        mixins: [authorizationMixin],
        data() {
            return {
                person: {
                    fileData: null,
                },
                idDetail: null,
                buttonDetail: false,
                qrCodePerson: {
                    imageUpdate: null,
                    avatar: null,
                    note: null,
                },
                deptId: null,
                searchForm: {
                    filterPerson: null,
                    filterDeptId: null,
                    filterDateFrom: null,
                    filterDateTo: null,
                    filterStatus: 1,
                },
                lstDepartment: [],
                listId: [],
                lstStatus: [
                    { id: '1', text: this.$t('QRCodePerson.Approve.Form.ListStatus.1') },
                    { id: '2', text: this.$t('QRCodePerson.Approve.Form.ListStatus.2') },
                    { id: '0', text: this.$t('QRCodePerson.Approve.Form.ListStatus.0') },
                ],
                avatar: null,
                modalApprove: false,
                treeselect: {
                    value: null,
                    options: [],
                },
                gridOptions: {
                    fields: [
                        { key: 'dateUpdateStr', label: this.$t('QRCodePerson.Approve.GridField.DateUpdate'), sortable: false },
                        { key: 'personCode', label: this.$t('QRCodePerson.Approve.GridField.PersonCode'), sortable: false },
                        { key: 'personName', label: this.$t('QRCodePerson.Approve.GridField.PersonName'), sortable: false },
                        { key: 'deptName', label: this.$t('QRCodePerson.Approve.GridField.Department'), sortable: false },
                        { key: 'gender', label: this.$t('QRCodePerson.Approve.GridField.Gender'), sortable: false },
                        { key: 'statusStr', label: this.$t('QRCodePerson.Approve.GridField.Status'), sortable: false },
                        { key: 'note', label: this.$t('QRCodePerson.Approve.GridField.Note'), sortable: false },
                        { key: 'avatar', label: this.$t('QRCodePerson.Approve.GridField.Image'), sortable: false },
                        { key: 'checkBox', label: this.$t('QRCodePerson.Approve.GridField.CheckBox'), sortable: false },
                    ],
                    sortBy: 'dateUpdate',
                    sortDesc: true,
                },
            }
        },
        validations: {},
        created() {
            this.loadDepartmentTree();
        },
        methods: {
            search() {
                if (this.deptId != null) {
                    var deptStr = this.deptId.join(',');
                    this.searchForm.filterDeptId = deptStr;
                } else {
                    this.searchForm.filterDeptId = null;
                }
                this.$refs.qrCodePersonTable.refresh();
            },
            loadPersonApproveDetail(value) {
                var vm = this;
                return this.$services.get(`qrCodePerson/detailPerson/${value}`).done(res => {
                    vm.qrCodePerson = res;
                    vm.idDetail = res.id;
                    vm.search();
                    if (res.status == 1) {
                        vm.buttonDetail = true;
                    } else {
                        vm.buttonDetail = false;
                    }
                });
            },
            //Danh sách phòng ban - tree view
            async loadDepartmentTree() {
                var vm = this;
                return this.$services.get(`lookup/department-tree`).done(res => {
                    vm.treeselect.options = res;
                });
            },
            async approve() {
                var vm = this;
                const ok = await this.$refs.confirmModal.show({
                    title: this.$t('QRCodePerson.Approve.BarName'),
                    message: this.$t('QRCodePerson.Approve.Message.Approve.Confirm'),
                })
                // If you throw an error, the method will terminate here unless you surround it wil try/catch
                if (ok) {
                    for (var i = 0; i < this.$refs.qrCodePersonTable._data.sourceTable.length; i++) {
                        if (this.$refs.qrCodePersonTable._data.sourceTable[i].checkBox == true) {
                            this.listId.push(this.$refs.qrCodePersonTable._data.sourceTable[i].id);
                        }
                    }
                    if (this.listId.length > 0) {
                            var data = this.listId.toString();
                            this.$services.post(`qrCodePerson/approveReq?ids=${data}`).done(() => {
                                vm.$toastr.s(i18n.t("QRCodePerson.Approve.Message.Approve.Success"));
                                vm.search();
                                window.location.reload();
                            });
                    }
                    else {
                        vm.$toastr.e(i18n.t("QRCodePerson.Approve.Message.Approve.NoSelect"));
                    }
                } else {

                    this.$refs.popup.close();
                }
            },
            //approve() {
            //    var vm = this;
            //    for (var i = 0; i < this.$refs.qrCodePersonTable._data.sourceTable.length; i++) {
            //        if (this.$refs.qrCodePersonTable._data.sourceTable[i].checkBox == true) {
            //            this.listId.push(this.$refs.qrCodePersonTable._data.sourceTable[i].id);
            //        }
            //    }
            //    if (this.listId.length > 0) {
            //        if (confirm(i18n.t("QRCodePerson.Approve.Message.Approve.Confirm"))) {
            //            var data = this.listId.toString();
            //            this.$services.post(`qrCodePerson/approveReq?ids=${data}`).done(() => {
            //                vm.$toastr.s(i18n.t("QRCodePerson.Approve.Message.Approve.Success"));
            //                vm.search();
            //                window.location.reload();
            //            });
            //        }
            //    }
            //    else {
            //        vm.$toastr.e(i18n.t("QRCodePerson.Approve.Message.Approve.NoSelect"));
            //    }
            //},
            approveDetail() {
                var vm = this;
                if (confirm(i18n.t("QRCodePerson.Approve.Message.Approve.Confirm"))) {
                    var data = this.idDetail.toString();
                    this.$services.post(`qrCodePerson/approveReq?ids=${data}`).done(() => {
                        vm.$toastr.s(i18n.t("QRCodePerson.Approve.Message.Approve.Success"));
                        vm.search();
                        window.location.reload();
                        this.modalApprove = false;
                    });
                }
            },
            async reject() {
                var vm = this;
                const ok = await this.$refs.confirmModal.show({
                    title: this.$t('QRCodePerson.Approve.BarName1'),
                    message: this.$t('QRCodePerson.Approve.Message.Reject.Confirm'),
                })
                // If you throw an error, the method will terminate here unless you surround it wil try/catch
                if (ok) {
                    for (var i = 0; i < this.$refs.qrCodePersonTable._data.sourceTable.length; i++) {
                        if (this.$refs.qrCodePersonTable._data.sourceTable[i].checkBox == true) {
                            this.listId.push(this.$refs.qrCodePersonTable._data.sourceTable[i].id);
                        }
                    }
                    if (this.listId.length > 0) {
                        
                            var data = this.listId.toString();
                            this.$services.post(`qrCodePerson/rejectReq?ids=${data}`).done(() => {
                                vm.$toastr.s(i18n.t("QRCodePerson.Approve.Message.Reject.Success"));
                                vm.search();
                            });
                    }
                    else {
                        vm.$toastr.e(i18n.t("QRCodePerson.Approve.Message.Reject.NoSelect"));
                    }
                } else {

                    this.$refs.popup.close();
                }
            },
            //reject() {
            //    var vm = this;
            //    for (var i = 0; i < this.$refs.qrCodePersonTable._data.sourceTable.length; i++) {
            //        if (this.$refs.qrCodePersonTable._data.sourceTable[i].checkBox == true) {
            //            this.listId.push(this.$refs.qrCodePersonTable._data.sourceTable[i].id);
            //        }
            //    }
            //    if (this.listId.length > 0) {
            //        if (confirm(i18n.t("QRCodePerson.Approve.Message.Reject.Confirm"))) {
            //            var data = this.listId.toString();
            //            this.$services.post(`qrCodePerson/rejectReq?ids=${data}`).done(() => {
            //                vm.$toastr.s(i18n.t("QRCodePerson.Approve.Message.Reject.Success"));
            //                vm.search();
            //            });
            //        }
            //    }
            //    else {
            //        vm.$toastr.e(i18n.t("QRCodePerson.Approve.Message.Reject.NoSelect"));
            //    }
            //},
            rejectDetail() {
                var vm = this;
                if (confirm(i18n.t("QRCodePerson.Approve.Message.Reject.Confirm"))) {
                    var data = this.idDetail.toString();
                    this.$services.post(`qrCodePerson/rejectReq?ids=${data}`).done(() => {
                        vm.$toastr.s(i18n.t("QRCodePerson.Approve.Message.Reject.Success"));
                        vm.search();
                        this.modalApprove = false;
                    });
                }
            },
            showApproveModal(value) {
                this.modalApprove = true;
                this.loadPersonApproveDetail(value);
            },
            cancelDetail() {
                this.modalApprove = false;
            },
            onChange: function (evt) {
                this.turnOffCamera();
                var vm = this;
                var f = evt.target.files[0]; // FileList object
                var acceptExtensions = ["jpg", "png", "PNG", "JPG"];
                let fileExtensions = (/[.]/.exec(f.name)) ? /[^.]+$/.exec(f.name) : undefined;
                if (fileExtensions && !acceptExtensions.includes(fileExtensions[0])) {
                    alert(i18n.t("Categories.Person.Detail.Label.Fileinput"));
                    return;
                }
                if (f.size > 5000000) {
                    alert(i18n.t("Timesheet.TimeKeeping.List.ImportExcel.FileInput50Mb"));
                    f.value = "";
                    return;
                };
                var reader = new FileReader();
                // Closure to capture the file information.
                reader.onload = (function (theFile) {
                    return function (e) {
                        var binaryData = e.target.result;
                        //Converting Binary Data to base 64
                        vm.person.fileData = window.btoa(binaryData);
                    };
                })(f);
                reader.readAsBinaryString(f, vm);
                vm.qrCodePerson.imageUpdate = f.name;
                vm.avatar = URL.createObjectURL(f);
            },
        }
    }
</script>
<style lang="scss">
    .modal-footer {
        justify-content: right !important;
    }
</style>
