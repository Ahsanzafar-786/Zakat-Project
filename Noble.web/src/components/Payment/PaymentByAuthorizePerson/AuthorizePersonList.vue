<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title"> {{ $t('Payment.AddPaymentbyAuthorizedPersonList') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Home') }}</a>
                                    </li>
                                    <li class="breadcrumb-item active">{{ $t('Payment.AddPaymentbyAuthorizedPersonList') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="GotoPage('/addpaymentauthoirizeperson')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary mx-1" >
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('Payment.AddPayment') }}
                                </a>
                                <a v-on:click="GotoPage('/dashboard')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col-md-4">
                           
                            <label class="text  font-weight-bolder">
                                {{ $t('Payment.SearchByCode') }}
                            </label>
                            <input v-model="code" type="text" class="form-control" :placeholder="$t('Payment.SearchByCode')"
                                aria-label="Example text with button addon" aria-describedby="button-addon1">
                        </div>
                        <div class="col-md-4">
                            <label class="text  font-weight-bolder">
                                {{ $t('Benificary.AuthorizedPerson') }}
                            </label>
                            <authorizedperson v-model="authorizationPersonId" ref="AuthorizedDropdown" />

                        </div>
                        <div class="col-md-4">
                            <label class="text  font-weight-bolder">
                                {{ $t('Payment.Amount') }}
                            </label>
                            <input v-model="amount" type="text" class="form-control"
                                :placeholder="$t('Payment.SearchByAmount')" aria-label="Example text with button addon"
                                aria-describedby="button-addon1">
                        </div>

                        <div class="col-md-12 mt-2">
                            <a class="btn btn btn-soft-primary" v-on:click="AdvanceFilterFor" id="button-addon2">
                                <i class="fa fa-filter"></i>
                            </a>
                        </div>
                    </div>
                    <div class="row" v-if="advanceFilters">
                       
                       
                        
                        <div class="col-md-3 form-group">
                            <label class="text  font-weight-bolder">{{ $t('Benificary.SelectMonth') }}</label>
                            <datepicker v-model="month" :type="'month'" :key="render" />

                        </div>
                        <div class="col-md-3 form-group">
                            <label>{{ $t('Benificary.SelectYear') }}</label>
                            <datepicker v-model="year" :type="'year'" :key="render" />
                        </div>
                        <div class="col-md-3 form-group">
                            <label class="text  font-weight-bolder">
                                {{ $t('Benificary.FromDate') }}
                            </label>
                            <datepicker v-model="fromDate" :key="render" />
                        </div>
                        <div class="col-md-3 form-group">
                            <label class="text  font-weight-bolder">
                                {{ $t('Benificary.ToDate') }}
                            </label>
                            <datepicker v-model="toDate" :key="render" />
                        </div>
                       

                      
                    </div>
                    <div class="row">
                        <div class="col-sm-4 mt-2">
                            <a v-on:click="SearchFilter" href="javascript:void(0);"
                                class="btn btn-sm btn-outline-primary mx-1">
                                {{ $t('Benificary.SearchFilter') }}
                            </a>
                            <a @click="ClearFilter" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                                {{ $t('Benificary.ClearFilter') }}
                            </a>

                        </div>
                    </div>

                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>#</th>
                                    <!-- <th class="text-center">
                                        ID
                                    </th> -->
                                    <th class="text-center">
                                        {{ $t('Payment.PaymentCode') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Payment.Date') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Payment.AuthoirzedPersonName') }}
                                        
                                    </th>
                                    <th class="text-center">
                                        {{ $t('AddPayment.Amount') }}
                                    </th>
                                   
                                    <th class="text-center">
                                        {{ $t('Payment.Action') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(brand, index) in paymentList" v-bind:key="brand.id">
                                    <td v-if="currentPage === 1">
                                        {{ index + 1 }}
                                    </td>
                                    <td v-else>
                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                    </td>

                                    <td class="text-center " >
                                        <strong>
                                            <a  href="javascript:void(0)">{{brand.code }}</a>
                                        </strong>
                                    </td>
                                    

                                    <td class="text-center">{{ GetDate(brand.dateTime) }}</td>

                                    <td class="text-center">{{ brand.authorizePersonNameAr }}</td>

                                  
                                  
                                  
                                    <td class="text-center" >{{ parseFloat(brand.amount).toFixed(3).slice(0,
                                        -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,") }}</td>

                                    <td class="text-center d-flex align-items-baseline justify-content-center"
                                        v-if="roleName != 'User'">
                                        <button type="button" class="btn btn-light dropdown-toggle"
                                            data-bs-toggle="dropdown" aria-expanded="false"> {{ $t('Payment.Action') }} <i
                                                class="mdi mdi-chevron-down"></i></button>
                                        <div class="dropdown-menu text-center">
                                            <a class="dropdown-item" href="javascript:void(0)"
                                                v-on:click="ViewPayment(brand.id)">{{
                                                    $t('Payment.ViewDetail') }}</a>
                                           
                                            
                                            <strong>
                                                <a href="javascript:void(0)" v-on:click="PrintRecord(brand.id,brand)"> {{
                                                    $t('Payment.Print') }}</a>
                                            </strong>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <hr />
                    <div class="float-start">
                        <span v-if="currentPage === 1 && rowCount === 0"> {{ $t('Pagination.ShowingEntries') }}</span>
                        <span v-else-if="currentPage === 1 && rowCount < 100">
                            {{ $t('Pagination.Showing') }}
                            {{ currentPage }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                            {{ rowCount }} {{ $t('Pagination.entries') }}
                        </span>
                        <span v-else-if="currentPage === 1 && rowCount >= 101">
                            {{ $t('Pagination.Showing') }}
                            {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 100 }} {{ $t('Pagination.of') }}
                            {{ rowCount }} {{ $t('Pagination.entries') }}
                        </span>
                        <span v-else-if="currentPage === 1">
                            {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                                $t('Pagination.to')
                            }} {{ currentPage * 100 }} of {{ rowCount }} {{ $t('Pagination.entries') }}
                        </span>
                        <span v-else-if="currentPage !== 1 && currentPage !== pageCount">
                            {{ $t('Pagination.Showing') }}
                            {{ (currentPage * 100) - 99 }} {{ $t('Pagination.to') }} {{ currentPage * 100 }} {{
                                $t('Pagination.of')
                            }} {{ rowCount }} {{ $t('Pagination.entries') }}
                        </span>
                        <span v-else-if="currentPage === pageCount">
                            {{ $t('Pagination.Showing') }}
                            {{ (currentPage * 100) - 99 }} {{ $t('Pagination.to') }} {{ rowCount }} {{
                                $t('Pagination.of')
                            }}
                            {{ rowCount }} {{ $t('Pagination.entries') }}
                        </span>
                    </div>
                    <div class="float-end">
                        <div class="" v-on:click="GetPayment()">
                            <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount" :per-page="100"
                                :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')"
                                :next-text="$t('Table.Next')" :last-text="$t('Table.Last')"></b-pagination>
                        </div>
                    </div>
                </div>
            </div>

            <authorizepaymentreport :documentName="'List'"  :headerFooter="headerFooter" :show="show"  :brandObj="brandObj" v-if="show" v-bind:key="changereport" :printDetails="paymentRecord"  />
            <paymentauthorizedetail :id="authorizeId" :show="show1" v-if="show1"  @close="RefreshList"
                @IsSave="IsSave" />
            <loading :active.sync="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>

    </div>
</template>


<script>
import clickMixin from '@/Mixins/clickMixin'
import 'vue-loading-overlay/dist/vue-loading.css';
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/vue-loading.css';
import moment from 'moment'
// import 'moment/locale/ar'; // Import Arabic locale

export default {
    mixins: [clickMixin],
    components: {
        Loading,
    },
    data: function () {
        return {
            advanceFilters: false,
            authorizationPersonId: '',
            brandObj: '',
            authorizeId: '',
            approvalPersonId: '',
            registered: '',
            fromDate: '',
            toDate: '',
            startMonth: '',
            month: '',
            year: '',
            render: 0,
            nationalId: '',
            nationality: '',
            gender: '',
            status: '',
            language:'',
            contact: '',
            uqamaNo: '',
            show: false,
            show1: false,
            reportsrc: '',
            changereport: 0,
            type: '',
            search: '',
            paymentList: [],
            paymentRecord: [],
            currentPage: 1,
            pageCount: '',
            rowCount: '0',
            arabic: '',
            english: '',
            user: '',
            isVoid: false,
            roleName: '',
            benificaryName: '',
            code: '',
            amount: '',
            benificaryCode: '',
            loading: false,
            headerFooter: {
                    company: ''
                },


        }
    },
    // watch: {

    //     search: function (val) {
    //         debugger; //eslint-disable-line
    //         this.GetPayment(val, 1, '', 0, 0);
    //     }
    // },
    methods: {
        ClearFilter() {
            // Reset the filter conditions here
            this.code = '';
            this.search = '';
            this.uqamaNo = '';
            this.beneficiaryId = '';
            this.authorizationPersonId = '';
            this.approvalPersonId = '';
            this.registered = '';
            this.fromDate = '';
            this.toDate = '';
            this.render++
            this.startMonth = '';
            this.month = '';
            this.year = '';
            this.amount = '';
            this.nationalId = '';
            this.nationality = '';
            this.contact = '';
            this.gender = '';
            this.status = '';
            this.paymentType = '';

            if (this.$refs.AuthorizedDropdown != undefined)
                this.$refs.AuthorizedDropdown.Remove();
            if (this.$refs.approvalPersonId != undefined)
                this.$refs.approvalPersonId.Remove();

            if (this.$refs.DatePicker != undefined)
                this.$refs.DatePicker.Remove();

            if (this.$refs.PaymentType != undefined)
                this.$refs.PaymentType.Remove();


            // Trigger the search or data refresh
            this.GetPayment(this.currentPage);
        },

        AdvanceFilterFor: function () {
            this.advanceFilters = !this.advanceFilters;
            // if (this.advanceFilters == false) {
            //     this.FilterRecord(false);
            // }
        },

        RefreshList: function () {
           this.show1=false;
           this.GetPayment();

        },
        SearchFilter: function () {
            this.GetPayment();
        },
        IsSave: function () {
            this.show = false;
        },
       
        GetDate: function (link) {
            if (link != undefined && link != null && link != '') {

                const date = moment(link);
                const arabicMonth = date.locale('ar').format('MMMM');
                return `${arabicMonth} ${date.format('Do YYYY')}`;

            }
            else {
                return '';
            }
        },
       
       
        getPage: function () {
            this.GetPayment(this.currentPage);
        },

        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        PrintRecord: function (id,brandObj) {
            this.loading=true;
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            this.brandObj=brandObj;
            root.$https.get('/Benificary/PaymentDetailQueryByAuth?authorizationPersonId=' + id, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {
                    if (response.data != null) {

                                                debugger;
                        root.paymentRecord = response.data.results;
                        root.show = true; 
                            root.changereport++;
                            root.loading = false; 

                       
                       
                    } else {
                        console.log("error: something wrong from db.");
                    }
                },
                    function (error) {
                        this.loading = false;
                        console.log(error);
                    });
        },

        GetPayment: function () {
            debugger;

            var root = this;
            var token = '';
            // this.loading = true;
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.$https.get('Benificary/GetAuthorizePaymentsList?pageNumber=' + this.currentPage + '&searchTerm=' + this.search + '&amount=' + this.amount + '&code=' + this.code  + '&fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&month=' + this.month + '&year=' + this.year + '&authorizationPersonId=' + this.authorizationPersonId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.paymentList = response.data.results;
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;
                    root.loading = false;
                }
                root.loading = false;
            });
        },


        

        ViewPayment: function (Id) {
            debugger;
            this.authorizeId=Id;
            this.show1= !this.show1;

            
        },
        GetHeaderDetail: function () {
                var root = this;
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                root.$https.get("/Company/GetCompanyDetail?id=" + localStorage.getItem('CompanyID'), { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {
                        if (response.data != null) {
                            debugger;
                            root.headerFooter.company = response.data;
                            root.headerFooter.company.base64Logo = 'data:image/png;base64,' + response.data.base64Logo;
                            root.$store.dispatch('GetCompanyList',  root.headerFooter);

                        }
                    });
            },
    },

    created: function () {
        this.GetHeaderDetail();
        this.$emit('input', this.$route.name);
    },
    mounted: function () {
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        this.GetPayment(this.currentPage);
        this.roleName = localStorage.getItem('RoleName');
       


    }
}
</script>