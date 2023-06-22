<template>
    <modal :show="show" :modalLarge="true">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type == 'Edit'">
                    {{ $t('AddFunds.UpdateFunds') }}
                </h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>
                    {{ $t('AddFunds.AddFunds') }}
                </h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="form-group has-label col-sm-3 ">
                        <label class="text  font-weight-bolder">
                            {{ $t('Payment.Code') }}:<span class="text-danger"> *</span>
                        </label>
                        <input type="text" class="form-control" v-model="$v.brand.code.$model" :values="brand.code"
                            disabled />
                    </div>
                    <div class="form-group has-label col-sm-3 ">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddFunds.CharityResource') }}:<span class="text-danger"> *</span>
                        </label>
                        <charityresources v-model="brand.charityResouceId" :values="brand.charityResouceId" />
                    </div>
                    <div class="form-group has-label col-sm-3 ">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddFunds.TypeOfTransaction') }}:<span class="text-danger"> *</span>
                        </label>
                        <div class="col-sm-12">
                            <multiselect v-model="brand.typeOfTransaction"
                            :options="['Monetary ', 'Check']" :show-labels="false"
                            :placeholder="$t('AddBenificary.SelectType')">
                        </multiselect>
                            </div>
                    </div>
                    <div class="form-group has-label col-sm-3 ">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddFunds.Amount') }}:<span class="text-danger"> *</span>
                        </label>
                        <input class="form-control" v-model="$v.brand.amount.$model" type="number" />
                    </div>
                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddFunds.Description') }}:
                        </label>
                        <VueEditor v-model="brand.description" />
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveFunds"
                    v-bind:disabled="$v.brand.$invalid" v-if="type != 'Edit' && roleName != 'User'">
                    {{ $t('Save') }}
                </button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveFunds"
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
import { required } from "vuelidate/lib/validators"
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/vue-loading.css';
import { VueEditor } from "vue2-editor";
import Multiselect from 'vue-multiselect';


export default {
    mixins: [clickMixin],
    name: 'AddBenificary',
    props: ['show', 'brand', 'type'],
    components: {
        Loading,
        VueEditor,
        Multiselect
    },
    data: function () {
        return {
            user: '',
            arabic: '',
            english: '',
            roleName: '',
            loading: false,
        }
    },
    validations: {
        brand: {
            amount: {
                required
            },
            code: {
                required
            }
        }
    },
    methods: {
        close: function () {
            this.$emit('close');
        },
        SaveFunds: function () {

            var root = this;
            var aa = localStorage.getItem('UserId');
            this.brand.userId = aa;
            this.loading = true;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }

            this.$https.post('/Benificary/SaveFunds', this.brand, { headers: { "Authorization": `Bearer ${token}` } })
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
        },
        GetAutoCode: function (val) {

            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Benificary/AutoCodeGenerate?name=' + val, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {
                    if (response.data) {
                        root.brand.code = response.data;
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
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        this.roleName = localStorage.getItem('RoleName');

        if (this.brand.id == '00000000-0000-0000-0000-000000000000') {
            this.GetAutoCode('Funds');
        }
    }
}
</script>
