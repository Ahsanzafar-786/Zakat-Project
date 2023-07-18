<template>
    <modal :show="show">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type == 'Edit'">
                     {{ $t('AddBenificaryNote.UpdateBenificaryNote') }}
                </h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>
                    {{ $t('AddBenificaryNote.AddBenificaryNote') }}
                </h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">
                           {{ $t('AddBenificaryNote.Benificary') }}:
                        </label>
                        <benificary v-model="brand.benificaryId" :values="brand.benificaryId" v-bind:class="$i18n.locale == 'en' ? 'text-left ' : 'multiselect__placeholder12'" />
                    </div>
                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddBenificaryNote.Note') }}:<span class="text-danger"> *</span>
                        </label>
                        <input class="form-control" v-model="$v.brand.note.$model" type="text" />
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveBenificaryNote"
                    v-bind:disabled="$v.brand.$invalid" v-if="type != 'Edit' && roleName != 'User'">
                    {{ $t('Save') }}
                </button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveBenificaryNote"
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
        user:'',
            arabic: '',
            english: '',
            roleName: '',
            loading: false,
        }
    },
    validations: {
        brand: {
            note: {
                required
            },
        }
    },
    methods: {
        close: function () {
            this.$emit('close');
        },
        SaveBenificaryNote: function () {
             
            var root = this;
            var aa = this.brand.authorizedPersonCode;
            this.brand.authorizedPersonCode = aa;
            this.loading = true;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
             
            this.$https.post('/Benificary/SaveBenificaryNote', this.brand, { headers: { "Authorization": `Bearer ${token}` } })
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
    }
}
</script>
