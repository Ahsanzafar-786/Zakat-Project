<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('LedgerReport.LedgerReport') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Home') }}</a>
                                    </li>
                                    <li class="breadcrumb-item active">{{ $t('LedgerReport.LedgerReport') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="GotoPage()" href="javascript:void(0);"
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
                            <benificary v-model="benificaryId" :key="render"/>
                        </div>
                        <div class="col-sm-2">
                            <datepicker :type="'month'" v-model="month" :key="render"/>
                        </div>
                        <div class="col-sm-2">
                            <datepicker v-model="fromDate" :isDisabled="month == '' ? false : true " :key="render"/>
                        </div>
                        <div class="col-sm-2">
                            <datepicker v-model="toDate" :isDisabled="month == '' ? false : true " :key="render"/>
                        </div>
                        <div class="col-sm-2">
                            <button class="btn btn-outline-primary me-2"  v-on:click="GetTransactions()" >Filter</button>
                            <button class="btn btn-outline-primary" v-on:click="ClearFilter()">Clear Filters</button>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                     
                </div>
            </div>
        </div>

    </div>
</template>


<script>
import clickMixin from '@/Mixins/clickMixin'

export default {
    mixins: [clickMixin],
    data: function () {
        return {
            benificaryId: '',
            arabic: '',
            english: '',
            fromDate:'',
            toDate:'',
            month:'',
            transactions: '',
            render:0
        }
    },
    watch: {
        benificaryId: function (val) {
            this.GetTransactions(val, 1);
        }
    },
    methods: {
        
        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        ClearFilter: function () 
        {
            this.benificaryId =  '';
            this.fromDate = '';
            this.toDate = '';
            this.month =  '';
            this.render++
        },

        GetTransactions: function () {
          
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Benificary/GetCharityTransactionList?benificaryId=' + this.benificaryId + '&month=' + this.month + '&fromDate=' + this.fromDate + '&toDate=' + this.toDate,  {
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