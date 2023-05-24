import Vue from 'vue'
import App from './App.vue'
import router from './router'
import { store } from './store'
import Vuelidate from 'vuelidate'
import i18n from './i18n'
import cors from 'cors'
import moment from 'moment'
import axios from 'axios'
import VueSweetalert2 from 'vue-sweetalert2';
import 'sweetalert2/dist/sweetalert2.min.css';
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/vue-loading.css';
import { BootstrapVue, BPagination } from 'bootstrap-vue'
Vue.use(BootstrapVue, BPagination);
import ElementUI from 'element-ui'
import { DatePicker } from 'element-ui';
import ar from 'element-ui/lib/locale/lang/ar'
import en from 'element-ui/lib/locale/lang/en'
import pt from 'element-ui/lib/locale/lang/pt'


import 'element-ui/lib/theme-chalk/index.css';
import locale from 'element-ui/lib/locale'
Vue.use(DatePicker);

Vue.use(ElementUI, { locale });
import 'vue2-timepicker/dist/VueTimepicker.css'
import 'vue-multiselect/dist/vue-multiselect.min.css';
import VueHtmlToPaper from 'vue-html-to-paper';
import ToggleButton from 'vue-js-toggle-button';
import clickMixin from '@/Mixins/clickMixin';
import json from '../public/Config.json';
import VueBarcodeScanner from 'vue-barcode-scanner'
import shortKey from "vue-shortkey";

import { MonthPicker } from 'vue-month-picker'
import { MonthPickerInput } from 'vue-month-picker'
import VueCryptojs from 'vue-cryptojs'
import vueshortkey from 'vue-shortkey'

Vue.use(VueCryptojs)
Vue.use(MonthPicker)
Vue.use(MonthPickerInput)
Vue.use(clickMixin);
Vue.use(vueshortkey);

//Vue.use(VueRadioButton);
Vue.use(ToggleButton);
Vue.prototype.$locale = locale;
Vue.use(VueBarcodeScanner);
Vue.use(shortKey);


if (i18n.locale === 'en') {
    locale.use(en)
}
else if (i18n.locale === 'pt') {
    locale.use(pt)
}
else {
    locale.use(ar);
}



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
    autoClose: true,
    windowTitle: window.document.title
}
Vue.use(VueHtmlToPaper, options);
Vue.use(VueHtmlToPaper);
import VueApexCharts from 'vue-apexcharts';
Vue.use(VueApexCharts);
Vue.component('apexchart', VueApexCharts);
//import the theme

// register VueFusionCharts component
Vue.use(VueSweetalert2);
Vue.use(Loading);
Vue.use(Vuelidate);
Vue.use(cors);
Vue.use(moment);
Vue.config.productionTip = false;
Vue.component('yeardropdown', require('./components/General/YearDropdown.vue').default);
Vue.component('grouping', require('./components/General/Grouping.vue').default);
Vue.component('email-compose', require('./components/General/EmailCompose.vue').default);
Vue.component('email-compose-received', require('./components/General/EmailComposeReceived.vue').default);
Vue.component('category-multidropdown', require('./components/General/CategoryMultiDropdown.vue').default);
Vue.component('departmentmodel', require('./components/Department/AddDepartment.vue').default);
Vue.component('categorymodal', require('./components/Category/AddCategory.vue').default);
Vue.component('subcategorymodal', require('./components/SubCategory/AddSubCategory.vue').default);
Vue.component('brandmodal', require('./components/Brand/AddBrand.vue').default);
Vue.component('originmodal', require('./components/Origin/AddOrigin.vue').default);
Vue.component('unitmodal', require('./components/Unit/AddUnit.vue').default);
Vue.component('sizemodal', require('./components/Size/AddSize.vue').default);
Vue.component('colormodal', require('./components/Color/AddColor.vue').default);
Vue.component('taxratemodal', require('./components/TaxRate/AddTaxRate.vue').default);
//Vue.component('categorydropdown', require('./components/General/CategoryDropdown.vue').default);
Vue.component('companyprocessdropdown', require('./components/General/CompanyProcessDropdown.vue').default);
Vue.component('subcategorydropdown', require('./components/General/SubCategoryDropdown.vue').default);
Vue.component('branddropdown', require('./components/General/BrandDropdown.vue').default);
Vue.component('origindropdown', require('./components/General/OriginDropdown.vue').default);
Vue.component('unitdropdown', require('./components/General/UnitDropdown.vue').default);
Vue.component('unitleveldropdown', require('./components/General/UnitLevelDropdown.vue').default);
Vue.component('sizedropdown', require('./components/General/SizeDropdown.vue').default);
Vue.component('size-multiselect-dropdown', require('./components/General/SizeMultiSelectDropdown.vue').default);
Vue.component('colordropdown', require('./components/General/ColorDropdown.vue').default);
Vue.component('color-multiselect-dropdown', require('./components/General/Color-Multiselect-Dropdown.vue').default);
Vue.component('warranty-type-dropdown', require('./components/General/WarrantyTypeDropdown.vue').default);
Vue.component('salutation-dropdown', require('./components/General/SalutationDropdown.vue').default);
Vue.component('display-name-dropdown', require('./components/General/DisplayNameDropdown.vue').default);
Vue.component('proforma-invoice-dropdown', require('./components/General/ProformaInvoiceDropdown.vue').default);

Vue.component('DeliveryChallanDropdown', require('./components/General/DeliveryChallanDropdown.vue').default);

//Leave
Vue.component('addleavetypes', require('./components/Hr/LeaveManagement/LeaveTypes/AddLeaveTypes.vue').default);
Vue.component('addleaveperiod', require('./components/Hr/LeaveManagement/LeavePeriod/AddLeavePeriod.vue').default);
Vue.component('addworkweek', require('./components/Hr/LeaveManagement/WorkWeek/AddWorkWeek.vue').default);
Vue.component('addholiday', require('./components/Hr/LeaveManagement/Holiday/AddHoliday.vue').default);
Vue.component('addleaverules', require('./components/Hr/LeaveManagement/LeaveRules/AddLeaveRules.vue').default);
Vue.component('addleavegroups', require('./components/Hr/LeaveManagement/LeaveGroups/AddLeaveGroups.vue').default);
Vue.component('addleavegroups', require('./components/Hr/LeaveManagement/LeaveGroups/AddLeaveGroups.vue').default);
Vue.component('addpaidtimeoff', require('./components/Hr/LeaveManagement/PaidTimeOff/AddPaidTimeOff.vue').default);


