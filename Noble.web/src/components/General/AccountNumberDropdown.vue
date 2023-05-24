<template>
    <div class="finishDownArrow">
        <div v-bind:class="dropdownAccount" v-if="disabled">
            <multiselect v-model="displayValue" disabled :options="options" :multiple="false" group-values="libs" group-label="AccountType" :show-labels="false" :group-select="false" v-bind:placeholder="$t('AccountNumberDropdown.Search')" track-by="name" label="name" >
                <p slot="noResult" class="text-danger">{{ $t('AccountNumberDropdown.NoAccountfound') }}</p>
                <span slot="noResult" class="btn btn-primary " v-on:click="addModal('Add')" v-if="isValid('CanAddCOA') ">{{ $t('AccountNumberDropdown.CreateNewAccount') }}</span><br />

            </multiselect>
        </div>
        <div v-bind:class="dropdownAccount" v-else>
            <div v-if="PanelWidth" >
                <multiselect v-model="displayValue" :options="options" :multiple="false" group-values="libs" group-label="AccountType" :show-labels="false" :group-select="false" v-bind:placeholder="$t('AccountNumberDropdown.Search')" track-by="name" label="name" >
                    <p slot="noResult" class="text-danger"> {{ $t('AccountNumberDropdown.NoAccountfound') }}</p>
                    <span slot="noResult" class="btn btn-primary " v-on:click="addModal('Add')" v-if="isValid('CanAddCOA') "> {{ $t('AccountNumberDropdown.CreateNewAccount') }}</span><br />

                </multiselect>
            </div>
            <div v-else>
                <multiselect v-model="displayValue" :options="options" :multiple="false" group-values="libs" group-label="AccountType" :show-labels="false" :group-select="false" v-bind:placeholder="$t('AccountNumberDropdown.Search')" track-by="name" label="name">
                    <p slot="noResult" class="text-danger">{{ $t('AccountNumberDropdown.NoAccountfound') }}</p>
                    <span slot="noResult" class="btn btn-primary " v-on:click="addModal('Add')" v-if="isValid('CanAddCOA') ">{{ $t('AccountNumberDropdown.CreateNewAccount') }}</span><br />

                </multiselect>
            </div>
        </div>

        <modal :show="showModal" v-if="showModal">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel">{{ $t('AccountNumberDropdown.AddAccount') }}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="form-group col-md-6" v-if="type=='Add'">
                        <label>{{ $t('AccountNumberDropdown.AccountType') }} </label>
                            <select v-model="accountTypeId" class="form-control" v-if="showModal">
                                <option value="">{{ $t('AccountNumberDropdown.SelectAccountType') }}</option>
                                <option v-for="accountType in Accounts.accountTypes" v-bind:key="accountType.id" :value="accountType.id">{{ accountType.name }}</option>
                            </select>
                        </div>
                        <div class="form-group col-md-6" v-if="type=='Add'">
                            <label>{{ $t('AccountNumberDropdown.CostCenter') }}</label>
                            <span v-if="accountTypeId!== undefined">
                                <select v-model="newAccount.costCenterId" @change="GetCostCenterCode(newAccount.costCenterId)" class="form-control" v-if="showModal">
                                    <option value="-1">{{ $t('AccountNumberDropdown.SelectCostCenter') }}</option>
                                    <option v-for="costCenter in costCenters" v-bind:key="costCenter.id" :value="costCenter.id">{{ costCenter.name }}</option>
                                </select>
                            </span>
                        </div>
                        <div class="form-group" :key="render" v-bind:class="{'has-danger': $v.newAccount.code.$error, 'col-md-4' :type== 'Add' , 'col-md-12':type=='Edit' }" v-if="CoaCode">
                            <label>{{ $t('AccountNumberDropdown.Code') }}</label>
                            <input v-model.trim="$v.newAccount.code.$model" disabled class="form-control" />
                            <span v-if="!$v.newAccount.code.required && $v.newAccount.code.$error" class="error validation-error field-validation-valid">{{ $t('AccountNumberDropdown.CodeRequired') }}</span>
                            <span v-if="!$v.newAccount.code.isExist && $v.newAccount.code.$error" class="error validation-error field-validation-valid">{{ $t('AccountNumberDropdown.CODEExist') }}</span>

                        </div>
                        <div class="form-group" v-bind:class="{'has-danger': $v.newAccount.code.$error, 'col-md-4' :type== 'Add' , 'col-md-12':type=='Edit' }" v-else>
                            <label>{{ $t('AccountNumberDropdown.Code') }}</label>
                            <input v-model.trim="$v.newAccount.code.$model" class="form-control" />
                            <span v-if="!$v.newAccount.code.required && $v.newAccount.code.$error" class="error validation-error field-validation-valid">{{ $t('AccountNumberDropdown.CodeRequired') }}</span>
                            <span v-if="!$v.newAccount.code.isExist && $v.newAccount.code.$error" class="error validation-error field-validation-valid">{{ $t('AccountNumberDropdown.CODEExist') }}</span>

                        </div>

                        <div class="form-group" v-bind:class="{'has-danger': $v.newAccount.name.$error, 'col-md-8' :type== 'Add' , 'col-md-12':type=='Edit' }">
                            <label>{{ $t('AccountNumberDropdown.Name') }}</label>
                            <input v-model.trim="$v.newAccount.name.$model" class="form-control" />
                            <span v-if="!$v.newAccount.name.required && $v.newAccount.name.$error" class="error validation-error field-validation-valid">{{ $t('AccountNumberDropdown.NameRequired') }}</span>

                        </div>

                        <div class="form-group col-md-12 ">
                            <label>{{ $t('AccountNumberDropdown.Description') }}</label>
                            <textarea v-model="newAccount.description" class="form-control" rows="3" />
                        </div>
                    
                    <div class="form-group col-md-4">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="newAccount.isActive">
                            <label for="inlineCheckbox1">{{ $t('AccountNumberDropdown.Active') }}</label>
                        </div>
                    </div>
                    
                   


                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="addAccount(newAccount)" v-bind:disabled="$v.newAccount.$invalid">{{ $t('AccountNumberDropdown.Save') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('AccountNumberDropdown.Cancel') }}</button>
            </div>
           
        </div>



    </modal>
        <!-- <modal :show="showModal" v-if="showModal">
             <button type="button" class="btn btn-primary  " v-on:click="addAccount(newAccount)" :disabled="$v.newAccount.$invalid">Save</button>

                <button type="button" class="btn btn-danger  ml-2 " v-on:click="close()">Cancel</button>
            <div class="modal-header">
                <h5 class="modal-title DayHeading" id="myModalLabel">Add Account</h5>
            </div>
            <div class="modal-body">
                <form>
                    <div class="form-row">
                        <div class="form-group col-md-6" v-if="type=='Add'">
                            <label>Account Type</label>
                            <select v-model="accountTypeId" class="form-control" v-if="showModal">
                                <option value="">Select account Type</option>
                                <option v-for="accountType in Accounts.accountTypes" v-bind:key="accountType.id" :value="accountType.id">{{ accountType.name }}</option>
                            </select>
                        </div>
                        <div class="form-group col-md-6" v-if="type=='Add'">
                            <label>Cost Center</label>
                            <span v-if="accountTypeId!== undefined">
                                <select v-model="newAccount.costCenterId" @change="GetCostCenterCode(newAccount.costCenterId)" class="form-control" v-if="showModal">
                                    <option value="-1">Select cost Center</option>
                                    <option v-for="costCenter in costCenters" v-bind:key="costCenter.id" :value="costCenter.id">{{ costCenter.name }}</option>
                                </select>
                            </span>
                        </div>
                        <div class="form-group" :key="render" v-bind:class="{'has-danger': $v.newAccount.code.$error, 'col-md-4' :type== 'Add' , 'col-md-12':type=='Edit' }" v-if="CoaCode">
                            <label>Code</label>
                            <input v-model.trim="$v.newAccount.code.$model" disabled class="form-control" />
                            <span v-if="!$v.newAccount.code.required && $v.newAccount.code.$error" class="error validation-error field-validation-valid">Code is required.</span>
                            <span v-if="!$v.newAccount.code.isExist && $v.newAccount.code.$error" class="error validation-error field-validation-valid">Code is already exist</span>

                        </div>
                        <div class="form-group" v-bind:class="{'has-danger': $v.newAccount.code.$error, 'col-md-4' :type== 'Add' , 'col-md-12':type=='Edit' }" v-else>
                            <label>Code</label>
                            <input v-model.trim="$v.newAccount.code.$model" class="form-control" />
                            <span v-if="!$v.newAccount.code.required && $v.newAccount.code.$error" class="error validation-error field-validation-valid">Code is required.</span>
                            <span v-if="!$v.newAccount.code.isExist && $v.newAccount.code.$error" class="error validation-error field-validation-valid">Code is already exist</span>

                        </div>

                        <div class="form-group" v-bind:class="{'has-danger': $v.newAccount.name.$error, 'col-md-8' :type== 'Add' , 'col-md-12':type=='Edit' }">
                            <label>Name</label>
                            <input v-model.trim="$v.newAccount.name.$model" class="form-control" />
                            <span v-if="!$v.newAccount.name.required && $v.newAccount.name.$error" class="error validation-error field-validation-valid">Name is required.</span>

                        </div>

                        <div class="form-group col-md-12">
                            <label>Description</label>
                            <textarea v-model="newAccount.description" class="form-control" />
                        </div>

                        <div class="form-group col-md-12">

                            <label style="margin: 7px;">Active</label> <br />
                            <div class="bootstrap-switch bootstrap-switch-wrapper bootstrap-switch-animate" v-bind:class="{'bootstrap-switch-on': newAccount.isActive, 'bootstrap-switch-off': !newAccount.isActive}" v-on:click="newAccount.isActive = !newAccount.isActive" style="width: 72px;">

                                <div class="bootstrap-switch-container" style="width: 122px; margin-left: 0px;">
                                    <span class="bootstrap-switch-handle-on bootstrap-switch-success" style="width: 50px;">
                                        <i class="nc-icon nc-check-2"></i>

                                    </span>
                                    <span class="bootstrap-switch-label" style="width: 30px;">&nbsp;</span>
                                    <span class="bootstrap-switch-handle-off bootstrap-switch-success" style="width: 50px;">

                                        <i class="nc-icon nc-simple-remove"></i>

                                    </span>
                                    <input class="bootstrap-switch" type="checkbox" data-toggle="switch" checked="" data-on-label="<i class='nc-icon nc-check-2'></i>" data-off-label="<i class='nc-icon nc-simple-remove'></i>" data-on-color="success" data-off-color="success">

                                </div>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
            <div class="modal-footer justify-content-center pt-3 pb-3">
                <button type="button" class="btn btn-primary  " v-on:click="addAccount(newAccount)" :disabled="$v.newAccount.$invalid">Save</button>

                <button type="button" class="btn btn-danger  ml-2 " v-on:click="close()">Cancel</button>
            </div>
        </modal> -->
    </div>

