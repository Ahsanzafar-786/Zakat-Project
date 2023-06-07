<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('Payment.Payment') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Home') }}</a>
                                    </li>
                                    <li class="breadcrumb-item active">{{ $t('Payment.PaymentList') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="GotoPage('/addpayment')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary mx-1" v-if ="UserRole == 'User'">
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
                        <input v-model="search" type="text" class="form-control" :placeholder="$t('Search')"
                            aria-label="Example text with button addon" aria-describedby="button-addon1">
                    </div>

                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>#</th>
                                    <!-- <th class="text-center">
                                        ID
                                    </th> -->
                                    <th class="text-center">
                                        {{ $t('Payment.Code') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Payment.BenifcaryName') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Payment.Amount') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Payment.Month') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Payment.Year') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Payment.Period') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(brand, index) in paymentList" v-bind:key="brand.id">
                                    <td v-if="currentPage === 1">
                                        {{ index + 1 }}
                                    </td>
                                    <td v-else>
                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                    </td>

                                    <td class="text-center">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditPayment(brand.id)">{{ brand.paymentCode }}</a>
                                        </strong>
                                    </td>
                                    <td class="text-center">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditPayment(brand.id)">{{ brand.benificaryName }}</a>
                                        </strong>
                                    </td>
                                     <td class="text-center">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditPayment(brand.id)"> {{ brand.amount }}</a>
                                        </strong>
                                    </td>
                                    <td class="text-center" v-if="brand.month != null">
                                        {{ GetMonth(brand.month) }}
                                    </td>
                                    <td class="text-center" v-else>
                                        {{ GetMonth(brand.date) }}
                                    </td>
                                    <td class="text-center">
                                        {{ brand.year }}
                                    </td>
                                    <td class="text-center">
                                        {{ brand.period }}
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
                        <div class="" v-on:click="GetPayment()">
                            <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount" :per-page="10"
                                :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')"
                                :next-text="$t('Table.Next')" :last-text="$t('Table.Last')"></b-pagination>
                        </div>
                    </div>
                </div>
            </div>

            <authorizedPerson-mod :brand="newauthorizedPerson" :show="show" v-if="show" @close="IsSave" :type="type" />
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
            show: false,
            paymentList: [],
            type: '',
            search: '',
            currentPage: 1,
            pageCount: '',
            rowCount: '0',
            arabic: '',
            english: '',
            user: '',
        }
    },
    watch: {
        search: function (val) {
            this.GetPayment(val, 1);
        }
    },
    methods: {
        GetMonth: function (link) {
           if(link!=undefined)
           {
            return moment(link).format('MMMM');

           } 
           else
           {
            return '';
           }
        },
        getPage: function () {
            this.GetPayment(this.search, this.currentPage);
        },

        GotoPage: function (link) {
            this.$router.push({ path: link });
        },

       

        GetPayment: function () {
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.$https.get('Benificary/GetPaymentsList?pageNumber=' + this.currentPage + '&searchTerm=' + this.search, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.paymentList = response.data.results;
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;
                    root.loading = false;
                }
                root.loading = false;
            });
        },

        EditPayment: function (Id) {
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Benificary/GetPaymentsDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data) {
                        root.$router.push({
                            path: '/addpayment',
                            query: {data:response.data}
                        });
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
        this.GetPayment(this.search, 1);
        this.payment = localStorage.getItem('User');

        
    }
}
</script>