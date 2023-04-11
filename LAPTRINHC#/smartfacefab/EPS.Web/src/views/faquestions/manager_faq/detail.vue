<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t('ManagerFAQ.Detail.Header')}}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="5" offset="0">
                                <b-form-group :label="this.$t('ManagerFAQ.Detail.Label.Question')"
                                              :label-cols="3"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing? 'required' : ''">
                                    <b-form-input type="text" id="name" v-if="editing"
                                                  v-model="$v.faquestions.name.$model"
                                                  :state="$v.faquestions.name.$dirty? !$v.faquestions.name.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.faquestions.name.required">
                                        {{$t('ManagerFAQ.Detail.Label.QuestionRequire')}}
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing">{{ faquestions.name }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="5">
                                <b-form-group :label="this.$t('ManagerFAQ.Detail.Label.Type')"
                                              :label-cols="3"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing? 'required' : ''">
                                    <select2 :options="lstLanguage" :placeholder="this.$t('PlaceHolder.Select')" v-if="editing" id="dpl_compId" :settings="{allowClear: true}"
                                             v-model="$v.faquestions.type.$model">
                                    </select2>
                                    <b-form-invalid-feedback v-if="!$v.faquestions.type.required">
                                        {{$t('ManagerFAQ.Detail.Label.TypeRequire')}}
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing">{{ $t(faquestions.typestr) }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <b-form-group :label="this.$t('ManagerFAQ.Detail.Label.Content')"
                                              :label-cols="1"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing? 'required' : ''">
                                    <div v-show="editing">
                                        <app-ckeditor v-model="faquestions.content">
                                        </app-ckeditor>
                                    </div>
                                    <div v-if="!editing" v-html="faquestions.content"></div>
                                    <b-form-input type="text" style="display: none;"
                                                  v-model="$v.faquestions.content.$model"
                                                  :state="$v.faquestions.content.$dirty? !$v.faquestions.content.$error : null"></b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.faquestions.content.required">
                                        {{$t('ManagerFAQ.Detail.Label.ContentRequire')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <div class="text-center">
                            <b-button type="button" variant="primary" @click="edit" v-if="!editing"><i class="fa fa-pencil-square-o"></i> {{$t("Button.Edit")}}</b-button>
                            <b-button type="button" variant="secondary" @click="back" v-if="!editing"><i class="fa fa-ban"></i> {{$t("Button.Back")}}</b-button>
                            <b-button type="submit" variant="primary" v-if="editing"><i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}</b-button>
                            <b-button type="button" variant="secondary" @click="cancel" v-if="editing"><i class="fa fa-ban"></i> {{$t("Button.Cancel")}}</b-button>
                        </div>
                    </b-form>
                </b-card>
            </b-col>
        </b-row>
    </div>
</template>
<script>
    import { validationMixin } from 'vuelidate'
    import { authorizationMixin } from '@/shared/mixins'
    import { required } from 'vuelidate/lib/validators'
    import ckeditor from '@/components/Ckeditor.vue'
    import i18n from '@/libs/i18n'

    export default {
        name: 'FAQuestionsDetail',
        mixins: [validationMixin, authorizationMixin],

        components: {
            'app-ckeditor': ckeditor
        },
        data() {
            return {
                faquestions: {
                    name: null,
                    content: null,
                    type: null
                },
                editing: false,
                lstLanguage: [
                    { id: 'en', text: this.$t('ManagerFAQ.Detail.Label.TypeEn') },
                    { id: 'vi', text: this.$t('ManagerFAQ.Detail.Label.TypeVi') }
                ],
            }
        },
        computed: {
            faquestionsId() {
                return this.$route.params.faquestionsId;
            },
        },
        validations: {
            faquestions: {
                name: { required },
                content: { required },
                type: { required }
            }
        },
        created() {
            this.loadfaquestionsDetail();
        },
        methods: {
            loadfaquestionsDetail() {
                var vm = this;
                return this.$services.get(`faquestions/${this.faquestionsId}`).done(faquestions => {
                    vm.faquestions = faquestions;
                });
            },
            edit() {
                this.editing = true;
            },
            back() {
                this.$router.push({ path: '/faquestions/manager_faq/list' });
            },
            cancel() {
                var vm = this;
                this.loadfaquestionsDetail().done(() => vm.editing = false);
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.put(`faquestions/${this.faquestionsId}`, this.faquestions).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmUpdate"));
                        vm.cancel();
                    });
                }
            }
        }
    }
</script>
<style scoped>
    div >>> .media {
        display: block;
    }
</style>
