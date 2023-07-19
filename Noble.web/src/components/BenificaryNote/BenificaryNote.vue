<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title"> {{ $t('BenificaryNote.BenificaryNote') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Home') }}</a>
                                    </li>
                                    <li class="breadcrumb-item active">{{ $t('BenificaryNote.BenificaryNoteList') }}</li>
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
                    <!-- <div class="input-group">
                        <button class="btn btn-secondary" type="button" id="button-addon1">
                            <i class="fas fa-search"></i>
                        </button>
                        <input v-model="search" type="text" class="form-control" :placeholder="$t('BenificaryNote.Search')"
                            aria-label="Example text with button addon" aria-describedby="button-addon1">
                    </div> -->
                    <div class="row">
                        <div class="col-md-3 form-group">
                            <input v-model="benificaryCode" type="text" class="form-control"
                                :placeholder="$t('BenificaryNote.SearchByID')" aria-label="Example text with button addon"
                                aria-describedby="button-addon1">
                        </div>
                        <div class="col-md-3 form-group">
                            <input v-model="search" type="text" class="form-control"
                                :placeholder="$t('BenificaryNote.Search')" aria-label="Example text with button addon"
                                aria-describedby="button-addon1">
                        </div>
                        <div class="col-md-3 form-group">
                            <input v-model="beneficiaryNote" type="text" class="form-control"
                                :placeholder="$t('BenificaryNote.SearchByBeneficiaryNote')"
                                aria-label="Example text with button addon" aria-describedby="button-addon1">
                        </div>
                        <div class="col-md-3 form-group">
                            <input v-model="nationalId" type="text" class="form-control"
                                :placeholder="$t('BenificaryNote.SearchByNationalID')"
                                aria-label="Example text with button addon" aria-describedby="button-addon1">
                        </div>
                        <div class="col-md-3 form-group">
                            <input v-model="contactNo" type="text" class="form-control"
                                :placeholder="$t('BenificaryNote.SearchByContactNo')"
                                aria-label="Example text with button addon" aria-describedby="button-addon1">
                        </div>
                        <div class="col-md-3 form-group">
                            <a v-on:click="SearchFilter" href="javascript:void(0);"
                                class="btn btn-sm btn-outline-primary mx-1">
                                {{ $t('BenificaryNote.SearchFilter') }}
                            </a>
                            <a @click="ClearFilter" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                                {{ $t('BenificaryNote.ClearFilter') }}
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
                                        {{ $t('BenificaryNote.Code') }}
                                    </th>
                                    <th class="text-start">
                                        {{ $t('BenificaryNote.BenificaryName') }}
                                    </th>
                                    <th class="text-start">
                                        {{ $t('BenificaryNote.BenificiaryNotes') }}
                                    </th>
                                    <th class="text-start">
                                        {{ $t('BenificaryNote.NationalID') }}
                                    </th>
                                    <th class="text-start">
                                        {{ $t('BenificaryNote.Nationality') }}
                                    </th>
                                    <th class="text-start">
                                        {{ $t('BenificaryNote.ContactNo') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('BenificaryNote.Date') }}
                                    </th>
                                    <th class="text-center" v-if="roleName != 'Cashier'">
                                        {{ $t('BenificaryNote.Actions') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(brand, index) in benificaryNotelist" v-bind:key="brand.id">
                                    <td v-if="currentPage === 1">
                                        {{ index + 1 }}
                                    </td>
                                    <td v-else>
                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                    </td>
                                    <td v-if="brand.benificaryCode != null">
                                        {{ brand.benificaryCode }}
                                    </td>
                                    <td v-else>
                                        ---
                                    </td>

                                    <td v-if="brand.benificaryName != null">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditbenificaryNote(brand.id)">{{
                                                brand.benificaryName }}</a>
                                        </strong>
                                    </td>
                                    <td v-else>
                                        ---
                                    </td>
                                    <td class="text-start">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditbenificaryNote(brand.id)"> {{
                                                brand.note }}</a>
                                        </strong>
                                    </td>
                                    
                                    <td v-if="brand.nationalId != null">
                                        {{ brand.nationalId }}
                                    </td>
                                    <td v-else>
                                        ---
                                    </td>

                                    <td v-if="brand.nationality != null">
                                        {{ brand.nationality }}
                                    </td>
                                    <td v-else>
                                        ---
                                    </td>

                                    <td v-if="brand.nationality != null">
                                        {{ brand.contactNo }}
                                    </td>
                                    <td v-else>
                                        ---
                                    </td>
                                    <td class="text-center">
                                        {{ brand.date }}
                                    </td>
                                    <td class="text-center" v-if="roleName != 'Cashier'">
                                        <button class="btn btn-sm btn-danger" v-on:click="deleteBenificaryNote(brand.id)">
                                            {{ $t('BenificaryNote.Delete') }}
                                        </button>
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
                        <div class="" v-on:click="GetbenificaryNote()">
                            <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount" :per-page="10"
                                :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')"
                                :next-text="$t('Table.Next')" :last-text="$t('Table.Last')"></b-pagination>
                        </div>
                    </div>
                </div>
            </div>

            <benificarynote-mod :brand="newbenificaryNote" :show="show" v-if="show" @close="IsSave" :type="type" />
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
            benificaryNotelist: [],
            newbenificaryNote: {
                id: '',
                note: '',
                benificaryId: '',
            },
            type: '',
            search: '',
            currentPage: 1,
            pageCount: '',
            rowCount: '0',
            arabic: '',
            english: '',
            roleName: '',
            beneficiaryNote: '',
            benificaryCode:'',
            nationalId:'',
            contactNo:'',
            
        }
    },
    // watch: {
    //     search: function (val) {
    //         this.GetbenificaryNote(val, 1);
    //     }
    // },
    methods: {
        ClearFilter() {
            // Reset the filter conditions here
            this.search = '';
            this.beneficiaryNote = '';
            this.benificaryCode = '';
            this.nationalId='';
            this.contactNo='';

            // Trigger the search or data refresh
            this.GetbenificaryNote(this.currentPage);
        },


        SearchFilter: function () {
            debugger;
            this.GetbenificaryNote(this.currentPage);
        },


        IsSave: function () {
            this.show = false;
            this.GetbenificaryNote(this.search, this.currentPage);
        },

        getPage: function () {
            this.GetbenificaryNote(this.currentPage);
        },

        GotoPage: function (link) {
            this.$router.push({ path: link });
        },

        openmodel: function () {
            this.newbenificaryNote = {
                id: '00000000-0000-0000-0000-000000000000',
                note: '',
                benificaryId: '',
            }
            this.show = !this.show;
            this.type = "Add";
        },

        GetbenificaryNote: function () {
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.$https.get('Benificary/GetBenificaryNoteList?pageNumber=' + this.currentPage + '&searchTerm=' + this.search + '&beneficiaryNote=' + this.beneficiaryNote + '&benificaryCode=' + this.benificaryCode + '&nationalId=' + this.nationalId + '&contactNo=' + this.contactNo, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.benificaryNotelist = response.data.results;
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;
                    root.loading = false;
                }
                root.loading = false;
            });
        },

        EditbenificaryNote: function (Id) {

            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Benificary/GetBenificaryNoteDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data) {
                        root.newbenificaryNote = response.data;
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

        },
        deleteBenificaryNote: function(id)
        {
            var root = this;
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Benificary/DeleteBeneficiaryNote?id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    debugger;
                    if (response.data.isSuccess == true) {
                            root.$swal({
                                title: 'Save',
                                text: response.data.isAddUpdate,
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.GetbenificaryNote();
                    }
                    else {
                        root.$swal({
                            title: 'Error',
                            text: response.data.isAddUpdate,
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });
                    }
                })
                .catch(error => {
                    console.log(error)
                    root.$swal.fire(
                        {
                            icon: 'error',
                            title: 'Error',
                            text: 'Something Went Wrong',
                            showConfirmButton: false,
                            timer: 5000,
                            timerProgressBar: true,
                        });

                    root.loading = false
                })
                .finally(() => root.loading = false);
        }
    },
    
    created: function () {
        this.$emit('input', this.$route.name);
    },
    mounted: function () {
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        this.GetbenificaryNote(this.currentPage);
        this.roleName = localStorage.getItem('RoleName');
    }
}
</script>