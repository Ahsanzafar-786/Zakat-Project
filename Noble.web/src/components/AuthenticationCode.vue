<template>
    <div class="login-page">
        <div class="wrapper wrapper-full-page" style="background:url(/../../assets/img/bg/newBackground.jpg); background-size: cover; ">

            <div class=" full-page section-image " filter-color="black" v-if="recoveryCodes==false">
                <div class="content">
                    <div class="container">
                        <div class="col-lg-5 col-md-6 ml-auto mr-auto">
                            <div class="card card-login">
                                <div class="card-header text-center">
                                    <h5> Sign In</h5>

                                    <p>You have enabled two-factor authentication, so you will also need to enter the 6‑digit verification code from your mobile authenticator app, or use a recovery code..</p>
                                </div>
                                <div class="card-body">
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">
                                                <i class="fa fa-key"></i>
                                            </span>
                                        </div>
                                        <input id="email" v-model="$v.authenticationCode.$model" name="email" type="text" class="form-control" placeholder="Authenticator Code">

                                    </div>
                                    <span v-if="$v.authenticationCode.$error" class="error text-danger">
                                        <p v-if="!$v.authenticationCode.required">Authentication Code is required</p>
                                        <p v-if="!$v.authenticationCode.maxLength">Authentication Code Maximum length 6</p>
                                    </span>



                                </div>
                                <div class="card-footer ">

                                    <button class="btn btn-outline-primary  btn-block mb-3" v-on:click="Authenticate" v-bind:disabled="$v.authenticationCode.$invalid">Login</button>
                                    <p>Don't have access to your authenticator device?</p>
                                    <a class="headingColor d-block text-center" href="javascript:void(0)" v-on:click="SignInRecoveryCodes"><i class="fa fa-lock"></i>  Sign in with a recovery code.</a>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <footer class="footer footer-black  footer-white ">
                    <div class="container-fluid">
                        <div class="row">
                            <nav class="footer-nav">
                                <ul>
                                    <li>
                                        <a href="javascript:void(0)" target="_blank"> Version 1.0.0.0</a>
                                    </li>
                                </ul>
                            </nav>
                            <div class="credits ml-auto">
                                <span class="copyright">
                                    © 2021, <a href="https://www.techqode.com/" style="color:white; font-weight:bold;" target="_blank">Techqode</a> Pvt Ltd.
                                </span>
                            </div>
                        </div>
                    </div>
                </footer>
                <div class="card-footer col-md-3" v-if="loading">
                    <loading :active.sync="loading" :can-cancel="true" :is-full-page="true"></loading>
                </div>
            </div>

            <div class=" full-page section-image " filter-color="black" v-else>
                <div class="content">
                    <div class="container">
                        <div class="col-lg-5 col-md-6 ml-auto mr-auto">
                            <div class="card card-login">
                                <div class="card-header text-center">
                                    <h5> Recovery code verification</h5>

                                    <p>This login will not be remembered until you provide an authenticator app code at log in or disable 2FA and log in again</p>
                                </div>
                                <div class="card-body">
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">
                                                <i class="fa fa-key"></i>
                                            </span>
                                        </div>
                                        <input id="email" v-model="$v.recoveryCode.$model" name="email" type="text" class="form-control" placeholder="Recovery Codes">

                                    </div>
                                    <span v-if="recoveryCode" class="error  text-danger">
                                        <!--<p v-if="!$v.recoveryCode.required">Authentication Code is required</p>-->
                                    </span>



                                </div>
                                <div class="card-footer ">

                                    <button class="btn btn-outline-primary  btn-block mb-3" v-on:click="Authenticate" >Login</button>
                                   

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <footer class="footer footer-black  footer-white ">
                    <div class="container-fluid">
                        <div class="row">
                            <nav class="footer-nav">
                                <ul>
                                    <li>
                                        <a href="javascript:void(0)" target="_blank"> Version 1.0.0.0</a>
                                    </li>
                                </ul>
                            </nav>
                            <div class="credits ml-auto">
                                <span class="copyright">
                                    © 2021, <a href="https://www.techqode.com/" style="color:white; font-weight:bold;" target="_blank">Techqode</a> Pvt Ltd.
                                </span>
                            </div>
                        </div>
                    </div>
                </footer>
                <div class="card-footer col-md-3" v-if="loading">
                    <loading :active.sync="loading" :can-cancel="true" :is-full-page="true"></loading>
                </div>
            </div>


        </div>
     
    </div>
</template>
<script>
    import { required, maxLength } from 'vuelidate/lib/validators';
  
    export default {
       
        data: function () {
            return {
                authenticationCode: '',
                open: false,
                website: 'https://www.techqode.com/',
                error: '',
                show: false,
                customError: '',
                loading: false,
                recoveryCodes: false,
                login: [],

            }
        },
        validations: {
            authenticationCode:
            {
                required,
                maxLength: maxLength(6)
            },
            recoveryCode:
            {
                required
               
            }
        },

        methods: {
            SignInRecoveryCodes: function () {
                this.recoveryCodes = true;
            },
            Authenticate: function () {
                
                var root = this;
            
                this.$https.get('/account/AuthenticatorCode?code=' + this.authenticationCode + '&email=' + this.login.email + '&password=' + this.login.password + '&recoveryCode=' + this.recoveryCode).then(function (response) {
                    

                    if (response.data == false) {
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: 'Unable to load two-factor authentication user.!',
                                text: 'Unable to load two-factor authentication user.!',
                                showConfirmButton: false,
                                timer: 1000,
                                timerProgressBar: true,

                            });
                    }
                    else if (response.data !=null) {
                        root.$session.start();
                        root.$swal.fire(
                            {
                                toast: true,
                                position: 'top-end',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                                icon: 'success',
                                title: 'Logged in Successfully',
                                didOpen: (toast) => {
                                    toast.addEventListener('mouseenter', root.$swal.stopTimer)
                                    toast.addEventListener('mouseleave', root.$swal.resumeTimer)
                                }
                            });

                        localStorage.setItem('token', response.data.token);
                        localStorage.setItem('UserName', response.data.userName);
                        localStorage.setItem('RoleName', response.data.roleName);
                        localStorage.setItem('ImagePath', response.data.imagePath);
                        localStorage.setItem('CompanyID', response.data.companyId);
                        localStorage.setItem('UserID', response.data.userId);
                        localStorage.setItem('FullName', response.data.fullName);

                        this.bankDetail = localStorage.getItem('BankDetail') == 'true' ? true : false;
                        if (response.data.employeeId != null) {
                            if (localStorage.getItem('BankDetail') == 'true')
                                root.$router.push('/WholeSaleDay')
                            else {
                                root.$router.push('/daystart')
                            }
                            localStorage.setItem('EmployeeId', response.data.employeeId);
                        } else {


                            root.$router.push('/StartScreen');

                        }

                    }
                    
                 

                }).catch(error => {
                    console.log(error)
                })
            }
        },
       
        mounted: function () {
            
            if (this.$route.query != undefined) {
                this.login = this.$route.query.data;

            }
           
        }
    }
    </script>
    