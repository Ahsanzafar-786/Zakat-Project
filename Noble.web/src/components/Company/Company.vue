<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('Company') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Home') }}</a></li>
                                    <li class="breadcrumb-item active">Company</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="AddCompany" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('Add New') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('Close') }}
                                </a>
                            </div>
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
                    <input v-model="searchQuery" type="text" class="form-control"
                        placeholder="Search by Name, VAT, Reg, Client No" aria-label="Example text with button addon"
                        aria-describedby="button-addon1">
                </div>

            </div>
            <div class="card-body">
                <div class="table-responsive">
                    <table class="table mb-0">
                        <thead class="thead-light table-hover">
                            <tr>
                                <th>#</th>
                                <th>Name </th>
                                <th>Reg No</th>
                                <th>VAT No</th>
                                <th>Client Code</th>
                                <th>Land Line</th>
                                <th>From Date</th>
                                <th>To Date</th>
                                <th>Type</th>
                                <!--<th>Action</th>-->
                                <th>Permissions</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="(company, index) in resultQuery" v-bind:key="company.id">
                                <td>
                                    {{ index + 1 }}
                                </td>
                                <td>
                                    <strong>
                                        <a href="javascript:void(0)" v-on:click="EditCompany(company.id)">{{
                                            company.nameEnglish }}</a>
                                    </strong>
                                </td>
                                <td>{{ company.companyRegNo }}</td>
                                <td>{{ company.vatRegistrationNo }}</td>
                                <td>{{ company.clientNo }}</td>
                                <td>{{ company.landLine }}</td>
                                <td>
                                    <span v-if="company.fromDate == null">

                                    </span>
                                    <span v-else>
                                        {{ company.fromDate | formatDate }}
                                    </span>
                                </td>
                                <td>
                                    <span v-if="company.toDate == null">

                                    </span>
                                    <span v-else>
                                        {{ company.toDate | formatDate }}
                                    </span>
                                </td>
                                <td>{{ company.companyType }}</td>
                                <td>
                                    <strong>
                                        <button type="button" class="btn btn-primary  "
                                            v-on:click="AddLicence(company.id, company.nameEnglish, company.companyLicenceId, company.companyLicences)">
                                            <span
                                                v-if="company.companyType == null || company.companyType == '' || company.companyType == undefined">
                                                Add Licence
                                            </span>
                                            <span v-else>
                                                Update Licence
                                            </span>
                                        </button>
                                    </strong>
                                    <a href="javascript:void(0)" class="btn btn-danger btn-sm btn-icon "
                                        v-on:click="showLicenceHistory(company.nameEnglish, company.companyLicences)"
                                        title="Show Licence History"><i class=" fa fa-history"></i></a>

                                    <!--<a href="javascript:void(0)" class="btn btn-danger btn-sm btn-icon " v-on:click="RemoveCompany(company.id)"><i class=" fa fa-trash"></i></a>-->
                                </td>
                                <td>
                                    <strong>
                                        <button type="button" class="btn btn-primary  "
                                            v-on:click="AddBusiness(company.id)">
                                            <span>
                                                Bus
                                            </span>
                                        </button>
                                    </strong>
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
                    <div class="" v-on:click="GetCompanyData()">
                        <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount" :per-page="10"
                            :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')" :next-text="$t('Table.Next')"
                            :last-text="$t('Table.Last')"></b-pagination>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import moment from "moment";

