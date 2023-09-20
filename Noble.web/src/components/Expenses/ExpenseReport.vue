<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">Expense Report</h4>
                               
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
                <!-- <div class="card-header">
                    <div class="row">
                       
                       
                        <div class="col-lg-4">
                       
                            <button style="float: right;" class="btn btn-outline-primary " v-on:click="PrintRdlc()">{{ $t('Print') }}</button>

                        </div>

                    </div>
                </div> -->
                <div class="card-body" >
                  
                    <div class="table-responsive">
                            <table class="table mb-0" style="table-layout: fixed; ">
                                <thead class="thead-light">
                                    <tr>
                                        <th >
                                            No#
                                        </th>
                                        
                                        <th >
                                            DATE
                                        </th>
                                        <th  >
                                            Expense Category
                                        </th>
                                        <th  >
                                            Expense Description
                                        </th>
                                        <th  >
                                           Amount
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    
                                    <tr v-for="(item,index) in transactions.expenseList" v-bind:key="index">
                                        <td >
                                        {{ index + 1 }}
                                    </td>
                                        <td > {{item.date}} </td>
                                    <td >{{item.expenseCategoryName}}</td>
                                    <td > {{item.description}}</td>

                                        <td>{{item.amount}}</td>
                                    </tr>
                                    
                                    <tr >
                                    
                                    <td ><b>TOTAL</b></td>
                                    <td >  </td>
                                    <td >  </td><td >  </td>
                                    <td >{{transactions.expenseTotal}}  </td>
                                        
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
        // ClearFilter: function () {
        
        //     this.fromDate = '';
        //     this.toDate = '';
           
        //     this.render++
        // },
      
        GetExpenseReport: function () {

            debugger;
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            this.loading = true;
            root.$https.get('/Benificary/GetExpenseReport' , {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {
                    debugger;
                    if (response.data) {
                        root.transactions = response.data;
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
                this.reportsrc1 = this.$ReportServer + '/Invoice/A4_DefaultTempletForm.aspx?companyId=' + companyId +'&formName=Expensereport' 
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
       this.GetExpenseReport();
    }
}
</script>