Vue.component('leavegroupdropdown', require('./components/General/LeaveGroupDropdown').default);
Vue.component('leavetypedropdown', require('./components/General/LeaveTypeDropdown').default);
Vue.component('leaveperioddropdown', require('./components/General/LeavePeriodDropdown').default);


Vue.component('taxratedropdown', require('./components/General/TaxRateDropdown.vue').default);
Vue.component('logisticDropdown', require('./components/General/LogisticDropdown.vue').default);
Vue.component('brandmodel', require('./components/Brand/AddBrand.vue').default);
Vue.component('originmodel', require('./components/Origin/AddOrigin.vue').default);
Vue.component('unitmodel', require('./components/Unit/AddUnit.vue').default);
Vue.component('addRoles', require('./components/UserRoles/AddRoles.vue').default);
Vue.component('addPermissions', require('./components/UserPermissions/AddPermissions.vue').default);
Vue.component('sizemodel', require('./components/Size/AddSize.vue').default);
Vue.component('colormodel', require('./components/Color/AddColor.vue').default);
Vue.component('addholidaysetup', require('./components/Hr/ShiftManagement/AddHolidaySetup.vue').default);
Vue.component('taxratemodel', require('./components/TaxRate/AddTaxRate.vue').default);
Vue.component('currencymodel', require('./components/Currency/AddCurrency.vue').default);
Vue.component('currency-dropdown', require('./components/General/CurrencyDropdown.vue').default);
Vue.component('purchaseorder-payment', require('./components/PurchaseOrder/PurchaseOrderPayment.vue').default);
Vue.component('purchaseorder-expense', require('./components/PurchaseOrder/PurchaseOrderExpense.vue').default);
Vue.component('paymentmodel', require('./components/Payment_Options/AddPaymentOptions.vue').default);
Vue.component('PaymentVoucherItem', require('./components/PaymentVouchers/PaymentVoucherItem.vue').default);
Vue.component('productfiltermodel', require('./components/Product/ProductFilterModel.vue').default);
Vue.component('AddAddress', require('./components/Customer/AddAddress.vue').default);
Vue.component('invoicedetailsprint', require('./components/SaleServiceInvoice/InvoiceDetailsPrint.vue').default);

Vue.component('supervisor-login-model', require('./components/DayStart/SupervisorLogin.vue').default);
Vue.component('transfer-login-model', require('./components/DayStart/TransferCounter.vue').default);
Vue.component('cash-receiving-model', require('./components/DayStart/CashReceiving.vue').default);
Vue.component('adddaystartmodel', require('./components/DayStart/AddDayStart.vue').default);
Vue.component('adddayendmodel', require('./components/DayStart/AddDayEnd.vue').default);
Vue.component('cashcounterdetail', require('./components/TouchScreen/CashCounterDetail.vue').default);
Vue.component('day-register-modal', require('./components/DayStart/DayRegister.vue').default);
Vue.component('day-counter-modal', require('./components/DayStart/DayCounter.vue').default);
Vue.component('walk-customer-modal', require('./components/TouchScreen/WalkCustomerModel.vue').default);
Vue.component('hold-modal', require('./components/TouchScreen/HoldModel.vue').default);
Vue.component('unhold-modal', require('./components/TouchScreen/UnHoldModel.vue').default);
Vue.component('return-item-model', require('./components/TouchScreen/ReturnItemModel.vue').default);
Vue.component('denominationSetupmodel', require('./components/DenominationSetup/AddDenominationSetup.vue').default);

//view stock invoice print
Vue.component('viewStockInPrintDownload', require('./components/Reports/ViewStockInPrintDownload.vue').default);
Vue.component('viewStockInPrint', require('./components/Reports/ViewStockInPrint.vue').default);
Vue.component('dayEndReportPrint', require('./components/Reports/DayEndReportPrint.vue').default);
Vue.component('dayEndA4ReportPrint', require('./components/Reports/DayEndA4ReportPrint.vue').default);
Vue.component('wholeSaleDayEndReport', require('./components/Reports/WholeSaleDayEndReport.vue').default);
Vue.component('product-inventory-Report-print', require('./components/Reports/ProductInventoryReportPrint .vue').default);

Vue.component('billsReport', require('./components/Reports/BillsReport.vue').default);
Vue.component('BankTransactionReportPrint', require('./components/Reports/BankTransactionReportPrint.vue').default);
Vue.component('InvoiceReportPrint', require('./components/Reports/InvoiceReportPrint.vue').default);
Vue.component('invoiceReportPrintDownload', require('./components/Reports/InvoiceReportPrintDownload.vue').default);
Vue.component('BankTransactionReport', require('./components/Reports/BankTransactionReport.vue').default);
Vue.component('sale-service-invoice-a4', require('./components/Reports/SaleServiceInvoiceA4.vue').default);



Vue.component('priceLabelingmodel', require('./components/PriceLabeling/AddPriceLabeling.vue').default);
Vue.component('priceLabelingDropdown', require('./components/General/PriceLabelingDropdown.vue').default);


//Bank Model
Vue.component('bankmodel', require('./components/Bank/AddBank.vue').default);
Vue.component('PaymentModel', require('./components/PurchaseBill/PaymentModel.vue').default);

//Quick Product Item
Vue.component('quickProductItem', require('./components/TouchScreen/QuickProductItem.vue').default);

Vue.component('TransferModel', require('./components/ProductionBatch/TransferModel.vue').default);
Vue.component('ProcessModel', require('./components/ProductionBatch/ProcessModel.vue').default);
Vue.component('CompletionModel', require('./components/ProductionBatch/CompletionModel.vue').default);
Vue.component('productMastermodel', require('./components/ProductMaster/AddProductMaster.vue').default);

//User&UserRoles Dropdown_Modals
Vue.component('usersDropdown', require('./components/General/UsersDropdown.vue').default);
Vue.component('acessdenied', require('./components/General/AcessDenied.vue').default);
Vue.component('userRolesDropdown', require('./components/General/UserRolesDropdown.vue').default);
Vue.component('moduleNamesDropdown', require('./components/General/ModuleNamesDropdown.vue').default);
Vue.component('moduleCategoryDropdown', require('./components/General/ModuleCategoryDropdown.vue').default);
Vue.component('moduleNamesDropdownForAdd', require('./components/General/ModuleNamesDropdownForAdd.vue').default);
Vue.component('printer-list-dropdown', require('./components/General/PrinterListDropdown.vue').default);

