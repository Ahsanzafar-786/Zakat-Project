import Vue from 'vue'
import VueRouter from 'vue-router'
//import i18n from '../i18n'
import Login from '../views/Login.vue'
import Dashboard from '../views/Dashboard.vue'
import NewPassword from '../views/NewPassword.vue'
import ResetPassword from '../views/ResetPassword.vue'
import Navbar from '../views/Navbar.vue'
import sync from '../views/Sync.vue'
import CommingSoon from '../views/CommingSoon.vue'
//import App from '../App.vue'
import RegisterUser from '../components/RegisterUser.vue'
import SetupUser from '../components/RegisterUser.vue'
import UserProfileInfo from '../components/UserProfileInfo.vue'
import AddCompany from '../components/Company/AddCompany.vue'
import Company from '../components/Company/Company.vue'
import ClientManagement from '../components/Company/ClientManagement.vue'
import AddBusiness from '../components/Business/AddBusiness.vue'
import Business from '../components/Business/Business.vue'
import Location from '../components/Location/Location.vue'
import AddLocation from '../components/Location/AddLocation.vue'
import Admin from '../components/Dashboard/Admin.vue'
import SuperAdmin from '../components/Dashboard/SuperAdmin.vue'
/*import SuperduperAdmin from '../components/Dashboard/SuperduperAdmin.vue'*/
import CompanyAdditionalInfo from '../components/Company/CompanyAdditionalInfo.vue'
import CompanyAdditionalList from '../components/Company/CompanyAdditionalList.vue'
import CompanyAttachments from '../components/Company/CompanyAttachments.vue'
import aboutUs from '../components/Company/AboutUs.vue'

import AddSupplier from '../components/Supplier/AddSupplier.vue'
import Supplier from '../components/Supplier/Supplier.vue'
import SupplierDetail from '../components/Supplier/SupplierDetail.vue'
import AddCustomer from '../components/Customer/AddCustomer.vue'
import Customer from '../components/Customer/Customer.vue'
import AddCustomer2 from '../components/Customer2/AddCustomer.vue'
import Customer2 from '../components/Customer2/Customer.vue'
import CustomerDetail from '../components/Customer/CustomerDetail.vue'

import AddProduct from '../components/Product/AddProduct.vue'
import Product from '../components/Product/Product.vue'
import Category from '../components/Category/Category.vue'
import SubCategory from '../components/SubCategory/SubCategory.vue'
import AddWarehouse from '../components/Warehouse/AddWarehouse.vue'
import Warehouse from '../components/Warehouse/Warehouse.vue'
import Brand from '../components/Brand/Brand.vue'
import Unit from '../components/Unit/Unit.vue'
import Color from '../components/Color/Color.vue'
import Size from '../components/Size/Size.vue'
import Origin from '../components/Origin/Origin.vue'
import TaxRate from '../components/TaxRate/TaxRate.vue'
import CompanyProcess from '../components/CompanyProcess/CompanyProcessList.vue'

import About from '../views/About.vue'
import HolidaySetup from '../components/Hr/ShiftManagement/HolidaySetup.vue';

import coa from '../components/COA/AddUpdateAccount.vue'
import Setupcoa from '../components/COA/AddUpdateAccount.vue'
import VueSession from 'vue-session'
//PurchaseOrder
import AddInvoice from '../components/Invoice/AddInvoice.vue'
import AddPurchase from '../components/Purchase/AddPurchase.vue'
import PurchaseView from '../components/Purchase/PurchaseView.vue'
import Terminal from '../components/Terminal/Terminal.vue'
import AddPromotion from '../components/Promotion_Discount/AddPromotion.vue'
import Promotion from '../components/Promotion_Discount/Promotion.vue'
import AddBundles from '../components/Bundle_Category/AddBundles.vue'
import Bundles from '../components/Bundle_Category/Bundles.vue'
import AddCurrency from '../components/Currency/AddCurrency.vue'
import Currency from '../components/Currency/Currency.vue'
import AddPaymentOptions from '../components/Payment_Options/AddPaymentOptions.vue'
import PaymentOptions from '../components/Payment_Options/PaymentOptions.vue'

import Purchase from '../components/Purchase/Purchase.vue'
import AddPurchaseOrder from '../components/PurchaseOrder/AddPurchaseOrder.vue'
import PurchaseOrder from '../components/PurchaseOrder/PurchaseOrder.vue'
import PurchaseOrderView from '../components/PurchaseOrder/PurchaseOrderView.vue'
import EmployeeRegistration from '../components/EmployeeRegistration/EmployeeRegistration.vue'
import EmployeeView from '../components/EmployeeRegistration/EmployeeView.vue'
import AddEmployeeRegistration from '../components/EmployeeRegistration/AddEmployeeRegistration.vue'
import SignUp from '../components/LoginCredentials/SignUp.vue'
import AddSignUp from '../components/LoginCredentials/AddSignUp.vue'

import AddLoginPermission from '../components/LoginPermission/AddLoginPermission.vue'

import MonthlyCost from '../components/MonthlyCost/MonthlyCost.vue'

//Sale
import AddSale from '../components/Sale/AddSale.vue'
import InvoiceView from '../components/Sale/InvoiceView.vue'
import InvoiceViewFroPaksitaniClient from '../components/Sale/InvoiceViewFroPaksitaniClient.vue'
import Sale from '../components/Sale/Sale.vue'
import SalePaymentDetail from '../components/Sale/SalePaymentDetail.vue'
import ProductDetailDashboard from '../components/Dashboard/ProductDetailDashBoard.vue'
import SaleEmail from '../components/Sale/SaleEmail.vue'


//Sale Service
import AddSaleService from '../components/SaleServiceInvoice/AddSaleService.vue'
import SaleService from '../components/SaleServiceInvoice/SaleService.vue'
import InvoiceServiceView from '../components/SaleServiceInvoice/InvoiceServiceView.vue'
import SaleServiceView from '../components/SaleServiceInvoice/SaleServiceView.vue'

//DayStart
import DayStart from '../components/DayStart/DayStart.vue'
import DayStart2 from '../components/DayStart/DayStart2.vue'
import WholeSaleDay from '../components/WholeSaleDay/WholeSaleDay.vue'
import PurchaseReturn from '../components/PurchaseReturn/PurchaseReturn.vue'
import AddPurchaseReturn from '../components/PurchaseReturn/AddPurchaseReturn.vue'
import PurchaseReturnView from '../components/PurchaseReturn/PurchaseReturnView.vue'


//JournalVoucher
import AddJournalVoucher from '../components/JournalVouchers/AddJournalVoucher.vue'
import JournalVoucherView from '../components/JournalVouchers/JournalVoucherView.vue'

//PaymentVoucher
import AddPaymentVoucher from '../components/PaymentVouchers/AddPaymentVoucher.vue'
import PaymentVoucherList from '../components/PaymentVouchers/PaymentVoucherList.vue'
//JournalVoucher
import AddDailyExpense from '../components/DailyExpense/AddDailyExpense.vue'
import DailyExpense from '../components/DailyExpense/DailyExpense.vue'

//Stock Value
import AddStockValue from '../components/Product_Stock_Value/AddStockValue.vue'
import StockValue from '../components/Product_Stock_Value/StockValue.vue'
import ViewStock from '../components/Product_Stock_Value/ViewStock.vue'

//Reporting
//import AllReports from '../components/Reports/AllReports.vue'
import DateWiseTransaction from '../components/Reports/DaywisetransactionReport.vue'
import PurchaseInvoice from '../components/Reports/PurchaseInvoice.vue'
import SaleInvoice from '../components/Reports/SaleInvoice.vue'
import PaymentVoucherInvoice from '../components/Reports/PaymentVoucherInvoice.vue'
import InventoryFilterReport from '../components/Reports/InventoryFilterReport.vue'
import ProductInventoryReport from '../components/Reports/ProductInventoryReport.vue'
import ProductFilterDashBoard from '../components/Reports/ProductFilterDashBoard.vue'
import ProductStockValue from '../components/Reports/ProductStockValue.vue'
import ProductStockAvgValue from '../components/Reports/ProductStockAvgValue.vue'
import ProductStockValueTransactionType from '../components/Reports/ProductStockValueTransactionType.vue'
import CustomerWiseProductSaleReport from '../components/Reports/CustomerWiseProductSaleReport.vue'
import CustomersWiseProductSaleReport from '../components/Reports/CustomersWiseProductSaleReport.vue'
import SupplierWiseProductPurchaseReport from '../components/Reports/SupplierWiseProductPurchaseReport.vue'
import SuppliersWiseProductPurchaseReport from '../components/Reports/SuppliersWiseProductPurchaseReport.vue'
import SupplierDiscountProducts from '../components/Reports/SupplierDiscountProducts.vue'
import CustomerDiscountProducts from '../components/Reports/CustomerDiscountProducts.vue'
import ProductDiscountCustomers from '../components/Reports/ProductDiscountCustomers.vue'
import ProductDiscountSuppliers from '../components/Reports/ProductDiscountSuppliers.vue'
import FreeofCostPurchase from '../components/Reports/FreeofCostPurchase.vue'
import FreeofCostSale from '../components/Reports/FreeofCostSale.vue'
import AccountLedger from '../components/Reports/AccountLedger.vue'
import AccountLedgerCostCenterWise from '../components/Reports/AccountLedgerCostCenterWise.vue'
import BankTransactionReport from '../components/Reports/BankTransactionReport.vue'
import CustomerBalanceReport from '../components/Reports/CustomerBalanceReport.vue'
import StockReport from '../components/Reports/StockReport.vue'
import InvoiceReport from '../components/Reports/InvoiceReport.vue'
import MonthlySalesReport from '../components/Reports/MonthlySalesReport.vue'
import MonthlySalesComparisionReport from '../components/Reports/MonthlySalesComparisionReport.vue'
import TrialBalanceReport from '../components/Reports/TrialBalanceReport.vue'
import TrialBalanceAccountReport from '../components/Reports/TrialBalanceAccountReport.vue'
import TrialBalanceTreeReport from '../components/Reports/TrialBalanceTreeReport.vue'
import RetailReport from '../components/Reports/RetailReport.vue'
import BalanceSheetReport from '../components/Reports/BalanceSheetReport.vue'
import IncomeStatementReport from '../components/Reports/IncomeStatementReport.vue'
import CustomerLedgerReport from '../components/Reports/CustomerLedgerReport.vue'
import VatPayableReport from '../components/Reports/VatPayableReport.vue'
import SaleInvoiceReport from '../components/Reports/SaleInvoiceReport.vue'
import SupplierPurchaseReport from '../components/Reports/SupplierPurchaseReport.vue'
import ProductComparisonReport from '../components/Reports/ProductComparisonReport.vue'
import ProductLedgerReport from '../components/Reports/ProductLedgerReport.vue'
import InvoiceSerialReport from '../components/Reports/InvoiceSerialReport.vue'

// MobileOrder
import MobileOrder from '../components/MobileOrder/MobileOrder.vue'
import AddMobileOrder from '../components/MobileOrder/AddMobileOrder.vue'
import DailyExpenseView from '../components/DailyExpense/DailyExpenseView.vue'

//Application Update
import ApplicationUpdate from '../components/ApplicationUpdate/UpdateApplication.vue'

//Bank
import PriceLabeling from '../components/PriceLabeling/PriceLabeling.vue'

//ProductGroup
import ProductGroup from '../components/ProductGroup/ProductGroup.vue'


import Bank from '../components/Bank/Bank.vue'
import AddBank from '../components/Bank/AddBank.vue'
import PriceRecord from '../components/PriceRecord/PriceRecord.vue'


//COMPANY ACCOUNTS SETUP
import AddAccountSetup from '../components/CompanyAccountSetup/AddAccountSetup.vue'
import SetupAccount from '../components/CompanyAccountSetup/AddAccountSetup.vue'
import Dashboard1 from '../components/Dashboard/Dashboard.vue'


//Sale Return
import ServiceSaleReturn from '../components/SaleServiceReturn/ServiceSaleReturn.vue'
import AddServiceSaleReturn from '../components/SaleServiceReturn/AddServiceSaleReturn.vue'

import SaleReturn from '../components/SaleReturn/SaleReturn.vue'
import AddSaleReturn from '../components/SaleReturn/AddSaleReturn.vue'
import ViewSaleReturn from '../components/SaleReturn/ViewSaleReturn.vue'
import MedicalReport from '../components/Reports/MedicalReport.vue'

//Sale Return
import MultiBarcodePrinting from '../components/BarcodePrinting/MultiBarcodePrinting.vue'

Vue.use(VueSession);
Vue.use(VueRouter);

//Ware House Transfer
import WareHouseTransfer from '../components/WareHouseTransfer/WareHouseTransfer.vue'
import AddWareHouseTransfer from '../components/WareHouseTransfer/AddWareHouseTransfer.vue'

