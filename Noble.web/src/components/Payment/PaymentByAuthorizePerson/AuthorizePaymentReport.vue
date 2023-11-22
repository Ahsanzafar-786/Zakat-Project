<template>
    <div>
        <div hidden id='inventoryDetailReport' class="col-md-12" style="background-color:white;padding-left:40px !important;padding-right:40px !important ">
            
            <div style="background-color:white" >
                <!--HEADER-->
                
                <!-- <div class="col-md-12" style="height:45mm;border:2px solid #000000;background-color:white" >
                    <div class="row" style="height:35mm;background-color:white">
                        <div class="col-md-4 ">
                            <table class="text-left">
                                <tr>
                                    <td>
                                        <p>
                                            <span style="font-size:25px;color:black !important;font-weight:bold;">{{headerFooters.company.nameEnglish}}</span><br />
                                            <span style="font-size:17px;color:black !important;font-weight:bold;">{{headerFooters.company.categoryEnglish}}</span><br />
                                            <span style="font-size:16px;color:black !important;font-weight:bold;">VAT No.: {{headerFooters.company.vatRegistrationNo}}</span><br />
                                            <span style="font-size:16px;color:black !important;font-weight:bold;">Cr No.:{{headerFooters.company.companyRegNo}}</span><br />
                                            <span style="font-size:15px;color:black !important;font-weight:bold;">
                                                Tel: {{headerFooters.company.phoneNo}}
                                            </span>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-md-4 text-center my-5" style="padding:0px !important; margin:0 !important">
                            <img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; max-height:100px; padding:5px !important; margin:0 !important">
                        </div>
                        <div class="col-md-4 ">
                            <table class="text-right" v-if="arabic=='true'">
                                <tr>
                                    <td>
                                        <p>
                                            <span style="font-size:25px;color:black !important;font-weight:bold;">{{headerFooters.company.nameArabic}}.</span><br />
                                            <span style="font-size:17px;color:black !important;font-weight:bold;">{{headerFooters.company.categoryArabic}}</span><br />
                                            <span style="font-size:16px;color:black !important;font-weight:bold;">رقم ضريبة القيمة المضافة: {{headerFooters.company.vatRegistrationNo}}</span><br />
                                            <span style="font-size:16px;color:black !important;font-weight:bold;">رقم السجل التجاري :{{headerFooters.company.companyRegNo}}</span><br />
                                            <span style="font-size:15px;color:black !important;font-weight:bold;">
                                                هاتف: {{headerFooters.company.phoneNo}}:
                                            </span>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="row" style="background-color:white">
                        <div class="col-md-12" style="margin-bottom:10px !important;height:10mm" v-if="($i18n.locale == 'en' ||isLeftToRight())">
                            <p style="text-align: center; margin: 0px; padding: 0px; line-height: 1; ">
                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">Account Ledger Report</span>
                            </p>
                        </div>

                    </div>
                </div> -->
                
                <!--INFORMATION-->
                
                <div class="col-md-12 " style="background-color:white;padding-left:40px !important;padding-right:40px !important">
                    <div class="row p-1" style="background-color:white;height:310mm;">
                        <div class="col-12">
                            <table class="table col-md-12 ">
                            <tr style="font-size:14px;color:black !important;color:black;border-top:0px !important">
                                <th class="text-left" style="border-top:0px !important">#</th>
                                <th class="text-left" style="border-top:0px !important">{{ $t('Payment.ID') }}</th>
                                <th class="text-left" style="border-top:0px !important">{{ $t('Payment.Date') }}</th>
                                <th class="text-left" style="border-top:0px !important"> {{ $t('Payment.Code') }}</th>
                                <th class="text-left" style="border-top:0px !important"> {{ $t('Payment.BenificaryName') }}</th>
                                <th class="text-left" style="border-top:0px !important">{{ $t('AddBenificary.PaymentType') }}</th>
                                <th class="text-right" style="border-top:0px !important">{{ $t('Payment.LastPayment') }}</th>
                                <th class="text-right" style="border-top:0px !important">{{ $t('Payment.NextPaymentDate') }}</th>
                                <th class="text-right" style="border-top:0px !important;width:15%">{{ $t('Payment.Cashier') }}</th>
                                <th class="text-right" style="border-top:0px !important;width:15%">{{ $t('Payment.Amount') }}</th>
                            </tr>
                            <tr style="font-size:13px; page-break-after: always;color:black" v-for="(brand, index) in transactionList" v-bind:key="brand.id">
                                <td class="text-left">{{index+1}}</td>
                                <td class="text-left">{{brand.code}}}</td>
                                <td class="text-left">{{ GetDate(brand.date) }}</td>
                                <td class="text-left">{{ brand.benificaryCode }}</td>
                                <td class="text-left">{{brand.benificaryName == '' ? brand.benificaryNameAr : brand.benificaryName }}</td>
                                <td class="text-left">{{brand.paymentTypeAr}}</td>
                                <td class="text-left" v-if="brand.paymentType =='Daily Payment' ">
                                    ------</td>
                                <td class="text-left" v-else-if="brand.paymentType =='One Time' ">
                                    ------</td>
                                <td class="text-left" v-else-if="brand.durationType =='Indefinite' ">
                                    {{ GetDate2(brand.lastPaymentDate) }}</td>
                                <td class="text-right">
                                    {{ brand.cashier }}
                                </td>
                                <td class="text-right">
                                    {{ parseFloat(brand.totalAmount).toFixed(3).slice(0,
                                        -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,") }}
                                </td>
                               >
                            </tr>

                           

                        </table>
                        </div>

                      


                    </div>
                    

                </div>
            </div>



        </div>
    </div>

</template>

<script>
    import moment from "moment";

    export default {
        props: ['printDetails', 'isShown', 'formName', 'isPrint', 'dates', 'headerFooter'],
        data: function () {
            return {
                transactionList: [],
                itemTotal: 0,
                fDate: '',
                tDate: '',
                headerFooters: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
                IsPaksitanClient: false,
                Print: false,
                render: 0,
                arabic: '',
                english: '',
                listItemP1: [],
                listItemP2: [],
                listItemP3: [],
            }
        },
        created: function (){
            alert("Ok");
        },
    
        mounted: function () {
            debugger;
            alert("Ok");

           

            var root = this;

            if (this.printDetails.length > 0) {
                
                this.transactionList = this.printDetails;
                

                setTimeout(function () {
                    root.printInvoice();
                }, 125)
            }
        },
        methods: {
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
       
          
          
            printInvoice: function () {

                this.$htmlToPaper('inventoryDetailReport');
            }
        }
    }
</script>


<style scoped>
    #font11 {
        font-size: 11px;
        line-height: 0;
    }

    #font16 {
        font-size: 16px;
    }
</style>