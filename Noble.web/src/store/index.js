import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export const store= new Vuex.Store({
    state: {
        companyList: [],
        customerList: [],
        supplierList: [],
        businessList: [],
        brandList: [],
        sizeList: [],
        regionList: [],
        colorList: [],
        originList: [],
        taxRateList: [],
        accounts: [],
        unitList: [],
        categoryList: [],
        subCategoryList: [],
        purchaseOrderList: [],
        purchasePostList: [],   
        terminalList: [],
        currencyList: [],
        paymentOptionsList: [],
        journalVoucherList: [],
        vouchersList: [],
        stockValueList: [],
        
        dailyExpenseList: [],
        mobileOrderList: [],

        bankList: [],
        productMasterList: [],
        rolesList: [],
        permissionList: [],
        userRolesList: [],
        employeeList: [],
        cityList: [],
        wareHouseTransferList: [],
        wareHouseList: [],
        printSettingList: [],
        processList: [],
        denominationSetupList: [],
        allowanceTypeList: [],
        authenticated: false
  },
    mutations: {
        setAuthentication(state, status) {
            state.authenticated = status;
        }
  },
    actions: {
        
        GetCustomerList: function (commit, customerList) {
            
            store.state.customerList = customerList;
        },
        GetAllowanceTypeList: function (commit, allowanceTypeList) {
            
            store.state.allowanceTypeList = allowanceTypeList;
        },
        GetProcessList: function (commit, processList) {
            
            store.state.processList = processList;
        },
        GetUserRolesList: function (commit, userRolesList) {
            
            store.state.userRolesList = userRolesList;
        },
        GetPermissionList: function (commit, permissionList) {
            
            store.state.permissionList = permissionList;
        },
        GetRoleList: function (commit, rolesList) {
            
            store.state.rolesList = rolesList;
        },
        GetComapanyList: function (commit, companyList) {
            
            store.state.companyList = companyList;
        },
        GetTerminalList: function (commit, terminalList) {
            
            store.state.terminalList = terminalList;
        },
        GetSupplierList: function (commit, supplierList) {
            
            store.state.supplierList = supplierList;
        },
        GetBusinessList: function (commit, businessList) {
            
            store.state.businessList = businessList;
        },
        GetAccountList: function (commit, accounts) {
            
            store.state.accounts = accounts;
        },
        GetBrandList: function (commit, brandList) {
            
            store.state.brandList = brandList;
        },
        GetProductMasterList: function (commit, productMasterList) {
            
            store.state.productMasterList = productMasterList;
        },
        GetSizeList: function (commit, sizeList) {
            
            store.state.sizeList = sizeList;
        },
        GetColorList: function (commit, colorList) {
            
            store.state.colorList = colorList;
        },
        GetOriginList: function (commit, originList) {
            
            store.state.originList = originList;
        },
        GetUnitList: function (commit, unitList) {
            
            store.state.unitList = unitList;
        }
        ,
        GetTaxRateList: function (commit, taxRateList) {
            
            store.state.taxRateList = taxRateList;
        },
        GetCategoryList: function (commit, categoryList) {
            
            store.state.categoryList = categoryList;
        },
        GetSubCategoryList: function (commit, subCategoryList) {
            
            store.state.subCategoryList = subCategoryList;
        },
        GetPurchaseOrderList: function (commit, purchaseOrderList) {
            store.state.purchaseOrderList = purchaseOrderList;
        },
        GetPurchasePostList: function (commit, purchasePostList) {
            
            store.state.purchasePostList = purchasePostList;
        },
        GetCurrencyList: function (commit, currencyList) {
            
            store.state.currencyList = currencyList;
        },
        GetPaymentOptionsList: function (commit, paymentOptionsList) {
            
            store.state.paymentOptionsList = paymentOptionsList;
        },
        GetPaymentVouchersList: function (commit, vouchersList) {
            
            store.state.vouchersList = vouchersList;
        },
        GetStockValueList: function (commit, stockValueList) {
            
            store.state.stockValueList = stockValueList;
        },
        GetjournalVoucherList: function (commit, journalVoucherList) {

            store.state.journalVoucherList = journalVoucherList;
        },
        GetDailyExpenseList: function (commit, dailyExpenseList) {
            store.state.dailyExpenseList = dailyExpenseList;
        },
        GetDailyMobileOrderList: function (commit, mobileOrderList) {
            store.state.mobileOrderList = mobileOrderList;
        },
        GetBankList: function (commit, bankList) {
            store.state.bankList = bankList;
        },
        GetWareHouseTransferList: function (commit, wareHouseTransferList) {
            store.state.wareHouseTransferList = wareHouseTransferList;
        },
        GetEmployeeList: function (commit, employeeList) {
            store.state.employeeList = employeeList;
        },
        GetCityList: function (commit, cityList) {
            store.state.cityList = cityList;
        },
        GetRegionList: function (commit, regionList) {
            store.state.regionList = regionList;
        },
        GetPrintSettingList: function (commit, printSettingList) {
            store.state.printSettingList = printSettingList;
        },
        GetWareHouseList: function (commit, wareHouseList) {
            store.state.wareHouseList = wareHouseList;
        },
        GetDenominationSetupList: function (commit, denominationSetupList) {
            store.state.denominationSetupList = denominationSetupList;
        },
  },
  modules: {
  }
})
