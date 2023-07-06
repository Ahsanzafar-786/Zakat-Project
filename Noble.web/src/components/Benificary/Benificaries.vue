<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('Benificary.Benificary') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Home') }}</a>
                                    </li>
                                    <li class="breadcrumb-item active">{{ $t('Benificary.BenificaryList') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="openmodel" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary mx-1" v-if="roleName != 'User'">
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
                    <div class="row">
                        <div class="col-4">
                            <input v-model="search" type="text" class="form-control" :placeholder="$t('Benificary.Search')"
                                aria-label="Example text with button addon" aria-describedby="button-addon1">
                        </div>
                        <div class="col-4">
                            <input v-model="beneficiaryId" type="text" class="form-control" :placeholder="$t('Benificary.SearchByID')"
                                aria-label="Example text with button addon" aria-describedby="button-addon1">
                        </div>
                        <div class="col-4">
                            <input v-model="uqamaNo" type="text" class="form-control"
                                :placeholder="$t('Benificary.SearchByUqamaNo')" aria-label="Example text with button addon"
                                aria-describedby="button-addon1">
                        </div>
                        <div class="col-md-4 form-group">
                            <label class="text  font-weight-bolder">
                                {{ $t('Benificary.AuthorizedPerson') }}
                            </label>
                            <authorizedperson v-model="authorizationPersonId" ref="AuthorizedDropdown" />
                        </div>
                        <div class="col-md-4 form-group">
                            <label class="text  font-weight-bolder">
                                {{ $t('Benificary.ApprovalPerson') }}
                            </label>
                            <approvalperson v-model="approvalPersonId"/>
                        </div>
                        <div class="col-md-3 form-group">
                            <label class="text  font-weight-bolder">
                               {{ $t('Benificary.Register/Un-Register') }}
                            </label>
                            <multiselect v-model="registered" 
                            :options="['Register', 'Un-Register']" :show-labels="false"
                            :placeholder="$t('AddBenificary.SelectType')">
                        </multiselect>
                        </div>
                        <div class="col-3">
                            <a v-on:click="SearchFilter" href="javascript:void(0);"
                                class="btn btn-sm btn-outline-primary mx-1">
                                {{ $t('Benificary.SearchFilter') }}
                            </a>
                            <a @click="ClearFilter" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                                {{ $t('Benificary.ClearFilter') }}
                            </a>

                        </div>



                    </div>

                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>#</th>
                                    <th class="text-center">
                                        {{ $t('Benificary.ID') }}
                                    </th>
                                    <th class="text-start">
                                        {{ $t('Benificary.Name') }}
                                    </th>
                                    <th class="text-start">
                                        {{ $t('Benificary.AuthorizePersonName') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Benificary.AmountPerMonth') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('AddBenificary.RecurringAmount') }}
                                    </th>
                                    <th class="text-center" v-if="roleName=='Admin'">
                                        Approval Type
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Benificary.Status') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Benificary.BenificaryStatus') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(brand, index) in benificarylist" v-bind:key="brand.id">
                                    <td v-if="currentPage === 1">
                                        {{ index + 1 }}
                                    </td>
                                    <td v-else>
                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                    </td>

                                    <td class="text-center">
                                        {{ brand.beneficiaryId }}
                                    </td>
                                    
                                    <td class="text-start">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditBenificary(brand.id)"> {{
                                                brand.name == '' ? brand.nameAr : brand.name }}</a>
                                        </strong>
                                    </td>
                                    
                                    <td class="text-start">
                                        <span v-for="item in brand.benificaryAuthorization" :key="item.id" class="mx-2">
                                            {{ item.authorizationPersonName == '' ? item.authorizationPersonNameAr :
                                                item.authorizationPersonName }}
                                        </span>
                                    </td>
                                    <td class="text-center">
                                        {{ brand.amountPerMonth }}
                                    </td>
                                    <td class="text-center">
                                        {{ brand.recurringAmount }}
                                    </td>
                                    <td class="text-center" v-if="roleName=='Admin'" >
                                          <span class="badge badge-boxed  badge-outline-danger" v-if="brand.approvalStatus=='4'">Waiting For Approved</span>
                                          <span class="badge badge-boxed  badge-outline-success" v-if="brand.approvalStatus=='3'">Approved</span>
                                        </td>
                                    <td class="text-center">
                                      
                                        <span v-if="brand.isActive" class="badge badge-boxed  badge-outline-success">
                                            {{
                                                $t('Benificary.Active')
                                            }}
                                        </span>
                                        <span v-else class="badge badge-boxed  badge-outline-danger">

                                            {{
                                                $t('Benificary.DeActive')
                                            }}
                                        </span>
                                    </td>
                                    <td class="text-center">
                                        <span v-if="brand.isRegister" class="badge badge-boxed  badge-outline-success">
                                            {{ $t('Benificary.Register') }}
                                        </span>
                                        <span v-else class="badge badge-boxed  badge-outline-danger">
                                            {{ $t('Benificary.UnRegister') }}
                                        </span>
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
                        <div class="" v-on:click="GetBenificaryData()">
                            <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount" :per-page="10"
                                :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')"
                                :next-text="$t('Table.Next')" :last-text="$t('Table.Last')"></b-pagination>
                        </div>
                    </div>
                </div>
            </div>

            <benificary-mod :brand="newBenificary" :show="show" v-if="show" @close="IsSave" :type="type" />
        </div>

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
            user: '',
            show: false,
            roleName: '',
            benificarylist: [],
            newBenificary: {
                id: '',
                name: '',
                nameAr: '',
                gender: '',
                beneficiaryId: 0,
                paymentIntervalMonth: 0,
                amountPerMonth: 0,
                recurringAmount: 0,
                ugamaNo: '',
                phoneNo: '',
                note: '',
                authorizedPersonId: '',
                approvalPersonId: '',
                paymentTypeId: '',
                isActive: true,
                isRegister: true,
                address: '',
                approvalStatus: '',
                passportNo: '',
                reason: '',
                nationality: '',
                startMonth: '',
                startDate: '',
                endDate: '',
                isDisable: false,
                approvedPaymentId: '',
                advancePayment: 0,
                durationType: '',
                benificaryAuthorization: [{
                    id: '',
                    benficaryId: '',
                    authorizationPersonId: '',
                    approvalPersonId: '',
                    date: '',
                    description: '',
                    isActive: false
                }],
            },
            type: '',
            search: '',
            currentPage: 1,
            pageCount: '',
            rowCount: '0',
            arabic: '',
            english: '',
            benificaryName: '',
            uqamaNo: '',
            beneficiaryId:'',
            authorizationPersonId: '',
                approvalPersonId: '',
                registered: '',
        }
    },
    // watch: {
    //     search: function (val) {
    //         this.GetBenificaryData(val, 1);
    //     }
    // },
    methods: {
        ClearFilter() {
            debugger;

            // Reset the filter conditions here
            this.search = '';
            this.uqamaNo = '';
            this.beneficiaryId = '';
            this.authorizationPersonId = '';
            if(this.$refs.AuthorizedDropdown!=undefined)
            this.$refs.AuthorizedDropdown.Remove();
                this.approvalPersonId = '';
                this.registered = '';

            // Trigger the search or data refresh
            this.GetBenificaryData(this.currentPage);
        },


        SearchFilter: function () {
            debugger;
            this.GetBenificaryData(this.currentPage);
        },


        IsSave: function () {
            this.show = false;
            this.GetBenificaryData(this.search, this.currentPage);
        },

        getPage: function () {
            this.GetBenificaryData(this.currentPage);
        },

        GotoPage: function (link) {
            this.$router.push({
                path: link
            });
        },

        openmodel: function () {
            this.newBenificary = {
                id: '00000000-0000-0000-0000-000000000000',
                name: '',
                nameAr: '',
                gender: 'Male',
                beneficiaryId: 0,
                paymentIntervalMonth: 0,
                amountPerMonth: 0,
                recurringAmount: 0,
                ugamaNo: '',
                phoneNo: '',
                note: '',
                approvalStatus: '',
                isDisable: false,
                authorizedPersonId: '',
                approvalPersonId: '',
                paymentTypeId: '',
                isActive: true,
                isRegister: true,
                address: '',
                passportNo: '',
                reason: '',
                nationality: '',
                startMonth: '',
                startDate: '',
                endDate: '',
                approvedPaymentId: '',
                advancePayment: 0,
                durationType: 'Customize',
                authorizationPersonName: '',
                benificaryAuthorization: [{
                    id: '',
                    benficaryId: '',
                    authorizationPersonId: '',
                    approvalPersonId: '',
                    date: '',
                    description: '',
                    isActive: true
                }],
            }
            this.show = !this.show;
            this.type = "Add";
        },

        GetBenificaryData: function () {
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            debugger; //eslint-disable-line
            root.$https.get('Benificary/GetBenificaryList?pageNumber=' + this.currentPage + '&searchTerm=' + this.search + '&beneficiaryId=' + this.beneficiaryId + '&uqamaNo='+ this.uqamaNo + '&authorizationPersonId='+ this.authorizationPersonId + '&approvalPersonId='+ this.approvalPersonId + '&registered='+ this.registered  , {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            }).then(function (response) {
                if (response.data != null) {
                    root.benificarylist = response.data.results;
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;
                    root.loading = false;
                }
                root.loading = false;
            });
        },

        EditBenificary: function (Id) {

            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Benificary/GetBenificaryDetail?Id=' + Id, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {
                    if (response.data) {
                        root.newBenificary = response.data;
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
        this.GetBenificaryData(this.currentPage);
        this.roleName = localStorage.getItem('RoleName');
    }
}
</script>
