<template>
    <modal :show="show">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type == 'Edit'">
                    Update Authorized Person
                </h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>
                    Add Authorized Person
                </h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">
                            Name:<span class="text-danger"> *</span>
                        </label>
                        <input class="form-control" v-model="$v.brand.name.$model" type="text" />
                    </div>
                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">
                            Name Arabic:<span class="text-danger"> *</span>
                        </label>
                        <input class="form-control" v-model="$v.brand.nameAr.$model" type="text" />
                    </div>
                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">
                            Gender:
                        </label>
                        <select v-model="brand.gender" class="form-select" aria-label="Default select example">
                            <option value="Male">Male</option>
                            <option value="Female">Female</option>
                        </select>
                    </div>
                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">
                            Nationality:
                        </label>
                        <input class="form-control" v-model="brand.nationality" type="text" />
                    </div>
                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">
                            ID:
                        </label>
                        <input class="form-control" v-model="brand.iqamaNo" type="text" />
                    </div>
                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">
                            Passport Number:
                        </label>
                        <input class="form-control" v-model="brand.passportNo" type="text" />
                    </div>
                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">
                            Contact No:
                        </label>
                        <input class="form-control" v-model="brand.phoneNo" type="number" />
                    </div>
                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">
                            Address:
                        </label>
                        <textarea class="form-control" v-model="brand.address" rows="3"></textarea>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveAuthorizedPerson"
                    v-bind:disabled="$v.brand.$invalid" v-if="type != 'Edit'">
                    Save
                </button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveAuthorizedPerson"
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
import { requiredIf } from "vuelidate/lib/validators"
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/vue-loading.css';
export default {
    mixins: [clickMixin],
    name: 'AddBenificary',
    props: ['show', 'brand', 'type'],
    components: {
        Loading,
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
        }
    },
    methods: {
        close: function () {
            this.$emit('close');
        },
        SaveAuthorizedPerson: function () {
            debugger;
            var root = this;
            var aa = this.brand.authorizedPersonCode;
            this.brand.authorizedPersonCode = aa;
            this.loading = true;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            debugger;
            this.$https.post('/Benificary/SaveAuthorizedPersons', this.brand, { headers: { "Authorization": `Bearer ${token}` } })
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
                            text: error.response.data.isUpdate,
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
