<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('Payment.Payment') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Home') }}</a>
                                    </li>
                                    <li class="breadcrumb-item active">{{ $t('Payment.PaymentList') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="GotoPage('/addpayment')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary mx-1" v-if="roleName != 'User'">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('AddNew') }}
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
                        <div class="col-3">
                            <label class="text  font-weight-bolder">
                                {{ $t('Benificary.BeneficiaryName') }}
                            </label>
                            <input v-model="search" type="text" class="form-control" :placeholder="$t('Payment.Search')"
                                aria-label="Example text with button addon" aria-describedby="button-addon1">
                        </div>
                        <div class="col-3">
                            <label class="text  font-weight-bolder">
                                {{ $t('Benificary.BeneficiaryID') }}
                            </label>
                            <input v-model="benificaryCode" type="text" class="form-control" :placeholder="$t('Payment.SearchByID')"
                                aria-label="Example text with button addon" aria-describedby="button-addon1">
                        </div>
                        <div class="col-3">
                            <label class="text  font-weight-bolder">
                                {{ $t('Payment.Code') }}
                            </label>
                            <input v-model="code" type="text" class="form-control" :placeholder="$t('Payment.SearchByCode')"
                                aria-label="Example text with button addon" aria-describedby="button-addon1">
                        </div>
                        <div class="col-3">
                            <label class="text  font-weight-bolder">
                                {{ $t('Payment.Amount') }}
                            </label>
                            <input v-model="amount" type="text" class="form-control"
                                :placeholder="$t('Payment.SearchByAmount')" aria-label="Example text with button addon"
                                aria-describedby="button-addon1">
                        </div>
                        <div class="col-md-3 form-group">
                            <label class="text  font-weight-bolder">{{ $t('AddBenificary.StartFrom') }}</label>
                            <datepicker v-model="month" :type="'month'" :key="render"  />

                        </div>
                        <div class="col-md-3 form-group">
                                <label>Select Year:</label>
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
                        <div class="col-sm-2 mt-3">
                            <a v-on:click="SearchFilter" href="javascript:void(0);"
                                class="btn btn-sm btn-outline-primary mx-1">
                                {{ $t('Payment.SearchFilter') }}
                            </a>
                            <a @click="ClearFilter" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                                {{ $t('Payment.ClearFilter') }}
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
                                        {{ $t('Payment.Code') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Payment.ID') }}
                                    </th>
                                    <th class="text-start">
                                        {{ $t('Payment.BenificaryName') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Payment.Amount') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Payment.Cashier') }}
                                    </th>
                                    <th class="text-center">

                                        {{ $t('Payment.Date') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Payment.Month') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Payment.Year') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Payment.Period') }}
                                    </th>
                                    <th class="text-center">

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

                                    <td class="text-center " v-if="brand.isVoid && roleName == 'Admin'">
                                        <strong>
                                            <a class="text-danger" href="javascript:void(0)"
                                                v-on:click="EditPayment(brand.id)">{{
                                                    brand.code }}</a>
                                        </strong>
                                    </td>
                                    <td class="text-center" v-else-if="roleName != 'Cashier'">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditPayment(brand.id)">{{
                                                brand.code }}</a>
                                        </strong>
                                    </td>
                                    <td class="text-center text-danger" v-else-if="brand.isVoid">{{ brand.code }}</td>

                                    <td class="text-center" v-else>{{ brand.code }}</td>
                                    <td class="text-center" >{{ brand.benificaryCode }}</td>

                                    <td class="text-center text-danger" v-if="brand.isVoid">Payment Voided</td>
                                    <td class="text-start" v-else-if="roleName != 'Cashier'">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditPayment(brand.id)">
                                                {{ brand.benificaryNameAr ==
                                                    '' ? brand.benificaryName : brand.benificaryNameAr }}</a>
                                        </strong>
                                    </td>
                                    <td class="text-start" v-else>{{
                                        brand.benificaryName == '' ? brand.benificaryNameAr : brand.benificaryName }}</td>


                                    <td class="text-center" v-if="brand.isVoid">--</td>
                                    <td class="text-center" v-else>{{ brand.amount }}</td>
                                    <td class="text-center">{{ brand.cashier }}</td>
                                    <td class="text-center">{{ GetDate(brand.date) }}</td>

                                    <td class="text-center" v-if="brand.isVoid">
                                        --
                                    </td>
                                    <td class="text-center" v-else-if="brand.month != null">
                                        {{ GetMonth(brand.month) }}
                                    </td>

                                    <td class="text-center" v-else>
                                        {{ GetMonth(brand.date) }}
                                    </td>
                                    <td class="text-center" v-if="brand.isVoid">
                                        --
                                    </td>
                                    <td class="text-center" v-else>
                                        {{ brand.year }}
                                    </td>
                                    <td class="text-center" v-if="brand.isVoid">
                                        --
                                    </td>
                                    <td class="text-center" v-else>
                                        {{ brand.period }}
                                    </td>
                                    <td class="text-center d-flex align-items-baseline justify-content-center"
                                        v-if="roleName != 'User'">
                                        <button type="button" class="btn btn-light dropdown-toggle"
                                            data-bs-toggle="dropdown" aria-expanded="false"> {{ $t('Payment.Action') }} <i
                                                class="mdi mdi-chevron-down"></i></button>
                                        <div class="dropdown-menu text-center">
                                            <div v-if="brand.allowVoid && !brand.isVoid && roleName == 'Cashier'">
                                                <input type="checkbox" v-model="brand.isVoid"
                                                    v-on:change="EditPayment(brand.id, brand.isVoid)" />
                                                <span class="mx-1"> {{
                                                    $t('Payment.IsVoid') }}
                                                </span>
                                            </div>
                                            <div v-if="brand.allowVoid && brand.isVoid && roleName == 'Cashier'">
                                                <input type="checkbox" v-model="brand.isVoid"
                                                    v-on:change="EditPayment(brand.id, brand.isVoid)" disabled />
                                                <span class="mx-1"> {{
                                                    $t('Payment.IsVoid') }}
                                                </span>
                                            </div>
                                            <div v-if="roleName != 'Cashier' && roleName != 'User'">
                                                <input type="checkbox" disabled v-model="brand.isVoid" />
                                                <span class="mx-1"> {{
                                                    $t('Payment.IsVoid') }}
                                                </span>
                                            </div>
                                            <div v-if="roleName != 'Cashier' && roleName != 'User'">
                                                <input type="checkbox" v-model="brand.allowVoid"
                                                    v-on:change="EditPayment(brand.id, brand.isVoid, brand.allowVoid)" />
                                                <span class="mx-1"> {{
                                                    $t('Payment.AllowVoid') }}
                                                </span>
                                            </div>
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
                        <span v-else-if="currentPage === 1 && rowCount < 10">
                            {{ $t('Pagination.Showing') }}
                            {{ currentPage }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                            {{ rowCount }} {{ $t('Pagination.entries') }}
                        </span>
                        <span v-else-if="currentPage === 1 && rowCount >= 11">
                            {{ $t('Pagination.Showing') }}
                            {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{ $t('Pagination.of') }}
                            {{ rowCount }} {{ $t('Pagination.entries') }}
                        </span>
                        <span v-else-if="currentPage === 1">
                            {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                                $t('Pagination.to')
                            }} {{ currentPage * 10 }} of {{ rowCount }} {{ $t('Pagination.entries') }}
                        </span>
                        <span v-else-if="currentPage !== 1 && currentPage !== pageCount">
                            {{ $t('Pagination.Showing') }}
                            {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{
                                $t('Pagination.of')
                            }} {{ rowCount }} {{ $t('Pagination.entries') }}
                        </span>
                        <span v-else-if="currentPage === pageCount">
                            {{ $t('Pagination.Showing') }}
                            {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ rowCount }} {{
                                $t('Pagination.of')
                            }}
                            {{ rowCount }} {{ $t('Pagination.entries') }}
                        </span>
                    </div>
                    <div class="float-end">
                        <div class="" v-on:click="GetPayment()">
                            <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount" :per-page="10"
                                :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')"
                                :next-text="$t('Table.Next')" :last-text="$t('Table.Last')"></b-pagination>
                        </div>
                    </div>
                </div>
            </div>

            <print :show="show" v-if="show" :reportsrc="reportsrc" :changereport="changereport" @close="show = false"
                @IsSave="IsSave" />

        </div>

    </div>
