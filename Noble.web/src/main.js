import Vue from 'vue'
import App from './App.vue'
import router from './router'
import { store } from './store'
import Vuelidate from 'vuelidate'
import i18n from './i18n'
import cors from 'cors'
import moment from 'moment'
import axios from 'axios'
import VueSweetalert2 from 'vue-sweetalert2';
import 'sweetalert2/dist/sweetalert2.min.css';
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/vue-loading.css';
import { BootstrapVue, BPagination } from 'bootstrap-vue'
Vue.use(BootstrapVue, BPagination);
import ElementUI from 'element-ui'
import { DatePicker } from 'element-ui';
import ar from 'element-ui/lib/locale/lang/ar'
import en from 'element-ui/lib/locale/lang/en'


import 'element-ui/lib/theme-chalk/index.css';
import locale from 'element-ui/lib/locale'
Vue.use(DatePicker);

Vue.use(ElementUI, { locale });
import 'vue2-timepicker/dist/VueTimepicker.css'
import 'vue-multiselect/dist/vue-multiselect.min.css';
import VueHtmlToPaper from 'vue-html-to-paper';
import ToggleButton from 'vue-js-toggle-button';
import clickMixin from '@/Mixins/clickMixin';
import json from '../public/Config.json';

import { MonthPicker } from 'vue-month-picker'
import { MonthPickerInput } from 'vue-month-picker'
import VueCryptojs from 'vue-cryptojs'
import vueshortkey from 'vue-shortkey'

Vue.use(VueCryptojs)
Vue.use(MonthPicker)
Vue.use(MonthPickerInput)
Vue.use(clickMixin);
Vue.use(vueshortkey);

//Vue.use(VueRadioButton);
Vue.use(ToggleButton);
Vue.prototype.$locale = locale;


if (i18n.locale === 'en') {
    locale.use(en)
}
else {
    locale.use(ar);
}



const options = {
    name: '',
    specs: [
        'fullscreen=no',
        'titlebar=yes',
        'scrollbars=yes'
    ],
    styles: [
        'https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css',
        'https://unpkg.com/kidlat-css/css/kidlat.css',
    ],
    timeout: 1000,
    autoClose: true,
    windowTitle: window.document.title
}
Vue.use(VueHtmlToPaper, options);
Vue.use(VueHtmlToPaper);
import VueApexCharts from 'vue-apexcharts';
Vue.use(VueApexCharts);
Vue.component('apexchart', VueApexCharts);
//import the theme

// register VueFusionCharts component
Vue.use(VueSweetalert2);
Vue.use(Loading);
Vue.use(Vuelidate);
Vue.use(cors);
Vue.use(moment);
Vue.config.productionTip = false;
Vue.component('datepicker', require('./components/DatePicker.vue').default);
Vue.component('AddProductImage', require('./components/AddProductImage.vue').default);
Vue.component('dashboard', require('./components/Dashboard/Dashboard.vue').default);
Vue.component('modal', require('./components/modalcomponent.vue').default);
Vue.component('roledropdown', require('./components/General/RolesDropdown.vue').default);

// Benificary
Vue.component('benificary-mod', require('./components/Benificary/AddBenificary.vue').default);
Vue.component('benificary', require('./components/General/BenificaryDropdown.vue').default);
Vue.component('benificaryreports', require('./components/Benificary/BenificaryReports.vue').default);


//Authorized Person
Vue.component('authorizedPerson-mod', require('./components/AuthorizedPersons/AddAuthorizedPersons.vue').default);
Vue.component('authorizedperson', require('./components/General/AuthorizedPersonDropdown.vue').default);


//Benificary Note
Vue.component('benificarynote-mod', require('./components/BenificaryNote/AddBenificaryNote.vue').default);

// Charity Resources
Vue.component('charityresources-mod', require('./components/CharityResources/AddCharityResources.vue').default);
Vue.component('charityresources', require('./components/General/CharityResourcesDropdown.vue').default);

Vue.component('decimaltofix', require('./components/General/DecimalToFix.vue').default);

// Approval Person
Vue.component('approvalperson-mod', require('./components/ApprovalPersons/AddApprovalPersons.vue').default);
Vue.component('approvalperson', require('./components/General/ApprovalPersonDropdown.vue').default);

