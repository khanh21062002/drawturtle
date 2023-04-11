<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Guess.List.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="search">
                        <b-row>
                            <b-col md="4">
                                <b-form-group :label="this.$t('Guess.List.Form.Company')"
                                              label-for="unit"
                                              :label-cols="3"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :options="lstcompany" :disabled="true" class="col-10" id="dpl_compId"
                                             :placeholder="this.$t('PlaceHolder.Select')"
                                             :settings="{allowClear: true}"
                                             v-model="searchForm.compId">
                                    </select2>
                                </b-form-group>
                            </b-col>
                            <b-col md="4">
                                <b-form-group :label="this.$t('Guess.List.Form.Name')"
                                              :label-cols="3"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text" id="txt_searchFullName"
                                                  :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="searchForm.fullName">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                            <b-col md="4">
                                <b-form-group :label="this.$t('Guess.List.Form.Identification')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text" id="txt_searchPassport"
                                                  :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="searchForm.passport">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group label="" :label-cols="4">
                                    <b-button type="submit" variant="primary">{{$t("Button.Search")}}</b-button>
                                </b-form-group>
                            </b-col>
                        </b-row>
                    </b-form>
                    <b-grid :searchForm="searchForm" class="grid-border-top"
                            :gridOptions="gridOptions"
                            dataUrl="guess"
                            :multiLanguage="true"
                            gridName="Guess.List.HeaderTable"
                            ref="guessTable">
                        <template v-slot:actions>
                            <router-link to="/guess/create" tag="button" class="btn btn-primary" size="md" v-if="authorize(['ManageGuess'])">
                                {{$t("Button.Create")}}
                            </router-link>
                            <b-button size="md" @click="viewQRCode" variant="primary">
                                {{$t("Guess.List.BtnQR")}}
                            </b-button>

                        </template>
                        <template v-slot:cell(imageUrl)="row">
                            <img v-bind:src="row.item.imageUrl" alt="No Image" width="60" height="60" style="width: 60px; height: 80px; object-fit: cover; border-radius: 0.3em;">
                        </template>

                        <template v-slot:cell(approve)="row">
                            <span data-v-5f7284b9="" class="badge badge-secondary" v-if="row.item.approve == 0">{{$t("Guess.Detail.Form.NotApproved")}}</span>
                            <span data-v-5f7284b9="" class="badge badge-success" v-if="row.item.approve == 1">{{$t("Guess.Detail.Form.Approved")}}</span>
                            <span data-v-5f7284b9="" class="badge badge-danger" v-if="row.item.approve == 2">{{$t("Guess.Detail.Form.Refuse")}}</span>
                        </template>

                        <template v-slot:cell(_actions)="row">
                            <b-button-group>
                                <b-button size="sm" :to="{ path: `/guess/detail/${row.item.id}` }" :title="$t('TitleDetail.View')">
                                    <i class="fa fa-eye" aria-hidden="true"></i>
                                </b-button>
                            </b-button-group>
                        </template>
                    </b-grid>
                    <CModal :show.sync="qrcodeModal"
                            :no-close-on-backdrop="true"
                            :title="$t('Guess.List.Modal.QRcode')"
                            size="lg"
                            color="dark">
                        <b-row>
                            <b-col md="12" id="test">
                                <template>
                                    <b-form-input type="text" v-model="msg">
                                    </b-form-input>
                                    <div style="text-align: center;" id="printMe">
                                        <h1 class="pt-1">{{ msg }}</h1>
                                        <vue-qrcode :value="qrcodeValue" :width="400" />
                                    </div>
                                </template>
                            </b-col>
                        </b-row>
                        <template #header>
                            <h6 class="modal-title">QR Code</h6>
                            <CButtonClose @click="qrcodeModal = false" class="text-white" />
                        </template>
                        <template #footer>
                            <b-button @click="copyc()"> Copy QR </b-button>
                            <b-button @click="copy(qrcodeValue)"> Copy link </b-button>
                            <b-button type="button" v-print="'#printMe'" variant="danger">
                                {{$t("Guess.List.Modal.PrintQR")}}
                            </b-button>
                            <b-button type="button" variant="secondary" @click="qrcodeModal = false"><i class="fa fa-ban"></i>{{$t("Button.Close")}}</b-button>
                        </template>
                    </CModal>
                </b-card>
            </b-col>
        </b-row>
    </div>
</template>
<script>
    import { authorizationMixin } from '@/shared/mixins'
    import Services from '@/utils/services'
    import VueQrcode from 'vue-qrcode'
    import i18n from '@/libs/i18n'

    export default {
        name: 'ListGroupAccess',
        mixins: [authorizationMixin],
        data() {
            return {
                searchForm: {
                    compId: null,
                    fullName: null,
                    passport: null,
                },
                gridOptions: {
                    fields: [
                        { key: 'approve', label: this.$t('Guess.List.Table.Status'), sortable: false, },
                        { key: 'fullName', label: this.$t('Guess.List.Table.Name'), sortable: false, },
                        { key: 'passport', label: this.$t('Guess.List.Table.Identification'), sortable: false, },
                        { key: 'phoneNumber', label: this.$t('Guess.List.Table.Phone'), sortable: false, },
                        { key: 'strStartTime', label: this.$t('Guess.List.Table.StartTime'), sortable: false, },
                        { key: 'strEndTime', label: this.$t('Guess.List.Table.EndTime'), sortable: false, },
                        { key: 'imageUrl', label: this.$t('Guess.List.Table.Image'), sortable: false },
                    ],
                    sortBy: 'id',
                    sortDesc: true,
                },
                lstcompany: [],
                qrcodeModal: false,
                qrcodeValue: 'https://www.google.com/',
                msg: this.$t('Guess.List.Modal.Message')
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
                return this.$services.get(`lookup/companys`).done(lstcompany => {
                    vm.lstcompany = lstcompany;
                });
            },
            search() {
                this.$refs.guessTable.refresh();
            },
            viewQRCode() {
                this.qrcodeValue = window.location.protocol + '//' + window.location.host + '/#/guessRegister/' + this.searchForm.compId;
                this.qrcodeModal = true;
            },
            async copy(s) {
                await navigator.clipboard.writeText(s);
                alert(i18n.t("Guess.List.Success"));
            },
            async copyc() {
                let img = document.getElementsByTagName("img")[0].src;
                const b64toBlob = (b64Data, contentType = '', sliceSize = 512) => {
                    const byteCharacters = atob(b64Data);
                    const byteArrays = [];
                    for (let offset = 0; offset < byteCharacters.length; offset += sliceSize) {
                        const slice = byteCharacters.slice(offset, offset + sliceSize);

                        const byteNumbers = new Array(slice.length);
                        for (let i = 0; i < slice.length; i++) {
                            byteNumbers[i] = slice.charCodeAt(i);
                        }
                        const byteArray = new Uint8Array(byteNumbers);
                        byteArrays.push(byteArray);
                    }
                    const blob = new Blob(byteArrays, { type: contentType });
                    return blob;
                }
                const contentType = 'img/png';
                const b64Data = img.src;
                const bloba = b64toBlob(b64Data, contentType);
                const response = await fetch(bloba);
                const blob = await response.blob();
                const item = new ClipboardItem({ [blob.type]: blob })
                await navigator.clipboard.write(item);
                alert(i18n.t("Guess.List.Success"));
            }
        },
        components: {
            VueQrcode,
        }
    }
</script>
<style scoped>
</style>
