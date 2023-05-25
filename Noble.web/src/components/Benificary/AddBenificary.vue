<template>
    <modal :show="show" :modalLarge="true">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type == 'Edit'">
                    Update Benificary
                </h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>
                    Add Benificary
                </h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group has-label col-sm-12 ">
                            <label class="text  font-weight-bolder">
                                Name:<span class="text-danger"> *</span>
                            </label>
                            <input class="form-control" v-model="$v.brand.name.$model" type="text" />
                        </div>
                        <div class="form-group has-label col-sm-12 ">
                            <label class="text  font-weight-bolder">
                                Payment Interval Months:<span class="text-danger"> *</span>
                            </label>
                            <input class="form-control" v-model="$v.brand.paymentIntervalMonth.$model" type="number" />
                        </div>

                        <div class="form-group has-label col-sm-12 ">
                            <label class="text  font-weight-bolder">
                                Amount Per Month:<span class="text-danger"> *</span>
                            </label>
                            <input class="form-control" v-model="$v.brand.amountPerMonth.$model" type="number" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group has-label col-sm-12 ">
                            <label class="text  font-weight-bolder">
                                Ugama No:<span class="text-danger"> *</span>
                            </label>
                            <input class="form-control" v-model="$v.brand.ugamaNo.$model" type="text" />
                        </div>

                        <div class="form-group has-label col-sm-12 ">
                            <label class="text  font-weight-bolder">
                                Phone No:<span class="text-danger"> *</span>
                            </label>
                            <input class="form-control" v-model="$v.brand.phoneNo.$model" type="number" />
                        </div>
                       <div class="row">
                                    <div class="form-group col-sm-4">
                                <div class="checkbox form-check-inline mx-2">
                                    <input type="checkbox" id="inlineCheckbox1" v-model="brand.isActive">
                                    <label for="inlineCheckbox1"> Active </label>
                                </div>
                            </div>

                            <div class="form-group col-sm-6">
                                <div class="checkbox form-check-inline mx-2">
                                    <input type="checkbox" id="inlineCheckbox2" v-model="brand.isRegister">
                                    <label for="inlineCheckbox2"> Register </label>
                                </div>
                            </div>
                       </div>
                    </div>




                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">
                            Note:<span class="text-danger"> *</span>
                        </label>
                        <VueEditor v-model="brand.note" />
                    </div>



                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveBenificary"
                    v-bind:disabled="$v.brand.$invalid" v-if="type != 'Edit'">
                    Save
                </button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveBenificary"
                    v-bind:disabled="$v.brand.$invalid" v-if="type == 'Edit'">
                    Update
                </button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">
                    Close
                </button>
            </div>
            <loading :active.sync="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>
    </modal>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import 'vue-loading-overlay/dist/vue-loading.css';
import { required } from "vuelidate/lib/validators"
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
                required
            },
            paymentIntervalMonth: {
                required
            },
            amountPerMonth: {
                required
            },
            ugamaNo: {
                required
            },
            phoneNo: {
                required
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
            debugger;
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
