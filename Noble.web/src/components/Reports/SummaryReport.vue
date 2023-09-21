<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">Summary Report</h4>
                               
                            </div>
                            <div class="col-auto align-self-center">
                                
                                <a v-on:click="PrintRdlc()" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary">
                                    {{ $t('Print') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card">


                
                <div class="card-body" >
                  
                    <div class="table-responsive">
                            <table class="table mb-0" style="table-layout: fixed; ">
                                <thead class="thead-light">
                                    <tr>
                                 
                                        
                                        <th  >
                                            
                                        </th>
                                        <th  >
                                            
                                        </th>
                                        <th >
                                         <b>Summary Report</b>  
                                        </th>
                                        <th  >
                                          
                                        </th>
                                        <th  >
                                           
                                        </th>
                                       
                                        <th  >
                                          
                                        </th>
                                        
                                       

                                    </tr>
                                </thead>
                                <tbody>
                                    
                                    <tr >
                                    <td >  </td>
                                    <td > <b> Funds Received</b> </td>
                                         <td > </td>
                                         <td > </td>
                                    <td >  </td>
                                    <td > {{SummaryReportLookupModel.fundsReceived}} </td>
                                    
                               
                                    
                                    </tr>
                                    <tr >
                                        
                                        <td >  </td>
                                        <td > <b>Total Expense</b> </td>
                                             <td></td>
                                             <td >  </td>
                                             <td >  </td>
                                             <td >  {{SummaryReportLookupModel.totalExpense}}  </td>
                                        </tr>
                                        
                                    <tr >
                                        
                                    <td >  </td>
                                    <td > </td>
                                         <td></td>
                                         <td >  <b>Balance</b> </td>
                                         <td >  </td>
                                         <td >  {{SummaryReportLookupModel.balance}}  </td>
                                    </tr>
                                    
                                    <tr >
                                        <td >  </td>
                                         <td >  </td>
                                         <td >  </td>
                                         <td ><b>Payment Details</b></td>
                                       <td >  </td>
                                        <td >   </td>
                        
                                    </tr>

                                       <tr >
                                        
                                        <td >  </td>
                                       
                                             <td><b>Beneficiary Monthly</b></td>
                                             <td >  </td>
                                             <td >  </td>
                                             <td >  </td> 
                                             <td >  {{SummaryReportLookupModel.charitymonthly}}  </td>
                                        </tr>

                                        <tr >
                                        
                                        <td >  </td>
                                    
                                             <td><b>Beneficiary Daily</b></td>
                                             <td >  </td>
                                             <td >  </td>
                                             <td >  </td>
                                             <td >  {{SummaryReportLookupModel.charitydaily}}  </td>
                                        </tr>
                                    
                                    <tr >
                                        <td >  </td>
                                    <td >  </td>  
                                     <td >  </td> 
                                       <td >  </td>
                                    <td ><b> TOTAL Payments</b></td>
                                    <td >   {{SummaryReportLookupModel.totalPayments}} </td>
                                  
                                    </tr>
                                    <tr >
                                        <td >  </td>
                                    <td >  </td>  
                                     <td >  </td> 
                                       <td >  </td>
                                    <td ><b> Cash In Hand</b></td>
                                    <td >  {{SummaryReportLookupModel.cashInHand}}  </td>
                                  
                                    </tr>
                                    
                                </tbody>
                            </table><!--end /table-->
                        </div><!--end /tableresponsive-->
                </div>
            </div>
        </div>
        <print :show="show" v-if="show1" :reportsrc="reportsrc1" :changereport="changereportt" @close="show1 = false"
            @IsSave="IsSave" />
    </div>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'

import moment from 'moment';

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
            show1: false,
            changereportt: 0,
           

            arabic: '',
            english: '',
            SummaryReportLookupModel: {

            fundsReceived :'',
             totalExpense:'',
             balance :'',
             charitydaily:'', 
            charitymonthly:'',
             totalPayments :'',
             cashInHand :'',

            },
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
        
      
        GetExpenseSummery: function () {

            debugger;
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            this.loading = true;
            root.$https.get('/Benificary/GetExpenseSummery', {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {
                    debugger;
                    if (response.data) {
                        
                        root.SummaryReportLookupModel=response.data;
                  
                       
                    } else {
                        console.log("error: something wrong from db.");
                    }
                },
                    function (error) {
                      
                        console.log(error);
                    });
                },
                IsSaveRpt: function () {
            this.show1 = !this.show1;
        },
                PrintRdlc: function (val,isDownload) {
            debugger;
            var companyId = '';
            if (this.$session.exists()) {
                companyId = localStorage.getItem('CompanyID');
            }
            var root=this;
            if (isDownload) {
                this.loading=true;
                this.$https.get(this.$ReportServer + '/Invoice/A4_DefaultTempletForm.aspx?id=' +val+'&CompanyID='+companyId+'&formName=Funds'+ '&isDownload=' + isDownload
                , {  responseType: 'blob' } ) .then(function (response) {
                       debugger;
                        root.loading=false;
                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;
                        var date = moment().format('DD MMM YYYY');
                        link.setAttribute('download','benificary ' + date + '.pdf');
                        document.body.appendChild(link);
                        link.click();

                    })
            }

           else{
                this.reportsrc1 = this.$ReportServer + '/Invoice/A4_DefaultTempletForm.aspx?id=' +val+'&CompanyID='+companyId+'&formName=Expense'+ '&isDownload=' + isDownload+ '&searchTerm=' + this.search+ '&pageNumber=' + this.currentPage
                this.changereportt++;
                this.show1 = !this.show1;
           }
        },
    },

    created: function () {
        this.$emit('input', this.$route.name);
    },
    mounted: function () {
        this.english = localStorage.getItem('locals');
        this.arabic = localStorage.getItem('locals');
       this.GetExpenseSummery();
    }
}
</script>