<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('Benificary.Benificary') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Home') }}</a>
                                    </li>
                                    <li class="breadcrumb-item active">{{ $t('Benificary.BenificaryList') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="openmodel" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary mx-1" v-if="roleName != 'User'">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('Benificary.Register') }}
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
                        <div class="col-3 form-group">
                            <label class="text  font-weight-bolder">
                                {{ $t('Benificary.BeneficiaryName') }}
                            </label>
                            <input v-model="search" type="text" class="form-control" :placeholder="$t('Benificary.Search')"
                                aria-label="Example text with button addon" aria-describedby="button-addon1">
                        </div>
                        <div class="col-3 form-group">
                            <label class="text  font-weight-bolder">
                                {{ $t('Benificary.BeneficiaryID') }}
                            </label>
                            <input v-model="beneficiaryId" type="text" class="form-control"
                                :placeholder="$t('Benificary.SearchByCode')" aria-label="Example text with button addon"
                                aria-describedby="button-addon1">
                        </div>
                        <div class="col-3 form-group">
                            <label></label>
                            <input v-model="amount" type="text" class="form-control"
                                :placeholder="$t('Benificary.SearchByAmount')" aria-label="Example text with button addon"
                                aria-describedby="button-addon1">
                            <!-- <decimaltofix v-model="amount" :placeholder="$t('Benificary.SearchByAmount')"  > </decimaltofix> -->
                        </div>
                        <div class="col-3 form-group">
                            <label>{{ $t('Benificary.PaymentType') }}</label>
                            <paymenttype v-model="paymentType" ref="PaymentType" />
                        </div>
                        <!-- <div class="col-4 form-group">
                            <label class="text  font-weight-bolder">
                                {{ $t('Benificary.UqamaNo') }}
                            </label>
                            <input v-model="uqamaNo" type="text" class="form-control"
                                :placeholder="$t('Benificary.SearchByUqamaNo')" aria-label="Example text with button addon"
                                aria-describedby="button-addon1">
                        </div> -->


                        <div class="col-md-12">
                            <!-- <a href="#" class="btn btn-sm btn-outline-primary" @click="AdvanceFilterFor">{{ $t('Benificary.AdvanceFilter') }}</a> -->
                            <a class="btn btn btn-soft-primary" v-on:click="AdvanceFilterFor" id="button-addon2">
                                <i class="fa fa-filter"></i>
                            </a>
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
                                <multiselect v-model="registered" :options="['Register', 'Un-Register']"
                                    :show-labels="false" :placeholder="$t('AddBenificary.SelectType')">
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
                                <datepicker v-model="startMonth" :type="'month'" :key="render" />

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
                                    <!-- <input v-model="gender" type="text" class="form-control"
                                :placeholder="$t('Benificary.SearchByGender')" aria-label="Example text with button addon"
                                aria-describedby="button-addon1"> -->
                                    <multiselect v-model="gender" :options="['Male', 'Female']" :show-labels="false"
                                        :placeholder="$t('AddBenificary.SelectType')">
                                    </multiselect>
                                </div>
                            </div>

                            <div class="col-xs-12  col-lg-3 ">
                                <label class="text  font-weight-bolder"> {{ $t('Benificary.Contact') }}</label>
                                <input v-model="contact" type="text" class="form-control"
                                    :placeholder="$t('Benificary.SearchByContact')"
                                    aria-label="Example text with button addon" aria-describedby="button-addon1">

                            </div>


                        </div>
                        <div class="col-sm-2 mt-3">
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
                                    <th class="text-center">
                                        {{ $t('Benificary.ID') }}
                                    </th>
                                    <th class="text-start">
                                        {{ $t('Benificary.Name') }}
                                    </th>

                                    <th class="text-center">
                                        {{ $t('Benificary.AmountPerMonth') }}
                                    </th>

                                    <th class="text-center">
                                        {{ $t('Benificary.PaymentType') }}
                                    </th>

                                    <th class="text-center">
                                        {{ $t('Benificary.RecurringAmount') }}
                                    </th>
                                    <th class="text-start">
                                        {{ $t('Benificary.AuthorizePersonName') }}
                                    </th>
                                    <th class="text-center">

                                        {{ $t('Benificary.Date') }}
                                    </th>

                                    <!-- <th class="text-center">
                                        {{ $t('Benificary.ApprovedBy') }}
                                    </th> -->
                                    <th class="text-center">
                                        {{ $t('Benificary.BenificaryStatus') }}
                                    </th>
                                    <th class="text-center" v-if="roleName == 'Admin'">
                                        {{ $t('Payment.ApprovalType') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Benificary.Notes') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Benificary.AccountStatus') }}
                                    </th>

                                    <th class="text-center">
                                        {{ $t('Benificary.Action') }}
                                    </th>



                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(brand, index) in benificarylist" v-bind:key="brand.id">
                                    <td v-if="currentPage === 1">
                                        {{ index + 1 }}
                                    </td>
                                    <td v-else>
                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                    </td>

                                    <td class="text-center">
                                        {{ brand.beneficiaryId }}
                                    </td>

                                    <td class="text-start">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditBenificary(brand.id, 'Edit')"> {{
                                                brand.name == '' ? brand.nameAr : brand.name }}</a>
                                        </strong>
                                    </td>


                                    <td class="text-center">
                                        {{ parseFloat(brand.amountPerMonth).toFixed(3).slice(0,
                                            -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,") }}
                                    </td>
                                    <td class="text-center">
                                        {{ $i18n.locale == 'en' ? brand.paymentTypeName : brand.paymentTypeNameAr }}
                                    </td>
                                    <td class="text-center">

                                        {{ parseFloat(brand.recurringAmount).toFixed(3).slice(0,
                                            -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,") }}

                                    </td>

                                    <td class="text-start">
                                        <span v-for="item in brand.benificaryAuthorization" :key="item.id" class="mx-2">
                                            {{ item.authorizationPersonName == '' ? item.authorizationPersonNameAr :
                                                item.authorizationPersonName }}
                                        </span>
                                    </td>
                                    <td class="text-center">{{ GetArabicDate(brand.startMonth) }}</td>

                                    <!-- <td class="text-center" v-if="brand.approvalPersonName != null">
                                        {{ brand.approvalPersonName }}
                                    </td> -->
                                    <!-- <td class="text-center" v-else>
                                        ---
                                    </td> -->

                                    <td class="text-center">

                                        <span v-if="brand.isActive" class="badge badge-boxed  badge-outline-success">
                                            {{
                                                $t('Benificary.Active')
                                            }}
                                        </span>
                                        <span v-else class="badge badge-boxed  badge-outline-danger">

                                            {{
                                                $t('Benificary.DeActive')
                                            }}
                                        </span>
                                    </td>

                                    <td class="text-center" v-if="roleName == 'Admin'">
                                        <span class="badge badge-boxed  badge-outline-danger"
                                            v-if="brand.approvalStatus == '4'">{{ $t('Benificary.WaitingForApproved')
                                            }}</span>
                                        <span class="badge badge-boxed  badge-outline-success"
                                            v-if="brand.approvalStatus == '3'">{{ $t('Benificary.Approved') }}</span>
                                    </td>
                                    <td v-if="brand.note != null">
                                        {{ brand.note }}
                                    </td>
                                    <td v-else>
                                        ---
                                    </td>


                                    <td class="text-center">
                                        <span v-if="brand.isRegister" class="badge badge-boxed  badge-outline-success">
                                            {{ $t('Benificary.Register') }}
                                        </span>
                                        <span v-else class="badge badge-boxed  badge-outline-danger">
                                            {{ $t('Benificary.UnRegister') }}
                                        </span>
                                    </td>
                                    <td class="text-center d-flex align-items-baseline justify-content-center"
                                        v-if="roleName != 'User'">
                                        <button type="button" class="btn btn-light dropdown-toggle"
                                            data-bs-toggle="dropdown" aria-expanded="false">
                                            {{ $t('Payment.Action') }} <i class="mdi mdi-chevron-down"></i></button>
                                        <div class="dropdown-menu text-center">
                                            <a class="dropdown-item" href="javascript:void(0)"
                                                v-on:click="EditBenificary(brand.id, 'View')">{{ $t('Benificary.View')
                                                }}</a>
                                            <a class="dropdown-item" href="javascript:void(0)"
                                                v-on:click="PrintRdlc(brand.id, false)">{{ $t('Payment.Print') }}</a>
                                            <a class="dropdown-item" href="javascript:void(0)"
                                                v-on:click="PrintRdlc(brand.id, true)">{{ $t('Benificary.PDF') }}</a>


                                        </div>
                                    </td>

                                    <!-- <td class="border-top-0 text-center">
                                        <approvalperson v-model="person.approvalPersonId"
                                            :values="person.approvalPersonId"/>

                                    </td> -->
                                    <!-- <td class="text-center">
                                        <span v-if="brand.gender" class="badge badge-boxed  badge-outline-success">
                                            {{ $t('Benificary.Male') }}
                                        </span>
                                        <span v-else class="badge badge-boxed  badge-outline-danger">
                                            {{ $t('Benificary.Female') }}
                                        </span>
                                    </td> -->
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
                        <div class="" v-on:click="GetBenificaryData()">
                            <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount" :per-page="10"
                                :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')"
                                :next-text="$t('Table.Next')" :last-text="$t('Table.Last')"></b-pagination>
                        </div>
                    </div>
                </div>
            </div>
            <!-- <loading :active.sync="loading" :can-cancel="true" :is-full-page="false"></loading>   -->
            <print :show="show1" v-if="show1" :reportsrc="reportsrc1" :changereport="changereportt" @close="show1 = false"
                @IsSave="IsSaveRpt" />
            <benificary-mod :brand="newBenificary" :show="show" v-if="show" @close="IsSave" :type="type" />
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


export default {


    mixins: [clickMixin],
    props: ['brand'],
    components: {
        Multiselect,
        Loading,
    },
    data: function () {
        return {
            paymentType: '',
            user: '',
            show: false,
            roleName: '',
            changereport: 0,
            reportsrc: '',
            show1: false,
            benificarylist: [],
            newBenificary: {
                id: '',
                name: '',
                nameAr: '',
                gender: '',
                beneficiaryId: 0,
                paymentIntervalMonth: 0,
                amountPerMonth: 0,
                recurringAmount: 0,
                ugamaNo: '',
                phoneNo: '',
                note: '',
                authorizedPersonId: '',
                approvalPersonId: '',
                paymentTypeId: '',
                isActive: true,
                isRegister: true,
                address: '',
                approvalStatus: '',
                passportNo: '',
                reason: '',
                nationality: '',
                startMonth: '',
                startDate: '',
                endDate: '',
                isDisable: false,
                approvedPaymentId: '',
                advancePayment: 0,
                durationType: '',
                benificaryAuthorization: [{
                    id: '',
                    benficaryId: '',
                    authorizationPersonId: '',
                    approvalPersonId: '',
                    date: '',
                    description: '',
                    isActive: false
                }],
            },
            type: '',
            search: '',
            currentPage: 1,
            pageCount: '',
            rowCount: '0',
            arabic: '',
            english: '',
            benificaryName: '',
            uqamaNo: '',
            beneficiaryId: '',
            authorizationPersonId: '',
            approvalPersonId: '',
            registered: '',
            fromDate: '',
            toDate: '',
            render: 0,
            startMonth: '',
            year: '',
            advanceFilters: false,
            amount: '',
            nationalId: '',
            nationality: '',
            gender: '',
            status: '',
            contact: '',
            loading: false,

        }

    },
    // watch: {
    //     search: function (val) {
    //         this.GetBenificaryData(val, 1);
    //     }
    // },
    methods: {
        ClearFilter() {
            // Reset the filter conditions here
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
            this.GetBenificaryData(this.currentPage);
        },


        SearchFilter: function () {
            this.GetBenificaryData(this.currentPage);
        },


        IsSave: function () {
            this.show = false;
            this.GetBenificaryData(this.search, this.currentPage);
        },

        getPage: function () {
            this.GetBenificaryData(this.currentPage);
        },
        GetArabicDate: function (link) {
      
            if (link != undefined && link != null && link != '') {

                const date = moment(link);
                const arabicMonth = date.locale('ar').format('MMMM');
                return `${arabicMonth} ${date.format('Do YYYY')}`;

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
        AdvanceFilterFor: function () {


            this.advanceFilters = !this.advanceFilters;
            if (this.advanceFilters == false) {
                this.FilterRecord(false);
            }

        },

        GotoPage: function (link) {
            this.$router.push({
                path: link
            });
        },

        openmodel: function () {
            this.newBenificary = {
                id: '00000000-0000-0000-0000-000000000000',
                name: '',
                nameAr: '',
                gender: 'Male',
                beneficiaryId: 0,
                paymentIntervalMonth: 0,
                amountPerMonth: 0,
                recurringAmount: 0,
                ugamaNo: '',
                phoneNo: '',
                note: '',
                approvalStatus: '',
                isDisable: false,
                authorizedPersonId: '',
                approvalPersonId: '',
                paymentTypeId: '',
                isActive: true,
                isRegister: true,
                address: '',
                passportNo: '',
                reason: '',
                nationality: '',
                startMonth: '',
                startDate: '',
                endDate: '',
                approvedPaymentId: '',
                advancePayment: 0,
                durationType: 'Customize',
                authorizationPersonName: '',
                benificaryAuthorization: [{
                    id: '',
                    benficaryId: '',
                    authorizationPersonId: '',
                    approvalPersonId: '',
                    date: '',
                    description: '',
                    isActive: true
                }],
            }
            this.show = !this.show;
            this.type = "Add";
        },

        GetBenificaryData: function () {
            var root = this;
            var token = '';
            this.loading = true;
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.$https.get('Benificary/GetBenificaryList?pageNumber=' + this.currentPage + '&searchTerm=' + this.search + '&beneficiaryId=' + this.beneficiaryId + '&uqamaNo=' + this.uqamaNo + '&authorizationPersonId=' + this.authorizationPersonId + '&approvalPersonId=' + this.approvalPersonId + '&registered=' + this.registered + '&fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&startMonth=' + this.startMonth + '&year=' + this.year + '&amount=' + this.amount + '&nationality=' + this.nationality + '&gender=' + this.gender + '&contact=' + this.contact + '&status=' + this.status + '&nationalId=' + this.nationalId + '&paymentType=' + this.paymentType, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            }).then(function (response) {
                if (response.data != null) {
                    root.benificarylist = response.data.results;
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;
                    root.loading = false;
                }
                root.loading = false;
            });
        },
        IsSaveRpt: function () {
            this.show1 = !this.show1;
        },
        PrintRdlc: function (val, isDownload) {
            var companyId = '';
            if (this.$session.exists()) {
                companyId = localStorage.getItem('CompanyID');
            }
            var root = this;
            if (isDownload) {
                this.loading = true;
                this.$https.get(this.$ReportServer + '/Invoice/A4_DefaultTempletForm.aspx?AuthorizationPersonId=' + val + '&CompanyID=' + companyId + '&formName=benificary' + '&Language=' + this.$i18n.locale + '&isDownload=' + isDownload
                    , { responseType: 'blob' }).then(function (response) {
                        debugger;
                        root.loading = false;
                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;
                        var date = moment().format('DD MMM YYYY');
                        link.setAttribute('download', 'benificary ' + date + '.pdf');
                        document.body.appendChild(link);
                        link.click();

                    })
            }

            else {
                this.reportsrc1 = this.$ReportServer + '/Invoice/A4_DefaultTempletForm.aspx?AuthorizationPersonId=' + val + '&CompanyID=' + companyId + '&formName=benificary' + '&Language=' + this.$i18n.locale + '&isDownload=' + isDownload
                this.changereportt++;
                this.show1 = !this.show1;
            }
        },
        EditBenificary: function (Id, type) {

            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Benificary/GetBenificaryDetail?Id=' + Id, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {
                    if (response.data) {
                        root.newBenificary = response.data;
                        root.show = !root.show;
                        root.type = type;
                    } else {
                        console.log("error: something wrong from db.");
                    }
                },
                    function (error) {
                        this.loading = false;
                        console.log(error);
                    });

        }
    },

    created: function () {
        this.$emit('input', this.$route.name);
    },
    mounted: function () {
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        this.GetBenificaryData(this.currentPage);
        this.roleName = localStorage.getItem('RoleName');



    }
}
</script>
