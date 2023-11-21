<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">Payment by Authorized Person</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Home') }}</a>
                                    </li>
                                    <li class="breadcrumb-item active">Payment by Authorized Person</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="GotoPage('/addpaymentauthoirizeperson')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary mx-1" >
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    Add Payment
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
                                {{ $t('Payment.BeneficiaryCode') }}
                            </label>
                            <input v-model="code" type="text" class="form-control" :placeholder="$t('Payment.SearchByCode')"
                                aria-label="Example text with button addon" aria-describedby="button-addon1">
                        </div>
                        <div class="col-md-4">
                            <label class="text  font-weight-bolder">
                                {{ $t('Benificary.BeneficiaryName') }}
                            </label>
                            <input v-model="search" type="text" class="form-control" :placeholder="$t('Payment.Search')"
                                aria-label="Example text with button addon" aria-describedby="button-addon1">
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
                        <div class="col-xs-12  col-lg-3">
                            <div class="form-group">
                                <label class="text  font-weight-bolder">
                                    {{ $t('Benificary.AuthorizedPerson') }}
                                </label>
                                <authorizedperson v-model="authorizationPersonId" ref="AuthorizedDropdown" />
                            </div>
                        </div>
                        <!-- <div class="col-md-3 form-group">
                            <label class="text  font-weight-bolder">
                                {{ $t('Benificary.ApprovalPerson') }}
                            </label>
                            <approvalperson v-model="approvalPersonId" ref="approvalPersonId" />
                        </div> -->
                        <div class="col-md-3 form-group">
                            <label class="text  font-weight-bolder">
                                {{ $t('Benificary.Register/Un-Register') }}
                            </label>
                            <multiselect v-model="registered" :options="['Register', 'Un-Register']" :show-labels="false"
                                :placeholder="$t('AddBenificary.SelectType')">
                            </multiselect>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3">
                            <label class="text  font-weight-bolder"> {{ $t('Benificary.Status') }}</label>
                            <multiselect v-model="status" :options="['Active', 'De-Active']" :show-labels="false"
                                :placeholder="$t('AddBenificary.SelectType')">
                            </multiselect>

                        </div>
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
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>{{ $t('Benificary.NationalID') }}</label>
                                <input v-model="uqamaNo" type="text" class="form-control"
                                    :placeholder="$t('Benificary.SearchByNationalId')"
                                    aria-label="Example text with button addon" aria-describedby="button-addon1">

                            </div>
                        </div>

                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>{{ $t('Benificary.Nationality') }}</label>
                                <input v-model="nationality" type="text" class="form-control"
                                    :placeholder="$t('Benificary.SearchByNationality')"
                                    aria-label="Example text with button addon" aria-describedby="button-addon1">
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>{{ $t('Benificary.Gender') }}</label>
                                <multiselect v-model="gender" :options="['Male', 'Female']" :show-labels="false"
                                    :placeholder="$t('AddBenificary.SelectType')">
                                </multiselect>
                            </div>
                        </div>

                        <div class="col-xs-12  col-lg-3 ">
                            <label class="text  font-weight-bolder"> {{ $t('Benificary.Contact') }}</label>
                            <input v-model="contact" type="text" class="form-control"
                                :placeholder="$t('Benificary.SearchByContact')" aria-label="Example text with button addon"
                                aria-describedby="button-addon1">

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
                                      Code
                                    </th>
                                    <th class="text-center">
                                      Date
                                    </th>
                                    <th class="text-center">
                                        Authoirzed Person Name
                                    </th>
                                    <th class="text-center">
                                       Amount
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
                                            <a class="text-danger" href="javascript:void(0)">{{brand.code }}</a>
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
                                                v-on:click="ViewPayment(brand.id)">View</a>
                                           
                                            
                                            <strong>
                                                <a href="javascript:void(0)" v-on:click="PrintRdlc(brand.id)"> {{
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

            <print :show="show" v-if="show" :reportsrc="reportsrc" :changereport="changereport" @close="show = false"
                @IsSave="IsSave" />
            <paymentauthorizedetail :id="authorizeId" :show="show1" v-if="show1"  @close="show1= false"
                @IsSave="IsSave" />
            <loading :active.sync="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>

    </div>
</template>


<script>
import clickMixin from '@/Mixins/clickMixin'
import 'vue-loading-overlay/dist/vue-loading.css';
import Multiselect from 'vue-multiselect';
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/vue-loading.css';
import moment from 'moment'
// import 'moment/locale/ar'; // Import Arabic locale

export default {
    mixins: [clickMixin],
    components: {
        Multiselect,
        Loading,
    },
    data: function () {
        return {
            advanceFilters: false,
            authorizationPersonId: '',
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
            paymentList: [],
            type: '',
            search: '',
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
       
        PrintRdlc: function (Id) {
            var companyId = '';
            if (this.$session.exists()) {
                companyId = localStorage.getItem('CompanyID');
            }

            this.reportsrc = this.$ReportServer + '/Invoice/A4_DefaultTempletForm.aspx?id=' + Id + '&pageNumber=' + this.currentPage + '&searchTerm=' + this.search + '&CompanyID=' + companyId + '&formName=Payment'
            this.changereport++;
            this.show = !this.show;
        },
        getPage: function () {
            this.GetPayment(this.currentPage);
        },

        GotoPage: function (link) {
            this.$router.push({ path: link });
        },

        GetPayment: function () {
            debugger;

            var root = this;
            var token = '';
            this.loading = true;
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.$https.get('Benificary/GetAuthorizePaymentsList?pageNumber=' + this.currentPage + '&searchTerm=' + this.search + '&amount=' + this.amount + '&code=' + this.code + '&benificaryCode=' + this.benificaryCode + '&fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&month=' + this.month + '&year=' + this.year + '&register=' + this.registered + '&status=' + this.status + '&contactNo=' + this.contact + '&gender=' + this.gender + '&nationality=' + this.nationality + '&uqamaNo=' + this.uqamaNo + '&approvalPersonId=' + this.approvalPersonId + '&authorizationPersonId=' + this.authorizationPersonId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
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

            
        }
    },

    created: function () {
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