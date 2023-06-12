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
                                <input type="text" disabled class="form-control" v-model="addPayment.paymentCode" readonly
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
                                <benificary v-model="addPayment.benificayId" :values="addPayment.benificayId" :key="rander"
                                    v-on:input="EditBenificary(addPayment.benificayId, true)" />
                                <a v-if="addPayment.benificayId == '' || addPayment.benificayId == null"
                                    href="javascript:void()" class="text-secondary">{{ $t('AddPayment.BenificaryDetails')
                                    }}</a>
                                <a v-else href="javascript:void()" class="text-primary" data-bs-toggle="offcanvas"
                                    ref="offcanvasRight" data-bs-target="#offcanvasRight"
                                    aria-controls="offcanvasRight">Benificary Details</a>
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
                                <input type="number" class="form-control" v-model="addPayment.amount" readonly />
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
                    <div class="form-group has-label col-sm-12" v-if="brand.paymentType != 0">
                        <div class="row">
                            <div class="col-sm-5 text-md-end align-middle">
                                <label class="text  font-weight-bolder">
                                    {{ $t('AddPayment.Month') }}:
                                </label>
                            </div>
                            <div class="col-sm-7">
                                <datepicker :type="'month'" v-model="addPayment.month" v-bind:key="randerDate"
                                    v-on:input="MonthSelection" />
                                <div class="row mt-2">
                                    <div style="text-align: right !important;" v-if="selectedMonth.length > 0">
                                        <button class="btn  btn-danger btn-round btn-sm btn-icon" @click="RemoveAll()"
                                            style="font-size: .4rem;  padding: 0.2rem 0.35rem;">
                                            Close All
                                        </button>
                                    </div>
                                    <div class="badge bg-success col-sm-3 me-3 mt-2" v-for="(val) in selectedMonth"
                                        v-bind:key="val + 1" style="position: relative;font-size: 13px !important;">
                                        <span>{{ val.selectedMonth }}</span>
                                        <span style="position:absolute; right: -12px; top: -8px;">
                                            <button class="btn  btn-danger btn-round btn-sm btn-icon"
                                                style="font-size: .4rem;  padding: 0.2rem 0.35rem;"
                                                @click="RemoveEffect(val)">
                                                <i class="fas fa-times"></i>
                                            </button>
                                        </span>
                                    </div>

                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="form-group has-label col-sm-12 mb-5">
                        <div class="row">
                            <div class="col-sm-5 text-md-end align-middle">
                                <label class="text  font-weight-bolder">
                                    {{ $t('AddBenificary.Note') }}:
                                </label>
                            </div>
                            <div class="col-sm-7">
                                <textarea name="" rows="3" class="form-control" v-model="addPayment.note"></textarea>
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
                        <div class=" col-sm-6 mt-1">
                            <label class="rounded text-white bg-primary px-2">{{ $t('AddBenificary.ApprovalPersons') }}: {{
                                brand.approvalPersonName }}</label>
                        </div>
                        <div class=" col-sm-6 mt-1">
                            <label class="rounded text-white bg-primary px-2">{{ $t('AddBenificary.StartMonth') }}: &nbsp;
                                &nbsp;{{ GetMonth(brand.startMonth) }}</label>
                        </div>
                    </div>
                    <div class="row mt-2" v-if="brand.paymentType != 0">
                        <div class="col-md-4">
                            <p class="text-primary"><b>{{ $t('PaidPayments') }}</b></p>
                            <div v-for="(month) in months" v-bind:key="month.id" :class="month.year == '' ? '' : 'col-sm-12'">
                                <span v-if="month.color == 'red'" class="text-primary"><b>{{ month.name }} {{ month.year==''?month.years:brand.year
                                }}</b></span>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <p class="text-success"><b>{{ $t('DuePayments') }}</b></p>
                            <div v-for="(month) in months" v-bind:key="month.id" :class="month.year == '' ? '' : 'col-sm-12'">
                                <span v-if="month.color == 'green'" class="text-success"><b>{{ month.name }}
                                    {{ month.year }}</b></span>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <p class="text-danger"><b>{{ $t('UnPaidPayments') }}</b></p>
                            <div v-for="(month) in months" v-bind:key="month.id" :class="month.year == '' ? '' : 'col-sm-12'">
                                <span v-if="month.color == ''" class="text-danger"><b>{{ month.name }}
                                    {{ month.year }}</b></span>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-lg-12 invoice-btn-fixed-bottom">
                    <div class="button-items">
                        <button v-on:click="SavePayment()" class="btn btn-outline-primary  mr-2" v-if="rollName != 'User'">
                            <i class="far fa-save"></i>
                            <span>
                                {{ $t('Save') }}
                            </span>
                        </button>
                        <button class="btn btn-danger mr-2" v-on:click="GotoPage('/payment')">
                            {{ $t('Close') }}
                        </button>
                    </div>
                </div>
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
                                <input type="text" class="form-control" v-model="brand.name" />
                            </div>
                            <div class="col-lg-12 form-group">
                                <label>{{ $t('AddBenificary.NameArabic') }} :</label>
                                <input type="text" class="form-control" v-model="brand.nameAr" />
                            </div>
                            <div class="col-lg-12 form-group">
                                <label>{{ $t('AddBenificary.ID') }} :</label>
                                <input type="text" class="form-control" v-model="brand.ugamaNo" />
                            </div>
                            <div class="col-lg-12 form-group">
                                <label>{{ $t('AddBenificary.PassportNo') }} :</label>
                                <input type="text" class="form-control" v-model="brand.passportNo" />
                            </div>
                            <div class="col-lg-12 form-group">
                                <label>{{ $t('AddBenificary.Nationality') }} :</label>
                                <input type="text" class="form-control" v-model="brand.nationality" />
                            </div>

                            <div class="col-lg-12 form-group">
                                <label>{{ $t('AddBenificary.Gender') }} :</label>
                                <input type="text" class="form-control" v-model="brand.gender" />
                            </div>
                            <div class="col-lg-12 form-group">
                                <label>{{ $t('AddBenificary.ContactNo') }} :</label>
                                <input type="text" class="form-control" v-model="brand.phoneNo" />
                            </div>

                            <div class="col-lg-12 form-group">
                                <label>{{ $t('AddBenificary.Address') }} :</label>
                                <textarea rows="3" class="form-control" v-model="brand.address">  </textarea>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import moment from 'moment';

