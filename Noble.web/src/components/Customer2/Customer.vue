<template>
    <div class="row" v-if="isValid('CanViewCustomer')">


        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('Customer.Customer') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                    <li class="breadcrumb-item active">{{ $t('Customer.Customer') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="ImportDataFromXlsx" href="javascript:void(0);" v-if="isValid('CanImportCustomer') && ((rowCount < limitedCustomer) || (limitedCustomer == 0))"
                                   class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{$t('Customer.ImportCustomer')}}
                                </a>
                                <a v-if="isValid('CanAddCustomer') && ((rowCount < limitedCustomer) || (limitedCustomer == 0))" v-on:click="AddCustomer" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('Customer.AddNew') }}
                                </a>

                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('Bank.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-header">
                    <div class="input-group">
                        <button class="btn btn-secondary" type="button" id="button-addon1">
                            <i class="fas fa-search"></i>
                        </button>
                        <input v-model="search" type="text" class="form-control" :placeholder="$t('Customer.SearchbyBrand')"
                               aria-label="Example text with button addon" aria-describedby="button-addon1">
                    </div>
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>#</th>
                                    <th>{{ $t('Customer.Code') }} </th>
                                    <th v-if="english=='true'">{{ $t('Customer.CustomerName(English)') |englishLanguage}} </th>
                                    <th v-if="isOtherLang()">{{ $t('Customer.CustomerName(Arabic)') |arabicLanguage}}</th>
                                    <th>{{ $t('Customer.CRN') }}</th>
                                    <th>{{ $t('Customer.RegDate') }}</th>
                                    <th>{{ $t('Customer.Category') }}</th>
                                    <th class="text-center">{{ $t('Customer.RunningBalance') }}</th>
                                    <th>{{ $t('Customer.PaymentType') }}</th>

                                    <th>{{ $t('Customer.Status') }}</th>

                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(cust,index) in customerlist" v-bind:key="cust.id">
                                    <td>
                                        {{index+1}}
                                    </td>
                                    <td v-if="isValid('CanEditCustomer')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditCustomer(cust.id)">{{cust.code}}</a>
                                        </strong>
                                    </td>
                                    <td v-else v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                        {{cust.code}}
                                    </td>
                                    <td v-if="english=='true'" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">{{cust.englishName}}</td>
                                    <td v-if="isOtherLang()" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">{{cust.arabicName}}</td>
                                    <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">{{cust.commercialRegistrationNo}}</td>
                                    <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">{{cust.registrationDate}}</td>
                                    <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">{{cust.category}}</td>
                                    <td class="text-center">{{cust.openingBalance>=0?'Dr '+ cust.openingBalance*+1: 'Cr '+ cust.openingBalance*(-1) }}</td>
                                    <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">{{cust.paymentTerms}}</td>
                                    <td>
                                        <span v-if="cust.isActive" class="badge badge-boxed  badge-outline-success">{{$t('Customer.Active')}}</span>
                                        <span v-else class="badge badge-boxed  badge-outline-danger">{{$t('Customer.De-Active')}}</span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <hr />
                    <div class="float-start">
                        <span v-if="currentPage===1 && rowCount === 0">  {{ $t('Pagination.ShowingEntries') }}</span>
                        <span v-else-if="currentPage===1 && rowCount < 10">  {{ $t('Pagination.Showing') }} {{currentPage}}  {{ $t('Pagination.to') }} {{rowCount}}  {{ $t('Pagination.of') }} {{rowCount}}  {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage===1 && rowCount >= 11  "> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*10}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage===1"> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*10}} of {{rowCount}} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage!==1 && currentPage!==pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*10)-9}} {{ $t('Pagination.to') }} {{currentPage*10}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*10)-9}} {{ $t('Pagination.to') }} {{rowCount}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                    </div>
                    <div class="float-end">
                        <div class="" v-on:click="getPage()">
                            <b-pagination pills size="sm" v-model="currentPage"
                                              :total-rows="rowCount"
                                              :per-page="10"
                                              :first-text="$t('Table.First')"
                                              :prev-text="$t('Table.Previous')"
                                              :next-text="$t('Table.Next')"
                                              :last-text="$t('Table.Last')" ></b-pagination>
                        </div>
                    </div>

                </div>
            </div>

           
        </div>

    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        name: 'Customer',
        mixins: [clickMixin],
        data: function () {
            return {
                arabic: '',
                english: '',
                customerlist: [],
                search: '',
                customer: [],
                loading: true,
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                limitedCustomer: 0
            }
        },
        watch: {
            search: function (val) {
                this.GetCustomerData(val, 1);
            }
        },
        methods: {
            ImportDataFromXlsx: function () {
                var root = this;
                root.$router.push({
                    path: '/ImportExportRecords',
                    query: { data: 'Customer' }
                })
            },
            AddCustomer: function () {
                this.$router.push('/addCustomer2')
            },

            EditCustomer: function (Id) {
                var root = this;
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Contact/ContactDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.customer = response.data
                    }
                    root.$router.push({
                        path: '/addCustomer',
                        query: {
                            data: root.customer
                        }
                    })
                });
            },
            getPage: function () {
                this.GetCustomerData(this.search, this.currentPage);
            },

            GetCustomerData: function (search, currentPage) {
                var root = this;

                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Contact/ContactList?isCustomer=true' + '&searchTerm=' + search + '&pageNumber=' + currentPage, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {


                        // root.$store.state.customerlist.push(response.data.contacts)
                        root.customerlist = response.data.results;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;

                    }
                });
            },
            RemoveCustomer: function (id) {
                var root = this;
                // working with IE and Chrome both
                this.$swal({
                    icon: 'error',
                    title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Are you sure?' : 'هل أنت متأكد؟', 
                    text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'You will not be able to recover this!' : 'لن تتمكن من استرداد هذا!', 
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Yes, delete it!' : 'نعم ، احذفها!', 
                    closeOnConfirm: false,
                    closeOnCancel: false
                }).then(function (result) {
                    if (result) {
                        var token = '';
                        if (root.$session.exists()) {
                            token = localStorage.getItem('token');
                        }
                        root.$https
                            .get('/Contact/ContactDelete?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                            .then(function (response) {
                                if (response.data.id != '00000000-0000-0000-0000-000000000000') {
                                    root.$store.state.customerlist.splice(root.$store.state.customerlist.findIndex(function (i) {
                                        return i.id === response.data;
                                    }), 1);
                                    root.$swal({
                                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Deleted!' : 'تم الحذف!',
                                        text: response.data.isAddUpdate,
                                        type: 'success',
                                        confirmButtonClass: "btn btn-success",
                                        buttonsStyling: false
                                    });
                                } else {
                                    root.$swal({
                                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                        text: response.data.isAddUpdate,
                                        type: 'error',
                                        confirmButtonClass: "btn btn-danger",
                                        buttonsStyling: false
                                    });
                                }
                            },
                                function () {
                                    root.loading = false;
                                    root.$swal({
                                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                        text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                        type: 'error',
                                        confirmButtonClass: "btn btn-danger",
                                        buttonsStyling: false
                                    });
                                });
                    }
                    else {
                        this.$swal((this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Cancelled!' : 'ألغيت!', (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Your file is still intact!' : 'ملفك لا يزال سليما!', (this.$i18n.locale == 'en' || root.isLeftToRight()) ? 'info' : 'معلومات');
                    }
                });
            },
        },
        created: function () {
            
            this.$emit('input', this.$route.name);
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.limitedCustomer = parseInt(localStorage.getItem('LimitedCustomer'));
            if (isNaN(this.limitedCustomer)) {
                this.limitedCustomer = 0
            }
            this.GetCustomerData(this.search, 1);
            

        }
    }
</script>