Vue.component('assignRoleToUsers', require('./components/UserPermissions/AssignRoleToUsers.vue').default);
Vue.component('assignPermissionsToRole', require('./components/UserPermissions/AssignPermissionsToRole.vue').default);
Vue.component('assignPermissionsToRoleForAdd', require('./components/UserPermissions/AssignPermissionsToRoleForAdd.vue').default);
Vue.component('updatePermissionsToRole', require('./components/UserPermissions/UpdatePermissionsToRole.vue').default);

//Quick Employee Model
Vue.component('quickemployeemodel', require('./components/EmployeeRegistration/AddQuickEmployee.vue').default);
Vue.component('AddPriceRecord', require('./components/PriceRecord/AddPriceRecord.vue').default);

Vue.component('terminalmodel', require('./components/Terminal/AddTerminal.vue').default);
Vue.component('modal', require('./components/modalcomponent.vue').default);
Vue.component('datepicker', require('./components/DatePicker.vue').default);
Vue.component('coa-modal', require('./components/COA/chartOfAccountModal.vue').default);
Vue.component('addupdate-coa', require('./components/COA/AddUpdateAccount.vue').default);
Vue.component('licence-model', require('./components/Company/LicenceModel.vue').default);
Vue.component('ftp-account-detail', require('./components/Company/FtpAccountDetail.vue').default);
Vue.component('licence-history-model', require('./components/Company/LicenceHistoryModel.vue').default);
Vue.component('company-attachment-modal', require('./components/Company/CompanyAttachmentModal.vue').default);
Vue.component('company-attachments', require('./components/Company/CompanyAttachments.vue').default);

Vue.component('paymentvoucherlinedetail', require('./components/PaymentVouchers/AddPaymentVoucherDetails/AddPaymentVoucherDetail.vue').default);
Vue.component('stocklinedetail', require('./components/Product_Stock_Value/StockDetails/StockLineDetails.vue').default);
Vue.component('viewstockitem', require('./components/Product_Stock_Value/ViewStockItem.vue').default);

//Journal Voucher Items
Vue.component('jvitems', require('./components/JournalVouchers/AddJvLineItem/AddLineItem.vue').default);
//Daily Expense Row
Vue.component('adddailyexpenserow', require('./components/DailyExpense/AddDailyExpenseLineItem/AddLineItem.vue').default);
//Mobile Order Items
Vue.component('mobile-item', require('./components/MobileOrder/MobileItem.vue').default);

//DropDown

Vue.component('productMasterdropdown', require('./components/General/ProductMasterDropdown.vue').default);
Vue.component('accountdropdown', require('./components/General/AccountNumberDropdown.vue').default);
Vue.component('currencyinput', require('./components/General/InputDropdown.vue').default);
Vue.component('categorydropdown', require('./components/General/CategoryDropdown.vue').default);
Vue.component('supplierdropdown', require('./components/General/SupplierDropdown.vue').default);
Vue.component('customerdropdown', require('./components/General/CustomerDropdown.vue').default);
Vue.component('customerdropdown1', require('./components/General/CustomerDropdown1.vue').default);
Vue.component('add-purchase', require('./components/Purchase/AddPurchase.vue').default);
Vue.component('purchase-item', require('./components/Purchase/PurchaseItem.vue').default);
Vue.component('purchase-costing-item', require('./components/PurchaseCosting/PurchaseCostingItem.vue').default);
Vue.component('purchase-view-item', require('./components/Purchase/PurchaseViewItem.vue').default);
Vue.component('poItem', require('./components/PurchaseOrder/PurchaseItem.vue').default);
Vue.component('product-dropdown', require('./components/General/ProductDropdown.vue').default);
Vue.component('product-for-promotion-dropdown', require('./components/General/ProductForPromotionDropdown.vue').default);

Vue.component('warehouse-dropdown', require('./components/General/WarehouseDropdown.vue').default);
Vue.component('employeeDropdown', require('./components/General/EmployeeDropdown.vue').default);
Vue.component('purchaseinvoicedropdown', require('./components/General/PurchaseInvoiceDropDown.vue').default);
Vue.component('productMultiSelectDropdown', require('./components/General/ProductMultiSelectDropdown.vue').default);
Vue.component('company-dropdown', require('./components/General/CompanyDropdown.vue').default);
Vue.component('terminal-dropdown', require('./components/General/TerminalDropDown.vue').default);
Vue.component('terminal-dropdown', require('./components/General/TerminalDropDown.vue').default);
Vue.component('barcodeDropdown', require('./components/General/BarcodeDropdown.vue').default);
Vue.component('my-currency-input', require('./components/General/DecimalDropdown.vue').default);
Vue.component('BillsDropdown', require('./components/General/BillsDropdown.vue').default);

Vue.component('decimal-to-fixed', require('./components/General/DecimalToFixed.vue').default);
Vue.component('expense-type-dropdown', require('./components/General/ExpensetypeDropdown.vue').default);
Vue.component('paymentVoucherdesign2', require('./components/Reports/PaymentVoucher2.vue').default);
Vue.component('PaymentVoucherPakistan', require('./components/Reports/PaymentVoucherPakistan.vue').default);
Vue.component('creditnote', require('./components/Reports/CreditNote.vue').default);



Vue.component('rawProductDropdown', require('./components/General/RawProductDropdown.vue').default);
Vue.component('PaymentImageList', require('./components/PaymentImageList.vue').default);

Vue.component('add-serial-model', require('./components/Sale/AddSerialModel.vue').default);
Vue.component('add-sale', require('./components/Sale/AddSale.vue').default);
Vue.component('sale-item', require('./components/Sale/SaleItem.vue').default);
Vue.component('credit-payment', require('./components/Sale/CreditPayment.vue').default);
Vue.component('sale', require('./components/Sale/Sale.vue').default);
Vue.component('select-batch', require('./components/Sale/SelectBatch.vue').default);
Vue.component('payment-option-dropdown', require('./components/General/PaymentOptionDropdown.vue').default);
Vue.component('sale-payment-detail', require('./components/Sale/SalePaymentDetail.vue').default);
Vue.component('sale-return', require('./components/SaleReturn/SaleReturn.vue').default);
Vue.component('add-sale-return', require('./components/SaleReturn/AddSaleReturn.vue').default);
Vue.component('sale-invoice-dropdown', require('./components/General/SaleInvoiceDropdown.vue').default);
Vue.component('cash-customer-dropdown', require('./components/General/CashCustomerDropdown.vue').default);
Vue.component('sale-return-item', require('./components/SaleReturn/SaleReturnItem.vue').default);
Vue.component('view-sale-return-item', require('./components/SaleReturn/ViewSaleReturnItem.vue').default);

