<template>
    <modal :show="show" :modalLarge="true">
        <div class="modal-content">
            <div class="modal-header">
                
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" >
                    View Payment Detail
                </h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="table-responsive">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>#</th>
                                    <!-- <th class="text-center">
                                        ID
                                    </th> -->
                                    <th class="text-center">
                                        {{ $t('Payment.ID') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Payment.Date') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Payment.Code') }}
                                    </th>
                                    <th class="text-start">
                                        {{ $t('Payment.BenificaryName') }}
                                    </th>
                                    <th class="text-start">
                                        {{ $t('Benificary.NationalID') }}
                                    </th>
                                    <th class="text-start">
                                        {{ $t('Payment.AuthorizedPerson') }}
                                    </th>
                                    <!-- <th class="text-start">
                                        {{ $t('AddBenificary.ApprovedBy') }}
                                    </th> -->
                                    <th class="text-start">
                                        {{ $t('AddBenificary.PaymentType') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Payment.LastPayment') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Payment.NextPaymentDate') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Payment.Note') }}
                                    </th>

                                    <th class="text-center">
                                        {{ $t('Payment.Cashier') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Payment.Amount') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Payment.Action') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(brand, index) in paymentList" v-bind:key="brand.id">
                                    <td >
                                        {{ index + 1 }}
                                    </td>
                                    

                                    <td class="text-center " v-if="brand.isVoid && roleName == 'Admin'">
                                        <strong>
                                            <a class="text-danger" href="javascript:void(0)"
                                                v-on:click="EditPayment(brand.id)">{{
                                                    brand.code }}</a>
                                        </strong>
                                    </td>
                                    <td class="text-center" v-else-if="roleName != 'Cashier'">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditPayment(brand.id)">{{
                                                brand.code }}</a>
                                        </strong>
                                    </td>
                                    <td class="text-center text-danger" v-else-if="brand.isVoid">{{ brand.code }}</td>

                                    <td class="text-center" v-else>{{ brand.code }}</td>
                                    <td class="text-center">{{ GetDate(brand.date) }}</td>

                                    <td class="text-center">{{ brand.benificaryCode }}</td>

                                    <td class="text-center text-danger" v-if="brand.isVoid">Payment Voided</td>
                                    <td class="text-start" v-else-if="roleName != 'Cashier'">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditPayment(brand.id)">
                                                {{ brand.benificaryNameAr ==
                                                    '' ? brand.benificaryName : brand.benificaryNameAr }}</a>
                                        </strong>
                                    </td>
                                    <td class="text-start" v-else>{{
                                        brand.benificaryName == '' ? brand.benificaryNameAr : brand.benificaryName }}
                                    </td>
                                    <td class="text-center" v-if="brand.ugamaNo != null">
                                        {{ brand.ugamaNo }}
                                    </td>
                                    <td class="text-center" v-else>
                                        ---
                                    </td>
                                    <td class="text-center" v-if="brand.authorizePersonName != null">
                                        {{ brand.authorizePersonName }}
                                    </td>
                                    <td class="text-center" v-else>
                                        ---
                                    </td>
                                    <!-- <td class="text-center" v-if="brand.approvalPersonName != null">
                                        {{ brand.approvalPersonName }}
                                    </td>
                                    <td class="text-center" v-else>
                                        ---
                                    </td> -->
                                    <td class="text-center" v-if="brand.paymentType != null">
                                        {{ $i18n.locale == 'en' ? brand.paymentType : brand.paymentTypeAr }}
                                    </td>
                                    <td v-else>
                                        ---
                                    </td>
                                    <td class="text-center">
                                        <!-- {{ brand.lastPaymentAmount.toFixed(2) }} - {{ GetDate(brand.lastPaymentDate) }} -->
                                         {{
        GetDate(brand.lastPaymentDate) }}
                                    </td>
                                    <td class="text-center"  v-if="brand.paymentType =='Daily Payment' ">
                                        <span>----</span>
                                       
                                    </td>
                                    <td class="text-center"  v-else-if="brand.paymentType =='One Time' ">
                                        <span>----</span>
                                       
                                    </td>
                                     <td class="text-center"  v-else-if="brand.durationType =='Indefinite' ">
                                        <span> {{ GetDate2(brand.lastPaymentDate) }}</span>
                                       
                                    </td>
                                   
                                    <td class="text-center" v-else-if=" brand.nextMonth> brand.endMonth ">
                                        <span>----</span>
                                       
                                    </td>
                                    <td class="text-center" v-else>
                                    
                                        {{ GetDate2(brand.lastPaymentDate) }}
                                    </td>

                                    <td class="text-center">{{ brand.note }}</td>
                                    <td class="text-center">{{ brand.cashier }}</td>
                                    <td class="text-center" v-if="brand.isVoid">--</td>
                                    <td class="text-center" v-else>{{ parseFloat(brand.totalAmount).toFixed(3).slice(0,
                                        -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,") }}</td>

                                    <td class="text-center d-flex align-items-baseline justify-content-center"
                                        v-if="roleName != 'User'">
                                        <button type="button" class="btn btn-light dropdown-toggle"
                                            data-bs-toggle="dropdown" aria-expanded="false"> {{ $t('Payment.Action') }} <i
                                                class="mdi mdi-chevron-down"></i></button>
                                        <div class="dropdown-menu text-center">
                                            <div v-if="!brand.isVoid && roleName == 'Admin' ">
                                                <input type="checkbox" v-model="brand.isVoid"
                                                    v-on:change="EditPayment(brand.id, brand.isVoid)" />
                                                <span class="mx-1"> {{
                                                    $t('Payment.IsVoid') }}
                                                </span>
                                            </div>
                                           
                                            
                                            <strong>
                                                <a href="javascript:void(0)" v-on:click="PrintRdlc(brand.id)"> {{
                                                    $t('Payment.Print') }}</a>
                                            </strong>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>


            </div>
            
            <loading :active.sync="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>
    </modal>
</template>

<script>
import clickMixin from '@/Mixins/clickMixin'
import 'vue-loading-overlay/dist/vue-loading.css';

import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/vue-loading.css';

import moment from 'moment'

export default {
    mixins: [clickMixin],
    name: 'PaymentAuthoirzeDetail',
    props: ['show', 'id'],
    components: {
        Loading,
    },
    data: function () {
        return {
            paymentList: [],
            arabic: '',
            english: '',
            loading: false,
            roleName: '',
            isDisable: false,
        }
    },
    
    methods: {
        PrintRdlc: function (Id) {
            var companyId = '';
            if (this.$session.exists()) {
                companyId = localStorage.getItem('CompanyID');
            }

            this.reportsrc = this.$ReportServer + '/Invoice/A4_DefaultTempletForm.aspx?id=' + Id + '&pageNumber=' + this.currentPage + '&searchTerm=' + this.search + '&CompanyID=' + companyId + '&formName=Payment'
            this.changereport++;
            this.show = !this.show;
        },
        EditPayment: function (Id, val, allowVoid) {
            debugger;
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            if (allowVoid) {
                root.$https.get('/Benificary/GetPaymentsDetail?Id=' + Id + '&allowVoid=' + allowVoid, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        debugger;

                        if (response.data == "") {
                            root.$swal({
                                title: 'Save Allow Void',
                                text: 'Permission Allow to make payment void',
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 3000,
                                timerProgressBar: true,
                            });
                            root.GetPayment();
                        } else {
                            console.log("error: something wrong from db.");
                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });
            }
            else if (val) {
                root.$https.get('/Benificary/GetPaymentsDetail?Id=' + Id + '&isVoid=' + val, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        debugger;
                        if (response.data == "") {
                            root.$swal({
                                title: 'Save Void',
                                text: 'Successfully Payment Voided',
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 3000,
                                timerProgressBar: true,
                            });
                            root.GetPayment();
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
                root.$https.get('/Benificary/GetPaymentsDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data) {
                            root.$router.push({
                                path: '/addpayment',
                                query: { data: response.data }
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
        GetDate: function (link) {
            if (link != undefined && link != null && link != '') {

                const date = moment(link);
                const arabicMonth = date.locale('ar').format('MMMM');
                return `${arabicMonth} ${date.format('Do YYYY')}`;

            }
            else {
                return '';
            }
        },
        GetDate2: function (link) {
            if (link != undefined) {

                const date = moment(link);
                const arabicMonth = date.locale('ar').add(1, 'months').format('MMMM');
                return `${arabicMonth} ${date.format('Do YYYY')}`;

            }
            else {
                return '';
            }
        },
       
       
       
        close: function () {
            this.$emit('close');
        },
       
        GetRecordById: function () {
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Benificary/PaymentDetailQueryByAuth?authorizationPersonId=' + this.id, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {
                    if (response.data != null) {
                                                debugger;
                        root.paymentList = response.data.results;
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
    mounted: function () {
        debugger;
        this.roleName = localStorage.getItem('RoleName');
       
        this.english = localStorage.getItem('locales');
        this.arabic = localStorage.getItem('locales');

       
            this.GetRecordById();
    }
}
</script>
