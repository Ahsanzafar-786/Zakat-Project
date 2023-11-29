<template>
    <div>
        <div hidden id='inventoryDetailReport' class="col-md-12">
            <!--page1-->
            <div v-if="itemTotal < 18">
                <!--HEADER-->
                <div
                    style="height:55mm; border:2px solid #000000;padding-top: 5px !important;padding-bottom: 5px !important;">
                    <table class="table " style="width: 100%;">
                        <tr>
                            <td style="width:42%;">
                                <p>
                                    <span style="font-size:23px;color:black !important;font-weight:bold;">{{
                                        headerFooters.company.companyNameEnglish }}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">VAT No.:
                                        {{ headerFooters.company.vatRegistrationNo }}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">Cr
                                        No.:{{ headerFooters.company.companyRegNo }}</span><br />
                                    <span style="font-size:17px;color:black !important;font-weight:bold;">
                                        Address: {{ headerFooters.company.addressEnglish }}
                                    </span>
                                </p>
                            </td>
                            <td style="width:24%;">
                                <img :src="headerFooters.company.base64Logo"
                                    style="width:auto;max-width:300px; max-height:100px; padding:5px !important; margin:0 !important">
                            </td>
                            <td style="width:34%;">
                                <p style="text-align: right;">
                                    <span style="font-size:23px;color:black !important;font-weight:bold;">{{
                                        headerFooters.company.companyNameArabic }}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">رقم ضريبة القيمة
                                        المضافة :&nbsp; &nbsp; {{ headerFooters.company.vatRegistrationNo }}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">رقم السجل التجاري
                                        :&nbsp; &nbsp;{{ headerFooters.company.companyRegNo }}</span><br />
                                    <span style="font-size:17px;color:black !important;font-weight:bold;">
                                        عنوان:&nbsp; &nbsp; {{ headerFooters.company.addressArabic }}
                                    </span>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7"
                                style="text-align: center;width:100%;font-size:20px;color:black !important;font-weight:bold;">
                                الدفع عن طريق الوكيل </td>
                        </tr>


                    </table>
                </div>
                <div class="row " style="margin-top:10px !important">
                    <div class="col-md-12 ml-2 mr-2">
                        <table class="table " style="border: none !important;">
                            <!--Row 1-->
                            <tr class="mt-2" style="border: none !important;">
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:35%; text-align:right;color:black !important;font-weight:bold;font-size:18px !important;border: none !important;">
                                    {{ brandObj.code }}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:15%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: center;border: none !important;">
                                    :رقم المعاملة </td>

                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:35%; text-align:right;color:black !important;font-weight:bold;font-size:18px !important;border: none !important;">
                                    {{ GetDate(brandObj.dateTime) }}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:15%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: center;border: none !important;">
                                    :تاريخ الدفع </td>
                            </tr>
                            <tr class="mt-2" style="border: none !important;">
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:35%; text-align:right;color:black !important;font-weight:bold;font-size:18px !important;border: none !important;">
                                </td>
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:15%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: center;border: none !important;">
                                </td>

                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:35%; text-align:right;color:black !important;font-weight:bold;font-size:18px !important;border: none !important;">
                                    {{ GetTime(brandObj.dateTime) }}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:15%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: center;border: none !important;">
                                    : وقت </td>
                            </tr>
                            <tr class="mt-2" style="border: none !important;">
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:35%; text-align:right;color:black !important;font-weight:bold;font-size:18px !important;border: none !important;">
                                    {{ brandObj.authorizedPersonCode }}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:15%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: center;border: none !important;">
                                    : الكود الوكيل</td>

                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:35%; text-align:right;color:black !important;font-weight:bold;font-size:18px !important;border: none !important;">
                                    {{ brandObj.authorizePersonNameAr }}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:15%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: center;border: none !important;">
                                    :اسم الوكيل</td>
                            </tr>
                            <tr class="mt-2" style="border: none !important;">
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:35%; text-align:right;color:black !important;font-weight:bold;font-size:18px !important;border: none !important;">
                                    {{ count() }}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:15%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: center;border: none !important;">
                                    : إجمالي المستفيدين</td>

                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:35%; text-align:right;color:black !important;font-weight:bold;font-size:18px !important;border: none !important;">
                                    {{ parseFloat(totalAmount()).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")
                                    }}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:15%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: center;border: none !important;">
                                    :المبلغ الإجمالي </td>
                            </tr>




                        </table>
                    </div>
                    <!--INFORMATION-->

                    <div class="col-md-12 "
                        style="background-color:white;padding-left:5px !important;padding-right:5px !important">
                        <div class="row p-1" style="background-color:white">
                            <div class="col-12">
                                <table class="table col-md-12 " style="direction: rtl !important;">
                                    <tr style="font-size:18px;color:black !important;color:black;border-top:0px !important">
                                        <th class="text-center" style="border-top:0px !important">#</th>
                                        <th class="text-center" style="border-top:0px !important">رقم المعاملة</th>
                                        <th class="text-center" style="border-top:0px !important">التاريخ</th>
                                        <th class="text-center" style="border-top:0px !important">ID</th>
                                        <th class="text-center" style="border-top:0px !important"> أسم المستفيد</th>
                                        <th class="text-center" style="border-top:0px !important">المبلغ </th>

                                        <th class="text-center" style="border-top:0px !important">فترة الدفع بالأشهر</th>
                                        <th class="text-center" style="border-top:0px !important">آخر شهر مدفوع</th>
                                        <th class="text-center" style="border-top:0px !important">شهر الدفع التالي</th>
                                    </tr>

                                    <tr style="font-size:16px;color:black" v-for="(brand, index) in listItemP1"
                                        v-bind:key="brand.id">
                                        <td class="text-center">{{ index + 1 }}</td>
                                        <td class="text-center">{{ brand.code }}</td>
                                        <td class="text-center">{{ GetDate(brand.date) }}</td>
                                        <td class="text-center">{{ brand.benificaryCode }}</td>
                                        <td class="text-center">{{ brand.benificaryName == '' ? brand.benificaryNameAr :
                                            brand.benificaryName }}</td>
                                        <td class="text-center" style="font-weight: bold;">
                                            {{ parseFloat(brand.totalAmount).toFixed(3).slice(0,
                                                -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,") }}
                                        </td>
                                        <td class="text-center">{{ brand.paymentTypeAr }}</td>
                                        <td class="text-center">
                                            {{ GetDate(brand.lastPaymentDate) }}</td>

                                        <td class="text-center" v-if="brand.paymentType == 'Daily Payment'">
                                            ------</td>
                                        <td class="text-center" v-else-if="brand.paymentType == 'One Time'">
                                            ------</td>
                                        <td class="text-center" v-else-if="brand.durationType == 'Indefinite'">
                                            {{ GetDate2(brand.lastPaymentDate) }}</td>
                                        <td class="text-center" v-else-if="brand.nextMonth > brand.endMonth">
                                            <span>----</span>

                                        </td>
                                        <td class="text-center" v-else>

                                            {{ GetDate2(brand.lastPaymentDate) }}
                                        </td>




                                    </tr>
                                    <tr style="font-size:18px;color:black;font-weight: bold;">

                                        <td colspan="7" style="text-align: left;">المبلغ الإجمالي</td>
                                        <td>
                                            {{ parseFloat(totalAmount()).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                "$1,") }}

                                        </td>

                                    </tr>



                                </table>

                                <table class="table " style="border: none !important;">
                                    <!--Row 1-->
                                    <tr class="mt-2" style="border: none !important;">
                                        <td class="pl-0 pr-0 pt-0 pb-0"
                                            style="width:85%; text-align:right;color:black !important;font-weight:bold;font-size:18px !important;border: none !important;">
                                            {{ brandObj.authorizePersonNameAr }}</td>
                                        <td class="pr-3 pr-0 pt-0 pb-0 "
                                            style="width:15%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: center;border: none !important;">
                                            :اسم الوكيل </td>


                                    </tr>
                                    <tr class="mt-2" style="border: none !important;">
                                        <td class="pl-0 pr-0 pt-0 pb-0"
                                            style="width:85%; text-align:right;color:black !important;font-weight:bold;font-size:18px !important;border: none !important;padding-top: 50px !important;">
                                        </td>
                                        <td class="pr-3 pr-0 pt-0 pb-0 "
                                            style="width:15%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: center;border: none !important;;padding-top: 50px !important;">
                                            :توقيع الوكيل </td>


                                    </tr>





                                </table>



                            </div>




                        </div>


                    </div>

                </div>




            </div>
            <div v-if="itemTotal >= 18 && itemTotal < 33">
                <!--HEADER-->
                <div
                    style="height:55mm; border:2px solid #000000;padding-top: 5px !important;padding-bottom: 5px !important;">
                    <table class="table " style="width: 100%;">
                        <tr>
                            <td style="width:42%;">
                                <p>
                                    <span style="font-size:23px;color:black !important;font-weight:bold;">{{
                                        headerFooters.company.companyNameEnglish }}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">VAT No.:
                                        {{ headerFooters.company.vatRegistrationNo }}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">Cr
                                        No.:{{ headerFooters.company.companyRegNo }}</span><br />
                                    <span style="font-size:17px;color:black !important;font-weight:bold;">
                                        Address: {{ headerFooters.company.addressEnglish }}
                                    </span>
                                </p>
                            </td>
                            <td style="width:24%;">
                                <img :src="headerFooters.company.base64Logo"
                                    style="width:auto;max-width:300px; max-height:100px; padding:5px !important; margin:0 !important">
                            </td>
                            <td style="width:34%;">
                                <p style="text-align: right;">
                                    <span style="font-size:23px;color:black !important;font-weight:bold;">{{
                                        headerFooters.company.companyNameArabic }}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">رقم ضريبة القيمة
                                        المضافة :&nbsp; &nbsp; {{ headerFooters.company.vatRegistrationNo }}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">رقم السجل التجاري
                                        :&nbsp; &nbsp;{{ headerFooters.company.companyRegNo }}</span><br />
                                    <span style="font-size:17px;color:black !important;font-weight:bold;">
                                        عنوان:&nbsp; &nbsp; {{ headerFooters.company.addressArabic }}
                                    </span>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7"
                                style="text-align: center;width:100%;font-size:20px;color:black !important;font-weight:bold;">
                                الدفع عن طريق الوكيل </td>
                        </tr>


                    </table>
                </div>
                <div class="row " style="margin-top:10px !important">
                    <div class="col-md-12 ml-2 mr-2">
                        <table class="table " style="border: none !important;">
                            <!--Row 1-->
                            <tr class="mt-2" style="border: none !important;">
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:35%; text-align:right;color:black !important;font-weight:bold;font-size:18px !important;border: none !important;">
                                    {{ brandObj.code }}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:15%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: center;border: none !important;">
                                    :رقم المعاملة </td>

                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:35%; text-align:right;color:black !important;font-weight:bold;font-size:18px !important;border: none !important;">
                                    {{ GetDate(brandObj.dateTime) }}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:15%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: center;border: none !important;">
                                    :تاريخ الدفع </td>
                            </tr>
                            <tr class="mt-2" style="border: none !important;">
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:35%; text-align:right;color:black !important;font-weight:bold;font-size:18px !important;border: none !important;">
                                </td>
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:15%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: center;border: none !important;">
                                </td>

                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:35%; text-align:right;color:black !important;font-weight:bold;font-size:18px !important;border: none !important;">
                                    {{ GetTime(brandObj.dateTime) }}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:15%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: center;border: none !important;">
                                    : وقت </td>
                            </tr>
                            <tr class="mt-2" style="border: none !important;">
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:35%; text-align:right;color:black !important;font-weight:bold;font-size:18px !important;border: none !important;">
                                    {{ brandObj.authorizedPersonCode }}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:15%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: center;border: none !important;">
                                    : الكود الوكيل</td>

                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:35%; text-align:right;color:black !important;font-weight:bold;font-size:18px !important;border: none !important;">
                                    {{ brandObj.authorizePersonNameAr }}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:15%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: center;border: none !important;">
                                    :اسم الوكيل</td>
                            </tr>
                            <tr class="mt-2" style="border: none !important;">
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:35%; text-align:right;color:black !important;font-weight:bold;font-size:18px !important;border: none !important;">
                                    {{ count() }}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:15%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: center;border: none !important;">
                                    : إجمالي المستفيدين</td>

                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:35%; text-align:right;color:black !important;font-weight:bold;font-size:18px !important;border: none !important;">
                                    {{ parseFloat(totalAmount()).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")
                                    }}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:15%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: center;border: none !important;">
                                    :المبلغ الإجمالي </td>
                            </tr>




                        </table>
                    </div>
                    <!--INFORMATION-->

                    <div class="col-md-12 "
                        style="background-color:white;padding-left:5px !important;padding-right:5px !important">
                        <div class="row p-1" style="background-color:white">
                            <div class="col-12">
                                <table class="table col-md-12 " style="direction: rtl !important;">
                                    <tr style="font-size:18px;color:black !important;color:black;border-top:0px !important">
                                        <th class="text-center" style="border-top:0px !important">#</th>
                                        <th class="text-center" style="border-top:0px !important">رقم المعاملة</th>
                                        <th class="text-center" style="border-top:0px !important">التاريخ</th>
                                        <th class="text-center" style="border-top:0px !important">ID</th>
                                        <th class="text-right" style="border-top:0px !important"> أسم المستفيد</th>
                                        <th class="text-center" style="border-top:0px !important">المبلغ </th>

                                        <th class="text-center" style="border-top:0px !important">فترة الدفع بالأشهر</th>
                                        <th class="text-center" style="border-top:0px !important">آخر شهر مدفوع</th>
                                        <th class="text-center" style="border-top:0px !important">شهر الدفع التالي</th>
                                    </tr>

                                    <tr style="font-size:16px;color:black" v-for="(brand, index) in listItemP1"
                                        v-bind:key="brand.id" st>
                                        <td class="text-center">{{ index + 1 }}</td>
                                        <td class="text-center">{{ brand.code }}</td>
                                        <td class="text-center">{{ GetDate(brand.date) }}</td>
                                        <td class="text-center">{{ brand.benificaryCode }}</td>
                                        <td class="text-right">{{ brand.benificaryName == '' ? brand.benificaryNameAr :
                                            brand.benificaryName }}</td>
                                        <td class="text-center" style="font-weight: bold;">
                                            {{ parseFloat(brand.totalAmount).toFixed(3).slice(0,
                                                -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,") }}
                                        </td>
                                        <td class="text-center">{{ brand.paymentTypeAr }}</td>
                                        <td class="text-center">
                                            {{ GetDate(brand.lastPaymentDate) }}</td>

                                        <td class="text-center" v-if="brand.paymentType == 'Daily Payment'">
                                            ------</td>
                                        <td class="text-center" v-else-if="brand.paymentType == 'One Time'">
                                            ------</td>
                                        <td class="text-center" v-else-if="brand.durationType == 'Indefinite'">
                                            {{ GetDate2(brand.lastPaymentDate) }}</td>
                                        <td class="text-center" v-else-if="brand.nextMonth > brand.endMonth">
                                            <span>----</span>

                                        </td>
                                        <td class="text-center" v-else>

                                            {{ GetDate2(brand.lastPaymentDate) }}
                                        </td>




                                    </tr>
                                    <!-- <tr style="font-size:18px;color:black;font-weight: bold;">

                                        <td colspan="7" style="text-align: left;">المبلغ الإجمالي</td>
                                        <td>
                                            {{ parseFloat(totalAmount()).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                "$1,") }}

                                        </td>

                                    </tr> -->



                                </table>

                                <!-- <table class="table " style="border: none !important;">
                                    <tr class="mt-2" style="border: none !important;">
                                        <td class="pl-0 pr-0 pt-0 pb-0"
                                            style="width:85%; text-align:right;color:black !important;font-weight:bold;font-size:18px !important;border: none !important;">
                                            {{ brandObj.authorizePersonNameAr }}</td>
                                        <td class="pr-3 pr-0 pt-0 pb-0 "
                                            style="width:15%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: center;border: none !important;">
                                           :اسم الوكيل  </td>

                                        
                                    </tr>
                                    <tr class="mt-2" style="border: none !important;">
                                        <td class="pl-0 pr-0 pt-0 pb-0"
                                            style="width:85%; text-align:right;color:black !important;font-weight:bold;font-size:18px !important;border: none !important;padding-top: 50px !important;">
                                            </td>
                                        <td class="pr-3 pr-0 pt-0 pb-0 "
                                            style="width:15%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: center;border: none !important;;padding-top: 50px !important;">
                                           :توقيع الوكيل   </td>

                                        
                                    </tr>





                                </table> -->



                            </div>




                        </div>


                    </div>

                </div>

                <p style="page-break-after: always;margin-bottom:0;margin-top:0;"></p>
                <p style="page-break-before: always;margin-bottom:0;margin-top:0;"></p>


                <div
                    style="height:55mm; border:2px solid #000000;padding-top: 5px !important;padding-bottom: 5px !important;">
                    <table class="table " style="width: 100%;">
                        <tr>
                            <td style="width:42%;">
                                <p>
                                    <span style="font-size:23px;color:black !important;font-weight:bold;">{{
                                        headerFooters.company.companyNameEnglish }}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">VAT No.:
                                        {{ headerFooters.company.vatRegistrationNo }}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">Cr
                                        No.:{{ headerFooters.company.companyRegNo }}</span><br />
                                    <span style="font-size:17px;color:black !important;font-weight:bold;">
                                        Address: {{ headerFooters.company.addressEnglish }}
                                    </span>
                                </p>
                            </td>
                            <td style="width:24%;">
                                <img :src="headerFooters.company.base64Logo"
                                    style="width:auto;max-width:300px; max-height:100px; padding:5px !important; margin:0 !important">
                            </td>
                            <td style="width:34%;">
                                <p style="text-align: right;">
                                    <span style="font-size:23px;color:black !important;font-weight:bold;">{{
                                        headerFooters.company.companyNameArabic }}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">رقم ضريبة القيمة
                                        المضافة :&nbsp; &nbsp; {{ headerFooters.company.vatRegistrationNo }}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">رقم السجل التجاري
                                        :&nbsp; &nbsp;{{ headerFooters.company.companyRegNo }}</span><br />
                                    <span style="font-size:17px;color:black !important;font-weight:bold;">
                                        عنوان:&nbsp; &nbsp; {{ headerFooters.company.addressArabic }}
                                    </span>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7"
                                style="text-align: center;width:100%;font-size:20px;color:black !important;font-weight:bold;">
                                الدفع عن طريق الوكيل </td>
                        </tr>


                    </table>
                </div>
                <div class="row " style="margin-top:10px !important">
                    <div class="col-md-12 ml-2 mr-2">
                        <table class="table " style="border: none !important;">
                            <!--Row 1-->
                            <tr class="mt-2" style="border: none !important;">
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:35%; text-align:right;color:black !important;font-weight:bold;font-size:18px !important;border: none !important;">
                                    {{ brandObj.code }}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:15%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: center;border: none !important;">
                                    :رقم المعاملة </td>

                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:35%; text-align:right;color:black !important;font-weight:bold;font-size:18px !important;border: none !important;">
                                    {{ GetDate(brandObj.dateTime) }}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:15%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: center;border: none !important;">
                                    :تاريخ الدفع </td>
                            </tr>
                            <tr class="mt-2" style="border: none !important;">
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:35%; text-align:right;color:black !important;font-weight:bold;font-size:18px !important;border: none !important;">
                                </td>
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:15%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: center;border: none !important;">
                                </td>

                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:35%; text-align:right;color:black !important;font-weight:bold;font-size:18px !important;border: none !important;">
                                    {{ GetTime(brandObj.dateTime) }}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:15%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: center;border: none !important;">
                                    : وقت </td>
                            </tr>
                            <tr class="mt-2" style="border: none !important;">
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:35%; text-align:right;color:black !important;font-weight:bold;font-size:18px !important;border: none !important;">
                                    {{ brandObj.authorizedPersonCode }}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:15%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: center;border: none !important;">
                                    : الكود الوكيل</td>

                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:35%; text-align:right;color:black !important;font-weight:bold;font-size:18px !important;border: none !important;">
                                    {{ brandObj.authorizePersonNameAr }}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:15%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: center;border: none !important;">
                                    :اسم الوكيل</td>
                            </tr>
                            <tr class="mt-2" style="border: none !important;">
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:35%; text-align:right;color:black !important;font-weight:bold;font-size:18px !important;border: none !important;">
                                    {{ count() }}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:15%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: center;border: none !important;">
                                    : إجمالي المستفيدين</td>

                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:35%; text-align:right;color:black !important;font-weight:bold;font-size:18px !important;border: none !important;">
                                    {{ parseFloat(totalAmount()).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")
                                    }}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0"
                                    style="width:15%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: center;border: none !important;">
                                    :المبلغ الإجمالي </td>
                            </tr>




                        </table>
                    </div>
                    <!--INFORMATION-->

                    <div class="col-md-12 "
                        style="background-color:white;padding-left:5px !important;padding-right:5px !important">
                        <div class="row p-1" style="background-color:white">
                            <div class="col-12">
                                <table class="table col-md-12 " style="direction: rtl !important;">
                                    <tr style="font-size:18px;color:black !important;color:black;border-top:0px !important">
                                        <th class="text-center" style="border-top:0px !important">#</th>
                                        <th class="text-center" style="border-top:0px !important">رقم المعاملة</th>
                                        <th class="text-center" style="border-top:0px !important">التاريخ</th>
                                        <th class="text-center" style="border-top:0px !important">ID</th>
                                        <th class="text-center" style="border-top:0px !important"> أسم المستفيد</th>
                                        <th class="text-center" style="border-top:0px !important">المبلغ </th>

                                        <th class="text-center" style="border-top:0px !important">فترة الدفع بالأشهر</th>
                                        <th class="text-center" style="border-top:0px !important">آخر شهر مدفوع</th>
                                        <th class="text-center" style="border-top:0px !important">شهر الدفع التالي</th>
                                    </tr>

                                    <tr style="font-size:16px;color:black" v-for="(brand, index) in listItemP2"
                                        v-bind:key="brand.id" st>
                                        <td class="text-center">{{ index + 18 }}</td>
                                        <td class="text-center">{{ brand.code }}</td>
                                        <td class="text-center">{{ GetDate(brand.date) }}</td>
                                        <td class="text-center">{{ brand.benificaryCode }}</td>
                                        <td class="text-center">{{ brand.benificaryName == '' ? brand.benificaryNameAr :
                                            brand.benificaryName }}</td>
                                        <td class="text-center" style="font-weight: bold;">
                                            {{ parseFloat(brand.totalAmount).toFixed(3).slice(0,
                                                -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,") }}
                                        </td>
                                        <td class="text-center">{{ brand.paymentTypeAr }}</td>
                                        <td class="text-center">
                                            {{ GetDate(brand.lastPaymentDate) }}</td>

                                        <td class="text-center" v-if="brand.paymentType == 'Daily Payment'">
                                            ------</td>
                                        <td class="text-center" v-else-if="brand.paymentType == 'One Time'">
                                            ------</td>
                                        <td class="text-center" v-else-if="brand.durationType == 'Indefinite'">
                                            {{ GetDate2(brand.lastPaymentDate) }}</td>
                                        <td class="text-center" v-else-if="brand.nextMonth > brand.endMonth">
                                            <span>----</span>

                                        </td>
                                        <td class="text-center" v-else>

                                            {{ GetDate2(brand.lastPaymentDate) }}
                                        </td>




                                    </tr>
                                    <tr style="font-size:18px;color:black;font-weight: bold;">

                                        <td colspan="7" style="text-align: left;">المبلغ الإجمالي</td>
                                        <td>
                                            {{ parseFloat(totalAmount()).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                "$1,") }}

                                        </td>

                                    </tr>



                                </table>

                                <table class="table " style="border: none !important;">
                                    <!--Row 1-->
                                    <tr class="mt-2" style="border: none !important;">
                                        <td class="pl-0 pr-0 pt-0 pb-0"
                                            style="width:85%; text-align:right;color:black !important;font-weight:bold;font-size:18px !important;border: none !important;">
                                            {{ brandObj.authorizePersonNameAr }}</td>
                                        <td class="pr-3 pr-0 pt-0 pb-0 "
                                            style="width:15%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: center;border: none !important;">
                                            :اسم الوكيل </td>


                                    </tr>
                                    <tr class="mt-2" style="border: none !important;">
                                        <td class="pl-0 pr-0 pt-0 pb-0"
                                            style="width:85%; text-align:right;color:black !important;font-weight:bold;font-size:18px !important;border: none !important;padding-top: 50px !important;">
                                        </td>
                                        <td class="pr-3 pr-0 pt-0 pb-0 "
                                            style="width:15%;font-weight:bolder;font-size:18px !important;color:black !important;text-align: center;border: none !important;;padding-top: 50px !important;">
                                            :توقيع الوكيل </td>


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
const options = {
    name: '',
    specs: [
        'fullscreen=no',
        'titlebar=yes',
        'scrollbars=yes'
    ],
    styles: [
        'https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css',
        'https://unpkg.com/kidlat-css/css/kidlat.css',
    ],
    timeout: 1000,
    autoClose: false,
    title : 'الدفع عن طريق الوكيل ',
    windowTitle: 'الدفع عن طريق الوكيل '
}
import moment from "moment";

export default {
    props: ['printDetails', 'brandObj', 'documentName'],
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

    created: function () {

    },

    mounted: function () {
        debugger;
        this.headerFooters.company = this.$store.state.companyList.company;



        var root = this;

        if (this.printDetails.length > 0) {

            this.transactionList = this.printDetails;

            var totalItem = this.transactionList.length;
            this.itemTotal = totalItem;
            if (totalItem < 18) {
                for (let i = 0; i < totalItem; i++) {
                    root.listItemP1.push(root.transactionList[i]);
                }
            }
            else if (totalItem >= 18 && totalItem <= 35) {
                for (let k = 0; k < totalItem; k++) {
                    if (k < 18) {
                        root.listItemP1.push(root.transactionList[k]);
                    }
                    else {
                        root.listItemP2.push(root.transactionList[k]);
                    }
                }
            }


            setTimeout(function () {
                root.printInvoice();
                if (root.documentName == 'Add') {
                    root.$router.push({
                        path: '/authorizepersonlist',

                    })
                }


            }, 125)
        }
    },
    methods: {
        count: function () {

            {
                return this.transactionList.length;
            }
        },
        totalAmount: function () {

            {
                return this.transactionList.reduce(function (a, c) { return a + Number((c.totalAmount) || 0) }, 0)
            }
        },
        GetTime: function (link) {
            if (link !== undefined && link !== null && link !== '') {
                const time = moment(link);
                const arabicTime = time.locale('ar').format('LT'); // 'LT' for Local Time format
                return arabicTime;
            }
            else {
                return '';
            }
        },

        GetDate: function (link) {
            if (link != undefined && link != null && link != '') {

                const date = moment(link);
                const arabicMonth = date.locale('ar').format('MMMM');
                return `${arabicMonth} ${date.format('Do / YYYY')}`;

            }
            else {
                return '';
            }
        },


        GetDate2: function (link) {
            if (link != undefined) {

                const date = moment(link);
                const arabicMonth = date.locale('ar').add(1, 'months').format('MMMM');
                return `${arabicMonth} ${date.format('Do / YYYY')}`;

            }
            else {
                return '';
            }
        },



        printInvoice: function () {

            this.$htmlToPaper('inventoryDetailReport', options, () => {


                console.log('No Rander the Page');


            });
        }
    }
}
</script>


<style scoped></style>