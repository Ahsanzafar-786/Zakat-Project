<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('AddPayment.AddPayment') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Home') }}</a>
                                    </li>
                                    <li class="breadcrumb-item active">{{ $t('AddPayment.AddPayment') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="GotoPage('/dashboard')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
                <hr />
                <div class="col-md-7">
                    <div class="form-group has-label col-sm-12 ">
                        <div class="row">
                            <div class="col-sm-5 text-md-end align-middle">
                                <label class="text  font-weight-bolder">
                                    {{ $t('AddPayment.Code') }}:<span class="text-danger"> *</span>
                                </label>
                            </div>
                            <div class="col-sm-7">
                                <input type="text" disabled class="form-control" v-model="addPayment.code" readonly
                                    :key="rendar" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group has-label col-sm-12 ">
                        <div class="row">
                            <div class="col-sm-5 text-md-end align-middle">
                                <label class="text  font-weight-bolder">
                                    {{ $t('AddPayment.Benificary') }}:<span class="text-danger"> *</span>
                                </label>
                            </div>
                            <div class="col-sm-7">
                                <benificary v-model="addPayment.benificayId" :values="addPayment.benificayId"
                                    v-on:input="EditBenificary(addPayment.benificayId, true)" />
                                <a v-if="addPayment.benificayId == '' || addPayment.benificayId == null"
                                    href="javascript:void()" class="text-secondary">{{
                                        $t('AddPayment.BenificaryDetails') }}</a>
                                <a v-else href="javascript:void()" class="text-primary" data-bs-toggle="offcanvas"
                                    ref="offcanvasRight" data-bs-target="#offcanvasRight" aria-controls="offcanvasRight">{{
                                        $t('AddPayment.BenificaryDetails') }}</a>
                            </div>
                        </div>
                    </div>

                    <div class="form-group has-label col-sm-12 ">
                        <div class="row">
                            <div class="col-sm-5 text-md-end align-middle">
                                <label class="text  font-weight-bolder">
                                    {{ $t('AddPayment.Amount') }}:<span class="text-danger"> *</span>
                                </label>
                            </div>
                            <div class="col-sm-7">
                                <!-- <input type="number" class="form-control" v-model="addPayment.amount" /> -->
                                <decimaltofix v-model="amountValue" :key="rander" :disable="true"> </decimaltofix>
                            </div>
                        </div>
                    </div>
                    <div class="form-group has-label col-sm-12 ">
                        <div class="row">
                            <div class="col-sm-5 text-md-end align-middle">
                                <label class="text  font-weight-bolder">
                                    {{ $t('AddPayment.Cashier') }}:
                                </label>
                            </div>
                            <div class="col-sm-7">
                                <input type="text" class="form-control" v-model="cashierName" readonly />
                            </div>
                        </div>
                    </div>
                    <div class="form-group has-label col-sm-12">
                        <div class="row">
                            <div class="col-sm-5 text-md-end align-middle">
                                <label class="text  font-weight-bolder">
                                    {{ $t('AddPayment.Month') }}:
                                </label>
                            </div>
                            <div class="col-sm-7">
                                <datepicker :type="'month'" v-model="addPayment.month" v-bind:key="randerDate"
                                    v-on:input="MonthSelection" />
                                <div class="row mt-2" v-if="brand.paymentType != 0">
                                    
                                    <div class="badge bg-success col-sm-4 me-3 mt-2" v-for="(val) in selectedMonth"
                                        v-bind:key="val + 1" style="position: relative;font-size: 13px !important;">
                                        <span>{{ val.selectedMonth }}</span>
                                      
                                    </div>

                                </div>

                            </div>
                        </div>
                    </div>

                </div>
                <div class="col-md-5" v-if="addPayment.benificayId != '' && addPayment.benificayId != null">
                    <div class="row">
                        <div class=" col-sm-6 ">
                            <label class="rounded text-white bg-primary px-2">{{ $t('AddBenificary.AdvancePayment') }}: {{
                                advancePayment
                            }}</label>
                        </div>
                        <div class=" col-sm-6 ">
                            <label class="rounded text-white bg-primary px-2">{{ $t('AddBenificary.PaymentType') }}: {{
                                paymentType }}</label>
                        </div>
                        <div class=" col-sm-6 mt-1">
                            <label class="rounded text-white bg-primary px-2">{{ $t('AddBenificary.AuthorizedPerson') }}:
                                <span v-for="item in brand.benificaryAuthorization" :key="item.id">
                                    <span>{{ item.authorizationPersonName }}</span>
                                    &nbsp;
                                </span>
                            </label>
                        </div>
                        <div class=" col-sm-6 mt-1">
                            <label class="rounded text-white bg-primary px-2">{{ $t('AddBenificary.AuthorizePersonId') }}:
                                <span v-for="item in brand.benificaryAuthorization" :key="item.id">
                                    <span>{{ item.authorizationPersonCode }}</span>
                                    &nbsp;,
                                </span>
                            </label>
                        </div>
                        <!-- <div class=" col-sm-6 mt-1">
                            <label class="rounded text-white bg-primary px-2">{{ $t('AddBenificary.ApprovalPersons') }}: {{
                                brand.approvalPersonName }}</label>
                        </div> -->
                        <div class=" col-sm-6 mt-1">
                            <label class="rounded text-white bg-primary px-2">{{ $t('AddBenificary.StartMonth') }}: &nbsp;
                                &nbsp;{{ GetMonth(brand.startMonth) }}</label>
                        </div>
                    </div>
                    <div class="row mt-2" v-if="onlyOneTime">
                        <p class="text-danger" v-if="onlyOneTimeDes == 'OneTime'"><b>{{
                            $t('AddPayment.ThisBeneficaryhasonetimePayment') }} </b></p>
                        <p class="text-danger" v-if="onlyOneTimeDes == 'Customize'"><b>{{
                            $t('AddPayment.NotSelectAnotherPaymentYourCustomizePeriodEnd') }} </b></p>

                    </div>
                    <p class="text-danger"
                    
                        v-if="brand.currentPaymentMonth != undefined && brand.currentPaymentMonth != null">
                        <b> {{ $t('AddPayment.LastMonthPayment') }} {{ GetDate(brand.currentPaymentMonth) }}</b>
                    </p>


                </div>

                <div class="col-lg-12 invoice-btn-fixed-bottom">
                    <div class="button-items">
                        <button v-on:click="SavePayment()" class="btn btn-outline-primary  mr-2"
                            v-bind:disabled="$v.addPayment.$invalid" v-if="rollName != 'User' && !onlyOneTime">
                            <i class="far fa-save"></i>
                            <span>
                                {{ $t('Save') }}
                            </span>
                        </button>
                        <button type="button" class="btn btn-outline-primary  mr-2" v-bind:disabled="$v.addPayment.$invalid"
                            v-if="rollName != 'User' && !onlyOneTime"
                            v-on:click="SavePayment(true) && PrintRdlc(addPayment.id)">
                            {{ $t('SaveasPrint') }}
                        </button>

                        <!-- <button type="button" class="btn btn-outline-primary mr-2"   v-on:click="PrintRdlc(addPayment.id) && GotoPage('/payment')"
                    v-bind:disabled="$v.addPayment.$invalid" >
                    {{ $t('SaveasPrint') }} </button> -->
                        <button class="btn btn-danger mr-2" v-on:click="GotoPage('/payment')">
                            {{ $t('Close') }}
                        </button>
                    </div>
                </div>
                <print :show="show" v-if="show1" :reportsrc="reportsrc1" :changereport="changereportt" @close="IsSaveRpt"
                    @IsSave="IsSaveRpt" />

                <div class="offcanvas offcanvas-end p-0" tabindex="-1" id="offcanvasRight"
                    aria-labelledby="offcanvasRightLabel" style="width: 500px !important;">
                    <div class="offcanvas-header">
                        <h5 id="offcanvasRightLabel" class="m-0">{{ $t('AddPayment.BenificaryDetails') }}</h5>
                        <button type="button" class="btn-close text-reset filter-green " data-bs-dismiss="offcanvas"
                            aria-label="Close"></button>
                    </div>
                    <div class="offcanvas-body">
                        <div class="row">
                            <div class="col-lg-12 form-group">
                                <label> {{ $t('AddBenificary.Name') }}:</label>
                                <input type="text" class="form-control" v-model="brand.name" readonly />
                            </div>
                            <div class="col-lg-12 form-group">
                                <label>{{ $t('AddBenificary.NameArabic') }} :</label>
                                <input type="text" class="form-control" v-model="brand.nameAr" readonly />
                            </div>
                            <div class="col-lg-12 form-group">
                                <label>{{ $t('AddBenificary.Ids') }} :</label>
                                <input type="text" class="form-control" v-model="brand.ugamaNo" readonly />
                            </div>
                            <div class="col-lg-12 form-group">
                                <label>{{ $t('AddBenificary.PassportNo') }} :</label>
                                <input type="text" class="form-control" v-model="brand.passportNo" readonly />
                            </div>
                            <div class="col-lg-12 form-group">
                                <label>{{ $t('AddBenificary.Nationality') }} :</label>
                                <input type="text" class="form-control" v-model="brand.nationality" readonly />
                            </div>

                            <div class="col-lg-12 form-group">
                                <label>{{ $t('AddBenificary.Gender') }} :</label>
                                <input type="text" class="form-control" v-model="brand.gender" readonly />
                            </div>
                            <div class="col-lg-12 form-group">
                                <label>{{ $t('AddBenificary.ContactNo') }} :</label>
                                <input type="text" class="form-control" v-model="brand.phoneNo" readonly />
                            </div>

                            <div class="col-lg-12 form-group">
                                <label>{{ $t('AddBenificary.Address') }} :</label>
                                <textarea rows="3" class="form-control" v-model="brand.address" readonly>  </textarea>
                            </div>
                            <div v-for="item in brand.benificaryAuthorization" :key="item.id">
                                <div class="col-lg-12 form-group">
                                    <label>{{ $t('AddBenificary.AuthorizedPersonId') }} :</label>
                                    <input class="form-control" v-model="item.authorizationPersonCode" readonly />
                                </div>
                                <div class="col-lg-12 form-group">
                                    <label>{{ $t('AddBenificary.AuthorizedPerson') }} :</label>
                                    <input class="form-control" v-model="item.authorizationPersonName" readonly />
                                </div>
                            </div>
                            <!-- <div class="col-lg-12 form-group">
                                <label>{{ $t('AddBenificary.ApprovedBy') }} :</label>
                                <input class="form-control" v-model="brand.approvalPersonName" readonly />
                            </div> -->
                            <div class="col-lg-12 form-group">
                                <label>{{ $t('AddBenificary.PaymentType') }} :</label>
                                <input class="form-control" v-model="brand.paymentTypeName" readonly />
                            </div>
                            <div class="col-lg-12 form-group">
                                <label>{{ $t('AddBenificary.AdvancePayment') }} :</label>
                                <input class="form-control" v-model="brand.advancePayment" readonly />
                            </div>
                            <div class="col-lg-12 form-group">
                                <label>{{ $t('AddBenificary.StartFrom') }} :</label>
                                <input class="form-control" v-model="brand.startMonthAndYear" readonly />
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <loading :active.sync="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>
    </div>
</template>

<script>
import moment from 'moment';
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/vue-loading.css';
import {
    required
} from "vuelidate/lib/validators"

export default {
    components: {
        Loading,
    },

    data: function () {
        return {
            loading: false,
            isTransaction: false,
            show1: false,
            onlyOneTime: false,
            onlyOneTimeDes: '',
            rollName: '',
            rendar: 0,
            amountValue: 0,
            year: '',
            randerDate: 0,
            selectedMonth: [],
            user: '',
            rander: 0,
            forRequest: 0,
            arabic: '',
            english: '',
            brand: {},
            paymentType: '',
            advancePayment: '',
            transactions: [],
            cashierName: '',
            openingBalance:0,
            addPayment: {
                Id: '',
                benificayId: '',
                paymentCode: '',
                amount: '',
                amountPerMonth: '',
                userId: '',
                month: '',
                note: '',
                year: '',
                code: 0,
                period: '',
                selectedMonth: '',
                openingBalance:0,
            }
        }
    },
    validations: {
        addPayment: {
            benificayId: {
                required,
            },

        }
    },
    watch: {
        search: function (val) {
            this.GetPayment(val, 1);
        },
    },
    methods: {
        GetDate: function (val) {
            if (val == null || val == undefined) {
                return '';
            } else {
                return moment(val).format('MMMM YYYY');
            }
        },
        IsSaveRpt: function () {
            this.show1 = !this.show1;
            this.$router.push({
                path: '/payment',

            })
        },
        GetMonth: function (link) {
            if (link != undefined) {
                return moment(link).format('MMMM');

            } else {
                return '';
            }
        },
        MonthSelection: function () {
            debugger;

            var root = this;

            debugger;
            if (this.addPayment.month == null && this.addPayment.month == undefined) {
                return;
            }

            if (this.addPayment.month != null && this.addPayment.month != undefined) {
                const record1 = this.selectedMonth.some(x => x.selectedMonth == (moment(this.addPayment.month).format('DD MMMM YYYY')));
                if (record1) {
                    root.$swal({
                        title: 'Error',
                        text: root.$i18n.locale == 'en' ? 'You cannot Add Duplicate Month' : 'لا يمكنك إضافة شهر مكرر',
                        type: 'error',
                        icon: 'error',
                        showConfirmButton: false,
                        timer: 3000,
                        timerProgressBar: true,
                    });
                    return;

                }
                var month12 = moment(this.addPayment.month, 'MMMM').format('M');
                var year = moment(this.addPayment.month).format('YYYY');
                {
                    if (this.brand.durationType == 'Customize') {

                        if (this.brand.endMonth != null && this.brand.endMonth != undefined && this.brand.firstMonth != null && this.brand.firstMonth != undefined) {



                            if (this.brand.endMonth < parseInt(month12)) {
                                root.$swal({
                                    title: 'Error',
                                    text: root.$i18n.locale == 'en' ? 'Not Select Another Payment ,Your Customize Period End' : 'لا تحدد دفعة أخرى ، قم بتخصيص نهاية الفترة',
                                    type: 'error',
                                    icon: 'error',
                                    showConfirmButton: false,
                                    timer: 3000,
                                    timerProgressBar: true,
                                });
                                return;

                            }

                            if (year >= this.brand.year && year <= this.brand.endYear) {
                                {
                                    if (this.brand.firstMonth > parseInt(month12)) {
                                        root.$swal({
                                            title: 'Error',
                                            text: root.$i18n.locale == 'en' ? 'Not Select Another Month' : 'لا تحدد دفعة أخرى',
                                            type: 'error',
                                            icon: 'error',
                                            showConfirmButton: false,
                                            timer: 3000,
                                            timerProgressBar: true,
                                        });
                                        return;

                                    }
                                }

                            }
                            else {
                                root.$swal({
                                    title: 'Error',
                                    text: root.$i18n.locale == 'en' ? 'Not Select Another Month' : 'لا تحدد دفعة أخرى',
                                    type: 'error',
                                    icon: 'error',
                                    showConfirmButton: false,
                                    timer: 3000,
                                    timerProgressBar: true,
                                });
                                return;

                            }









                        }
                    }
                    else {

                        if (this.isTransaction) {
                            var currentMonth = moment(this.brand.currentPaymentMonth).add(1, 'months').format('YYYY');

                            if (year >= currentMonth) {
                                if (year == currentMonth) {

                                    var compareMonth = moment(this.brand.currentPaymentMonth, 'YYYY-MM-DD'); // Parse the startMonth date
                                    var month123 = moment(this.addPayment.month, 'YYYY-MM-DD');
                                    if (month123.isAfter(compareMonth)) {
                                        root.$swal({
                                            title: 'Error',
                                            text: root.$i18n.locale == 'en' ? 'Not Select Another Month' : 'لا تحدد دفعة أخرى',
                                            type: 'error',
                                            icon: 'error',
                                            showConfirmButton: false,
                                            timer: 3000,
                                            timerProgressBar: true,
                                        });
                                        return;

                                    }
                                }




                            }
                            else {
                                root.$swal({
                                    title: 'Error',
                                    text: root.$i18n.locale == 'en' ? 'Not Select Another Month' : 'لا تحدد دفعة أخرى',
                                    type: 'error',
                                    icon: 'error',
                                    showConfirmButton: false,
                                    timer: 3000,
                                    timerProgressBar: true,
                                });
                                return;
                            }



                        }
                        else {
                            if (year <= this.brand.year) {
                                if (year == this.brand.year) {
                                    if (this.brand.firstMonth > parseInt(month12)) {
                                        root.$swal({
                                            title: 'Error',
                                            text: root.$i18n.locale == 'en' ? 'Not Select Another Month' : 'لا تحدد دفعة أخرى',
                                            type: 'error',
                                            icon: 'error',
                                            showConfirmButton: false,
                                            timer: 3000,
                                            timerProgressBar: true,
                                        });
                                        return;

                                    }
                                }




                            }

                        }





                    }


                    const record1 = this.selectedMonth.some(x => x.selectedMonth != (moment(this.addPayment.month).format('DD MMMM YYYY')));
                    if (record1) {
                        this.selectedMonth.push({
                            selectedMonth: this.addPayment.month
                        });
                        if (root.selectedMonth.length != 0) {
                            this.amountValue = root.addPayment.amount * root.selectedMonth.length;

                        } else {
                            this.addPayment.amount = root.addPayment.amountPerMonth;

                        }
                        return;

                    }
                    root.randerDate++;



                }

            }

            // const record = this.months.find(x => x.name == (moment(this.addPayment.month).format('MMMM')));
            this.selectedMonth.push({
                selectedMonth: this.addPayment.month
            });
            if (root.selectedMonth.length != 0) {
                this.amountValue = root.addPayment.amount * root.selectedMonth.length;

            } else {
                this.addPayment.amount = root.addPayment.amountPerMonth;

            }

        },
        RemoveEffect: function (value) {
            const index = this.selectedMonth.indexOf(value);
            if (index !== -1) {
                this.selectedMonth.splice(index, 1);
            }
            if (this.selectedMonth.length != 0) {
                this.amountValue = this.addPayment.amount * this.selectedMonth.length;

            } else {
                this.addPayment.amount = this.addPayment.amountPerMonth;

            }

        },
        RemoveAll: function () {
            this.addPayment.amount = this.addPayment.amountPerMonth;
            this.selectedMonth = [];

        },
        GotoPage: function (link) {
            this.$router.push({
                path: link
            });
        },
        EditBenificary: function (Id, val) {

            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }

            root.$https.get('/Benificary/GetBenificaryDetail?Id=' + Id + '&isPayment=' + val, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {
                    if (response.data) {
                        debugger;

                        root.brand = response.data;

                        if (root.brand.paymentType == 0) {
                            root.paymentType = 'One Time';
                            root.onlyOneTime = response.data.charityTransactions.length > 0 ? true : false;
                            if (root.onlyOneTime) {
                                root.onlyOneTimeDes = 'OneTime';
                            }
                            root.addPayment.month = moment().format('DD MMMM YYYY');
                            root.randerDate++;

                        } else if (root.brand.paymentType == 1) {
                            root.paymentType = 'Monthly';
                        } else {
                            root.paymentType = `${root.brand.paymentType} Months`;
                        }

                        if (root.brand.advancePayment == 0) {
                            root.advancePayment = 'None';
                        } else if (root.brand.advancePayment == 1) {
                            root.advancePayment = 'One Month';
                        } else {
                            root.advancePayment = `${root.brand.advancePayment} Months`;
                        }

                        root.addPayment.amount = response.data.amountPerMonth;
                        root.addPayment.amountPerMonth = response.data.amountPerMonth;
                        var paymentMonths = response.data.charityTransactions;
                        root.isTransaction = response.data.charityTransactions.length > 0 ? true : false;
                       debugger;
                        if (response.data.durationType == 'Indefinite') {

                            if (paymentMonths.length > 0) {

                                if (response.data.currentPaymentMonth != null && response.data.currentPaymentMonth != undefined) {
                                    if (root.brand.paymentType == 1) {
                                        root.selectedMonth = [];
                                        root.selectedMonth.push({
                                            selectedMonth: moment(response.data.currentPaymentMonth).add(1, 'months').format('DD MMMM YYYY')
                                        });
                                        root.addPayment.month = moment(response.data.currentPaymentMonth).add(1, 'months').format('DD MMMM YYYY');
                                        root.randerDate++;

                                    } else {
                                        root.selectedMonth = [];

                                        // var month12 = moment(response.data.currentPaymentMonth).add(1, 'months').format('DD MMMM YYYY')
                                        // root.selectedMonth.push({
                                        //     selectedMonth: month12
                                        // });
                                        // root.addPayment.month = moment(response.data.currentPaymentMonth).add(1, 'months').format('DD MMMM YYYY');

                                        // root.randerDate++

                                        //for Multiple
                                        var month12 = moment(response.data.currentPaymentMonth).add(1, 'months').format('DD MMMM YYYY')
                                        for (var j = 1; j <= root.brand.paymentType; j++) {

                                            root.selectedMonth.push({
                                                selectedMonth: month12
                                            });
                                            month12 = moment(month12).add(1, 'months').format('DD MMMM YYYY')

                                        }

                                        root.randerDate++;

                                    }

                                }

                            } else {
                                if (response.data.startMonth != null && response.data.startMonth != undefined) {
                                    root.selectedMonth = [];
                                    if (root.brand.paymentType == 1) {
                                        root.selectedMonth.push({
                                            selectedMonth: moment(response.data.startMonth).format('DD MMMM YYYY')
                                        });
                                        root.addPayment.month = response.data.startMonth;
                                        root.randerDate++;

                                    } else {
                                        root.selectedMonth = [];

                                        // var month14 = moment(response.data.startMonth).format('DD MMMM YYYY')
                                        // root.selectedMonth.push({
                                        //     selectedMonth: month14
                                        // });
                                        // root.addPayment.month = response.data.startMonth;

                                        // root.randerDate++

                                        var month14 = moment(response.data.startMonth).format('DD MMMM YYYY')
                                        for (var o = 1; o <= root.brand.paymentType; o++) {

                                            root.selectedMonth.push({
                                                selectedMonth: month14
                                            });
                                            month14 = moment(month14).add(1, 'months').format('DD MMMM YYYY')

                                        }

                                        root.randerDate++;

                                    }

                                } else {
                                    root.selectedMonth = [];
                                    if (response.data.startMonth == null && response.data.startMonth == undefined) {

                                        root.$swal({
                                            title: 'Error',
                                            text: root.$i18n.locale == 'en' ? 'Not Select Start Month Date' : ' لم تحدد تاريخ شهر البدء',
                                            type: 'error',
                                            icon: 'error',
                                            showConfirmButton: false,
                                            timer: 3000,
                                            timerProgressBar: true,
                                        });
                                        root.onlyOneTime = true;

                                        return;

                                    }
                                    var month = moment(response.data.startMonth).format('DD MMMM YYYY')
                                    for (var i = 1; i <= root.brand.paymentType; i++) {

                                        root.selectedMonth.push({
                                            selectedMonth: month
                                        });
                                        month = moment(month).add(1, 'months').format('DD MMMM YYYY')

                                    }

                                    root.randerDate++;

                                }

                            }

                        } else {
                            // For Customize

                            if (paymentMonths.length > 0) {
                                if (response.data.endMonth != null && response.data.endMonth != undefined && response.data.startDate != null && response.data.startDate != undefined) {
debugger;
                                    if(root.addPayment.id == null || root.addPayment.id == ''  ){
                                    if (response.data.isCustomize) {
                                        root.$swal({
                                            title: 'Error',
                                            text: root.$i18n.locale == 'en' ? 'Not Select Another Payment ,Your Customize Period End' : 'لا تحدد دفعة أخرى ، قم بتخصيص نهاية الفترة',
                                            type: 'error',
                                            icon: 'error',
                                            showConfirmButton: false,
                                            timer: 3000,
                                            timerProgressBar: true,
                                        });
                                        root.onlyOneTime = true;
                                        if (root.onlyOneTime) {
                                            root.onlyOneTimeDes = 'Customize';
                                        }

                                        return;
                                    }
                                    }
                                    if (response.data.currentPaymentMonth != null && response.data.currentPaymentMonth != undefined) {
                                        root.selectedMonth = [];
                                        var month123 = moment(response.data.currentPaymentMonth).add(1, 'months').format('DD MMMM YYYY')

                                        for (var ui = 1; ui <= root.brand.paymentType; ui++) {

                                            const monthComp = moment(month123, 'MMMM').format('M');

                                            if (root.brand.endMonth >= parseInt(monthComp)) {
                                                root.selectedMonth.push({
                                                    selectedMonth: month123
                                                });
                                                month123 = moment(month123).add(1, 'months').format('DD MMMM YYYY')

                                            }

                                        }

                                    }
                                } else {
                                    if (root.brand.paymentType == 0) {
                                        root.$swal({
                                            title: 'Error',
                                            text: root.$i18n.locale == 'en' ? 'This Beneficary has one time Payment' : 'هذا المستفيد لديه دفعة واحدة',
                                            type: 'error',
                                            icon: 'error',
                                            showConfirmButton: false,
                                            timer: 3000,
                                            timerProgressBar: true,
                                        });

                                    } else {
                                        root.$swal({
                                            title: 'Error',
                                            text: root.$i18n.locale == 'en' ? 'Not Select The Month Start Date and End Date' : 'لم تحدد الشهر تاريخ البدء وتاريخ الانتهاء',
                                            type: 'error',
                                            icon: 'error',
                                            showConfirmButton: false,
                                            timer: 3000,
                                            timerProgressBar: true,
                                        });

                                    }

                                }

                            } else {
                                if (response.data.startMonth != null && response.data.startMonth != undefined) {
                                    root.selectedMonth = [];
                                    if (response.data.startMonth == null && response.data.startMonth == undefined) {

                                        root.$swal({
                                            title: 'Error',
                                            text: root.$i18n.locale == 'en' ? 'Not Select Start Month Date' : 'لم تحدد تاريخ شهر البدء',
                                            type: 'error',
                                            icon: 'error',
                                            showConfirmButton: false,
                                            timer: 3000,
                                            timerProgressBar: true,
                                        });
                                        root.onlyOneTime = true;

                                        return;

                                    }
                                    var month3 = moment(response.data.startMonth).format('DD MMMM YYYY');

                                    for (var z = 1; z <= root.brand.paymentType; z++) {

                                        root.selectedMonth.push({
                                            selectedMonth: month3
                                        });
                                        month3 = moment(month3).add(1, 'months').format('DD MMMM YYYY')

                                    }

                                    root.randerDate++;

                                } else {
                                    if (root.brand.paymentType != 0) {
                                        if (response.data.startMonth == null && response.data.startMonth == undefined) {

                                            root.$swal({
                                                title: 'Error',
                                                text: root.$i18n.locale == 'en' ? 'Not Select Start Month Date' : 'لم تحدد تاريخ شهر البدء',
                                                type: 'error',
                                                icon: 'error',
                                                showConfirmButton: false,
                                                timer: 3000,
                                                timerProgressBar: true,
                                            });
                                            root.onlyOneTime = true;

                                            return;

                                        }

                                    }

                                }

                            }

                        }

                        if (root.selectedMonth.length != 0) {

                            root.amountValue = root.addPayment.amount * root.selectedMonth.length;

                        } else {
                            root.amountValue = root.addPayment.amountPerMonth;

                        }

                        root.rander++;
                    } else {
                        console.log("error: something wrong from db.");
                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });

        },

        close: function () {
            this.$router.push({
                path: '/payment',

            })
        },
        PrintRdlc: function (val) {
            var companyId = '';
            if (this.$session.exists()) {
                companyId = localStorage.getItem('CompanyID');
            }
            debugger;

            this.reportsrc1 = this.$ReportServer + '/Invoice/A4_DefaultTempletForm.aspx?id=' + val + '&pageNumber=' + this.currentPage + '&searchTerm=' + this.search + '&CompanyID=' + companyId + '&formName=Payment'

            this.changereportt++;
            this.show1 = !this.show1;

        },
        SavePayment: function (isPrint) {


            debugger;

            var root = this;
            this.addPayment.openingBalance = this.brand.openingBalance;
       
       

            if (this.addPayment.amount > this.addPayment.openingBalance) {
                this.$swal({
                    title: 'Error',
                    text: root.$i18n.locale == 'en' ? 'Insufficient funds' : 'رصيد غير كاف',
                    type: 'error',
                    icon: 'error',
                    showConfirmButton: false,
                    timer: 3000,
                    timerProgressBar: true,
                })
                .finally(() => root.loading = false);
                return ;
            }
            this.loading = true;
            var token = '';
            var userId = localStorage.getItem('UserId');
            root.addPayment.userId = userId;
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }


             {

                this.addPayment.selectedMonth = this.selectedMonth;
            this.$https.post('/Benificary/SavePayments', this.addPayment, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {

                    if (response.data.isSuccess == true) {
                        if (root.type != "Edit") {
                            root.$swal({
                                title: 'Save',
                                text: response.data.isAddUpdate,
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 3000,
                                timerProgressBar: true,
                            });
                            if (isPrint == true) {
                                root.PrintRdlc(response.data.paymentId);

                            } else {
                                root.$router.push({
                                    path: '/payment',

                                })
                            }

                            //   root.GotoPage('/payment');
                        } else {

                            root.$swal({
                                title: 'Update',
                                text: response.data.isAddUpdate,
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 3000,
                                timerProgressBar: true,
                            });
                            //  root.GotoPage('/payment');

                        }
                    } else {
                        root.$swal({
                            title: 'Error',
                            text: response.data.isAddUpdate,
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 3000,
                            timerProgressBar: true,
                        });
                    }
                })
                .catch(error => {

                    console.log(error)
                    root.$swal.fire({
                        icon: 'error',
                        title: 'Something Went Wrong',
                        text: error.response.data,
                        showConfirmButton: false,
                        timer: 5000,
                        timerProgressBar: true,
                    });

                    root.loading = false
                })
                .finally(() => root.loading = false);

            } 
          


            
        },
        GetAutoCode: function () {
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Benificary/AutoCodeGenerate', {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {
                    if (response.data) {
                        root.addPayment.code = parseInt(response.data);
                        root.rendar++;
                    } else {
                        console.log("error: something wrong from db.");
                    }
                },
                    function (error) {
                        this.loading = false;
                        console.log(error);
                    });
        },

    },

    created: function () {
        debugger;
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        this.addPayment.userId = localStorage.getItem('UserId');
        this.cashierName = localStorage.getItem('UserName');
        if (this.$route.query.data != undefined) {
            this.addPayment = this.$route.query.data;
            this.EditBenificary(this.addPayment.benificayId, true);
            this.rander++;

        } else {
            this.GetAutoCode();
        }
    },
    mounted: function () {
        this.rollName = localStorage.getItem('RoleName');
    }
}
</script>