//NobleRoles
import AddRoles from '../components/UserRoles/AddRoles.vue'
import Roles from '../components/UserRoles/Roles.vue'

//Permissions
import AddPermissions from '../components/UserPermissions/AddPermissions.vue'
import Permissions from '../components/UserPermissions/Permissions.vue'
import NoblePermissions from '../components/UserPermissions/NoblePermission.vue'


//Authenticator
import Authenticator from '../components/AuthenticationCode.vue'
//Touch Invoice
import InvoiceBarCodeItem from '../components/TouchScreen/InvoiceBarCodeItem.vue'
import InvoiceBarcode from '../components/TouchScreen/TouchScreenForBarcodeUser.vue'

//Import Categorey
import ImportCategory from '../components/Imports/ImportCategory.vue'
//Import Product
import ImportProduct from '../components/Imports/ImportProduct.vue'
import ImportExportRecords from '../components/Imports/ImportExportRecords.vue'

//Import Stock
import ImportStock from '../components/Imports/ImportStockIn.vue'
//Import Contact
import ImportContact from '../components/Imports/ImportContact.vue'
import ImportReparingOrder from '../components/Imports/ImportReparingOrder.vue'

// Terms And Conditions
import TermsAndConditions from '../components/TermsAndConditions/TermAndCondition.vue'
import Setup from '../components/TermsAndConditions/Setup.vue'


//Term Dashboard
import TermDashboard from '../components/Dashboard/TermsDashboard.vue'

// Hr Dashboard
import HrDashboard from '../components/HrDashboard/HrDashboard.vue'

//Product Management
//import ProductManagement from '../components/SidebarMenu/ProductManagement.vue'
import UserSetup from '../components/SidebarMenu/UserSetup.vue'
//import HrSetup from '../components/SidebarMenu/HrSetup.vue'

//Support
import Support from '../views/Support.vue'


//Countries
import Region from '../components/Region/Region.vue'

//City
import City from '../components/City/City.vue'

//Sale Order
import AddSaleOrder from '../components/SaleOrder/AddSaleOrder.vue'
import SaleOrder from '../components/SaleOrder/SaleOrder.vue'
import SaleOrderView from '../components/SaleOrder/SaleOrderView.vue'
import SaleOrderEmail from '../components/SaleOrder/SaleOrderEmail.vue'

// Sale Order Tracking Report
import SaleOrderTrackingReport from '../components/SaleOrderTrackingReport/SaleOrderTrackingReport.vue'

//Quotation
import QuotationView from '../components/Quotation/QuotationView.vue'
import AddQuotation from '../components/Quotation/AddQuotation.vue'
import Quotation from '../components/Quotation/Quotation.vue'
import QuotationEmail from '../components/Quotation/QuotationEmail.vue'

//PurchaseBill
import AddPurchaseBill from '../components/PurchaseBill/AddPurchaseBill.vue'
import PurchaseBillView from '../components/PurchaseBill/PurchaseBillView.vue'
import PurchaseBill from '../components/PurchaseBill/PurchaseBill.vue'
//Recipe No
import AddRecipeNo from '../components/RecipeNo/AddRecipeNo.vue'
import RecipeNo from '../components/RecipeNo/RecipeNo.vue'
import ViewRecipe from '../components/RecipeNo/ViewRecipe.vue'

//Production Batch
import AddProductionBatch from '../components/ProductionBatch/AddProductionBatch.vue'
import ProcessBatchScreen from '../components/ProductionBatch/ProcessBatchScreen.vue'
import ProductionBatch from '../components/ProductionBatch/ProductionBatch.vue'
import BatchView from '../components/ProductionBatch/BatchView.vue'
//Dispatch  Notes
import AddDispatchNote from '../components/DispatchNote/AddDispatchNote.vue'
import DispatchNotes from '../components/DispatchNote/DispatchNotes.vue'


import StartScreen from '../components/UserScreen/StartScreen.vue'
import Welcome from '../components/UserScreen/Welcome.vue'
import NotPermission from '../components/UserScreen/NotPermission.vue'
import GeographicalSetup from '../components/SidebarMenu/GeographicalSetup.vue'
import ImportExportSetup from '../components/SidebarMenu/ImportExportSetup.vue'
import ImportExport from '../components/ImportExport/ImportExport.vue'
import ReparingOrderType from '../components/ReparingOrderType/ReparingOrderType.vue'

import BarcodeSetup from '../components/SidebarMenu/BarcodeSetup.vue'
import Synchronization from '../components/SidebarMenu/Synchronization.vue'
import InquirySetup from '../components/SidebarMenu/InquirySetup.vue'
import BackupAndRestore from '../components/SidebarMenu/Backup.vue'

//Print Setting
import PrintSetting from '../components/PrintSetting/PrintSetting.vue'
import Backup from '../components/DatabaseBackup/Backup.vue'
import Restore from '../components/Restore/Restore.vue'
import Upload from '../components/Upload/Upload.vue'
import PushRecords from '../components/PushRecords/PushRecords.vue'
import PullRecords from '../components/PullRecords/PullRecords.vue'
import FlushDatabase from '../components/FlushDatabase/FlushDatabase.vue'

//Company Profile
import CompanyProfile from '../components/CompanyProfile/CompanyProfile.vue'
import CompanyInfo from '../components/CompanyProfile/CompanyInfo.vue'

//Touch Screen
import TouchScreen from '../components/TouchScreen/TouchScreen.vue'

//Denomination Setup
import DenominationSetup from '../components/DenominationSetup/DenominationSetup.vue'
// Production Batch View
import ProductionBatchView from '../components/ProductionBatch/ProductionBatchView.vue'
// Product View
import ProductView from '../components/Product/ProductView.vue'

// COA Template
import CoaTemplate from '../components/COA/CoaTemplate.vue'

//Purchase Return History
import PurchaseReturnHistory from '../components/PurchaseReturn/PurchaseReturnHistory.vue'

//Ware House wise inventory
import InventoryBlind from '../components/InventoryBlind/AddInventoryBlind.vue'
import InventoryBlindList from '../components/InventoryBlind/InventoryBlind.vue'



import PaymentVoucherView from '../components/PaymentVouchers/PaymentVoucherView.vue'
import RackBarcodeCreate from '../components/RacksBarcode/RackBarcodeCreate.vue'

import BankPosTerminal from '../components/BankPosTerminal/BankPosTerminal.vue'

import ProductMaster from '../components/ProductMaster/ProductMaster.vue'

import AddLogistics from '../components/Logistics/AddLogistics.vue'

import LogisticsList from '../components/Logistics/LogisticsList.vue'

import ItemRules from '../components/Rules/ItemRules.vue'
import PurchaseCosting from '../components/PurchaseCosting/AddPurchaseCosting.vue'
import FinancialYear from '../components/FinancialYear/FinancialYear.vue'
import FinancialYearSetup from '../components/FinancialYear/FinancialYearSetup.vue'
import CompanyOption from '../components/CompanyOptions/CompanyOption.vue'

import DayStartReport from '../components/Reports/DayStartWiseReport.vue'
import AddAutoPurchaseSetting from '../components/AutoPurchaseSetting/AddAutoPurchaseSetting.vue'

import AutoPurchaseTemplate from '../components/AutoPurchaseTemplate/AutoPurchaseTemplate.vue'
import AddAutoPurchaseTemplate from '../components/AutoPurchaseTemplate/AddAutoPurchaseTemplate.vue'

/*Sale Service Order*/
import AddSaleServiceOrder from '../components/SaleServiceOrder/AddSaleServiceOrder.vue'
import SaleServiceOrder from '../components/SaleServiceOrder/SaleServiceOrder.vue'
import SaleServiceOrderView from '../components/SaleServiceOrder/SaleServiceOrderView.vue'
//Designation
import Department from '../components/Hr/Employee/Department/Department.vue'
import Designation from '../components/Hr/Employee/Designation/Designation.vue'

//Service Quotation
import AddServiceQuotation from '../components/QuotationService/AddServiceQuotation.vue'
import ServiceQuotation from '../components/QuotationService/ServiceQuotation.vue'
import DeliveryChallan from '../components/DeliveryChallan/DeliveryChallan.vue'
import ServiceQuotationView from '../components/QuotationService/ServiceQuotationView.vue'


//Hr
import Leave from '../components/Hr/LeaveManagement/Leave.vue'
import Shift from '../components/Hr/Shift/Shift.vue'
import Deduction from '../components/Hr/Payroll/Deduction/Deduction.vue'
import Contribution from '../components/Hr/Payroll/Contribution/Contribution.vue'
import Allowance from '../components/Hr/Payroll/Allowance/Allowance.vue'
import AllowanceType from '../components/Hr/Payroll/AllowanceType/AllowanceType.vue'
import SalaryTemplate from '../components/Hr/Payroll/SalaryTemplate/SalaryTemplate.vue'
import AddSalaryTemplate from '../components/Hr/Payroll/SalaryTemplate/AddSalaryTemplate.vue'

import AddEmployeeSalary from '../components/Hr/Payroll/EmployeeSalary/AddEmployeeSalary.vue'
import EmployeeSalary from '../components/Hr/Payroll/EmployeeSalary/EmployeeSalary.vue'
import PayrollSchedule from '../components/Hr/Payroll/PayrollSchedule/PayrollSchedule.vue'
import SalaryTaxSlab from '../components/Hr/Payroll/SalaryTaxSlab/SalaryTaxSlab.vue'
import AddSalaryTaxSlab from '../components/Hr/Payroll/SalaryTaxSlab/AddSalaryTaxSlab.vue'

import AddRunPayroll from '../components/Hr/Payroll/RunPayroll/AddRunPayroll.vue'
import RunPayroll from '../components/Hr/Payroll/RunPayroll/RunPayroll.vue'

import ChequeAndGurantee from '../components/Bank/ChequeAndGurantee.vue'
import ChequeAndGuranteeDashboard from '../components/Bank/ChequeAndGuranteeDashboard.vue'


//Loan Payment
import LoanPayment from '../components/Hr/Payroll/LoanPayment/LoanPayment.vue'
import AddLoanPayment from '../components/Hr/Payroll/LoanPayment/AddLoanPayment.vue'

//import WarrantyType from '../components/warrantyType/WarrantyType.vue'

//Temporary Cash Request
import TemporaryCashRequest from '../components/TemporaryCashRequest/TemporaryCashRequest.vue'
import AddTemporaryCashRequest from '../components/TemporaryCashRequest/AddTemporaryCashRequest.vue'

//Temporary Cash Allocation
import TemporaryCashAllocation from '../components/TemporaryCashAllocation/TemporaryCashAllocation.vue'
import AddTemporaryCashAllocation from '../components/TemporaryCashAllocation/AddTemporaryCashAllocation.vue'
//import WarrantyType from '../components/warrantyType/WarrantyType.vue'
import SalesDashboardReport from '../components/Reports/SalesDashboardReport.vue'

//Temporary Cash Issue
import TemporaryCashIssue from '../components/TemporaryCashIssue/TemporaryCashIssue.vue'
import AddTemporaryCashIssue from '../components/TemporaryCashIssue/AddTemporaryCashIssue.vue'
import WarrantyType from '../components/warrantyType/WarrantyType.vue'
import Inquiry from '../components/Inquiry/Inquiry.vue'
import AddInquiry from '../components/Inquiry/AddInquiry.vue'
import InquiryProcess from '../components/InquiryProcess/InquiryProcess.vue'
import InquiryModule from '../components/InquiryModule/InquiryModule.vue'
import InquiryType from '../components/InquiryType/InquiryType.vue'
import SampleRequest from '../components/ProductionModule/SampleRequest/SampleRequest.vue'
import AddSampleRequest from '../components/ProductionModule/SampleRequest/AddSampleRequest.vue'
import Process from '../components/ProductionModule/Process/Process.vue'
import AddProcess from '../components/ProductionModule/Process/AddProcess.vue'



//Add Proforma Invoice
import AddProformaInvoice from '../components/ProformaInvoice/AddProformaInvoice.vue'
import AddServiceProformaInvoice from '../components/ProformaInvoice/AddServiceProformaInvoice.vue'
import ProformaInvoices from '../components/ProformaInvoice/ProformaInvoices.vue'
import ServiceProformaInvoice from '../components/ProformaInvoice/ServiceProformaInvoice.vue'

//Temporary Cash Allocation
import TemporaryCashAllocationReport from '../components/TemporaryCashAllocation/TemporaryCashAllocationReport.vue'

//Sale Report
import SaleVerificationReport from '../components/SaleReport/SaleVerificationReport.vue'

//Credit Note
import CreditNote from '../components/CreditNote/CreditNote.vue'
import AddCreditNote from '../components/CreditNote/AddCreditNote.vue'