Vue.component('permissionCategoryDropdown', require('./components/General/PermissionCategoryDropdown.vue').default);

//Sale Service
Vue.component('sale-service-item', require('./components/SaleServiceInvoice/SaleServiceItem.vue').default);
Vue.component('sale-service-view-item', require('./components/SaleServiceInvoice/InvoiceServiceViewItem.vue').default);
Vue.component('sale-service-order-item', require('./components/SaleServiceOrder/SaleServiceOrderItem.vue').default);
Vue.component('sale-service-order-view-item', require('./components/SaleServiceOrder/SaleServiceOrderViewItem.vue').default);

//Report Design For Customer
Vue.component('cash-voucher-report', require('./components/Reports/CashVoucherReport.vue').default);
Vue.component('readymade-garments', require('./components/Reports/ReadymadeGarments.vue').default);
Vue.component('ReparingOrderMultiPrint', require('./components/Reports/ReparingOrderMultiPrint.vue').default);
Vue.component('ReparingOrderThermalPrint', require('./components/Reports/ReparingOrderThermalPrint.vue').default);
Vue.component('SalesThermalpkReport', require('./components/Reports/SalesThermalpkReports.vue').default);
Vue.component('SalesThermalpkReport2', require('./components/Reports/SalesThermalpkReports2.vue').default);
Vue.component('SalesThermalSaudiReports3', require('./components/Reports/SalesThermalSaudiReports3.vue').default);
Vue.component('SalesThermalSaudiReports4', require('./components/Reports/SalesThermalSaudiReports4.vue').default);
Vue.component('pos-invoice-template1', require('./components/Reports/PosInvoiceTemplate1.vue').default);
Vue.component('jamal-fabric-report', require('./components/Reports/JamalFabricReport.vue').default);

Vue.component('productPrintReport', require('./components/Reports/ProductPrintReport.vue').default);

Vue.component('addExpense-type', require('./components/ExpenseType/AddExpenseType.vue').default);

//FOR Reports
Vue.component('purchaseReturnDownload', require('./components/Reports/PurchaseReturnDownload.vue').default);
Vue.component('purchaseReturn', require('./components/Reports/PurchaseReturn.vue').default);
Vue.component('purchaseOrderDownload', require('./components/Reports/PurchaseOrderDownload.vue').default);
Vue.component('GoodsReceiveDownload', require('./components/Reports/GoodsReceiveDownload.vue').default);
Vue.component('purchaseOrder', require('./components/Reports/PurchaseOrder.vue').default);
Vue.component('GoodReceive', require('./components/Reports/GoodReceive.vue').default);
Vue.component('purchaseInvoice', require('./components/Reports/PurchaseInvoice.vue').default);
Vue.component('purchaseInvoiceDownload', require('./components/Reports/PurchaseInvoiceDownload.vue').default);
Vue.component('SaleInvoiceView', require('./components/Reports/SaleInvoiceView.vue').default);
Vue.component('SaleDashboardViewReport', require('./components/Reports/SaleDashboardReport.vue').default);
Vue.component('saleinvoice-dotMatrixprint', require('./components/Reports/SaleInvoiceDotMatrixPrint.vue').default);
Vue.component('SaleInvoiceForMsFkahryPrint', require('./components/Reports/SaleInvoiceForMsFkahryPrint.vue').default);
Vue.component('saleInvoice-template-one', require('./components/Reports/SaleInvoiceTemplateOne.vue').default);
Vue.component('SmartDigitalInvoice', require('./components/Reports/SmartDigitalInvoice.vue').default);
Vue.component('QuotationSmartDigitalInvoice', require('./components/Reports/QuotationSmartDigitalInvoice.vue').default);

Vue.component('SaleInvoiceTemplate5', require('./components/Reports/SaleInvoiceTemplate5.vue').default);
Vue.component('ObaagestSaleInvoice', require('./components/Reports/ObaagestSaleInvoice.vue').default);
Vue.component('saleInvoice', require('./components/Reports/SaleInvoice.vue').default);
Vue.component('dailyExpenseA4', require('./components/Reports/DailyExpenseA4Report.vue').default);
Vue.component('dailyExpenseThreeInch', require('./components/Reports/DailyExpenseThreeInchReport.vue').default);
Vue.component('saleReturnReport', require('./components/Reports/SaleReturnReport.vue').default);
Vue.component('saleorder', require('./components/Reports/SaleOrder.vue').default);
Vue.component('quotation', require('./components/Reports/Quotation.vue').default);
Vue.component('paymentVoucherInvoice', require('./components/Reports/PaymentVoucherInvoice.vue').default);
Vue.component('stock-report-print', require('./components/Reports/StockReportPrint.vue').default);
Vue.component('stock-report-print-download', require('./components/Reports/StockReportPrintDownload.vue').default);
Vue.component('supplier-purchase-report-print', require('./components/Reports/SupplierPurchaseReportPrint.vue').default);
Vue.component('supplier-purchase-report-download', require('./components/Reports/SupplierPurchaseReportDownload.vue').default);



Vue.component('retail-report-print', require('./components/Reports/RetailReportPrint.vue').default);
Vue.component('inventoryFilterReportPrint', require('./components/Reports/InventoryFilterReportPrint.vue').default);
Vue.component('productInventoryRegisterPrintReport', require('./components/Reports/ProductInventoryRegisterPrintReport.vue').default);
Vue.component('productStockValuePrintReport', require('./components/Reports/ProductStockValuePrintReport.vue').default);
Vue.component('productStockAvgValuePrintReport', require('./components/Reports/ProductStockAvgValuePrintReport.vue').default);
Vue.component('customerWiseProductSalePrintReport', require('./components/Reports/CustomerWiseProductSalePrintReport.vue').default);
Vue.component('customersWiseProductSalePrintReport', require('./components/Reports/CustomersWiseProductSalePrintReport.vue').default);
Vue.component('supplierWiseProductPurchasePrintReport', require('./components/Reports/SupplierWiseProductPurchasePrintReport.vue').default);
Vue.component('suppliersWiseProductPurchasePrintReport', require('./components/Reports/SuppliersWiseProductPurchasePrintReport.vue').default);
Vue.component('customerDiscountProductsPrintReport', require('./components/Reports/CustomerDiscountProductsPrintReport.vue').default);

