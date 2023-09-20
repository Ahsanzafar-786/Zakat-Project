<template>
    <modal :show="show">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type == 'Edit'">
                    Update Expense Category
                </h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>
                    Add Expense Category
                </h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">
                            Code
                        </label>
                        <input class="form-control" v-model="brand.expenseCategoryCode" type="text" disabled :key="rendar"/>
                    </div>
                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">
                            CategoryName
                        </label>
                        <input class="form-control" v-model="brand.categoryName" type="text" />
                    </div>
                    
                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">
                            Discription:
                        </label>
                        <textarea class="form-control" v-model="brand.description" rows="3"></textarea>
                    </div>
                    <div class="form-group col-sm-12">
                        <label></label>
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="brand.isActive">
                            <label for="inlineCheckbox1"> {{ $t('AddBenificary.Active') }} </label>
                        </div>
                    </div>
                    
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveExpenseCategory"
                    v-bind:disabled="brand.$invalid" v-if="type != 'Edit'">
                    {{ $t('Save') }}
                </button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveExpenseCategory"
                    v-bind:disabled="brand.$invalid" v-if="type == 'Edit'">
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
//import { requiredIf } from "vuelidate/lib/validators"
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
            rendar:0,
            loading: false,

        }
    },
    methods: {
        close: function () {
            this.$emit('close');
        },
        SaveExpenseCategory: function () {
             
            var root = this;
            var aa = this.brand.expenseCategoryCode;
            this.brand.expenseCategoryCode = aa;
            this.loading = true;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
             
            this.$https.post('/Benificary/SaveExpenseCategory', this.brand, { headers: { "Authorization": `Bearer ${token}` } })
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
        GetAutoCode: function () {
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Benificary/AutoCodeGenerate?name=ExpenseCategory', {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                .then(function (response) {
                        if (response.data) {
                            debugger;
                            root.brand.expenseCategoryCode = response.data;
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

        if(this.brand.id == '00000000-0000-0000-0000-000000000000')
        {
            this.GetAutoCode();
        }
    }
}
</script>
