<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title"> {{ $t('Benificary.BeneficaryReport') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Home') }}</a>
                                    </li>
                                    <li class="breadcrumb-item active">{{ $t('Benificary.BeneficaryReport') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="PrintRdlc(true)" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('Payment.Print') }}
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
                        <div class="col-md-4 form-group">
                            <label class="text  font-weight-bolder">
                                {{ $t('Benificary.BeneficiaryName') }}
                            </label>
                            <input v-model="BeneficiaryName" type="text" class="form-control" :placeholder="$t('Benificary.Search')"
                                aria-label="Example text with button addon" aria-describedby="button-addon1">
                        </div>
                        <div class="col-md-4 form-group">
                            <label class="text  font-weight-bolder">
                                {{ $t('Benificary.BeneficiaryID') }}
                            </label>
                            <input v-model="beneficiaryId" type="text" class="form-control" :placeholder="$t('Benificary.SearchByID')"
                                aria-label="Example text with button addon" aria-describedby="button-addon1">
                        </div>
                        <div class="col-4 form-group">
                            <label class="text  font-weight-bolder">
                                {{ $t('Benificary.UqamaNo') }}
                            </label>
                            <input v-model="uqamaNo" type="text" class="form-control" :placeholder="$t('Benificary.SearchByUqamaNo')"
                                aria-label="Example text with button addon" aria-describedby="button-addon1">
                        </div>
                        <div class="col-md-4 form-group">
                            <label class="text  font-weight-bolder">
                                {{ $t('Benificary.AuthorizedPerson') }}
                            </label>
                            <authorizedperson v-model="authorizationPersonId" ref="AuthorizedDropdown" />
                        </div>
                        <!-- <div class="col-md-4 form-group">
                            <label class="text  font-weight-bolder">
                                {{ $t('Benificary.ApprovalPerson') }}
                            </label>
                            <approvalperson v-model="approvalPersonId" ref="approvalPersonId" />
                        </div> -->
                        <div class="col-md-4 form-group">
                            <label class="text  font-weight-bolder">
                                {{ $t('Benificary.Register/Un-Register') }}
                            </label>
                            <multiselect v-model="registered" :options="['Register', 'Un-Register']" :show-labels="false"
                                :placeholder="$t('AddBenificary.SelectType')">
                            </multiselect>
                        </div>
                        <div class="col-md-4 form-group">
                            <label class="text  font-weight-bolder">
                                {{ $t('Benificary.FromDate') }}
                            </label>
                            <datepicker v-model="fromDate" :key="render" />
                        </div>
                        <div class="col-md-4 form-group">
                            <label class="text  font-weight-bolder">
                                {{ $t('Benificary.ToDate') }}
                            </label>
                            <datepicker v-model="toDate" :key="render" />
                        </div>

                        <div class="col-sm-4 mt-3">
                            <button class="btn btn-outline-primary me-2" v-on:click="PrintRdlc(false)">{{
                                $t('LedgerReport.Filter') }}
                            </button>
                            <button class="btn btn-outline-primary btn-outline-danger" v-on:click="ClearFilter()">{{
                                $t('LedgerReport.ClearFilters') }}</button>
                        </div>
                    </div>
                </div>
                <div class="card-body">

                </div>
            </div>
        </div>
        <print :show="show" v-if="show" :reportsrc="reportsrc1" :changereport="changereportt" @close="show = false"
            @IsSave="IsSave" />
        <iframe :key="changereport" height="1080" width="1080" :src="reportsrc"></iframe>
    </div>
</template>
    
<script>
import clickMixin from '@/Mixins/clickMixin'
import Multiselect from 'vue-multiselect';


export default {
    mixins: [clickMixin],
    components: {
        Multiselect,
    },
    data: function () {
        return {
            reportsrc: '',
            changereport: 0,
            reportsrc1: '',
            show: false,
            changereportt: 0,
            authorizationPersonId: '',
            approvalPersonId: '',
            registered: '',
            arabic: '',
            english: '',
            fromDate: '',
            toDate: '',
            transactions: '',
            render: 0,
            uqamaNo: '',
            beneficiaryId: '',
            BeneficiaryName: '',
        }
    },
    watch: {
        benificaryId: function (val) {
            this.GetTransactions(val, 1);
        }
    },
    methods: {
        IsSave: function () {
            this.show = !this.show;
        },
        GotoPage: function (link) {
            this.$router.push({
                path: link
            });
        },
        ClearFilter: function () {
            this.search = '';
            this.uqamaNo = '';
            this.beneficiaryId = '';
            this.authorizationPersonId = '';
            this.approvalPersonId = '';
            this.registered = '';
            this.fromDate = '';
            this.toDate = '';
            this.render++

            if (this.$refs.AuthorizedDropdown != undefined)
                this.$refs.AuthorizedDropdown.Remove();
            if (this.$refs.approvalPersonId != undefined)
                this.$refs.approvalPersonId.Remove();
        },
        PrintRdlc: function (val) {
            var companyId = '';
            if (this.$session.exists()) {
                companyId = localStorage.getItem('CompanyID');
            }
            debugger;

            if (val) {
                this.reportsrc1 = this.$ReportServer + '/Invoice/A4_DefaultTempletForm.aspx?AuthorizationPersonId=' + this.authorizationPersonId + '&ApprovalPersonId=' + this.approvalPersonId + '&Registered=' + this.registered + '&fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&formName=benificaryreports' + '&Print=' + val + '&CompanyId=' + companyId+'&uqamaNo='+this.uqamaNo+'&BeneficiaryID='+this.beneficiaryId+'&searchTerm='+this.BeneficiaryName
                this.changereportt++;
                this.show = !this.show;
            } else {
                this.reportsrc = this.$ReportServer + '/Invoice/A4_DefaultTempletForm.aspx?AuthorizationPersonId=' + this.authorizationPersonId + '&ApprovalPersonId=' + this.approvalPersonId + '&Registered=' + this.registered + '&fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&formName=benificaryreports' + '&Print=' + val + '&CompanyId=' + companyId+'&uqamaNo='+this.uqamaNo+'&BeneficiaryID='+this.beneficiaryId+'&searchTerm='+this.BeneficiaryName
                this.changereport++;
            }
        },
        GetTransactions: function () {

            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Benificary/GetBenificaryReport?benificaryId=' + this.benificaryId + '&month=' + this.month + '&fromDate=' + this.fromDate + '&toDate=' + this.toDate  + '&searchTerm=' + this.search + '&beneficiaryId=' + this.beneficiaryId + '&uqamaNo=' + this.uqamaNo, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {
                    if (response.data) {
                        root.transactions = response.data;
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

    created: function () {
        this.$emit('input', this.$route.name);
    },
    mounted: function () {
        this.english = localStorage.getItem('locals');
        this.arabic = localStorage.getItem('locals');
    }
}
</script>
    