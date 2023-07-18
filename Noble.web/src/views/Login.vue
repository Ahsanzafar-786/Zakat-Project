﻿<template>
<body class="account-body accountbg">

    <!-- Log In page -->
    <div class="container">
        <div class="row vh-100 d-flex justify-content-center">
            <div class="col-12 align-self-center">
                <div class="row">
                    <div class="col-lg-5 mx-auto">
                        <div class="card">
                            <div class="card-body p-0 auth-header-box bg-white">
                                <div class="text-center p-3">
                                    <a href="javascript:void(0);" class="logo logo-admin">
                                        <img src="msFakhry.png" height="50" alt="logo" class="auth-logo">
                                    </a>
                                    <h4 class="mt-3 mb-1 fw-semibold font-18" style="color: #0C213A;">NMS Fakhry-ERP</h4>
                                    <p class="text-muted  mb-0">{{ $t('login.WelcomePleaselogintoyouraccount') }}</p>
                                </div>
                            </div>
                            <div class="card-body p-0">
                                <div class="tab-content">
                                    <div class="tab-pane active p-3" id="LogIn_Tab" role="tabpanel">
                                        <div class="form-horizontal auth-form">

                                            <div class="form-group mb-2">
                                                <label class="form-label">{{ $t('login.UsernameorEmail') }}</label>
                                                <div class="input-group">
                                                    <input v-model="$v.login.email.$model" type="text" class="form-control" @keyup.enter="userlogin" :placeholder="$t('login.Enteryourusernameoremail')">
                                                </div>
                                            </div>
                                            <!--end form-group-->

                                            <div class="form-group mb-2">
                                                <label class="form-label">{{ $t('login.RegisterUser_Password')}}</label>
                                                <div class="input-group ">
                                                    <input v-model="$v.login.password.$model" :type="password" class="form-control" @keyup.enter="userlogin" :placeholder="$t('login.EnteryourPassword')">
                                                    <div style=" position: absolute; top: 24%; " @click="showPassword" v-bind:style="$i18n.locale == 'en' ? 'left: 94%' : 'left: 11px'">
                                                        <i class="fas fa-eye" v-if="eyeValue == false"></i>
                                                        <i class="fas fa-eye-slash" v-if="eyeValue == true"></i>
                                                    </div>
                                                </div>

                                            </div>
                                            <!--end form-group-->

                                            <div class="form-group row my-3">
                                                <div class="col-sm-6">
                                                    <div class="custom-control custom-switch switch-success">
                                                        <input v-model="login.rememberMe" type="checkbox" class="custom-control-input" id="customSwitchSuccess">
                                                        <label class="form-label text-muted mx-1" for="customSwitchSuccess">{{ $t('login.Rememberme')
                                                                }}</label>
                                                    </div>
                                                </div>
                                                <!--end col-->
                                                <div class="col-sm-6 text-end">
                                                    <a href="javascript:void(0)" v-on:click="openmodel" class="text-muted font-13"><i class="dripicons-lock"></i> {{
                                                                $t('login.ForgotPassword?') }}</a>
                                                </div>
                                                <!--end col-->
                                            </div>
                                            <!--end form-group-->

                                            <div class="form-group mb-0 row">
                                                <div class="col-12">
                                                    <small class="text-danger">{{ customError }}</small>
                                                    <button v-on:click="userlogin" class="btn btn-primary w-100 waves-effect waves-light" type="button">
                                                        <i v-if="loading" class="la la-refresh text-secondary la-spin progress-icon-spin" style="font-size:.8125rem;"></i>
                                                        <span v-else>{{ $t('login.Login1') }} <i class="fas fa-sign-in-alt ms-1"></i></span>
                                                    </button>
                                                </div>
                                                <!--end col-->
                                            </div>
                                            <!--end form-group-->
                                        </div>
                                        <!--end form-->
                                        <!--<div class="m-3 text-center text-muted">
                                                <p class="mb-0">Don't have an account ?  <a href="auth-register.html" class="text-primary ms-2">Free Resister</a></p>
                                                </div>-->
                                        <div class="account-social">
                                            <h6 class="mb-3 " style="color:transparent;">-</h6>
                                        </div>
                                        <div class="btn-group w-100">
                                            <button type="button" class="btn btn-sm btn-outline-secondary">{{ $t('login.Support')}}</button>
                                            <button type="button" class="btn btn-sm btn-outline-secondary">{{ $t('login.ContactUs')}}</button>
                                            <button type="button" class="btn btn-sm btn-outline-secondary">{{ $t('login.VisitUs')}}</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end card-body-->
                            <div class="card-body bg-light-alt text-center">
                                <span class="text-muted d-none d-sm-inline-block">
                                    <small>Version 1.2.9.5 Last Updated June 1, 2023. ms Fakhry (Pvt) Ltd.</small>
                                </span>
                            </div>
                        </div>
                        <!--end card-->
                    </div>
                    <!--end col-->
                </div>
                <!--end row-->
            </div>
            <!--end col-->
        </div>
        <!--end row-->
    </div>
    <!--end container-->
    <!-- End Log In page -->

    <forgotPassword :show="show" v-if="show" @close="show = false" />
