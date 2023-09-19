<template>
    <modal :show="show" :modalLarge="true">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type == 'Edit'">
                    {{ $t('AddBenificary.UpdateBenificary') }}
                </h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else-if="type == 'Add'">
                    {{ $t('AddBenificary.AddBenificary') }}
                </h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else-if="type == 'View'">
                    {{ $t('AddBenificary.ViewBenificary') }}
                </h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-6 form-group">
                        <label class="text  font-weight-bolder">
                            {{ $t('Payment.Code') }}:<span class="text-danger"> *</span>
                        </label>
                        <input class="form-control" v-model="brand.beneficiaryId" type="text" disabled />
                    </div>
                    <div class="col-md-6 form-group">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddBenificary.Name') }}:<span class="text-danger"> </span>
                        </label>
                        <input class="form-control" v-model="$v.brand.name.$model" type="text" />
                    </div>
                    <div class="col-md-6 form-group">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddBenificary.NameArabic') }}:<span class="text-danger"> *</span>
                        </label>
                        <input class="form-control" v-model="$v.brand.nameAr.$model" type="text" />
                    </div>

                    <div class="col-md-6 form-group">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddBenificary.Ids') }}:<span class="text-danger"> *</span>
                        </label>
                        <input class="form-control" v-model="$v.brand.ugamaNo.$model" placeholder="XXXXXXXXXX"
                            maxlength="10" type="text" />
                        <span v-if="$v.brand.ugamaNo.$error && english == 'en'" class="error text-danger">
                            <span v-if="!$v.brand.ugamaNo.minLength">ID length should be 10 characters.</span>
                            <span v-if="!$v.brand.ugamaNo.maxLength">ID length should be 10 characters.</span>
                        </span>
                        <span v-else class="error text-danger">
                            <span v-if="!$v.brand.ugamaNo.minLength">يجب أن يكون طول الهوية 10 أحرف</span>
                            <span v-if="!$v.brand.ugamaNo.maxLength">يجب أن يكون طول الهوية 10 أحرف</span>
                        </span>
                    </div>
                    <!-- <div class="col-md-6 form-group">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddBenificary.PassportNo') }}:
                        </label>
                        <input class="form-control" v-model="brand.passportNo" type="text" />
                    </div> -->

                    <div class="col-md-6 form-group">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddBenificary.Nationality') }}:
                        </label>
                        <input class="form-control" v-model="brand.nationality" type="text" />
                    </div>
                    <div class="col-md-6 form-group">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddBenificary.Gender') }}:
                        </label>
                        <select v-model="brand.gender" class="form-select" aria-label="Default select example">
                            <option value="Male">{{ $t('AddBenificary.Male') }}</option>
                            <option value="Female">{{ $t('AddBenificary.Female') }}</option>
                        </select>
                    </div>

                    <div class="col-md-6 form-group">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddBenificary.ContactNo') }}:
                        </label>
                        <input class="form-control" v-model="$v.brand.phoneNo.$model" placeholder="05xxxxxxxx" type="text"
                            maxlength="10" />
                        <span v-if="$v.brand.phoneNo.$error && english == 'en'" class="error text-danger">
                            <span v-if="!$v.brand.phoneNo.minLength">Contact Number length should be 10 characters.</span>
                            <span v-if="!$v.brand.phoneNo.maxLength">Contact Number length should be 10 characters.</span>
                            <span v-if="!$v.brand.phoneNo.numeric">Only use numbers</span>
                        </span>
                        <span v-else class="error text-danger">
                            <span v-if="!$v.brand.phoneNo.minLength">طول رقم الاتصال يجب أن يكون 10 أحرف</span>
                            <span v-if="!$v.brand.phoneNo.maxLength">طول رقم الاتصال يجب أن يكون 10 أحرف</span>
                            <span v-if="!$v.brand.phoneNo.numeric">فقط استخدم الأرقام</span>
                        </span>
                    </div>
                    <div class="col-md-6 form-group">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddBenificary.Address') }}:
                        </label>
                        <input class="form-control" v-model="brand.address" type="text" />
                    </div>
                    <div class="col-md-12">
                        <h4 style="color:black !important;">
                            {{ $t('AddBenificary.PaymentDetail') }}

                        </h4>

                    </div>

                    <div class="col-md-4 form-group">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddBenificary.PaymentType') }}<span class="text-danger"> *</span>
                        </label>
                        <paymenttype v-model="brand.paymentTypeId" v-on:input="GetRecord" ref="ChlidDropdown"
                            :values="brand.paymentTypeId" :isDisable="isDisableValue" />
                    </div>
                    <div class="col-md-4 form-group"
                        v-if="brand.paymentTypeId != '' && brand.paymentTypeId != null && paymentType != 0">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddBenificary.RecurringAmount') }}:
                        </label>
                        <decimaltofix v-model="$v.brand.recurringAmount.$model" v-on:input="GetValueOfRecurring"
                            v-bind:disabled="isDisableValue"> </decimaltofix>

                        <!-- <input class="form-control" 
                            v-model="$v.brand.recurringAmount.$model" @click="$event.target.select()" type="text" v-bind:disabled="isDisableValue" />
                            <span v-if="$v.brand.recurringAmount.$error && english == 'en'" class="error text-danger">
                            <span v-if="!$v.brand.recurringAmount.decimal ">Amount should be in number</span>
                        </span>
                        <span v-else class="error text-danger">
                            <span v-if="!$v.brand.recurringAmount.decimal ">يجب أن يكون المبلغ عبارة عن رقم</span>
                        </span> -->
                    </div>
                    <div class="col-md-4 form-group">
                        <label class="text  font-weight-bolder"
                            v-if="brand.paymentTypeId != '' && brand.paymentTypeId != null && paymentType != 0">
                            {{ $t('AddBenificary.AmountPerMonth') }}:
                        </label>
                        <label class="text  font-weight-bolder" v-else>
                            {{ $t('AddBenificary.Amount') }}:
                        </label>
                        <decimaltofix v-model="$v.brand.amountPerMonth.$model" v-bind:disabled="isDisableValue">
                        </decimaltofix>

                        <!-- <input class="form-control" v-model="$v.brand.amountPerMonth.$model" @click="$event.target.select()" v-if="paymentType != 0"
                            type="text" />
                        <input class="form-control" v-model="$v.brand.amountPerMonth.$model" @click="$event.target.select()" v-else
                            type="text" v-on:change="FloatValue()"/> -->
                        <span v-if="$v.brand.amountPerMonth.$error && english == 'en'" class="error text-danger">
                            <span v-if="!$v.brand.amountPerMonth.decimal">Amount should be in number</span>
                        </span>
                        <span v-else class="error text-danger">
                            <span v-if="!$v.brand.amountPerMonth.decimal">يجب أن يكون المبلغ عبارة عن رقم</span>
                        </span>
                    </div>
                    <div class="col-md-4 form-group" v-if="paymentType != 0">

                        <label class="text  font-weight-bolder">
                            {{ $t('AddBenificary.AdvancePayment') }}:
                        </label>
                        <multiselect v-model="advancePaymentvalue" v-on:input="GetDateMonth" v-if="$i18n.locale == 'en'"
                            v-bind:disabled="isDisableValue"
                            :options="['1 Month', '2 Months', '3 Months', '4 Months', '5 Months', '6 Months', '7 Months', '8 Months', '9 Months', '10 Months', '11 Months', '12 Months']"
                            :show-labels="false" :placeholder="$t('AddBenificary.SelectType')"
                            v-bind:class="$i18n.locale == 'en' ? 'text-left ' : 'multiselect__placeholder12'">
                        </multiselect>
                        <multiselect v-model="advancePaymentvalue" v-on:input="GetDateMonth"
                            v-bind:disabled="isDisableValue" v-else
                            :options="['شهر واحد', 'شهرين', 'ثلاثة أشهر', 'أربعة أشهر', 'خمسة أشهر', 'ستة أشهر', 'سبعة اشهر', 'ثمانية اشهر', 'تسعة أشهر', 'عشرة أشهر', 'أحد عشر شهرا', 'سنة']"
                            :show-labels="false" :placeholder="$t('AddBenificary.SelectType')"
                            v-bind:class="$i18n.locale == 'en' ? 'text-left ' : 'multiselect__placeholder12'">
                        </multiselect>
                    </div>

                    <div class="col-md-4 form-group" v-if="paymentType != 0">
                        <label>{{ $t('AddBenificary.StartFrom') }}:</label><span class="text-danger"> *</span>
                        <datepicker v-model="brand.startMonth" v-on:input="GetDateMonth" :type="'month'"
                            :isDisable="isDisableValue" />

                    </div>
                    <!-- <div class="col-md-4 form-group">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddBenificary.ApprovedBy') }}:
                        </label>
                        <approvalperson v-model="brand.approvalPersonId" :values="brand.approvalPersonId"
                            :isDisable="isDisableValue" />
                    </div> -->

                    <div class="form-group col-sm-12" >

                        <label></label>
                        <div class="checkbox form-check-inline mx-2" v-if="roleName != 'Cashier' && type != 'Add'">
                          
                            <input type="checkbox" id="inlineCheckbox1" v-model="brand.isActive">
                            <label for="inlineCheckbox1"> {{ $t('AddBenificary.Active') }} </label>
                        </div>
                    </div>
                    <div class="col-md-12 form-group"
                        v-if="brand.id != '00000000-0000-0000-0000-000000000000' && !brand.isActive && brand.advancePayment != 0">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddBenificary.Reason') }}:<span class="text-danger"> *</span>
                        </label>
                        <textarea rows="3" class="form-control" v-model="brand.reason"></textarea>
                    </div>

                </div>
                <div class="row" v-if="paymentType != 0 && roleName != 'Cashier'" >
                    <div class="col-md-12 form-group">
                        <h4 style="color:black !important;">

                            {{ $t('AddBenificary.PaymentDuration') }}

                        </h4>
                    </div>

                    <div class="col-md-6 form-group">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddBenificary.Type') }}:
                        </label>
                        <multiselect v-model="brand.durationType" v-on:input="GetDateMonth"
                            :options="['Customize', 'Indefinite']" :show-labels="false"
                            :placeholder="$t('AddBenificary.SelectType')">
                        </multiselect>
                    </div>
                    <div class="col-md-3 form-group" v-if="brand.durationType == 'Customize'">
                        <label>{{ $t('AddBenificary.StartMonth') }}:</label>
                        <datepicker v-model="brand.startDate" v-bind:isDisable="true" v-bind:key="randerforempty"
                            :type="'month'" />

                    </div>
                    <div class="col-md-3 form-group" v-if="brand.durationType == 'Customize'">
                        <label>{{ $t('AddBenificary.EndMonth') }}:</label>
                        <datepicker v-model="brand.endDate" v-bind:key="randerforempty" :type="'month'" />

                    </div>
                    <!-- <div class="col-md-6 form-group">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddBenificary.ApprovedBy') }}:
                        </label>
                        <approvalperson v-model="brand.approvalPersonId" :values="brand.approvalPersonId" />
                    </div> -->

                </div>
                <div class="row" v-if="paymentType != 0 && roleName != 'Cashier'" >
                    <div class="col-md-12 form-group">
                        <h4 style="color:black !important;">

                            {{ $t('AddBenificary.BeneficiaryAuthorization') }}

                        </h4>
                    </div>
                    <div class="col-lg-12">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th class="text-center"> #</th>
                                    <th class="text-center">{{ $t('AddBenificary.AuthorizedPerson') }}</th>
                                    <!-- <th class="text-center">{{ $t('AddBenificary.ApprovedBy')
                                    }}</th> -->
                                    <th class="text-center">{{ $t('AddBenificary.Date') }}
                                    </th>

                                    <th class="text-center">{{ $t('AddBenificary.Status') }}
                                    </th>
                                    <th class="text-center" v-if="giveReason">{{ $t('AddBenificary.Reason') }}
                                    </th>
                                    <th class="text-center">{{ $t('AddBenificary.Action') }}
                                    </th>
                                </tr>
                            </thead>

                            <tbody>
                                <tr v-for="(person, index) in brand.benificaryAuthorization" :key="index">
                                    <td class="border-top-0 text-center">
                                        {{ index + 1 }}
                                    </td>
                                    <td class="border-top-0 text-center" v-if="brand.isDisable">
                                        <authorizedperson v-model="person.authorizationPersonId"
                                            :values="person.authorizationPersonId" :isDisable="'true'" />
                                    </td>
                                    <td class="border-top-0 text-center" v-else>
                                        <authorizedperson v-model="person.authorizationPersonId"
                                            :values="person.authorizationPersonId" />
                                    </td>
                                    <!-- <td class="border-top-0 text-center" v-if="brand.isDisable && roleName != 'Admin'">
                                        <approvalperson v-model="person.approvalPersonId"
                                            :values="person.approvalPersonId" :isDisable="'true'"/>
                                    </td> -->
                                    <!-- <td class="border-top-0 text-center">
                                        <approvalperson v-model="person.approvalPersonId"
                                            :values="person.approvalPersonId" />

                                    </td> -->
                                    <td class="border-top-0 text-center" v-if="brand.isDisable">
                                        <datepicker v-model="person.date" :isDisable="true" />
                                    </td>
                                    <td class="border-top-0 text-center" v-else>
                                        <datepicker v-model="person.date" />
                                    </td>
                                    <td class="border-top-0 text-center" v-if="brand.isDisable">
                                        <div class="checkbox form-check-inline">
                                            <input v-bind:id="index + 1" type="checkbox" disabled v-model="person.isActive">
                                            <label v-bind:for="index + 1"></label>
                                        </div>
                                    </td>
                                    <td class="border-top-0 text-center" v-else>
                                        <div class="checkbox form-check-inline">
                                            <input v-bind:id="index + 1" type="checkbox" v-model="person.isActive"
                                                v-on:input="GiveReason()">
                                            <label v-bind:for="index + 1"></label>
                                        </div>
                                    </td>
                                    <td class="border-top-0 text-center" v-if="giveReason">
                                        <div class="checkbox form-check-inline">
                                            <textarea class="form-control" v-model="person.dec" rows="1"></textarea>
                                        </div>
                                    </td>
                                    <td class="border-top-0 text-center" v-if="brand.isDisable && roleName != 'Admin'">
                                        <button title="Remove Item" id="bElim" type="button" disabled
                                            class="btn btn-sm btn-soft-danger btn-circle" v-on:click="RemoveRow(index)">
                                            <i class="dripicons-trash" aria-hidden="true"></i>
                                        </button>
                                    </td>
                                    <td class="border-top-0 text-center" v-else>
                                        <button title="Remove Item" id="bElim" type="button"
                                            class="btn btn-sm btn-soft-danger btn-circle" v-on:click="RemoveRow(index)">
                                            <i class="dripicons-trash" aria-hidden="true"></i>
                                        </button>
                                    </td>

                                </tr>
                                <tr class="text-end">
                                    <td colspan="6" class="border-top-0 text-end">
                                        <button id="but_add" class="btn btn-success btn-sm"
                                            v-on:click="AddRow()">+Add</button>
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                    </div>

                </div>
                <div class="row">
                    <div class="col-md-12" v-if="roleName != 'Cashier'">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddBenificary.Note') }}:<span class="text-danger"> *</span>
                        </label>
                        <textarea v-model="brand.note" class="form-control" rows="4"></textarea>
                        <!-- <VueEditor v-model="brand.note" /> -->
                    </div>

                </div>


            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveBenificary('Draft')"
                    v-bind:disabled="$v.brand.$invalid" v-if="type == 'Add' && roleName != 'User'">
                    {{ $t('Save') }}
                </button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveBenificary('Approved')"
                    v-bind:disabled="$v.brand.$invalid" v-if="type == 'Add' && roleName == 'Admin'">
                    {{ $t('SaveasApproved') }}
                </button>

                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveBenificary('Approved')"
                    v-bind:disabled="$v.brand.$invalid" v-if="type == 'Edit' && roleName == 'Admin'">
                    {{ $t('Approved') }}
                </button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveBenificary('Draft')"
                    v-bind:disabled="$v.brand.$invalid" v-if="type == 'Edit' && roleName != 'User'">
                    {{ $t('Update') }}
                </button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">
                    {{ $t('Close') }}
                </button>
            </div>
            <loading :active.sync="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>
    </modal>
