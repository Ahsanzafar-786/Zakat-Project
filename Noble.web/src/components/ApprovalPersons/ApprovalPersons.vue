<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('ApprovalPerson.ApprovalPerson') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Home') }}</a>
                                    </li>
                                    <li class="breadcrumb-item active">{{ $t('ApprovalPerson.ApprovalPersonList') }}</li>
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
                        <input v-model="search" type="text" class="form-control" :placeholder="$t('ApprovalPerson.SearchByNameAndId')"
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
                                        {{ $t('ApprovalPerson.ApprovalPersonId') }}
                                    </th>
                                    <th class="text-start">
                                        {{ $t('ApprovalPerson.Name') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('ApprovalPerson.Status') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(brand, index) in approvalPersonlist" v-bind:key="brand.id">
                                    <td v-if="currentPage === 1">
                                        {{ index + 1 }}
                                    </td>
                                    <td v-else>
                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                    </td>

                                    <td class="text-start">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditapprovalPerson(brand.id)">{{ brand.aprovalPersonId }}</a>
                                        </strong>
                                    </td>
                                    <td class="text-start" v-if="brand.name != ''">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditapprovalPerson(brand.id)"> {{ brand.name }}</a>
                                        </strong>
                                    </td>
                                    <td class="text-start" v-else>
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditapprovalPerson(brand.id)"> {{ brand.nameAr }}</a>
                                        </strong>
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
                        <div class="" v-on:click="GetapprovalPerson()">
                            <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount" :per-page="10"
                                :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')"
                                :next-text="$t('Table.Next')" :last-text="$t('Table.Last')"></b-pagination>
                        </div>
                    </div>
                </div>
            </div>

            <approvalperson-mod :brand="newapprovalPerson" :show="show" v-if="show" @close="IsSave" :type="type" />
        </div>

    </div>
</template>


<script>
import clickMixin from '@/Mixins/clickMixin'
export default {
    mixins: [clickMixin],
    data: function () {
        return {
            roleName:'',
            show: false,
            approvalPersonlist: [],
            newapprovalPerson: {
                id: '',
                name: '',
                nameAr: '',
                email: '',
                aprovalPersonId: 0,
                phoneNo: '', 
                isActive:true,
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
            this.GetapprovalPerson(val, 1);
        }
    },
    methods: {
        IsSave: function () {
            this.show = false;
            this.GetapprovalPerson(this.search, this.currentPage);
        },

        getPage: function () {
            this.GetapprovalPerson(this.search, this.currentPage);
        },

        GotoPage: function (link) {
            this.$router.push({ path: link });
        },

        openmodel: function () {
            this.newapprovalPerson = {
                id: '00000000-0000-0000-0000-000000000000',
                name: '',
                nameAr: '',
                email: '',
                aprovalPersonId: 0,
                phoneNo: '',
                isActive:true,  
            }
            this.show = !this.show;
            this.type = "Add";
        },

        GetapprovalPerson: function () {
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.$https.get('Benificary/GetApprovalPersonsList?pageNumber=' + this.currentPage + '&searchTerm=' + this.search, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.approvalPersonlist = response.data.results;
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;
                    root.loading = false;
                }
                root.loading = false;
            });
        },

        EditapprovalPerson: function (Id) {

            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Benificary/GetApprovalPersonsDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data) {
                        debugger; //eslint-disable-line
                        root.newapprovalPerson= response.data;
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
        this.GetapprovalPerson(this.search, 1);
        this.roleName = localStorage.getItem('RoleName');
    }
}
</script>