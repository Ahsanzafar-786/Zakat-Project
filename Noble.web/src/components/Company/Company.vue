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
                                <a v-on:click="GotoPage('/dashboard')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <hr class="hr-dashed hr-menu mt-0" />


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
                                <th>CR No</th>
                                <th>VAT No</th>
                                <th>Address</th>
                                
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
                                <td>{{ company.addressEnglish }}</td>
                            </tr>
                        </tbody>
                    </table>
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
            updatePermissions: false,

            currentPage: 1,
                pageCount: '',
                rowCount: '',

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
        GotoPage: function (link) {
                this.$router.push({ path: link });
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
            var id = localStorage.getItem('CompanyID');
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            this.$https.get('/Company/List?id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
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