export default {

    data: function () {
        return {
            rollName: '',
            rendar: 0,
            year: '',
            randerDate: 0,
            selectedMonth: [],
            months: [{
                id: 1,
                order: 0,
                year: '',
                name: 'January',
                color: '',
                active: false,
            },
            {
                id: 2,
                order: 0,
                year: '',

                name: 'February',
                color: '',
                active: false,

            },
            {
                id: 3,
                order: 0,
                year: '',

                name: 'March',
                color: '',
                active: false,

            },
            {
                id: 4,
                order: 0,
                year: '',

                name: 'April',
                color: '',
                active: false,

            },
            {
                id: 5,
                order: 0,
                year: '',

                name: 'May',
                color: '',
                active: false,

            },
            {
                id: 6,
                order: 0,
                year: '',

                name: 'June',
                color: '',
                active: false,

            },
            {
                id: 7,
                order: 0,
                year: '',

                name: 'July',
                color: '',
                active: false,

            },
            {
                id: 8,
                order: 0,
                year: '',

                name: 'August',
                color: '',
                active: false,

            },
            {
                id: 9,
                order: 0,
                name: 'September',
                color: '',
                active: false,
                year: '',

            },
            {
                id: 10,
                order: 0,
                name: 'October',
                color: '',
                active: false,
                year: '',

            },
            {
                id: 11,
                order: 0,
                name: 'November',
                color: '',
                active: false,
                year: '',

            },
            {
                id: 12,
                order: 0,
                name: 'December',
                color: '',
                active: false,
                year: '',

            }
            ],
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
                selectedMonth: ''
            }
        }
    },
    watch: {
        search: function (val) {
            this.GetPayment(val, 1);
        },
    },
    methods: {
        GetMonth: function (link) {
            if (link != undefined) {
                return moment(link).format('MMMM');

            } else {
                return '';
            }
        },
        MonthSelection: function () {

            var root = this;


            if (this.addPayment.month != null && this.addPayment.month != undefined) {
                // const record1 = this.selectedMonth.some(x => x.selectedMonth == (moment(this.addPayment.month).format('MMMM')));
                // if(record1)
                // {
                //     root.$swal({
                //                     title: 'Error',
                //                     text: 'You cannot Add Duplicate Month',
                //                     type: 'error',
                //                     icon: 'error',
                //                     showConfirmButton: false,
                //                     timer: 3000,
                //                     timerProgressBar: true,
                //                 });
                //                 return;

                // }
                if (this.brand.advancePayment == 0) {
                    if (this.brand.paymentType != null) {
                        if (this.brand.paymentType == 1) {
                            if (moment(this.addPayment.month).format('MMMM') != moment().format('MMMM')) {
                                root.$swal({
                                    title: 'Error',
                                    text: 'You can Only Rceive Payment of Current Month',
                                    type: 'error',
                                    icon: 'error',
                                    showConfirmButton: false,
                                    timer: 3000,
                                    timerProgressBar: true,
                                });
                                return;

                            }

                        }
                        else if (this.brand.paymentType == 2) {
                            if (moment(this.addPayment.month).format('MMMM') == moment().add(1, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().format('MMMM')) {
                                console.log("");

                            }
                            else {
                                root.$swal({
                                    title: 'Error',
                                    text: 'You can Only Rceive Payment of Current Month',
                                    type: 'error',
                                    icon: 'error',
                                    showConfirmButton: false,
                                    timer: 3000,
                                    timerProgressBar: true,
                                });
                                return;
                            }

                        }
                        else if (this.brand.paymentType == 3) {
                            if (moment(this.addPayment.month).format('MMMM') == moment().add(1, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(2, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().format('MMMM')) {
                                console.log("");
                            }
                            else {
                                root.$swal({
                                    title: 'Error',
                                    text: 'You can Only Rceive Payment of Current Month',
                                    type: 'error',
                                    icon: 'error',
                                    showConfirmButton: false,
                                    timer: 3000,
                                    timerProgressBar: true,
                                });
                                return;
                            }

                        }
                        else if (this.brand.paymentType == 4) {
                            if (moment(this.addPayment.month).format('MMMM') == moment().add(1, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(2, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(3, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().format('MMMM')) {
                                console.log("");
                            }
                            else {
                                root.$swal({
                                    title: 'Error',
                                    text: 'You can Only Rceive Payment of Current Month',
                                    type: 'error',
                                    icon: 'error',
                                    showConfirmButton: false,
                                    timer: 3000,
                                    timerProgressBar: true,
                                });
                                return;
                            }

                        }
                        else if (this.brand.paymentType == 5) {
                            if (moment(this.addPayment.month).format('MMMM') == moment().add(1, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(2, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(3, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(4, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().format('MMMM')) {
                                console.log("");
                            }
                            else {
                                root.$swal({
                                    title: 'Error',
                                    text: 'You can Only Rceive Payment of Current Month',
                                    type: 'error',
                                    icon: 'error',
                                    showConfirmButton: false,
                                    timer: 3000,
                                    timerProgressBar: true,
                                });
                                return;
                            }

                        }
                        else if (this.brand.paymentType == 6) {
                            if (moment(this.addPayment.month).format('MMMM') == moment().add(1, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(2, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(3, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(4, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(5, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().format('MMMM')) {
                                console.log("");
                            }
                            else {
                                root.$swal({
                                    title: 'Error',
                                    text: 'You can Only Rceive Payment of Current Month',
                                    type: 'error',
                                    icon: 'error',
                                    showConfirmButton: false,
                                    timer: 3000,
                                    timerProgressBar: true,
                                });
                                return;
                            }

                        }
                        else if (this.brand.paymentType == 7) {
                            if (moment(this.addPayment.month).format('MMMM') == moment().add(1, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(2, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(3, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(4, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(5, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(6, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().format('MMMM')) {
                                console.log("");
                            }
                            else {
                                root.$swal({
                                    title: 'Error',
                                    text: 'You can Only Rceive Payment of Current Month',
                                    type: 'error',
                                    icon: 'error',
                                    showConfirmButton: false,
                                    timer: 3000,
                                    timerProgressBar: true,
                                });
                                return;
                            }

                        }
                        else if (this.brand.paymentType == 8) {
                            if (moment(this.addPayment.month).format('MMMM') == moment().add(1, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(2, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(3, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(4, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(5, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(6, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(7, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().format('MMMM')) {
                                console.log("");
                            }
                            else {
                                root.$swal({
                                    title: 'Error',
                                    text: 'You can Only Rceive Payment of Current Month',
                                    type: 'error',
                                    icon: 'error',
                                    showConfirmButton: false,
                                    timer: 3000,
                                    timerProgressBar: true,
                                });
                                return;
                            }

                        }
                        else if (this.brand.paymentType == 9) {
                            if (moment(this.addPayment.month).format('MMMM') == moment().add(1, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(2, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(3, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(4, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(5, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(6, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(7, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(8, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().format('MMMM')) {
                                console.log("");
                            }
                            else {
                                root.$swal({
                                    title: 'Error',
                                    text: 'You can Only Rceive Payment of Current Month',
                                    type: 'error',
                                    icon: 'error',
                                    showConfirmButton: false,
                                    timer: 3000,
                                    timerProgressBar: true,
                                });
                                return;
                            }

                        }
                        else if (this.brand.paymentType == 10) {
                            if (moment(this.addPayment.month).format('MMMM') == moment().add(1, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(2, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(3, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(4, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(5, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(6, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(7, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(8, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(9, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().format('MMMM')) {
                                console.log("");
                            }
                            else {
                                root.$swal({
                                    title: 'Error',
                                    text: 'You can Only Rceive Payment of Current Month',
                                    type: 'error',
                                    icon: 'error',
                                    showConfirmButton: false,
                                    timer: 3000,
                                    timerProgressBar: true,
                                });
                                return;
                            }

                        }
                        else if (this.brand.paymentType == 11) {
                            if (moment(this.addPayment.month).format('MMMM') == moment().add(1, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(2, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(3, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(4, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(5, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(6, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(7, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(8, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(9, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(10, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().format('MMMM')) {
                                console.log("");
                            }
                            else {
                                root.$swal({
                                    title: 'Error',
                                    text: 'You can Only Rceive Payment of Current Month',
                                    type: 'error',
                                    icon: 'error',
                                    showConfirmButton: false,
                                    timer: 3000,
                                    timerProgressBar: true,
                                });
                                return;
                            }

                        }
                        else if (this.brand.paymentType == 12) {
                            if (moment(this.addPayment.month).format('MMMM') == moment().add(1, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(2, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(3, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(4, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(5, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(6, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(7, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(8, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(9, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(10, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().add(11, 'months').format('MMMM') ||
                                moment(this.addPayment.month).format('MMMM') == moment().format('MMMM')) {
                                console.log("");
                            }
                            else {
                                root.$swal({
                                    title: 'Error',
                                    text: 'You can Only Rceive Payment of Current Month',
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

               
                const record = this.months.find(x => x.name == (moment(this.addPayment.month).format('MMMM')));
                if (record != null) {
                    if (record.active == true) {
                        debugger;
                        var str = moment(this.addPayment.month).format('DD MMMM YYYY');
                        console.log(this.selectedMonth);
                        var res = this.selectedMonth.some(item => item.selectedMonth === str);
                        console.log(this.addPayment.month);

                            if(res)
                            {
                                root.$swal({
                                title: 'Error',
                                text: 'You have no Permission to Select Same Month',
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 3000,
                                timerProgressBar: true,
                            });
                            }
                            else
                            {
                                this.selectedMonth.push({
                                    selectedMonth: this.addPayment.month
                                });
                            }
                        
                       
                        
                        if(this.selectedMonth.length>this.brand.advancePayment)
                        {
                            root.$swal({
                                    title: 'Error',
                                    text: 'You can Only Take'+this.brand.advancePayment +' Month Payment in Advance',
                                    type: 'error',
                                    icon: 'error',
                                    showConfirmButton: false,
                                    timer: 3000,
                                    timerProgressBar: true,
                                });
                                return;
                        }


                        if (root.selectedMonth.length != 0) {
                            root.addPayment.amount = root.addPayment.amount * root.selectedMonth.length;

                        } else {
                            root.addPayment.amount = root.addPayment.amountPerMonth;

                        }

                    } else {
                        this.addPayment.month = null;
                        this.randerDate++;
                        root.$swal({
                            title: 'Error',
                            text: 'You have no Permission to Select another Month',
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 3000,
                            timerProgressBar: true,
                        });
                    }

                }

            }

        },
        RemoveEffect: function (value) {
            const index = this.selectedMonth.indexOf(value);
            if (index !== -1) {
                this.selectedMonth.splice(index, 1);
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
            root.months.map(auth => {
                {
                    auth.color = '';
                    auth.active = false;
                }
                return auth;
            });
            root.$https.get('/Benificary/GetBenificaryDetail?Id=' + Id + '&isPayment=' + val, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {
                    if (response.data) {
                      
                        root.brand = response.data;

                        if (root.brand.paymentType == 0) {
                            root.paymentType = 'One Time';
                        }
                        else if (root.brand.paymentType == 1) {
                            root.paymentType = 'Monthly';
                        }
                        else {
                            root.paymentType = `${root.brand.paymentType} Months`;
                        }

                        if (root.brand.advancePayment == 0) {
                            root.advancePayment = 'None';
                        }
                        else if (root.brand.advancePayment == 1) {
                            root.advancePayment = 'One Month';
                        }
                        else {
                            root.advancePayment = `${root.brand.advancePayment} Months`;
                        }

                        root.addPayment.amount = response.data.amountPerMonth;
                        root.addPayment.amountPerMonth = response.data.amountPerMonth;
                        var paymentMonths = response.data.charityTransactions;
                        if (response.data.durationType == 'Indefinite') {
                            root.months.map(auth => {

                                {
                                    auth.color = 'green';
                                    auth.active = true;
                                    auth.year = response.data.year;
                                }
                                return auth;
                            });
                            if (paymentMonths.length > 0) {

                                for (var k = 0; k < paymentMonths.length; k++) {

                                    root.months.map(auth => {

                                        if (auth.id === paymentMonths[k].paymentMonths) {
                                            auth.color = 'red';
                                            auth.active = false;
                                            auth.year = paymentMonths[k].year;

                                        }
                                        return auth;
                                    });

                                }
                            }

                        } else if (response.data.firstMonth != null && response.data.endMonth != null) {

                            {
                                for (var i = response.data.firstMonth; i <= response.data.endMonth; i++) {
                                    if (i <= response.data.endMonth) {
                                        root.months.map(auth => {

                                            if (auth.id === i) {
                                                auth.color = 'green';
                                                auth.active = true;
                                                auth.year = response.data.year;
                                            }
                                            return auth;
                                        })
                                    }

                                }
                                if (paymentMonths.length > 0) {
                                    for (var j = 0; j < paymentMonths.length; j++) {

                                        root.months.map(auth => {

                                            if (auth.id === paymentMonths[j].paymentMonths) {
                                                auth.color = 'red';
                                                auth.active = false;
                                                auth.year = paymentMonths[j].year;

                                            }
                                            return auth;
                                        });

                                    }

                                }

                            }

                        }
                        root.selectedMonth = [];
                        root.months.forEach(element => {
                            if(element.color == 'green')
                            {
                                root.selectedMonth.push({
                                    selectedMonth: '01' + ' ' + element.name + ' ' + element.year
                                });
                            }
                        });
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
        GetTransactions: function (Id) {

            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Benificary/GetCharityTransactionList?benificaryId=' + Id, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {
                    if (response.data) {
                        root.transactions = response.data;
                    } else {
                        console.log("error: something wrong from db.");
                    }
                },
                    function (error) {
                        this.loading = false;
                        console.log(error);
                    });

        },
        SavePayment: function () {

            var root = this;
            this.loading = true;
            var token = '';
            var userId = localStorage.getItem('UserId');
            root.addPayment.userId = userId;
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
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

                            root.GotoPage('/payment');
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
                            root.GotoPage('/payment');

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
                        root.addPayment.paymentCode = response.data;
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
