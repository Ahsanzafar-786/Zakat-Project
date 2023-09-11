<template>
<div class="col-lg-12 ">
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box">
                <div class="row">
                    <div class="col">

                        <h4 class="page-title">{{$t('Import.Import')}}</h4>

                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="javascript:void(0);">{{$t('Home')}}</a></li>

                        </ol>
                    </div>
                    <div class="col-auto align-self-center d-flex" v-bind:key="randerAll">
                        <xlsx-workbook >
                            <xlsx-sheet :collection="sheet.data" v-for="sheet in sheets" :key="sheet.name" :sheet-name="sheet.name"  />
                            <xlsx-download :filename="'Item Template.xlsx'" v-bind:key="randerAll">
                                <a class="btn btn-sm btn-outline-primary mx-1" data-toggle="tooltip" data-placement="top" title="Download"><i class="fa fa-download"></i> {{ $t('Import.DownloadTemplate') }}</a>

                            </xlsx-download>
                        </xlsx-workbook>
                        <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);" class="btn btn-sm btn-outline-danger ">
                            {{ $t('Import.colorNameEnglish') }}
                        </a>

                        <!-- <a v-on:click="GetBenificaryData" href="javascript:void(0);" class="btn btn-sm btn-outline-primary ">
                            ReAssign 
                        </a> -->
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="card ">

        <div class="card-body ">
            <div class="row">
                <div class="col-5">
                    <label class="col-form-label">{{ $t('Import.SelectImportType') }}: </label>

                    <multiselect v-model="formName" v-on:input="ImportType" :options="['Authorized','Beneficries','Payment','funds','Payments_Beneficries']" :show-labels="false" :placeholder="$t('Import.SelectType')">
                    </multiselect>

                </div>

            </div>

            <div class="row" :key="render">

                <div class="col-lg-12 ml-auto mr-auto">
                    <label>{{ $t('Import.File') }}</label>
                    <b-form-file v-model="file1" id="uplaodfile" :no-drop="true" accept=".xlsx" :state="Boolean(file1)" @change="onFileChanging" :placeholder="$t('Import.ChooseFile')"></b-form-file>

                </div>
                <div class="col-lg-12 ml-auto mr-auto mt-4">
                    <!--<b-progress :value="totalImportItem" :max="max" :label="'${((totalImportItem / max) * 100).toFixed(2)}%'" show-progress animated></b-progress>-->
                    <b-progress :max="totalImportRecord" height="15px" variant="success">
                        <b-progress-bar :value="totalImportItem" :label="`${((totalImportItem / (totalImportRecord==0?1:totalImportRecord)) * 100).toFixed(0)}%`"></b-progress-bar>
                    </b-progress>
                </div>

                <div class="col-sm-12 col-md-12 col-lg-12 col-xl-12 ml-0 mr-0 mt-4 mb-2">
                    <a href="javascript:void(0)" class="btn btnTotalHover mx-1 " style="border-radius: 20px; background: #DDE9FF; color: #3178F6; ">{{ $t('Import.Total') }} <span>{{totalImportRecord}}</span></a>
                    <a href="javascript:void(0)" class="btn  btnUpdatedHover mx-1" style="border-radius: 20px; background: #B9E9C6; color: #198754; ">{{ $t('Import.Updated') }} <span>{{totalImportItem - errorCollection.length}}</span></a>
                    <a href="javascript:void(0)" class="btn  btnErrorHover mx-1" style="border-radius: 20px; background: #FEDCDC; color: #EB5757 ">{{ $t('Import.Error') }} <span>{{errorCollection.length}}</span></a>
                </div>

            </div>
            <div class="row">
                <div class="card-footer">
                    <div class="row">
                        <div class="col">
                            <div class="d-flex">
                                <xlsx-workbook>
                                    <xlsx-sheet :collection="sheet.data" v-for="sheet in errorSheets" :key="sheet.name" :sheet-name="sheet.name" />
                                    <xlsx-download :filename="'MSF DATA.xlsx'">
                                        <a class="btn btn-sm btn-outline-primary mx-1" data-toggle="tooltip" :disabled="isErrorFileDownload" data-placement="top" title="Download Error File"><i class="fa fa-download"></i> {{ $t('Import.ErrorFile') }}</a>

                                    </xlsx-download>
                                </xlsx-workbook>

                            </div>
                        </div>
                        <div class="col-auto align-self-center d-flex">
                            <button class="btn btn-sm btn-primary mx-1  " @click="uploadFile">
                                <i class="nc-icon nc-cloud-upload-94"></i> {{ $t('Import.Upload') }}
                            </button>
                            <button class="btn btn-sm btn-danger mx-1  " v-on:click="onCancel">
                                {{ $t('Import.Cancel') }}
                            </button>
                        </div>
                    </div>

                </div>
                <!--<div class="col-lg-6 mt-3 ">

                    </div>-->
            </div>
        </div>
    </div>
