<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-lg-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title" v-if="loginDetails.id=='00000000-0000-0000-0000-000000000000'">{{ $t('AddSignUp.SignUpDetails') }}</h4>
                                <h4 class="page-title" v-if="loginDetails.id!='00000000-0000-0000-0000-000000000000'">{{ $t('AddSignUp.SignUpDetails') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('AddSignUp.SignUpDetails') }}</li>
                                </ol>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-body" >
                    <div class="row">

                        <div class="col-sm-6">
                            <label>{{ $t('AddSignUp.UserEmail') }} :<span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : $v.loginDetails.email.$error}">
                                <input class="form-control" v-model="$v.loginDetails.email.$model" @blur="EmailDuplicate(loginDetails.email)"  />
                                <span v-if="$v.loginDetails.email.$error" class="error text-danger">
                                    <span v-if="!$v.loginDetails.email.required"> {{ $t('AddSignUp.RegisterUser_Error_Required_EmailID') }} </span>
                                    <span v-if="!$v.loginDetails.email.email"> {{ $t('AddSignUp.RegisterUser_Error_Format_EmailID') }} </span>
                                </span>
                                <span class="text-right text-danger" v-if="emailExist">{{ $t('AddSignUp.EmailExist') }}</span>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <label>{{ $t('AddSignUp.UserId') }} :<span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : $v.loginDetails.userName.$error}">
                                <input class="form-control" v-model="$v.loginDetails.userName.$model"  />
                                <span v-if="$v.loginDetails.userName.$error" class="error text-danger">
                                    <span v-if="!$v.loginDetails.userName.required"> {{ $t('AddSignUp.RegisterUser_Error_Required_DisplayName') }}</span>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <label>{{ $t('AddSignUp.RoleId')}} :<span class="text-danger"> *</span></label>
                            <roledropdown v-model="loginDetails.roleName" :values="loginDetails.roleName" :isDisable="loginDetails.isProceed"></roledropdown>
                           
                        </div>
                        <div class="col-sm-6">
                            <label>{{ $t('AddSignUp.Remarks')}} :</label>
                            <textarea rows="1" class="form-control" v-model="loginDetails.remarks" @blur="EmailDuplicate(loginDetails.email)"></textarea>
                        </div>
                        <div class="col-sm-6" v-if="loginDetails.id=='00000000-0000-0000-0000-000000000000'">
                            <label> {{ $t('AddSignUp.Password') }} :<span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : $v.loginDetails.password.$error}">

                                <div class="input-group mb-3">
                                    <input  id="password" v-model="$v.loginDetails.password.$model"  autocomplete="off"  :type="password1" class="form-control" v-bind:placeholder="$t('AddSignUp.Password')" aria-label="Recipient's username" aria-describedby="button-addon2">
                                    <button class="btn btn-secondary" v-on:mousedown="showPassword1" @mouseleave="hidepassword1" type="button" id="button-addon2"><i v-bind:class="eyeValue1?  'fa fa-eye-slash' : 'fas fa-eye'"></i></button>
                                </div>
                                <span v-if="$v.loginDetails.password.$error" class="error text-danger">
                                    <span v-if="!$v.loginDetails.password.required">{{ $t('AddSignUp.RegisterUser_Error_Required_Password') }}</span>
                                    <span v-if="!$v.loginDetails.password.strongPassword">{{ $t('AddSignUp.RegisterUser_Error_Format_Password') }}</span>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-6" v-if="loginDetails.id=='00000000-0000-0000-0000-000000000000'">
                            <label>{{ $t('AddSignUp.ConfirmPassword') }}:<span class="text-danger"> *</span></label>

                            <div v-bind:class="{'has-danger' : $v.loginDetails.confirmPassword.$error}">

                                <div class="input-group mb-3">
                                    <input id="password" v-model="$v.loginDetails.confirmPassword.$model" autocomplete="off"  name="password" :type="password" class="form-control" v-bind:placeholder="$t('AddSignUp.Password')" aria-label="Recipient's username" aria-describedby="button-addon2">
                                    <button class="btn btn-secondary" v-on:mousedown="showPassword" @mouseleave="hidepassword" type="button" id="button-addon2"><i v-bind:class="eyeValue?  'fa fa-eye-slash' : 'fas fa-eye'"></i></button>
                                </div>
                                <span v-if="$v.loginDetails.confirmPassword.$error" class="error text-danger">
                                    <span v-if="!$v.loginDetails.confirmPassword.required">{{ $t('AddSignUp.RegisterUser_Error_Required_ConfirmPassword') }}</span>
                                    <span v-if="!$v.loginDetails.confirmPassword.sameAsPassword">{{ $t('AddSignUp.RegisterUser_Error_SameAs_ConfirmPassword') }}</span>
                                </span>
                            </div>
                        </div>
                        <div class="form-group col-sm-12">
                        <label></label>
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="loginDetails.isActive">
                            <label for="inlineCheckbox1"> {{ $t('AddBenificary.Active') }} </label>
                        </div>
                    </div>
                    </div>
                </div>
               
                <div v-if="!loading" class="card-footer">
                    <div class="row">
                        <div v-if="!loading" class=" col-md-12">
                            <div class="button-items">
                                <button class="btn btn-primary" v-bind:disabled="$v.loginDetails.$invalid " v-if="loginDetails.id=='00000000-0000-0000-0000-000000000000' && roleName != 'User' " v-on:click="SaveLoginDetails" ><i class="mdi mdi-check-all me-2"></i> {{ $t('Save') }}</button>
                                <button class="btn btn-primary" v-if="loginDetails.id!='00000000-0000-0000-0000-000000000000' && roleName != 'User' " v-on:click="UpdateLoginDetails" ><i class="mdi mdi-check-all me-2"></i> {{ $t('Update') }}</button>
                                <button class="btn btn-danger" v-on:click="Cancel">{{ $t('Close') }}</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import { required, sameAs, email } from 'vuelidate/lib/validators';
    export default ({
        mixins: [clickMixin],
        data: function () {
            return {
                roleName:'',
                invoiveItem: false,
                terminalUserTypeOptions:[],
                invoiveBarCode: false,
                terminalUserTypeDisabled: false,
                invoiveBarCodeItem: false,
                terminalType: '',
                dayStart: '',
                arabic: '',
                onlineTerminalId:'',
                english: '',
                isTouch: '',
                invoiceWoInventory: '',
                registrationDate: '',
                arabicName: '',
                emailExist: false,
                loading: false,
                gender: '',
                idNumber: '',
                userId: '',
                rander: 0,
                randered: 0,
                language: 'Nothing',
                password: "password",
                password1: "password",
                eyeValue: false,
                eyeValue1: false,
                isOpenDay: '',
                terminalRander: 0,
                terminalRanderOnline: 0,
                options: [],
                loginDetails: {
                    id: '00000000-0000-0000-0000-000000000000',
                    email: '',
                    employeeId: '',
                    userName: '',
                    password: '',
                    confirmPassword: '',
                 
                    terminalUserType:'',
                    invoiceWoInventory: false,
                    terminalId: '',
                    isTouchInvoice: false,
                    isActive: true,
                    allowAll: false,
                    permissionToStartExpenseDay: false,
                    isSupervisor: false,
                    temporaryCashReceiver: false,
                    temporaryCashIssuer: false,
                    temporaryCashRequester: false,
                    roleName: '',
                    days: 0,
                    limit: 0,
                    isStatus:true,
                    remarks:'',
                    touchScreen: ''
                }
            }
        },
        validations: {
            loginDetails: {
                userName: { required },
                password:
                {
                    required,
                    strongPassword(password) {
                        return /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$/.test(password);
                    }
                },
                confirmPassword:
                {
                    required,
                    sameAsPassword: sameAs('password')
                },
                email:
                {
                    required,
                    email
                },
                roleName:
                {
                    required,
                },
            }
        },
        methods: {
            onCahngeTerminalUserType: function(){
                
                if(this.loginDetails.terminalUserType == 'Both'){
                    this.terminalRanderOnline++
                    this.terminalRander++
                }
                else if(this.loginDetails.terminalUserType == 'Online'){
                    this.terminalRanderOnline++
                }
                else{

                    this.terminalRander++
                }
            },
            
            showPassword() {
                if (this.password === "password") {
                    this.password = "text";
                    this.eyeValue = true;
                }

            },
            hidepassword() {
                this.password = "password"
                this.eyeValue = false;
            },
            showPassword1() {

                if (this.password1 === "password") {
                    this.password1 = "text";
                    this.eyeValue1 = true;
                }

            },
            hidepassword1() {

                this.password1 = "password"
                this.eyeValue1 = false;
            },
            //userNameRejix: function (value) {
            //
            //    this.userName = value.replace(/[^A-Z0-9]/ig, "").toLowerCase();
            //    console.log(this.userName);
            //},
           

            languageChange: function (lan) {
                if (this.language == lan) {
                    if (this.loginDetails.id == '00000000-0000-0000-0000-000000000000') {

                        var getLocale = this.$i18n.locale;
                        this.language = getLocale;

                        this.$router.go('/AddSignUp');
                    }
                    else {
                        this.$swal({
                            title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text:(this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'You cannot Change language during update, otherwise your current page data will be lose!' : 'لا يمكنك تغيير اللغة أثناء التحديث ، وإلا ستفقد بيانات صفحتك الحالية!',
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            icon: 'error',
                            timer: 4000,
                            timerProgressBar: true,
                        });
                    }
                }


            },
            EmailDuplicate: function (x) {
                var root = this;
                var token = '';
                if (root.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                root.emailExist = false;
                this.$https.get('/account/DuplicateEmail?email=' + x, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data == true) {
                            root.emailExist = true;
                        }
                        else {
                            root.emailExist = false;
                        }
                    })
            },

            Cancel: function () {
                this.$router.push({
                    path: '/signUp',

                })
            },
            
           
            SaveLoginDetails: function () {
                
                this.loading = true;
                var root = this;
                var token = '';
                
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                if (this.emailExist) {
                    this.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                        text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Email Already Exist!' : 'البريد الإلكتروني موجود بالفعل!',
                        type: 'error',
                        icon: 'error',
                        showConfirmButton: false,
                        timer: 1700,
                        timerProgressBar: true,
                    });
                }
                else {
                    root.$https
                        .post('/account/SaveUser', this.loginDetails, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(response => {
                            if (response.data != "Already Exists") {
                                this.loading = false
                                this.info = response.data.bpi


                                this.$swal.fire({
                                    icon: 'success',
                                    title: 'Saved Successfully',
                                    showConfirmButton: false,
                                    timer: 1800,
                                    timerProgressBar: true,

                                });
                                this.$router.push('/signUp');
                            }
                            else {
                                this.loading = false
                                this.info = response.data.bpi

                                this.$swal.fire({
                                    icon: 'error',
                                    title: 'UserId Already Exist',
                                    showConfirmButton: false,
                                    timer: 1800,
                                    timerProgressBar: true,

                                });
                            }
                        })
                        .catch(error => {
                            console.log(error)
                            this.$swal.fire(
                                {
                                    icon: 'error',
                                    title: 'Server Error',
                                    text: error,
                                });

                            this.loading = false
                        })
                        .finally(() => this.loading = false)
                }
            },
            UpdateLoginDetails: function (userId) {
                this.userId = userId;
                this.loading = true;
                var root = this;
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                
                root.$https
                    .post('/account/SaveUser', this.loginDetails, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        if (response.data != null) {
                            root.loading = false

                            root.$swal.fire({
                                icon: 'success',
                                title: 'Saved Successfully',
                                showConfirmButton: false,
                                timer: 1800,
                                timerProgressBar: true,

                            });
                            root.$router.push('/signUp');
                        }
                        
                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: 'Server Error',
                                text: error,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false)
            },
            getUserWiseRecords: function () {

                if (this.$route.query.data != undefined) {
                     
                    this.loginDetails.id = this.$route.query.data.userId;
                    this.loginDetails.firstName = this.$route.query.data.firstName;
                    this.loginDetails.lastName = this.$route.query.data.lastName;
                    this.userId = this.$route.query.data.userId;
                    this.loginDetails.email = this.$route.query.data.email;
                    this.loginDetails.userName = this.$route.query.data.userName;
                    this.loginDetails.roleName = this.$route.query.data.roleName;
                    this.loginDetails.isProceed = this.$route.query.data.isProceed;
                    this.loginDetails.remarks = this.$route.query.data.remarks;
                    this.rander++;
                }
            }
        },
        created: function () {
            this.getUserWiseRecords();

            
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.isOpenDay = localStorage.getItem('IsOpenDay') == 'true' ? true : false;
            this.language = this.$i18n.locale;
            this.roleName = localStorage.getItem('RoleName');
        }
    })
</script>