//comingsoon system management
Vue.component('print-setting', require('./components/PrintSettings/ComingSoon.vue').default);
Vue.component('databackup', require('./components/DataBackUp/ComingSoon.vue').default);
Vue.component('datarestore', require('./components/DataRestore/ComingSoon.vue').default);

//Payment Type
Vue.component('paymenttype-mod', require('./components/PaymentType/AddPaymentType.vue').default);
Vue.component('paymenttype', require('./components/General/PaymentType.vue').default);

//Funds
Vue.component('funds-mod', require('./components/Funds/AddFunds.vue').default);
Vue.component('charityfundsreports', require('./components/Funds/CharityFundsReports.vue').default);
Vue.component('charityfundsresource', require('./components/Funds/CharityFundsResource.vue').default);

//Expenses
Vue.component('expensecategory-mod', require('./components/Expenses/AddExpenseCategory.vue').default);
Vue.component('expense-mod', require('./components/Expenses/AddExpense.vue').default);
Vue.component('expensecategory', require('./components/General/ExpenseCategoryDropdown.vue').default);



//CompanyProfile
Vue.component('companyprofile-mod' , require('./components/CompanyProfile/CompanyProfile.vue').default);
Vue.component('companyinfo-mod', require('./components/CompanyProfile/CompanyInfo.vue').default);

//Print
Vue.component('print', require('./components/Print/Print.vue').default);

//userdropdwon
Vue.component('userdropdown', require('./components/General/UsersDropdown.vue').default);


Vue.prototype.$https = axios;
axios.defaults.baseURL = json.ServerIP;
Vue.prototype.$ServerIp = json.ServerIP;
Vue.prototype.$PermissionIp = json.PermissionIP;
Vue.prototype.$ClientIP = json.ClientIP;
Vue.prototype.$ReportServer = json.ReportServer;
Vue.prototype.$macAddess = 'D8-FC-93-2E-5B-94';
Vue.prototype.$ReportServer = json.ReportServer;
Vue.prototype.$SystemType = json.SystemType;

router.beforeEach((to, from, next) => { // <------------
    var getLocale = localStorage.getItem('locales');
    let language = getLocale;
    if (!language) {
        language = 'en'
    }
    i18n.locale = language
    next()
});


Vue.filter('englishLanguage',
    function (value) {

        var getEnglishLanguage = localStorage.getItem('English');
        var getArabicLanguage = localStorage.getItem('Arabic');
        var getPortuguesLanguage = localStorage.getItem('Portugues');
        if (getEnglishLanguage == 'true' && getArabicLanguage == 'false' && getPortuguesLanguage == 'false') {
            return value;
        }
        else if (getEnglishLanguage == 'false' && getArabicLanguage == 'true' && getPortuguesLanguage == 'false') {
            return value;
        }
        else if (getEnglishLanguage == 'false' && getArabicLanguage == 'false' && getPortuguesLanguage == 'true') {
            return value;
        }
       
        else {
            var lan = localStorage.getItem('locales');

            if (lan == 'en') {
                return value + ' (English)';
            }
            else if (lan == 'pt') {
                return value + ' (Inglês)';
            }
            else {
                return value + ' (انجليزي)';
            }

        }

    });

Vue.filter('arabicLanguage',
    function (value) {
        var getEnglishLanguage = localStorage.getItem('English');
        var getArabicLanguage = localStorage.getItem('Arabic');
        if (getEnglishLanguage == 'true' && getArabicLanguage == 'false') {
            return value;
        }
        else if (getEnglishLanguage == 'false' && getArabicLanguage == 'true') {
            return value;
        }
        else {
            var lan = localStorage.getItem('locales');
            if (lan == 'en' && getArabicLanguage == 'false') {
                return value + ' (Portugues)';
            }
            else if (lan == 'en' && getArabicLanguage == 'true') {
                return value + ' (Arabic)';
            }
            else {
                return value + ' (عربي)';
            }

        }
    });

Vue.filter('formatAmount', function (value) {
    if (!value) return '';

    return parseFloat(value).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
})

Vue.filter('roundAmount', function (value) {
    if (!value) return 0;

    return parseFloat(value).toFixed(3).slice(0, -1);
})

new Vue({
    router,
    store,
    i18n,
    render: h => h(App)
}).$mount('#app')
