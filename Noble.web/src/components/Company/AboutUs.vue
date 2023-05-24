<template>
    <div class="row">

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('AboutUs.AboutUs') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item">
                                        <a href="javascript:void(0);">
                                            {{
                                        $t('AboutUs.Home')
                                            }}
                                        </a>
                                    </li>
                                    <li class="breadcrumb-item active">{{ $t('AboutUs.AboutUs') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('AboutUs.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                    <div class="form-group">
                                        <h4 style="padding:0; margin:0;">{{ $t('AboutUs.LicenseGroup') }}: </h4>
                                        <span>{{ aboutUsDetail.groupName }}</span>

                                    </div>
                                </div>

                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-12">
                                    <div class="form-group">
                                        <h4>{{ $t('AboutUs.ModuleList') }}: </h4>
                                    </div>
                                </div>
                                <div class="col-4" v-for="(module, index) in aboutUsDetail.moduleList" :key="index">
                                    <div class="form-group">
                                        <p style="font-size:0.8rem;">{{ module }}</p>
                                    </div>
                                </div>

                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-12">
                                    <div class="form-group">
                                        <h4>
                                            {{ $t('AboutUs.LicenseType') }}: <span style="font-size:16px !important; font-weight:400;">
                                                {{
                                                    aboutUsDetail.licenseType
                                                }}
                                            </span>
                                        </h4>

                                    </div>
                                </div>
                                <div class="col-4"
                                     v-if="!aboutUsDetail.isTechnicalSupport && aboutUsDetail.licenseType != 'Unlimited'">
                                    <div class="form-group">
                                        <p style="padding: 0; margin: 0; font-size: 16px !important; font-weight:500;">
                                            {{ $t('AboutUs.FromDate') }}: <span style="font-size:14px !important; ">
                                                {{
                                                aboutUsDetail.fromDate
                                                }}
                                            </span>
                                        </p>
                                    </div>
                                </div>
                                <div class="col-4"
                                     v-else-if="!aboutUsDetail.isTechnicalSupport && aboutUsDetail.licenseType === 'Unlimited' && !aboutUsDetail.isActive">
                                    <div class="form-group">
                                        <p style="padding: 0; margin: 0; font-size: 16px !important; font-weight:500;">
                                            {{ $t('AboutUs.FromDate') }}: <span style="font-size:14px !important; ">
                                                {{
                                                aboutUsDetail.fromDate
                                                }}
                                            </span>
                                        </p>
                                    </div>
                                </div>
                                <div class="col-4"
                                     v-if="!aboutUsDetail.isTechnicalSupport && aboutUsDetail.licenseType != 'Unlimited'">
                                    <div class="form-group">
                                        <p style="padding: 0; margin: 0; font-size: 16px !important; font-weight:500;">
                                            {{ $t('AboutUs.ToDate') }}: <span style="font-size:14px !important; ">
                                                {{
                                                aboutUsDetail.toDate
                                                }}
                                            </span>
                                        </p>
                                    </div>
                                </div>
                                <div class="col-4"
                                     v-if="!aboutUsDetail.isTechnicalSupport && aboutUsDetail.licenseType === 'Unlimited' && !aboutUsDetail.isActive">
                                    <div class="form-group">
                                        <p style="padding: 0; margin: 0; font-size: 16px !important; font-weight:500;">
                                            {{ $t('AboutUs.ToDate') }}: <span style="font-size:14px !important; ">
                                                {{
                                                aboutUsDetail.toDate
                                                }}
                                            </span>
                                        </p>
                                    </div>
                                </div>

                                <div class="col-4">
                                    <div class="form-group"
                                         v-if="aboutUsDetail.licenseType == 'Subscription' && aboutUsDetail.isActive">
                                        <p style="padding: 0; margin: 0; font-size: 16px !important; font-weight:500;">
                                            {{ $t('AboutUs.PaymentFrequency') }}: <span style="font-size:14px !important; ">
                                                {{
                                                    aboutUsDetail.paymentFrequency
                                                }}
                                            </span>
                                        </p>
                                    </div>
                                </div>
                            </div>
                            <div class="row" v-if="aboutUsDetail.isTechnicalSupport">
                                <div class="col-12">
                                    <div class="form-group">
                                        <h4 style="padding:0 ; margin:0;">
                                            Technical Support: <span style="font-size:16px !important; font-weight:500;">
                                                {{
                                                    aboutUsDetail.supportType
                                                }}
                                            </span>
                                        </h4>

                                    </div>
                                </div>
                                <div class="col-4" v-if="aboutUsDetail.supportType != 'UnLimited'">
                                    <div class="form-group">
                                        <p style="padding: 0 ; margin: 0; font-size: 16px !important; font-weight: 500;">
                                            {{ $t('AboutUs.FromDate') }}: <span style="font-size:14px !important; ">
                                                {{
                                                aboutUsDetail.fromDate
                                                }}
                                            </span>
                                        </p>
                                    </div>
                                </div>
                                <div class="col-4" v-if="aboutUsDetail.supportType != 'UnLimited'">
                                    <div class="form-group">
                                        <p style="padding: 0 ; margin: 0; font-size: 16px !important; font-weight:500;">
                                            {{ $t('AboutUs.ToDate') }}: <span style="font-size:14px !important; ">
                                                {{
                                                aboutUsDetail.toDate
                                                }}
                                            </span>
                                        </p>
                                    </div>
                                </div>
                                <div class="col-4" v-if="aboutUsDetail.supportType != 'UnLimited'">
                                    <div class="form-group">
                                        <p style="padding: 0; margin: 0; font-size: 16px !important; font-weight:500;">
                                            {{ $t('AboutUs.PaymentFrequency') }}: <span style="font-size:14px !important; ">
                                                {{
                                                    aboutUsDetail.paymentFrequency
                                                }}
                                            </span>
                                        </p>
                                    </div>
                                </div>

                            </div>

                            <div class="row">
                                <div class="col-2">
                                    <a href="javascript:void(0)" v-on:click="UpdateQuantity()"
                                       style="color:transparent"> {{ $t('AboutUs.UpdateQuantity') }}</a>
                                </div>
                                <div class="col-2">
                                    <a href="javascript:void(0)" v-on:click="UpdatePurchase()"
                                       style="color:transparent">{{ $t('AboutUs.UpdatePurchase') }}</a>
                                </div>
                                <div class="col-2">
                                    <a href="javascript:void(0)" v-on:click="UpdateSale()" style="color:transparent">
                                        {{ $t('AboutUs.UpdateSale') }}
                                    </a>
                                </div>
                                <div class="col-2">
                                    <a href="javascript:void(0)" v-on:click="OneTimeStockQuantity()" style="color:transparent">
                                        Stock Entry
                                    </a>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <a class="btn btn-outline-primary" v-on:click="CreateProductAccount()">
                                        {{ $t('AboutUs.CreateProductAccount') }}
                                    </a>
                                </div>
                                <div class="col-md-3">
                                    <a class="btn btn-outline-primary" v-on:click="CreataLedgerTransaction()">
                                        Creata Ledger Transaction
                                    </a>
                                </div>
                                <!-- <div class="col-md-3">
                                    <a class="btn btn-outline-primary" v-on:click="SumOfTransaction('Sum')">
                                        Sum Of Transaction
                                    </a>
                                </div> -->
                            </div>
                            <div class="row mt-2">

                                <div class="col-md-3 ">
                                    <a class="btn btn-outline-primary" v-on:click="ContactDisplayName(true,false,false)">
                                        Change Display Name
                                    </a>
                                </div>
                                <div class="col-md-3 ">
                                    <a class="btn btn-outline-primary" v-on:click="ContactDisplayName(false, true,false)">
                                        Cash Customer To Contacts
                                    </a>
                                </div>
                                <div class="col-md-3 ">
                                    <a class="btn btn-outline-primary" v-on:click="ContactDisplayName(false,false,true)">
                                        Change Invoice Id to Contact Id
                                    </a>
                                </div>
                                <div class="col-md-3 ">
                                    <a class="btn btn-outline-primary" v-on:click="ContactDisplayName(false,false,true,'Invoice')">
                                        Document Type Invoice
                                    </a>
                                </div>
                                <div class="col-md-3 mt-2">
                                    <a class="btn btn-outline-primary" v-on:click="UpdateInvoiceTotal()">
                                        Update Invoice Total
                                    </a>
                                </div>

                                <div class="col-md-3 mt-2">
                                    <a class="btn btn-outline-primary" v-on:click="UpdateSaleOrderTotal()">
                                        Update SaleOrder Total
                                    </a>
                                </div>


                                <div class="col-md-3 mt-2">
                                    <a class="btn btn-outline-primary" v-on:click="UpdatePurchaseInvoiceTotal()">
                                        Update Purchase Invoice Total
                                    </a>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <brandmodel :brand="newBrand" :show="show" v-if="show" @close="IsSave" :type="type" />
            </div>
            <loading :active.sync="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>
    </div>


</template>



<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/vue-loading.css';
    export default {
        mixins: [clickMixin],
        components: {
            Loading
        },
        data: function () {
            return {
                aboutUsDetail: '',
                productAccount: false,
                loading: false,
            }
        },

        methods: {
            close: function () {
                this.$emit('close');
            },
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },

            UpdateInvoiceTotal: function () {
                var root = this;
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                root.loading = true;
                root.$https.get('Sale/CalculateSaleInvoice', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data) {
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },

            UpdateSaleOrderTotal: function () {
                var root = this;
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                root.loading = true;
                root.$https.get('Sale/CalculateSaleOrder', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data) {
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },

            UpdatePurchaseInvoiceTotal: function () {
                var root = this;
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                root.loading = true;
                root.$https.get('Sale/CalculatePurchaseInvoice', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data) {
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },

            UpdateQuantity: function () {
                var root = this;
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Sale/UpdateQuantity', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data) {
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },
            OneTimeStockQuantity: function () {
                var root = this;
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Sale/OneTimeStockQuantity', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data) {
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },
            UpdatePurchase: function () {
                var root = this;
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Sale/RemoveDuplicateTransactions?purchaseNumber=' + localStorage.getItem('purchaseNumber'), { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data) {
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },
            UpdateSale: function () {
                var root = this;
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Sale/TrialBalanceSetting?saleNumber=' + localStorage.getItem('saleNumber'), { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data) {
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },

            GetAboutUsDetail: function () {
                var root = this;
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Company/AboutUs', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.isSuccess) {
                        root.aboutUsDetail = response.data.message
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },
            CreateProductAccount: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Product/CreateProductAccount?productAccount=' + true, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isAddUpdate != null) {
                            root.$swal({
                                title: "Saved!",
                                text: "Data has been added successfully",
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
                                title: "Error!",
                                text: "Something Went Wrong!",
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                    }).catch(error => {
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: 'Something Went Wrong!',
                                text: error.response.data,

                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false);
            },
            CreataLedgerTransaction: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Product/CreateProductAccount?productAccount=' + this.productAccount, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isAddUpdate != null) {
                            root.$swal({
                                title: "Saved!",
                                text: "Data has been added successfully",
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
                                title: "Error!",
                                text: "Your Color Name  Already Exist!",
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: 'Something Went Wrong!',
                                text: error.response.data,

                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false);
            },
            SumOfTransaction: function (val) {
                var root = this;
                this.loading = true;
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Product/CreateProductAccount?sum=' + val, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isAddUpdate != null) {
                            root.$swal({
                                title: "Saved!",
                                text: "Data has been added successfully",
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
                                title: "Error!",
                                text: "Your Color Name  Already Exist!",
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: 'Something Went Wrong!',
                                text: error.response.data,

                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false);
            },

            ContactDisplayName: function (val, val2, val3,documentType) {
                debugger;
                var root = this;
                this.loading = true;
                var token = '';
                debugger;
                if (this.$session.exists()) {
                    debugger;
                    token = localStorage.getItem('token');
                }
                root.$https.get('Contact/ChangeCashCustomerToContact?changeDisplayName=' + val + '&cashCustomerToContact=' + val2 + '&invoiceChange=' + val3+ '&documentType=' + documentType, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isAddUpdate != null) {
                            root.$swal({
                                title: "Saved!",
                                text: "Data has been added successfully",
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
                                title: "Error!",
                                text: "Something Went Wrong!",
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                    }).catch(error => {
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: 'Something Went Wrong!',
                                text: error.response.data,

                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false);
            },
        },

        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.GetAboutUsDetail();

        }
    }
</script>