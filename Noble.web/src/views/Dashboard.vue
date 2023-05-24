<template>

    <body>
        <!--Left Sidenav-->
        <div class="left-sidenav">
            <!-- LOGO -->
            <div class="brand text-start ms-2">
                <a href="index.html" class="logo">
                    <span>
                        <img src="Smartdigitalerpsol.png" alt="logo-small" class="logo-sm"
                             style="width:100px;height:auto; max-height:45px;">
                    </span>

            </a>
        </div>
        
       
        <div class="menu-content h-100" data-simplebar>
            <ul class="metismenu left-sidenav-menu">
                <li >
                    <a href="javascript:void(0);">
                        <i data-feather="trending-up" class="align-self-center menu-icon"></i><span>{{ $t('Dashboard.Dashboard') }}</span>
                    </a>
                </li>
                <li>
                    <a href="javascript:void(0);">
                        <i data-feather="grid" class="align-self-center menu-icon"></i><span>
                            {{
                                    $t('Dashboard.Dashboard')
                                }}
                        </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                    </a>
                    <ul class="nav-second-level" aria-expanded="false">
                        <li>
                            <a href="javascript:void(0);">
                                Company
                                Attachments
                            </a>
                        </li>
                    </ul>
                </li>
            </ul>

        </div>
    </div>
    <!--end left-sidenav-->

    <div class="page-wrapper">
        <!--Top Bar Start-->
        <div class="topbar">
            <!--Navbar-->
            <nav class="navbar-custom">
                <ul class="list-unstyled topbar-nav float-end mb-0">
                    <li class="dropdown hide-phone">
                        <a class="nav-link dropdown-toggle arrow-none waves-light waves-effect" data-bs-toggle="dropdown" href="javascript:void(0)" role="button" aria-haspopup="false" aria-expanded="false">
                            <i data-feather="search" class="topbar-icon"></i>
                        </a>

                        <div class="dropdown-menu dropdown-menu-end dropdown-lg p-0">
                            <!-- Top Search Bar -->
                            <div class="app-search-topbar">
                                <form action="#" method="get">
                                    <input type="search" name="search" class="from-control top-search mb-0" placeholder="Type text...">
                                    <button type="submit"><i class="ti-search"></i></button>
                                </form>
                            </div>
                        </div>
                    </li>

                    
                    <li class="dropdown">
                        <a class="nav-link dropdown-toggle waves-effect waves-light nav-user" data-bs-toggle="dropdown" href="javascript:void(0)" role="button" aria-haspopup="false" aria-expanded="false">

                            <i class="fas fa-globe"></i>
                            Language
                        </a>
                        <div class="dropdown-menu dropdown-menu-end">
                            <a v-if="english=='true'" @click="setLocale('en')" class="dropdown-item" href="javascript:void(0)">
                                English
                            </a>
                            <a v-if="arabic=='true'" @click="setLocale('ar')" class="dropdown-item" href="javascript:void(0)">
                                Arabic
                            </a>
                        </div>
                    </li>

                    <li class="dropdown">
                        <a class="nav-link dropdown-toggle waves-effect waves-light nav-user" data-bs-toggle="dropdown" href="javascript:void(0)" role="button" aria-haspopup="false" aria-expanded="false">
                            <span class="mx-1 nav-user-name hidden-sm">{{ DisplayUserName }}</span>
                            <img src="assets/images/users/user-5.jpg" alt="profile-user" class="rounded-circle thumb-xs" />
                        </a>
                        <div class="dropdown-menu dropdown-menu-end">
                            <a  class="dropdown-item" href="javascript:void(0)">
                                <i data-feather="user" class="align-self-center icon-xs icon-dual me-1"></i> {{
                                                $t('Dashboard.MyProfile')
                                    }}
                            </a>
                            
                            <div class="dropdown-divider mb-0"></div>
                            <a class="dropdown-item" href="javascript:void(0)" >
                                <i data-feather="power" class="align-self-center icon-xs icon-dual me-1"></i> {{
                                                $t('Dashboard.LogOut')
                                    }}
                            </a>
                        </div>
                    </li>
                </ul>

                <ul class="list-unstyled topbar-nav mb-0">
                    <li>
                        <button class="nav-link button-menu-mobile">
                            <i data-feather="menu" class="align-self-center topbar-icon"></i>
                        </button>
                    </li>

                </ul>
            </nav>
            <!--end navbar-->
        </div>
        <!--Top Bar End-->
        <!--Page Content-->
        <div class="page-content">
            <router-view @input="logoutUser"></router-view>
            <div v-if="dashboard == '/dashboard'">
                <dashboard></dashboard>
            </div>

            <footer class="footer text-center text-sm-start">
                <span>
                    &copy;
                    2022 <a href="https://www.techqode.com/" target="_blank" class="fw-normal">Oobagest (Pvt) Ltd.</a>
                </span>

                <span class="text-muted d-none d-sm-inline-block float-end">
                    Version 1.2.9.5 Last Updated Jan 12, 2023
                </span>
            </footer>
        </div>
    </div>
    <supervisor-login-model @close="onCloseEvent" :show="show" :isFlushData="true" :isReset="true" v-if="show" />
    <loading :active.sync="loading" :can-cancel="true" :is-full-page="true"></loading>
