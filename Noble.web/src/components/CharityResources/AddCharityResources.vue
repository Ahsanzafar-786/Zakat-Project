<template>
    <modal :show="show">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type == 'Edit'">
                     {{ $t('AddCharityResources.UpdateCharityResources') }}
                </h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>
                     {{ $t('AddCharityResources.AddCharityResources') }}
                </h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddCharityResources.Id') }}:
                        </label>
                        <input class="form-control" v-model="brand.chartiyId" type="text" readonly />
                    </div>

                    <fieldset class="form-group">
                        <div class="row">
                            <div class="col-form-label col-sm-3 pt-0 text-start">
                                <span id="ember694" class="text font-weight-bolder ">{{ $t('AddCharityResources.FundsSource') }}:</span>
                            </div>
                            <div class="col-sm-9">
                                <div class="form-check form-check-inline">
                                    <input v-model="brand.business" name="contact-sub-type" id="a49946497"
                                        class=" form-check-input" type="radio" value="true">
                                    <label class="form-check-label pl-0" for="a49946497"> {{ $t('AddCharityResources.Entity') }}</label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <input v-model="brand.business" name="contact-sub-type" id="a9ff8eb35"
                                        class=" form-check-input" type="radio" value="false">
                                    <label class="form-check-label pl-0" for="a9ff8eb35">{{ $t('AddCharityResources.Individual') }}</label>
                                </div>
                            </div>
                        </div>
                    </fieldset>

                    <div class="form-group has-label col-sm-12 ">
                        <label v-if="brand.business == 'true'" class="text  font-weight-bolder">
                            {{ $t('AddCharityResources.EntityName') }}:<span class="text-danger"> *</span>
                        </label>
                        <label v-else class="text  font-weight-bolder">
                            {{ $t('AddCharityResources.PersonName') }}:<span class="text-danger"> *</span>
                        </label>
                        <input class="form-control" v-model="$v.brand.name.$model" type="text" />
                    </div>
                    <div class="form-group has-label col-sm-12 " v-if="brand.business == 'true'">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddCharityResources.City') }}:
                        </label>
                        <input class="form-control" v-model="brand.city" type="text" />
                    </div>

                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddCharityResources.ContactNo') }}:
                        </label>
                        <input class="form-control" v-model="brand.phone" type="number" />
                    </div>

                    <div class="form-group col-sm-12">
                        <label></label>
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="brand.isActive">
                            <label for="inlineCheckbox1">  {{ $t('AddCharityResources.Active') }}</label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveAuthorizedPerson"
                    v-bind:disabled="$v.brand.$invalid" v-if="type != 'Edit' && roleName != 'User'">
                    {{ $t('Save') }}
                </button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveAuthorizedPerson"
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
export default {
    mixins: [clickMixin],
    name: 'AddBenificary',
    props: ['show', 'brand', 'type'],
    components: {
        Loading,
    },
    data: function () {
        return {
            roleName:'',
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
        }
    },
    methods: {
        close: function () {
            this.$emit('close');
        },
        GetAutoCode: function () {
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Benificary/AutoCodeGenerate?name=CharityResources', {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                .then(function (response) {
                        if (response.data) {
                            debugger;
                            root.brand.chartiyId = parseInt(response.data);
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
        SaveAuthorizedPerson: function () {
             
            var root = this;
            var aa = this.brand.chartiyId;
            this.brand.chartiyId = aa;
            if(this.brand.business == 'true')
            {
                this.brand.business = true
            }
            else
            {
                this.brand.business = false
            }
            this.loading = true;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
             
            this.$https.post('/Benificary/SaveCharityResources', this.brand, { headers: { "Authorization": `Bearer ${token}` } })
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
        this.roleName = localStorage.getItem('RoleName');

        if(this.brand.id == '00000000-0000-0000-0000-000000000000')
        {
            this.GetAutoCode();
        }
    }
}
</script>
