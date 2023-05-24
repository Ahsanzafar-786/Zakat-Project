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
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Customer.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('Customer.Customer') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                
                                <a v-if="isValid('CanAddCustomer') && ((rowCount < limitedCustomer) || (limitedCustomer == 0))" v-on:click="AddCustomer" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('Customer.AddNew') }}
                                </a>
                                <a v-on:click="ImportDataFromXlsx" href="javascript:void(0);" v-if="isValid('CanImportCustomer') && ((rowCount < limitedCustomer) || (limitedCustomer == 0))"
                                   class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{$t('Customer.ImportCustomer')}}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('Customer.Close') }}
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
                    <div v-if="isCashCustomer">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item"><a class="nav-link" v-bind:class="{active:active == false}" v-on:click="makeActive(false)" data-bs-toggle="tab" href="#link6" role="tab" aria-selected="true">Cash/Credit</a></li>
                            <li class="nav-item"><a class="nav-link" v-bind:class="{active:active == true}" v-on:click="makeActive(true)" data-bs-toggle="tab" href="#link5" role="tab" aria-selected="false">Walk-In</a></li>
                        </ul>
                    </div>
                    <div class="table-responsive" v-if="active == false">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>#</th>
                                    <th>{{ $t('Customer.Code') }} </th>
                                    <th v-if="english=='true'">{{ $t('Customer.CustomerName(English)') |englishLanguage}} </th>
                                    <th v-if="isOtherLang()">{{ $t('Customer.CustomerName(Arabic)') |arabicLanguage}}</th>
                                    <th v-if="isOtherLang()">{{ $t('Customer.CompanyName')}}</th>
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
                                    <td v-if="isValid('CanEditCustomer')">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditCustomer(cust.id)">{{cust.code}}</a>
                                        </strong>
                                    </td>
                                    <td v-else>
                                        {{cust.code}}
                                    </td>
                                    <td v-if="english=='true'">{{cust.englishName}}</td>
                                    <td v-if="isOtherLang()">{{cust.arabicName}}</td>
                                    <td>{{cust.customerDisplayName}}</td>
                                    <td>{{cust.commercialRegistrationNo}}</td>
                                    <td>{{cust.registrationDate}}</td>
                                    <td>{{cust.category}}</td>
                                    <td class="text-center">{{cust.openingBalance>=0?'Dr '+ cust.openingBalance*+1: 'Cr '+ cust.openingBalance*(-1) }}</td>
                                    <td>{{cust.paymentTerms}}</td>
                                    <td>
                                        <span v-if="cust.isActive" class="badge badge-boxed  badge-outline-success">{{$t('Customer.Active')}}</span>
                                        <span v-else class="badge badge-boxed  badge-outline-danger">{{$t('Customer.De-Active')}}</span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="table-responsive" v-if="active == true">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>#</th>
                                    <th>{{ $t('Customer.Code') }} </th>
                                    <th v-if="english=='true'">{{ $t('Customer.CustomerName(English)') |englishLanguage}} </th>
                                    <th v-if="isOtherLang()">{{ $t('Customer.CustomerName(Arabic)') |arabicLanguage}}</th>
                                    <th v-if="isOtherLang()">{{ $t('Customer.CompanyName')}}</th>
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
                                    <td v-if="isValid('CanEditCustomer')">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditCustomer(cust.id)">{{cust.code}}</a>
                                        </strong>
                                    </td>
                                    <td v-else>
                                        {{cust.code}}
                                    </td>
                                    <td v-if="english=='true'">{{cust.englishName}}</td>
                                    <td v-if="isOtherLang()">{{cust.arabicName}}</td>
                                    <td>{{cust.customerDisplayName}}</td>
                                    <td>{{cust.commercialRegistrationNo}}</td>
                                    <td>{{cust.registrationDate}}</td>
                                    <td>{{cust.category}}</td>
                                    <td class="text-center">{{cust.openingBalance>=0?'Dr '+ cust.openingBalance*+1: 'Cr '+ cust.openingBalance*(-1) }}</td>
                                    <td>{{cust.paymentTerms}}</td>
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
                isCashCustomer: false,
                multipleAddress: false,
                active: false,

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
                this.GetCustomerData(val, 1,this.active);
            }
        },
        methods: {
            makeActive: function (item) {

            this.active = item;
            this.GetCustomerData(this.search, 1, item);
        },
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            ImportDataFromXlsx: function () {
                var root = this;
                root.$router.push({
                    path: '/ImportExportRecords',
                    query: { data: 'Customer' }
                })
            },
            AddCustomer: function () {
                this.$router.push('/addCustomer')
            },

            EditCustomer: function (Id) {
                var root = this;
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                var multipleAddress = localStorage.getItem('MultipleAddress') == 'true' ? true : false;

                this.$https.get('/Contact/ContactDetail?Id=' + Id+ '&multipleAddress=' + multipleAddress, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
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
                this.GetCustomerData(this.search, this.currentPage,this.active);
            },

            GetCustomerData: function (search, currentPage,isCashCustomer) {
                var root = this;

                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Contact/ContactList?isCustomer=true' + '&searchTerm=' + search + '&pageNumber=' + currentPage + '&isCashCustomer=' + isCashCustomer, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
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
            

            this.isCashCustomer = localStorage.getItem('CashCustomer') == 'true' ? true : false;
            this.multipleAddress = localStorage.getItem('MultipleAddress') == 'true' ? true : false;



            this.GetCustomerData(this.search, 1,this.active);


        }
    }
</script>