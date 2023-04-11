﻿
<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t('ManagerFAQ.Create.Header')}}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="6" offset="0">
                                <b-form-group :label="this.$t('ManagerFAQ.Detail.Label.Question')"
                                              label-for="name"
                                              :label-cols="3"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text" id="name"
                                                  v-model="$v.faquestions.name.$model"
                                                  :state="$v.faquestions.name.$error ? false : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        {{$t('ManagerFAQ.Detail.Label.QuestionRequire')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('ManagerFAQ.Detail.Label.Type')"
                                              :label-cols="3"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <select2 :options="lstLanguage" :placeholder="this.$t('PlaceHolder.Select')" id="dpl_compId" :settings="{allowClear: true}"
                                             v-model="$v.faquestions.type.$model">
                                    </select2>
                                    <b-form-invalid-feedback>
                                        {{$t('ManagerFAQ.Detail.Label.TypeRequire')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <b-form-group :label="this.$t('ManagerFAQ.Detail.Label.Content')"
                                              :label-cols="1"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <app-ckeditor v-model="$v.faquestions.content.$model">
                                    </app-ckeditor>
                                    <b-form-input type="text" style="display: none;"
                                           v-model="$v.faquestions.content.$model" 
                                           :state="$v.faquestions.content.$dirty? !$v.faquestions.content.$error : null"></b-form-input>
                                    <b-form-invalid-feedback>
                                        {{$t('ManagerFAQ.Detail.Label.ContentRequire')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col class="text-center">
                                <b-form-group >
                                    <b-button type="submit" variant="primary">
                                        <i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}
                                    </b-button>
                                    
                                    <b-button type="button" variant="secondary" @click="cancel"><i class="fa fa-ban"></i> {{$t("Button.Cancel")}}</b-button>
                                </b-form-group>
                            </b-col>
                        </b-row>
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
        name: 'CreateFAQuestions',
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
                lstLanguage: [
                    { id: 'en', text: this.$t('ManagerFAQ.Detail.Label.TypeEn') },
                    { id: 'vi', text: this.$t('ManagerFAQ.Detail.Label.TypeVi') }
                ],
            }
        },
        validations: {
            faquestions: {
                name: { required },
                content: { required },
                type: { required },
            }
        },
        created() {

        },
        methods: {
            cancel() {
                this.$router.push({ path: '/faquestions/manager_faq/list' });
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {                    
                    var vm = this;
                    this.$services.post('faquestions', this.faquestions).done((id) => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmCreate"));
                        vm.$router.push({ path: '/faquestions/manager_faq/list' });
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