Vue.component('customerBalancePrintReport', require('./components/Reports/CustomerBalancePrintReport.vue').default);
Vue.component('supplierDiscountProductsPrintReport', require('./components/Reports/SupplierDiscountProductsPrintReport.vue').default);
Vue.component('productDiscountCustomersPrintReport', require('./components/Reports/ProductDiscountCustomersPrintReport.vue').default);
Vue.component('productDiscountSuppliersPrintReport', require('./components/Reports/ProductDiscountSuppliersPrintReport.vue').default);
Vue.component('freeofCostPurchasePrintReport', require('./components/Reports/FreeofCostPurchasePrintReport.vue').default);
Vue.component('freeofCostSalePrintReport', require('./components/Reports/FreeofCostSalePrintReport.vue').default);
Vue.component('ledgerPrintReport', require('./components/Reports/LedgerPrintReport.vue').default);
Vue.component('trial-balance-account-report-print', require('./components/Reports/TrialBalanceAccountReportPrint.vue').default);
Vue.component('printMultipleBarcodes', require('./components/Reports/PrintMultipleBarcodes.vue').default);
Vue.component('rack-barcode-print', require('./components/Reports/RacksBarcodePrint.vue').default);
Vue.component('trialBalanceReport', require('./components/Reports/TrialBalanceReport.vue').default);
Vue.component('balanceSheetReport', require('./components/Reports/BalanceSheetReport.vue').default);
Vue.component('incomeStatementReport', require('./components/Reports/IncomeStatementReport.vue').default);
Vue.component('customerLedgerReport', require('./components/Reports/CustomerLedgerReport.vue').default);
Vue.component('vatPayableReport', require('./components/Reports/VatPayableReport.vue').default);
Vue.component('saleInvoiceReport', require('./components/Reports/SaleInvoiceReport.vue').default);
Vue.component('VatReportPrint', require('./components/Reports/VatReportPrint.vue').default);
Vue.component('productledgerreportprint', require('./components/Reports/ProductLedgerReportPrint.vue').default);
Vue.component('productledgerreportdownload', require('./components/Reports/ProductLedgerReportDownload.vue').default);

//Dashboard
Vue.component('dashboard', require('./components/Dashboard/Dashboard.vue').default);
Vue.component('invoicedashboard', require('./components/Dashboard/InvocieDashboard.vue').default);
Vue.component('forgotPassword', require('./views/ForgotPassword.vue').default);
Vue.component('CompanyInfoImage', require('./components/CompanyInfoImage.vue').default);
Vue.component('AddProductImage', require('./components/AddProductImage.vue').default);
Vue.component('productimage', require('./components/productImageList.vue').default);

Vue.component('PurchaseBillItem', require('./components/PurchaseBill/PurchaseBillItem.vue').default);

//Ware House Transfer Items
Vue.component('warehousetransferitem', require('./components/WareHouseTransfer/WareHouseTransferItem.vue').default);


//Image
Vue.component('uploadImage', require('./components/UploadImage.vue').default);
//Purchase Order Dropdown
Vue.component('purchase-order-dropdown', require('./components/General/PurchaseOrderDropdown.vue').default);

//AuthorizeUserModel
Vue.component('authorize-user-model', require('./components/Sale/AuthorizeUserModel.vue').default);

Vue.component('purchase-order-dropdown', require('./components/General/PurchaseOrderDropdown.vue').default);
Vue.component('product-single-dropdown', require('./components/General/ProductSingleDropdown.vue').default);
Vue.component('invoice-view-item', require('./components/Sale/InvoiceViewItem.vue').default);
Vue.component('sale-email-item', require('./components/Sale/SaleEmailItem.vue').default);


Vue.component('bankPosTerminalmodal', require('./components/BankPosTerminal/AddBankPosTerminal.vue').default);
Vue.component('citymodal', require('./components/City/AddCity.vue').default);
Vue.component('regionmodal', require('./components/Region/AddRegion.vue').default);
Vue.component('countrydropdown', require('./components/General/CountryDropdown.vue').default);
Vue.component('countryfor-employee-dropdown', require('./components/General/CountryForEmployeeDropdown.vue').default);
Vue.component('city-for-employee-dropdown', require('./components/General/CityForEmployeeDropdown.vue').default);
Vue.component('provincedropdown', require('./components/General/ProvinceDropdown.vue').default);
Vue.component('bankPosTerminalDropdown', require('./components/General/BankPosTerminalDropdown.vue').default);
Vue.component('citydropdown', require('./components/General/CityDropdown.vue').default);
//Area Dropdowb
Vue.component('areadropdown', require('./components/General/AreaDropdown.vue').default);
Vue.component('bankdropdown', require('./components/General/BankDropdown.vue').default);
//Sale Order Item
Vue.component('saleorder-view-item', require('./components/SaleOrder/SaleOrderViewItem.vue').default);
Vue.component('saleorder-item', require('./components/SaleOrder/SaleOrderItem.vue').default);
Vue.component('saleorder-email-item', require('./components/SaleOrder/SaleOrderEmailItem.vue').default);
Vue.component('quotation-item', require('./components/Quotation/QuotationItem.vue').default);
Vue.component('quotation-view-item', require('./components/Quotation/QuotationViewItem.vue').default);
Vue.component('quotation-email-item', require('./components/Quotation/QuotationEmailItem.vue').default);

//Recipe Item
Vue.component('recipe-item', require('./components/RecipeNo/RecipeItem.vue').default);
Vue.component('view-recipe-item', require('./components/RecipeNo/ViewRecipeItem.vue').default);
//ProductionBatch Item
Vue.component('productionbatch-item', require('./components/ProductionBatch/ProductionBatchItem.vue').default);
Vue.component('batch-view-item', require('./components/ProductionBatch/BatchViewItem.vue').default);
Vue.component('saleorderdropdown', require('./components/General/SaleOrderDropdown.vue').default);
Vue.component('quotationdropdown', require('./components/General/QuotationDropdown.vue').default);
Vue.component('reciptdropdown', require('./components/General/ReciptNoDropDown.vue').default);
Vue.component('roledropdown', require('./components/General/RolesDropdown.vue').default);
Vue.component('actionmodal', require('./components/DailyExpense/ActionModel.vue').default);


