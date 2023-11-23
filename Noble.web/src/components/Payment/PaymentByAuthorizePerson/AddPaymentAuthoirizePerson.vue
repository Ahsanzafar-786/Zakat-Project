<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="col-sm-12">
                <div class="page-title-box">
                    <div class="row">
                        <div class="col">
                            <h4 class="page-title">{{ $t('Payment.AddPaymentbyAuthorizedPerson') }}</h4>
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Home') }}</a>
                                </li>
                                <li class="breadcrumb-item active">{{ $t('Payment.AddPaymentbyAuthorizedPerson') }}</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal-body card">
                <div class="row">
                    <div class="col-md-6 form-group">
                        <label class="text  font-weight-bolder">
                            {{ $t('Payment.AuthorizePerson') }}:<span class="text-danger"> </span>
                        </label>
                        <authorizedperson ref="AuthorizedDropdown"
                            v-on:input="GetBeneficaryList(brand.authorizationPersonId)"
                            v-model="brand.authorizationPersonId" :values="brand.authorizationPersonId" />
                    </div>
                    <div class="col-md-6 form-group">
                        <label class="text  font-weight-bolder">{{ $t('Payment.PaymentCode') }}:<span class="text-danger">
                            </span>
                        </label>
                        <input class="form-control" v-model="brand.code" type="text" disabled />
                    </div>


                </div>
                <div class="row" v-if="benificarylist.length > 0">
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

                                        <th class="text-center">
                                            {{ $t('Benificary.AmountPerMonth') }}
                                        </th>

                                        <th class="text-center">
                                            {{ $t('Benificary.PaymentType') }}
                                        </th>

                                        <th class="text-center">
                                            {{ $t('Benificary.RecurringAmount') }}
                                        </th>
                                        <th class="text-start">
                                            {{ $t('Benificary.AuthorizePersonName') }}
                                        </th>
                                        <th class="text-center">

                                            {{ $t('Benificary.Date') }}
                                        </th>

                                        <!-- <th class="text-center">
                                        {{ $t('Benificary.ApprovedBy') }}
                                    </th> -->
                                        <th class="text-center">
                                            {{ $t('Benificary.BenificaryStatus') }}
                                        </th>
                                        <th class="text-center" v-if="roleName == 'Admin'">
                                            {{ $t('Payment.ApprovalType') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('Benificary.Notes') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('Benificary.AccountStatus') }}
                                        </th>

                                        <th class="text-center">
                                            {{ $t('Benificary.Action') }}
                                        </th>



                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(brand, index) in benificarylist" v-bind:key="brand.id">
                                        <td>
                                            {{ index + 1 }}
                                        </td>


                                        <td class="text-center">
                                            <strong>
                                                <a href="javascript:void(0)" v-on:click="GotoPayment(brand.id)"> {{
                                                    brand.beneficiaryId }}</a>
                                            </strong>

                                        </td>
                                        <td class="text-start">
                                            <strong>
                                                <a href="javascript:void(0)"> {{
                                                    brand.name == '' ? brand.nameAr : brand.name }}</a>
                                            </strong>
                                        </td>


                                        <td class="text-center">
                                            {{ parseFloat(brand.amountPerMonth).toFixed(3).slice(0,
                                                -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,") }}
                                        </td>
                                        <td class="text-center">
                                            {{ $i18n.locale == 'en' ? brand.paymentTypeName : brand.paymentTypeNameAr }}
                                        </td>
                                        <td class="text-center">

                                            {{ parseFloat(brand.recurringAmount).toFixed(3).slice(0,
                                                -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,") }}

                                        </td>

                                        <td class="text-start">
                                            <span v-for="item in brand.benificaryAuthorization" :key="item.id" class="mx-2">
                                                {{ item.authorizationPersonName == '' ? item.authorizationPersonNameAr :
                                                    item.authorizationPersonName }}
                                            </span>
                                        </td>
                                        <td class="text-center">{{ GetArabicDate(brand.startMonth) }}</td>


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

                                        <td class="text-center" v-if="roleName == 'Admin'">
                                            <span class="badge badge-boxed  badge-outline-danger"
                                                v-if="brand.approvalStatus == '4'">{{ $t('Benificary.WaitingForApproved')
                                                }}</span>
                                            <span class="badge badge-boxed  badge-outline-success"
                                                v-if="brand.approvalStatus == '3'">{{ $t('Benificary.Approved') }}</span>
                                        </td>
                                        <td v-if="brand.note != null">
                                            {{ brand.note }}
                                        </td>
                                        <td v-else>
                                            ---
                                        </td>


                                        <td class="text-center">
                                            <span v-if="brand.isRegister" class="badge badge-boxed  badge-outline-success">
                                                {{ $t('Benificary.Register') }}
                                            </span>
                                            <span v-else class="badge badge-boxed  badge-outline-danger">
                                                {{ $t('Benificary.UnRegister') }}
                                            </span>
                                        </td>
                                        <td class="text-end">
                                            <a href="javascript:void(0)" v-on:click="EditBenificary(brand.id, 'View')"><i
                                                    class="las la-eye text-secondary font-16"></i></a>
                                            <a href="javascript:void(0)" v-on:click="DeleteBenficay(brand.id)"><i
                                                    class="las la-trash-alt text-secondary font-16"></i></a>
                                        </td>



                                    </tr>
                                </tbody>
                            </table>
                        </div>

                    </div>

                </div>



            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveBenificary(false)">
                    {{ $t('Save') }}
                </button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveBenificary(true)">
                    Save As Print
                </button>


                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="GotoPage('/authorizepersonlist')">
                    {{ $t('Close') }}
                </button>
            </div>

            <loading :active.sync="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>
        <benificary-mod :brand="newBenificary" :show="show" v-if="show" @close="IsSave" :type="type" />
        <authorizepaymentreport :headerFooter="headerFooter" :documentName="'Add'" :show="show2" :brandObj="brandObj" v-if="show2"
            v-bind:key="changereport" :printDetails="paymentRecord" />


    </div>
</template>

<script>
import clickMixin from '@/Mixins/clickMixin'
import 'vue-loading-overlay/dist/vue-loading.css';
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/vue-loading.css';


import moment from 'moment'

export default {
    mixins: [clickMixin],
    name: 'AddBenificary',
    components: {
        Loading,
    },
    data: function () {
        return {
            show1: false,
            brand: {
                id: '00000000-0000-0000-0000-000000000000',
                authorizationPersonId: '',
                userId: localStorage.getItem('UserId'),
                code: '',
                benificarylist: [],
            },
            brandObj: {
                code: '',
                dateTime: '',
                authorizePersonNameAr: '',
                amount: [],
            },
            show: false,
            show2: false,

            newBenificary: {
                id: '00000000-0000-0000-0000-000000000000',
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




            benificarylist: [],


            advancePaymentvalue: '',
            giveReason: false,
            randerforempty: 0,
            paymentType: null,
            arabic: '',
            english: '',
            loading: false,
            roleName: ''
        }
    },

    methods: {
        IsSave: function () {
            this.show = false;
        },
        GetArabicDate: function (link) {

            if (link != undefined && link != null && link != '') {

                const date = moment(link);
                const arabicMonth = date.locale('ar').format('MMMM');
                return `${arabicMonth} ${date.format('Do YYYY')}`;

            }
            else {
                return '';
            }
        },

        GotoPage: function (link) {
            this.$router.push({
                path: link
            });
        },


        close: function () {
            this.$router.push({
                path: '/authorizepersonlist',

            })
        },

        DeleteBenficay: function (Id) {
            const index = this.benificarylist.findIndex(x => x.id === Id);
            if (index !== -1) {
                this.benificarylist.splice(index, 1); // Remove 1 element at the found index
            }
        },
        EditBenificary: function (Id, type) {

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
                        root.type = type;
                    } else {
                        console.log("error: something wrong from db.");
                    }
                },
                    function (error) {
                        this.loading = false;
                        console.log(error);
                    });

        },
        PrintRecord: function (id) {
            debugger;
            this.loading = true;
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }

            if (this.$refs.AuthorizedDropdown != undefined) {

                let name = this.$refs.AuthorizedDropdown.GetName();
                if (name != undefined && name != null && name != '') {
                    this.brandObj.authorizePersonNameAr = name;

                }
                this.brandObj.dateTime = moment().format('llll');
                this.brandObj.code = this.brand.code;





            }
            root.$https.get('/Benificary/PaymentDetailQueryByAuth?authorizationPersonId=' + id, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {
                    if (response.data != null) {

                        debugger;
                        root.paymentRecord = response.data.results;


                        root.show2 = true;
                        root.changereport++;
                        root.loading = false;
                       
                    } else {
                        console.log("error: something wrong from db.");
                    }
                },
                    function (error) {
                        this.loading = false;
                        console.log(error);
                    });
        },

        SaveBenificary: function (val) {
            debugger;
            var root = this;
            this.loading = true;
            var token = '';

            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }



            this.brand.benificarylist = this.benificarylist;




            this.$https.post('/Benificary/SavePaymentsByAuthorizePerson', this.brand, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {
                    debugger;

                    if (val) {
                        debugger;
                        if (response.data.paymentId != null) {
                            root.PrintRecord(response.data.paymentId);
                        }

                    }
                    else {
                        if (response.data.isSuccess == true) {

                            {
                                root.$swal({
                                    title: 'Save',
                                    text: response.data.isAddUpdate,
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });
                                root.$router.push({
                                    path: '/authorizepersonlist',

                                })


                            }
                        } else {
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

                    }

                })
                .catch(error => {
                    console.log(error)
                    root.$swal.fire({
                        icon: 'error',
                        title: 'Something Went Wrong',
                        text: error.response.data,

                        showConfirmButton: false,
                        timer: 5000,
                        timerProgressBar: true,
                    });

                    root.loading = false
                })
                .finally(() => root.loading = false);
        },
        GetBeneficaryList: function (authoirzePerson) {
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Benificary/GetBeneficaryByAuthorizePerson?authorizationPersonId=' + authoirzePerson, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {
                    if (response.data) {
                        debugger;
                        root.benificarylist = response.data.results;
                    } else {
                        console.log("error: something wrong from db.");
                    }
                },
                    function (error) {
                        this.loading = false;
                        console.log(error);
                    });
        },
        GetAutoCode: function () {
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Benificary/AutoCodeGenerate?name=PaymentAuthorization', {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {
                    if (response.data) {
                        debugger;
                        root.brand.code = response.data;
                        root.rendar++;
                    } else {
                        console.log("error: something wrong from db.");
                    }
                },
                    function (error) {
                        this.loading = false;
                        console.log(error);
                    });
        },
    },
    created: function () {
        this.brand.startMonth = new Date();
    },
    mounted: function () {
        debugger;

        this.roleName = localStorage.getItem('RoleName');

        this.english = localStorage.getItem('locales');
        this.arabic = localStorage.getItem('locales');


        if (this.brand.id == '00000000-0000-0000-0000-000000000000') {
            this.GetAutoCode();
        }
    }
}
</script>