</body>
</template>

<script>
import {
    required
} from 'vuelidate/lib/validators';

export default {

    data() {
        return {
            isPortuguese: false,
            langs: ['en', 'ar'],
            login: {
                email: '',
                password: '',
                rememberMe: true
            },
            password: "password",
            eyeValue: false,
            roles: [],
            open: false,
            website: 'https://www.techqode.com/',
            error: '',
            show: false,
            customError: '',
            loading: false,
            isEmployee: true,
            isAccount: false,
            IsProceed: false,
            heading: '',
            description: '',
            companyName: '',
            applicationName: '',
            email: '',
            favName: '',
            terms: false,
            os: '',
            loginHistory: {
                userId: '',
                isLogin: true,
                operatingSystem: '',
                companyId: ''
            },
            isPayment: false,
            expirationMsg: '',
        }
    },
    validations: {
        login: {
            email: {
                required
            },
            password: {
                required
            }
        }
    },

    methods: {

        showPassword() {
            if (this.password === "password") {
                this.password = "text";
                this.eyeValue = true;
            } else {
                this.password = "password"
                this.eyeValue = false;
            }

        },
        hidepassword() {
            this.password = "password"
            this.eyeValue = false;
        },
        setLocale(locale) {

            this.$i18n.locale = locale
            localStorage.setItem('locales', locale);
        },

        openmodel: function () {
            this.show = !this.show;
        },

        userlogin: function () {
            var root = this;
            root.customError = '';
            root.loading = true;
            var url = '/account/login';
             
            this.$https.post(url, this.login).then(function (response) {
                 
                if (response.data.companyId == "00000000-0000-0000-0000-000000000000") {

                    root.loading = false;
                    root.customError = 'Invalid Login Attempt.';
                   
                }
                else if (response.data != null) {
                    root.$session.start();
                    localStorage.setItem('CompanyID', response.data.companyId);
                    localStorage.setItem('token', response.data.token);
                    localStorage.setItem('RoleName', response.data.roleName);
                    localStorage.setItem('UserId', response.data.userId);
                    localStorage.setItem('UserName', response.data.userName);
                    localStorage.setItem('Email', response.data.emailConfirmed);
                    root.ReportServerLog();

                    root.$router.push('/dashboard');
                }

            }).catch(error => {
                root.customError = JSON.stringify(error.response.data.error);
                root.loading = false;
            })

        },
        ReportServerLog: function () {
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.$https.get('/account/ReportServer' , { headers: { "Authorization": `Bearer ${token}` } }) 
            .then(function (response) {
                   console.log(response);
                },
                    function (error) {
                        this.loading = false;
                        console.log(error);
                    });               

        },
    },
    created: function () {

    },
    mounted() {},
}
</script>