//Dispatch Note
Vue.component('dispatch-item', require('./components/DispatchNote/DispatchItem.vue').default);
Vue.component('dispatch-dropdown', require('./components/General/DispatchNoteDropdown.vue').default);
Vue.component('Backup', require('./components/DatabaseBackup/Backup.vue').default);
Vue.component('Restore', require('./components/Restore/Restore.vue').default);
Vue.component('Upload', require('./components/Upload/Upload.vue').default);
Vue.component('rejectionmodel', require('./components/DailyExpense/RejectionModel.vue').default);

// PrintSetting Modal
Vue.component('printSettingmodal', require('./components/PrintSetting/AddPrintSettings.vue').default);
Vue.component('Userprofileimage', require('./components/UserProfileImage.vue').default);
Vue.component('purchaseReturnHistorymodel', require('./components/PurchaseReturn/PurchaseReturnHistory.vue').default);

//Inventory Blind Print
Vue.component('blindPrint', require('./components/Reports/InventoryBlindPrint.vue').default);
Vue.component('add-company-process', require('./components/CompanyProcess/AddCompanyProcess.vue').default);
Vue.component('add-company-action', require('./components/CompanyAction/AddCompanyAction.vue').default);
Vue.component('import-attachment', require('./components/CompanyAction/ImportAttachment.vue').default);
Vue.component('bulk-attachment', require('./components/Attachment/ImportAttachment.vue').default);
Vue.component('attachment-view', require('./components/Attachment/AttachmentView.vue').default);
Vue.component('purchase-order-payment-view', require('./components/PurchaseOrder/PurchaseOrderPaymentView.vue').default);
Vue.component('add-company-option', require('./components/CompanyOptions/AddCompanyOption.vue').default);
Vue.component('day-dropdown', require('./components/General/DayDropdown.vue').default);
Vue.component('sample-request-dropdown', require('./components/General/SampleRequestDropDown.vue').default);

Vue.component('purchaseorder-expensebill', require('./components/PurchaseOrder/PurchaseOrderExpenseBill.vue').default);
Vue.component('customerLedgerPrintReport', require('./components/Reports/CustomerLedgerPrintReport.vue').default);

//Hr
Vue.component('add-deduction', require('./components/Hr/Payroll/Deduction/AddDeduction.vue').default);
Vue.component('add-shift', require('./components/Hr/Shift/AddShift.vue').default);
Vue.component('shift-dropdown', require('./components/General/ShiftDropdown.vue').default);
Vue.component('add-contribution', require('./components/Hr/Payroll/Contribution/AddContribution.vue').default);


//PayRoll
Vue.component('add-payroll-schedule', require('./components/Hr/Payroll/PayrollSchedule/AddPayrollSchedule.vue').default);
Vue.component('addAllowance', require('./components/Hr/Payroll/Allowance/AddAllowance.vue').default);
Vue.component('AddAllowanceType', require('./components/Hr/Payroll/AllowanceType/AddAllowanceType.vue').default);
Vue.component('allowanceTypeDropdown', require('./components/General/AllowanceTypeDropdown.vue').default);
Vue.component('allowanceDropdown', require('./components/General/AllowanceDropdown.vue').default);
Vue.component('allowanceDropdown', require('./components/General/AllowanceDropdown.vue').default);
Vue.component('deductionDropdown', require('./components/General/DeductionDropdown.vue').default);
Vue.component('addDeduction', require('./components/Hr/Payroll/Deduction/AddDeduction.vue').default);
Vue.component('contributionDropdown', require('./components/General/ContributionDropdown.vue').default);
Vue.component('salary-template-dropdown', require('./components/General/SalaryTemplateDropdown.vue').default);
Vue.component('payroll-schedule-dropdown', require('./components/General/PayrollScheduleDropdown.vue').default);

Vue.component('sample-item', require('./components/ProductionModule/SampleRequest/SampleItem.vue').default);
Vue.component('process-item', require('./components/ProductionModule/Process/ProcessItem.vue').default);

Vue.component('contractor-item', require('./components/ProductionBatch/ContractorItem.vue').default);
Vue.component('gate-pass-report', require('./components/Reports/GatePassReport.vue').default);

Vue.component('departmentModel', require('./components/Hr/Employee/Department/AddDepartment.vue').default);
Vue.component('designationModel', require('./components/Hr/Employee/Designation/AddDesignation.vue').default);
Vue.component('designationDropdown', require('./components/General/DesignationDropdown.vue').default);
Vue.component('designation-multi-dropdown', require('./components/General/DesignationMultiDropdown.vue').default);
Vue.component('department-multi-dropdown', require('./components/General/DepartmentMultiDropdown.vue').default);

Vue.component('departmentDropdown', require('./components/General/DepartmentDropdown.vue').default);
Vue.component('loanDetailModel', require('./components/Hr/Payroll/LoanPayment/LoanDetailModel.vue').default);
Vue.component('DeliveryChallanModel', require('./components/DeliveryChallan/AddDeliveryChallanModel.vue').default);

Vue.component('loanRecovery', require('./components/Hr/Payroll/LoanPayment/LoanRecoveryModel.vue').default);
Vue.component('salaryTemplateModel', require('./components/Hr/Payroll/RunPayroll/SalaryTemplateModel.vue').default);

Vue.component('run-payroll-model', require('./components/Hr/Payroll/RunPayroll/RunPayrollView.vue').default);
Vue.component('run-payroll-report-print', require('./components/Reports/PayRunReportPrint.vue').default);

Vue.component('monthlysalesReportPrint', require('./components/Reports/MonthlySalesReportPrint.vue').default);

/*Service Quotation*/
Vue.component('quotation-service-item', require('./components/QuotationService/ServiceQuotationItem.vue').default);
Vue.component('quotation-service-view-item', require('./components/QuotationService/ServiceQuotationViewItem.vue').default);

Vue.component('payslip-print', require('./components/Reports/PaySlipPrint.vue').default);

Vue.component('add-warranty', require('./components/warrantyType/AddWarranty.vue').default);

Vue.component('auto-purchase-template-item', require('./components/AutoPurchaseTemplate/AutoPurchaseTemplateItem.vue').default);
Vue.component('auto-purchase-template-dropdown', require('./components/General/AutoPurchaseOrderDropdown.vue').default);