</template>
<script>
    import { required } from "vuelidate/lib/validators";
    import Multiselect from 'vue-multiselect'
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        components: {
            'multiselect': Multiselect
        },
        mixins: [clickMixin],
        props: ['value', 'headerName', 'accounts', 'accountsvalue', 'dropdownaccount', 'formName', 'formNames', 'disabled', 'advance', 'isPurchase', 'isVat', 'PanelWidth', 'companyId'],
        data: function () {
            return {
                language: "",
                render: 0,
                dropdownAccount: "",
                cashAccountId: "",
                showModal: false,
                CoaCode: false,
                Accounts: [],
                selectValue: [],
                options: [],
                type: '',
                accountTypeId: '00000000-0000-0000-0000-000000000000',
                costCenters: [],
                newAccount: {
                    id: '',
                    code: '',
                    costCenterId: '',
                    name: '',
                    isActive: true,
                    description: ''
                }
            }
        },
        computed: {
            displayValue: {
                get: function () {

                    return this.selectValue;
                },
                set: function (modifiedValue) {

                    if (modifiedValue != null) {
                        this.selectValue = modifiedValue;
                        this.$emit('input', modifiedValue.id);
                    }
                }
            }

        },
        mounted: function () {
            
            this.CoaCode = localStorage.getItem('coaCode') == 'true' ? true : false;
            // this.cashAccountId = localStorage.getItem('CashAccountId');
            // if (this.formName == 'CashPay') {
            //     if (this.cashAccountId != null && this.cashAccountId != undefined && this.cashAccountId != '') {
            //         this.value = this.cashAccountId;
            //     }
            // }
           
            this.language = this.$i18n.locale;
            this.dropdownAccount = this.dropdownaccount;
            this.isopen = true;

            this.selectValue = this.value;
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            var company = this.companyId;
            if (company == undefined) {
                company = '';
            }

            root.$https.get('/Accounting/GetCOA?isDropdown=true' + '&companyId=' + company, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {

                    root.Accounts = response.data;


                    root.$store.dispatch('GetAccountList', root.Accounts);
                    root.Accounts = root.$store.state.accounts

                    root.Accounts.accountTypes.forEach(function (accountType) {
                        var arr = [];

                        if (root.headerName != undefined) {
                            if (root.headerName == accountType.name) {
                                accountType.costCenters.forEach(function (costCenter) {
                                    costCenter.accounts.forEach(function (account) {
                                        if (account.isActive) {
                                            if (account.name == null || account.name == undefined)
                                                account.name = '';
                                            if (account.nameArabic == null || account.nameArabic == undefined)
                                                account.nameArabic = '';
                                            arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                        }
                                     });
                                });
                            }
                        }
                        else {
                            accountType.costCenters.forEach(function (costCenter) {
                                if (root.formName == 'CashReceipt') {
                                    if (costCenter.code == '101000') {
                                        costCenter.accounts.forEach(function (account) {
                                            if (account.isActive) {
                                                if (account.name == null || account.name == undefined)
                                                    account.name = '';
                                                if (account.nameArabic == null || account.nameArabic == undefined)
                                                    account.nameArabic = '';
                                                arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                            }
                                         });
                                    }
                                }
                                else if (root.formName == 'Expense') {
                                    if (costCenter.code == '605050' || costCenter.code == '606001' || costCenter.code == '605001' || costCenter.code == '609000') {
                                        costCenter.accounts.forEach(function (account) {
                                            if (account.isActive) {
                                                if (account.name == null || account.name == undefined)
                                                    account.name = '';
                                                if (account.nameArabic == null || account.nameArabic == undefined)
                                                    account.nameArabic = '';
                                                arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                            }
                                        });
                                    }
                                }
                                else if (root.formName == 'ForExpense') {
                                    if (costCenter.code == '101000' || costCenter.code == '105000') {
                                        costCenter.accounts.forEach(function (account) {
                                            if (account.isActive) {
                                                if (account.name == null || account.name == undefined)
                                                    account.name = '';
                                                if (account.nameArabic == null || account.nameArabic == undefined)
                                                    account.nameArabic = '';
                                                arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                            }
                                          });
                                    }
                                }
                                else if (root.formName == 'Customer') {

                                    if (costCenter.code == '120000') {
                                        costCenter.accounts.forEach(function (account) {
                                            if (account.isActive) {
                                                if (account.name == null || account.name == undefined)
                                                    account.name = '';
                                                if (account.nameArabic == null || account.nameArabic == undefined)
                                                    account.nameArabic = '';
                                                arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                            }
                                           });
                                    }
                                }
                                else if (root.formName == 'Supplier') {
                                    if (costCenter.code == '200000') {
                                        costCenter.accounts.forEach(function (account) {
                                            if (account.isActive) {
                                                if (account.name == null || account.name == undefined)
                                                    account.name = '';
                                                if (account.nameArabic == null || account.nameArabic == undefined)
                                                    account.nameArabic = '';
                                                arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                            }
                                          });
                                    }
                                }
                                else if (root.formName == 'OpeningCash') {
                                    if (costCenter.code != '111000' && costCenter.code != '600001') {
                                        costCenter.accounts.forEach(function (account) {
                                            if (account.isActive) {
                                                if (account.name == null || account.name == undefined)
                                                    account.name = '';
                                                if (account.nameArabic == null || account.nameArabic == undefined)
                                                    account.nameArabic = '';
                                                arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                            }
                                         });
                                    }
                                }

                                else if (root.formName == 'PettyCash') {
                                    if (costCenter.code == '101000') {
                                        costCenter.accounts.forEach(function (account) {
                                            if (account.isActive) {
                                                if (account.name == null || account.name == undefined)
                                                    account.name = '';
                                                if (account.nameArabic == null || account.nameArabic == undefined)
                                                    account.nameArabic = '';
                                                arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                            }
                                        });
                                    }
                                }
                                else if (root.formName == 'BankReceipt') {

                                    if (costCenter.code == '105000') {
                                        costCenter.accounts.forEach(function (account) {
                                            if (account.isActive) {
                                                if (account.name == null || account.name == undefined)
                                                    account.name = '';
                                                if (account.nameArabic == null || account.nameArabic == undefined)
                                                    account.nameArabic = '';
                                                arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                            }
                                          });
                                    }
                                }
                                else if (root.formNames == 'AdvanceExpense' && root.isVat) {
                                    if (costCenter.code == '130000') {
                                        costCenter.accounts.forEach(function (account) {
                                            if (account.isActive) {
                                                if (account.name == null || account.name == undefined)
                                                    account.name = '';
                                                if (account.nameArabic == null || account.nameArabic == undefined)
                                                    account.nameArabic = '';
                                                arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                            }
                                        });
                                    }
                                }
                                else if (root.formNames == 'AdvanceExpense') {
                                    if (costCenter.code == '220000' || costCenter.code == '605001') {
                                        costCenter.accounts.forEach(function (account) {
                                            if (account.isActive) {
                                                if (account.name == null || account.name == undefined)
                                                    account.name = '';
                                                if (account.nameArabic == null || account.nameArabic == undefined)
                                                    account.nameArabic = '';
                                                arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                            }
                                         });
                                    }

                                }
                                else if (root.formName == 'Advance') {

                                    if (costCenter.code == '609000') {
                                        costCenter.accounts.forEach(function (account) {
                                            if (account.isActive) {
                                                if (account.name == null || account.name == undefined)
                                                    account.name = '';
                                                if (account.nameArabic == null || account.nameArabic == undefined)
                                                    account.nameArabic = '';
                                                arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                            }
                                           });
                                    }
                                }
                                else if (root.formName == 'CashPay') {

                                    if (root.advance ? costCenter.code == '101000' : root.isPurchase ? (costCenter.code == '101000' || costCenter.code == '160000') : (costCenter.code == '101000' || costCenter.code == '210000')) {
                                        costCenter.accounts.forEach(function (account) {
                                            if (account.isActive) {
                                                if (account.name == null || account.name == undefined)
                                                    account.name = '';
                                                if (account.nameArabic == null || account.nameArabic == undefined)
                                                    account.nameArabic = '';
                                                arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                            }
                                        });
                                    }
                                }
                                else if (root.formName == 'BankPay') {

                                    if (root.advance ? costCenter.code == '105000' : root.isPurchase ? (costCenter.code == '105000' || costCenter.code == '160000') : (costCenter.code == '105000' || costCenter.code == '210000')) {
                                        costCenter.accounts.forEach(function (account) {
                                            if (account.isActive) {
                                                if (account.name == null || account.name == undefined)
                                                    account.name = '';
                                                if (account.nameArabic == null || account.nameArabic == undefined)
                                                    account.nameArabic = '';
                                                arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                            }
                                        });
                                    }
                                }
                                else if (root.formName == 'AdvanceReceipt') {

                                    if (costCenter.code == '210000') {
                                        costCenter.accounts.forEach(function (account) {
                                            if (account.isActive) {
                                                if (account.name == null || account.name == undefined)
                                                    account.name = '';
                                                if (account.nameArabic == null || account.nameArabic == undefined)
                                                    account.nameArabic = '';
                                                arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                            }
                                        });
                                    }
                                }
                                else if (root.formName == 'AdvancePay') {

                                    if (costCenter.code == '160000') {

                                        costCenter.accounts.forEach(function (account) {
                                            if (account.isActive) {
                                                if (account.name == null || account.name == undefined)
                                                    account.name = '';
                                                if (account.nameArabic == null || account.nameArabic == undefined)
                                                    account.nameArabic = '';
                                                arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                            }
                                        });
                                    }
                                }
                                else if (root.formName == 'ContractorAdvancePay') {

                                    if (costCenter.code == '160000') {

                                        costCenter.accounts.forEach(function (account) {
                                            if (account.isActive) {
                                                if (account.name == null || account.name == undefined)
                                                    account.name = '';
                                                if (account.nameArabic == null || account.nameArabic == undefined)
                                                    account.nameArabic = '';
                                                arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                            }
                                        });
                                    }
                                }



                                else {

                                    if (root.formNames == 'CashReceipt') {
                                        if (costCenter.code == '120000') {
                                            costCenter.accounts.forEach(function (account) {
                                                if (account.isActive) {
                                                    if (account.name == null || account.name == undefined)
                                                        account.name = '';
                                                    if (account.nameArabic == null || account.nameArabic == undefined)
                                                        account.nameArabic = '';
                                                    arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                                }
                                                    
                                            });
                                        }
                                    }
                                    else if (root.formNames == 'BankReceipt') {
                                        if (costCenter.code == '120000') {
                                            costCenter.accounts.forEach(function (account) {
                                                if (account.isActive) {
                                                    if (account.name == null || account.name == undefined)
                                                        account.name = '';
                                                    if (account.nameArabic == null || account.nameArabic == undefined)
                                                        account.nameArabic = '';
                                                    arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                                }
                                            });
                                        }
                                    }
                                    else if (root.formNames == 'CashPay') {
                                        if (costCenter.code == '200000') {
                                            costCenter.accounts.forEach(function (account) {
                                                if (account.isActive) {
                                                    if (account.name == null || account.name == undefined)
                                                        account.name = '';
                                                    if (account.nameArabic == null || account.nameArabic == undefined)
                                                        account.nameArabic = '';
                                                    arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                                }
                                            });
                                        }
                                    }
                                    else if (root.formNames == 'BankPay') {
                                        if (costCenter.code == '200000') {
                                            costCenter.accounts.forEach(function (account) {
                                                if (account.isActive) {
                                                    if (account.name == null || account.name == undefined)
                                                        account.name = '';
                                                    if (account.nameArabic == null || account.nameArabic == undefined)
                                                        account.nameArabic = '';
                                                    arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                                } });
                                        }
                                    }
                                    else if (root.formName == 'JournalVoucher') {
                                        if (costCenter.code != '111000' && costCenter.code != '600001') {
                                            costCenter.accounts.forEach(function (account) {
                                                if (account.isActive) {
                                                    if (account.name == null || account.name == undefined)
                                                        account.name = '';
                                                    if (account.nameArabic == null || account.nameArabic == undefined)
                                                        account.nameArabic = '';
                                                    arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                                }  });
                                        }
                                    }
                                    else if (root.formName == 'PettyCashAccount') {

                                        if (costCenter.code != '111000' && costCenter.code != '600001' && costCenter.code != '420000') {
                                            costCenter.accounts.forEach(function (account) {
                                                if (account.isActive) {
                                                    if (account.name == null || account.name == undefined)
                                                        account.name = '';
                                                    if (account.nameArabic == null || account.nameArabic == undefined)
                                                        account.nameArabic = '';
                                                    arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                                } });
                                        }
                                    }
                                    else {
                                        costCenter.accounts.forEach(function (account) {
                                            if (account.isActive) {
                                                if (account.name == null || account.name == undefined)
                                                    account.name = '';
                                                if (account.nameArabic == null || account.nameArabic == undefined)
                                                    account.nameArabic = '';
                                                arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                            } });
                                    }

                                }
                            });

                        }
                        root.options.push({
                            AccountType: accountType.name,
                            libs: arr
                        });
                    });
                });

            this.GetAccount();

            if (this.accounts != undefined) {
                this.options = this.accounts;
            }

            this.selectValue = this.accountsvalue;


        },

        validations: {
            newAccount: {
                code: {
                    required: required,
                    //    isExist: function (value) {
                    //
                    //        if (value === '' || value === this.oldCode) return true;
                    //        var root = this;
                    //        var token = '';
                    //        if (this.$session.exists()) {
                    //            token = localStorage.getItem('token');
                    //        }

                    //        return new Promise(function (resolve, reject) {

                    //            resolve(
                    //                root.$https.get('/Accounting/IsAccountExist?code=' + value, { headers: { "Authorization": `Bearer ${token}` } })
                    //                    .then(function (response) {
                    //                        if (response.data.value) {
                    //                            console.log("isExist");
                    //                            return false;
                    //                        } else {
                    //                            console.log("Not isExist");
                    //                            return true;
                    //                        }
                    //                    },
                    //                        function () {
                    //                            console.log(" not isExist");
                    //                            return true;
                    //                        })
                    //            );

                    //            reject(console.log("ddd"));

                    //        });
                    //    }
                },
                name: {
                    required: required
                }
            }
        },

        methods: {
            GetCostCenterCode: function (x) {

                if (this.CoaCode) {
                    var root = this;
                    var token = '';
                    if (this.$session.exists()) {
                        token = localStorage.getItem('token');
                    }
                    this.$https.get('/Accounting/AccountCode?Id=' + x, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                        if (response.data != null) {
                            root.newAccount.code = response.data;
                            root.render++;
                        }
                    });
                }
            },
            AfterAddReturnList: function () {


                this.language = this.$i18n.locale;
                this.dropdownAccount = this.dropdownaccount;
                this.isopen = true;

                this.selectValue = this.value;
                var root = this;
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                var company = this.companyId;
                if (company == undefined) {
                    company = '';
                }
                root.$https.get('/Accounting/GetCOA?isDropdown=true' + '&companyId=' + company, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {

                        root.Accounts = response.data;


                        root.$store.dispatch('GetAccountList', root.Accounts);
                        root.Accounts = root.$store.state.accounts

                        root.Accounts.accountTypes.forEach(function (accountType) {
                            var arr = [];

                            if (root.headerName != undefined) {
                                
                                if (root.headerName == accountType.name) {
                                    accountType.costCenters.forEach(function (costCenter) {
                                        costCenter.accounts.forEach(function (account) {
                                            if (account.isActive) {
                                                if (account.name == null || account.name == undefined)
                                                    account.name = '';
                                                if (account.nameArabic == null || account.nameArabic == undefined)
                                                    account.nameArabic = '';
                                                arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                            } });
                                    });
                                }
                            }
                            else {
                                accountType.costCenters.forEach(function (costCenter) {
                                    
                                    if (root.formName == 'CashReceipt') {
                                        if (costCenter.code == '101000') {
                                            costCenter.accounts.forEach(function (account) {
                                                if (account.isActive) {
                                                    if (account.name == null || account.name == undefined)
                                                        account.name = '';
                                                    if (account.nameArabic == null || account.nameArabic == undefined)
                                                        account.nameArabic = '';
                                                    arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                                } });
                                        }
                                    }
                                   
                                    else if (root.formName == 'ForExpense') {
                                        if (costCenter.code == '101000' || costCenter.code == '105000') {
                                            costCenter.accounts.forEach(function (account) {
                                                if (account.isActive) {
                                                    if (account.name == null || account.name == undefined)
                                                        account.name = '';
                                                    if (account.nameArabic == null || account.nameArabic == undefined)
                                                        account.nameArabic = '';
                                                    arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                                }
                                            });
                                        }
                                    }
                                    else if (root.formName == 'Customer') {

                                        if (costCenter.code == '120000') {
                                            costCenter.accounts.forEach(function (account) {
                                                if (account.isActive) {
                                                    if (account.name == null || account.name == undefined)
                                                        account.name = '';
                                                    if (account.nameArabic == null || account.nameArabic == undefined)
                                                        account.nameArabic = '';
                                                    arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                                }});
                                        }
                                    }
                                    else if (root.formName == 'Supplier') {
                                        if (costCenter.code == '200000') {
                                            costCenter.accounts.forEach(function (account) {
                                                if (account.isActive) {
                                                    if (account.name == null || account.name == undefined)
                                                        account.name = '';
                                                    if (account.nameArabic == null || account.nameArabic == undefined)
                                                        account.nameArabic = '';
                                                    arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                                }});
                                        }
                                    }
                                    else if (root.formName == 'OpeningCash') {
                                        if (costCenter.code != '111000' && costCenter.code != '600001') {
                                            costCenter.accounts.forEach(function (account) {
                                                if (account.isActive) {
                                                    if (account.name == null || account.name == undefined)
                                                        account.name = '';
                                                    if (account.nameArabic == null || account.nameArabic == undefined)
                                                        account.nameArabic = '';
                                                    arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                                } });
                                        }
                                    }

                                    else if (root.formName == 'PettyCash') {
                                        if (costCenter.code == '101000') {
                                            costCenter.accounts.forEach(function (account) {
                                                if (account.isActive) {
                                                    if (account.name == null || account.name == undefined)
                                                        account.name = '';
                                                    if (account.nameArabic == null || account.nameArabic == undefined)
                                                        account.nameArabic = '';
                                                    arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                                } });
                                        }
                                    }
                                    else if (root.formName == 'BankReceipt') {

                                        if (costCenter.code == '105000') {
                                            costCenter.accounts.forEach(function (account) {
                                                if (account.isActive) {
                                                    if (account.name == null || account.name == undefined)
                                                        account.name = '';
                                                    if (account.nameArabic == null || account.nameArabic == undefined)
                                                        account.nameArabic = '';
                                                    arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                                } });
                                        }
                                    }
                                    else if (root.formNames == 'AdvanceExpense' && root.isVat) {
                                        if (costCenter.code == '130000') {
                                            costCenter.accounts.forEach(function (account) {
                                                if (account.isActive) {
                                                    if (account.name == null || account.name == undefined)
                                                        account.name = '';
                                                    if (account.nameArabic == null || account.nameArabic == undefined)
                                                        account.nameArabic = '';
                                                    arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                                } });
                                        }
                                    }
                                    else if (root.formNames == 'AdvanceExpense') {
                                        if (costCenter.code == '220000' || costCenter.code == '605001') {
                                            costCenter.accounts.forEach(function (account) {
                                                if (account.isActive) {
                                                    if (account.name == null || account.name == undefined)
                                                        account.name = '';
                                                    if (account.nameArabic == null || account.nameArabic == undefined)
                                                        account.nameArabic = '';
                                                    arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                                } });
                                        }

                                    }
                                    else if (root.formName == 'Advance') {

                                        if (costCenter.code == '609000') {
                                            costCenter.accounts.forEach(function (account) {
                                                if (account.isActive) {
                                                    if (account.name == null || account.name == undefined)
                                                        account.name = '';
                                                    if (account.nameArabic == null || account.nameArabic == undefined)
                                                        account.nameArabic = '';
                                                    arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                                } });
                                        }
                                    }
                                    else if (root.formName == 'CashPay') {

                                        if (root.advance ? costCenter.code == '101000' : root.isPurchase ? (costCenter.code == '101000' || costCenter.code == '160000') : (costCenter.code == '101000' || costCenter.code == '210000')) {
                                            costCenter.accounts.forEach(function (account) {
                                                if (account.isActive) {
                                                    if (account.name == null || account.name == undefined)
                                                        account.name = '';
                                                    if (account.nameArabic == null || account.nameArabic == undefined)
                                                        account.nameArabic = '';
                                                    arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                                } });
                                        }
                                    }
                                    else if (root.formName == 'BankPay') {

                                        if (root.advance ? costCenter.code == '105000' : root.isPurchase ? (costCenter.code == '105000' || costCenter.code == '160000') : (costCenter.code == '105000' || costCenter.code == '210000')) {
                                            costCenter.accounts.forEach(function (account) {
                                                if (account.isActive) {
                                                    if (account.name == null || account.name == undefined)
                                                        account.name = '';
                                                    if (account.nameArabic == null || account.nameArabic == undefined)
                                                        account.nameArabic = '';
                                                    arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                                } });
                                        }
                                    }
                                    else if (root.formName == 'AdvanceReceipt') {

                                        if (costCenter.code == '210000') {
                                            costCenter.accounts.forEach(function (account) {
                                                if (account.isActive) {
                                                    if (account.name == null || account.name == undefined)
                                                        account.name = '';
                                                    if (account.nameArabic == null || account.nameArabic == undefined)
                                                        account.nameArabic = '';
                                                    arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                                } });
                                        }
                                    }
                                    else if (root.formName == 'AdvancePay') {

                                        if (costCenter.code == '160000') {

                                            costCenter.accounts.forEach(function (account) {
                                                if (account.isActive) {
                                                    if (account.name == null || account.name == undefined)
                                                        account.name = '';
                                                    if (account.nameArabic == null || account.nameArabic == undefined)
                                                        account.nameArabic = '';
                                                    arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                                }  });
                                        }
                                    }
                                    else if (root.formName == 'ContractorAdvancePay') {

                                        if (costCenter.code == '160000') {

                                            costCenter.accounts.forEach(function (account) {
                                                if (account.isActive) {
                                                    if (account.name == null || account.name == undefined)
                                                        account.name = '';
                                                    if (account.nameArabic == null || account.nameArabic == undefined)
                                                        account.nameArabic = '';
                                                    arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                                } });
                                        }
                                    }



                                    else {


                                        if (root.formNames == 'CashReceipt') {
                                            if (costCenter.code == '120000') {
                                                costCenter.accounts.forEach(function (account) {
                                                    if (account.isActive) {
                                                        if (account.name == null || account.name == undefined)
                                                            account.name = '';
                                                        if (account.nameArabic == null || account.nameArabic == undefined)
                                                            account.nameArabic = '';
                                                        arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                                    } });
                                            }
                                        }
                                        else if (root.formNames == 'Expense') {
                                            if (costCenter.code == '605050' || costCenter.code == '606001' || costCenter.code == '605001' || costCenter.code == '609000') {
                                                costCenter.accounts.forEach(function (account) {
                                                    if (account.isActive) {
                                                        if (account.name == null || account.name == undefined)
                                                            account.name = '';
                                                        if (account.nameArabic == null || account.nameArabic == undefined)
                                                            account.nameArabic = '';
                                                        arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                                    }
                                                });
                                            }
                                        }
                                        else if (root.formNames == 'Expense') {
                                            if (costCenter.code == '605050' || costCenter.code == '606001' || costCenter.code == '605001' || costCenter.code == '609000') {
                                                costCenter.accounts.forEach(function (account) {
                                                    if (account.isActive) {
                                                        if (account.name == null || account.name == undefined)
                                                            account.name = '';
                                                        if (account.nameArabic == null || account.nameArabic == undefined)
                                                            account.nameArabic = '';
                                                        arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                                    }
                                                });
                                            }
                                        }
                                        else if (root.formNames == 'BankReceipt') {
                                            if (costCenter.code == '120000') {
                                                costCenter.accounts.forEach(function (account) {
                                                    if (account.isActive) {
                                                        if (account.name == null || account.name == undefined)
                                                            account.name = '';
                                                        if (account.nameArabic == null || account.nameArabic == undefined)
                                                            account.nameArabic = '';
                                                        arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                                    } });
                                            }
                                        }
                                        else if (root.formNames == 'CashPay') {
                                            if (costCenter.code == '200000') {
                                                costCenter.accounts.forEach(function (account) {
                                                    if (account.isActive) {
                                                        if (account.name == null || account.name == undefined)
                                                            account.name = '';
                                                        if (account.nameArabic == null || account.nameArabic == undefined)
                                                            account.nameArabic = '';
                                                        arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                                    } });
                                            }
                                        }
                                        else if (root.formNames == 'BankPay') {
                                            if (costCenter.code == '200000') {
                                                costCenter.accounts.forEach(function (account) {
                                                    if (account.isActive) {
                                                        if (account.name == null || account.name == undefined)
                                                            account.name = '';
                                                        if (account.nameArabic == null || account.nameArabic == undefined)
                                                            account.nameArabic = '';
                                                        arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                                    } });
                                            }
                                        }
                                        else if (root.formName == 'JournalVoucher') {
                                            if (costCenter.code != '111000' && costCenter.code != '600001') {
                                                costCenter.accounts.forEach(function (account) {
                                                    if (account.isActive) {
                                                        if (account.name == null || account.name == undefined)
                                                            account.name = '';
                                                        if (account.nameArabic == null || account.nameArabic == undefined)
                                                            account.nameArabic = '';
                                                        arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                                    } });
                                            }
                                        }
                                        else if (root.formName == 'PettyCashAccount') {

                                            if (costCenter.code != '111000' && costCenter.code != '600001' && costCenter.code != '420000') {
                                                costCenter.accounts.forEach(function (account) {
                                                    if (account.isActive) {
                                                        if (account.name == null || account.name == undefined)
                                                            account.name = '';
                                                        if (account.nameArabic == null || account.nameArabic == undefined)
                                                            account.nameArabic = '';
                                                        arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                                    } });
                                            }
                                        }
                                        else {
                                            costCenter.accounts.forEach(function (account) {
                                                if (account.isActive) {
                                                    if (account.name == null || account.name == undefined)
                                                        account.name = '';
                                                    if (account.nameArabic == null || account.nameArabic == undefined)
                                                        account.nameArabic = '';
                                                    arr.push({ name: root.language == 'en' ? ((account.name != '' && account.name != null) ? account.code + ' ' + account.name : account.code + ' ' + account.nameArabic) : (account.nameArabic != '' && account.nameArabic != null) ? account.code + ' ' + account.nameArabic : account.code + ' ' + account.name, id: account.id });


                                                }});
                                        }

                                    }
                                });

                            }
                            root.options.push({
                                AccountType: accountType.name,
                                libs: arr
                            });
                        });
                    });

                this.GetAccount();

                if (this.accounts != undefined) {
                    this.options = this.accounts;
                }

                this.selectValue = this.accountsvalue;
            },

            GetAccount: function () {
                var root = this;
                //working with IE and Chrome both
                //const config = {
                //    headers: { Pragma: 'no-cache' },
                //    params: { id: this.selectValue }
                //}
                if (this.selectValue.length != 0) {
                    var token = '';
                    if (this.$session.exists()) {
                        token = localStorage.getItem('token');
                    }
                    var company = this.companyId;
                    if (company == undefined) {
                        company = '';
                    }
                    root.$https.get('/Accounting/GetAccount?id=' + this.selectValue + '&companyId=' + company, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(function (response) {
                            if (response.data) {

                                root.selectValue = { name: root.language == 'en' ? ((response.data.name != null && response.data.name != '') ? response.data.code + ' : ' + response.data.name : response.data.code + ' : ' + response.data.nameArabic) : ((response.data.nameArabic != null && response.data.nameArabic != '') ? response.data.code + ' : ' + response.data.nameArabic : response.data.code + ' : ' + response.data.name), id: root.selectValue };
                            }
                        });
                }
            },
            addModal: function (type) {

                this.CoaCode = localStorage.getItem('coaCode') == 'true' ? true : false;
                this.$v.$reset();

                this.showModal = !this.showModal;
                this.type = type;

                this.newAccount = {
                    id: '',
                    code: '',
                    costCenterId: "00000000-0000-0000-0000-000000000000",
                    name: '',
                    isActive: true,
                    description: ''
                };

            },

            addAccount: function (account) {
                account.id = "00000000-0000-0000-0000-000000000000";
                var root = this;
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }

                root.$https.post('/Accounting/AddAccount', account, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {

                        if (response.data.value === true) {


                            root.Accounts.accountTypes.find(function (x) {
                                return x.id === root.accountTypeId;
                            }).costCenters.find(function (y) {
                                return y.id === root.newAccount.costCenterId;
                            }).accounts.push({
                                costCenterId: root.newAccount.costCenterId,
                                code: root.newAccount.code,
                                description: root.newAccount.description,
                                id: response.data.id,
                                isActive: root.newAccount.isActive,
                                name: root.newAccount.name
                            });
                            root.$swal({
                                icon: 'success',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully!' : 'حفظ بنجاح',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });

                            //root.options.filter(function (e) { return e.AccountType === 'Assets'; })[0]
                            //    .libs
                            //    .push({ name: root.newAccount.name, id: response.data.id });

                            root.showModal = !root.showModal;
                            root.AfterAddReturnList();


                        } else {

                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: 'Account does not add.',
                                type: 'error',
                                confirmButtonClass: "btn btn-info",
                                buttonsStyling: false
                            });

                        }
                    },
                        function (error) {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: error,
                                type: 'error',
                                confirmButtonClass: "btn btn-info",
                                buttonsStyling: false
                            });
                        });
            },
            close: function () {
                this.showModal = !this.showModal;
            }
        },
        watch:
        {
            account: function (a) {
                //compatibilty with chrome
                this.oldCode = a.code;
            },
            accountTypeId: function (newTypeId) {
                this.costCenters = this.Accounts.accountTypes.find(function (x) { return x.id == newTypeId; }).costCenters;
            }
        }

    }

</script>
