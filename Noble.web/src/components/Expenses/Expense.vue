<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('Expense.Expense') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Home') }}</a>
                                    </li>
                                    <li class="breadcrumb-item active">{{ $t('Expense.ExpenseList') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="openmodel" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary mx-1" >
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
                    <div class="input-group">
                        <button class="btn btn-secondary" type="button" id="button-addon1">
                            <i class="fas fa-search"></i>
                        </button>
                        <input v-model="search" type="text" class="form-control" :placeholder="$t('Expense.SearchByCategoryNameAndAmount')"
                            aria-label="Example text with button addon" aria-describedby="button-addon1">
                    </div>

                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>#</th>
                                    <th class="text-center">
                                        {{ $t('Expense.Code') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Expense.CategoryName') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Expense.Amount') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Expense.Discription') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Expense.Date') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Expense.Action') }}
                                    </th>
                                    
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(brand, index) in fundslist" v-bind:key="brand.id">
                                    <td v-if="currentPage === 1">
                                        {{ index + 1 }}
                                    </td>
                                    <td v-else>
                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                    </td>

                                    <td class="text-center">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditExpense(brand.id)">{{ brand.code }}</a>
                                        </strong>
                                    </td>
                                    <td class="text-center">
                                        
                                    {{ brand.expenseCategoryName }}
                                        
                                    </td>
                                    <td class="text-center">
                                        {{ parseFloat(brand.amount).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,"$1,") }}
                                    </td>
                                    <td class="text-center">
                                        {{ brand.description }}
                                    </td>
                                    <td class="text-center">
                                        {{ brand.date }}
                                    </td>
                                    <td class="text-center d-flex align-items-baseline justify-content-center"
                                        v-if="roleName != 'User'">
                                        <button type="button" class="btn btn-light dropdown-toggle"
                                            data-bs-toggle="dropdown" aria-expanded="false">
                                            {{ $t('Payment.Action') }} <i class="mdi mdi-chevron-down"></i></button>
                                        <div class="dropdown-menu text-center">
                                            <a class="dropdown-item" href="javascript:void(0)"
                                                v-on:click="PrintRdlc()">{{ $t('Payment.Print') }}</a>
                                           
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
                        <div class="" v-on:click="GetFunds()">
                            <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount" :per-page="10"
                                :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')"
                                :next-text="$t('Table.Next')" :last-text="$t('Table.Last')"></b-pagination>
                        </div>
                    </div>
                </div>
            </div>
            <print :show="show1" v-if="show1" :reportsrc="reportsrc1" :changereport="changereportt" @close="show1 = false"
                @IsSave="IsSaveRpt" />
            <expense-mod :brand="newExpense" :show="show" v-if="show" @close="IsSave" :type="type" />
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
            changereport: 0,
            reportsrc: '',
            show1: false,
            show: false,
            fundslist: [],
            newExpense: {
                id: '',
                description: '',
                amount: '',
                expenseCategoryName:'',
                expenseCategoryId:'',
            },
            type: '',
            search: '',
            currentPage: 1,
            pageCount: '',
            rowCount: '0',
            arabic: '',
            english: '',
            
        }
    },
    watch: {
        search: function (val) {
            this.GetExpense(val, 1);
        }
    },
    methods: {
        IsSave: function () {
            this.show = false;
            this.GetExpense(this.search, this.currentPage);
        },

        getPage: function () {
            this.GetExpense(this.search, this.currentPage);
        },

        GotoPage: function (link) {
            this.$router.push({ path: link });
        },

        openmodel: function () {
            this.newExpense = {
                id: '00000000-0000-0000-0000-000000000000',
                description: '',
                amount: 0,
                expenseCategoryName:'',
                expenseCategoryId:'',
            }
            this.show = !this.show;
            this.type = "Add";
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
        GetExpense: function () {
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.$https.get('Benificary/GetExpenseList?pageNumber=' + this.currentPage + '&searchTerm=' + this.search, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.fundslist = response.data.results;
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;
                    root.loading = false;
                }
                root.loading = false;
            });
        },

        EditExpense: function (Id) {

            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Benificary/GetExpenseDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data) {
                        root.newExpense= response.data;
                        root.show = !root.show;
                        root.type = "Edit"
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
        this.GetExpense(this.search, 1);
    }
}
</script>