//Shift Assign
import ShiftAssign from '../components/Hr/ShiftAssign/ShiftAssign.vue'
import AddShiftAssign from '../components/Hr/ShiftAssign/AddShiftAssign.vue'

import UserDefineFlow from '../components/UserDefineFlow/UserDefineFlow.vue'

import AddGoodReceive from '../components/GRN/AddGoodReceive.vue'
import GoodReceiveView from '../components/GRN/GoodReceiveView.vue'
import GoodReceive from '../components/GRN/GoodReceive.vue'
import VatExpenseReport from '../components/Reports/VatExpenseReport.vue'

import ExpenseType from '../components/ExpenseType/ExpenseType.vue'

import ReparingOrder from '../components/ReparingOrder/ReparingOrder.vue'
import AddReparingOrder from '../components/ReparingOrder/AddReparingOrder.vue'


import ManualAttendance from '../components/ManualAttendance/ManualAttendance.vue'
import AddHolidayOfMonth from '../components/ManualAttendance/AddHolidayOfMonth.vue'
import AttendanceReport from '../components/ManualAttendance/AttendanceReport.vue'
import AttendanceReport2 from '../components/ManualAttendance/AttendanceReport2.vue'
import EmployeeHourOfAttendence from '../components/ManualAttendance/EmployeeHourOfAttendence.vue'
import EmployeeTodayAttendence from '../components/ManualAttendance/EmployeeTodayAttendence.vue'
import AddQuotationTemplate from '../components/Quotation/AddQuotationTemplate.vue'
import QuotationTemplate from '../components/Quotation/QuotationTemplate.vue'
import ServiceQuotationTemplate from '../components/QuotationService/ServiceQuotationTemplate.vue'
import AddDeliveryChallan from '../components/DeliveryChallan/AddDeliveryChallan.vue'
import DefaultSetting from '../components/DefaultSetting/DefaultSetting.vue'
import Prefixes from '../components/Prefixes/Prefixes.vue'
import SmsSetup from '../components/SmsSetup/SmsSetup.vue'
import ComparisionLedgerReport from '../components/Reports/ComparisionLedgerReport.vue'

import MultiReparingOrder from '../components/MuiltiReparingOrder/MultiReparingOrder.vue'
import AddMultiReparingOrder from '../components/MuiltiReparingOrder/AddMultiReparingOrder.vue'

import clickMixin from '@/Mixins/clickMixin'
Vue.use(clickMixin);

Vue.use(VueSession);
Vue.use(VueRouter);