</template>

<script>
import clickMixin from '@/Mixins/clickMixin'
import 'vue-loading-overlay/dist/vue-loading.css';
import {
    required, requiredIf, minLength, maxLength, numeric, decimal
} from "vuelidate/lib/validators"
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/vue-loading.css';
// import {
//     VueEditor
// } from "vue2-editor";

import Multiselect from 'vue-multiselect';
import moment from 'moment'

export default {
    mixins: [clickMixin],
    name: 'AddBenificary',
    props: ['show', 'brand', 'type'],
    components: {
        Loading,
        Multiselect,
    },
    data: function () {
        return {
            advancePaymentvalue: '',
            giveReason: false,
            isDisableValue: false,
            randerforempty: 0,
            paymentType: null,
            arabic: '',
            english: '',
            loading: false,
            roleName: '',
            isDisable: false,

        }
    },
    validations: {
        brand: {
            name: {

            },
            nameAr: {
                required: requiredIf((x) => {
                    if (x.name == '' || x.name == null)
                        return true;
                    return false;
                }),
            },
            paymentIntervalMonth: {},
            amountPerMonth: {
                decimal,
                required: requiredIf((x) => {
                    if (x.recurringAmount == 0)
                        return true;
                    return false;
                }),
            },
            ugamaNo: {
                required,
                minLength: minLength(10),
                maxLength: maxLength(10),
            },
            phoneNo: {
                numeric,
                minLength: minLength(10),
                maxLength: maxLength(10),
            },
            reason: {
                required: requiredIf((x) => {
                    if (x.type == 'Add') {
                        return false;
                    } else if (x.type == 'Edit') {
                        return true;
                    }
                }),
            },
            recurringAmount: {
                decimal,
            },
            paymentTypeId: {
                required,

            },
            startMonth: {
                required: requiredIf((x) => {
                    if (x.paymentTypeId == "42d23cb7-560f-4d30-18c4-08dbb5b41e3f") {
                        return false;
                    } else {
                        return true;
                    }
                }),

            }
        }
    },
    methods: {
        GiveReason: function () {
            this.giveReason = true;
        },
        GetRecord: function () {
            var root = this;
            debugger;

            if (root.$refs.ChlidDropdown != undefined) {

                var value = this.$refs.ChlidDropdown.GetSalaryOfSelected();
                if (value == '' || value == null) {
                    this.paymentType = null;
                } else {
                    this.paymentType = value.code;
                    if (value.code == 0) {
                        this.brand.durationType = '';
                        this.brand.isActive = false;
                    }
                    else {
                        this.brand.durationType = 'Customize';
                        this.GetDateMonth();
                        this.brand.isActive = true;

                    }
                    this.GetValueOfRecurring();
                }

            } else {
                this.paymentType = null;
            }
        },

        GetValueOfRecurring: function () {
            debugger;
            //   alert(this.brand.recurringAmount);
            if (this.brand.recurringAmount > 0) {
                if (this.paymentType == null || this.paymentType == 0) {
                    this.brand.amountPerMonth = 0

                } else {
                    this.brand.amountPerMonth = parseFloat(this.brand.recurringAmount / this.paymentType);
                    this.brand.recurringAmount = parseFloat(this.brand.recurringAmount);
                }

            } else {
                this.brand.amountPerMonth = 0;

            }

        },
        FloatValue: function () {
            this.brand.amountPerMonth = parseFloat(this.brand.amountPerMonth).toFixed(3).slice(0, -1);
        },
        RemoveRow: function (index) {
            this.brand.benificaryAuthorization.splice(index, 1);
        },
        AddRow: function () {

            this.brand.benificaryAuthorization.push({
                id: '',
                benficaryId: '',
                authorizationPersonId: '',
                approvalPersonId: '',
                date: '',
                description: '',
                isActive: ''
            });

        },
        GetDateMonth: function () {


            if (this.brand.durationType != undefined && this.brand.durationType != '' && this.brand.durationType != null) {
                if (this.brand.durationType != 'Indefinite') {
                    debugger;
                    if (this.brand.startMonth != undefined && this.brand.startMonth != '' && this.brand.startMonth != null) {
                        this.brand.startDate = this.brand.startMonth;
                        this.brand.endDate = moment(this.brand.startMonth).add((this.paymentType - 1), 'months');

                    } else {
                        this.brand.startDate = '';
                        this.brand.endDate = '';
                    }

                }

            } else {
                this.brand.startDate = '';
                this.brand.endDate = '';
            }
            this.randerforempty++;

        },
        close: function () {
            this.$emit('close');
        },
        SaveBenificary: function (approvedtype) {
            debugger;
            var root = this;
            this.brand.approvalStatus = approvedtype;
            var aa = this.brand.beneficiaryId;
            this.brand.beneficiaryId = aa;
            this.loading = true;
            var token = '';

            if (this.brand.recurringAmount === 0 && this.brand.amountPerMonth ===0 ) {
                this.loading = false;
                return this.$swal({
                    title: 'Error',
                    text: this.english == 'en' ? 'amount cannot be zero' : 'المبلغ لا يمكن أن يكون صفرًا',
                    type: 'error',
                    icon: 'error',
                    showConfirmButton: false,
                    timer: 1500,
                    timerProgressBar: true,
                });
                
            }



            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }

            if (this.brand.id != '00000000-0000-0000-0000-000000000000' && this.brand.isActive == false && this.brand.advancePayment != 0 && this.brand.reason == null) {
                this.loading = false;
                return this.$swal({
                    title: 'Error',
                    text: this.english == 'en' ? 'Please give reason' : 'من فضلك اذكر السبب',
                    type: 'error',
                    icon: 'error',
                    showConfirmButton: false,
                    timer: 1500,
                    timerProgressBar: true,
                });
            }

            this.brand.benificaryAuthorization.map(auth => {
                if (auth.isActive === '') {
                    auth.isActive = false;
                }
                return auth;
            });
            var prd = root.brand.benificaryAuthorization.findIndex(x => x.authorizationPersonId == '' || x.authorizationPersonId == null);
            if (prd >= 0) {
                root.brand.benificaryAuthorization.splice(prd, 1)
            }

            if (this.advancePaymentvalue != '' && this.advancePaymentvalue != null) {
                debugger;
                if (this.$i18n.locale == 'en') {
                    var advance = parseInt(this.advancePaymentvalue.match(/\d+/)[0], 10);
                    this.brand.advancePayment = advance;
                }
                else {
                    let intValue;
                    const arabicToIntMap = {
                        'شهر واحد': 1,
                        'شهرين': 2,
                        'ثلاثة أشهر': 3,
                        'أربعة أشهر': 4,
                        'خمسة أشهر': 5,
                        'ستة أشهر': 6,
                        'سبعة اشهر': 7,
                        'ثمانية اشهر': 8,
                        'تسعة أشهر': 9,
                        'عشرة أشهر': 10,
                        'أحد عشر شهرا': 11,
                        'سنة': 12
                    };
                    intValue = arabicToIntMap[this.advancePaymentvalue];
                    this.brand.advancePayment = intValue;
                }

            }


            this.$https.post('/Benificary/SaveBenificary', this.brand, {
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
                                timer: 1500,
                                timerProgressBar: true,
                            });

                            root.close();
                        } else {

                            root.$swal({
                                title: 'Update',
                                text: response.data.isAddUpdate,
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.close();

                        }
                    } else {
                        root.$swal({
                            title: 'Error',
                            text: response.data.isAddUpdate,
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 1500,
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
            root.$https.get('/Benificary/AutoCodeGenerate?name=Benificaries', {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {
                    if (response.data) {
                        debugger;
                        root.brand.beneficiaryId = parseInt(response.data);
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
    mounted: function () {
        this.roleName = localStorage.getItem('RoleName');
        if (this.type == 'Edit') {
            this.paymentType = this.brand.paymentType;
            this.advancePaymentvalue = `${this.brand.advancePayment} Months`;
        }
        this.english = localStorage.getItem('locales');
        this.arabic = localStorage.getItem('locales');

        if (this.brand.id == '00000000-0000-0000-0000-000000000000') {
            this.GetAutoCode();
        }
        this.isDisableValue = this.roleName == 'Cashier' && this.type == 'Edit' ? true : false;
    }
}
</script>