</body>
</template>

<script>
import clickMixin from '@/Mixins/clickMixin'
import language from 'element-ui/lib/locale'
import ar from 'element-ui/lib/locale/lang/ar'
import en from 'element-ui/lib/locale/lang/en'
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/vue-loading.css';

export default {
    mixins: [clickMixin],
    name: 'locale-changer',
    components: {
        Loading
    },
    data() {
        return {
            isHrPortal: false,
            aboutUsDetail: '',
            createDocument: false,
            loading: false,
            IsDailyExpense: false,
            noblePermissions: '',
            paymentLimitInterval: '',
            companyId: '',
            langs: ['en', 'ar', 'pt'],
            invoiveItem: false,
            invoiveBarCode: false,
            invoiveBarCodeItem: false,
            saleOrderPerm: false,
            WholeSale: '',
            purchaseOrder: false,
            isMouseover: false,
            expenseBill: false,
            IsExpenseAccount: false,
            DisplayUserName: '',
            role: '',
            dashboard: '',
            ur: '',
            isAccount: '',
            isDayStart: '',
            arabic: '',
            english: '',
            isMasterProduct: false,
            nobleRole: '',
            show: false,
            loginHistory: {
                userId: '',
                isLogin: false,
                companyId: ''
            },
            dayStart: '',
            propValvue: '',
            saleMenu: false,
            inventoryMenu: false,
            wareHouseMenu: false,
            startOperationMenu: false,
            startOpSetupMenu: false,
            accountMenu: false,
            purchaseMenu: false,
            settingMenu: false,
            humanMenu: false,
            financialMenu: false,
            contractMenu: false,
            logisticMenu: false,
            importExportMenu: false,
            systemMenu: false,
            productionMenu: false,
            inquiryMenu: false,
            portugues: false,
            leftToRight: false,

            importExportSale: false,
            IsSimpleInvoice: false,
            AllSale: false,
            applicationName: '',
        }
    },
    methods: {
     
       
        GoTo: function (link, token, fromDashboard, formName, fromService) {
            
            localStorage.setItem('IsService', fromService);
           
            this.$router.push({
                path: link,
                query: {
                    token_name: token,
                    fromDashboard: fromDashboard,
                    formName: formName,
                }
            });
        },

        StartScreen: function () {
            this.$router.push('/StartScreen')
        },
        commingSoonPage: function () {
            this.$router.push('/CommingSoon')
        },
      
        
       
        setLocale(locale) {

            this.$i18n.locale = locale;
            if (locale == 'en') {
                language.use(en)
            } 
            else {
                language.use(ar)
            }

            localStorage.setItem('locales', locale);
            this.$router.go(this.$router.currentRoute.fullPath)

        },

        UserProfile: function () {
            this.$router.push('/RegisterUser');
        },
        
        logout: function () {
            var root = this;
            //  var Swal = this.$swal
            var url = '/account/logout';
            this.$https.post(url, this.login).then(function (response) {

                if (response.data == "Success") {
                    root.PaymentLimitIntervalClear();
                    root.logoutHistorySave();

                    var getLocale = localStorage.getItem('locales');

                    root.$session.destroy();
                    //document.cookie.split(';').forEach(cookie => document.cookie = cookie.replace(/^ +/, '').replace(/=.*/, `=;expires=${new Date(0).toUTCString()};path=/`));
                    localStorage.clear();

                    localStorage.setItem('locales', getLocale);

                    root.$router.push('/')

                } else {
                    root.$swal.fire({
                        icon: 'error',
                        title: 'Error Logging Out'
                    });
                }

            });

        },
        
    },
    created() {
        this.leftToRight = localStorage.getItem('LeftToRight') == "true" ? true : false;
        this.isHrPortal = localStorage.getItem('HrPortal') == "true" ? true : false;

        this.applicationName = localStorage.getItem('ApplicationName')
        //this.PaymentLimitIntervalFunc();
    },
    mounted: function () {

        this.companyId = localStorage.getItem('CompanyID')

       
        if (!this.$session.exists()) {
            this.$router.push('/')
        }
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
    }
}
</script>
