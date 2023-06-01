<template>
<modal :show="show" :modalLarge="true">
    <div class="modal-content">
        <div class="modal-header">
            <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type == 'Edit'">
                {{ $t('AddBenificary.UpdateBenificary') }}
            </h6>
            <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>
                {{ $t('AddBenificary.AddBenificary') }}
            </h6>
            <button type="button" class="btn-close" v-on:click="close()"></button>
        </div>
        <div class="modal-body">
            <div class="row">
                <div class="col-md-6 form-group">
                    <label class="text  font-weight-bolder">
                        {{ $t('AddBenificary.Name') }}:<span class="text-danger"> *</span>
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
                        {{ $t('AddBenificary.Ids') }}:
                    </label>
                    <input class="form-control" v-model="$v.brand.ugamaNo.$model" type="text" />
                </div>
                <div class="col-md-6 form-group">
                    <label class="text  font-weight-bolder">
                        {{ $t('AddBenificary.PassportNo') }}:
                    </label>
                    <input class="form-control" v-model="brand.passportNo" type="text" />
                </div>

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
                    <input class="form-control" v-model="$v.brand.phoneNo.$model" type="number" />
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

                <div class="col-md-6 form-group">
                    <label class="text  font-weight-bolder">
                        {{ $t('AddBenificary.PaymentType') }}
                    </label>
                    <paymenttype v-model="brand.paymentTypeId" v-on:input="GetRecord" ref="ChlidDropdown" :values="brand.paymentTypeId" />
                </div>
                <div class="col-md-6 form-group" v-if="paymentType!=0">
                    <label class="text  font-weight-bolder">
                        {{ $t('AddBenificary.RecurringAmount') }}:
                    </label>
                    <input class="form-control" v-on:input="GetValueOfRecurring" v-model="$v.brand.recurringAmount.$model" @click="$event.target.select()" type="number" />
                </div>
                <div class="col-md-6 form-group" v-if="paymentType!=0">

                    <label class="text  font-weight-bolder">
                        {{ $t('AddBenificary.AdvancePayment') }}:
                    </label>
                    <multiselect v-model="brand.advancePayment " v-on:input="GetDateMonth" :options="[0,1,2,3,4,5,6,7,8,9,10,11,12]" :show-labels="false" :placeholder="$t('AddBenificary.SelectType')">
                    </multiselect>
                </div>
                <div class="col-md-6 form-group">
                    <label class="text  font-weight-bolder" v-if="paymentType!=0">
                        {{ $t('AddBenificary.AmountPerMonth') }}:
                    </label>
                    <label class="text  font-weight-bolder" v-else>
                        Amount:
                    </label>
                    <input class="form-control" v-model="$v.brand.amountPerMonth.$model"  @click="$event.target.select()" type="number" />
                </div>
                <div class="col-md-6 form-group">
                    <label>{{ $t('AddBenificary.StartFrom') }}:</label>
                    <datepicker v-model="brand.startMonth " :type="'month'" />

                </div>
                <div class="col-md-6 form-group">
                    <label class="text  font-weight-bolder">
                        {{ $t('AddBenificary.ApprovedBy') }}:
                    </label>
                    <approvalperson v-model="brand.approvedPaymentId " :values="brand.approvedPaymentId" />
                </div>

                <!-- <div class="col-md-6 form-group">
                    <label class="text  font-weight-bolder">
                        {{ $t('AddBenificary.AuthorizedPerson') }}
                    </label>
                    <authorizedperson v-model="brand.authorizedPersonId" :values="brand.authorizedPersonId" />
                </div> -->

                <!-- <div class="col-md-6 form-group">
                    <label class="text  font-weight-bolder">
                        {{ $t('AddBenificary.PaymentIntervalMonths') }}:
                    </label>
                    <input class="form-control" v-model="$v.brand.paymentIntervalMonth.$model" type="number" />
                </div> -->
                <div class="form-group col-sm-12">
                    <label></label>
                    <div class="checkbox form-check-inline mx-2">
                        <input type="checkbox" id="inlineCheckbox1" v-model="brand.isActive">
                        <label for="inlineCheckbox1"> {{ $t('AddBenificary.Active') }} </label>
                    </div>
                </div>

                <!-- <div class="col-md-12">
                    <label class="text  font-weight-bolder">
                        {{ $t('AddBenificary.Note') }}:
                    </label>
                    <VueEditor v-model="brand.note" />
                </div> -->
            </div>
            <div class="row" v-if="paymentType!=0">
                <div class="col-md-12 form-group">
                    <h4 style="color:black !important;">

                        {{ $t('AddBenificary.PaymentDuration') }}

                    </h4>
                </div>

                <div class="col-md-6 form-group">
                    <label class="text  font-weight-bolder">
                        {{ $t('AddBenificary.Type') }}:
                    </label>
                    <multiselect v-model="brand.durationType " v-on:input="GetDateMonth" :options="['Indefinite ', 'Customize']" :show-labels="false" :placeholder="$t('AddBenificary.SelectType')">
                    </multiselect>
                </div>
                <div class="col-md-3 form-group" v-if="brand.durationType=='Customize'">
                    <label>Start Month:</label>
                    <datepicker v-model="brand.startDate " v-bind:key="randerforempty" :type="'month'" />

                </div>
                <div class="col-md-3 form-group" v-if="brand.durationType=='Customize'">
                    <label>End Month:</label>
                    <datepicker v-model="brand.endDate  " v-bind:key="randerforempty" :type="'month'" />

                </div>
                <div class="col-md-6 form-group">
                    <label class="text  font-weight-bolder">
                        {{ $t('AddBenificary.ApprovedBy') }}:
                    </label>
                    <approvalperson v-model="brand.approvedPaymentId " :values="brand.approvedPaymentId" />
                </div>

            </div>
            <div class="row" v-if="paymentType!=0">
                <div class="col-md-12 form-group">
                    <h4 style="color:black !important;">

                        {{ $t('AddBenificary.BeneficiaryAuthorization') }}

                    </h4>
                </div>
                <div class="col-lg-12">
                    <table class="table mb-0">
                        <thead class="thead-light table-hover">
                            <tr>
                                <th class="text-center" style="width: 5% !important">#</th>
                                <th class="text-center" style="width: 20% !important">{{ $t('AddBenificary.AuthorizedPerson') }}</th>
                                <th class="text-center" style="width: 20% !important">{{ $t('AddBenificary.ApprovedBy') }}</th>
                                <th class="text-center" style="width: 20% !important">{{ $t('AddBenificary.Date') }}</th>
                                <th class="text-center" style="width: 20% !important">{{ $t('AddBenificary.Status') }}</th>
                                <th class="text-center" style="width: 15% !important">{{ $t('AddBenificary.Action') }}</th>
                            </tr>
                        </thead>

                        <tbody>
                            <tr v-for="(person , index) in brand.benificaryAuthorization" :key="index">
                                <td class="border-top-0 text-center">
                                    {{ index+1 }}
                                </td>
                                <td class="border-top-0 text-center">
                                    <authorizedperson v-model="person.authorizationPersonId" :values="person.authorizationPersonId" />

                                </td>
                                <td class="border-top-0 text-center">
                                    <approvalperson v-model="person.approvalPersonId " :values="person.approvalPersonId" />

                                </td>
                                <td class="border-top-0 text-center">
                                    <datepicker v-model="person.date" />

                                </td>
                                <td class="border-top-0 text-center">
                                    <div class="checkbox form-check-inline">
                                        <input v-bind:id="index+1" type="checkbox" v-model="person.isActive">
                                        <label v-bind:for="index+1"></label>
                                    </div>
                                </td>
                                <td class="border-top-0 text-center">
                                    <button title="Remove Item" id="bElim" type="button" class="btn btn-sm btn-soft-danger btn-circle" v-on:click="RemoveRow(index)">
                                        <i class="dripicons-trash" aria-hidden="true"></i>
                                    </button>
                                </td>

                            </tr>
                            <tr class="text-end">
                                <td colspan="6" class="border-top-0 text-end">
                                    <button id="but_add" class="btn btn-success btn-sm" v-on:click="AddRow()">+Add</button>
                                </td>
                            </tr>
                        </tbody>
                    </table>

                </div>

            </div>

        </div>
        <div class="modal-footer">
            <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveBenificary" v-bind:disabled="$v.brand.$invalid" v-if="type != 'Edit'">
                {{ $t('Save') }}
            </button>
            <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveBenificary" v-bind:disabled="$v.brand.$invalid" v-if="type == 'Edit'">
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
    requiredIf
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
        // VueEditor
    },
    data: function () {
        return {
            randerforempty: 0,
            paymentType: null,
            arabic: '',
            english: '',
            loading: false,
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
            amountPerMonth: {},
            ugamaNo: {},
            phoneNo: {},
            recurringAmount: {}
        }
    },
    methods: {
        GetRecord: function () {
            var root = this;
            debugger;

            if (root.$refs.ChlidDropdown != undefined) {
                debugger;
                var value = this.$refs.ChlidDropdown.GetSalaryOfSelected();
                if (value == '' || value == null) {
                    this.paymentType = null;
                } else {
                    this.paymentType = value.code;
                    this.GetValueOfRecurring();
                }

            } else {
                this.paymentType = null;
            }
        },
        GetValueOfRecurring: function () {
            if(this.brand.recurringAmount>0)
            {
                this.brand.amountPerMonth=parseFloat(this.brand.recurringAmount/this.paymentType);


            }

           
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
            debugger;

            if (this.brand.durationType != undefined && this.brand.durationType != '' && this.brand.durationType != null) {
                if (this.brand.startMonth != undefined && this.brand.startMonth != '' && this.brand.startMonth != null) {
                    this.brand.startDate = this.brand.startMonth;
                    this.brand.endDate = moment(this.brand.startMonth).add(1, 'months');

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
        SaveBenificary: function () {
            var root = this;
            var aa = this.brand.beneficiaryId;
            this.brand.beneficiaryId = aa;
            this.loading = true;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
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
                                text: response.data.isUpdate,
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
                                text: response.data.isUpdate,
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
                            text: response.data.isUpdate,
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
        }
    },
    mounted: function () {
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
    }
}
</script>
