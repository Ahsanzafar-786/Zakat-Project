<template>
    <body>
        <!--Left Sidenav-->
        <div class="left-sidenav">
            <!-- LOGO -->
            <div class="brand text-start ms-2">
                <a v-on:click="GoTo('/dashboard')" href="javascript: void()" class="logo" v-if="roleName == 'Noble Admin'">
                    <span>
                        <img src="msFakhry.png" alt="logo-small" class="logo-sm"
                            style="width:100px;height:auto; max-height:45px;">
                    </span>

                </a>
                <a href="javascript: void()" class="logo" v-else>
                    <span>
                        <img src="msFakhry.png" alt="logo-small" class="logo-sm"
                            style="width:100px;height:auto; max-height:45px;">
                    </span>

                </a>
            </div>

            <div class="menu-content h-100" data-simplebar>
                <ul class="metismenu left-sidenav-menu">
                    <li v-if="roleName == 'Noble Admin'">
                        <a v-on:click="GoTo('/chartDashboard')" href="javascript:void(0);">
                            <i data-feather="trending-up" class="align-self-center menu-icon"></i><span>{{
                                $t('Dashboard.Dashboard') }}</span>
                        </a>
                    </li>

                    <li v-if="roleName == 'Noble Admin'">
                        <a href="javascript:void(0);">
                            <i data-feather="grid" class="align-self-center menu-icon"></i><span>
                                {{ $t('Company.Company') }}
                            </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                        </a>
                        <ul class="nav-second-level" aria-expanded="false">
                            <li>
                                <a v-on:click="GoTo('/company')" href="javascript:void(0);">
                                    {{ $t('Company.Company') }}
                                </a>
                            </li>
                        </ul>
                    </li>
                    <li v-if="roleName != 'Noble Admin' ">
                        <a href="javascript:void(0);">
                            <i data-feather="user" class="align-self-center menu-icon"></i><span>
                                {{ $t('Payment.Payment') }}
                            </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                        </a>

                        <ul class="nav-second-level" aria-expanded="false">
                            <li>
                                <a v-on:click="GoTo('/payment')" href="javascript:void(0);"> {{ $t('Payment.BenificaryPayment')
                                }}</a>
                            </li>
                            <li>
                                <a v-on:click="GoTo('/dailyPayment')" href="javascript:void(0);"> {{ $t('Payment.DailyPayments')
                                }}</a>
                            </li>
                            
                        </ul>
                    </li>
                    <li v-if="roleName != 'Noble Admin'">
                        <a href="javascript:void(0);">
                            <i data-feather="user" class="align-self-center menu-icon"></i><span>
                                {{ $t('Dashboard.Benificary') }}
                            </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                        </a>

                        <ul class="nav-second-level" aria-expanded="false">
                            <li>
                                <a v-on:click="GoTo('/benificaries')" href="javascript:void(0);"> {{ $t('Dashboard.BenificaryRegistration')
                                }}</a>
                            </li>
                            <li>
                                <a v-on:click="GoTo('/benificarynote')" href="javascript:void(0);"> {{ $t('BenificaryNote.BenificaryNote')
                                }}</a>
                            </li>
                            <li v-if="roleName != 'Cashier'">
                                <a v-on:click="GoTo('/benificaryreports')" href="javascript:void(0);"> {{ $t('Payment.BenificaryReports')
                                }}</a>
                            </li>
                        </ul>
                    </li>
                    <li v-if="roleName != 'Noble Admin'">
                        <a href="javascript:void(0);">
                            <i data-feather="user" class="align-self-center menu-icon"></i><span>
                                Expenses
                            </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                        </a>

                        <ul class="nav-second-level" aria-expanded="false">
                            <li>
                                <a v-on:click="GoTo('/expense')" href="javascript:void(0);"> Expense</a>
                            </li>
                            <li>
                                <a v-on:click="GoTo('/expensecategory')" href="javascript:void(0);"> Expense Category</a>
                            </li>
                            <li>
                                <a v-on:click="GoTo('/expensereport')" href="javascript:void(0);"> Expense Report</a>
                            </li>
                            
                        </ul>
                    </li>
                    <li v-if="roleName != 'Noble Admin'">
                        <a href="javascript:void(0);">
                            <i data-feather="user" class="align-self-center menu-icon"></i><span>
                                {{ $t('Funds.Funds') }}
                            </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                        </a>

                        <ul class="nav-second-level" aria-expanded="false">
                            <li>
                                <a v-on:click="GoTo('/funds')" href="javascript:void(0);"> {{ $t('Funds.AddFunds')
                                }}</a>
                            </li>
                        </ul>
                        <ul class="nav-second-level" aria-expanded="false">
                            <li>
                                <a v-on:click="GoTo('/charityfundsreports')" href="javascript:void(0);"> {{ $t('Funds.CharityFundsReports')
                                }}</a>
                            </li>
                        </ul>
                        <ul class="nav-second-level" aria-expanded="false">
                            <li>
                                <a v-on:click="GoTo('/charityfundsresource')" href="javascript:void(0);"> {{ $t('Funds.CharityFundsResource')
                                }}</a>
                            </li>
                        </ul>
                    </li>
                    <li v-if="roleName != 'Nobel Admin' && roleName != 'Cashier'">
                        <a href="javascript:void(0);">
                            <i data-feather="user" class="align-self-center menu-icon"></i><span>
                                {{ $t('Dashboard.Setup') }}
                            </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                        </a>

                        <ul class="nav-second-level" aria-expanded="false">

                            <li>
                                <a v-on:click="GoTo('/signup')" href="javascript:void(0);"> {{ $t('Dashboard.AddUser')
                                }}</a>
                            </li>
                        </ul>
                        <ul class="nav-second-level" aria-expanded="false">
                            <li>
                                <a v-on:click="GoTo('/authorizedpersons')" href="javascript:void(0);"> 
                                {{ $t('Dashboard.AddAuthorizedPerson')
                                }}</a>
                            </li>
                        </ul>
                        <!-- <ul class="nav-second-level" aria-expanded="false">
                            <li>
                                <a v-on:click="GoTo('/approvalpersons')" href="javascript:void(0);"> 
                                    {{ $t('Dashboard.AddApprovalPerson') }}
                                </a>
                            </li>
                        </ul> -->
                        <ul class="nav-second-level" aria-expanded="false">
                            <li>
                                <a v-on:click="GoTo('/paymenttype')" href="javascript:void(0);"> {{ $t('Dashboard.AddPaymentType')
                                }}</a>
                            </li>
                        </ul>
                        <ul class="nav-second-level" aria-expanded="false">
                            <li>
                                <a v-on:click="GoTo('/charityresource')" href="javascript:void(0);"> 
                                    {{ $t('Dashboard.AddCharityFundsResources') }}</a>
                            </li>
                        </ul>
                    </li>
                    
                    <li v-if="roleName != 'Noble Admin'">
                        <a href="javascript:void(0);">
                            <i data-feather="user" class="align-self-center menu-icon"></i><span>
                                {{ $t('LedgerReport.ReportsManagement') }}
                            </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                        </a>

                        <ul class="nav-second-level" aria-expanded="false">
                            <li>
                                <a v-on:click="GoTo('/ledgerreport')" href="javascript:void(0);"> {{ $t('LedgerReport.LedgerReport')
                                }}</a>
                            </li>
                            <li>
                                <a v-on:click="GoTo('/paymentwisereport')" href="javascript:void(0);">{{ $t('Payment.PaymentWiseReport')
                                }}</a>
                            </li>
                            <li>
                                <a v-on:click="GoTo('/transactionreport')" href="javascript:void(0);"> Transaction Report</a>
                            </li>
                        </ul>
                    </li>
                    <li v-if="roleName != 'Noble Admin' && roleName != 'Cashier'">
                        <a href="javascript:void(0);">
                            <i data-feather="user" class="align-self-center menu-icon"></i><span>
                                {{ $t('SystemManagement.SystemManagement') }}
                            </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                        </a>

                        <ul class="nav-second-level" aria-expanded="false">
                            <li>
                                <a v-on:click="GoTo('/PrintSetting')" href="javascript:void(0);"> {{ $t('SystemManagement.PrintSettings')
                                }}</a>
                            </li>
                        </ul>
                        <ul class="nav-second-level" aria-expanded="false">
                            <li>
                                <a v-on:click="GoTo('/DataBackUp')" href="javascript:void(0);"> {{ $t('SystemManagement.DataBackUp')
                                }}</a>
                            </li>
                        </ul>
                        <ul class="nav-second-level" aria-expanded="false">
                            <li>
                                <a v-on:click="GoTo('/DataRestore')" href="javascript:void(0);"> {{ $t('SystemManagement.DataRestore')
                                }}</a>
                            </li>
                        </ul>
                    </li>
                    <li v-if="roleName != 'Noble Admin' && roleName != 'Cashier'">
                        <a href="javascript:void(0);">
                            <i data-feather="user" class="align-self-center menu-icon"></i><span>
                                {{ $t('CompanyProfile.CompanyProfile') }}
                            </span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span>
                        </a>
                        <ul class="nav-second-level" aria-expanded="false">
                            <li>
                                <a v-on:click="GoTo('/companyinfo')" href="javascript:void(0);"> {{ $t('CompanyProfile.CompanyInfo')
                                }}</a>
                            </li>
                            <li>
                                <a v-on:click="GoTo('/Imports')" href="javascript:void(0);">{{ $t('CompanyProfile.Imports') }}</a>
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
                            <a class="nav-link dropdown-toggle arrow-none waves-light waves-effect"
                                data-bs-toggle="dropdown" href="javascript:void(0)" role="button" aria-haspopup="false"
                                aria-expanded="false">
                                <i data-feather="search" class="topbar-icon"></i>
                            </a>

                            <div class="dropdown-menu dropdown-menu-end dropdown-lg p-0">
                                <!-- Top Search Bar -->
                                <div class="app-search-topbar">
                                    <form action="#" method="get">
                                        <input type="search" name="search" class="from-control top-search mb-0"
                                            placeholder="Type text...">
                                        <button type="submit"><i class="ti-search"></i></button>
                                    </form>
                                </div>
                            </div>
                        </li>

                        <li class="dropdown">
                            <a class="nav-link dropdown-toggle waves-effect waves-light nav-user" data-bs-toggle="dropdown"
                                href="javascript:void(0)" role="button" aria-haspopup="false" aria-expanded="false">
                                <i class="fas fa-globe"></i>
                                Language
                            </a>
                            <div class="dropdown-menu dropdown-menu-end">
                                <a @click="setLocale('en')" class="dropdown-item" href="javascript:void(0)">
                                    English
                                </a>
                                <a @click="setLocale('ar')" class="dropdown-item" href="javascript:void(0)">
                                   {{$t( 'Arabic')}}
                                </a>
                            </div>
                        </li>

                        <li class="dropdown">
                            <a class="nav-link dropdown-toggle waves-effect waves-light nav-user" data-bs-toggle="dropdown"
                                href="javascript:void(0)" role="button" aria-haspopup="false" aria-expanded="false">
                                <span class="mx-1 nav-user-name hidden-sm">{{ displayUserName }}</span>
                                <img src="assets/images/users/user-5.jpg" alt="profile-user"
                                    class="rounded-circle thumb-xs" />
                            </a>
                            <div class="dropdown-menu dropdown-menu-end">
                                <a v-on:click="UserProfile" class="dropdown-item" href="javascript:void(0)">
                                    <i data-feather="user" class="align-self-center icon-xs icon-dual me-1"></i> {{
                                        $t('Dashboard.MyProfile')
                                    }}
                                </a>

                                <div class="dropdown-divider mb-0"></div>
                                <a v-on:click="logout()" class="dropdown-item" href="javascript:void(0)">
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
                <router-view></router-view>
                <dashboard v-if="dashboard == 'Dashboard' && roleName != 'Cashier' "></dashboard>

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
            loading: false,
            companyId: '',
            dashboard: '',
            roleName: '',
            displayUserName: '',
            langs: ['en', 'ar'],

        }
    },
    methods: {
        GoTo: function (link) {
            if (link != 'dashboard') {
                this.dashboard = '';
            }

             
            this.$router.push({
                path: link,
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
            } else {
                language.use(ar)
            }

            localStorage.setItem('locales', locale);
            this.$router.go(this.$router.currentRoute.fullPath)
        },

        UserProfile: function () {
            this.dashboard = '';
            this.$router.push('/RegisterUser');
        },

        logout: function () {
            var root = this;
            //  var Swal = this.$swal
            var url = '/account/logout';
            this.$https.post(url, this.login).then(function (response) {

                if (response.data == "Success") {

                    var getLocale = localStorage.getItem('locales');

                    root.$session.destroy();
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

    },
    mounted: function () {
         

        if (this.$session.exists()) {

            this.companyId = localStorage.getItem('CompanyID');
            this.roleName = localStorage.getItem('RoleName');
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.dashboard = this.$route.name;
            this.displayUserName = localStorage.getItem('UserName');
        }

        if (!this.$session.exists()) {
            this.$router.push('/')
        }

    }
}
</script>