//Temporary Cash Request
Vue.component('temporary-cash-request-item', require('./components/TemporaryCashRequest/TemporaryCashRequestItem.vue').default);
Vue.component('temporary-cash-request-print', require('./components/Reports/TemporaryCashRequestPrint.vue').default);
Vue.component('temporary-cash-issue-print', require('./components/Reports/TemporaryCashIssuePrint.vue').default);


//Temporary Cash Issue
Vue.component('temporary-cash-issue-item', require('./components/TemporaryCashIssue/TemporaryCashIssueItem.vue').default);
Vue.component('temporary-cash-request-dropdown', require('./components/General/TemporaryCashRequestDropdown.vue').default);
Vue.component('temporary-cash-issue-Return', require('./components/TemporaryCashIssue/TemporaryCashIssueReturn.vue').default);
Vue.component('temporary-cash-issue-history', require('./components/TemporaryCashIssue/TemporaryCashIssueHistory.vue').default);
Vue.component('temporary-cash-issue-detail-Print', require('./components/Reports/TemporaryCashIssueDetailPrint.vue').default);
Vue.component('temporary-cash-allocation-Print', require('./components/Reports/TemporaryCashAllocationPrint.vue').default);


Vue.component('chequelistmodel', require('./components/Bank/ChequeListModel.vue').default);
Vue.component('BankMultiDropdown', require('./components/General/BankMultiDropdown.vue').default);
Vue.component('chequemodel', require('./components/Bank/ChequeModel.vue').default);
Vue.component('blockmodel', require('./components/Bank/BlockModel.vue').default);
Vue.component('chequeandguranteemodel', require('./components/Bank/ChequeAndGuranteeModel.vue').default);
Vue.component('issuedtodropdown', require('./components/General/IssuedToDropdown.vue').default);

Vue.component('usc-report-print', require('./components/Reports/USCReportPrint.vue').default);

Vue.component('monthlycostitem', require('./components/MonthlyCost/MonthlyCostItem.vue').default);
Vue.component('trialSubAccount', require('./components/Reports/TrialSubAccountModel.vue').default);

Vue.component('SalesDashboardReport', require('./components/Reports/SalesDashboardReport.vue').default);
Vue.component('trial-balance-tree-report-print', require('./components/Reports/TrialBalanceTreeReportPrint.vue').default);
Vue.component('importExportmodel', require('./components/ImportExport/AddImportExport.vue').default);
Vue.component('reparingOrdermodel', require('./components/ReparingOrderType/AddReparingOrderType.vue').default);




Vue.component('importexportdropdown', require('./components/General/ImportExportDropdown.vue').default);
Vue.component('reparingordertypeDropdown', require('./components/General/ReparingOrderTypeDropdown.vue').default);
Vue.component('sale-invoice-pdf-download', require('./components/Reports/SaleInvoicePdfDownload.vue').default);
Vue.component('sale-order-tracking-report-download', require('./components/Reports/SaleOrderTrackingReportDownload.vue').default);
Vue.component('sale-order-tracking-report-print', require('./components/Reports/SaleOrderTrackingReportPrint.vue').default);

Vue.component('SaleOrderPdf', require('./components/Reports/SaleOrderPdf.vue').default);
Vue.component('QuotationPdf', require('./components/Reports/QuotationPdf.vue').default);
Vue.component('SaleReturnPdf', require('./components/Reports/SaleReturnPdf.vue').default);
Vue.component('CustomerLedgerPdf', require('./components/Reports/CustomerLedgerDownloadReport.vue').default);
Vue.component('CustomerBalancePdf', require('./components/Reports/CustomerBalanceDownloadReport.vue').default);
Vue.component('LedgerDownloadReport', require('./components/Reports/LedgerDownloadReport.vue').default);
Vue.component('VatDownloadReport', require('./components/Reports/VatReportDownloadPrint.vue').default);
Vue.component('MonthlyPdf', require('./components/Reports/MonthlySalesPdfPrint.vue').default);
Vue.component('BankTransactionPdf', require('./components/Reports/BankTransactionPdfPrint.vue').default);
Vue.component('trial-balance-tree-report-print', require('./components/Reports/TrialBalanceTreeReportPrint.vue').default);
Vue.component('AccountDashboard', require('./components/Dashboard/AccountDashboard.vue').default);
Vue.component('HRDashboard', require('./components/Dashboard/HRDashboard.vue').default);
Vue.component('InventoryDashboard', require('./components/Dashboard/InventoryDashboard.vue').default);
Vue.component('CashAndBankDashboard', require('./components/Dashboard/CashAndBankDashboard.vue').default);
Vue.component('CashAndBankDashboard', require('./components/Dashboard/CashAndBankDashboard.vue').default);

//AddInquiry Process
Vue.component('add-inquiry-process-model', require('./components/InquiryProcess/AddInquiryProcess.vue').default);
Vue.component('inquiry-process-dropdown', require('./components/General/InquiryProcessDropdown.vue').default);

//Add Inquiry Module
Vue.component('add-inquiry-module-model', require('./components/InquiryModule/AddInquiryModule.vue').default);
Vue.component('inquiry-module-dropdown', require('./components/General/InquiryModuleDropdown.vue').default);
Vue.component('GoodReceiveDropdown', require('./components/General/GoodReceiveDropdown.vue').default);

//Add Inquiry Update Status

Vue.component('inquiry-status-update', require('./components/Inquiry/UpdateStatus.vue').default);
//Add Inquiry Type
Vue.component('add-inquiry-type-model', require('./components/InquiryType/AddInquiryType.vue').default);
Vue.component('inquiry-type-dropdown', require('./components/General/InquiryTypeDropdown.vue').default);
Vue.component('sale-invoice-default', require('./components/Reports/SaleInvoiceDefault.vue').default);
Vue.component('sale-invoice-service-default-Download', require('./components/Reports/SaleInvoiceServicePdfDownload.vue').default);

