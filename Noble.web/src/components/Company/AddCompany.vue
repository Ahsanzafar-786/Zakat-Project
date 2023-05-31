<template>
    <div class="col-md-12 ">
        <div class="row">
            <div class="col-sm-12">
                <div class="page-title-box">
                    <div class="row">
                        <div class="col">
                            <h4 class="page-title">{{$t('SetupCompany.SetupCompany')}}</h4>

                        </div>
                        <!-- <div class="col-auto align-self-center">

                            <a v-on:click="syncSetup()" href="javascript:void(0);"
                               class="btn btn-sm btn-outline-primary mx-1">
                                <i class="align-self-center icon-xs ti-plus"></i>
                                Sync Setup
                            </a>


                        </div> -->
                    </div>
                </div>
            </div>
        </div>
        <div class="card   col-lg-12  ">
            <div class="card-body">

                <div class="text-left">
                    <div class="row ">
                        <div class="col-sm-4" v-if="role=='Noble Admin'">
                            <label>{{$t('SetupCompany.ClientCode')}}:<span class="text-danger"> *</span></label>
                            <div>
                                <input class="form-control" disabled :key="rander" v-model="company.clientNo" />

                            </div>
                        </div>
                        <div class="col-sm-4">
                            <label>{{$t('SetupCompany.CompanyName')}}:<span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : $v.company.nameEnglish.$error}">
                                <input class="form-control" v-model="$v.company.nameEnglish.$model" />
                                <span v-if="$v.company.nameEnglish.$error" class="error text-danger">
                                    <span v-if="!$v.company.nameEnglish.required"> Name is Required</span>
                                    <span v-if="!$v.company.nameEnglish.maxLength"> Name maximum length 50</span>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <label for="company.email">{{$t('SetupCompany.CompanyEmail')}}: <span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : $v.company.companyEmail.$error,'has-blue':($v.company.companyEmail.required && $v.company.companyEmail.email)}">
                                <input class="form-control" v-model="$v.company.companyEmail.$model" />
                                <span v-if="$v.company.companyEmail.$error" class="error text-danger">
                                    <span v-if="!$v.company.companyEmail.required">Email is Required</span>
                                    <span v-if="!$v.company.companyEmail.email">Email is Invalid</span>
                                </span>

                            </div>

                        </div>
                        <div class="col-sm-4">
                            <label>{{$t('SetupCompany.CommercialRegistrationNo')}} :</label>
                          
                                <input class="form-control" v-model="company.comercialRegNo" />
                              
                        </div>
                        <div class="col-sm-4">
                            <label for="company.TaxRegisterNo">{{$t('SetupCompany.VATNTNTAXNo')}}: </label>
                            <input class="form-control" v-model="company.vatRegistrationNo" />
                          
                        </div>
                       
                        <div class="col-sm-4">
                            <label>{{$t('SetupCompany.Website')}}: </label>
                            <div>
                                <input class="form-control" v-model="company.website" />

                            </div>
                        </div>
                        <div class="col-sm-4">
                            <label>{{$t('SetupCompany.MobileNumber')}}: </label>
                            <input class="form-control" v-model="company.phoneNo" type="number" />
                        </div>
                        <div class="col-sm-4">
                            <label>{{$t('SetupCompany.LandlineNumber')}}: </label>
                            <input class="form-control" type="number" v-model="company.landLine" />

                        </div>

                        <div class="col-sm-4">
                            <label for="company.country">{{$t('SetupCompany.Country')}}: </label>
                            <country-select class="form-control" v-model="company.countryEnglish" :country="country" topCountry="SA" />

                        </div>
                        <div class="col-sm-4">
                            <label for="company.city">{{$t('SetupCompany.City')}}: </label>
                            <input class="form-control" v-model="company.cityEnglish" />

                        </div>
                        <div class="col-sm-8">
                            <label for="company.address">{{$t('SetupCompany.Address')}}: </label>
                            <textarea class="form-control" v-model="company.addressEnglish" />
                        </div>
                        <div v-if="$route.query.data!=undefined" class="card-footer col-md-12 text-right">
                            <a href="javascript:void(0)" class="btn btn-outline-primary  mr-2" v-on:click="SaveCompany" v-bind:disabled="$v.company.$invalid"><i class="fa fa-save"></i> Update</a>
                            <a href="javascript:void(0)" class="btn btn-outline-reset " v-on:click="GoToCompany"><i class="fa fa-refresh"></i> Cancel</a>


                        </div>

                    </div>

                </div>
            </div>

            <div class="card col-lg-12  " >
                <div class="card-body  " v-if="$route.query.data==undefined">
                    <div class="pb-2">
                        <h5 class="card-title  "> {{$t('SetupCompany.AdminInfo')}}</h5>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label for="company.email">{{$t('SetupCompany.Email')}}:<span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : $v.company.email.$error,'has-blue':($v.company.email.required && $v.company.email.email)}">
                                <input class="form-control" type="text" v-model="$v.company.email.$model" />
                                <span v-if="$v.company.email.$error" class="error text-danger">
                                    <span v-if="!$v.company.email.required">Email  is required  </span>
                                    <span v-if="!$v.company.email.email">Email is invalid</span>
                                    <!--<span v-if="!$v.company.email.isExist && $v.company.email.$error" class="error validation-error field-validation-valid">Email is already exist</span>-->
                                       
                                </span>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>{{$t('SetupCompany.Name')}}:<span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : $v.company.firstName.$error}">
                                <input class="form-control" autofocus="autofocus" v-model="$v.company.firstName.$model" />
                                <span v-if="$v.company.firstName.$error" class="error text-danger">
                                    <span v-if="!$v.company.firstName.required">Name is required  </span>
                                    <span v-if="!$v.company.firstName.maxLength">Name has maximum Length 30</span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label for="addLocation.password">{{ $t('SetupCompany.RegisterUser_Password') }} :<span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : $v.company.password.$error,'has-blue':($v.company.password.required && $v.company.password.strongPassword)}">
                                <input class="form-control " type="password" v-model="$v.company.password.$model" />
                                <span v-if="$v.company.password.$error" class="error text-danger">
                                    <span v-if="!$v.company.password.required">{{ $t('SetupCompany.RegisterUser_Error_Required_Password') }} </span>
                                    <span v-if="!$v.company.password.strongPassword">{{ $t('SetupCompany.RegisterUser_Error_Format_Password') }}</span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label for="addLocation.confirmPassword">{{ $t('SetupCompany.RegisterUser_ConfirmPassword') }}:<span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : $v.company.confirmPassword.$error,'has-blue':($v.company.confirmPassword.required && $v.company.confirmPassword.sameAsPassword)}">
                                <input class="form-control " type="password" v-model="$v.company.confirmPassword.$model" />
                                <span v-if="$v.company.confirmPassword.$error" class="error text-danger">
                                    <span v-if="!$v.company.confirmPassword.required">{{ $t('SetupCompany.RegisterUser_Error_Required_ConfirmPassword') }}  </span>
                                    <span v-if="!$v.company.confirmPassword.sameAsPassword">{{ $t('SetupCompany.RegisterUser_Error_SameAs_ConfirmPassword') }}</span>
                                </span>
                            </div>
                        </div>
                        <div class="text-center" hidden>
                            <span class="text-danger text-center ">You Received a mail and you Can Set Your Password from mail </span>
                        </div>

                        <div v-if="!loading" class=" col-md-12 pt-2 text-right">
                            <a href="javascript:void(0)" class="btn btn-outline-primary  me-2" v-on:click="SaveCompany" v-bind:disabled="$v.company.$invalid"><i class="fa fa-save"></i> {{$t('Save')}}</a>
                            <a href="javascript:void(0)" class="btn btn-outline-danger " v-on:click="GoToCompany"> {{$t('Close')}}</a>


                        </div>
                        <div class=" col-md-3" v-else>
                            <loading :active.sync="loading" :can-cancel="true" :is-full-page="true"></loading>
                        </div>

                    </div>



                </div>

            </div>
        </div>

    </div>

