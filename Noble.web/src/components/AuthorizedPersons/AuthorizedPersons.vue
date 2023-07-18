<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('AuthorizedPerson.AuthorizedPerson') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Home') }}</a>
                                    </li>
                                    <li class="breadcrumb-item active">{{ $t('AuthorizedPerson.AuthorizedPersonList') }}</li>
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
                        <input v-model="search" type="text" class="form-control" :placeholder="$t('AuthorizedPerson.SearchByNameAndID')"
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
                                        {{ $t('AuthorizedPerson.ID') }}
                                    </th>
                                    <th class="text-start">
                                        {{ $t('AuthorizedPerson.Name') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('AuthorizedPerson.Status') }}
                                    </th>
                                    <!-- <th class="text-center">
                                        {{ $t('AuthorizedPerson.ID') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('AuthorizedPerson.Nationality') }}
                                    </th> -->
                                   
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(brand, index) in authorizedPersonlist" v-bind:key="brand.id">
                                    <td v-if="currentPage === 1">
                                        {{ index + 1 }}
                                    </td>
                                    <td v-else>
                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                    </td>

                                    <td class="text-center">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditauthorizedPerson(brand.id)">{{ brand.authorizedPersonCode }}</a>
                                        </strong>
                                    </td>
                                    <td class="text-start">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditauthorizedPerson(brand.id)"> {{ brand.name }}</a>
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
                                    <!-- <td class="text-center">
                                        {{ brand.gender }}
                                    </td>
                                    <td class="text-center">
                                        {{ brand.iqamaNo }}
                                    </td>
                                    <td class="text-center">
                                        {{ brand.nationality }}
                                    </td> -->
                                    
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
                        <div class="" v-on:click="GetauthorizedPerson()">
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
export default {
    mixins: [clickMixin],
    data: function () {
        return {
        user: '',
            show: false,
            authorizedPersonlist: [],
            newauthorizedPerson: {
                id: '',
                name: '',
                nameAr: '',
                authorizedPersonCode: 0,
                phoneNo: '',
                address: '', 
                passportNo: '',
                nationality: '',
                gender: '',
                iqamaNo: '',
                isActive:true
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
            this.GetauthorizedPerson(val, 1);
        }
    },
    methods: {
        IsSave: function () {
            this.show = false;
            this.GetauthorizedPerson(this.search, this.currentPage);
        },

        getPage: function () {
            this.GetauthorizedPerson(this.search, this.currentPage);
        },

        GotoPage: function (link) {
            this.$router.push({ path: link });
        },

        openmodel: function () {
            this.newauthorizedPerson = {
                id: '00000000-0000-0000-0000-000000000000',
                name: '',
                nameAr: '',
                authorizedPersonCode: 0,
                phoneNo: '',
                address: '', 
                passportNo: '',
                nationality: '',
                gender: 'Male',
                iqamaNo: '',
                isActive:true
            }
            this.show = !this.show;
            this.type = "Add";
        },

        GetauthorizedPerson: function () {
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.$https.get('Benificary/GetAuthorizedPersonsList?pageNumber=' + this.currentPage + '&searchTerm=' + this.search, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.authorizedPersonlist = response.data.results;
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;
                    root.loading = false;
                }
                root.loading = false;
            });
        },

        EditauthorizedPerson: function (Id) {

            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Benificary/GetAuthorizedPersonsDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data) {
                        root.newauthorizedPerson= response.data;
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
        this.GetauthorizedPerson(this.search, 1);
        this.roleName = localStorage.getItem('RoleName');
    }
}
</script>