<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t('School.Grades.Detail.Header')}}</strong>
                    </div>
                    <b-form style="float:none;margin:auto;" @submit.prevent="save">
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.CompanyName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-select class="col-sm-12" :placeholder="this.$t('PlaceHolder.Select')" :disabled="true"
                                                   v-model="grades.compId" :options="lstcompany">
                                    </b-form-select>
                                </b-form-group>
                            </b-col>

                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('School.Grades.List.SearchForm.UnitName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">

                                    <b-form-input type="text" id="txt_code" v-if="editing"
                                                  v-model.trim="$v.grades.name.$model"
                                                  :state="$v.grades.name.$dirty? !$v.grades.name.$error : null">
                                    </b-form-input>

                                    <b-form-invalid-feedback v-if="!$v.grades.name.required">
                                        {{$t('School.Grades.Detail.Label.NameRequire')}}
                                    </b-form-invalid-feedback>

                                    <label class="col-form-label" v-if="!editing">{{ grades.name }}</label>
                                </b-form-group>
                            </b-col>

                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('School.Grades.Detail.Label.Unit')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">

                                    <b-form-input type="number" style="display: none;" id="txt_gradenumber" min="1" max="12" step="1"
                                                  v-model="$v.grades.gradeNumber.$model"
                                                  :state="$v.grades.gradeNumber.$dirty? !$v.grades.gradeNumber.$error : null">
                                    </b-form-input>
                                    <select2 v-if="editing" style="width: 100% !important; margin: 0; padding: 0;" class="col-sm-12" placeholder="Chọn giá trị"
                                             v-model="grades.gradeNumber" :options="lstgradenumber">
                                    </select2>

                                    <b-form-invalid-feedback v-if="!$v.grades.gradeNumber.required">

                                        {{$t('School.Grades.Detail.Label.CodeRequire')}}
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing">{{ grades.gradeNumber }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('School.Grades.Detail.Label.Descriptions')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <label class="col-form-label" v-if="!editing">{{ grades.descriptions }}</label>
                                    <b-form-input v-if="editing" type="text" id="txt_descriptions"
                                                  v-model="grades.descriptions">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>

                        </b-row>

                        <b-row>
                            <b-col class="text-center">
                                <b-form-group >
                                    <b-button type="button" variant="primary" @click="edit" v-if="!editing && authorize(['ManageGrades'])"><i class="fa fa-pencil-square-o"></i> {{$t("Button.Edit")}}</b-button>
                                    <b-button type="button" variant="secondary" @click="back" v-if="!editing"><i class="fa fa-ban"></i> {{$t("Button.Back")}}</b-button>
                                    <b-button type="submit" variant="primary" v-if="editing"><i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}</b-button>
                                    <b-button type="button" variant="secondary" @click="cancel" v-if="editing"><i class="fa fa-ban"></i> {{$t("Button.Cancel")}}</b-button>
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
    import { required, maxLength, helpers } from 'vuelidate/lib/validators'
    const passwordPattern = helpers.regex('passwordPattern', /^[a-zA-Z0-9_]*$/)
    import Services from '@/utils/services'
    import i18n from '@/libs/i18n'

    export default {
        name: 'GradesDetail',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                grades: {
                    compId: null,
                    name: null,
                    gradeNumber: null,
                    descriptions: null,
                }, lstcompany: [], lstgradenumber: [],
                isAdmin: false,
                editing: false,
            }
        },
        validations: {
            grades: {
                name: { required },
                gradeNumber: { required },
            }
        },
        async created() {

            var accessToken = Services.getAccessToken();
            this.grades.compId = accessToken.comId;
            // this.department.departmenT_ID = accessToken.unitId;
            this.initListGradeNumber();
            await this.loadCompany();
            await this.loadDetail();
            //await this.loadDepartment();
        },
        computed: {
            Id() {
                return this.$route.params.Id;
            },
        },
        methods: {
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(lstcompany => {
                    vm.lstcompany = lstcompany;
                });
            },
            initListGradeNumber() {
                // init 12 number - from 1 to 12
                this.lstgradenumber = [];
                for (var i = 0; i < 12; i++) {
                    var itemGrade = {};
                    var number = i + 1;
                    itemGrade.id = number;
                    itemGrade.value = number;
                    itemGrade.text = " Lớp " + number;
                    this.lstgradenumber.push(itemGrade);
                }
            },
            async loadDetail() {
                var vm = this;
                return this.$services.get(`grades/${this.Id}`).done(grades => {
                    vm.grades = grades;
                });
            },
            edit() {
                this.editing = true;
            },
            back() {
                this.$router.push({ path: '/school/grades/list' });
            },
            async cancel() {
                var vm = this;
                await this.loadDetail();
                vm.editing = false;
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.put(`grades/${this.Id}`, this.grades).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmUpdate"));
                        vm.cancel();
                    });
                }
            }
        }
    }
</script>
<style scoped>
</style>
