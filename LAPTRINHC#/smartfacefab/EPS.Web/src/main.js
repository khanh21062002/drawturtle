// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
//import 'core-js/es6/promise'
//import 'core-js/es6/string'
//import 'core-js/es7/array'
// import cssVars from 'css-vars-ponyfill'

import 'select2';                       // globally assign select2 fn to $ element
import 'select2/dist/css/select2.css';  // optional if you have css loader
import 'vue-loading-overlay/dist/vue-loading.css';
import '@riophae/vue-treeselect/dist/vue-treeselect.css'
import 'vue2-datepicker/index.css';
import 'font-awesome/css/font-awesome.css';
import 'vue2-timepicker/dist/VueTimepicker.css'
import 'apexcharts/dist/apexcharts.css'
import 'vue-ads-table-tree/dist/vue-ads-table-tree.css';
import "devextreme/dist/css/dx.common.css";
import "devextreme/dist/css/dx.light.css";
import "@devexpress/analytics-core/dist/css/dx-analytics.common.css";
import "@devexpress/analytics-core/dist/css/dx-analytics.light.css";
import "devexpress-reporting/dist/css/dx-webdocumentviewer.css";
import 'vue-select/dist/vue-select.css';

import 'vue-slider-component/theme/default.css'
import '@vueform/slider/themes/default.css';
import flvPlayer from '@/components/FlvPlayer';
import flvLive from '@/components/FlvLive';
// paste the line below only if you need "bootstrap" theme
import 'vue-cool-select/dist/themes/bootstrap.css'
// paste the line below only if you need "material-design" theme
import 'vue-cool-select/dist/themes/material-design.css'
// you can also import your theme

/*import 'vue-ads-table-tree/dist/tailwind.css';*/

import 'core-js/stable'
import Vue from 'vue'
import App from './App'
import router from './router'
import CoreuiVue from '@coreui/vue'
import { iconsSet as icons } from './assets/icons/icons.js'
import store from './store/index'
import BootstrapVue from 'bootstrap-vue'
import Toastr from 'vue-toastr';
import Services from './utils/services';
//import select2 from '@/components/Select2'
import bgrid from '@/components/BGrid'
import kpgrid from '@/components/KPGrid'
import popupModal from '@/components/PopupModal.vue'
import confirmModal from '@/components/ConfirmModal.vue'
import VueSlider from 'vue-slider-component'
import UnitTree from '@/components/UnitTree'
import DepartmentTree from '@/components/DepartmentTree'
import ImageUpload from '@/components/ImageUpload'
import ckeditor from '@/components/Ckeditor.vue'
import currencyInput from '@/components/CurrencyInput'
import moment from 'moment';
import VueMoment from 'vue-moment'
import Vuelidate from 'vuelidate'
import Loading from 'vue-loading-overlay';
import Treeselect from '@riophae/vue-treeselect'
import './utils/common'
import DatePicker from 'vue2-datepicker';
import 'vue2-datepicker/locale/vi';

import VueTimepicker from 'vue2-timepicker';
//import select2 from '@/components/Select2'
import select2 from 'v-select2-component';
import JsonExcel from "@/components/JsonExcelCustom";
import VueApexCharts from 'vue-apexcharts'
import EasyCamera from '@/components/EasyCamera';
import VueAdsTableTree from 'vue-ads-table-tree';
import Print from 'vue-print-nb';
import i18n from '@/libs/i18n'
import WebDocumentViewer from '@/components/WebDocumentViewer';
import CKEditor from '@ckeditor/ckeditor5-vue2';
import vSelect from 'vue-select'
import { CoolSelectPlugin } from 'vue-cool-select'
import en from 'date-format-parse/lib/locale/en';
import VueCompositionAPI from '@vue/composition-api';
import Slider from '@vueform/slider/dist/slider.vue2.js'
import VueSignalR from '@latelier/vue-signalr';
Vue.use(Print);
Vue.config.performance = true
const isDevEnv = process.env.NODE_ENV == 'development';



//require("expose-loader?$!jquery");
//require('vue-toastr/dist/vue-toastr.min.css');
Vue.use(CoolSelectPlugin)
Vue.use(EasyCamera);
Vue.use(VueMoment, { moment });
Vue.use(Toastr, {
    defaultPosition: 'toast-bottom-right'
});
Vue.use(CoreuiVue)
Vue.use(BootstrapVue)
Vue.prototype.$services = Services;
Vue.use(Vuelidate);
Vue.use(Loading);
Vue.use(CKEditor);
Vue.use(VueCompositionAPI)
Vue.use(VueSignalR, isDevEnv ? 'http://localhost:1938/eventHub' : 'https://smartfactory.atin.vn/Service/eventHub');
Vue.component('currency-input', currencyInput);
Vue.component('v-select', vSelect)
Vue.component('WebDocumentViewer', WebDocumentViewer);
Vue.component("v-easy-camera", EasyCamera);
Vue.component("downloadExcel", JsonExcel);
Vue.component('FlvPlayer', flvPlayer);
Vue.component('Live', flvLive);
Vue.component('select2', select2);
Vue.component('BGrid', bgrid);
Vue.component('KPGrid', kpgrid);
Vue.component('UnitTree', UnitTree);
Vue.component('DepartmentTree', DepartmentTree);
Vue.component('ImageUpload', ImageUpload);
Vue.component('Ckeditor', ckeditor);
Vue.component('PopupModal', popupModal);
Vue.component('ConfirmModal', confirmModal);

Vue.component('treeselect', Treeselect);
Vue.component('datePicker', DatePicker);
Vue.component('VueTimepicker', VueTimepicker);
Vue.component('apexchart', VueApexCharts);
Vue.component('vue-ads-table-tree', VueAdsTableTree);
Vue.component('Slider', Slider);
Vue.component('VueSlider', VueSlider)

Vue.filter('formatDate', function (value, format) {
    if (value) {
        format = format || 'DD/MM/YYYY';
        return moment(String(value)).format(format);
    }
});

Vue.filter('formatDateTime', function (value, format) {
    if (value) {
        format = format || 'DD/MM/YYYY HH:mm';
        return moment(String(value)).format(format);
    }
});

Vue.filter('formatTime', function (value, format) {
    if (value) {
        format = format || 'HH:mm:ss';
        return moment(String(value)).format(format);
    }
});
Vue.filter('formatTimeHour', function (value, format) {
    if (value) {
        format = format || 'HH:mm';
        return moment(String(value)).format(format);
    }
});

Vue.filter('formatCurrency', function (value) {
    if (value) {
        return value.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
    }
});
store.dispatch("loadIdentity");

NodePlayer.load(() => {
    new Vue({
        el: '#app',
        router,
        store,
        i18n,
        icons,
        template: '<App/>',
        components: {
            App,
        },
        mounted() {
            //store.dispatch('initializeSettings');
            var self = this;
            $(document).ajaxSend(function (event, jqXHR, options) {
                if (options.async) {
                    let loader = self.$loading.show({
                        container: options.formContainer,
                        canCancel: true,
                    });

                    jqXHR.always(function () {
                        loader.hide();
                    });
                }
            });
            let locale = localStorage.getItem("lang");
            if (locale == 'en') {
                var lang = {
                    formatLocale: en,
                    yearFormat: 'YYYY',
                    monthFormat: 'MMM',
                    monthBeforeYear: true
                };
                DatePicker.locale('en', lang);
            }

        },
        created() {
            this.$socket.start({
                log: false, // Active only in development for debugging.
            });
        },
    });
});


