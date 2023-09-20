<template>
    <modal :show="show" :modalLarge="true">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type == 'Edit'">
                    {{ $t('AddExpense.UpdateExpense') }}
                </h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>
                    {{ $t('AddExpense.AddExpense') }}
                </h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="form-group has-label col-sm-4 ">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddExpense.Code') }}:<span class="text-danger"> *</span>
                        </label>
                        <input type="text" class="form-control" v-model="$v.brand.code.$model" :values="brand.code"
                            disabled />
                    </div>
                    <div class="form-group has-label col-sm-4 ">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddExpense.CategoryName') }}
                        </label>
                            <expensecategory v-model="brand.expenseCategoryId" :values="brand.expenseCategoryId"/>
                            
                    </div>
                    <div class="form-group has-label col-sm-4 ">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddExpense.Amount') }}:<span class="text-danger"> *</span>
                        </label>
                        <decimaltofix v-model="brand.amount"  > </decimaltofix>
                    </div>
                    
                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddExpense.Description') }}:
                        </label>
                        <textarea v-model="brand.description" class="form-control"  rows="4"></textarea>
                    </div>

                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveExpense"
                    v-bind:disabled="$v.brand.$invalid" v-if="type != 'Edit' && roleName != 'User'">
                    {{ $t('Save') }}
                </button>
                <!-- <button type="button" class="btn btn-soft-primary btn-sm" v-bind:disabled="$v.brand.$invalid"
                v-if="type != 'Edit' && roleName != 'User'" v-on:click="SaveExpense(true)">
                            {{ $t('SaveasPrint') }}
                        </button> -->
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveExpense"
                    v-bind:disabled="$v.brand.$invalid" v-if="type == 'Edit' && roleName != 'User'">
                    {{ $t('Update') }}
                </button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">
                    {{ $t('Close') }}
                </button>
            </div>
            <loading :active.sync="loading" :can-cancel="false" :is-full-page="true"></loading>
            <print :show="show1" v-if="show1" :reportsrc="reportsrc1" :changereport="changereportt" @close="IsSaveRpt"
                @IsSave="IsSaveRpt" />
        </div>
    </modal>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import 'vue-loading-overlay/dist/vue-loading.css';
import { required,minValue } from "vuelidate/lib/validators"
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
            user: '',
            arabic: '',
            english: '',
            roleName: '',
            loading: false,
            changereport: 0,
            reportsrc1: '',
            show1: false,
            
        }
    },
    validations: {
        brand: {
            amount: {
                required,
                minValue: minValue(0.01)
                        },
            code: {
                
            }
        }
    },
    methods: {
        IsSaveRpt: function () {
            this.show1 = !this.show1;
            this.$emit('close');

            
        },
        close: function () {
            this.$emit('close');
        },
  
        PrintRdlc: function (val,printReport) {
            debugger;

            var companyId = '';
            if (this.$session.exists()) {
                companyId = localStorage.getItem('CompanyID');
            }
            this.reportsrc1 = this.$ReportServer + '/Invoice/A4_DefaultTempletForm.aspx?id=' +val+'&CompanyID='+companyId+'&formName=Funds'+ '&Print=' + printReport



            this.changereport++;
            this.show1 = !this.show1;


        },
        SaveExpense: function (isPrint) {

            var root = this;
            if(this.roleName != 'Admin')
            {
                var aa = localStorage.getItem('UserId');
                this.brand.userId = aa;
            }
           
            this.loading = true;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }

            this.$https.post('/Benificary/SaveExpense', this.brand, { headers: { "Authorization": `Bearer ${token}` } })
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

                        }
                        if (isPrint == true) {
                                root.PrintRdlc(response.data.id,true);

                            } 
                            else
                            {
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
            this.GetAutoCode('Expense');
        }
    },
    created:function(){
        if (this.brand.id == '00000000-0000-0000-0000-000000000000') {
            this.brand.userId=localStorage.getItem('UserId');

        }
    }
}
</script>