</template>


<script>
import clickMixin from '@/Mixins/clickMixin'
import moment from 'moment'

export default {
    mixins: [clickMixin],
    data: function () {
        return {
            show: false,
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
            benificaryCode:'',
            fromDate: '',
            toDate: '',
            month:'',
            year: '',
            render:0

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
            this.search = '';
            this.amount = '';
            this.code = '';
            this.benificaryCode = '';
            this.fromDate = '';
            this.toDate = '';
            this.render++
            this.month = '';
            this.year = '';

            // Trigger the search or data refresh
            this.GetPayment(this.currentPage);
        },


        SearchFilter: function () {
            debugger;
            this.GetPayment(this.currentPage);
        },
        IsSave: function () {
            this.show = false;
        },
        GetMonth: function (link) {
            if (link != undefined) {
                return moment(link).format('MMMM');

            }
            else {
                return '';
            }
        },
        GetDate: function (link) {
            if (link != undefined) {
                return moment(link).format('MMMM Do YYYY');

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
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.$https.get('Benificary/GetPaymentsList?pageNumber=' + this.currentPage + '&searchTerm=' + this.search + '&amount=' + this.amount + '&code=' + this.code + '&benificaryCode=' + this.benificaryCode + '&fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&month=' + this.month + '&year=' + this.year, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.paymentList = response.data.results;
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;
                    root.loading = false;
                }
                root.loading = false;
            });
        },

        EditPayment: function (Id, val, allowVoid) {
            debugger;
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            if (allowVoid) {
                root.$https.get('/Benificary/GetPaymentsDetail?Id=' + Id + '&allowVoid=' + allowVoid, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        debugger;

                        if (response.data == "") {
                            root.$swal({
                                title: 'Save Allow Void',
                                text: 'Permission Allow to make payment void',
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 3000,
                                timerProgressBar: true,
                            });
                            root.GetPayment();
                        } else {
                            console.log("error: something wrong from db.");
                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });
            }
            else if (val) {
                root.$https.get('/Benificary/GetPaymentsDetail?Id=' + Id + '&isVoid=' + val, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        debugger;
                        if (response.data == "") {
                            root.$swal({
                                title: 'Save Void',
                                text: 'Successfully Payment Voided',
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 3000,
                                timerProgressBar: true,
                            });
                            root.GetPayment();
                        } else {
                            console.log("error: something wrong from db.");
                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });
            }
            else {
                root.$https.get('/Benificary/GetPaymentsDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data) {
                            root.$router.push({
                                path: '/addpayment',
                                query: { data: response.data }
                            });
                        } else {
                            console.log("error: something wrong from db.");
                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });
            }
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