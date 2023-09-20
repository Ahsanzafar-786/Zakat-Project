<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">Transaction Report</h4>
                               
                            </div>
                            
                        </div>
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-header">
                    <div class="row">
                       
                       
                        <div class="col-lg-4">
                            <datepicker v-model="fromDate" :key="render" />
                        </div>
                        <div class="col-lg-4">
                            <datepicker v-model="toDate"  :key="render" />
                        </div>
                        <div class="col-sm-3 col-md-8 mt-2" >
                            <button class="btn btn-outline-primary me-2" v-on:click="GetTransactions()">{{
                                $t('LedgerReport.Filter') }}
                            </button>
                            <button class="btn btn-outline-primary" v-on:click="ClearFilter()">{{
                                $t('LedgerReport.ClearFilters') }}</button>
                            <button style="float: right;" class="btn btn-outline-primary " v-on:click="PrintRdlc()">{{ $t('Print') }}</button>

                        </div>

                    </div>
                </div>
                <div class="card-body" >
                  
                    <div class="table-responsive">
                            <table class="table mb-0" style="table-layout: fixed; ">
                                <thead class="thead-light">
                                    <tr>
                                 
                                        
                                        <th >
                                            DATE
                                        </th>
                                        <th width="100px" >
                                            BENEFICIARY ID
                                        </th>
                                        <th width="200px" >
                                            DESCRIPTION
                                        </th>
                                        <th width="100px" >
                                           STATUS
                                        </th>
                                        <th >
                                            PERIOD
                                        </th>
                                       
                                        <th width="200px"  >
                                            PAYMENT INTERVAL IN MONTHS
                                        </th>
                                        <th >
                                            DEBIT
                                        </th>
                                        <th >
                                            CREDIT
                                        </th>

                                        <th >
                                           PAYMENT TYPE
                                        </th>
                                       

                                    </tr>
                                </thead>
                                <tbody>
                                    
                                    <tr >
                                        <td >  </td>
                                    <td >  </td>
                                    <td ><b>OPENING BALANCE</b></td>
                                    <td >  </td>
                                    <td >  </td>
                                    <td >  </td>
                               
                                    <td >  </td>
                                        <td>{{transactions.openingBalance}}</td>
                                        <td >  </td>
                                    </tr>
                                    <tr v-for="(item,index) in transactions.paymentList" v-bind:key="index">
                                        <td > {{item.paymentDate}} </td>
                                    <td >{{item.cashierName}}</td>
                                    <td > {{item.description}}</td>
                                    <td >   {{item.transactiontype}}  </td>
                                    <td >  </td>
                                    
                                    <td >  </td>
                                    <td >  </td>
                                        <td>{{item.amount}}</td>
                                        <td >  </td>
                                    </tr>
                                    
                                    <tr >
                                        <td >  </td>
                                    <td >  </td>
                                    <td ><b>TOTAL</b></td>
                                    <td >  </td>
                                    <td >  </td>
                                   
                                    <td >  </td>
                                    <td >  </td>
                                        <td>{{transactions.fundsTotal}}</td>
                                        <td >  </td>
                                    </tr>
                                    <tr v-for="(item,index) in transactions.charitylist" v-bind:key="index">
                                        
                                       

                                       
                                        <td>
                                            {{item.paymentDate}}
                                        </td>
                                        <td>
                                            {{item.beneficaryId}}
                                        </td>
                                        <td>
                                            {{item.beneficaryName}}
                                        </td>
                                        <td>
                                            {{item.isRegistered}}
                                        </td>
                                        <td>
                                            {{item.paymentMonth}}    
                                        </td>
                                       
                                        <td>
                                            {{item.paymentIntervalMonth}}
                                        </td>
                                        <td> {{item.amount}}
                                            </td>
                                        <td>
                                           
                                        </td>

                                        
                                        <td>
                                            {{item.paymentType}}
                                        </td>
                                    </tr>
                                    <tr >
                                        <td >  </td>
                                    <td >  </td>
                                    <td ><b>TRANSACTION TOTAL</b></td>
                                    <td >  </td>
                                    <td >  </td>
                                   
                                    <td >  </td>
                                    <td >{{transactions.transactionTotal}}  </td>
                                        <td></td>
                                        <td >  </td>
                                    </tr>

                                    <tr >
                                        <td >  </td>
                                    <td >  </td>
                                    <td ><b>CLOSING BALANCE</b></td>
                                    <td >  </td>
                                    <td >  </td>
                                    <td >  </td>
                                  
                                    <td >  </td>
                                        <td>{{transactions.closingBalance}}</td>
                                        <td >  </td>
                                    </tr>
                                </tbody>
                            </table><!--end /table-->
                        </div><!--end /tableresponsive-->
                </div>
            </div>
        </div>
        <print :show="show" v-if="show" :reportsrc="reportsrc1" :changereport="changereportt" @close="show = false"
            @IsSave="IsSave" />
    </div>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'


export default {
    mixins: [clickMixin],
    components: {
     
    },
    data: function () {
        return {
            locals:'',
          
            reportsrc: '',
            changereport: 0,
            reportsrc1: '',
            show: false,
            changereportt: 0,
          
            arabic: '',
            english: '',
            fromDate: '',
            toDate: '',
            paymentWiseOpeningClosingModel:[],
            transactions: [],
            render: 0
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
        
            this.fromDate = '';
            this.toDate = '';
           
            this.render++
        },
      
        GetTransactions: function () {

            debugger;
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            this.loading = true;
            root.$https.get('/Benificary/GetTransactionReport?fromDate=' + this.fromDate + '&toDate=' + this.toDate, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {
                    debugger;
                    if (response.data) {
                        root.transactions = response.data;
                        root.paymentWiseOpeningClosingModel=response.data;
                    } else {
                        console.log("error: something wrong from db.");
                    }
                },
                    function (error) {
                      
                        console.log(error);
                    });
                },
        PrintRdlc: function () {
            debugger;
            if(this.fromDate == '' && this.toDate == '' )
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
                this.reportsrc1 = this.$ReportServer + '/Invoice/A4_DefaultTempletForm.aspx?companyId=' + companyId +  '&fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&formName=transactionreport' 
                this.changereportt++;
                this.show = !this.show;
                this.loading = false

        }
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