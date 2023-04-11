<template>

    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("School.Student.List.Table.Header")}}</strong>
                        <a class="btn btn-success" v-if="lang == 'vi'" style="float: right; color: white;" href="download/template_HocSinh.xlsx">{{$t("Categories.Person.List.Table.ImportSample")}}</a>
                        <a class="btn btn-success" v-if="lang == 'en'" style="float: right; color: white;" href="download/template_Student.xlsx">{{$t("Categories.Person.List.Table.ImportSample")}}</a>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="12">
                                <b-form-group :label="this.$t('Categories.Person.List.Table.Import')"
                                              :label-cols="2"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">

                                    <b-form-file ref="file-input"
                                                 id="file-input" accept=".xlsx" @change="onChange($event)"
                                                 :placeholder="this.$t('School.Student.List.Table.ImportPlaceholder')"></b-form-file>

                                    <b-form-input type="text" style="display: none;"
                                                  v-model="$v.file.fileData.$model"
                                                  :state="$v.file.fileData.$error ? false : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        {{$t('Categories.Person.List.Table.ImportRequire')}}
                                    </b-form-invalid-feedback>

                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label-cols="4"
                                              :horizontal="true">
                                    <b-button v-if="authorize(['ManageStudent'])" type="submit" variant="primary"><i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}</b-button>
                                    <b-button type="button" variant="secondary" @click="cancel"><i class="fa fa-ban"></i> {{$t("Button.Cancel")}}</b-button>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="12">
                                <ImageUpload multiple_files />
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

    export default {
        name: 'PersonImport',
        mixins: [validationMixin, authorizationMixin],

        data() {
            return {
                file: {
                    fileData: null,
                    fileName: null
                }
            }
        },
        validations: {
            file: {
                fileData: { required }
            }
        },
        created() {

        },
        computed: {
            lang: function () {
                let language = localStorage.getItem("lang") == null ? "vi" : localStorage.getItem("lang");
                return language;
            }
        },
        methods: {
            onChange: function (evt) {
                var vm = this;
                var f = evt.target.files[0]; // FileList object

                var acceptExtensions = ["xlsx", "XLSX"];
                let fileExtensions = (/[.]/.exec(f.name)) ? /[^.]+$/.exec(f.name) : undefined;

                if (fileExtensions && !acceptExtensions.includes(fileExtensions[0])) {
                    this.$refs['file-input'].reset()
                    alert(i18n.t("Timesheet.TimeKeeping.List.ImportExcel.FileInputXlxs"));
                    return;
                }

                if (f.size > 5000000) {
                    this.$refs['file-input'].reset()
                    alert(i18n.t("Timesheet.TimeKeeping.List.ImportExcel.FileInput50Mb"));
                    return;
                };

                var reader = new FileReader();

                reader.onload = (function (theFile) {
                    return function (e) {
                        var binaryData = e.target.result;
                        //Converting Binary Data to base 64
                        vm.file.fileData = window.btoa(binaryData);
                    };
                })(f);
                reader.readAsBinaryString(f, vm);
                vm.file.fileName = f.name;
            },
            cancel() {
                this.$router.push({ path: '/school/student/list' });
            },
            save() {
                var vm = this;

                this.$v.$touch();
                this.file.Lang = vm.lang;

                if (!this.$v.file.$invalid) {
                    this.$services.post('person/student/import', this.file).done((res) => {

                        //clear file
                        vm.$refs['file-input'].reset()
                        vm.file.fileData = null;
                        vm.file.fileName = null;

                        //add download link
                        let a = document.createElement('a');
                        a.setAttribute("href", res.url);
                        a.click();

                        alert("Import thành công " + "(" + res.count + "/" + res.sumcount + ")");
                        vm.$router.push({ path: '/school/student/list' });
                    });
                }
            },
           
        }
    }

</script>
<style scoped>
</style>