export default {
    name: 'company',
    data: function () {
        return {
            companylist: [],
            searchQuery: '',
            show: false,
            type: '',
            companyId: null,
            companyName: '',
            companyLicenceId: '',
            companyLicenceList: [],
            showHistory: false,
            roleDetailsForPermissions: [],
            typePermissions: '',
            showPermissions: false,
            updatePermissions: false

        }
    },
    computed: {
        resultQuery: function () {
            var root = this;
            if (this.searchQuery) {
                return root.companylist.filter((companylist) => {

                    return root.searchQuery.toLowerCase().split(' ').every(v => companylist.nameEnglish.toLowerCase().includes(v) || companylist.clientNo.toLowerCase().includes(v) || companylist.vatRegistrationNo.toLowerCase().includes(v) || companylist.companyRegNo.toLowerCase().includes(v))
                })
            } else {
                return root.companylist;
            }
        },
    },
    filters: {
        formatDate: function (value) {
            return moment(value).format("DD MMM yyyy");
        }
    },
    methods: {
        StartScreen: function () {
            this.$router.push('/StartScreen')
        },
        AddBusiness: function (companyId) {
            this.$router.push({
                path: '/addbusiness',
                query: { data: companyId }
            })
        },
        closeModal: function () {
            this.show = false;
            this.GetCompanyData();
        },
        AddLicence: function (id, name, companyLicenceId, licenceList) {
            this.companyLicenceList = [];
            this.companyName = name;
            this.companyId = id;
            this.companyLicenceId = companyLicenceId;
            this.show = !this.show;
            this.companyLicenceList = licenceList;
            this.type = "Add";
        },
        showLicenceHistory: function (name, licenceList) {
            this.companyLicenceList = [];
            this.companyName = name;
            this.showHistory = !this.showHistory;
            this.companyLicenceList = licenceList;
        },
        AddPermissions: function (id) {
            this.roleDetailsForPermissions = {
                roleId: '00000000-0000-0000-0000-000000000000',
                name: '',
                allowAll: false,
                removeAll: false,
                moduleId: '',
                companyId: id,
                id: '00000000-0000-0000-0000-000000000000'
            }
            this.showPermissions = !this.showPermissions;
            this.typePermissions = "Add";
        },
        UpdatePermissions: function (id) {
            this.roleDetailsForPermissions = {
                roleId: '00000000-0000-0000-0000-000000000000',
                name: '',
                allowAll: false,
                removeAll: false,
                moduleId: '',
                companyId: id,
                id: '00000000-0000-0000-0000-000000000000'
            }
            this.updatePermissions = !this.updatePermissions;
            this.typePermissions = "Add";
        },
        GetCompanyData: function () {

            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            this.$https.get('/Company/List', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {

                    root.$store.state.companyList.push(response.data)
                    root.companylist = response.data;
                }
            });
        },
        EditCompany: function (Id) {
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            this.$https.get('/Company/EditCompany?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {

                    root.companylist = response.data
                }
                root.$router.push({
                    path: '/AddCompany',
                    query: {
                        data: root.companylist
                    }
                })
            });
        },
        RemoveCompany: function (id) {
            var root = this;
            // working with IE and Chrome both
            this.$swal({
                title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Are you sure?' : 'هل أنت متأكد؟',
                text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'You will not be able to recover this!' : 'لن تتمكن من استرداد هذا!',
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Yes, delete it!' : 'نعم ، احذفها!',
                closeOnConfirm: false,
                closeOnCancel: false
            }).then(function (result) {
                if (result) {
                    var token = '';
                    if (this.$session.exists()) {
                        token = localStorage.getItem('token');
                    }
                    this.$https.post('/Company/Delete?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(function (response) {
                            if (response.data) {
                                root.$store.state.companyList.splice(root.$store.state.companyList.findIndex(function (i) {
                                    return i.id === response.data;
                                }), 1);
                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Deleted!' : 'تم الحذف!',
                                    text: 'Comapany has been Deleted.',
                                    type: 'success',
                                    confirmButtonClass: "btn btn-success",
                                    buttonsStyling: false
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
                else {
                    this.$swal((this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Cancelled!' : 'ألغيت!', (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Your file is still intact!' : 'ملفك لا يزال سليما!', (this.$i18n.locale == 'en' || root.isLeftToRight()) ? 'info' : 'معلومات');
                }
            });
        },
        AddCompany: function () {
            this.$router.push('/AddCompany')
        },
    },
    mounted: function () {
        this.GetCompanyData();
    }
}
</script>