Vue.component('sale-order-service', require('./components/Reports/SaleOrderService.vue').default);
Vue.component('sale-order-service-pdf', require('./components/Reports/SaleOrderServicePdf.vue').default);
Vue.component('quotation-service-pdf', require('./components/Reports/QuotationServicePdf.vue').default);
Vue.component('GoodReceiveItem', require('./components/GRN/GoodReceiveItem.vue').default);
Vue.component('GoodReceiveViewItem', require('./components/GRN/GoodReceiveViewItem.vue').default);
Vue.component('ReparingOrderItem', require('./components/ReparingOrder/ReparingOrderItem.vue').default);
Vue.component('LedgerAccountWisePrintReport', require('./components/Reports/LedgerAccountWisePrintReport.vue').default);
Vue.component('LedgerAccountWiseDownloadReport', require('./components/Reports/LedgerAccountWiseDownloadReport.vue').default);
Vue.component('VatExpenseReportPrint', require('./components/Reports/VatExpenseReportPrint.vue').default);
Vue.component('VatExpenseReportPrintDownload', require('./components/Reports/VatExpenseReportPrintDownload.vue').default);
Vue.component('ProductFilterDownloadReport', require('./components/Reports/ProductFilterDownloadReport.vue').default);
Vue.component('AttendenceFilterReport', require('./components/Reports/AttendenceFilterReportPrint.vue').default);

Vue.component('NoorAbdullahInvoiveTemplate', require('./components/Reports/NoorAbdullahInvoiveTemplate.vue').default);


Vue.component('PaksitanClientInvoiceTemplate', require('./components/Reports/PaksitanClientInvoiceTemplate.vue').default);

Vue.component('PaksitanClientInvoicePdf', require('./components/Reports/PaksitanClientInvoiceTemplatePdf.vue').default);
Vue.component('ReparingOrderPaymentPrint', require('./components/Reports/ReparingOrderPaymentPrint.vue').default);
Vue.component('PrintSettingImages', require('./components/PrintSettingImages.vue').default);
Vue.component('SmartDigitalInvoiceDownload', require('./components/SaleServiceInvoice/SmartDigitalInvoiceDownload.vue').default);


Vue.component('JournalVoucherPrint', require('./components/Reports/JournalVoucherPrint.vue').default);
Vue.component('TodayAttendenceModel', require('./components/ManualAttendance/AddManualAttendence.vue').default);
Vue.component('QuotationItemTemplate', require('./components/Quotation/QuotationItemTemplate.vue').default);
Vue.component('DeliveryChallanItem', require('./components/DeliveryChallan/DeliveryChallanItem.vue').default);
Vue.component('QuotationTemplateDropdown', require('./components/General/QuotationTemplateDropdown.vue').default);
Vue.component('ProductLedgerPrintReport', require('./components/Reports/ProductLedgerPrintReport.vue').default);
Vue.component('ProductLedgerPrintDownload', require('./components/Reports/ProductLedgerPrintDownload.vue').default);
Vue.component('ComparisionLedgerPrintReport', require('./components/Reports/ComparisionLedgerPrintReport.vue').default);
Vue.component('ComparisionLedgerPrintDownload', require('./components/Reports/ComparisionLedgerPrintDownload.vue').default);
Vue.component('AddCustomerModel', require('./components/ReparingOrder/AddCustomer.vue').default);


// DeliveryChallan
Vue.component('deliverychallana4print', require('./components/Reports/DeliveryChallanA4Print.vue').default);


// DeliveryChallan
Vue.component('creditnoteitem', require('./components/CreditNote/CreditNoteItem.vue').default);


// ProductGroup
Vue.component('addproductgroup', require('./components/ProductGroup/AddProductGroup.vue').default);
Vue.component('productgroupdropdown', require('./components/General/ProductGroupDropdown.vue').default);



Vue.prototype.$https = axios;
axios.defaults.baseURL = json.ServerIP;
Vue.prototype.$ServerIp = json.ServerIP;
Vue.prototype.$PermissionIp = json.PermissionIP;
Vue.prototype.$ClientIP = json.ClientIP;
Vue.prototype.$ReportServer = json.ReportServer;
Vue.prototype.$macAddess = 'D8-FC-93-2E-5B-94';
Vue.prototype.$SystemType = json.SystemType;

router.beforeEach((to, from, next) => { // <------------
    var getLocale = localStorage.getItem('locales');
    let language = getLocale;
    if (!language) {
        language = 'en'
    }
    i18n.locale = language
    next()
});


Vue.filter('englishLanguage',
    function (value) {

        var getEnglishLanguage = localStorage.getItem('English');
        var getArabicLanguage = localStorage.getItem('Arabic');
        var getPortuguesLanguage = localStorage.getItem('Portugues');
        if (getEnglishLanguage == 'true' && getArabicLanguage == 'false' && getPortuguesLanguage == 'false') {
            return value;
        }
        else if (getEnglishLanguage == 'false' && getArabicLanguage == 'true' && getPortuguesLanguage == 'false') {
            return value;
        }
        else if (getEnglishLanguage == 'false' && getArabicLanguage == 'false' && getPortuguesLanguage == 'true') {
            return value;
        }
       
        else {
            var lan = localStorage.getItem('locales');

            if (lan == 'en') {
                return value + ' (English)';
            }
            else if (lan == 'pt') {
                return value + ' (Inglês)';
            }
            else {
                return value + ' (انجليزي)';
            }

        }

    });

Vue.filter('arabicLanguage',
    function (value) {
        var getEnglishLanguage = localStorage.getItem('English');
        var getArabicLanguage = localStorage.getItem('Arabic');
        var getPortuguesLanguage = localStorage.getItem('Portugues');
        if (getEnglishLanguage == 'true' && getArabicLanguage == 'false' && getPortuguesLanguage == 'false') {
            return value;
        }
        else if (getEnglishLanguage == 'false' && getArabicLanguage == 'true' && getPortuguesLanguage == 'false') {
            return value;
        }
        else if (getEnglishLanguage == 'false' && getArabicLanguage == 'false' && getPortuguesLanguage == 'true') {
            return value;
        }
        else {
            var lan = localStorage.getItem('locales');
            if (lan == 'en' && getArabicLanguage == 'false' && getPortuguesLanguage == 'true') {
                return value + ' (Portugues)';
            }
            else if (lan == 'en' && getArabicLanguage == 'true' && getPortuguesLanguage == 'false') {
                return value + ' (Arabic)';
            }
            else if (lan == 'pt') {
                return value + ' (Portugues)';
            }
            else {
                return value + ' (عربي)';
            }

        }
    });

Vue.filter('formatAmount', function (value) {
    if (!value) return '';

    return parseFloat(value).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
})

Vue.filter('roundAmount', function (value) {
    if (!value) return 0;

    return parseFloat(value).toFixed(3).slice(0, -1);
})

new Vue({
    router,
    store,
    i18n,
    render: h => h(App)
}).$mount('#app')
