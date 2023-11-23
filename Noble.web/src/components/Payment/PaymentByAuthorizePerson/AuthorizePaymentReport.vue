<template>
    <div>
        <div hidden  id='inventoryDetailReport' class="col-md-12" style="background-color:white;padding-left:40px !important;padding-right:40px !important ">
            
            <div style="background-color:white" >
                <!--HEADER-->
                <div style="height:35mm;margin-top:1mm; border:2px solid #000000;">
                <table class="table table-borderless">
                    <tr>
                            <td style="width:36%;" class="text-left pt-0 pb-0 pl-0 pr-0">
                                <p class="mb-0">
                                    <span style="font-size:23px;color:black !important;font-weight:bold;">{{headerFooters.company.companyNameEnglish}}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">VAT No.: {{headerFooters.company.vatRegistrationNo}}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">Cr No.:{{headerFooters.company.companyRegNo}}</span><br />
                                    <span style="font-size:17px;color:black !important;font-weight:bold;">
                                        Address: {{headerFooters.company.addressEnglish}}
                                    </span>
                                </p>
                            </td>
                            <td style="width:26%;" class="text-center pt-0 pb-0 pl-0 pr-0">
                                <img :src="headerFooters.company.base64Logo" style="width:auto;max-width:300px; max-height:100px; padding:5px !important; margin:0 !important">
                            </td>
                            <td style="width:38%;" class="pt-0 pb-0 pl-0 pr-0">
                                <p class=" mb-0" style="text-align: right;" >
                                    <span style="font-size:23px;color:black !important;font-weight:bold;">{{headerFooters.company.companyNameArabic}}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">رقم ضريبة القيمة المضافة :&nbsp; &nbsp; {{headerFooters.company.vatRegistrationNo}}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">رقم السجل التجاري  :&nbsp; &nbsp;{{headerFooters.company.companyRegNo}}</span><br />
                                    <span style="font-size:17px;color:black !important;font-weight:bold;">
                                         عنوان:&nbsp; &nbsp; {{headerFooters.company.addressArabic}}
                                    </span>
                                </p>
                            </td>
                        </tr>


                    </table>
                    </div>

                    <div class="row mt-2"  >
                        <div class="col-md-12 ml-2 mr-2">
                            <table class="table table-borderless">
                                <!--Row 1-->
                                <tr class="mt-2">
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:18px !important;">{{brandObj.code}}</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: right;" >:شفرة </td>

                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:18px !important;">{{ GetDate(brandObj.dateTime) }}</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: right;" >  :تاريخ </td>
                                </tr>
                                <tr class="mt-2"> 
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:18px !important;">{{ brandObj.authorizePersonNameAr }}</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: right;" >:اسم الشخص المعتمد </td>

                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:18px !important;">{{ parseFloat(totalAmount()).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,") }}</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: right;" >  :المبلغ الإجمالي </td>
                                </tr>




                            </table>
                        </div>
                        <!--INFORMATION-->
                
                <div class="col-md-12 " style="background-color:white;padding-left:5px !important;padding-right:5px !important">
                    <div class="row p-1" style="background-color:white">
                        <div class="col-12">
                            <table class="table col-md-12 "  style="direction: rtl !important;">
                            <tr style="font-size:18px;color:black !important;color:black;border-top:0px !important">
                                <th class="text-center" style="border-top:0px !important">#</th>
                                <th class="text-center" style="border-top:0px !important">رقم المعاملة</th>
                                <th class="text-center" style="border-top:0px !important">التاريخ</th>
                                <th class="text-center" style="border-top:0px !important"> بطاقة تعريف</th>
                                <th class="text-center" style="border-top:0px !important"> أسم المستفيد</th>
                                <th class="text-center" style="border-top:0px !important">فترة الدفع بالأشهر</th>
                                <th class="text-center" style="border-top:0px !important">آخر شهر مدفوع</th>
                                <th class="text-center" style="border-top:0px !important">كمية</th>
                            </tr>
                            <!-- <tr style="font-size:14px;color:black !important;color:black;border-top:0px !important">
                                <th class="text-center" style="border-top:0px !important">#</th>
                                <th class="text-center" style="border-top:0px !important">{{ $t('Payment.ID') }}</th>
                                <th class="text-center" style="border-top:0px !important">{{ $t('Payment.Date') }}</th>
                                <th class="text-center" style="border-top:0px !important"> {{ $t('Payment.Code') }}</th>
                                <th class="text-center" style="border-top:0px !important"> أسم المستفيد</th>
                                <th class="text-center" style="border-top:0px !important">نوع الدفع</th>
                                <th class="text-center" style="border-top:0px !important">تاريخ الدفع الأخير</th>
                                <th class="text-center" style="border-top:0px !important">كمية</th>
                            </tr> -->
                            <tr style="font-size:16px;color:black" v-for="(brand, index) in transactionList" v-bind:key="brand.id" st>
                                <td class="text-center">{{index+1}}</td>
                                <td class="text-center">{{brand.code}}</td>
                                <td class="text-center">{{ GetDate(brand.date) }}</td>
                                <td class="text-center">{{ brand.benificaryCode }}</td>
                                <td class="text-center">{{brand.benificaryName == '' ? brand.benificaryNameAr : brand.benificaryName }}</td>
                                <td class="text-center">{{brand.paymentTypeAr}}</td>
                                <td class="text-center" v-if="brand.paymentType =='Daily Payment' ">
                                    ------</td>
                                <td class="text-center" v-else-if="brand.paymentType =='One Time' ">
                                    ------</td>
                                <td class="text-center" v-else-if="brand.durationType =='Indefinite' ">
                                    {{ GetDate2(brand.lastPaymentDate) }}</td>
                                    
                               
                                <td class="text-center" style="font-weight: bold;">
                                    {{ parseFloat(brand.totalAmount).toFixed(3).slice(0,
                                        -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,") }}
                                </td>
                               
                            </tr>
                            <tr style="font-size:18px;color:black;font-weight: bold;"  >
                               
                                <td colspan="7" style="text-align: left;">المبلغ الإجمالي</td>
                                <td >
                                    {{ parseFloat(totalAmount()).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,") }}

                                </td>
                               
                            </tr>

                           

                        </table>
                        </div>

                      


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
        props: ['printDetails', 'headerFooter','brandObj'],
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

        },
    
        mounted: function () {
            debugger;
            this.headerFooters.company = this.$store.state.companyList.company;

           

            var root = this;

            if (this.printDetails.length > 0) {
                
                this.transactionList = this.printDetails;
                

                setTimeout(function () {
                    root.printInvoice();
                    root.$router.push({
                                    path: '/authorizepersonlist',

                                })
                }, 125)
            }
        },
        methods: {
            totalAmount: function () {
               
               {
                  return this.transactionList.reduce(function (a, c) { return a + Number(( c.totalAmount) || 0) }, 0)
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