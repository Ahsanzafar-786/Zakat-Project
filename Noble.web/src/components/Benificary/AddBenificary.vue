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
                    <div class="col-md-6">
                        <div class="form-group has-label col-sm-12 ">
                            <label class="text  font-weight-bolder">
                                {{ $t('AddBenificary.Name') }}:<span class="text-danger"> *</span>
                            </label>
                            <input class="form-control" v-model="$v.brand.name.$model" type="text" />
                        </div>

                        <div class="form-group has-label col-sm-12 ">
                            <label class="text  font-weight-bolder">
                                {{ $t('AddBenificary.Ids') }}:
                            </label>
                            <input class="form-control" v-model="$v.brand.ugamaNo.$model" type="text" />
                        </div>

                        <div class="form-group has-label col-sm-12 ">
                            <label class="text  font-weight-bolder">
                                {{ $t('AddBenificary.Nationality') }}:
                            </label>
                            <input class="form-control" v-model="brand.nationality" type="text" />
                        </div>

                        <div class="form-group has-label col-sm-12 ">
                            <label class="text  font-weight-bolder">
                                {{ $t('AddBenificary.ContactNo') }}:
                            </label>
                            <input class="form-control" v-model="$v.brand.phoneNo.$model" type="number" />
                        </div>
                        <h3>
                            {{ $t('AddBenificary.OtherDetail') }}
                        </h3>
                        <div class="form-group has-label col-sm-12 ">
                            <label class="text  font-weight-bolder">
                                {{ $t('AddBenificary.PaymentType') }}
                            </label>
                            <paymenttype v-model="brand.paymentTypeId" :values="brand.paymentTypeId" />
                        </div>

                        <div class="form-group has-label col-sm-12 ">
                            <label class="text  font-weight-bolder">
                                {{ $t('AddBenificary.AuthorizedPerson') }}
                            </label>
                            <authorizedperson v-model="brand.authorizedPersonId" :values="brand.authorizedPersonId" />
                        </div>
                        
                        <div class="form-group has-label col-sm-12 ">
                            <label class="text  font-weight-bolder">
                                {{ $t('AddBenificary.PaymentIntervalMonths') }}:
                            </label>
                            <input class="form-control" v-model="$v.brand.paymentIntervalMonth.$model" type="number" />
                        </div>
                        <div class="form-group col-sm-12">
                            <label></label>
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox1" v-model="brand.isActive">
                                <label for="inlineCheckbox1"> {{ $t('AddBenificary.Active') }} </label>
                            </div>
                        </div>
                    </div>


                    <div class="col-md-6">
                        <div class="form-group has-label col-sm-12 ">
                            <label class="text  font-weight-bolder">
                                {{ $t('AddBenificary.NameArabic') }}:<span class="text-danger"> *</span>
                            </label>
                            <input class="form-control" v-model="$v.brand.nameAr.$model" type="text" />
                        </div>

                        <div class="form-group has-label col-sm-12 ">
                            <label class="text  font-weight-bolder">
                                {{ $t('AddBenificary.PassportNo') }}:
                            </label>
                            <input class="form-control" v-model="brand.passportNo" type="text" />
                        </div>
                         <div class="form-group has-label col-sm-12 ">
                            <label class="text  font-weight-bolder">
                                {{ $t('AddBenificary.Gender') }}:
                            </label>
                            <select v-model="brand.gender" class="form-select" aria-label="Default select example">
                                <option value="Male">{{ $t('AddBenificary.Male') }}</option>
                                <option value="Female">{{ $t('AddBenificary.Female') }}</option>
                            </select>
                        </div>
                        <div class="form-group has-label col-sm-12 ">
                            <label class="text  font-weight-bolder">
                                {{ $t('AddBenificary.Address') }}:
                            </label>
                            <input class="form-control" v-model="brand.address" type="text" />
                        </div>
                        <div class="form-group has-label col-sm-12 ">
                            &nbsp;
                        </div>
                        <div class="form-group has-label col-sm-12 ">
                            <label class="text  font-weight-bolder">
                                {{ $t('AddBenificary.ApprovalPersons') }}
                            </label>
                            <approvalperson v-model="brand.approvalPersonId" :values="brand.approvalPersonId" />
                        </div>
                        
                        <div class="form-group has-label col-sm-12 ">
                            <label class="text  font-weight-bolder">
                                {{ $t('AddBenificary.RecurringAmount') }}:
                            </label>
                            <input class="form-control" v-model="$v.brand.recurringAmount.$model" type="number" />
                        </div>
                        <div class="form-group has-label col-sm-12 ">
                            <label class="text  font-weight-bolder">
                                {{ $t('AddBenificary.AmountPerMonth') }}:
                            </label>
                            <input class="form-control" v-model="$v.brand.amountPerMonth.$model" type="number" />
                        </div>
                    </div>
                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddBenificary.Note') }}:
                        </label>
                        <VueEditor v-model="brand.note" />
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveBenificary"
                    v-bind:disabled="$v.brand.$invalid" v-if="type != 'Edit'">
                     {{ $t('Save') }}
                </button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveBenificary"
                    v-bind:disabled="$v.brand.$invalid" v-if="type == 'Edit'">
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
import {  requiredIf } from "vuelidate/lib/validators"
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/vue-loading.css';
import { VueEditor } from "vue2-editor";
export default {
    mixins: [clickMixin],
    name: 'AddBenificary',
    props: ['show', 'brand', 'type'],
    components: {
        Loading,
        VueEditor
    },
    data: function () {
        return {
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
            paymentIntervalMonth: {
            },
            amountPerMonth: {
            },
            ugamaNo: {
            },
            phoneNo: {
            },
            recurringAmount: {
            }
        }
    },
    methods: {
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
             
            this.$https.post('/Benificary/SaveBenificary', this.brand, { headers: { "Authorization": `Bearer ${token}` } })
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
                        }
                        else {

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
                    }
                    else {
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
                    root.$swal.fire(
                        {
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