</div>
</template>

<script>
/*import { BFormFile } from 'bootstrap-vue';*/
import Multiselect from 'vue-multiselect'

import {
    XlsxWorkbook,
    XlsxSheet,
    XlsxDownload
} from "vue-xlsx"
import readXlsxFile from 'read-excel-file'
export default {
    components: {
        //BFormFile,
        XlsxWorkbook,
        Multiselect,
        XlsxSheet,
        XlsxDownload
    },
    data: function () {
        return {
            file1: null,
            loading: false,
            render: 0,
            randerAll: 0,
            year: '',
            fileInterval: '',
            sheets: [],
            collection: [],
            selectedFileData: [],
            isUploadDisabled: true,
            isErrorFileDownload: true,
            errorSheets: [],
            errorCollection: [],
            totalImportItem: 0,
            totalImportRecord: 0,
            wareHouseId: null,
            formName: ''
        }
    },
    methods: {
        GotoPage: function (link) {
            this.$router.push({
                path: link
            });
        },
        GetBenificaryData: function () {
            var root = this;
            var token = '';
            this.loading = true;
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.$https.get('Benificary/BeneficaryPaymentUpdate' , {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            }).then(function (response) {
                if (response.data != null) {
                    
                    root.loading = false;
                }
                root.loading = false;
            });
        },
        onCancel: function () {

        },
        onFileChanging: function (file) {
            debugger;
            this.errorSheets = []
            this.errorCollection = []
            var root = this;
            this.file1 = file.target.files ? event.target.files[0] : null;
            root.selectedFileData = []
            root.totalImportItem = 0;
            root.totalImportRecord = 0;
            root.isErrorFileDownload = true
            readXlsxFile(this.file1).then((allRows) => {
                if (allRows.length > 1) {

                    if ((root.formName == 'Authorized')) {
                        allRows.splice(0, 1)
                        allRows.forEach(function (data) {
                            root.selectedFileData.push({

                                id: data[0],
                                org_id: data[1],
                                isactive: data[2],
                                stamp_date: data[3],
                                sync_erp: data[4],
                                created_date: data[5],
                                edited_date: data[6],
                                name: data[7],
                                created_by_id: data[8],
                                edited_by_id: data[9],

                            })

                        })

                        root.totalImportRecord = allRows.length;
                        root.isUploadDisabled = false;
                    } else if ((root.formName == 'Beneficries')) {
                        allRows.splice(0, 1)
                        allRows.forEach(function (data) {
                            root.selectedFileData.push({

                                id: data[0],
                                org_id: data[1],
                                isactive: data[2],
                                stamp_date: data[3],
                                sync_erp: data[4],
                                created_date: data[5],
                                edited_date: data[6],
                                name: data[7],
                                phone: data[8],
                                payment_interval: data[9],
                                recurring_amount: data[10],
                                iqama_no: data[11],
                                authorized_person_id: data[12],
                                created_by_id: data[13],
                                edited_by_id: data[14],

                            })

                        })
                        root.totalImportRecord = allRows.length;
                        root.isUploadDisabled = false;
                    } else if ((root.formName == 'Payments_Beneficries')) {
                        allRows.splice(0, 1)
                        allRows.forEach(function (data) {

                          
                            root.selectedFileData.push({

                                id: data[0],
                                org_id: data[1],
                                isactive: data[2],
                                stamp_date: data[3],
                                sync_erp: data[4],
                                created_date: data[5],
                                edited_date: data[6],
                                note: data[7],
                               beneficiary_id: data[8],

                            })

                        })
                        root.totalImportRecord = allRows.length;
                        root.isUploadDisabled = false;
                    }
                     else if ((root.formName == 'funds')) {
                        allRows.splice(0, 1)
                        allRows.forEach(function (data) {
                            root.selectedFileData.push({

                                id: data[0],
                                org_id: data[1],
                                isactive: data[2],
                                stamp_date: data[3],
                                sync_erp: data[4],
                                created_date: data[5],
                                edited_date: data[6],
                                created_by_id: data[7],
                                edited_by_id: data[8],
                                Transection_Type: data[9],
                                Amount: data[9],
                                cheque_number: data[10]

                            })

                        })
                        root.totalImportRecord = allRows.length;
                        root.isUploadDisabled = false;
                    } 
                   
                    else if ((root.formName == 'Payment')) {

                       

                        allRows.splice(0, 1)
                        allRows.forEach(function (data) {
                            root.selectedFileData.push({

                                id: data[0],
                                org_id: data[1],
                                isactive: data[2],
                                stamp_date: data[3],
                                sync_erp: data[4],
                                created_date: data[5],
                                edited_date: data[6],
                                amount: data[7],
                                month: data[8],
                                year: data[9],
                                period: data[10],
                                beneficiary_id: data[11],
                                cashier_id: data[12],
                                created_by_id: data[13],
                                by_order: data[14]
                                

                            })

                        })
                        root.totalImportRecord = allRows.length;
                        root.isUploadDisabled = false;
                    }
                     else {
                        root.file1 = null
                        root.$swal({
                            title: 'Wrong File',
                            text: "Please select correct file",
                            type: 'warning',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });
                    }

                }
            })

        },
        ImportType: function () {
            debugger;
            this.selectedFileData = [];
            this.file1= null;
            this.fileInterval= '';
            this.sheets= [];
            this.collection = [];
            this.selectedFileData = [];
            this.isUploadDisabled= true;
            this.isErrorFileDownload= true;
            this.errorSheets= [];
            this.errorCollection= [];
            this.totalImportItem= 0;
            this.totalImportRecord=0;
            this.randerAll++;




            if (this.formName == 'Authorized') {
                this.collection = ["AuthorizedPersonCode", "org_id", "isactive", "stamp_date", "sync_erp", "created_date", "edited_date", "NameAr", "created_by_id", "edited_by_id"]
                this.sheets = [];
                this.sheets.push({
                    name: "payment_authorizedperson",
                    data: [this.collection]
                });
            } else if (this.formName == 'Beneficries') {
                this.collection = ["id", "org_id", "isactive", "stamp_date", "sync_erp", "created_date", "edited_date", "name", "phone", "payment_interval", "recurring_amount",
                    "iqama_no", "authorized_person_id"
                ];
                this.sheets = [];
                this.sheets.push({
                    name: "payment_beneficiary",
                    data: [this.collection]
                });
            } else if (this.formName == 'Payments_Beneficries') {
                this.collection = ["id", "org_id", "isactive", "stamp_date", "sync_erp", "created_date", "edited_date", "note", "beneficiary_id", "Created_By", "Edit_By",
                    "iqama_no", "authorized_person_id"
                ];
                this.sheets = [];
                this.sheets.push({
                    name: "payment_beneficiary",
                    data: [this.collection]
                });
            } else if (this.formName == 'Payment') {
                this.collection = ["id", "org_id", "isactive", "stamp_date", "sync_erp", "created_date", "edited_date", "amount","month","year","period", "beneficiary_id", "cashier_id", "created_by_id", "by_order"
                    
                ];
                this.sheets = [];
                this.sheets.push({
                    name: "Sheet1",
                    data: [this.collection]
                });
            } else if (this.formName == 'funds') {
                this.collection = ["id", "org_id", "isactive", "stamp_date", "sync_erp", "created_date", "edited_date", "Amount", "Month", "Year", "Period", "beneficiary_id",
                    "iqama_no", "authorized_person_id"
                ];
                this.sheets = [];
                this.sheets.push({
                    name: "fund",
                    data: [this.collection]
                });
            }

        },
        uploadFile: function () {
            debugger;

            this.loading = true;
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.loding == true;

            root.isUploadDisabled = true;
            var url = ''
            var rows = ''

            if (root.formName == 'Beneficries') {
                rows = this.selectedFileData;
                url = '/Benificary/UploadFilesForBeneficary'
            } else if (root.formName == 'Authorized') {
                rows = this.selectedFileData;
                url = '/Benificary/UploadFilesForImportAuthorize'
            } else if (root.formName == 'Payments_Beneficries') {
                rows = this.selectedFileData;
                url = '/Benificary/Payments_Beneficries'
            } else if (root.formName == 'funds') {
                rows = this.selectedFileData;
                url = '/Benificary/Funds'
            } else if (root.formName == 'Payment') {
                rows = this.selectedFileData.splice(0, 2000);
                url = '/Benificary/PaymentsBeneficry'
            }

            root.$https.post(url, rows, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                .then(function (response) {
                    debugger;
                    if (response.data != null) {
                        debugger;
                        root.totalImportItem += rows.length
                        if (response.data.length > 0) {

                            if (root.formName == 'Authorized') {
                                response.data.forEach(function (x) {
                                    var errorData = {
                                        AuthorizedPersonCode: x.AuthorizedPersonCode,
                                        NameAr: x.NameAr,

                                        ErrorDescription: x.errorDescription,
                                    }

                                    root.errorCollection.push(errorData)
                                })
                            } else if (root.formName == 'Payments_Beneficries') {
                                response.data.forEach(function (x) {
                                    var errorData = {
                                        AuthorizedPersonCode: x.AuthorizedPersonCode,
                                        NameAr: x.NameAr,

                                        ErrorDescription: x.errorDescription,
                                    }

                                    root.errorCollection.push(errorData)
                                })
                            } else if (root.formName == 'funds') {
                                response.data.forEach(function (x) {
                                    var errorData = {
                                        AuthorizedPersonCode: x.AuthorizedPersonCode,
                                        NameAr: x.NameAr,

                                        ErrorDescription: x.errorDescription,
                                    }

                                    root.errorCollection.push(errorData)
                                })
                            } else if (root.formName == 'Beneficries') {
                                response.data.forEach(function (x) {
                                    var errorData = {
                                        AuthorizedPersonCode: x.AuthorizedPersonCode,
                                        NameAr: x.NameAr,

                                        ErrorDescription: x.errorDescription,
                                    }

                                    root.errorCollection.push(errorData)
                                })
                            } else if (root.formName == 'Payment') {
                                response.data.forEach(function (x) {
                                    var errorData = {
                                        AuthorizedPersonCode: x.AuthorizedPersonCode,
                                        NameAr: x.NameAr,

                                        ErrorDescription: x.errorDescription,
                                    }

                                    root.errorCollection.push(errorData)
                                })
                            }
                            // root.errorCollection.push(response.data)

                        }
                        if (root.selectedFileData.length > 0) {
                            root.uploadFile()
                        } else {
                            root.errorSheets.push({
                                name: "Template",
                                data: [...root.errorCollection]
                            });
                            if (root.errorCollection.length > 0) {
                                root.isErrorFileDownload = false
                            }

                            root.file1 = null;

                        }
                    }

                });

        },

        DownloadRecordForStockIn: function () {

            this.loading = true;
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.loding == true;
            root.isUploadDisabled = true;
            root.$https.get('/Product/DownloadStockFileAsync', {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                .then(function (response) {
                    if (response.data != null) {
                        if (response.data.length > 0) {
                            root.collection = []
                            // root.errorCollection.push(response.data)
                            response.data.forEach(function (x) {
                                var data = {
                                    ProductCode: x.productCode,
                                    ProductNameEnglish: x.productNameEnglish,
                                    ProductNameArabic: x.productNameArabic,
                                    Quantity: x.quantity,
                                    UnitPrice: x.unitPrice,
                                }
                                root.collection.push(data)
                            })
                            root.sheets.push({
                                name: "Template",
                                data: [...root.collection]
                            });
                        }

                    }

                });

        }
    },
    mounted: function () {
        debugger;

        // this.formName = 'Authorized';

    }
}
</script>

<style>

</style><style scoped>
.btnTotalHover:hover {
    background-color: #DDE9FF !important;
    color: #3178F6 !important;
}

.btnUpdatedHover:hover {
    background-color: #B9E9C6 !important;
    color: #198754 !important;
}

.btnErrorHover:hover {
    background-color: #FEDCDC !important;
    color: #EB5757 !important;
}
</style>
