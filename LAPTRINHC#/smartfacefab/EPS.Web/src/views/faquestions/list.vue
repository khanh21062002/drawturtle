<template>
    <CRow>
        <CCol xl="12">
            <CCard>
                <CCardHeader>
                    <strong>{{$t("FAQ.List.Header")}}</strong>
                    <a class="btn btn-success" style="float: right; color: white;" href="download/ATSmartface_Huong-dan-su-dung_VN_v2.3.docx">{{$t("FAQ.List.BtnDownload")}}</a>
                </CCardHeader>
                <CCardBody>
                    <div>
                        <CCard class="mb-0" v-for="item in lstquestions" :key="item.id">
                            <CButton block
                                     color="link"
                                     class="text-left shadow-none card-header"
                                     @click="accordion = accordion === item.id ? false : item.id">
                                <h5 class="m-0">{{item.name}}</h5>
                            </CButton>
                            <CCollapse :show="accordion === item.id ">
                                <CCardBody >
                                    <div v-html="item.content"></div>
                                </CCardBody>
                            </CCollapse>
                        </CCard>
                    </div>
                </CCardBody>
            </CCard>
        </CCol>
    </CRow>
</template>
<script>
    import { authorizationMixin } from '@/shared/mixins';

    export default {
        name: 'ListQuestions',
        mixins: [authorizationMixin],
        data() {
            return {
                accordion: true,
                lstquestions: []
            }
        },
        created() {
            this.loadQuestions();
        },
        methods: {
            //Danh sách câu hỏi
            loadQuestions() {
                var vm = this;
                let localeCache = localStorage.getItem("lang");
                if (localeCache == null) {
                    localeCache = "vi";
                }
                return this.$services.get(`lookup/questions/${localeCache}`).done(lstquestions => {
                    vm.lstquestions = lstquestions;
                });
            }

        }
    }
</script>
<style scoped>
    div >>> .media {
        display: block;
    }
</style>
