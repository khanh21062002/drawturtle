/// <reference path="../public/assets/js/theme.js" />
/// <reference path="../public/assets/js/theme.js" />
/// <reference path="../public/assets/js/theme.js" />
// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
//import 'core-js/es6/promise'
//import 'core-js/es6/string'
//import 'core-js/es7/array'
// import cssVars from 'css-vars-ponyfill'

//import * as jquery from './assets/vendor/jquery/jquery.js';   
//import theme from './assets/js/theme.js';  

import 'select2';                       // globally assign select2 fn to $ element
import 'select2/dist/css/select2.css';  // optional if you have css loader
import 'vue-loading-overlay/dist/vue-loading.css';
import '@riophae/vue-treeselect/dist/vue-treeselect.css'
import 'vue2-datepicker/index.css';
import 'font-awesome/css/font-awesome.css';
import 'vue-ads-table-tree/dist/vue-ads-table-tree.css';

import jQuery from "jquery";
import 'core-js/stable'
import Vue from 'vue'
import App from './App'
import router from './router'
import CoreuiVue from '@coreui/vue'
/*import { iconsSet as icons } from './assets/icons/icons.js'*/
import store from './store/index'
import BootstrapVue from 'bootstrap-vue'
import Toastr from 'vue-toastr';
import Services from './utils/services';
import select2 from '@/components/Select2'
import bgrid from '@/components/BGrid'
import UnitTree from '@/components/UnitTree'
import moment from 'moment';
import VueMoment from 'vue-moment'
import Vuelidate from 'vuelidate'
import Loading from 'vue-loading-overlay';
import Treeselect from '@riophae/vue-treeselect'
import './utils/common'
import DatePicker from 'vue2-datepicker';
import i18n from '@/libs/i18n'
import DepartmentTree from '@/components/DepartmentTree'
import popupModal from '@/components/PopupModal.vue'
import confirmModal from '@/components/ConfirmModal.vue'
import VueAdsTableTree from 'vue-ads-table-tree';

Vue.config.performance = true


//require("expose-loader?$!jquery");
//require('vue-toastr/dist/vue-toastr.min.css');

Vue.use(VueMoment, { moment });
Vue.use(Toastr, {
    defaultPosition: 'toast-bottom-right'
});
Vue.use(CoreuiVue)
Vue.use(BootstrapVue)
Vue.prototype.$services = Services;
Vue.use(Vuelidate);
Vue.use(Loading);

Vue.component('select2', select2);
Vue.component('BGrid', bgrid);
Vue.component('UnitTree', UnitTree);
Vue.component('treeselect', Treeselect);
Vue.component('datePicker', DatePicker);
Vue.component('DepartmentTree', DepartmentTree);
Vue.component('vue-ads-table-tree', VueAdsTableTree);
Vue.component('PopupModal', popupModal);
Vue.component('ConfirmModal', confirmModal);

Vue.filter('formatDate', function (value, format) {
    if (value) {
        format = format || 'MM/DD/YYYY';
        return moment(String(value)).format(format);
    }
});
Vue.filter('formatCurrency', function (value) {
    if (value) {
        return value.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
    }
});
store.dispatch("loadIdentity");

new Vue({
    el: '#app',
    router,
    store,
    i18n,
    //icons,
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
    }
});
