<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('Funds.Funds') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Home') }}</a>
                                    </li>
                                    <li class="breadcrumb-item active">{{ $t('Funds.FundsList') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="openmodel" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary mx-1" v-if ="roleName != 'User'">
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
                        <input v-model="search" type="text" class="form-control" :placeholder="$t('Funds.SearchByCharityResourceNameAndAmount')"
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
                                        {{ $t('Payment.Code') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Funds.CharityResource') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Funds.Amount') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Funds.TransactionType') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Funds.Date') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Benificary.Action') }}
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
                                            <a href="javascript:void(0)" v-on:click="EditFunds(brand.id)">{{ brand.code }}</a>
                                        </strong>
                                    </td>
                                    <td class="text-center">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditFunds(brand.id)">{{ brand.charityResouceName }}</a>
                                        </strong>
                                    </td>
                                    <td class="text-center">
                                        {{ parseFloat(brand.amount).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,"$1,") }}
                                    </td>
                                    <td class="text-center">
                                        {{ brand.typeOfTransaction }}
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
                                                v-on:click="PrintRdlc(brand.id,false)">{{ $t('Payment.Print') }}</a>
                                            <a class="dropdown-item" href="javascript:void(0)"
                                                v-on:click="PrintRdlc(brand.id,true)">{{ $t('Benificary.PDF') }}</a>
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
            <funds-mod :brand="newFunds" :show="show" v-if="show" @close="IsSave" :type="type" />
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
        user:'',
            show: false,
            fundslist: [],
            newFunds: {
                id: '',
                description: '',
                amount: '',
                userId:'',
                code:'',
                charityResouceId:'',
                typeOfTransaction:''
            },
            type: '',
            search: '',
            currentPage: 1,
            pageCount: '',
            rowCount: '0',
            arabic: '',
            english: '',
            roleName: '',
        }
    },
    watch: {
        search: function (val) {
            this.GetFunds(val, 1);
        }
    },
    methods: {
        IsSave: function () {
            this.show = false;
            this.GetFunds(this.search, this.currentPage);
        },

        getPage: function () {
            this.GetFunds(this.search, this.currentPage);
        },

        GotoPage: function (link) {
            this.$router.push({ path: link });
        },

        openmodel: function () {
            this.newFunds = {
                id: '00000000-0000-0000-0000-000000000000',
                description: '',
                amount: 0,
                userId:'',
                code:'',
                charityResouceId:'',
                typeOfTransaction:''
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
                this.reportsrc1 = this.$ReportServer + '/Invoice/A4_DefaultTempletForm.aspx?id=' +val+'&CompanyID='+companyId+'&formName=Funds'+ '&isDownload=' + isDownload
                this.changereportt++;
                this.show1 = !this.show1;
           }
        },
        GetFunds: function () {
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.$https.get('Benificary/GetFundsList?pageNumber=' + this.currentPage + '&searchTerm=' + this.search, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.fundslist = response.data.results;
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;
                    root.loading = false;
                }
                root.loading = false;
            });
        },

        EditFunds: function (Id) {

            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Benificary/GetFundsDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data) {
                        root.newFunds= response.data;
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
        this.GetFunds(this.search, 1);
        this.roleName = localStorage.getItem('RoleName');
    }
}
</script>