</template>
<script>


    import { required, maxLength, email, sameAs } from 'vuelidate/lib/validators';
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/vue-loading.css';
    import Vue from 'vue';
    import vueCountryRegionSelect from 'vue-country-region-select';
    Vue.use(vueCountryRegionSelect);
    export default ({
        components: {
            Loading
        },

        data: function () {
            return {
                active: 'company',
                country: '',
                company: {
                    id: '',
                    nameEnglish: '',
                    nameArabic: '',
                    vatRegistrationNo: '',
                    cityEnglish: '',
                    cityArabic: '',
                    countryEnglish: '',
                    countryArabic: '',
                    phoneNo: '',
                    website: '',
                    companyEmail: '',
                    logoPath: '',
                    addressEnglish: '',
                    addressArabic: '',
                    landLine: '',
                    comercialRegNo: '',
                    firstName: '',
                    username: '',
                    email: '',
                    phonenumber: '',
                    logo: '',
                    commercialRegistration: '',
                    businessLicence: '',
                    civilDefenceLicense: '',
                    cctvLicence: '',
                    companyAttachments: [],
                    password: '',
                    confirmPassword: ''
                },
                loading: false,
                randomNumber: '',
                rander: 0,
                rander1: 0,
                role: ''
            }
        },
        validations: {

            company:
            {
                nameEnglish:
                {
                    required,
                    maxLength: maxLength(50)
                },

             


                companyEmail:
                {
                    required,
                    email

                },
                password:
                {
                    required
                 
                    
                },
                confirmPassword:
                {
                    required,
                    sameAsPassword: sameAs('password')
                },
                

                firstName:
                {
                    required,
                    maxLength: maxLength(30)
                },


                email:
                {
                    required,
                    email,


                },

                landLine:
                {
                    maxLength: maxLength(15)
                }


            }
        },
        methods: {
            syncSetup: function () {
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }

                this.$https.get('/System/SyncSetup', { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        console.log(response.data);
                    })
            },

            RandomNumber: function () {

                //Math.floor(Math.random() * max) + min;
                //let len = 6;
                //let text = "1"
                //let chars = "abcdefghijklmnopqrstuvwxyz"

                //for (let i = 0; i < len; i++) {
                //    text += chars.charAt(Math.floor(Math.random() * chars.length))
                //}
                this.company.clientNo = Math.floor(1000000000 + Math.random() * 90000000);
                this.rander++;
            },
            GoToCompany: function () {

                this.$router.push('/company');
            },
            makeActive: function (tab) {
                this.active = tab;
            },
            getImage: function (value) { this.company.logoPath = value; },
            SaveCompany: function () {
                 
                
                this.loading = true;
                var root = this;
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                root.$https
                    .post('/Company/SaveCompany', root.company, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                         
                        this.loading = false
                        this.info = response.data.bpi
                        this.$swal.fire({
                            icon: 'success',
                            title: 'Saved Successfully',
                            showConfirmButton: false,
                            timer: 1000,
                            timerProgressBar: true,

                        });
                        this.$router.push('/company');
                    })
                    .catch(error => {
                        this.$swal.fire(
                            {
                                icon: 'error',
                                title: ' User Email is already exist',
                                text: error,
                                showConfirmButton: false,
                                timer: 1000,
                                timerProgressBar: true,
                            });

                        this.loading = false
                    })
                    .finally(() => this.loading = false)
            }
        },
        mounted: function () {
            if (this.$session.exists()) {
                this.DisplayUserName = localStorage.getItem('UserName');
                this.role = localStorage.getItem('RoleName');
            }
            if (this.role == 'Noble Admin') {
                this.RandomNumber();
            }

            if (this.$route.query.data != undefined) {
                this.company = this.$route.query.data;

                this.country = this.company.countryEnglish;
            }
        }
    })
</script>
<style scoped>
    .card label {
        font-weight: 750;
    }

    .has-Primary {
        color: blue;
        background-color: blue
    }
</style>