<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">Payment Summary</h4>
                               
                            </div>
                            
                        </div>
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-header">
                    <div class="row">
                       
                       
                        <div class="col-sm-2">
                            <datepicker v-model="fromDate" :key="render" />
                        </div>
                        <div class="col-sm-2">
                            <datepicker v-model="toDate"  :key="render" />
                        </div>
                        <div class="col-sm-3 col-md-8">
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
                                 
                                        <th width="200px" >
                                            BENEFICIARY NAME
                                        </th>
                                        <th width="100px" >
                                            BENEFICIARY ID
                                        </th>
                                        <th >
                                            PAYMENT
                                        </th>
                                        <th >
                                           PAYMENT DATE
                                        </th>
                                     
                                       
                                        <th width="100px" >
                                          STATUS
                                        </th>
                                        <th >
                                            PERIOD(MONTH)
                                        </th>
                                       
                                        <th width="200px"  >
                                            PAYMENT PERIOD
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    
                                    <tr >
                                        <td ><b>OPENING BALANCE</b></td>
                                        <td >  </td>
                                    <td >{{transactions.openingBalance}}  </td>
                               
                                    <td >  </td>
                                    <td >  </td>
                                    <td >  </td>
                                    <td >  </td>
                                   
                                    </tr>

                                    
                                    
                                    <tr v-for="(item,index) in transactions.charitylist" v-bind:key="index">
                                        
                                       

                                        <td>
                                            {{item.beneficaryName}}
                                        </td>
                                        <td>
                                            {{item.beneficaryId}}
                                        </td>
                                        <td> {{item.amount}}
                                            </td>
                                        <td>
                                            {{item.paymentDate}}
                                        </td>
                                        
                                        
                                        <td>
                                            {{item.isRegistered}}
                                        </td>
                                       
                                       
                                        <td>
                                            {{item.paymentType}}
                                        </td>
                                        <td>
                                            {{item.paymentMonth}}    
                                        </td>
                                      
                                       

                                        
                                       
                                    </tr>
                                    <tr >
                                        <td ><b>TOTAL</b></td>
                                        <td >  </td>
                                    <td >{{transactions.transactionTotal}}   </td>
                                  
                                    <td >  </td>
                                    <td >  </td>
                                    <td >  </td>
                                    <td >  </td>
                                    
                                    </tr>

                                    <tr >
                                        <td ><b> BALANCE</b></td>
                                        <td >  </td>
                                    <td >  {{transactions.closing}}</td>
                                 
                                    <td >  </td>
                                    <td >  </td>
                                    <td >  </td>
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
                this.reportsrc1 = this.$ReportServer + '/Invoice/A4_DefaultTempletForm.aspx?companyId=' + companyId +  '&fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&formName=charityfundsreports' 
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