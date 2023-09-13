<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('Payment.PaymentWiseReport') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Home') }}</a>
                                    </li>
                                    <li class="breadcrumb-item active">{{ $t('Payment.PaymentWiseReport') }}</li>
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
                        <div class="col-sm-3">
                            <benificary v-model="benificaryId" :key="render" />
                        </div>
                        <div class="col-sm-3">
                            <userdropdown v-model="userId" />
                        </div>
                        <div class="col-sm-2">
                            <datepicker v-model="fromDate" :isDisabled="month == '' ? false : true" :key="render" />
                        </div>
                        <div class="col-sm-2">
                            <datepicker v-model="toDate" :isDisabled="month == '' ? false : true" :key="render" />
                        </div>
                        <div class="col-sm-2">
                            <button class="btn btn-outline-primary me-2" v-on:click="PrintRdlc(false)">{{
                                $t('LedgerReport.Filter') }}
                            </button>
                            <button class="btn btn-outline-primary" v-on:click="ClearFilter()">{{
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
<!-- <div class="col-md-8">
    <td width="100px" class="fw-bold">Total</td>
    <td style="text-align: right;" class="fw-bold">500</td>
</div> -->
        <iframe :key="changereport" height="1500" width="1000" :src="reportsrc"></iframe>
        <loading :active.sync="loading" :can-cancel="false" :is-full-page="true"></loading>
    </div>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import 'vue-loading-overlay/dist/vue-loading.css';
import Loading from 'vue-loading-overlay';

export default {
    mixins: [clickMixin],
    components: {
        Loading
    },
    data: function () {
        return {
            locals:'',
            loading: false,
            reportsrc: '',
            changereport: 0,
            reportsrc1: '',
            show: false,
            changereportt: 0,
            benificaryId: '',
            userId:'',
            arabic: '',
            english: '',
            fromDate: '',
            toDate: '',
            month: '',
            transactions: '',
            render: 0
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
            this.$router.push({ path: link });
        },
        ClearFilter: function () {
            this.benificaryId = '';
            this.fromDate = '';
            this.toDate = '';
            this.userId = '';
            this.render++
        },
        PrintRdlc: function (val) {
            debugger;
            if(this.benificaryId == '' && this.fromDate == '' && this.toDate == '' && this.userId == '')
            {
                return this.$swal.fire({
                        icon: 'error',
                        title: this.english == 'en' ? 'Filters' : 'المرشحات',
                        text: this.english == 'en' ? 'Please Select Filters' : 'يرجى تحديد عوامل التصفية',
                        showConfirmButton: false,
                        timer: 5000,
                        timerProgressBar: true,
                    });
            }
            else{
            var companyId = '';
            if (this.$session.exists()) {
                companyId = localStorage.getItem('CompanyID');
            }
            debugger;

            if (val) {
                this.reportsrc1 = this.$ReportServer + '/Invoice/A4_DefaultTempletForm.aspx?companyId=' + companyId + '&benificaryId=' + this.benificaryId + '&userId=' + this.userId + '&fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&formName=PaymentWiseReport' + "&Print=" + val
                this.changereportt++;
                this.show = !this.show;
                this.loading = false

            }
            else {
                this.reportsrc = this.$ReportServer + '/Invoice/A4_DefaultTempletForm.aspx?companyId=' + companyId + '&benificaryId=' + this.benificaryId + '&userId=' + this.userId + '&fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&formName=PaymentWiseReport' + "&Print=" + val
                this.changereport++;
            }
        }
        },
       
    },

    created: function () {
      
    },
    mounted: function () {
        this.english = localStorage.getItem('locals');
        this.arabic = localStorage.getItem('locals');
    }
}
</script>