const routes = [
    {
        path: '/',

        component:
        {
            render(c) { return c('router-view') }
        },
        children:
            [
                {
                    path: '/SaleEmail',
                    name: 'SaleEmail',
                    component: SaleEmail
                },
                {
                    path: '/SaleOrderEmail',
                    name: 'SaleOrderEmail',
                    component: SaleOrderEmail
                },
                {
                    path: '/QuotationEmail',
                    name: 'QuotationEmail',
                    component: QuotationEmail
                },
                {


                    path: '/TermDashboard',
                    name: 'TermDashboard',
                    component: TermDashboard,
                    children: [
                        {
                            path: '/TermsAndConditions',
                            name: 'TermsAndConditions',
                            component: TermsAndConditions

                        },
                        {
                            path: '/Setup',
                            name: 'Setup',
                            component: Setup
                        },
                        {
                            path: '/Setupcoa',
                            name: 'Setupcoa',
                            component: Setupcoa
                        },
                        {
                            path: '/CoaTemplate',
                            name: 'CoaTemplate',
                            component: CoaTemplate
                        },
                        {
                            path: '/SetupAccount',
                            name: 'SetupAccount',
                            component: SetupAccount
                        },
                        {
                            path: '/Support',
                            name: 'Support',
                            component: Support
                        },
                        {
                            path: '/SetupUser',
                            name: 'SetupUser',
                            component: SetupUser
                        },
                        {
                            path: '/CompanyInfo',
                            name: 'CompanyInfo',
                            component: CompanyInfo
                        },
                        {
                            path: '/FinancialYearSetup',
                            name: 'FinancialYearSetup',
                            component: FinancialYearSetup
                        }
                    ]
                },

                {
                    path: '/NewPassword',
                    name: 'NewPassword',
                    component: NewPassword
                },
                {
                    path: '/Authenticator',
                    name: 'Authenticator',
                    component: Authenticator
                },
                {
                    path: '/ResetPassword',
                    name: 'ResetPassword',
                    component: ResetPassword
                },
                {
                    path: '/',
                    name: 'Login',
                    component: Login
                },
                {
                    path: '/TouchScreen',
                    name: 'TouchScreen',
                    component: TouchScreen
                },
                {
                    path: '/InvoiceBarcode',
                    name: 'InvoiceBarcode',
                    component: InvoiceBarcode
                },
                {
                    path: '/InvoiceBarCodeItem',
                    name: 'InvoiceBarCodeItem',
                    component: InvoiceBarCodeItem
                },
                {
                    path: '/Welcome',
                    name: 'Welcome',
                    component: Welcome
                },


                {
                    path: '/dashboard',
                    name: 'Dashboard',
                    component: Dashboard,
                    children: [
                        {
                            path:'/HrDashboard',
                            name:'HrDashboard',
                            component:HrDashboard,
                        },
                        {
                            path: '/SmsSetup',
                            name: 'SmsSetup',
                            component: SmsSetup
                        },
                        {
                            path: '/ComparisionLedgerReport',
                            name: 'ComparisionLedgerReport',
                            component: ComparisionLedgerReport
                        },
                        {
                            path: '/DefaultSetting',
                            name: 'DefaultSetting',
                            component: DefaultSetting
                        },
                        {
                            path: '/Prefixes',
                            name: 'Prefixes',
                            component: Prefixes
                        },
                        {
                            path: '/registeruser',
                            name: 'RegisterUser',
                            component: RegisterUser
                        },
                        {
                            path: '/about',
                            name: 'About',
                            component: About
                        },
                        {
                            path: '/navbar',
                            name: 'Navbar',
                            component: Navbar
                        },

                        {
                            path: '/Company',
                            name: 'Company',
                            component: Company
                        },
                        {
                            path: '/CompanyAttachments',
                            name: 'CompanyAttachments',
                            component: CompanyAttachments
                        },

                        {
                            path: '/admin',
                            name: 'Admin',
                            component: Admin
                        },
                        {
                            path: '/superadmin',
                            name: 'Superadmin',
                            component: SuperAdmin
                        },

                        {
                            path: '/addcompany',
                            name: 'AddCompany',
                            component: AddCompany
                        },


                        {
                            path: '/addbusiness',
                            name: 'AddBusiness',
                            component: AddBusiness
                        },


                        {
                            path: '/business',
                            name: 'Business',
                            component: Business
                        },
                        {
                            path: '/location',
                            name: 'Location',
                            component: Location
                        },
                        {
                            path: '/addLocation',
                            name: 'AddLocation',
                            component: AddLocation
                        },
                        {
                            path: '/companyAdditionalInfo',
                            name: 'CompanyAdditionalInfo',
                            component: CompanyAdditionalInfo
                        },
                        {
                            path: '/companyAdditionalList',
                            name: 'CompanyAdditionalList',
                            component: CompanyAdditionalList
                        },

                        {
                            path: '/supplier',
                            name: 'Supplier',
                            component: Supplier
                        }
                        ,
                        {
                            path: '/addsupplier',
                            name: 'AddSupplier',
                            component: AddSupplier
                        }
                        ,
                        {
                            path: '/customer',
                            name: 'Customer',
                            component: Customer
                        }
                        ,
                        {
                            path: '/AddReparingOrder',
                            name: 'AddReparingOrder',
                            component: AddReparingOrder
                        }
                        ,
                        {
                            path: '/ReparingOrder',
                            name: 'ReparingOrder',
                            component: ReparingOrder
                        }
                        ,
                        {
                            path: '/AddMultiReparingOrder',
                            name: 'AddMultiReparingOrder',
                            component: AddMultiReparingOrder
                        }
                        ,
                        {
                            path: '/MultiReparingOrder',
                            name: 'MultiReparingOrder',
                            component: MultiReparingOrder
                        }
                        ,
                        {
                            path: '/GoodReceive',
                            name: 'GoodReceive',
                            component: GoodReceive
                        }
                        ,
                        {
                            path: '/AddQuotationTemplate',
                            name: 'AddQuotationTemplate',
                            component: AddQuotationTemplate
                        }
                        ,
                        {
                            path: '/ServiceQuotationTemplate',
                            name: 'ServiceQuotationTemplate',
                            component: ServiceQuotationTemplate
                        }
                        ,
                        {
                            path: '/AddDeliveryChallan',
                            name: 'AddDeliveryChallan',
                            props(route) {
                                return { formName: route.query.formName };
                            },
                            component: AddDeliveryChallan
                        }
                        ,
                        {
                            path: '/QuotationTemplate',
                            name: 'QuotationTemplate',
                            component: QuotationTemplate
                        }
                        ,
                        {
                            path: '/AddGoodReceive',
                            name: 'AddGoodReceive',
                            component: AddGoodReceive
                        }
                        ,
                        {
                            path: '/GoodReceiveView',
                            name: 'GoodReceiveView',
                            component: GoodReceiveView
                        }
                        ,
                        {
                            path: '/addCustomer',
                            name: 'AddCustomer',
                            component: AddCustomer
                        }
                        ,
                        {
                            path: '/customer2',
                            name: 'Customer2',
                            component: Customer2
                        }
                        ,
                        {
                            path: '/addCustomer2',
                            name: 'AddCustomer2',
                            component: AddCustomer2
                        }
                        
                        ,
                        {
                            path: '/AddReparingOrder',
                            name: 'AddReparingOrder',
                            component: AddReparingOrder
                        }
                        ,
                        {
                            path: '/ReparingOrder',
                            name: 'ReparingOrder',
                            component: ReparingOrder
                        },

                        {
                            path: '/userprofile',
                            name: 'UserProfileInfo',
                            component: UserProfileInfo
                        },

                        {
                            path: '/products',
                            name: 'Product',
                            component: Product
                        }
                        ,
                        {
                            path: '/addproduct',
                            name: 'AddProduct',
                            component: AddProduct
                        },

                        {
                            path: '/terminal',
                            name: 'Terminal',
                            component: Terminal
                        },

                        {
                            path: '/addinvoice',
                            name: 'addinvoice',
                            component: AddInvoice
                        },
                        {
                            path: '/addpurchase',
                            name: 'Addpurchase',
                            component: AddPurchase
                        },
                        {
                            path: '/promotion',
                            name: 'Promotion',
                            component: Promotion
                        },
                        {
                            path: '/addPromotion',
                            name: 'AddPromotion',
                            component: AddPromotion
                        },
                        {
                            path: '/bundles',
                            name: 'Bundles',
                            component: Bundles
                        },
                        {
                            path: '/addBundles',
                            name: 'AddBundles',
                            component: AddBundles
                        },

                        {
                            path: '/addCurrency',
                            name: 'AddCurrency',
                            component: AddCurrency
                        },

                        {
                            path: '/addPaymentOptions',
                            name: 'AddPaymentOptions',
                            component: AddPaymentOptions
                        },
                        {
                            path: '/addpurchaseorder',
                            name: 'Addpurchaseorder',
                            component: AddPurchaseOrder
                        },
                        {
                            path: '/purchaseorder',
                            name: 'purchaseorder',
                            component: PurchaseOrder
                        },
                        {
                            path: '/EmployeeTodayAttendence',
                            name: 'EmployeeTodayAttendence',
                            component: EmployeeTodayAttendence
                        },
                        {
                            path: '/daystart',
                            name: 'daystart',
                            component: DayStart
                        },
                        {
                            path: '/WholeSaleDay',
                            name: 'WholeSaleDay',
                            component: WholeSaleDay
                        },
                        {
                            path: '/daystart2',
                            name: 'daystart2',
                            component: DayStart2
                        },
                        {
                            path: '/employeeRegistration',
                            name: 'EmployeeRegistration',
                            component: EmployeeRegistration
                        },
                        {
                            path: '/EmployeeView',
                            name: 'EmployeeView',
                            component: EmployeeView
                        },
                        {
                            path: '/addEmployeeRegistration',
                            name: 'AddEmployeeRegistration',
                            component: AddEmployeeRegistration
                        },
                        {
                            path: '/addSignUp',
                            name: 'AddSignUp',
                            component: AddSignUp
                        },

                        {
                            path: '/addPurchaseReturn',
                            name: 'AddPurchaseReturn',
                            component: AddPurchaseReturn
                        },
                        {
                            path: '/PurchaseReturn',
                            name: 'PurchaseReturn',
                            component: PurchaseReturn
                        },
                        {
                            path: '/ClientManagement',
                            name: 'clientManagement',
                            component: ClientManagement
                        },
                        {
                            path: '/addjournalvoucher',
                            name: 'AddJournalVoucher',
                            props(route) {
                                return { formName: route.query.formName };
                            },
                            component: AddJournalVoucher
                        },
                        {
                            path: '/journalvoucherview',
                            name: 'JournalVoucherView',
                            props(route) {
                                return { formName: route.query.formName };
                            },
                            component: JournalVoucherView
                        },
                        {
                            path: '/paymentVoucherList',
                            name: 'PaymentVoucherList',
                            props(route) {
                                return { formName: route.query.formName };
                            },
                            component: PaymentVoucherList
                        },
                        {
                            path: '/addPaymentVoucher',
                            name: 'addPaymentVoucher',
                            props(route) {
                                return { formName: route.query.formName };
                            },
                            component: AddPaymentVoucher
                        },
                        {

                            path: '/PaymentVoucherView',
                            name: 'PaymentVoucherView',
                            props(route) {
                                return { formName: route.query.formName };
                            },
                            component: PaymentVoucherView
                        },
                        {
                            path: '/stockValue',
                            name: 'StockValue',
                            props(route) {
                                return { formName: route.query.formName };
                            },
                            component: StockValue
                        },
                        {
                            path: '/addStockValue',
                            name: 'AddStockValue',
                            props(route) {
                                return { formName: route.query.formName };
                            },
                            component: AddStockValue
                        },
                        {
                            path: '/ViewStock',
                            name: 'ViewStock',
                            props(route) {
                                return { formName: route.query.formName };
                            },
                            component: ViewStock
                        },
                        {
                            path: '/dailyexpense',
                            name: 'DailyExpense',
                            props(route) {
                                return { formName: route.query.formName };
                            },
                            component: DailyExpense
                        },
                        {
                            path: '/adddailyexpense',
                            name: 'AddDailyExpense',
                            props(route) {
                                return { formName: route.query.formName };
                            },
                            component: AddDailyExpense
                        },
                        {
                            path: '/DailyExpenseView',
                            props(route) {
                                return { formName: route.query.formName };
                            },
                            name: 'DailyExpenseView',
                            component: DailyExpenseView
                        },
                        {
                            path: '/addwarehouse',
                            name: 'AddWarehouse',
                            component: AddWarehouse
                        },
                        {
                            path: '/warehouse',
                            name: 'Warehouse',
                            component: Warehouse
                        },
                        {
                            path: '/addsale',
                            name: 'AddSale',
                            component: AddSale,
                            props(route) {
                                return { formName: route.query.formName };
                            },
                        },
                        {
                            path: '/invoiceView',
                            name: 'InvoiceView',
                            component: InvoiceView
                        },
                        {
                            path: '/InvoiceViewFroPaksitaniClient',
                            name: 'InvoiceViewFroPaksitaniClient',
                            component: InvoiceViewFroPaksitaniClient
                        },
                        {
                            path: '/Sale',
                            name: 'Sales',
                            component: Sale,
                            props(route) {
                                return { formName: route.query.formName };
                            },
                        },
                        {
                            path: '/SalePaymentDetail',
                            name: 'SalePaymentDetail',
                            component: SalePaymentDetail
                        },
                        {
                            path: '/PurchaseInvoice',
                            name: 'PurchaseInvoice',
                            component: PurchaseInvoice
                        },
                        {
                            path: '/SaleInvoice',
                            name: 'SaleInvoice',
                            component: SaleInvoice,
                            props(route) {
                                return { formName: route.query.formName };
                            },
                        },
                        {
                            path: '/AddSaleServiceOrder',
                            name: 'AddSaleServiceOrder',
                            component: AddSaleServiceOrder
                        },
                        {
                            path: '/SaleServiceOrder',
                            name: 'SaleServiceOrder',
                            props(route) {
                                return { formName: route.query.formName };
                            },
                            component: SaleServiceOrder
                        },
                        {
                            path: '/SaleServiceOrderView',
                            name: 'SaleServiceOrderView',
                            component: SaleServiceOrderView
                        },
                        {
                            path: '/PaymentVoucherInvoice',
                            name: 'PaymentVoucherInvoice',
                            props(route) {
                                return { formName: route.query.formName };
                            },
                            component: PaymentVoucherInvoice
                        },
                        {
                            path: '/addAccountSetup',
                            name: 'AddAccountSetup',
                            component: AddAccountSetup
                        },
                        {
                            path: '/mobileorder',
                            name: 'MobileOrder',
                            component: MobileOrder
                        },

                        {
                            path: '/addmobileorder',
                            name: 'AddMobileOrder',
                            component: AddMobileOrder
                        },

                        {
                            path: '/AddBank',
                            name: 'AddBank',
                            component: AddBank
                        },

                        {
                            path: '/Dashboard1',
                            name: 'Dashboard1',
                            component: Dashboard1
                        },

                        {
                            path: '/ProductDetailDashboard',
                            name: 'ProductDetailDashboard',
                            component: ProductDetailDashboard
                        },
                        {
                            path: '/ProductFilterDashBoard',
                            name: 'ProductFilterDashBoard',
                            component: ProductFilterDashBoard
                        },

                        {
                            path: '/SaleReturn',
                            name: 'SaleReturn',
                            component: SaleReturn
                        },
                        {
                            path: '/AddSaleReturn',
                            name: 'AddSaleReturn',
                            component: AddSaleReturn
                        },

                        {
                            path: '/ServiceSaleReturn',
                            name: 'ServiceSaleReturn',
                            component: ServiceSaleReturn
                        },
                        {
                            path: '/AddServiceSaleReturn',
                            name: 'AddServiceSaleReturn',
                            component: AddServiceSaleReturn
                        },
                        {
                            path: '/ViewSaleReturn',
                            name: 'ViewSaleReturn',
                            component: ViewSaleReturn
                        },

                        {
                            path: '/WareHouseTransfer',
                            name: 'WareHouseTransfer',
                            component: WareHouseTransfer
                        },
                        {
                            path: '/AddWareHouseTransfer',
                            name: 'AddWareHouseTransfer',
                            component: AddWareHouseTransfer
                        },
                        {
                            path: '/AddRoles',
                            name: 'AddRoles',
                            component: AddRoles
                        },

                        {
                            path: '/AddPermissions',
                            name: 'AddPermissions',
                            component: AddPermissions
                        },
                        {
                            path: '/ImportCategory',
                            name: 'ImportCategory',
                            component: ImportCategory
                        },
                        {
                            path: '/ImportProduct',
                            name: 'ImportProduct',
                            component: ImportProduct
                        },
                        {
                            path: '/ImportExportRecords',
                            name: 'ImportExportRecords',
                            component: ImportExportRecords
                        },
                        {
                            path: '/ImportStock',
                            name: 'ImportStock',
                            component: ImportStock
                        },
                        {
                            path: '/AddSaleService',
                            name: 'AddSaleService',
                            component: AddSaleService,
                            props(route) {
                                return { formName: route.query.formName };
                            },
                        },
                        {
                            path: '/SaleService',
                            name: 'SaleService',
                            component: SaleService,
                            props(route) {
                                return { formName: route.query.formName };
                            },
                        },
                        {
                            path: '/InvoiceServiceView',
                            name: 'InvoiceServiceView',
                            component: InvoiceServiceView
                        },
                        {
                            path: '/SaleServiceView',
                            name: 'SaleServiceView',
                            component: SaleServiceView
                        },
                        {
                            path: '/AddServiceQuotation',
                            name: 'AddServiceQuotation',
                            component: AddServiceQuotation
                        },
                        {
                            path: '/ServiceQuotation',
                            name: 'ServiceQuotation',
                            props(route) {
                                return { formName: route.query.formName };
                            },
                            component: ServiceQuotation
                        },
                        {
                            path: '/DeliveryChallan',
                            name: 'DeliveryChallan',
                            component: DeliveryChallan
                        },
                        {
                            path: '/ServiceQuotationView',
                            name: 'ServiceQuotationView',
                            component: ServiceQuotationView
                        },
                        {
                            path: '/TemporaryCashRequest',
                            name: 'TemporaryCashRequest',
                            component: TemporaryCashRequest
                        },
                        {
                            path: '/AddTemporaryCashRequest',
                            name: 'AddTemporaryCashRequest',
                            component: AddTemporaryCashRequest
                        },
                        {
                            path: '/AddTemporaryCashAllocation',
                            name: 'AddTemporaryCashAllocation',
                            component: AddTemporaryCashAllocation
                        },
                        {
                            path: '/TemporaryCashAllocation',
                            name: 'TemporaryCashAllocation',
                            component: TemporaryCashAllocation
                        },
                        {
                            path: '/TemporaryCashIssue',
                            name: 'TemporaryCashIssue',
                            component: TemporaryCashIssue
                        },
                        {
                            path: '/AddTemporaryCashIssue',
                            name: 'AddTemporaryCashIssue',
                            component: AddTemporaryCashIssue
                        },
                        {
                            path: '/MonthlySalesComparisionReport',
                            name: 'MonthlySalesComparisionReport',
                            component: MonthlySalesComparisionReport
                        },
                        {
                            path: '/StockReport',
                            name: 'StockReport',
                            component: StockReport
                        },
                        {
                            path: '/InvoiceReport',
                            name: 'InvoiceReport',
                            props(route) {
                                return { formName: route.query.formName };
                            },
                            component: InvoiceReport
                        },
                        {
                            path: '/MonthlySalesReport',
                            name: 'MonthlySalesReport',
                            props(route) {
                                return { formName: route.query.formName };
                            },
                            component: MonthlySalesReport
                        },
                        {
                            path: '/InventoryFilterReport',
                            name: 'InventoryFilterReport',
                            component: InventoryFilterReport
                        },
                        {
                            path: '/ProductInventoryReport',
                            name: 'ProductInventoryReport',
                            component: ProductInventoryReport
                        },
                        {
                            path: '/TrialBalanceReport',
                            name: 'TrialBalanceReport',
                            component: TrialBalanceReport
                        },
                        {
                            path: '/TrialBalanceAccountReport',
                            name: 'TrialBalanceAccountReport',
                            component: TrialBalanceAccountReport
                        },
                        {
                            path: '/TrialBalanceTreeReport',
                            name: 'TrialBalanceTreeReport',
                            component: TrialBalanceTreeReport
                        },
                        {
                            path: '/BalanceSheetReport',
                            name: 'BalanceSheetReport',
                            component: BalanceSheetReport
                        },
                        {
                            path: '/IncomeStatementReport',
                            name: 'IncomeStatementReport',
                            component: IncomeStatementReport
                        },
                        {
                            path: '/CustomerLedgerReport',
                            name: 'CustomerLedgerReport',
                            props(route) {
                                return { formName: route.query.formName };
                            },
                            component: CustomerLedgerReport,


                        },
                        {
                            path: '/VatPayableReport',
                            name: 'VatPayableReport',
                            component: VatPayableReport
                        },
                        {
                            path: '/DateWiseTransaction',
                            name: 'DateWiseTransaction',
                            component: DateWiseTransaction
                        },

                        {
                            path: '/ProductStockValue',
                            name: 'ProductStockValue',
                            component: ProductStockValue
                        },
                        {
                            path: '/SupplierPurchaseReport',
                            name: 'SupplierPurchaseReport',
                            component: SupplierPurchaseReport
                        },
                        {
                            path: '/ProductComparisonReport',
                            name: 'ProductComparisonReport',
                            component: ProductComparisonReport
                        },
                        {
                            path: '/ProductLedgerReport',
                            name: 'ProductLedgerReport',
                            component: ProductLedgerReport
                        },                        
                        {
                            path: '/InvoiceSerialReport',
                            name: 'InvoiceSerialReport',
                            component: InvoiceSerialReport
                        },
                        {
                            path: '/ProductStockValueTransactionType',
                            name: 'ProductStockValueTransactionType',
                            component: ProductStockValueTransactionType
                        },
                        {
                            path: '/ProductStockAvgValue',
                            name: 'ProductStockAvgValue',
                            component: ProductStockAvgValue
                        },
                        {
                            path: '/CustomerWiseProductSaleReport',
                            name: 'CustomerWiseProductSaleReport',
                            component: CustomerWiseProductSaleReport
                        },
                        {
                            path: '/CustomersWiseProductSaleReport',
                            name: 'CustomersWiseProductSaleReport',
                            component: CustomersWiseProductSaleReport
                        },
                        {
                            path: '/SupplierWiseProductPurchaseReport',
                            name: 'SupplierWiseProductPurchaseReport',
                            component: SupplierWiseProductPurchaseReport
                        },
                        {
                            path: '/SuppliersWiseProductPurchaseReport',
                            name: 'SuppliersWiseProductPurchaseReport',
                            component: SuppliersWiseProductPurchaseReport
                        },
                        {
                            path: '/SupplierDiscountProducts',
                            name: 'SupplierDiscountProducts',
                            component: SupplierDiscountProducts
                        },
                        {
                            path: '/CustomerDiscountProducts',
                            name: 'CustomerDiscountProducts',
                            component: CustomerDiscountProducts
                        },
                        {
                            path: '/ProductDiscountCustomers',
                            name: 'ProductDiscountCustomers',
                            component: ProductDiscountCustomers
                        },
                        {
                            path: '/ProductDiscountSuppliers',
                            name: 'ProductDiscountSuppliers',
                            component: ProductDiscountSuppliers
                        },
                        {
                            path: '/FreeofCostPurchase',
                            name: 'FreeofCostPurchase',
                            component: FreeofCostPurchase
                        },
                        {
                            path: '/FreeofCostSale',
                            name: 'FreeofCostSale',
                            component: FreeofCostSale
                        },
                        {
                            path: '/AccountLedger',
                            name: 'AccountLedger',
                            component: AccountLedger
                        },
                        {
                            path: '/AccountLedgerCostCenterWise',
                            name: 'AccountLedgerCostCenterWise',
                            component: AccountLedgerCostCenterWise
                        },
                        {
                            path: '/BankTransactionReport',
                            name: 'BankTransactionReport',
                            component: BankTransactionReport
                        },
                        {
                            path: '/CustomerBalanceReport',
                            name: 'CustomerBalanceReport',
                            props(route) {
                                return { formName: route.query.formName };
                            },
                            component: CustomerBalanceReport
                        },
                        {
                            path: '/SaleInvoiceReport',
                            name: 'SaleInvoiceReport',
                            component: SaleInvoiceReport
                        },
                        {
                            path: '/DayStartReport',
                            name: 'DayStartReport',
                            component: DayStartReport
                        }
                        ,
                        {
                            path: '/VatExpenseReport',
                            name: 'VatExpenseReport',
                            component: VatExpenseReport
                        },
                        {
                            path: '/ExpenseType',
                            name: 'ExpenseType',
                            component: ExpenseType
                        },
                        {
                            path: '/TemporaryCashAllocationReport',
                            name: 'TemporaryCashAllocationReport',
                            component: TemporaryCashAllocationReport
                        }
                        ,
                        {
                            path: '/SalesDashboardReport',
                            name: 'SalesDashboardReport',
                            component: SalesDashboardReport
                        },
                        {
                            path: '/ProductMaster',
                            name: 'ProductMaster',
                            component: ProductMaster
                        },
                        {
                            path: '/category',
                            name: 'Category',
                            component: Category
                        },
                        {
                            path: '/PriceLabeling',
                            name: 'PriceLabeling',
                            component: PriceLabeling
                        },
                        {
                            path: '/PriceRecord',
                            name: 'PriceRecord',
                            component: PriceRecord
                        },
                        {
                            path: '/subcategory',
                            name: 'SubCategory',
                            component: SubCategory
                        },
                        {
                            path: '/brand',
                            name: 'Brand',
                            component: Brand
                        },
                        {
                            path: '/unit',
                            name: 'Unit',
                            component: Unit
                        },
                        {
                            path: '/color',
                            name: 'Color',
                            component: Color
                        },
                        {
                            path: '/HolidaySetup',
                            name: 'HolidaySetup',
                            component: HolidaySetup
                        },
                        {
                            path: '/size',
                            name: 'Size',
                            component: Size
                        },
                        {
                            path: '/origin',
                            name: 'Origin',
                            component: Origin
                        },
                        {
                            path: '/Backup',
                            name: 'Backup',
                            component: Backup
                        },
                        {
                            path: '/Restore',
                            name: 'Restore',
                            component: Restore
                        },
                        {
                            path: '/CompanyProfile',
                            name: 'CompanyProfile',
                            component: CompanyProfile
                        },
                        {
                            path: '/MultiBarcodePrinting',
                            name: 'MultiBarcodePrinting',
                            component: MultiBarcodePrinting
                        },
                        {
                            path: '/RackBarcodeCreate',
                            name: 'RackBarcodeCreate',
                            component: RackBarcodeCreate
                        },
                        {
                            path: '/purchase',
                            name: 'purchase',
                            component: Purchase
                        },
                        {
                            path: '/AddAutoPurchaseSetting',
                            name: 'AddAutoPurchaseSetting',
                            component: AddAutoPurchaseSetting
                        }
                        ,
                        {
                            path: '/WarrantyType',
                            name: 'WarrantyType',
                            component: WarrantyType
                        },
                        {
                            path: '/Roles',
                            name: 'Roles',
                            component: Roles
                        },
                        {
                            path: '/signUp',
                            name: 'SignUp',
                            component: SignUp
                        },
                        {
                            path: '/AddLoginPermission',
                            name: 'AddLoginPermission',
                            component: AddLoginPermission
                        },

                        {
                            path: '/UserSetup',
                            name: 'UserSetup',
                            component: UserSetup,
                            children: [





                            ],
                        },

                        {
                            path: '/UserDefineFlow',
                            name: 'UserDefineFlow',
                            component: UserDefineFlow
                        },
                        {
                            path: '/Permissions',
                            name: 'Permissions',
                            component: Permissions
                        },
                        {
                            path: '/NoblePermissions',
                            name: 'NoblePermissions',
                            component: NoblePermissions
                        },
                        {
                            path: '/currency',
                            name: 'Currency',
                            component: Currency
                        },
                        {
                            path: '/Bank',
                            name: 'Bank',
                            component: Bank
                        },

                        {
                            path: '/monthlycost',
                            name: 'MonthlyCost',
                            component: MonthlyCost
                        },
                        {
                            path: '/paymentOptions',
                            name: 'PaymentOptions',
                            component: PaymentOptions
                        },
                        {
                            path: '/DenominationSetup',
                            name: 'DenominationSetup',
                            component: DenominationSetup
                        },
                        {
                            path: '/taxrate',
                            name: 'TaxRate',
                            component: TaxRate
                        },

                        {
                            path: '/FinancialYear',
                            name: 'FinancialYear',
                            component: FinancialYear
                        },
                        {
                            path: '/Deduction',
                            name: 'Deduction',
                            component: Deduction
                        },
                        {
                            path: '/Shift',
                            name: 'Shift',
                            component: Shift
                        },
                        {
                            path: '/Leave',
                            name: 'Leave',
                            component: Leave
                        },
                        {
                            path: '/ShiftAssign',
                            name: 'ShiftAssign',
                            component: ShiftAssign
                        },
                        {
                            path: '/AddShiftAssign',
                            name: 'AddShiftAssign',
                            component: AddShiftAssign
                        },
                        {
                            path: '/Contribution',
                            name: 'Contribution',
                            component: Contribution
                        },
                        {
                            path: '/Allowance',
                            name: 'Allowance',
                            component: Allowance
                        },
                        {
                            path: '/SalaryTemplate',
                            name: 'SalaryTemplate',
                            component: SalaryTemplate
                        },
                        {
                            path: '/AddSalaryTemplate',
                            name: 'AddSalaryTemplate',
                            component: AddSalaryTemplate
                        },
                        {
                            path: '/AllowanceType',
                            name: 'AllowanceType',
                            component: AllowanceType
                        },
                        {
                            path: '/AddEmployeeSalary',
                            name: 'AddEmployeeSalary',
                            component: AddEmployeeSalary
                        },
                        {
                            path: '/EmployeeSalary',
                            name: 'EmployeeSalary',
                            component: EmployeeSalary
                        },
                        {
                            path: '/LoanPayment',
                            name: 'LoanPayment',
                            component: LoanPayment
                        },
                        {
                            path: '/PayrollSchedule',
                            name: 'PayrollSchedule',
                            component: PayrollSchedule
                        },
                        {
                            path: '/AddLoanPayment',
                            name: 'AddLoanPayment',
                            component: AddLoanPayment
                        },
                        {
                            path: '/SalaryTaxSlab',
                            name: 'SalaryTaxSlab',
                            component: SalaryTaxSlab
                        },
                        {
                            path: '/AddSalaryTaxSlab',
                            name: 'AddSalaryTaxSlab',
                            component: AddSalaryTaxSlab
                        },


                        {
                            path: '/ChequeAndGurantee',
                            name: 'ChequeAndGurantee',
                            component: ChequeAndGurantee
                        },
                        {
                            path: '/ChequeAndGuranteeDashboard',
                            name: 'ChequeAndGuranteeDashboard',
                            component: ChequeAndGuranteeDashboard
                        },
                        {
                            path: '/coa',
                            name: 'coa',
                            component: coa
                        },
                        {
                            path: '/RunPayroll',
                            name: 'RunPayroll',
                            component: RunPayroll
                        },
                        {
                            path: '/AddRunPayroll',
                            name: 'AddRunPayroll',
                            component: AddRunPayroll
                        },
                        {
                            path: '/SaleOrder',
                            name: 'SaleOrder',
                            props(route) {
                                return { formName: route.query.formName };
                            },
                            component: SaleOrder
                        },
                        {
                            path: '/SaleOrderTrackingReport',
                            name: 'SaleOrderTrackingReport',
                            component: SaleOrderTrackingReport
                        },
                        {
                            path: '/AddSaleOrder',
                            name: 'AddSaleOrder',
                            props(route) {
                                return { formName: route.query.formName };
                            },
                            component: AddSaleOrder
                        },
                        {
                            path: '/SaleOrderView',
                            name: 'SaleOrderView',
                            props(route) {
                                return { formName: route.query.formName };
                            },
                            component: SaleOrderView
                        },
                        {
                            path: '/SyncData',
                            name: 'SyncData',
                            component: sync
                        },
                        {
                            path: '/RecipeNo',
                            name: 'RecipeNo',
                            component: RecipeNo
                        },
                        {
                            path: '/AddPurchaseBill',
                            name: 'AddPurchaseBill',
                            component: AddPurchaseBill
                        }, {
                            path: '/PurchaseBillView',
                            name: 'PurchaseBillView',
                            component: PurchaseBillView
                        },
                        {
                            path: '/PurchaseBill',
                            name: 'PurchaseBill',
                            component: PurchaseBill
                        },

                        {
                            path: '/AddRecipeNo',
                            name: 'AddRecipeNo',
                            component: AddRecipeNo
                        },
                        {
                            path: '/ViewRecipe',
                            name: 'ViewRecipe',
                            component: ViewRecipe
                        },
                        {
                            path: '/AddProductionBatch',
                            name: 'AddProductionBatch',
                            component: AddProductionBatch
                        },
                        {
                            path: '/ProductionBatch',
                            name: 'ProductionBatch',
                            component: ProductionBatch
                        },
                        {
                            path: '/BatchView',
                            name: 'BatchView',
                            component: BatchView
                        },
                        {
                            path: '/AddDispatchNote',
                            name: 'AddDispatchNote',
                            component: AddDispatchNote
                        },
                        {
                            path: '/DispatchNotes',
                            name: 'DispatchNotes',
                            component: DispatchNotes
                        },
                        {
                            path: '/SampleRequest',
                            name: 'SampleRequest',
                            component: SampleRequest
                        },
                        {
                            path: '/AddSampleRequest',
                            name: 'AddSampleRequest',
                            component: AddSampleRequest
                        },
                        {
                            path: '/ProcessBatchScreen',
                            name: 'ProcessBatchScreen',
                            component: ProcessBatchScreen
                        },
                        {
                            path: '/Process',
                            name: 'Process',
                            component: Process
                        },
                        {
                            path: '/AddProcess',
                            name: 'AddProcess',
                            component: AddProcess
                        },
                        {
                            path: '/StartScreen',
                            name: 'StartScreen',
                            component: StartScreen
                        },
                        {
                            path: '/NotPermission',
                            name: 'NotPermission',
                            component: NotPermission
                        },
                        {
                            path: '/FlushDatabase',
                            name: 'FlushDatabase',
                            component: FlushDatabase
                        },
                        {
                            path: '/GeographicalSetup',
                            name: 'GeographicalSetup',
                            component: GeographicalSetup,
                            children: [
                                {
                                    path: '/City',
                                    name: 'City',
                                    component: City
                                },
                              
                            ]
                        },
                        {
                            path: '/Region',
                            name: 'Region',
                            component: Region
                        },
                        {
                            path: '/ImportExportSetup',
                            name: 'ImportExportSetup',
                            component: ImportExportSetup,
                            children: [
                                {
                                    path: '/ImportExport',
                                    props(route) {
                                        return { formName: route.query.formName }
                                    },
                                    name: 'ImportExport',
                                    component: ImportExport
                                }

                            ],
                        },
                        {
                            path: '/ReparingOrderType',
                            name: 'ReparingOrderType',
                            props(route) {

                                return { formName: route.query.formName };
                            },
                            component: ReparingOrderType,

                        },
                        {
                            path: '/BarcodeSetup',
                            name: 'BarcodeSetup',
                            component: BarcodeSetup,
                            children: [

                            ],
                        },
                        {
                            path: '/PushRecords',
                            name: 'PushRecords',
                            component: PushRecords
                        },
                        {
                            path: '/PullRecords',
                            name: 'PullRecords',
                            component: PullRecords
                        },

                        {
                            path: '/Upload',
                            name: 'Upload',
                            component: Upload
                        },

                        {
                            path: '/Synchronization',
                            name: 'Synchronization',
                            component: Synchronization,
                            children: [


                               
                            ],
                        },
                        {
                            path: '/InquiryProcess',
                            name: 'InquiryProcess',
                            component: InquiryProcess,
                            
                        },
                        {
                            path: '/InquiryModule',
                            name: 'InquiryModule',
                            component: InquiryModule,
                            
                        },
                        {
                            path: '/InquiryType',
                            name: 'InquiryType',
                            component: InquiryType,
                            
                        },
                        {
                            path: '/InquirySetup',
                            name: 'InquirySetup',
                            component: InquirySetup,
                            
                        },
                        
                        {
                            path: '/BackupAndRestore',
                            name: 'BackupAndRestore',
                            component: BackupAndRestore,
                            children: [



                            ],
                        },
                        {
                            path: '/PrintSetting',
                            name: 'PrintSetting',
                            component: PrintSetting
                        },

                        {
                            path: '/PurchaseView',
                            name: 'PurchaseView',
                            component: PurchaseView
                        },
                        {
                            path: '/PurchaseReturnView',
                            name: 'PurchaseReturnView',
                            component: PurchaseReturnView
                        },
                        {
                            path: '/PurchaseOrderView',
                            name: 'PurchaseOrderView',
                            component: PurchaseOrderView
                        },

                        {
                            path: '/ProductionBatchView',
                            name: 'ProductionBatchView',
                            component: ProductionBatchView
                        },
                        {
                            path: '/ProductView',
                            name: 'ProductView',
                            component: ProductView
                        },
                        {
                            path: '/PurchaseReturnHistory',
                            name: 'PurchaseReturnHistory',
                            component: PurchaseReturnHistory
                        },

                        {
                            path: '/InventoryBlind',
                            name: 'InventoryBlind',
                            component: InventoryBlind
                        },
                        {
                            path: '/InventoryBlindList',
                            name: 'InventoryBlindList',
                            component: InventoryBlindList
                        },
                        {
                            path: '/BankPosTerminal',
                            name: 'BankPosTerminal',
                            component: BankPosTerminal
                        },
                        {
                            path: '/RackBarcodeCreate',
                            name: 'RackBarcodeCreate',
                            component: RackBarcodeCreate
                        },




                        {
                            path: '/AddQuotation',
                            name: 'AddQuotation',
                            component: AddQuotation
                        },
                        {
                            path: '/Quotation',
                            name: 'Quotation',
                            component: Quotation
                        },
                        {
                            path: '/LogisticsList',
                            name: 'LogisticsList',
                            props(route) {
                                return { formName: route.query.formName };
                            },
                            component: LogisticsList
                        },
                        {
                            path: '/AddLogistics',
                            name: 'AddLogistics',
                            props(route) {
                                return { formName: route.query.formName };
                            },
                            component: AddLogistics
                        },
                        {
                            path: '/ItemRules',
                            name: 'ItemRules',
                            component: ItemRules

                        },
                        {
                            path: '/QuotationView',
                            name: 'QuotationView',
                            component: QuotationView
                        },
                        {
                            path: '/CompanyProcess',
                            name: 'CompanyProcess',
                            component: CompanyProcess
                        },

                        {
                            path: '/ImportContact',
                            name: 'ImportContact',
                            component: ImportContact

                        },
                        {
                            path: '/ImportReparingOrder',
                            name: 'ImportReparingOrder',
                            component: ImportReparingOrder

                        },
                        {
                            path: '/ApplicationUpdate',
                            name: 'ApplicationUpdate',
                            component: ApplicationUpdate
                        },


                        {
                            path: '/PurchaseCosting',
                            name: 'PurchaseCosting',
                            component: PurchaseCosting

                        },
                        {
                            path: '/FinancialYear',
                            name: 'FinancialYear',
                            component: FinancialYear
                        },
                        {
                            path: '/MedicalReport',
                            name: 'MedicalReport',
                            component: MedicalReport
                        },
                        {
                            path: '/RetailReport',
                            name: 'RetailReport',
                            component: RetailReport
                        },
                        {
                            path: '/CompanyOption',
                            name: 'CompanyOption',
                            component: CompanyOption
                        },
                        {
                            path: '/Department',
                            name: 'Department',
                            component: Department
                        },
                        {
                            path: '/Designation',
                            name: 'Designation',
                            component: Designation
                        },
                        {
                            path: '/aboutUs',
                            name: 'aboutUs',
                            component: aboutUs
                        },
                        {
                            path: '/AutoPurchaseTemplate',
                            name: 'AutoPurchaseTemplate',
                            component: AutoPurchaseTemplate
                        },
                        {
                            path: '/AddAutoPurchaseTemplate',
                            name: 'AddAutoPurchaseTemplate',
                            component: AddAutoPurchaseTemplate
                        },
                        {
                            path: '/AddInquiry',
                            name: 'AddInquiry',
                            component: AddInquiry
                        },
                        {
                            path: '/Inquiry',
                            name: 'Inquiry',
                            component: Inquiry
                        },
                        {
                            path: '/ManualAttendance',
                            name: 'ManualAttendance',
                            component: ManualAttendance
                        },
                        {
                            path: '/AddHolidayOfMonth',
                            name: 'AddHolidayOfMonth',
                            component: AddHolidayOfMonth
                        },
                        {
                            path: '/EmployeeHourOfAttendence',
                            name: 'EmployeeHourOfAttendence',
                            component: EmployeeHourOfAttendence
                        },
                        {
                            path: '/AttendanceReport',
                            name: 'AttendanceReport',
                            component: AttendanceReport
                        },
                        {
                            path: '/AttendanceReport2',
                            name: 'AttendanceReport2',
                            component: AttendanceReport2
                        },
                        {
                            path: '/AddProformaInvoice',
                            name: 'AddProformaInvoice',
                            component: AddProformaInvoice
                        },
                        {
                            path: '/AddServiceProformaInvoice',
                            name: 'AddServiceProformaInvoice',
                            component: AddServiceProformaInvoice
                        },
                        {
                            path: '/ProformaInvoices',
                            name: 'ProformaInvoices',
                            component: ProformaInvoices
                        },
                        {
                            path: '/ServiceProformaInvoice',
                            name: 'ServiceProformaInvoice',
                            props(route) {
                                return { formName: route.query.formName };
                            },
                            component: ServiceProformaInvoice
                        },
                        {
                            path: '/CustomerDetail',
                            name: 'CustomerDetail',
                            component: CustomerDetail
                        },
                        {
                            path: '/SupplierDetail',
                            name: 'SupplierDetail',
                            component: SupplierDetail
                        },
                        {
                            path: '/CommingSoon',
                            name: 'CommingSoon',
                            component: CommingSoon
                        },
                        {
                            path: '/SaleVerificationReport',
                            name: 'SaleVerificationReport',
                            component: SaleVerificationReport
                        },
                        {
                            path: '/ProductGroup',
                            name: 'ProductGroup',
                            component: ProductGroup
                        },
                        {
                            path: '/CreditNote',
                            name: 'CreditNote',
                            component: CreditNote,
                            props(route) {
                                return { formName: route.query.formName };
                            },
                        },
                        {
                            path: '/AddCreditNote',
                            name: 'AddCreditNote',
                            component: AddCreditNote,
                            props(route) {
                                return { formName: route.query.formName };
                            },
                        },
                    ]
                }
            ]
    }
];
const router = new VueRouter({
    mode: process.env.IS_ELECTRON ? 'hash' : 'history',
    base: process.env.BASE_URL,
    routes
});

export default router


router.beforeEach((to, from, next) => {
    
    // redirect to login page if not logged in and trying to access a restricted page

    let recaptchaScript = document.createElement('script')
    recaptchaScript.setAttribute('src', '/assets/js/app.js')
    document.head.appendChild(recaptchaScript);
  

    if (to.query.fromDashboard === 'true')
    {
        localStorage.setItem('token', localStorage.getItem(to.query.token_name))


    }
    //var token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoic2F1ZGtoYW5AYWwtdGFtbWFtLmNvbSIsInN1YiI6InNhdWRraGFuQGFsLXRhbW1hbS5jb20iLCJqdGkiOiJlN2FkMWQ0Ny0yN2M4LTQ2YTgtOGVlOC01ZDYxZWQ1YzhkNGYiLCJSb2xlIjoiVXNlciIsIkNvbXBhbnlJZCI6ImI5ODBjNjc5LTJlOWUtNDNlZS0wMjQ2LTA4ZDliYWRlYTA4NiIsIlVzZXJJZCI6IjEzZGVlYjI1LTllODItNGYxOS1hMTliLWIwZmQ1MzRiMmFkZCIsIkVtYWlsIjoic2F1ZGtoYW5AYWwtdGFtbWFtLmNvbSIsIk5vYmxlQ29tcGFueUlkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiQnVzaW5lc3NJZCI6IjUxMDNjZmQ1LWNkNmUtNGI4NC0wMjQ1LTA4ZDliYWRlYTA4NiIsIkNsaWVudFBhcmVudElkIjoiZmMwMjI2ODYtYjE1ZS00YWU3LTAyNDQtMDhkOWJhZGVhMDg2IiwiRW1wbG95ZWVJZCI6ImE1ZTM3NDMyLThlNDYtNDNjZC0yOGVhLTA4ZDliYWUwOTIyYyIsIkNvdW50ZXJJZCI6ImE2NzE3NmQxLTFhNzctNDRmNS0wNGNkLTA4ZDliYWUwOTBiZSIsIkRheUlkIjoiNGNmYWNiNzgtMzg5NC00ZmNkLWRiYjAtMDhkYTRmNzVhNjA3IiwiSXNQcm9jZWVkIjp0cnVlLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiVXNlciIsIkNhblZpZXdVc2VyUm9sZSIsIkNhbkFkZFVzZXJSb2xlIiwiQ2FuRWRpdFVzZXJSb2xlIiwiQ2FuVmlld1VzZXJQZXJtaXNzaW9uIiwiQ2FuQWRkVXNlclBlcm1pc3Npb24iLCJDYW5FZGl0VXNlclBlcm1pc3Npb24iLCJDYW5BZGRTaWduVXBVc2VyIiwiQ2FuRWRpdFNpZ25VcFVzZXIiLCJDYW5WaWV3U2lnblVwVXNlciIsIkFkZExpbWl0ZWRVc2VyIiwiQ2FuSW1wb3J0Q3VzdG9tZXIiLCJDYW5BZGRDdXN0b21lciIsIkNhblZpZXdDdXN0b21lciIsIkNhbkVkaXRDdXN0b21lciIsIjAiLCJDYW5WaWV3SG9sZEludm9pY2VzIiwiQ2FuVmlld1BhaWRJbnZvaWNlcyIsIkNhblZpZXdDcmVkaXRJbnZvaWNlcyIsIkNhbkR1cGxpY2F0ZUludm9pY2VzIiwiQ2FuQTRQcmludCIsIkNhblBvc1ByaW50IiwiQ2FuRGVsZXRlSG9sZEludm9pY2VzIiwiQ2FuRWRpdEhvbGRJbnZvaWNlcyIsIkNhblZpZXdJbnZvaWNlRGV0YWlsIiwiQ2FuR2VuZXJhdGVTYWxlUmVjb3JkUGRmIiwiQ2FuR2VuZXJhdGVTYWxlUmVjb3JkU2hlZXQiLCJDYXNoSW52b2ljZXMiLCJDYXNoSW52b2ljZXNGb3JXYWxrSW4iLCJDYXNoSW52b2ljZXNGb3JDdXN0b21lciIsIkNyZWRpdEludm9pY2VzIiwiQ2FuU2VsZWN0V2FyZWhvdXNlIiwiQ2FuU2VsZWN0VHJhbnNwb3J0ZXIiLCJDYW5TZWxlY3RBcmVhIiwiQ2FuU2VsZWN0RGlzcGF0Y2hOb3RlIiwiQ2FuVmlld0N1cnJlbnRRdWFudGl0eSIsIkNhbkhvbGRJbnZvaWNlcyIsIkNhbkFkZFF1aWNrSXRlbSIsIkNhblByaW50SW52b2ljZXMiLCJDYW5BZGRBZHZhbmNlVm91Y2hlciIsIkNhbkFkZFNhbGVSZXR1cm4iLCJDYW5WaWV3U2FsZVJldHVybiIsIkNhblByaW50SW52b2ljZSIsIkNhblZpZXdEZXRhaWxTYWxlUmV0dXJuIiwiQ2FuR2VuZXJhdGVTYWxlUmV0dXJuUGRmIiwiQ2FuR2VuZXJhdGVTYWxlUmV0dXJuU2hlZXQiLCJDYW5TZW5kU2FsZVJldHVybkVtYWlsIiwiQ2FuVmlld1NhbGVPcmRlciIsIkNhbkFkZFNhbGVPcmRlciIsIkNhbkNsb3NlU2FsZU9yZGVyIiwiQ2FuRHJhZnRTYWxlT3JkZXIiLCJDYW5EdXBsaWNhdGVTYWxlT3JkZXIiLCJDYW5QYXlBZHZhbmNlRnJvbVZpZXciLCJDYW5WaWV3RGV0YWlsU2FsZU9yZGVyIiwiQ2FuUHJpbnRTYWxlT3JkZXIiLCJDYW5FZGl0U2FsZU9yZGVyIiwiQ2FuR2VuZXJhdGVTYWxlT3JkZXJQZGYiLCJDYW5HZW5lcmF0ZVNhbGVPcmRlclNoZWV0IiwiQ2FuU2VuZFNhbGVPcmRlckVtYWlsIiwiQ2FuQ2hvb3NlQTRJbnZvaWNlVHlwZSIsIkRlZmF1bHRUZW1wbGF0ZSIsIlNBVGVtcGxhdGUxQTQiLCJTQVRlbXBsYXRlMkE0IiwiQ2FuVmlld0NQUiIsIkNhbkFkZENQUiIsIkNhblByaW50Q1BSIiwiQ2FuRHJhZnRDUFIiLCJDYW5FZGl0Q1BSIiwiQ2FuUmVqZWN0Q1BSIiwiQ2FuVmlld0RldGFpbENQUiIsIkNhbkFkZENQUkF0dGFjaG1lbnQiLCJDYXNoUHVyY2hhc2UiLCJDcmVkaXRQdXJjaGFzZSIsIkNhbkFkZFB1cmNoYXNlSW52b2ljZSIsIkNhblZpZXdQdXJjaGFzZURldGFpbCIsIkNhblZpZXdQdXJjaGFzZURyYWZ0IiwiQ2FuVmlld1B1cmNoYXNlUG9zdCIsIkNhblByaW50UHVyY2hhc2VJbnZvaWNlIiwiQ2FuRGVsZXRlUHVyY2hhc2VJbnZvaWNlIiwiQ2FuRWRpdFB1cmNoYXNlSW52b2ljZSIsIkNhblB1cmNoYXNlSW52b2ljZUNvc3RpbmciLCJDYW5TZW5kUElFbWFpbCIsIkNhbkdlbmVyYXRlUElQZGYiLCJDYW5HZW5lcmF0ZVBJU2hlZXQiLCJDYW5WaWV3UElBdHRhY2htZW50IiwiQ2FuRG93bmxvYWRQSUF0dGFjaG1lbnQiLCJDYW5WaWV3UElBY3Rpb25zIiwiQ2FuVmlld1BJUGF5bWVudHMiLCJDYW5WaWV3UElFeHBlbnNlcyIsIkNhbkRlbGV0ZVBJRXhwZW5zZXMiLCJDYW5WaWV3RHJhZnRPcmRlciIsIkNhblZpZXdQb3N0T3JkZXIiLCJDYW5WaWV3SW5Qcm9jZXNzT3JkZXIiLCJDYW5BZGRQdXJjaGFzZU9yZGVyIiwiQ2FuRWRpdFB1cmNoYXNlT3JkZXIiLCJDYW5WaWV3T3JkZXJEZXRhaWwiLCJDYW5VcGxvYWRBdHRhY2htZW50IiwiQ2FuRG93bmxvYWRBdHRhY2htZW50IiwiQ2FuQWRkQWR2YW5jZVBheW1lbnQiLCJDYW5WaWV3QWR2YW5jZVBheW1lbnQiLCJDYW5WaWV3RGV0YWlsQWR2YW5jZVBheW1lbnQiLCJDYW5BZGRPcmRlckFjdGlvbiIsIkNhblZpZXdPcmRlckFjdGlvbiIsIkNhblZpZXdPcmRlckV4cGVuc2UiLCJDYW5BZGRPcmRlckV4cGVuc2UiLCJDYW5WaWV3RGV0YWlsT3JkZXJFeHBlbnNlIiwiQ2FuQWxsb3dPcmRlclZlcnNpb24iLCJDYW5WaWV3UHJldmlvdXNWZXJzaW9uIiwiQ2FuQWR2YW5jZVBheW1lbnRGcm9tTGlzdCIsIkNhbkNsb3NlT3JkZXIiLCJDYW5WaWV3UHVyY2hhc2VSZXR1cm4iLCJDYW5WaWV3RGV0YWlsUHVyY2hhc2VSZXR1cm4iLCJDYW5BZGRQdXJjaGFzZVJldHVybiIsIkNhblZpZXdSZXR1cm5IaXN0b3J5IiwiQ2FuSW1wb3J0U3VwcGxpZXIiLCJDYW5BZGRTdXBwbGllciIsIkNhblZpZXdTdXBwbGllciIsIkNhbkVkaXRTdXBwbGllciIsIjAiLCJDYW5WaWV3U1BSIiwiQ2FuQWRkU1BSIiwiQ2FuUHJpbnRTUFIiLCJDYW5EcmFmdFNQUiIsIkNhbkVkaXRTUFIiLCJDYW5SZWplY3RTUFIiLCJDYW5WaWV3RGV0YWlsU1BSIiwiQ2FuQWRkQXR0YWNobWVudCIsIkNhblZpZXdQZXR0eUNhc2giLCJDYW5QcmludFBldHR5Q2FzaFRlbXBsYXRlMSIsIkNhblByaW50UGV0dHlDYXNoVGVtcGxhdGUyIiwiQ2FuRHJhZnRQZXR0eUNhc2giLCJDYW5FZGl0UGV0dHlDYXNoIiwiQ2FuUmVqZWN0UGV0dHlDYXNoIiwiQ2FuVmlld0RldGFpbFBldHR5Q2FzaCIsIkNhbkFkZFBldHR5Q2FzaEF0dGFjaG1lbnQiLCJDYW5BZGRQZXR0eUNhc2giLCJDYW5WaWV3SlYiLCJDYW5EcmFmdEpWIiwiQ2FuRWRpdEpWIiwiQ2FuVmlld0RldGFpbEpWIiwiQ2FuQWRkSlYiLCJDYW5WaWV3T0MiLCJDYW5EcmFmdE9DIiwiQ2FuRWRpdE9DIiwiQ2FuVmlld0RldGFpbE9DIiwiQ2FuQWRkT0MiLCJDYW5WaWV3Q3VycmVuY3kiLCJDYW5FZGl0Q3VycmVuY3kiLCJDYW5BZGRDdXJyZW5jeSIsIkNhblZpZXdCYW5rIiwiQ2FuRWRpdEJhbmsiLCJDYW5BZGRCYW5rIiwiQ2FuQWRkTW9udGhseUNvc3QiLCJDYW5WaWV3Q09BIiwiQ2FuRWRpdENPQSIsIkNhbkFkZENPQSIsIkNhblZpZXdQYXltZW50T3B0aW9uIiwiQ2FuRWRpdFBheW1lbnRPcHRpb24iLCJDYW5BZGRQYXltZW50T3B0aW9uIiwiQ2FuVmlld0Rlbm9taW5hdGlvbiIsIkNhbkVkaXREZW5vbWluYXRpb24iLCJDYW5BZGREZW5vbWluYXRpb24iLCJDYW5WaWV3VmF0UmF0ZSIsIkNhbkVkaXRWYXRSYXRlIiwiQ2FuQWRkVmF0UmF0ZSIsIkNhblZpZXdFeHBlbnNlVHlwZSIsIkNhbkVkaXRFeHBlbnNlVHlwZSIsIkNhbkFkZEV4cGVuc2VUeXBlIiwiQ2FuVmlld0ZpbmFuY2lhbFllYXIiLCJDYW5DcmVhdGVGaW5hbmNpYWxZZWFyIiwiQ2FuVmlld1N0b2NrUmVwb3J0IiwiQ2FuVmlld1NhbGVJbnZvaWNlUmVwb3J0IiwiQ2FuVmlld1B1cmNoYXNlSW52b2ljZVJlcG9ydCIsIkNhblZpZXdJbnZlbnRvcnlSZXBvcnQiLCJDYW5WaWV3UHJvZHVjdEludmVudG9yeVJlcG9ydCIsIkNhblZpZXdUcmlhbEJhbGFuY2UiLCJDYW5WaWV3QmFsYW5jZVNoZWV0UmVwb3J0IiwiQ2FuVmlld0luY29tZVN0YXRlbWVudFJlcG9ydCIsIkNhblZpZXdDdXN0b21lckxlZGdlciIsIkNhblZpZXdTdXBwbGllTGVkZ2VyIiwiQ2FuVmlld1N0b2NrVmFsdWVSZXBvcnQiLCJDYW5WaWV3U3RvY2tBdmVyYWdlVmFsdWUiLCJDYW5WaWV3VHJhbnNhY3Rpb25UeXBlU3RvY2siLCJDYW5WaWV3Q3VzdG9tZXJXaXNlUHJvZHVjdHNTYWxlIiwiQ2FuVmlld0N1c3RvbWVyc1dpc2VQcm9kdWN0U2FsZSIsIkNhblZpZXdTdXBwbGllcldpc2VQcm9kdWN0c1B1cmNoYXNlIiwiQ2FuVmlld1N1cHBsaWVyc1dpc2VQcm9kdWN0UHVyY2hhc2UiLCJDYW5WaWV3Q3VzdG9tZXJEaXNjb3VudFByb2R1Y3RzIiwiQ2FuVmlld1N1cHBsaWVyRGlzY291bnRQcm9kdWN0cyIsIkNhblZpZXdQcm9kdWN0RGlzY291bnRDdXN0b21lciIsIkNhblZpZXdQcm9kdWN0RGlzY291bnRTdXBwbGllciIsIkNhblZpZXdGcmVlT2ZDb3N0UHVyY2hhc2UiLCJDYW5WaWV3RnJlZU9mQ29zdFNhbGUiLCJDYW5WaWV3QWNjb3VudExlZGdlciIsIkNhblZpZXdCYW5UcmFuc2FjdGlvbiIsIkNhblZpZXdDdXN0b21lckJhbGFuY2UiLCJDYW5WaWV3U3VwcGxpZXJCYWxhbmNlIiwiQ2FuVmlld1ZhdFBheWFibGVSZXBvcnQiLCJDYW5WaWV3RGF5V2lzZVRyYW5zYWN0aW9ucyIsIkNhblZpZXdEYXlXaXNlUmVwb3J0IiwiQ2FuUHJpbnRTdG9ja1JlcG9ydCIsIkNhblByaW50U2FsZUludm9pY2VSZXBvcnQiLCJDYW5QcmludFB1cmNoYXNlSW52b2ljZVJlcG9ydCIsIkNhblByaW50SW52ZW50b3J5UmVwb3J0IiwiQ2FuUHJpbnRQcm9kdWN0SW52ZW50b3J5UmVwb3J0IiwiQ2FuUHJpbnRUcmlhbEJhbGFuY2UiLCJDYW5QcmludEJhbGFuY2VTaGVldFJlcG9ydCIsIkNhblByaW50SW5jb21lU3RhdGVtZW50UmVwb3J0IiwiQ2FuUHJpbnRDdXN0b21lckxlZGdlciIsIkNhblByaW50U3VwcGxpZUxlZGdlciIsIkNhblByaW50U3RvY2tWYWx1ZVJlcG9ydCIsIkNhblByaW50U3RvY2tBdmVyYWdlVmFsdWUiLCJDYW5QcmludFRyYW5zYWN0aW9uVHlwZVN0b2NrIiwiQ2FuUHJpbnRDdXN0b21lcldpc2VQcm9kdWN0c1NhbGUiLCJDYW5QcmludEN1c3RvbWVyc1dpc2VQcm9kdWN0U2FsZSIsIkNhblByaW50U3VwcGxpZXJXaXNlUHJvZHVjdHNQdXJjaGFzZSIsIkNhblByaW50U3VwcGxpZXJzV2lzZVByb2R1Y3RQdXJjaGFzZSIsIkNhblByaW50Q3VzdG9tZXJEaXNjb3VudFByb2R1Y3RzIiwiQ2FuUHJpbnRTdXBwbGllckRpc2NvdW50UHJvZHVjdHMiLCJDYW5QcmludFByb2R1Y3REaXNjb3VudEN1c3RvbWVyIiwiQ2FuUHJpbnRQcm9kdWN0RGlzY291bnRTdXBwbGllciIsIkNhblByaW50RnJlZU9mQ29zdFB1cmNoYXNlIiwiQ2FuUHJpbnRGcmVlT2ZDb3N0U2FsZSIsIkNhblByaW50QWNjb3VudExlZGdlciIsIkNhblByaW50QmFuVHJhbnNhY3Rpb24iLCJDYW5QcmludEN1c3RvbWVyQmFsYW5jZSIsIkNhblByaW50U3VwcGxpZXJCYWxhbmNlIiwiQ2FuUHJpbnRWYXRQYXlhYmxlUmVwb3J0IiwiQ2FuUHJpbnREYXlXaXNlVHJhbnNhY3Rpb25zIiwiQ2FuUHJpbnREYXlXaXNlUmVwb3J0IiwiQ2FuRHJhZnRFeHBlbnNlIiwiQ2FuVmlld0V4cGVuc2UiLCJDYW5BZGRFeHBlbnNlIiwiQ2FuRWRpdEV4cGVuc2UiLCJDYW5WaWV3RGV0YWlsRXhwZW5zZSIsIkNhblByaW50RXhwZW5zZSIsIkNhbkRlbGV0ZUV4cGVuc2UiLCJDYW5HZW5lcmF0ZUV4cGVuc2VEZXRhaWxQZGYiLCJDYW5HZW5lcmF0ZUV4cGVuc2VEZXRhaWxTaGVldCIsIkNhblNlbmRFeHBlbnNlRGV0YWlsRW1haWwiLCJDYW5VcGxvYWRFeHBlbnNlQXR0YWNobWVudCIsIkNhbkRyYWZ0RXhwZW5zZUJpbGwiLCJDYW5WaWV3RXhwZW5zZUJpbGwiLCJDYW5BZGRFeHBlbnNlQmlsbCIsIkNhbkVkaXRFeHBlbnNlQmlsbCIsIkNhblZpZXdEZXRhaWxFeHBlbnNlQmlsbCIsIkNhblByaW50RXhwZW5zZUJpbGwiLCJDYW5HZW5lcmF0ZURldGFpbFBkZkJpbGwiLCJDYW5HZW5lcmF0ZURldGFpbFNoZWV0QmlsbCIsIkNhblNlbmREZXRhaWxCaWxsRW1haWwiLCJDYW5QYXlFeHBlbnNlQmlsbCIsIkNhblVwbG9hZEV4cGVuc2VCaWxsQXR0YWNobWVudCIsIkNhblZpZXdXYXJlSG91c2UiLCJDYW5BZGRXYXJlSG91c2UiLCJDYW5FZGl0V2FyZUhvdXNlIiwiQ2FuVmlld1N0b2NrT3V0IiwiQ2FuQWRkU3RvY2tPdXQiLCJDYW5FZGl0U3RvY2tPdXQiLCJDYW5EcmFmdFN0b2NrT3V0IiwiQ2FuUHJpbnRTdG9ja091dCIsIkNhblZpZXdEZXRhaWxTdG9ja091dCIsIkNhbkdlbmVyYXRlUGRmU3RvY2tPdXQiLCJDYW5HZW5lcmF0ZVNoZWV0U3RvY2tPdXQiLCJDYW5TZW5kRW1haWxTdG9ja091dCIsIkNhblZpZXdTdG9ja0luIiwiQ2FuQWRkU3RvY2tJbiIsIkNhbkVkaXRTdG9ja0luIiwiQ2FuRHJhZnRTdG9ja0luIiwiQ2FuUHJpbnRTdG9ja0luIiwiQ2FuVmlld0RldGFpbFN0b2NrSW4iLCJDYW5HZW5lcmF0ZVBkZlN0b2NrSW4iLCJDYW5HZW5lcmF0ZVNoZWV0U3RvY2tJbiIsIkNhblNlbmRFbWFpbFN0b2NrSW4iLCJDYW5JbXBvcnRTdG9ja0luIiwiQ2FuVmlld1N0b2NrVHJhbnNmZXIiLCJDYW5BZGRTdG9ja1RyYW5zZmVyIiwiQ2FuRWRpdFN0b2NrVHJhbnNmZXIiLCJDYW5EcmFmdFN0b2NrVHJhbnNmZXIiLCJDYW5WaWV3UHJvZHVjdCIsIkNhbkVkaXRQcm9kdWN0IiwiQ2FuQWRkUHJvZHVjdCIsIkNhblZpZXdDYXRlZ29yeSIsIkNhbkVkaXRDYXRlZ29yeSIsIkNhbkFkZENhdGVnb3J5IiwiQ2FuVmlld1N1YkNhdGVnb3J5IiwiQ2FuRWRpdFN1YkNhdGVnb3J5IiwiQ2FuQWRkU3ViQ2F0ZWdvcnkiLCJDYW5WaWV3QnJhbmQiLCJDYW5FZGl0QnJhbmQiLCJDYW5BZGRCcmFuZCIsIkNhblZpZXdPcmlnaW4iLCJDYW5FZGl0T3JpZ2luIiwiQ2FuQWRkT3JpZ2luIiwiQ2FuVmlld1NpemUiLCJDYW5FZGl0U2l6ZSIsIkNhbkFkZFNpemUiLCJDYW5WaWV3Q29sb3IiLCJDYW5FZGl0Q29sb3IiLCJDYW5BZGRDb2xvciIsIkNhblZpZXdVbml0IiwiQ2FuRWRpdFVuaXQiLCJDYW5BZGRVbml0IiwiQ2FuVmlld0l0ZW0iLCJDYW5FZGl0SXRlbSIsIkNhbkFkZEl0ZW0iLCJDYW5WaWV3RGV0YWlsSXRlbSIsIkNhblZpZXdDdXJyZW50U3RvY2siLCJDYW5GaWx0ZXJJdGVtIiwiQ2FuSW1wb3J0UHJvZHVjdCIsIkNhbkFkZExpbWl0ZWRQcm9kdWN0IiwiQ2FuVmlld0ludmVudG9yeUNvdW50IiwiQ2FuRWRpdEludmVudG9yeUNvdW50IiwiQ2FuQWRkSW52ZW50b3J5Q291bnQiLCJDYW5WaWV3RGV0YWlsSW52ZW50b3J5Q291bnQiLCJDYW5QcmludEludmVudG9yeUNvdW50IiwiQ2FuUHJpbnRSYWNrQmFyY29kZSIsIkNhblByaW50SXRlbUJhcmNvZGUiLCJDYW5BZGRUcmFuc3BvcnRlciIsIkNhbkVkaXRUcmFuc3BvcnRlciIsIkNhblZpZXdUcmFuc3BvcnRlciIsIkNhbkFkZENsZWFyYW5jZUFnZW50IiwiQ2FuRWRpdENsZWFyYW5jZUFnZW50IiwiQ2FuVmlld0NsZWFyYW5jZUFnZW50IiwiQ2FuQWRkU2hpcHBpbmdMaW5lciIsIkNhbkVkaXRTaGlwcGluZ0xpbmVyIiwiQ2FuVmlld1NoaXBwaW5nTGluZXIiLCJDYW5VcGRhdGVDb21wYW55SW5mbyIsIkNhbkJhY2tVcERhdGEiLCJDYW5SZXN0b3JlRGF0YSIsIkNhblB1c2hSZWNvcmQiLCJDYW5QdWxsUmVjb3JkIiwiQ2FuRmx1c2hEYXRhYmFzZSIsIkNhblJlc2V0RGF0YWJhc2UiLCJDYW5BZGRQb3NUZXJtaW5hbCIsIkNhbkVkaXRQb3NUZXJtaW5hbCIsIkNhblZpZXdQb3NUZXJtaW5hbCIsIkNhbkFkZFRlcm1pbmFsIiwiQ2FuRWRpdFRlcm1pbmFsIiwiQ2FuVmlld1Rlcm1pbmFsIiwiU3RhcnREYXkiLCJDbG9zZURheSIsIklzQXJlYSIsIkVuZ2xpc2giLCJBcmFiaWMiLCJDYW5TdGFydERheSIsIklzT3BlbkRheSIsIkNhblRyYW5zZmVyQ291bnRlciIsIlRlcm1pbmFsIiwiRXhwZW5zZVdpdGhBY2NvdW50IiwiU2FsZU9yZGVySW52ZW50b3J5UmVzZXJ2ZSIsIkludGVybmF0aW9uYWxQdXJjaGFzZSIsIkV4cGVuc2VUb0dTVCIsIkFkbWluRGF5U3RhcnQiLCJCYW5rRGV0YWlsIiwiQ29hQXV0b0NvZGUiLCJTYWxlT3JkZXJUb0ludm9pY2UiLCJCMkIiLCJCMkMiLCLZgdin2KrZiNix2Kkg2LbYsdmK2KjZitipIiwiVGF4IEludm9pY2UiLCLZgdin2KrZiNix2Kkg2LbYsdmK2KjZitipINmF2KjYs9i32KkiLCJTaW1wbGlmaWVkIFRheCBJbnZvaWNlIiwiQ2FuVmlld0FsbG93YW5jZVR5cGUiLCJDYW5BZGRBbGxvd2FuY2VUeXBlIiwiQ2FuRWRpdEFsbG93YW5jZVR5cGUiLCJDYW5WaWV3QWxsb3dhbmNlIiwiQ2FuQWRkQWxsb3dhbmNlIiwiQ2FuRWRpdEFsbG93YW5jZSIsIkNhblZpZXdEZWR1Y3Rpb24iLCJDYW5BZGREZWR1Y3Rpb24iLCJDYW5FZGl0RGVkdWN0aW9uIiwiQ2FuVmlld0NvbnRyaWJ1dGlvbiIsIkNhbkFkZENvbnRyaWJ1dGlvbiIsIkNhbkVkaXRDb250cmlidXRpb24iLCJDYW5WaWV3UGF5Um9sbFNjaGVkdWxlIiwiQ2FuQWRkUGF5Um9sbFNjaGVkdWxlIiwiQ2FuRWRpdFBheVJvbGxTY2hlZHVsZSIsIkNhblZpZXdTYWxlcnlUZW1wbGF0ZSIsIkNhbkFkZFNhbGVyeVRlbXBsYXRlIiwiQ2FuRWRpdFNhbGVyeVRlbXBsYXRlIiwiQ2FuVmlld0VtcGxveWVlU2FsYXJ5IiwiQ2FuQWRkRW1wbG95ZWVTYWxhcnkiLCJDYW5FZGl0RW1wbG95ZWVTYWxhcnkiLCJDYW5WaWV3TG9hblBheW1lbnQiLCJDYW5BZGRMb2FuUGF5bWVudCIsIkNhbkVkaXRMb2FuUGF5bWVudCIsIkNhblZpZXdTYWxhcnlUYXhTbGFiIiwiQ2FuQWRkU2FsYXJ5VGF4U2xhYiIsIkNhbkVkaXRTYWxhcnlUYXhTbGFiIiwiQ2FuVmlld1J1blBheXJvbGwiLCJDYW5WaWV3QmFua1J1blBheXJvbGwiLCJDYW5WaWV3Q2FzaFJ1blBheXJvbGwiLCJDYW5BZGRSdW5QYXlyb2xsIiwiQ2FuRHJhZnRSdW5QYXlyb2xsIiwiQ2FuRWRpdE9wZW5SdW5QYXlyb2xsIiwiQ2FuRWRpdENsb3NlUnVuUGF5cm9sbCIsIkNhbkRvd25sb2FkUGRmUnVuUGF5cm9sbCIsIkNhbkRvd25sb2FkQ3N2UnVuUGF5cm9sbCIsIkNhblZpZXdFbXBsb3llZVJlZyIsIkNhbkFkZEVtcGxveWVlUmVnIiwiQ2FuRWRpdEVtcGxveWVlUmVnIiwiQ2FuVmlld1dhcnJhbnR5VHlwZSIsIkNhbkFkZFdhcnJhbnR5VHlwZSIsIkNhbkVkaXRXYXJyYW50eVR5cGUiLCJDYW5WaWV3RGFzaGJvYXJkIiwiQ2FuU2VuZFNhbGVFbWFpbEFzUGRmIiwiQ2FuVmlld1VuaXRQZXJQYWNrIl0sImV4cCI6MTY1ODY1NzAxMCwiaXNzIjoiaHR0cDovL3lvdXJkb21haW4uY29tIiwiYXVkIjoiaHR0cDovL3lvdXJkb21haW4uY29tIn0.6yeKJiSdNv-R1_Hfkek1gLj_tA6TRFvjOPMSUjaIK-8';
    localStorage.setItem('CurrentPath', to.name);
    console.log(from.name);
    next();
});