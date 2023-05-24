<template>
    <div v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'left:0' :'direction: rtl;right:0'">
        <div class="sidebar" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'sidebar_arabic'">
            <div class="logo" v-on:click="StartScreen">
                <a href="javascript:void(0)" class="simple-text logo-mini">
                    <div class="logo-image-small">
                        <img src="/images/logo-mini.svg">
                    </div>
                </a>
                <a href="javascript:void(0)" class="simple-text logo-normal">
                    <div class="logo-image-small" style="font-size: 17px; padding-top: 5px; padding-left: 5px; ">
                        {{applicationName}}
                        <!--<img src="Noble-POS-logo.png" v-bind:style="$i18n.locale == 'en' ? 'padding-left:7px;width:157px' : 'padding-left:39px;width:174px'" class="hidee">-->
                    </div>
                </a>
            </div>

            <div class="sidebar-wrapper">
                <ul class="nav " v-bind:style=" ($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'padding-right:10px !important;'">

                    <li v-if="isValid('CanViewDashboard') && role!='Noble Admin'">
                        <router-link :to="{path: '/Dashboard1', query: { token_name:'DayStart_token', fromDashboard:'true' } }" v-bind:class="propValvue=='Dashboard1'? 'router-link-exact-active router-link-active' : ''">
                            <i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/DashBoard.svg" /></i>
                            <p class="menu_txt" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">  {{ $t('Dashboard.Dashboard') }}</p>
                        </router-link>
                    </li>
                    <li v-if="isValid('CanViewDashboard') && role=='Noble Admin'">
                        <router-link :to="'/'">
                            <i><img class="ImageWidth" src="Noble menu icons/DashBoard.svg" /></i>
                            <p v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">{{ $t('Dashboard.Dashboard') }}</p>
                        </router-link>
                    </li>
                    <li v-if="role=='Noble Admin'">
                        <router-link :to="'/clientManagement'">
                            <i class="fas fa-tasks"></i>
                            <p v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">    Client Management</p>
                        </router-link>
                    </li>

                    <li v-if="role=='Admin'">
                        <a data-toggle="collapse" href="#Manage" aria-expanded="false">
                            <i><img class="ImageWidth" src="Noble menu icons/DashBoard.svg" /></i>
                            <p v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                {{ $t('Dashboard.Dashboard') }}
                                <b class="caret"></b>
                            </p>
                        </a>

                        <div class="collapse" id="Manage">
                            <ul class="nav">
                                <li v-if="isValid('CanViewDashboard') && role=='Admin'">
                                    <router-link :to="'/business'">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">B</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.business_Heading') }}

                                        </span>
                                    </router-link>
                                    <router-link :to="'/CompanyAdditionalInfo'">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CAI</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Company_Additional_Information') }}
                                        </span>
                                    </router-link>
                                    <router-link :to="'/CompanyAttachments'">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CA</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            Company Attachments
                                        </span>
                                    </router-link>
                                </li>
                            </ul>
                        </div>
                    </li>



<!-- Tommorow Start From Here -->


                    <li v-if="role=='Admin'">
                        <a data-toggle="collapse" href="#Manage" aria-expanded="false">
                            <i class="nc-icon nc-ruler-pencil"></i>
                            <p v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                {{ $t('Dashboard.Manage') }}
                                <b class="caret" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'right:0;' : 'position:static !important ;' "></b>
                            </p>
                        </a>

                        <div class="collapse" id="Manage">
                            <ul class="nav">
                                <li>
                                    <router-link :to="'/location'">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">L</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Location_Heading') }}

                                        </span>
                                    </router-link>

                                    <router-link :to="'/CompanyAttachments'">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">BA</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            Business Attachments
                                        </span>
                                    </router-link>
                                </li>
                            </ul>
                        </div>
                    </li>


                    <!--Menu List-->

                    <li v-if="isValid('StartOperationReport') || isValid('StartOperationSetup') || isValid('StartDay')">
                        <a data-toggle="collapse" href="#StartOperationManagement ">
                            <i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/start operation.svg" /></i>
                            <p class="menu_txt" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                {{ $t('Dashboard.StartOperations') }}
                                <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                            </p>
                        </a>
                        <div class="collapse" id="StartOperationManagement">
                            <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:0px !important' : ''">
                                <li v-if=" isValid('StartDay') && isDayStart=='true'">
                                    <router-link :to="{path: '/WholeSaleDay', query: { token_name:'DayStart_token', fromDashboard:'true' } }" v-if="WholeSale == 'true'" v-bind:class="propValvue=='daystart'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SO</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.StartOperation') }}
                                        </span>

                                    </router-link>
                                    <router-link :to="{path: '/daystart', query: { token_name:'DayStart_token', fromDashboard:'true' } }" v-else v-bind:class="propValvue=='daystart'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SO</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.StartOperation') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li v-if="isValid('StartOperationReport')">
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SOR</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Reports') }}
                                        </span>

                                    </a>
                                </li>
                                <li v-if="isValid('StartOperationSetup')">
                                    <a data-toggle="collapse" href="#StartOperationSetup ">
                                        <!--<i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/warehouse.svg" /></i>-->
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SOS</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Setup') }}
                                            <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                                        </span>
                                    </a>
                                    <div class="collapse" id="StartOperationSetup">
                                        <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:15px !important' : 'padding-left:15px !important'">
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">US</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.UserSetup') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CS</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.CounterSetup') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">AS</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.AdditionalSetup') }}
                                                    </span>

                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                </li>

                            </ul>
                        </div>
                    </li>


                    <li v-if="isValid('CanViewExpense') || isValid('CanDraftExpense') || isValid('ExpenseSetup')">
                        <a data-toggle="collapse" href="#DailyExpenseManagement ">
                            <i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/Daily Expense.svg" /></i>
                            <p class="menu_txt" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                {{ $t('Dashboard.DailyExpenses') }}
                                <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                            </p>
                        </a>
                        <div class="collapse" id="DailyExpenseManagement">
                            <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:0px !important' : ''">
                                <li v-if="(isValid('CanViewExpense') || isValid('CanDraftExpense')) && IsDailyExpense!=true && IsExpenseAccount">
                                    <router-link :to="{path: '/dailyexpense', query: { formName: 'generalexpense', token_name:'Expenses_token', fromDashboard:'true' } }" v-bind:class="propValvue=='DailyExpensegeneralexpense' || propValvue=='AddDailyExpense'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SO</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.DailyExpense') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li v-else-if="isValid('CanViewExpense') || isValid('CanDraftExpense') &&  IsDailyExpense==true">
                                    <router-link :to="{path: '/dailyexpense', query: {formName: 'dailyexpense', token_name:'Expenses_token', fromDashboard:'true' } }" v-bind:class="propValvue=='DailyExpensedailyexpense' || propValvue=='AddDailyExpense'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SO</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.DailyExpense') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li v-else-if="isValid('CanViewExpense') || isValid('CanDraftExpense') &&  IsExpenseAccount==false">
                                    <router-link :to="{path: '/dailyexpense', query: {  formName: 'dailyexpense' , token_name:'Expenses_token', fromDashboard:'true' } }" v-bind:class="propValvue=='DailyExpensedailyexpense' || propValvue=='AddDailyExpense'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SO</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.DailyExpense') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li v-if="isValid('ExpenseSetup')">
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">ES</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.ExpenseSetup') }}
                                        </span>

                                    </a>
                                </li>
                            </ul>
                        </div>
                    </li>


                    <li v-if="isValid('SimpleSaleInvoice')  && (isValid('CanAddQuotation') || isValid('CanDraftQuotation') || isValid('CanViewQuotation') || isValid('CustomerPurchaseOrder') || isValid('CustomerInquiry') ||isValid('SaleReport')|| isValid('DeliveryManagement')|| isValid('BillingManagement')|| isValid('ProposalManagement') ||isValid('CanChooseA4InvoiceType') || isValid('CanChooseThermalInvoiceType') || isValid('CanViewHoldInvoices') || isValid('CanViewPaidInvoices') || isValid('CanViewCreditInvoices') ||isValid('CanHoldInvoices') || isValid('CashInvoices') || isValid('CreditInvoices') || isValid('CanViewCustomer')||
  isValid('CanAddCustomer') || isValid('TouchInvoiceTemplate1')|| isValid('TouchInvoiceTemplate2')
  || isValid('TouchInvoiceTemplate3') || isValid('CanAddSaleReturn') || isValid('CanViewSaleReturn')||
  isValid('CanAddSaleOrder') || isValid('CanViewSaleOrder') || isValid('CanDraftSaleOrder') ||
  isValid('CanDraftCPR') || isValid('CanViewCPR') || isValid('CanAddCPR') || isValid('CanAddServiceQuotation')
  || (isValid('InquiryManagement') && (isValid('CanViewInquiryProcess') || isValid('CanViewInquirySetup')
  || isValid('CanViewInquiryType') || isValid('CanViewInquiry') || isValid('CanAddInquiry'))))">
                        <a data-toggle="collapse" href="#Sale" v-bind:aria-expanded="saleMenu? true : false">
                            <i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/sales.svg" /></i>
                            <p class="menu_txt" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                {{ $t('Dashboard.Sales') }}
                                <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                            </p>
                        </a>
                        <div class="collapse" v-bind:class="saleMenu ? 'show' : ''" id="Sale">
                            <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:0px !important' : ''">
                                <li v-if="isValid('CanHoldInvoices') || isValid('CashInvoices') || isValid('CreditInvoices')">
                                    <router-link :to="{path: '/addSale', query: { token_name:'Sales_token', fromDashboard:'true' } }" v-bind:class="propValvue=='AddSale'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">DI</span>
                                        <span class="sidebar-normal " v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.AddInvoice') }}
                                        </span>

                                    </router-link>

                                </li>
                                <li v-if="isValid('CanViewProforma')">
                                    <router-link :to="{path: '/ProformaInvoices', query: { token_name:'Sales_token', fromDashboard:'true' } }" v-bind:class="propValvue=='ProformaInvoices'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">PI</span>
                                        <span class="sidebar-normal " v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            Proforma Invoices
                                        </span>

                                    </router-link>

                                </li>
                                <li v-if="isValid('TouchInvoiceTemplate1')" >
                                    <router-link :to="{path: '/TouchScreen', query: { token_name:'Sales_token', fromDashboard:'true' } }">

                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">TS</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.TouchInvoice') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li v-if="isValid('TouchInvoiceTemplate2')">
                                    <router-link :to="{path: '/InvoiceBarcode', query: { token_name:'Sales_token', fromDashboard:'true' } }">

                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">IB</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.TouchInvoice') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li v-if="isValid('TouchInvoiceTemplate3')">
                                    <router-link :to="{path: '/InvoiceBarCodeItem', query: { token_name:'Sales_token', fromDashboard:'true' } }">

                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">IBI</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.TouchInvoice') }}
                                        </span>

                                    </router-link>
                                </li>

                                <li v-if=" isValid('CanViewSaleReturn')">
                                    <router-link :to="{path: '/SaleReturn', query: { token_name:'Sales_token', fromDashboard:'true' } }" v-bind:class="propValvue=='SaleReturn' || propValvue=='AddSaleReturn'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SR</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.ReturnInvoice') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li v-else-if=" isValid('CanAddSaleReturn')">
                                    <router-link :to="{path: '/AddSaleReturn', query: { token_name:'Sales_token', fromDashboard:'true' } }" v-bind:class="propValvue=='SaleReturn' || propValvue=='AddSaleReturn'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SR</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.ReturnInvoice') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li v-if="isValid('CanViewHoldInvoices') || isValid('CanViewPaidInvoices') || isValid('CanViewCreditInvoices') ">
                                    <router-link :to="{path: '/Sale', query: { token_name:'Sales_token', fromDashboard:'true' } }" v-bind:class="propValvue=='Sales'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SR</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.SalesRecords') }}
                                        </span>

                                    </router-link>
                                </li>

                                <li v-if="isValid('CanViewCPR') || isValid('CanDraftCPR')">
                                    <router-link :to="{path: '/paymentVoucherList', query: { formName: 'BankReceipt', token_name:'Sales_token', fromDashboard:'true' } }" v-bind:class="propValvue=='PaymentVoucherListBankReceipt' || propValvue=='addPaymentVoucherBankReceipt'? 'router-link-exact-active router-link-active' : ''">

                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CP</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.CustomerPayReceipt') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li v-if="isValid('CanViewCustomer')">
                                    <router-link :to="{path: '/Customer', query: { token_name:'Sales_token', fromDashboard:'true' } }" v-bind:class="propValvue=='Customer' || propValvue=='AddCustomer2'? 'router-link-exact-active router-link-active' : ''">

                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CU</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.ClientCustomer') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li v-else-if="isValid('CanAddCustomer')">
                                    <router-link :to="{path: '/addCustomer2', query: { token_name:'Sales_token', fromDashboard:'true' } }" v-bind:class="propValvue=='Customer' || propValvue=='AddCustomer2'? 'router-link-exact-active router-link-active' : ''">

                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CU</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.ClientCustomer') }}
                                        </span>

                                    </router-link>
                                </li>

                                <li v-if="isValid('CustomerPurchaseOrder')">
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CPO</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.CustomerPurchaseOrder') }}
                                        </span>

                                    </a>
                                </li>

                                <li v-if="isValid('CustomerInquiry')">
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CI</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.CustomerInquiry') }}
                                        </span>

                                    </a>
                                </li>
                                <li v-if="(isValid('CanViewQuotation') || isValid('CanDraftQuotation')) ">
                                    <router-link :to="{path: '/Quotation', query: { token_name:'Sales_token', fromDashboard:'true' } }" v-bind:class="propValvue=='Quotation' || propValvue=='AddQuotation'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">QO</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Quotation') }}
                                        </span>
                                    </router-link>
                                </li>
                                <li v-else-if="(isValid('CanAddQuotation'))">
                                    <router-link :to="{path: '/AddQuotation', query: { token_name:'Sales_token', fromDashboard:'true' } }" v-bind:class="propValvue=='Quotation' || propValvue=='AddQuotation'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">QO</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Quotation') }}
                                        </span>
                                    </router-link>
                                </li>
                                <li v-if="(isValid('CanViewSaleOrder') || isValid('CanDraftSaleOrder')) ">
                                    <router-link :to="{path: '/SaleOrder', query: { token_name:'Sales_token', fromDashboard:'true' } }" v-bind:class="propValvue=='SaleOrder' || propValvue=='AddSaleOrder'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SO</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.SalesOrder') }}
                                        </span>
                                    </router-link>
                                </li>
                                <li v-else-if="(isValid('CanAddSaleOrder'))">
                                    <router-link :to="{path: '/AddSaleOrder', query: { token_name:'Sales_token', fromDashboard:'true' } }" v-bind:class="propValvue=='SaleOrder' || propValvue=='AddSaleOrder'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SO</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.SalesOrder') }}
                                        </span>
                                    </router-link>
                                </li>

                                <li v-if="isValid('SaleReport')">
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SR</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.SalesReports') }}
                                        </span>

                                    </a>
                                </li>
                                <li v-if="isValid('CanChooseA4InvoiceType') || isValid('CanChooseThermalInvoiceType')">
                                    <router-link :to="{path: '/PrintSetting', query: { token_name:'Sales_token', fromDashboard:'true' } }" v-bind:class="propValvue=='PrintSetting'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">IS</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.InvoiceSetup') }}
                                        </span>
                                    </router-link>
                                </li>
                                <li >
                                    <router-link :to="{path: '/DiscountSetup', query: { token_name:'Sales_token', fromDashboard:'true' } }" v-bind:class="propValvue=='PrintSetting'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">IS</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.DiscountSetup') }}
                                        </span>
                                    </router-link>
                                </li>

                                <li v-if="isValid('DeliveryManagement')">
                                    <a data-toggle="collapse" href="#DeliveryManagement">
                                        <!--<i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/warehouse.svg" /></i>-->
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">DM</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.DeliveryManagement') }}
                                            <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                                        </span>
                                    </a>
                                    <div class="collapse" id="DeliveryManagement">
                                        <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:15px !important' : 'padding-left:15px !important'">
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">DL</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Delivery') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">DR</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.DeliveryReports') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">DS</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.DeliverySetup') }}
                                                    </span>

                                                </a>
                                            </li>

                                        </ul>
                                    </div>
                                </li>


                                <li v-if="isValid('BillingManagement')">
                                    <a data-toggle="collapse" href="#BillingManagement">
                                        <!--<i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/warehouse.svg" /></i>-->
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">BM</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.BillingManagement') }}
                                            <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                                        </span>
                                    </a>
                                    <div class="collapse" id="BillingManagement">
                                        <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:15px !important' : 'padding-left:15px !important'">
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">BL</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Billing') }}
                                                    </span>

                                                </a>
                                            </li>

                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">BR</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.BillingReports') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">BS</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.BillingSetup') }}
                                                    </span>

                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                </li>


                                <li v-if="isValid('ProposalManagement')">
                                    <a data-toggle="collapse" href="#ProposalManagement">
                                        <!--<i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/warehouse.svg" /></i>-->
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">PM</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.ProposalManagement') }}
                                            <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                                        </span>
                                    </a>
                                    <div class="collapse" id="ProposalManagement">
                                        <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:15px !important' : 'padding-left:15px !important'">
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CP</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.CommercialProposal') }}
                                                    </span>

                                                </a>
                                            </li>

                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CPR</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.ProposalReports') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CPS</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.ProposalSetup') }}
                                                    </span>

                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                </li>



                                <li v-if="(isValid('InquiryManagement') && (isValid('CanViewInquiryProcess') || isValid('CanViewInquirySetup') || isValid('CanViewInquiryType') || isValid('CanViewInquiry') || isValid('CanAddInquiry') ))">
                                    <a data-toggle="collapse" href="#InquiryManagement" v-bind:aria-expanded="inquiryMenu? true : false">

                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">INQ</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.InquiryManagement') }}
                                            <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                                        </span>
                                    </a>
                                    <div class="collapse" id="InquiryManagement">
                                        <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:15px !important' : 'padding-left:15px !important'">

                                            <li v-if="(isValid('CanViewInquiry') )">
                                                <router-link :to="{path: '/Inquiry', query: { token_name:'Inquiry Management_token', fromDashboard:'true' } }" v-bind:class="propValvue=='Inquiry' ? 'router-link-exact-active router-link-active' : ''">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">INQ</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Inquiry') }}
                                                    </span>
                                                </router-link>
                                            </li>
                                            <li v-else-if="(isValid('CanAddInquiry'))">
                                                <router-link :to="{path: '/AddInquiry', query: { token_name:'Inquiry Management_token', fromDashboard:'true' } }" v-bind:class="propValvue=='Inquiry' ? 'router-link-exact-active router-link-active' : ''">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">INQ</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Inquiry') }}
                                                    </span>
                                                </router-link>
                                            </li>

                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SR</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.InquiryReports') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li v-if="(isValid('CanViewInquiryProcess') || isValid('CanViewInquirySetup') || isValid('CanViewInquiryType'))">
                                                <router-link :to="{path: '/InquirySetup', query: { token_name:'Inquiry Management_token', fromDashboard:'true' } }" v-bind:class="propValvue=='InquirySetup'? 'router-link-exact-active router-link-active' : ''">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">IS</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.InquirySetup') }}
                                                    </span>
                                                </router-link>
                                            </li>


                                        </ul>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </li>



                    <li v-if="isValid('SaleServiceInvoice') && (isValid('CustomerPurchaseOrder') || isValid('CustomerInquiry') || isValid('SaleServiceReport') ||isValid('SaleServiceReturn') ||isValid('DeliveryManagement') ||isValid('BillingManagement') ||isValid('ProposalManagement') || isValid('CanChooseA4InvoiceType') || isValid('CanChooseThermalInvoiceType') || isValid('CanViewCustomer')|| isValid('CanAddCustomer') || isValid('TouchInvoiceTemplate1')|| isValid('TouchInvoiceTemplate2') || isValid('TouchInvoiceTemplate3') || isValid('CanDraftCPR') || isValid('CanViewCPR') || isValid('CanAddCPR') || isValid('CanAddServiceQuotation') || isValid('CanViewServiceQuotation') || isValid('CanDraftServiceQuotation') || isValid('CanViewHoldServiceInvoices') || isValid('CanViewPaidServiceInvoices') || isValid('CanViewCreditServiceInvoices') || isValid('CanHoldServiceInvoices') || isValid('CashServiceInvoices') || isValid('CreditServiceInvoices')  || isValid('CanAddServiceSaleOrder') || isValid('CanViewServiceSaleOrder') || isValid('CanDraftServiceSaleOrder') || (isValid('InquiryManagement') && (isValid('CanViewInquiryProcess') || isValid('CanViewInquirySetup') || isValid('CanViewInquiryType') || isValid('CanViewInquiry') || isValid('CanAddInquiry') )))">
                        <a data-toggle="collapse" href="#ServiceSale" v-bind:aria-expanded="saleMenu? true : false">
                            <i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/sales.svg" /></i>
                            <p class="menu_txt" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                {{ $t('Dashboard.Services-Sales') }}
                                <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                            </p>
                        </a>
                        <div class="collapse" v-bind:class="saleMenu ? 'show' : ''" id="ServiceSale">
                            <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:0px !important' : ''">
                                <li v-if="isValid('CashServiceInvoices') || isValid('CreditServiceInvoices') || isValid('CanHoldServiceInvoices')">
                                    <router-link :to="{path: '/AddSaleService', query: { token_name:'Sales_token', fromDashboard:'true' } }" v-bind:class="propValvue=='AddSaleService'? 'router-link-exact-active router-link-active' : ''">

                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CP</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.AddInvoice') }}
                                        </span>
                                    </router-link>
                                </li>
                                <li v-if="isValid('SaleServiceReturn')">
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SSR</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.ReturnInvoice') }}
                                        </span>

                                    </a>
                                </li>
                                <li v-if="isValid('CanViewHoldServiceInvoices') || isValid('CanViewPaidServiceInvoices') || isValid('CanViewCreditServiceInvoices')">
                                    <router-link :to="{path: '/SaleService', query: { token_name:'Sales_token', fromDashboard:'true' } }" v-bind:class="propValvue=='SaleService'? 'router-link-exact-active router-link-active' : ''">

                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CP</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.SalesRecords') }}
                                        </span>
                                    </router-link>
                                </li>


                                <li v-if="isValid('CustomerPurchaseOrder')">
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CPO</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.CustomerPurchaseOrder') }}
                                        </span>

                                    </a>
                                </li>

                                <li v-if="isValid('CustomerInquiry')">
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CI</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.CustomerInquiry') }}
                                        </span>

                                    </a>
                                </li>

                                <li v-if="isValid('CanViewCPR') || isValid('CanDraftCPR')">
                                    <router-link :to="{path: '/paymentVoucherList', query: { formName: 'BankReceipt', token_name:'Sales_token', fromDashboard:'true' } }" v-bind:class="propValvue=='PaymentVoucherListBankReceipt' || propValvue=='addPaymentVoucherBankReceipt'? 'router-link-exact-active router-link-active' : ''">

                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CP</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.CustomerPayReceipt') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li v-if="isValid('CanViewCustomer')">
                                    <router-link :to="{path: '/Customer', query: { token_name:'Sales_token', fromDashboard:'true' } }" v-bind:class="propValvue=='Customer' || propValvue=='AddCustomer2'? 'router-link-exact-active router-link-active' : ''">

                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CU</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.ClientCustomer') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li v-else-if="isValid('CanAddCustomer')">
                                    <router-link :to="{path: '/addCustomer2', query: { token_name:'Sales_token', fromDashboard:'true' } }" v-bind:class="propValvue=='Customer' || propValvue=='AddCustomer2'? 'router-link-exact-active router-link-active' : ''">

                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CU</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.ClientCustomer') }}
                                        </span>

                                    </router-link>
                                </li>

                                <li v-if="isValid('CanViewServiceQuotation') || isValid('CanDraftServiceQuotation')">
                                    <router-link :to="{path: '/ServiceQuotation', query: { token_name:'Sales_token', fromDashboard:'true' } }" v-bind:class="propValvue=='ServiceQuotation' || propValvue=='AddServiceQuotation'? 'router-link-exact-active router-link-active' : ''">

                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SQ</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Quotation') }}
                                        </span>
                                    </router-link>
                                </li>
                                <li v-else-if="isValid('CanAddServiceQuotation')">
                                    <router-link :to="{path: '/AddServiceQuotation', query: { token_name:'Sales_token', fromDashboard:'true' } }" v-bind:class="propValvue=='ServiceQuotation' || propValvue=='AddServiceQuotation'? 'router-link-exact-active router-link-active' : ''">

                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SQ</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Quotation') }}
                                        </span>
                                    </router-link>
                                </li>

                                <li v-if="isValid('CanViewServiceSaleOrder') || isValid('CanDraftServiceSaleOrder')">
                                    <router-link :to="{path: '/SaleServiceOrder', query: { token_name:'Sales_token', fromDashboard:'true' } }" v-bind:class="propValvue=='SaleServiceOrder' || propValvue=='AddSaleServiceOrder'? 'router-link-exact-active router-link-active' : ''">

                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SS</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.SalesOrder') }}
                                        </span>
                                    </router-link>
                                </li>
                                <li v-else-if="isValid('CanAddServiceSaleOrder')">
                                    <router-link :to="{path: '/AddSaleServiceOrder', query: { token_name:'Sales_token', fromDashboard:'true' } }" v-bind:class="propValvue=='SaleServiceOrder' || propValvue=='AddSaleServiceOrder'? 'router-link-exact-active router-link-active' : ''">

                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SS</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.SalesOrder') }}
                                        </span>
                                    </router-link>
                                </li>

                                <li v-if="isValid('SaleServiceReport')">
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SR</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Services-SalesReports') }}
                                        </span>

                                    </a>
                                </li>

                                <li v-if="isValid('CanChooseA4InvoiceType') || isValid('CanChooseThermalInvoiceType')">
                                    <router-link :to="{path: '/PrintSetting', query: { token_name:'Sales_token', fromDashboard:'true' } }" v-bind:class="propValvue=='PrintSetting'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">IS</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.InvoiceSetup') }}
                                        </span>
                                    </router-link>
                                </li>

                                <li>
                                    <router-link :to="{path: '/DiscountSetup', query: { token_name:'Sales_token', fromDashboard:'true' } }" v-bind:class="propValvue=='PrintSetting'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">IS</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.DiscountSetup') }}
                                        </span>
                                    </router-link>
                                </li>

                                <li v-if="isValid('DeliveryManagement')">
                                    <a data-toggle="collapse" href="#DeliverySManagement">
                                        <!--<i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/warehouse.svg" /></i>-->
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">DM</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.DeliveryManagement') }}
                                            <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                                        </span>
                                    </a>
                                    <div class="collapse" id="DeliverySManagement">
                                        <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:15px !important' : 'padding-left:15px !important'">
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">DL</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Delivery') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">DR</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.DeliveryReports') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">DS</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.DeliverySetup') }}
                                                    </span>

                                                </a>
                                            </li>

                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">GP</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.GatePass') }}
                                                    </span>

                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                </li>


                                <li v-if="isValid('BillingManagement')">
                                    <a data-toggle="collapse" href="#BillingSManagement">
                                        <!--<i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/warehouse.svg" /></i>-->
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">BM</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.BillingManagement') }}
                                            <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                                        </span>
                                    </a>
                                    <div class="collapse" id="BillingSManagement">
                                        <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:15px !important' : 'padding-left:15px !important'">
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">BL</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Billing') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">BR</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.BillingReports') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">BS</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.BillingSetup') }}
                                                    </span>

                                                </a>
                                            </li>

                                        </ul>
                                    </div>
                                </li>


                                <li v-if="isValid('ProposalManagement')">
                                    <a data-toggle="collapse" href="#ProposalSManagement">
                                        <!--<i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/warehouse.svg" /></i>-->
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">PM</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.ProposalManagement') }}
                                            <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                                        </span>
                                    </a>
                                    <div class="collapse" id="ProposalSManagement">
                                        <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:15px !important' : 'padding-left:15px !important'">
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CP</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.CommercialProposal') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CPR</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.ProposalReports') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CPS</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.ProposalSetup') }}
                                                    </span>

                                                </a>
                                            </li>

                                        </ul>
                                    </div>
                                </li>



                                <li v-if="(isValid('InquiryManagement') && (isValid('CanViewInquiryProcess') || isValid('CanViewInquirySetup') || isValid('CanViewInquiryType') || isValid('CanViewInquiry') || isValid('CanAddInquiry') ))">
                                    <a data-toggle="collapse" href="#InquirySManagement" v-bind:aria-expanded="inquiryMenu? true : false">

                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">INQ</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.InquiryManagement') }}
                                            <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                                        </span>
                                    </a>
                                    <div class="collapse" id="InquirySManagement">
                                        <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:15px !important' : 'padding-left:15px !important'">

                                            <li v-if="(isValid('CanViewInquiry') )">
                                                <router-link :to="{path: '/Inquiry', query: { token_name:'Inquiry Management_token', fromDashboard:'true' } }" v-bind:class="propValvue=='Inquiry' ? 'router-link-exact-active router-link-active' : ''">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">INQ</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Inquiry') }}
                                                    </span>
                                                </router-link>
                                            </li>
                                            <li v-else-if="(isValid('CanAddInquiry'))">
                                                <router-link :to="{path: '/AddInquiry', query: { token_name:'Inquiry Management_token', fromDashboard:'true' } }" v-bind:class="propValvue=='Inquiry' ? 'router-link-exact-active router-link-active' : ''">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">INQ</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Inquiry') }}
                                                    </span>
                                                </router-link>
                                            </li>

                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SR</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.InquiryReports') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li v-if="(isValid('CanViewInquiryProcess') || isValid('CanViewInquirySetup') || isValid('CanViewInquiryType'))">
                                                <router-link :to="{path: '/InquirySetup', query: { token_name:'Inquiry Management_token', fromDashboard:'true' } }" v-bind:class="propValvue=='InquirySetup'? 'router-link-exact-active router-link-active' : ''">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">IS</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.InquirySetup') }}
                                                    </span>
                                                </router-link>
                                            </li>


                                        </ul>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </li>


                    <li v-if="isValid('CanViewPromotionOffer') || isValid('PromotionOfferSetup') || isValid('PromotionOfferReport')">
                        <a data-toggle="collapse" href="#promotionManagement" v-bind:aria-expanded="promotionMenu? true : false" v-if=" isValid('CanViewPromotionOffer')|| isValid('CanViewBundleOffer') ">
                            <i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/Inventory.svg" /></i>
                            <p class="menu_txt" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                {{ $t('Dashboard.OffersPromotions') }}
                                <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                            </p>
                        </a>
                        <div class="collapse" v-bind:class="promotionMenu ? 'show' : ''" id="promotionManagement">
                            <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:0px !important' : ''">

                                <li v-if="isValid('CanViewPromotionOffer')">
                                    <router-link :to="{path: '/promotion', query: {formName: 'StockOut', token_name:'Product And Inventory Management_token', fromDashboard:'true' } }" v-bind:class="propValvue=='Promotion' || propValvue=='AddPromotion'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">PO</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.OfferPromotion') }}
                                        </span>
                                    </router-link>
                                </li>
                                <li v-if="isValid('PromotionOfferReport')">
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">OPR</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.OfferPromotionReports') }}
                                        </span>

                                    </a>
                                </li>
                                <li v-if="isValid('PromotionOfferSetup')">
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">OPS</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.OfferPromotionSetup') }}
                                        </span>

                                    </a>
                                </li>

                            </ul>
                        </div>
                    </li>


                    <li>
                        <a data-toggle="collapse" href="#membership" v-bind:aria-expanded="loylityMembership? true : false" v-if=" isValid('CanViewPromotionOffer')|| isValid('CanViewBundleOffer') ">
                            <i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/Inventory.svg" /></i>
                            <p class="menu_txt" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                {{ $t('Dashboard.LoylityMemberships') }}
                                <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                            </p>
                        </a>
                        <div class="collapse" v-bind:class="loylityMembership ? 'show' : ''" id="membership">
                            <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:0px !important' : ''">

                                <li>
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">OPS</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.LoylityMembership') }}
                                        </span>

                                    </a>
                                </li>
                                <li>
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">OPR</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Reports') }}
                                        </span>

                                    </a>
                                </li>
                                <li>
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">OPS</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Setup') }}
                                        </span>

                                    </a>
                                </li>

                            </ul>
                        </div>
                    </li>

                    <li v-if="isValid('QuickItemRegistration') ||isValid('InventoryReports') ||isValid('PriceManagement') ||isValid('InventoryCountManagement') ||isValid('CanViewItem') || isValid('CanAddItem') || isValid('CanViewInventoryCount') || isValid('CanEditInventoryCount')|| isValid('CanAddInventoryCount')|| isValid('CanViewPromotionOffer')|| isValid('CanViewBundleOffer') || isValid('CanViewProduct')|| isValid('CanViewCategory')|| isValid('CanViewSubCategory')|| isValid('CanViewBrand')|| isValid('CanViewOrigin')|| isValid('CanViewSize')|| isValid('CanViewColor')|| isValid('CanViewUnit')|| isValid('CanViewWarrantyType')">
                        <a data-toggle="collapse" href="#inventory " v-bind:aria-expanded="inventoryMenu? true : false">
                            <i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/Inventory.svg" /></i>
                            <p class="menu_txt" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                {{ $t('Dashboard.Inventory') }}
                                <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                            </p>
                        </a>
                        <div class="collapse" v-bind:class="inventoryMenu ? 'show' : ''" id="inventory">
                            <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:0px !important' : ''">

                                <li v-if="isValid('QuickItemRegistration')">
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">QIR</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.QuickItemRegistration') }}
                                        </span>

                                    </a>
                                </li>
                                <li v-if="isValid('CanViewProduct')">
                                    <router-link :to="{path: '/ProductMaster', query: { token_name:'Product And Inventory Management_token', fromDashboard:'true' } }" v-bind:class="propValvue=='Product' || propValvue=='AddProduct'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">IT</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.ProductRegistration') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li v-if="isValid('CanViewItem')">
                                    <router-link :to="{path: '/products', query: { token_name:'Product And Inventory Management_token', fromDashboard:'true' } }" v-bind:class="propValvue=='Product' || propValvue=='AddProduct'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">IT</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.ItemRegistration') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li v-else-if="isValid('CanAddItem')">
                                    <router-link :to="{path: '/addproduct', query: { token_name:'Product And Inventory Management_token', fromDashboard:'true' } }" v-bind:class="propValvue=='Product' || propValvue=='AddProduct'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">IT</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.ItemRegistration') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li v-if="isValid('CanViewInventoryCount') || isValid('CanEditInventoryCount')">
                                    <router-link :to="{path: '/InventoryBlindList', query: { token_name:'Product And Inventory Management_token', fromDashboard:'true' } }" v-bind:class="propValvue=='InventoryBlindList' || propValvue=='InventoryBlind'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">IC</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.InventoryCount') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li v-else-if="isValid('CanAddInventoryCount')">
                                    <router-link :to="{path: '/InventoryBlind', query: { token_name:'Product And Inventory Management_token', fromDashboard:'true' } }" v-bind:class="propValvue=='InventoryBlindList' || propValvue=='InventoryBlind'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">IC</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.InventoryCount') }}
                                        </span>

                                    </router-link>
                                </li>

                                <li v-if="isValid('InventoryReports')">
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">IR</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.InventoryReports') }}
                                        </span>

                                    </a>
                                </li>
                                <li v-if="isValid('CanViewProduct')|| isValid('CanViewCategory')|| isValid('CanViewSubCategory')|| isValid('CanViewBrand')|| isValid('CanViewOrigin')|| isValid('CanViewSize')|| isValid('CanViewColor')|| isValid('CanViewUnit')|| isValid('CanViewWarrantyType')">
                                    <router-link :to="{path: '/ProductManagement', query: { token_name:'Product And Inventory Management_token', fromDashboard:'true' } }" v-bind:class="propValvue=='ProductManagement' || propValvue=='ProductMaster' || propValvue=='Category' || propValvue=='SubCategory' || propValvue=='Brand' || propValvue=='Origin' || propValvue=='Size' || propValvue=='Color' || propValvue=='Unit'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">PM</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.ItemSetup') }}
                                        </span>
                                    </router-link>
                                </li>


                                <li v-if="isValid('PriceManagement')">
                                    <a data-toggle="collapse" href="#PriceManagement ">
                                        <!--<i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/warehouse.svg" /></i>-->
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">PM</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.PriceManagement') }}
                                            <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                                        </span>
                                    </a>
                                    <div class="collapse" id="PriceManagement">
                                        <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:15px !important' : 'padding-left:15px !important'">
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">PR</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Price') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">PRR</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.PriceReports') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">PS</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.PriceSetup') }}
                                                    </span>

                                                </a>
                                            </li>

                                        </ul>
                                    </div>
                                </li>
                                <li v-if="isValid('InventoryCountManagement')">
                                    <a data-toggle="collapse" href="#InventoryCountManagement ">
                                        <!--<i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/warehouse.svg" /></i>-->
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">ICM</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.InventoryCount') }}
                                            <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                                        </span>
                                    </a>
                                    <div class="collapse" id="InventoryCountManagement">
                                        <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:15px !important' : 'padding-left:15px !important'">
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">IC</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.InventoryCount') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">ICR</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Reports') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">ICS</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Setup') }}
                                                    </span>

                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </li>

                    <li v-if="isValid('WarehouseGatePass') ||isValid('WareHouseReport') ||isValid('CanViewWareHouse') || isValid('CanAddWareHouse') || isValid('CanViewStockIn') || isValid('CanDraftStockIn')  || isValid('CanAddStockIn') || isValid('CanViewStockOut')|| isValid('CanAddStockOut')|| isValid('CanDraftStockOut') || isValid('CanViewStockTransfer')|| isValid('CanDraftStockTransfer')|| isValid('CanAddStockTransfer')">
                        <a data-toggle="collapse" href="#WarehouseManagement " v-bind:aria-expanded="wareHouseMenu? true : false">
                            <i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/warehouse.svg" /></i>
                            <p class="menu_txt" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                {{ $t('Dashboard.Warehouse') }}
                                <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                            </p>
                        </a>
                        <div class="collapse" v-bind:class="wareHouseMenu ? 'show' : ''" id="WarehouseManagement">
                            <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:0px !important' : ''">
                                <li v-if="isValid('CanViewWareHouse')">
                                    <router-link :to="{path: '/warehouse', query: { token_name:'WareHouse Management_token', fromDashboard:'true' } }" v-bind:class="propValvue=='AddWarehouse' || propValvue=='Warehouse'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">PS</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Warehouses') }}
                                            <!-- <router-link :to="'/stockValue'">Add Stock Values</router-link> -->
                                        </span>

                                    </router-link>
                                </li>
                                <li v-else-if="isValid('CanAddWareHouse')">
                                    <router-link :to="{path: '/AddWarehouse', query: { token_name:'WareHouse Management_token', fromDashboard:'true' } }" v-bind:class="propValvue=='AddWarehouse' || propValvue=='Warehouse'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">PS</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Warehouses') }}
                                            <!-- <router-link :to="'/stockValue'">Add Stock Values</router-link> -->
                                        </span>

                                    </router-link>
                                </li>
                                <li v-if="isValid('CanViewStockTransfer') || isValid('CanDraftStockTransfer')">
                                    <router-link :to="{path: '/WareHouseTransfer', query: { token_name:'WareHouse Management_token', fromDashboard:'true' } }" v-bind:class="propValvue=='WareHouseTransfer' || propValvue=='AddWareHouseTransfer'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">ST</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.InventoryTransfer') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li v-else-if="isValid('CanAddStockTransfer')">
                                    <router-link :to="{path: '/addwareHouseTransfer', query: { token_name:'WareHouse Management_token', fromDashboard:'true' } }" v-bind:class="propValvue=='WareHouseTransfer' || propValvue=='AddWareHouseTransfer'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">ST</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.InventoryTransfer') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li v-if="isValid('CanViewStockIn') || isValid('CanDraftStockIn')">
                                    <router-link :to="{path: '/stockValue', query: { formName: 'StockIn', token_name:'WareHouse Management_token', fromDashboard:'true' } }" v-bind:class="propValvue=='AddStockValueStockIn' || propValvue=='StockValueStockIn'? 'router-link-exact-active router-link-active' : ''">

                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SI</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.InventoryAdjustment+') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li v-else-if="isValid('CanAddStockIn') ">
                                    <router-link :to="{path: '/addStockValue', query: { formName: 'StockIn', token_name:'WareHouse Management_token', fromDashboard:'true' } }" v-bind:class="propValvue=='AddStockValueStockIn' || propValvue=='StockValueStockIn'? 'router-link-exact-active router-link-active' : ''">

                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SI</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.InventoryAdjustment+') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li v-if="isValid('CanDraftStockOut') || isValid('CanViewStockOut')">
                                    <router-link :to="{path: '/stockValue', query: { formName: 'StockOut', token_name:'WareHouse Management_token', fromDashboard:'true' } }" v-bind:class="propValvue=='StockValueStockOut' || propValvue=='AddStockValueStockOut'? 'router-link-exact-active router-link-active' : ''">

                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SO</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.InventoryAdjustment-') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li v-else-if="isValid('CanAddStockOut')">
                                    <router-link :to="{path: '/addStockValue', query: { formName: 'StockOut', token_name:'WareHouse Management_token', fromDashboard:'true' } }" v-bind:class="propValvue=='StockValueStockOut' || propValvue=='AddStockValueStockOut'? 'router-link-exact-active router-link-active' : ''">

                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SO</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.InventoryAdjustment-') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li v-if="isValid('WarehouseGatePass')">
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">GP</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.GatePass') }}
                                        </span>

                                    </a>
                                </li>
                                <li v-if="isValid('WareHouseReport')">
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">WR</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Reports') }}
                                        </span>

                                    </a>
                                </li>
                            </ul>
                        </div>
                    </li>

                    <li v-if="isValid('PurchaseBillingManagement') ||isValid('PurchaseDeliveryManagement') ||isValid('PurchaseReport') ||isValid('GoodReceives') ||isValid('CanViewPurchaseDraft') || isValid('CanViewPurchasePost') || isValid('CanAddPurchaseInvoice') || isValid('CanViewDraftOrder') || isValid('CanViewPostOrder') || isValid('CanAddPurchaseOrder')|| isValid('CanAllowOrderVersion') || isValid('CanAddPurchaseReturn')|| isValid('CanViewPurchaseReturn')|| isValid('CanViewSupplier')|| isValid('CanAddSupplier')|| isValid('CanViewSPR')|| isValid('CanAddSPR')|| isValid('CanDraftSPR')|| isValid('CanDraftExpenseBill')|| isValid('CanViewExpenseBill')|| isValid('CanAddExpenseBill')|| isValid('CanViewAutoTemplate')|| isValid('CanViewGoodsReceiveasDraft') || isValid('CanViewGoodsReceiveasPost')">
                        <a data-toggle="collapse" href="#Purchase" v-bind:aria-expanded="purchaseMenu? true : false">
                            <i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''">   <img class="ImageWidth" src="Noble menu icons/Purchases.svg" /></i>
                            <p class="menu_txt" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                {{ $t('Dashboard.Purchase') }}
                                <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                            </p>
                        </a>
                        <div class="collapse" v-bind:class="purchaseMenu ? 'show' : ''" id="Purchase">
                            <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:0px !important' : ''">
                                <li v-if="isValid('CanViewPurchaseDraft') || isValid('CanViewPurchasePost')">

                                    <router-link :to="{path: '/purchase', query: { token_name:'Purchase_token', fromDashboard:'true' } }" v-bind:class="propValvue=='purchase' || propValvue=='Addpurchase'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">PI</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.PurchaseInvoice') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li v-else-if="isValid('CanAddPurchaseInvoice')">

                                    <router-link :to="{path: '/addpurchase', query: { token_name:'Purchase_token', fromDashboard:'true' } }" v-bind:class="propValvue=='purchase' || propValvue=='Addpurchase'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">PI</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.PurchaseInvoice') }}
                                        </span>

                                    </router-link>
                                </li>

                                <li v-if="isValid('CanViewPurchaseReturn') ">
                                    <router-link :to="{path: '/PurchaseReturn', query: { token_name:'Purchase_token', fromDashboard:'true' } }" v-bind:class="propValvue=='PurchaseReturn' || propValvue=='AddPurchaseReturn'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">PR</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.PurchaseReturn') }}
                                        </span>
                                    </router-link>
                                </li>
                                <li v-else-if="isValid('CanAddPurchaseReturn') ">
                                    <router-link :to="{path: '/addPurchaseReturn', query: { token_name:'Purchase_token', fromDashboard:'true' } }" v-bind:class="propValvue=='PurchaseReturn' || propValvue=='AddPurchaseReturn'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">PR</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.PurchaseReturn') }}
                                        </span>
                                    </router-link>
                                </li>
                                <li v-if="isValid('CanViewDraftOrder') || isValid('CanViewPostOrder')|| (isValid('CanViewInProcessOrder') && isValid('CanAllowOrderVersion'))">
                                    <router-link :to="{path: '/purchaseorder', query: { token_name:'Purchase_token', fromDashboard:'true' } }" v-bind:class="propValvue=='Addpurchaseorder' || propValvue=='purchaseorder'? 'router-link-exact-active router-link-active' : ''">

                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">PO</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.PurchaseOrder') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li v-else-if="isValid('CanAddPurchaseOrder')">
                                    <router-link :to="{path: '/addpurchaseorder', query: { token_name:'Purchase_token', fromDashboard:'true' } }" v-bind:class="propValvue=='Addpurchaseorder' || propValvue=='purchaseorder'? 'router-link-exact-active router-link-active' : ''">

                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">PO</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.PurchaseOrder') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li v-if="isValid('CanViewSupplier')">
                                    <router-link :to="{path: '/supplier', query: { token_name:'Purchase_token', fromDashboard:'true' } }" v-bind:class="propValvue=='AddSupplier' || propValvue=='Supplier'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SU</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Suppliers') }}
                                        </span>
                                    </router-link>
                                </li>
                                <li v-else-if="isValid('CanAddSupplier')">
                                    <router-link :to="{path: '/addsupplier', query: { token_name:'Purchase_token', fromDashboard:'true' } }" v-bind:class="propValvue=='AddSupplier' || propValvue=='Supplier'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SU</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Suppliers') }}
                                        </span>
                                    </router-link>
                                </li>

                                <li v-if="isValid('CanViewAutoTemplate')">
                                    <router-link :to="{path: '/autoPurchaseTemplate', query: { token_name:'Purchase_token', fromDashboard:'true' } }" v-bind:class="propValvue=='autoPurchaseTemplate' || propValvue=='AddAutoPurchaseTemplate'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">AP</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.AutoPurchaseTemplates') }}
                                        </span>
                                    </router-link>
                                </li>
                                <li v-if="isValid('CanViewGoodsReceiveasDraft') || isValid('CanViewGoodsReceiveasPost')">
                                    <router-link :to="{path: '/GoodReceive', query: { token_name:'Purchase_token', fromDashboard:'true' } }" v-bind:class="propValvue=='AddGoodReceive' || propValvue=='goodReceives'|| propValvue=='GoodReceive'? 'router-link-exact-active router-link-active' : ''">

                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">GRN</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.GoodsReceive') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li v-if=" isValid('CanDraftSPR')|| isValid('CanViewSPR')">
                                    <router-link :to="{path: '/paymentVoucherList', query: {formName: 'BankPay', token_name:'Purchase_token', fromDashboard:'true' } }" v-bind:class="propValvue=='PaymentVoucherListBankPay' || propValvue=='addPaymentVoucherBankPay'? 'router-link-exact-active router-link-active' : ''">

                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SP</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.SupplierPaymentReceipt') }}

                                        </span>

                                    </router-link>
                                </li>
                                <li v-else-if=" isValid('CanAddSPR')">
                                    <router-link :to="{path: '/addPaymentVoucher', query: {formName: 'BankPay', token_name:'Purchase_token', fromDashboard:'true' } }" v-bind:class="propValvue=='PaymentVoucherListBankPay' || propValvue=='addPaymentVoucherBankPay'? 'router-link-exact-active router-link-active' : ''">

                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SP</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.SupplierPaymentReceipt') }}
                                        </span>

                                    </router-link>
                                </li>






                                <li v-if="isValid('PurchaseReport') ">
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">PR</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Reports') }}
                                        </span>

                                    </a>
                                </li>


                                <li v-if="isValid('PurchaseDeliveryManagement') ">
                                    <a data-toggle="collapse" href="#DeliveryPManagement">
                                        <!--<i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/warehouse.svg" /></i>-->
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">DM</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.DeliveryManagement') }}
                                            <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                                        </span>
                                    </a>
                                    <div class="collapse" id="DeliveryPManagement">
                                        <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:15px !important' : 'padding-left:15px !important'">
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">DL</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Delivery') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">GP</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.GatePass') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">DR</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Reports') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">DS</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Setup') }}
                                                    </span>

                                                </a>
                                            </li>

                                        </ul>
                                    </div>
                                </li>


                                <li v-if="isValid('PurchaseBillingManagement') ">
                                    <a data-toggle="collapse" href="#BillingPManagement">
                                        <!--<i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/warehouse.svg" /></i>-->
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">BM</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.BillingManagement') }}
                                            <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                                        </span>
                                    </a>
                                    <div class="collapse" id="BillingPManagement">
                                        <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:15px !important' : 'padding-left:15px !important'">

                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">BL</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Billing') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">BR</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Reports') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">BS</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Setup') }}
                                                    </span>

                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                </li>


                            </ul>
                        </div>
                    </li>


                    <li v-if="isValid('CanViewReparingOrder') || isValid('CanViewWarrantyCategory') || isValid('CanViewDescription') || isValid('CanViewProblem') || isValid('CanViewAccessory')">
                        <a data-toggle="collapse" href="#RepairingOrder" v-bind:aria-expanded="contractMenu? true : false">
                            <i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/HR.svg" /></i>
                            <p class="menu_txt" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                {{ $t('Dashboard.RepairingOrder') }}
                                <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                            </p>
                        </a>

                        <div class="collapse" id="RepairingOrder">
                            <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:15px !important' : 'padding-left:15px !important'">
                                <li v-if="isValid('CanViewReparingOrder')">
                                    <router-link :to="{path: '/ReparingOrder', query: { token_name:'Purchase_token', fromDashboard:'true' } }" v-bind:class="propValvue=='ReparingOrder' || propValvue=='AddReparingOrder'? 'router-link-exact-active router-link-active' : ''">

                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">RO</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.RepairingOrder') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li>
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SR</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Reports') }}
                                        </span>

                                    </a>
                                </li>
                                <li v-if="isValid('CanViewWarrantyCategory') || isValid('CanViewDescription') || isValid('CanViewProblem') || isValid('CanViewAccessory')">
                                    <router-link :to="{path: '/ReparingOrderSetup', query: { token_name:'Purchase_token', fromDashboard:'true' } }" v-bind:class="propValvue=='ImportExportSetup'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">RO</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Setup') }}
                                        </span>
                                    </router-link>
                                </li>
                            </ul>
                        </div>
                    </li>

                    <li v-if="isValid('CashManagement')||isValid('AccountGatePass')||isValid('AccountReport')||isValid('CashierManagement')||isValid('RecurringInvoices')||isValid('RecurringPayments')||isValid('CanViewCheque')|| isValid('CanViewJV') || isValid('CanAddJV') || isValid('CanAddOC') || isValid('CanViewOC')|| isValid('CanViewPettyCash')|| isValid('CanAddPettyCash')|| isValid('CanDraftJV')|| isValid('CanDraftOC')|| isValid('CanDraftPettyCash')|| isValid('CanViewTCRequest')|| isValid('CanDraftTCRequest')|| isValid('CanAddTCRequest')|| isValid('CanViewTCIssue')|| isValid('CanDraftTCIssue')|| isValid('CanAddTCIssue')|| isValid('CanViewTCAllocation')|| isValid('CanDraftTCAllocation')|| isValid('CanAddTCAllocation') || isValid('CanViewCurrency') || isValid('CanViewBank')|| isValid('CanAddMonthlyCost')|| isValid('CanViewCOA')|| isValid('CanViewPaymentOption') || isValid('CanViewDenomination') || isValid('CanViewVatRate') || isValid('CanViewExpenseType') || isValid('CanViewFinancialYear') ">
                        <a data-toggle="collapse" href="#AccountManagement" v-bind:aria-expanded="accountMenu? true : false">
                            <i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/accounts.svg" /></i>
                            <p class="menu_txt" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                {{ $t('Dashboard.Accounts') }}
                                <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                            </p>
                        </a>
                        <div class="collapse" v-bind:class="accountMenu ? 'show' : ''" id="AccountManagement">
                            <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:0px !important' : ''">
                                <li v-if="isValid('CanDraftExpenseBill') || isValid('CanViewExpenseBill')">
                                    <router-link :to="{path: '/PurchaseBill', query: { token_name:'Expenses_token', fromDashboard:'true' } }" v-bind:class="propValvue=='AddPurchaseBill' || propValvue=='AddPurchaseBill'? 'router-link-exact-active router-link-active' : ''">

                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">BL</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.ExpenseBills') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li v-else-if="isValid('CanAddExpenseBill')">
                                    <router-link :to="{path: '/AddPurchaseBill', query: { token_name:'Expenses_token', fromDashboard:'true' } }" v-bind:class="propValvue=='AddPurchaseBill' || propValvue=='AddPurchaseBill'? 'router-link-exact-active router-link-active' : ''">

                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">BL</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.ExpenseBills') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li v-if=" isValid('CanViewJV') || isValid('CanDraftJV')">
                                    <router-link :to="{path: '/journalvoucherview', query: { formName: 'JournalVoucher', token_name:'Accounting_token', fromDashboard:'true' } }" v-bind:class="propValvue=='JournalVoucherViewJournalVoucher' || propValvue=='AddJournalVoucherJournalVoucher'? 'router-link-exact-active router-link-active' : ''">

                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">JV</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.JournalVoucher') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li v-else-if="isValid('CanAddJV')">
                                    <router-link :to="{path: '/addjournalvoucher', query: { formName: 'JournalVoucher', token_name:'Accounting_token', fromDashboard:'true' } }" v-bind:class="propValvue=='JournalVoucherViewJournalVoucher' || propValvue=='AddJournalVoucherJournalVoucher'? 'router-link-exact-active router-link-active' : ''">

                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">JV</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.JournalVoucher') }}
                                        </span>

                                    </router-link>
                                </li>


                                <li v-if="isValid('CanViewOC') || isValid('CanDraftOC')">
                                    <router-link :to="{path: '/journalvoucherview', query: {formName: 'OpeningCash', token_name:'Accounting_token', fromDashboard:'true' } }" v-bind:class="propValvue=='AddJournalVoucherOpeningCash' || propValvue=='JournalVoucherViewOpeningCash'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">OC</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.OpeningCash') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li v-else-if="isValid('CanAddOC')">
                                    <router-link :to="{path: '/addjournalvoucher', query: { formName: 'OpeningCash', token_name:'Accounting_token', fromDashboard:'true' } }" v-bind:class="propValvue=='AddJournalVoucherOpeningCash' || propValvue=='JournalVoucherViewOpeningCash'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">OC</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.OpeningCash') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li v-if="isValid('CashManagement')">
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon"> CM </span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.CashManagement') }}
                                        </span>

                                    </a>
                                </li>
                                <li v-if="isValid('CanViewCOA')">
                                    <router-link :to="{path: '/coa', query: { token_name:'Accounting_token', fromDashboard:'true' } }" v-bind:class="propValvue=='coa'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">COA</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.ChartofAccount-COA') }}
                                        </span>
                                    </router-link>
                                </li>
                                <li v-if="isValid('AccountGatePass')">
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon"> GP </span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.GatePass') }}
                                        </span>

                                    </a>
                                </li>

                                <li v-if="isValid('AccountReport')">
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon"> AR </span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Reports') }}
                                        </span>

                                    </a>
                                </li>
                                <li v-if="isValid('CanViewCurrency') || isValid('CanViewBank')|| isValid('CanAddMonthlyCost')|| isValid('CanViewCOA')|| isValid('CanViewPaymentOption') || isValid('CanViewDenomination') || isValid('CanViewVatRate') || isValid('CanViewExpenseType') || isValid('CanViewFinancialYear') ">
                                    <router-link :to="{path: '/FinancialSetup', query: { token_name:'Accounting_token', fromDashboard:'true' } }" v-bind:class="propValvue=='FinancialSetup' || propValvue=='Currency' || propValvue=='Bank' || propValvue=='AddBank' || propValvue=='MonthlyCost' || propValvue=='PaymentOptions' || propValvue=='DenominationSetup' || propValvue=='TaxRate' || propValvue=='ExpenseType'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">FS</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Setup') }}
                                        </span>
                                    </router-link>
                                </li>



                                <li v-if="isValid('CanAddPettyCash') || isValid('CanViewPettyCash') || isValid('CanDraftPettyCash') || isValid('CanAddTCAllocation') || isValid('CanViewTCAllocation') || isValid('CanDraftTCAllocation') || isValid('CanAddTCIssue') || isValid('CanViewTCIssue') || isValid('CanDraftTCIssue') || isValid('CanViewTCRequest') || isValid('CanDraftTCRequest') || isValid('CanAddTCRequest')">
                                    <a data-toggle="collapse" href="#TemporaryCash">
                                        <!--<i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/warehouse.svg" /></i>-->
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">TC</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.TemporaryPettyCash') }}
                                            <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                                        </span>
                                    </a>
                                    <div class="collapse" id="TemporaryCash">
                                        <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:15px !important' : 'padding-left:15px !important'">
                                            <li v-if="isValid('CanViewTCRequest') || isValid('CanDraftTCRequest')">
                                                <router-link :to="{path: '/TemporaryCashRequest', query: { token_name:'Accounting_token', fromDashboard:'true' } }" v-bind:class="propValvue=='TemporaryCashRequest' || propValvue=='AddTemporaryCashRequest'? 'router-link-exact-active router-link-active' : ''">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SU</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.IssuanceRequest') }}
                                                    </span>
                                                </router-link>
                                            </li>
                                            <li v-else-if="isValid('CanAddTCRequest')">
                                                <router-link :to="{path: '/TemporaryCashRequest', query: { token_name:'Accounting_token', fromDashboard:'true' } }" v-bind:class="propValvue=='TemporaryCashRequest' || propValvue=='AddTemporaryCashRequest'? 'router-link-exact-active router-link-active' : ''">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SU</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.IssuanceRequest') }}
                                                    </span>
                                                </router-link>
                                            </li>
                                            <li v-if="isValid('CanViewTCIssue') || isValid('CanDraftTCIssue')">
                                                <router-link :to="{path: '/TemporaryCashIssue', query: { token_name:'Accounting_token', fromDashboard:'true' } }" v-bind:class="propValvue=='TemporaryCashIssue' || propValvue=='AddTemporaryCashIssue'? 'router-link-exact-active router-link-active' : ''">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SU</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.CashIssueance') }}
                                                    </span>
                                                </router-link>
                                            </li>
                                            <li v-else-if="isValid('CanAddTCIssue')">
                                                <router-link :to="{path: '/AddTemporaryCashIssue', query: { token_name:'Accounting_token', fromDashboard:'true' } }" v-bind:class="propValvue=='TemporaryCashIssue' || propValvue=='AddTemporaryCashIssue'? 'router-link-exact-active router-link-active' : ''">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SU</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.CashIssueance') }}
                                                    </span>
                                                </router-link>
                                            </li>
                                            <li v-if="isValid('CanViewTCAllocation') || isValid('CanDraftTCAllocation')">
                                                <router-link :to="{path: '/TemporaryCashAllocation', query: { token_name:'Accounting_token', fromDashboard:'true' } }" v-bind:class="propValvue=='TemporaryCashAllocation' || propValvue=='AddTemporaryCashAllocation'? 'router-link-exact-active router-link-active' : ''">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SU</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Allocation') }}
                                                    </span>
                                                </router-link>
                                            </li>
                                            <li v-else-if="isValid('CanAddTCAllocation')">
                                                <router-link :to="{path: '/AddTemporaryCashAllocation', query: { token_name:'Accounting_token', fromDashboard:'true' } }" v-bind:class="propValvue=='TemporaryCashAllocation' || propValvue=='AddTemporaryCashAllocation'? 'router-link-exact-active router-link-active' : ''">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SU</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Allocation') }}
                                                    </span>
                                                </router-link>
                                            </li>
                                            <li v-if="isValid('CanViewPettyCash') || isValid('CanDraftPettyCash')">
                                                <router-link :to="{path: '/paymentVoucherList', query: { formName: 'PettyCash' , token_name:'Accounting_token', fromDashboard:'true' } }" v-bind:class="propValvue=='PaymentVoucherListPettyCash' || propValvue=='addPaymentVoucherPettyCash'? 'router-link-exact-active router-link-active' : ''">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">PC</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.PettyCash') }}
                                                    </span>

                                                </router-link>
                                            </li>
                                            <li v-else-if="isValid('CanAddPettyCash')">
                                                <router-link :to="{path: '/addPaymentVoucher', query: { formName: 'PettyCash', token_name:'Accounting_token', fromDashboard:'true' } }" v-bind:class="propValvue=='PaymentVoucherListPettyCash' || propValvue=='addPaymentVoucherPettyCash'? 'router-link-exact-active router-link-active' : ''">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">PC</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.PettyCash') }}
                                                    </span>

                                                </router-link>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">TCR</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Reports') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">TCR</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Setup') }}
                                                    </span>

                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                </li>


                                <li v-if="isValid('CashierManagement')">
                                    <a data-toggle="collapse" href="#CashierManagement">
                                        <!--<i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/warehouse.svg" /></i>-->
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CM</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.CashierManagement') }}
                                            <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                                        </span>
                                    </a>
                                    <div class="collapse" id="CashierManagement">
                                        <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:15px !important' : 'padding-left:15px !important'">
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CA</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Cashier') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">GP</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.GatePass') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CR</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.CashierReports') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CS</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.CashierSetup') }}
                                                    </span>

                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                </li>


                                <li v-if="isValid('RecurringInvoices')">
                                    <a data-toggle="collapse" href="#RecurringManagement">
                                        <!--<i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/warehouse.svg" /></i>-->
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">RI</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.RecurringInvoices') }}
                                            <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                                        </span>
                                    </a>
                                    <div class="collapse" id="RecurringManagement">
                                        <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:15px !important' : 'padding-left:15px !important'">
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">RI</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.RecurringInvoice') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">RIR</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Reports') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">RIS</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Setup') }}
                                                    </span>

                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                </li>


                                <li v-if="isValid('RecurringPayments')">
                                    <a data-toggle="collapse" href="#RecurringPaymentsManagement">
                                        <!--<i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/warehouse.svg" /></i>-->
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">RP</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.RecurringPayments') }}
                                            <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                                        </span>
                                    </a>
                                    <div class="collapse" id="RecurringPaymentsManagement">
                                        <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:15px !important' : 'padding-left:15px !important'">
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">RP</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.RecurringPayment') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">RPR</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Reports') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">RPS</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Setup') }}
                                                    </span>

                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                </li>



                                <li v-if="isValid('CanViewCheque') ">
                                    <a data-toggle="collapse" href="#ChequesAndGuarantee">
                                        <!--<i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/warehouse.svg" /></i>-->
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">RP</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Cheques&Guarantee') }}
                                            <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                                        </span>
                                    </a>
                                    <div class="collapse" id="ChequesAndGuarantee">
                                        <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:15px !important' : 'padding-left:15px !important'">
                                            <li v-if="isValid('CanViewCheque') ">
                                                <router-link :to="{path: '/ChequeAndGurantee', query: { token_name:'Accounting_token', fromDashboard:'true' } }" v-bind:class="propValvue=='FinancialSetup' || propValvue=='Currency' || propValvue=='Bank' || propValvue=='AddBank' || propValvue=='MonthlyCost' || propValvue=='PaymentOptions' || propValvue=='DenominationSetup' || propValvue=='TaxRate' || propValvue=='ExpenseType'? 'router-link-exact-active router-link-active' : ''">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CG</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.ChequesGurantee') }}
                                                    </span>
                                                </router-link>
                                            </li>

                                            <li v-if="isValid('CanViewCheque') ">
                                                <router-link :to="{path: '/ChequeAndGuranteeDashboard', query: { token_name:'Accounting_token', fromDashboard:'true' } }" v-bind:class="propValvue=='FinancialSetup' || propValvue=='Currency' || propValvue=='Bank' || propValvue=='AddBank' || propValvue=='MonthlyCost' || propValvue=='PaymentOptions' || propValvue=='DenominationSetup' || propValvue=='TaxRate' || propValvue=='ExpenseType'? 'router-link-exact-active router-link-active' : ''">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CGD</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Dashboard') }}
                                                    </span>
                                                </router-link>
                                            </li>

                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CGR</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Reports') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CGS</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Setup') }}
                                                    </span>

                                                </a>
                                            </li>

                                        </ul>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </li>


                  
                    <li v-if="isValid('HRSetup') ||isValid('HRReport') ||isValid('AttendanceManagement') ||isValid('EmployeePortal') ||isValid('EmployeeTasks') ||isValid('AssetsManagementI') ||isValid('DocumentManagement') ||isValid('CanViewRunPayroll') || isValid('CanAddRunPayroll') || isValid('CanDraftRunPayroll') || isValid('CanViewEmployeeReg') || isValid('CanViewAllowanceType') || isValid('CanViewAllowance') || isValid('CanViewDeduction') || isValid('CanViewContribution') || isValid('CanViewPayRollSchedule') || isValid('CanViewSaleryTemplate') || isValid('CanViewEmployeeSalary') || isValid('CanViewLoanPayment') || isValid('CanViewSalaryTaxSlab')">
                        <a data-toggle="collapse" href="#accountforms" v-bind:aria-expanded="humanMenu? true : false">
                            <i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/HR.svg" /></i>
                            <p class="menu_txt" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                {{ $t('Dashboard.HumanResources') }}
                                <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                            </p>
                        </a>
                        <div class="collapse" v-bind:class="humanMenu ? 'show' : ''" id="accountforms">
                            <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:0px !important' : ''">
                                <li v-if="isValid('CanViewEmployeeReg')">
                                    <router-link :to="{path: '/employeeRegistration', query: { token_name:'HR And PayRoll_token', fromDashboard:'true' } }" v-bind:class="propValvue=='EmployeeRegistration' || propValvue=='AddEmployeeRegistration'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">EP</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.EmployeeProfile') }}
                                        </span>
                                    </router-link>
                                </li>
                                <li v-else-if="isValid('CanAddEmployeeReg')">
                                    <router-link :to="{path: '/addEmployeeRegistration', query: { token_name:'HR And PayRoll_token', fromDashboard:'true' } }" v-bind:class="propValvue=='EmployeeRegistration' || propValvue=='AddEmployeeRegistration'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">EP</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.EmployeeProfile') }}
                                        </span>
                                    </router-link>
                                </li>

                                <li v-if="isValid('HRReport')">
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">HRR</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Reports') }}
                                        </span>

                                    </a>
                                </li>
                                <li v-if=" isValid('CanViewAllowanceType') || isValid('CanViewAllowance') || isValid('CanViewDeduction') || isValid('CanViewContribution') || isValid('CanViewPayRollSchedule') || isValid('CanViewSaleryTemplate') || isValid('CanViewEmployeeSalary') || isValid('CanViewLoanPayment') || isValid('CanViewSalaryTaxSlab')">
                                    <router-link :to="{path: '/HrSetup', query: { token_name:'HR And PayRoll_token', fromDashboard:'true' } }" v-bind:class="propValvue=='GeographicalSetup' || propValvue=='City' || propValvue=='Region'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">Hr</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Setup') }}
                                        </span>
                                    </router-link>
                                </li>



                                <li v-if="isValid('CanViewRunPayroll') || isValid('CanAddRunPayroll') || isValid('CanDraftRunPayroll') || isValid('CanViewAllowanceType') || isValid('CanViewAllowance') || isValid('CanViewDeduction') || isValid('CanViewContribution') || isValid('CanViewPayRollSchedule') || isValid('CanViewSaleryTemplate') || isValid('CanViewEmployeeSalary') || isValid('CanViewLoanPayment') || isValid('CanViewSalaryTaxSlab')">
                                    <a data-toggle="collapse" href="#EmployeePayroll">
                                        <!--<i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/warehouse.svg" /></i>-->
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">EP</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.PayrollManagement') }}
                                            <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                                        </span>
                                    </a>
                                    <div class="collapse" id="EmployeePayroll">
                                        <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:15px !important' : 'padding-left:15px !important'">
                                            <li v-if="isValid('CanViewRunPayroll')">
                                                <router-link :to="{path: '/RunPayroll', query: { token_name:'HR And PayRoll_token', fromDashboard:'true' } }">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">RP</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Payroll') }}
                                                    </span>

                                                </router-link>
                                            </li>
                                            <li v-else-if="isValid('CanAddRunPayroll') || isValid('CanDraftRunPayroll')">
                                                <router-link @click.native="AddSalaryTemplate" to="#">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">RP</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Payroll') }}
                                                    </span>

                                                </router-link>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">AS</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Payslip') }}
                                                    </span>

                                                </a>
                                            </li>

                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">AS</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Reports') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li v-if=" isValid('CanViewAllowanceType') || isValid('CanViewAllowance') || isValid('CanViewDeduction') || isValid('CanViewContribution') || isValid('CanViewPayRollSchedule') || isValid('CanViewSaleryTemplate') || isValid('CanViewEmployeeSalary') || isValid('CanViewLoanPayment') || isValid('CanViewSalaryTaxSlab')">
                                                <router-link :to="{path: '/HrSetup', query: { token_name:'HR And PayRoll_token', fromDashboard:'true' } }" v-bind:class="propValvue=='GeographicalSetup' || propValvue=='City' || propValvue=='Region'? 'router-link-exact-active router-link-active' : ''">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">HR</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Setup') }}
                                                    </span>
                                                </router-link>
                                            </li>
                                        </ul>
                                    </div>
                                </li>


                                <li v-if="isValid('AttendanceManagement')">
                                    <a data-toggle="collapse" href="#AttendanceManagement">
                                        <!--<i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/warehouse.svg" /></i>-->
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">AM</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.AttendanceManagement') }}
                                            <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                                        </span>
                                    </a>
                                    <div class="collapse" id="AttendanceManagement">
                                        <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:15px !important' : 'padding-left:15px !important'">
                                            <li v-if="isValid('CanViewManualAttendance')">
                                                <router-link :to="{path: '/ManualAttendance', query: { token_name:'HR And PayRoll_token', fromDashboard:'true' } }" v-bind:class="propValvue=='ManualAttendance' ? 'router-link-exact-active router-link-active' : ''">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">MA</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        Manual Attendece
                                                    </span>
                                                </router-link>
                                            </li>
                                            <li v-if="isValid('CanAddTodayAttendance')">
                                                <router-link :to="{path: '/EmployeeTodayAttendence', query: { token_name:'HR And PayRoll_token', fromDashboard:'true' } }" v-bind:class="propValvue=='EmployeeTodayAttendence' ? 'router-link-exact-active router-link-active' : ''">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">TA</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        Today Attendence
                                                    </span>
                                                </router-link>
                                            </li>

                                            <li v-if="isValid('CanViewAttendanceReport')">
                                                <router-link :to="{path: '/AttendanceReport', query: { token_name:'HR And PayRoll_token', fromDashboard:'true' } }" v-bind:class="propValvue=='AttendanceReport' ? 'router-link-exact-active router-link-active' : ''">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">AR</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Reports') }}
                                                    </span>
                                                </router-link>

                                            </li>
                                            
                                            <li v-if="isValid('CanAddHolidaySetup')">
                                                <router-link :to="{path: '/AddHolidayOfMonth', query: { token_name:'HR And PayRoll_token', fromDashboard:'true' } }" v-bind:class="propValvue=='AddHolidayOfMonth' ? 'router-link-exact-active router-link-active' : ''">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">EP</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Setup') }}
                                                    </span>
                                                </router-link>
                                            </li>
                                        </ul>
                                    </div>
                                </li>


                                <li v-if="isValid('EmployeePortal')">
                                    <a data-toggle="collapse" href="#EmployeePortal">
                                        <!--<i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/warehouse.svg" /></i>-->
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">EP</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.EmployeePortal') }}
                                            <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                                        </span>
                                    </a>
                                    <div class="collapse" id="EmployeePortal">
                                        <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:15px !important' : 'padding-left:15px !important'">
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">EP</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Portal') }}
                                                    </span>

                                                </a>
                                            </li>

                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">EPR</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Reports') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">EPS</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Setup') }}
                                                    </span>

                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                </li>


                                <li v-if="isValid('EmployeeTasks')">
                                    <a data-toggle="collapse" href="#EmployeeTasks">
                                        <!--<i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/warehouse.svg" /></i>-->
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">ETM</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.TasksManagement') }}
                                            <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                                        </span>
                                    </a>
                                    <div class="collapse" id="EmployeeTasks">
                                        <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:15px !important' : 'padding-left:15px !important'">
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">ET</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Tasks') }}
                                                    </span>

                                                </a>
                                            </li>

                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">ETR</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Reports') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">ETS</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Setup') }}
                                                    </span>

                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                </li>


                                <li v-if="isValid('AssetsManagementI')">
                                    <a data-toggle="collapse" href="#AssetsI">
                                        <!--<i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/warehouse.svg" /></i>-->
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">AMI</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.AssetsManagement-I') }}
                                            <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                                        </span>
                                    </a>
                                    <div class="collapse" id="AssetsI">
                                        <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:15px !important' : 'padding-left:15px !important'">
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">AM</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Assets') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">AR</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Reports') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">AS</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Setup') }}
                                                    </span>

                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                </li>



                                <li v-if="isValid('DocumentManagement')">
                                    <a data-toggle="collapse" href="#DocumentManagement">
                                        <!--<i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/warehouse.svg" /></i>-->
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">DM</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.DocumentManagement') }}
                                            <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                                        </span>
                                    </a>
                                    <div class="collapse" id="DocumentManagement">
                                        <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:15px !important' : 'padding-left:15px !important'">
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CD</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.CreateDocument') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">DR</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Reports') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">DS</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Setup') }}
                                                    </span>

                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </li>


                    <li v-if="isValid('NewModules')">
                        <a data-toggle="collapse" href="#FinancialManagement" v-bind:aria-expanded="financialMenu? true : false">
                            <i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/HR.svg" /></i>
                            <p class="menu_txt" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                {{ $t('Dashboard.FinancialManagement') }}
                                <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                            </p>
                        </a>
                        <div class="collapse" v-bind:class="financialMenu ? 'show' : ''" id="FinancialManagement">
                            <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:0px !important' : ''">

                                <li>
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">OB</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.OpeningBalances') }}
                                        </span>

                                    </a>
                                </li>
                                <!--<li>
        <a href="javascript:void(0)" v-on:click="commingSoonPage">
            <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">COA</span>
            <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                {{ $t('Dashboard.ChartofAccounts-COA') }}
            </span>

        </a>
    </li>-->
                                <li>
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CM</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.CashManagement') }}
                                        </span>

                                    </a>
                                </li>

                                <li>
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">FR</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Reports') }}
                                        </span>

                                    </a>
                                </li>
                                <li>
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">FS</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Setup') }}
                                        </span>

                                    </a>
                                </li>


                                <li>
                                    <a data-toggle="collapse" href="#AssetsII">
                                        <!--<i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/warehouse.svg" /></i>-->
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">AMI</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.AssetsManagement-II') }}
                                            <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                                        </span>
                                    </a>
                                    <div class="collapse" id="AssetsII">
                                        <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:15px !important' : 'padding-left:15px !important'">
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">AM</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Assets') }}
                                                    </span>

                                                </a>
                                            </li>

                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">AR</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Reports') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">AS</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Setup') }}
                                                    </span>

                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                </li>
                                <li>
                                    <a data-toggle="collapse" href="#BudgetManagement">
                                        <!--<i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/warehouse.svg" /></i>-->
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">BM</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.BudgetManagement') }}
                                            <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                                        </span>
                                    </a>
                                    <div class="collapse" id="BudgetManagement">
                                        <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:15px !important' : 'padding-left:15px !important'">
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">BD</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Budget') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">BR</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Reports') }}
                                                    </span>

                                                </a>
                                            </li>
                                            <li>
                                                <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                                    <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">BS</span>
                                                    <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                                        {{ $t('Dashboard.Setup') }}
                                                    </span>

                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                </li>
                                <li v-if="isValid('CanAddTodayAttendance')">
                                    <router-link :to="{path: '/EmployeeTodayAttendence', query: { token_name:'HR And PayRoll_token', fromDashboard:'true' } }" v-bind:class="propValvue=='ManualAttendance' ? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">EP</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                             Today Attendance
                                        </span>
                                    </router-link>
                                </li>
                            </ul>
                        </div>
                    </li>


                    <li v-if="isValid('CanViewTransporter') || isValid('CanAddTransporter')|| isValid('CanViewClearanceAgent')|| isValid('CanAddClearanceAgent')|| isValid('CanViewShippingLiner')|| isValid('CanAddShippingLiner')">
                        <a data-toggle="collapse" href="#logistics" v-bind:aria-expanded="logisticMenu? true : false">
                            <i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/Logistics.svg" /></i>
                            <p class="menu_txt" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                {{ $t('Dashboard.Logistics') }}
                                <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                            </p>
                        </a>
                        <div class="collapse" id="logistics">
                            <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:0px !important' : ''">
                                <li v-if="isValid('CanViewTransporter') ">
                                    <router-link :to="{path: '/LogisticsList', query: {formName: 'Transporter', token_name:'Logistic_token', fromDashboard:'true' } }" v-bind:class="propValvue=='LogisticsListTransporter' || propValvue=='AddLogisticsTransporter'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">TC</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.TransporterCargo') }}
                                        </span>
                                    </router-link>
                                </li>
                                <li v-else-if="isValid('CanAddTransporter')">
                                    <router-link :to="{path: '/AddLogistics', query: {formName: 'Transporter', token_name:'Logistic_token', fromDashboard:'true' } }" v-bind:class="propValvue=='LogisticsListTransporter' || propValvue=='AddLogisticsTransporter'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">TC</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.TransporterCargo') }}
                                        </span>
                                    </router-link>
                                </li>
                                <li v-if=" isValid('CanViewClearanceAgent')">
                                    <router-link :to="{path: '/LogisticsList', query: { formName: 'ClearanceAgent', token_name:'Logistic_token', fromDashboard:'true' } }" v-bind:class="propValvue=='LogisticsListClearanceAgent' || propValvue=='AddLogisticsClearanceAgent'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CA</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.ClearanceAgent') }}
                                        </span>
                                    </router-link>
                                </li>
                                <li v-else-if=" isValid('CanAddClearanceAgent')">
                                    <router-link :to="{path: '/AddLogistics', query: {  formName: 'ClearanceAgent', token_name:'Logistic_token', fromDashboard:'true' } }" v-bind:class="propValvue=='LogisticsListClearanceAgent' || propValvue=='AddLogisticsClearanceAgent'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CA</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.ClearanceAgent') }}
                                        </span>
                                    </router-link>
                                </li>
                                <li v-if="isValid('CanViewShippingLiner')">
                                    <router-link :to="{path: '/LogisticsList', query: {formName: 'ShippingLinear',  token_name:'Logistic_token', fromDashboard:'true' } }" v-bind:class="propValvue=='AddLogisticsShippingLinear' || propValvue=='LogisticsListShippingLinear'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SL</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">

                                            {{ $t('Dashboard.ShippingLiner') }}
                                        </span>
                                    </router-link>
                                </li>
                                <li v-else-if="isValid('CanAddShippingLiner')">
                                    <router-link :to="{path: '/AddLogistics', query: {formName: 'ShippingLinear', token_name:'Logistic_token', fromDashboard:'true' } }" v-bind:class="propValvue=='AddLogisticsShippingLinear' || propValvue=='LogisticsListShippingLinear'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SL</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.ShippingLiner') }}
                                        </span>
                                    </router-link>
                                </li>
                            </ul>
                        </div>
                    </li>


                    <li v-if="importExportSale">
                        <a data-toggle="collapse" href="#ImportExport" v-bind:aria-expanded="importExportMenu? true : false">
                            <i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/Logistics.svg" /></i>
                            <p class="menu_txt" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                {{ $t('Dashboard.ImportExport') }}
                                <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                            </p>
                        </a>
                        <div class="collapse" id="ImportExport">
                            <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:0px !important' : ''">
                                <li>
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">IE</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.ImportExport') }}
                                        </span>

                                    </a>
                                </li>

                                <li>
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">IER</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Reports') }}
                                        </span>

                                    </a>
                                </li>
                                <li v-if="importExportSale && (isValid('CanViewStuffingLocation') || isValid('CanViewPartOfLoading') || isValid('CanViewPartOfDestination') || isValid('CanViewOrderType') || isValid('CanViewService') || isValid('CanViewIncoterms') || isValid('CanViewCommodity') || isValid('CanViewCarrier') || isValid('CanViewExportExw') || isValid('CanViewImportFob') || isValid('CanViewQuantityContainer'))">
                                    <router-link :to="{path: '/ImportExportSetup', query: { token_name:'Sales_token', fromDashboard:'true' } }" v-bind:class="propValvue=='ImportExportSetup'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">IE</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Setup') }}
                                        </span>
                                    </router-link>
                                </li>

                            </ul>
                        </div>
                    </li>


                    <li v-if="(isValid('CanViewProductionRecipe') || isValid('CanViewProductionBatch') || isValid('CanViewDispatchNote') )">
                        <a data-toggle="collapse" href="#Production" v-bind:aria-expanded="productionMenu? true : false">
                            <i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/Statistics.svg" /></i>
                            <p class="menu_txt" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                {{ $t('Dashboard.Production') }}
                                <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                            </p>
                        </a>
                        <div class="collapse" id="Production">
                            <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:0px !important' : ''">
                                <li v-if="(isValid('CanViewProductionBatch'))">
                                    <router-link :to="{path: '/ProductionBatch', query: { token_name:'Manufacturing And Production_token', fromDashboard:'true' } }" v-bind:class="propValvue=='ProductionBatch' || propValvue=='AddProductionBatch'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">PB</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.ProductionBatch') }}
                                        </span>
                                    </router-link>
                                </li>
                                <li v-if="(isValid('CanViewProductionRecipe'))">
                                    <router-link :to="{path: '/RecipeNo', query: { token_name:'Manufacturing And Production_token', fromDashboard:'true' } }" v-bind:class="propValvue=='AddRecipeNo' || propValvue=='RecipeNo'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">RN</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Recipe') }}
                                        </span>
                                    </router-link>
                                </li>

                                <li v-if="(isValid('CanViewDispatchNote'))">
                                    <router-link :to="{path: '/DispatchNotes', query: { token_name:'Manufacturing And Production_token', fromDashboard:'true' } }" v-bind:class="propValvue=='DispatchNotes' || propValvue=='AddDispatchNote'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">DN</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.DispatchNote') }}
                                        </span>

                                    </router-link>
                                </li>
                            </ul>
                        </div>
                    </li>


                    <li v-if="isValid('NewModules')">
                        <a data-toggle="collapse" href="#ContractManagement" v-bind:aria-expanded="contractMenu? true : false">
                            <i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/HR.svg" /></i>
                            <p class="menu_txt" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                {{ $t('Dashboard.ContractManagement') }}
                                <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                            </p>
                        </a>
                        <div class="collapse" v-bind:class="contractMenu ? 'show' : ''" id="ContractManagement">
                            <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:0px !important' : ''">

                                <li>
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CA</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.ContractAgreement') }}
                                        </span>

                                    </a>
                                </li>
                                <li>
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CR</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Reports') }}
                                        </span>

                                    </a>
                                </li>
                                <li>
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CS</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Setup') }}
                                        </span>

                                    </a>
                                </li>
                            </ul>
                        </div>
                    </li>
                    <li v-if="isValid('InventoryItemLookUp') ||isValid('EmailManagement') ||isValid('SmsManagement') ||isValid('CanPrintRackBarcode') || isValid('CanPrintItemBarcode') || isValid('CanBackUpData') || isValid('CanRestoreData')">
                        <a data-toggle="collapse" href="#Miscellaneous" v-bind:aria-expanded="contractMenu? true : false">
                            <i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/HR.svg" /></i>
                            <p class="menu_txt" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                {{ $t('Dashboard.Miscellaneous') }}
                                <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                            </p>
                        </a>
                        <div class="collapse" v-bind:class="contractMenu ? 'show' : ''" id="Miscellaneous">
                            <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:0px !important' : ''">

                                <li v-if="isValid('CanPrintRackBarcode') || isValid('CanPrintItemBarcode')">
                                    <router-link :to="{path: '/BarcodeSetup', query: { token_name:'Product And Inventory Management_token', fromDashboard:'true' } }" v-bind:class="propValvue=='MultiBarcodePrinting'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">BS</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.BarcodeManagement') }}
                                        </span>
                                    </router-link>
                                </li>
                                <li>
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">EM</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.PriceBarcode') }}
                                        </span>

                                    </a>
                                </li>
                                <li>
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">EM</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.ShelfBarcode') }}
                                        </span>

                                    </a>
                                </li>
                                <li>
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">EM</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.BarcodeTemplates') }}
                                        </span>

                                    </a>
                                </li>
                                <li>
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">EM</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.BarcodeSetup') }}
                                        </span>

                                    </a>
                                </li>
                                <li>
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">EM</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Reports') }}
                                        </span>

                                    </a>
                                </li>
                                <li v-if="isValid('CanBackUpData') || isValid('CanRestoreData')">
                                    <router-link :to="{path: '/BackupAndRestore', query: { token_name:'Setups And Configuration_token', fromDashboard:'true' } }" v-bind:class="propValvue=='Backup'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">BR</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.CloudBackupResotre') }}
                                        </span>
                                    </router-link>
                                </li>

                                <li v-if="isValid('InventoryItemLookUp')">
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CR</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.InventoryItemLookup') }}
                                        </span>

                                    </a>
                                </li>

                            </ul>
                        </div>
                    </li>


                    <li v-if="isValid('EmailManagement')">
                        <a data-toggle="collapse" href="#EmailManagement" v-bind:aria-expanded="contractMenu? true : false">
                            <i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/HR.svg" /></i>
                            <p class="menu_txt" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                {{ $t('Dashboard.EmailManagement') }}
                                <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                            </p>
                        </a>

                        <div class="collapse" id="EmailManagement">
                            <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:15px !important' : 'padding-left:15px !important'">
                                <li>
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">EM</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Emails') }}
                                        </span>

                                    </a>
                                </li>
                                <li>
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">ER</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Reports') }}
                                        </span>

                                    </a>
                                </li>
                                <li>
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">ES</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Setup') }}
                                        </span>

                                    </a>
                                </li>
                            </ul>
                        </div>
                    </li>


                    <li v-if="isValid('SmsManagement')">
                        <a data-toggle="collapse" href="#SmsManagement" v-bind:aria-expanded="contractMenu? true : false">
                            <i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/HR.svg" /></i>
                            <p class="menu_txt" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                {{ $t('Dashboard.SMSManagement') }}
                                <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                            </p>
                        </a>

                        <div class="collapse" id="SmsManagement">
                            <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:15px !important' : 'padding-left:15px !important'">
                                <li>
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SM</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.SMS') }}
                                        </span>

                                    </a>
                                </li>
                                <li>
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SR</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Reports') }}
                                        </span>

                                    </a>
                                </li>
                                <li>
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SS</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Setup') }}
                                        </span>

                                    </a>
                                </li>
                            </ul>
                        </div>
                    </li>


                    <li v-if="isValid('CanViewStockReport') || isValid('CanViewSaleInvoiceReport') || isValid('CanViewPurchaseInvoiceReport') || isValid('CanViewInventoryReport') || isValid('CanViewProductInventoryReport') || isValid('CanViewTrialBalance') || isValid('CanViewBalanceSheetReport') || isValid('CanViewIncomeStatementReport') || isValid('CanViewCustomerLedger') || isValid('CanViewSupplieLedger') || isValid('CanViewStockValueReport') || isValid('CanViewStockAverageValue') || isValid('CanViewTransactionTypeStock') || isValid('CanViewCustomerWiseProductsSale') || isValid('CanViewCustomersWiseProductSale') || isValid('CanViewSupplierWiseProductsPurchase') || isValid('CanViewSuppliersWiseProductPurchase') || isValid('CanViewCustomerDiscountProducts') || isValid('CanViewSupplierDiscountProducts') || isValid('CanViewProductDiscountCustomer') || isValid('CanViewProductDiscountSupplier') || isValid('CanViewFreeOfCostPurchase') || isValid('CanViewFreeOfCostSale') || isValid('CanViewAccountLedger') || isValid('CanViewBanTransaction') || isValid('CanViewCustomerBalance') || isValid('CanViewSupplierBalance') || isValid('CanViewVatPayableReport') || isValid('CanViewDayWiseTransactions') || isValid('CanViewDayWiseReport')|| isValid('CanViewTCAllocationReport')">
                        <a data-toggle="collapse" href="#ReportManagement" v-bind:aria-expanded="contractMenu? true : false">
                            <i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/HR.svg" /></i>
                            <p class="menu_txt" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                {{ $t('Dashboard.ReportManagement') }}
                                <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                            </p>
                        </a>

                        <div class="collapse" id="ReportManagement">
                            <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:15px !important' : 'padding-left:15px !important'">
                               
                                <li>
                                    <router-link :to="{path: '/AllReports', query: { token_name:'Reporting_token', fromDashboard:'true' } }" v-bind:class="propValvue=='MultiBarcodePrinting'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">RP</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Reports') }}
                                        </span>
                                    </router-link>
                                   
                                </li>
                                <li>
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SS</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.ReportDesginer') }}
                                        </span>

                                    </a>
                                </li>
                                <li>
                                    <a href="javascript:void(0)" v-on:click="commingSoonPage">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">SR</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Setup') }}
                                        </span>

                                    </a>
                                </li>
                            </ul>
                        </div>
                    </li>


                    <li v-if="isValid('CanViewUserRole') || isValid('CanViewUserPermission') || isValid('CanViewSignUpUser') || isValid('CanViewTerminal') || isValid('CanViewPosTerminal') || isValid('CanUpdateCompanyInfo') || isValid('CanPushRecord')|| isValid('CanPullRecord')|| isValid('CanFlushDatabase')|| isValid('CanResetDatabase')">
                        <a data-toggle="collapse" href="#System" v-bind:aria-expanded="systemMenu? true : false">
                            <i v-bind:class="$i18n.locale == 'ar' ? 'ar_sidebar_menu' : ''"><img class="ImageWidth" src="Noble menu icons/System.svg" /></i>
                            <p class="menu_txt" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                {{ $t('Dashboard.SystemManagement') }}
                                <b class="caret" v-bind:class="$i18n.locale == 'ar' ? 'ar_caret' : ''"></b>
                            </p>
                        </a>
                        <div class="collapse" id="System">
                            <ul class="nav" v-bind:style="$i18n.locale == 'ar' ? 'padding-right:0px !important' : ''">
                                <li v-if="isValid('CanUpdateCompanyInfo')">
                                    <router-link :to="{path: '/CompanyProfile', query: { token_name:'Setups And Configuration_token', fromDashboard:'true' } }" v-bind:class="propValvue=='CompanyProfile'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">CP</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.CompanyInfo') }}
                                        </span>

                                    </router-link>
                                </li>
                                <li>
                                    <router-link :to="'/aboutUs'" v-bind:class="propValvue=='aboutUs'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">AU</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.LicenseInfo') }}
                                        </span>
                                    </router-link>
                                </li>
                                <li v-if="isValid('CanPushRecord') || isValid('CanPullRecord')">
                                    <router-link :to="{path: '/Synchronization', query: { token_name:'Setups And Configuration_token', fromDashboard:'true' } }" v-bind:class="propValvue=='PullRecords'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">PR</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.Synchronization') }}
                                        </span>
                                    </router-link>
                                </li>
                                <li v-if="isValid('CanFlushDatabase') ">
                                    <router-link :to="{path: '/FlushDatabase', query: { token_name:'Setups And Configuration_token', fromDashboard:'true' } }" v-bind:class="propValvue=='FlushDatabase'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">FD</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.FlushDatabase') }}
                                        </span>
                                    </router-link>
                                </li>
                                <li v-if="isValid('CanResetDatabase')">
                                    <a v-on:click="SupervisorLogin" class="linkhover">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">RD</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.ResetDatabase') }}
                                        </span>
                                    </a>
                                </li>
                                <li v-if="(isValid('CanViewTerminal') && dayStart)">
                                    <router-link :to="{path: '/terminal', query: { token_name:'DayStart_token', fromDashboard:'true' } }" v-bind:class="propValvue=='Terminal'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">ST</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.SystemTerminals') }}
                                        </span>
                                    </router-link>
                                </li>
                                <li v-if="(isValid('CanViewPosTerminal') && dayStart)">
                                    <router-link :to="{path: '/BankPosTerminal', query: { token_name:'DayStart_token', fromDashboard:'true' } }" v-bind:class="propValvue=='BankPosTerminal'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">BT</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.BankPOSTerminals') }}
                                        </span>
                                    </router-link>
                                </li>

                                <li v-if=" isValid('CanViewUserRole') || isValid('CanViewUserPermission') || isValid('CanViewSignUpUser')">
                                    <router-link :to="{path: '/UserSetup', query: { token_name:'Settings And Permission_token', fromDashboard:'true' } }" v-bind:class="propValvue=='UserSetup' || propValvue=='AddSignUp' || propValvue=='SignUp' || propValvue=='Roles' || propValvue=='Permissions'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">US</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.UserSetup') }}
                                        </span>
                                    </router-link>
                                </li>

                                <li v-if=" isValid('CanViewUserPermission')">
                                    <router-link :to="{path: '/Permissions', query: { token_name:'Settings And Permission_token', fromDashboard:'true' } }" v-bind:class="propValvue=='UserSetup' || propValvue=='AddSignUp' || propValvue=='SignUp' || propValvue=='Roles' || propValvue=='Permissions'? 'router-link-exact-active router-link-active' : ''">
                                        <span v-bind:class="$i18n.locale == 'ar' ? 'ar_mini_icon' : ''" class="sidebar-mini-icon">US</span>
                                        <span class="sidebar-normal" v-bind:class="$i18n.locale == 'ar' ? 'arabicLanguage' : ''">
                                            {{ $t('Dashboard.SetupPermissions') }}
                                        </span>
                                    </router-link>
                                </li>


                            </ul>
                        </div>
                    </li>


                </ul>
            </div>
        </div>
        <div class="main-panel" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'MainPanelPadding' : 'MainPanelPaddingRight'">
            <!-- Navbar -->
            <nav class="navbar navbar-expand-lg navbar-absolute fixed-top navbar-transparent" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'navPaddingLeft' : 'navPaddingRight'">
                <div class="container-fluid">
                    <div class="navbar-wrapper">
                        <div class="navbar-minimize">

                            <button id="minimizeSidebar" class="btn btn-primary btn-icon btn-round">
                                <i class="nc-icon nc-minimal-right text-center visible-on-sidebar-mini"></i>
                                <i class="nc-icon nc-minimal-left text-center visible-on-sidebar-regular"></i>
                            </button>



                        </div>
                        <div class="navbar-toggle">

                            <button type="button" class="navbar-toggler">
                                <span class="navbar-toggler-bar bar1"></span>
                                <span class="navbar-toggler-bar bar2"></span>
                                <span class="navbar-toggler-bar bar3"></span>
                            </button>
                        </div>

                    </div>
                    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navigation" aria-controls="navigation-index" aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-bar navbar-kebab"></span>
                        <span class="navbar-toggler-bar navbar-kebab"></span>
                        <span class="navbar-toggler-bar navbar-kebab"></span>
                    </button>

                    <div class="collapse navbar-collapse justify-content-end" id="navigation">
                        <div style="text-align:center !important; padding-right:35px;" v-if="aboutUsDetail.licenseType === 'Demo'">
                            <h6 style="color:red;margin:0;"> Demo Version of ERP, this copy will stop functioning fully or partially on {{aboutUsDetail.toDate}}.</h6>
                            <span>Please activate your license as soon as possible.</span>
                        </div>
                        <ul class="navbar-nav">
                            <li class="nav-item dropdown" style="margin-top: -3px;" v-if="(english=='true' && isOtherLang()) || (english == 'true' && portugues == true)">
                                <a class="nav-link dropdown-toggle" style="font-size:16px;" href="javascript:void(0)" id="navbarDropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <i class="fas fa-globe"></i>
                                    <p>
                                        <span class="d-lg-none d-md-block">Language</span>
                                    </p>
                                </a>
                                <div class="dropdown-menu" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'dropdown-menu-right' : 'dropdown-menu-left'" aria-labelledby="navbarDropdownMenuLink">
                                    <a class="dropdown-item" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" @click="setLocale('en')">English</a>
                                    <a class="dropdown-item" v-if="arabic=='true'" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" style="font-size: 0.8571em;  font-weight: 700" @click="setLocale('ar')">الـعــربـيـة</a>
                                    <a class="dropdown-item"  v-if="portugues==true"  v-bind:class="leftToRight ? 'text-left' : 'text-right'" style="font-size: 0.8571em;  font-weight: 700" @click="setLocale('pt')">Portuguese</a>
                                </div>
                            </li>
                            <li class="nav-item dropdown">
                                <a class="nav-link dropdown-toggle" style="margin-top: -5px;" href="javascript:void(0)" id="navbarDropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <i class="nc-icon nc-circle-10"></i>
                                    <p>
                                        <span class="d-lg-none d-md-block">
                                            {{
  $t('Dashboard.Profile')
                                            }}
                                        </span>
                                    </p>
                                </a>
                                <div class="dropdown-menu " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'dropdown-menu-right' : 'dropdown-menu-left'" aria-labelledby="navbarDropdownMenuLink">
                                    <a class="dropdown-item" href="javascript:void(0)">{{ DisplayUserName }}</a>
                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="UserProfile">{{ $t('Dashboard.MyProfile')}}</a>
                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="UpdateCompanyPermission" v-if="role != 'Noble Admin'"> &nbsp;{{ $t('Dashboard.SyncPermission')}}</a>

                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="logout"><i class="fas fa-sign-out-alt"></i> &nbsp;{{ $t('Dashboard.LogOut')}}</a>
                                </div>
                            </li>
                        </ul>
                    </div>

                </div>
            </nav>
            <!-- End Navbar -->
            <div class="content">
                <router-view @input="logoutUser"></router-view>
                <div v-if="dashboard=='/dashboard'">
                    <dashboard></dashboard>
                </div>
            </div>
        </div>
        <supervisor-login-model @close="onCloseEvent"
                                :show="show"
                                :isFlushData="true"
                                :isReset="true"
                                v-if="show" />

        <loading :active.sync="loading"
                 :can-cancel="true"
                 :is-full-page="true"></loading>
    </div>
</template>
<script>
    import Loading from 'vue-loading-overlay';
    import "vue-loading-overlay/dist/vue-loading.css";
    import clickMixin from '@/Mixins/clickMixin'
    import language from 'element-ui/lib/locale'
    import ar from 'element-ui/lib/locale/lang/ar'
    import en from 'element-ui/lib/locale/lang/en'
    import pt from 'element-ui/lib/locale/lang/pt'
    import axios from 'axios'
    export default {
        mixins: [clickMixin],
        name: 'locale-changer',
        components: {
            Loading
            
        },

        data() {
            return {
                aboutUsDetail: '',
                loading: false,
                IsDailyExpense: false,
                noblePermissions: '',
                paymentLimitInterval: '',
                companyId: '',
                langs: ['en', 'ar','pt'],
                invoiveItem: false,
                invoiveBarCode: false,
                invoiveBarCodeItem: false,
                saleOrderPerm: false,
                WholeSale: '',
                purchaseOrder: false,
                isMouseover: false,
                expenseBill: false,
                IsExpenseAccount: false,
                DisplayUserName: '',
                role: '',
                dashboard: '',
                ur: '',
                isAccount: '',
                isDayStart: '',
                arabic: '',
                english: '',
                isMasterProduct: false,
                nobleRole: '',
                show: false,
                loginHistory: {
                    userId: '',
                    isLogin: false,
                    companyId: ''
                },
                dayStart: '',
                simpleInvoice: '',
                propValvue: '',
                saleMenu: false,
                inventoryMenu: false,
                wareHouseMenu: false,
                startOperationMenu: false,
                startOpSetupMenu: false,
                accountMenu: false,
                purchaseMenu: false,
                settingMenu: false,
                humanMenu: false,
                financialMenu: false,
                contractMenu: false,
                logisticMenu: false,
                importExportMenu: false,
                systemMenu: false,
                productionMenu: false,
                inquiryMenu: false,
                portugues: false,
                leftToRight: false,
                 
                importExportSale: false,
                applicationName: '',
            }
        },
        methods: {
            StartScreen: function () {
                this.$router.push('/StartScreen')
            },
            commingSoonPage: function () {
                this.$router.push('/CommingSoon')
            },
            UpdateCompanyPermission: function () {
                this.loading = true
                
                var root = this;
                axios.get(root.$PermissionIp + '/NoblePermission/GetAllPermissionData?id=' + this.companyId + '&systemType=' + root.$SystemType).then(function (response) {
                    if (response.data != null) {
                        
                        root.noblePermissions = response.data.result
                        root.SaveNoblePermissions(root.companyId)
                    }

                }).catch(error => {
                    console.log(error)
                    root.$swal.fire(
                        {
                            icon: 'error',
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                            text: 'Please Contact to support to update license',

                            showConfirmButton: false,
                            timer: 5000,
                            timerProgressBar: true,
                        });
                    root.loading = false

                });
            },
            SaveNoblePermissions: function (locationId) {
                var root = this;
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                this.noblePermissions.locationId = locationId;
                if (this.noblePermissions != '' || this.noblePermissions != null || this.noblePermissions != undefined) {
                    this.$https.post('/Company/SaveNoblePermission', this.noblePermissions, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(function (response) {
                            if (response.data.isSuccess == true) {

                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update Successfully!' : 'تم التحديث بنجاح',
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });
                                root.logout()
                            }
                            else {
                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                    type: 'error',
                                    icon: 'error',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });
                            }
                            root.loading = false
                        }).catch(error => {
                            console.log(error)
                            root.$swal.fire(
                                {
                                    icon: 'error',
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                    text: error,

                                    showConfirmButton: false,
                                    timer: 5000,
                                    timerProgressBar: true,
                                });

                            root.loading = false
                        });
                }
            },
            PaymentLimitIntervalFunc: function () {
                var root = this;
                this.paymentLimitInterval = setInterval(() => {
                    root.AddUpdateNoblePaymentLimit();
                }, 1800000);
            },
            PaymentLimitIntervalClear: function () {
                clearInterval(this.paymentLimitInterval)
                this.paymentLimitInterval = ''
            },
            AddUpdateNoblePaymentLimit: function () {
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Company/AddUpdateNoblePaymentLimit', { headers: { "Authorization": `Bearer ${token}` } });
            },

            AddSalaryTemplate: function () {


                var root = this;
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Payroll/ChecKPaySchedule', { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data > 0) {
                            root.$router.push('/AddRunPayroll');
                        }
                        else {
                            root.$swal({
                                title: 'Warning!',
                                text: 'Please publish the opened payrolls before running the next payroll.',
                                type: 'warning',
                                confirmButtonClass: "btn btn-warning",
                                icon: 'warning',
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        });


            },
            logoutUser: function (user) {

                if (user == 'AddDailyExpensedailyexpense') {

                    user = 'DailyExpense';
                }
                else if (user == 'AddDailyExpense') {

                    user = 'DailyExpense';
                }
                else if (user == 'AddDailyExpensegeneralexpense') {

                    user = 'DailyExpense';
                }
                else if (user == 'AddDailyExpensedailyexpense') {

                    user = 'DailyExpense';
                }
                else if (user == 'AddDailyExpensegeneralexpense') {

                    user = 'DailyExpense';
                }
                else if (user == 'InvoiceView') {

                    user = 'InvoiceView';
                }
                else if (user == 'WareHouseTransfers') {

                    user = 'WareHouseTransfer';
                }
                else if (user == 'AddStockValueStockIn') {

                    user = 'AddStockValueStockIn';
                }
                else if (user == 'AddStockValueStockOut') {

                    user = 'AddStockValueStockOut';
                }
                else if (user == 'StockValuesStockIn') {

                    user = 'StockValueStockIn';
                }
                else if (user == 'StockValuesStockOut') {

                    user = 'StockValueStockOut';
                }
                else if (user == 'AddWareHouseTransfer') {

                    user = 'AddWareHouseTransfer';
                }
                else if (user == 'Addpurchase') {

                    user = 'purchase';
                }
                else if (user == 'DailyExpenseView') {

                    user = 'DailyExpenseView';
                }
                else if (user == 'PurchaseView') {

                    user = 'PurchaseView';
                }
                else if (user == 'AddProduct') {

                    user = 'Product';
                }
                else if (user == 'AddSales') {

                    user = 'Sale';
                }
                else if (user == 'AddSale') {

                    user = 'AddSale';
                }
                else if (user == 'AddSaleReturns') {

                    user = 'SaleReturn';
                }
                else if (user == 'AddSaleReturn') {

                    user = 'AddSaleReturn';
                }
                else if (user == 'ViewSaleReturn') {

                    user = 'ViewSaleReturn';
                }
                else if (user == 'AddSaleOrders') {

                    user = 'SaleOrder';
                }
                else if (user == 'AddSaleOrder') {

                    user = 'AddSaleOrder';
                }
                else if (user == 'SaleOrderView') {

                    user = 'SaleOrderView';
                }
                else if (user == 'purchaseorders') {

                    user = 'purchaseorder';
                }
                else if (user == 'Addpurchaseorder') {

                    user = 'Addpurchaseorder';
                }
                else if (user == 'PurchaseOrderView') {

                    user = 'PurchaseOrderView';
                }
              
                else if (user == 'goodReceives') {

                    user = 'goodReceives';
                }
                else if (user == 'AddGoodReceive') {

                    user = 'AddGoodReceive';
                }
               
                else if (user == 'PurchaseReturns') {

                    user = 'PurchaseReturn';
                }
                else if (user == 'AddPurchaseReturn') {

                    user = 'AddPurchaseReturn';
                }
                else if (user == 'PurchaseReturnView') {

                    user = 'PurchaseReturnView';
                }
                else if (user == 'PurchaseBills') {

                    user = 'PurchaseBill';
                }
                else if (user == 'AddPurchaseBill') {

                    user = 'AddPurchaseBill';
                }
                else if (user == 'PurchaseBillView') {

                    user = 'PurchaseBillView';
                }
                else if (user == 'PaymentVoucherListsBankReceipt') {

                    user = 'PaymentVoucherListBankReceipt';
                }
                else if (user == 'addPaymentVoucherBankReceipt') {

                    user = 'addPaymentVoucherBankReceipt';
                }
                else if (user == 'PaymentVoucherViewBankReceipt') {

                    user = 'PaymentVoucherViewBankReceipt';
                }
                else if (user == 'PaymentVoucherListsBankPay') {

                    user = 'PaymentVoucherListBankPay';
                }
                else if (user == 'addPaymentVoucherBankPay') {

                    user = 'addPaymentVoucherBankPay';
                }
                else if (user == 'PaymentVoucherViewBankPay') {

                    user = 'PaymentVoucherViewBankPay';
                }
                else if (user == 'PaymentVoucherListsPettyCash') {

                    user = 'PaymentVoucherListPettyCash';
                }
                else if (user == 'addPaymentVoucherPettyCash') {

                    user = 'addPaymentVoucherPettyCash';
                }
                else if (user == 'PaymentVoucherViewPettyCash') {

                    user = 'PaymentVoucherViewPettyCash';
                }
                else if (user == 'JournalVoucherViewsJournalVoucher') {

                    user = 'JournalVoucherViewJournalVoucher';
                }
                else if (user == 'AddJournalVoucherJournalVoucher') {

                    user = 'AddJournalVoucherJournalVoucher';
                }
                else if (user == 'JournalVoucherViewsOpeningCash') {

                    user = 'JournalVoucherViewOpeningCash';
                }
                else if (user == 'AddJournalVoucherOpeningCash') {

                    user = 'AddJournalVoucherOpeningCash';
                }
                else if (user == 'SaleServiceOrder') {

                    user = 'SaleServiceOrder';
                }
                else if (user == 'AddSaleServiceOrder') {

                    user = 'AddSaleServiceOrder';
                }


                else if (user == 'purchase') {

                    localStorage.removeItem("fromDate");
                    localStorage.removeItem("toDate");
                    localStorage.removeItem("active");
                    localStorage.removeItem("currentPage");
                }

                else {
                    localStorage.removeItem("fromDate");
                    localStorage.removeItem("toDate");
                    localStorage.removeItem("active");
                    localStorage.removeItem("currentPage");
                }
                this.propValvue = user;
                this.saleMenu = false;
                this.inventoryMenu = false;
                this.wareHouseMenu = false;
                this.startOperationMenu = false;
                this.startOpSetupMenu = false;
                this.purchaseMenu = false;
                this.accountMenu = false;
                this.settingMenu = false;
                this.humanMenu = false;
                this.financialMenu = false;
                this.contractMenu = false;
                this.logisticMenu = false;
                this.importExportMenu = false;
                this.systemMenu = false;
                this.productionMenu = false;
                this.inquiryMenu = false;






                if (this.propValvue == 'AddSale'
                    || this.propValvue == 'AddCustomer2'
                    || this.propValvue == 'Customer'
                    || this.propValvue == 'AddSaleOrder'
                    || this.propValvue == 'SaleOrder'
                    || this.propValvue == 'AddQuotation'
                    || this.propValvue == 'Quotation'
                    || this.propValvue == 'AddSaleReturn'
                    || this.propValvue == 'SaleReturn'
                    || this.propValvue == 'Sales'
                    || this.propValvue == 'AddSaleService'
                    || this.propValvue == 'SaleService'
                    || this.propValvue == 'addPaymentVoucherBankReceipt'
                    || this.propValvue == 'SaleServiceOrder'
                    || this.propValvue == 'AddSaleServiceOrder'
                    || this.propValvue == 'ServiceQuotation'
                    || this.propValvue == 'AddServiceQuotation'
                    || this.propValvue == 'PaymentVoucherListBankReceipt'
                    || this.propValvue == 'PrintSetting'
                    || this.propValvue == 'InquirySetup'
                    || this.propValvue == 'Inquiry') {
                    this.saleMenu = true;
                }
                if (this.propValvue == 'ProductMaster'
                    || this.propValvue == 'AddProduct'
                    || this.propValvue == 'InventoryBlind'
                    || this.propValvue == 'InventoryBlindList'
                    || this.propValvue == 'AddBundles'
                    || this.propValvue == 'Bundles'
                    || this.propValvue == 'AddPromotion'
                    || this.propValvue == 'Promotion'
                    || this.propValvue == 'Product'
                    || this.propValvue == 'Category'
                    || this.propValvue == 'Brand'
                    || this.propValvue == 'Origin'
                    || this.propValvue == 'Size'
                    || this.propValvue == 'Color'
                    || this.propValvue == 'Unit'
                    || this.propValvue == 'SubCategory'
                    || this.propValvue == 'ProductManagement') {
                    this.inventoryMenu = true;
                }
                if (this.propValvue == 'AddWareHouseTransfer'
                    || this.propValvue == 'WareHouseTransfer'
                    || this.propValvue == 'AddStockValueStockIn'
                    || this.propValvue == 'StockValueStockIn'
                    || this.propValvue == 'StockValueStockOut'
                    || this.propValvue == 'AddStockValueStockOut'
                    || this.propValvue == 'AddWarehouse'
                    || this.propValvue == 'Warehouse') {
                    this.wareHouseMenu = true;
                }
                if (this.propValvue == 'StartOperation'
                    || this.propValvue == 'StartOperationSetup'
                    || this.propValvue == 'StartOperationReport') {
                    this.startOperationMenu = true;
                }
                if (this.propValvue == 'UserSetup'
                    || this.propValvue == 'CounterSetup'
                    || this.propValvue == 'AdditionalSetup') {
                    this.startOpSetupMenu = true;
                }
                if (this.propValvue == 'Addpurchase'
                    || this.propValvue == 'purchase'
                    || this.propValvue == 'Addpurchaseorder'
                    || this.propValvue == 'GoodReceive'
                    || this.propValvue == 'goodReceives'
                    || this.propValvue == 'AddGoodReceive'
                    || this.propValvue == 'AddPurchaseReturn'
                    || this.propValvue == 'PurchaseReturn'
                    || this.propValvue == 'AddAutoPurchaseTemplate'
                    || this.propValvue == 'autoPurchaseTemplate'
                    || this.propValvue == 'AddSupplier'
                    || this.propValvue == 'Supplier'
                    || this.propValvue == 'addPaymentVoucherBankPay'
                    || this.propValvue == 'PaymentVoucherListBankPay'
                    || this.propValvue == 'purchaseorder') {
                    this.purchaseMenu = true;
                }
                if (this.propValvue == 'addPaymentVoucherPettyCash'
                    || this.propValvue == 'AddJournalVoucherJournalVoucher'
                    || this.propValvue == 'JournalVoucherViewJournalVoucher'
                    || this.propValvue == 'AddJournalVoucherOpeningCash'
                    || this.propValvue == 'JournalVoucherViewOpeningCash'
                    || this.propValvue == 'AddTemporaryCashRequest'
                    || this.propValvue == 'TemporaryCashRequest'
                    || this.propValvue == 'AddTemporaryCashIssue'
                    || this.propValvue == 'TemporaryCashIssue'
                    || this.propValvue == 'TemporaryCashAllocation'
                    || this.propValvue == 'AddTemporaryCashAllocation'
                    || this.propValvue == 'PaymentVoucherListPettyCash'
                    || this.propValvue == 'Currency'
                    || this.propValvue == 'Bank'
                    || this.propValvue == 'AddBank'
                    || this.propValvue == 'MonthlyCost'
                    || this.propValvue == 'coa'
                    || this.propValvue == 'PaymentOptions'
                    || this.propValvue == 'DenominationSetup'
                    || this.propValvue == 'TaxRate'
                    || this.propValvue == 'ExpenseType'
                    || this.propValvue == 'FinancialSetup') {
                    this.accountMenu = true;
                }
                if (this.propValvue == 'ProductMaster'
                    || this.propValvue == 'AddSignUp'
                    || this.propValvue == 'SignUp'
                    || this.propValvue == 'Roles'
                    || this.propValvue == 'Permissions'
                    || this.propValvue == 'UserSetup') {
                    this.settingMenu = true;
                }
                if (this.propValvue == 'EmployeeRegistration'
                    || this.propValvue == 'AddEmployeeRegistration'
                    || this.propValvue == 'City'
                    || this.propValvue == 'GeographicalSetup'
                    || this.propValvue == 'Region') {
                    this.humanMenu = true;
                }
                if (this.propValvue == 'EmployeeRegistration'
                    || this.propValvue == 'AddEmployeeRegistration'
                    || this.propValvue == 'City'
                    || this.propValvue == 'GeographicalSetup'
                    || this.propValvue == 'Region') {
                    this.financialMenu = true;
                }
                if (this.propValvue == 'EmployeeRegistration'
                    || this.propValvue == 'AddEmployeeRegistration'
                    || this.propValvue == 'City'
                    || this.propValvue == 'GeographicalSetup'
                    || this.propValvue == 'Region') {
                    this.contractMenu = true;
                }
                if (this.propValvue == 'ManualAttendance') {
                    this.humanMenu = true;
                }
                if (this.propValvue == 'EmployeeTodayAttendence') {
                    this.humanMenu = true;
                }
                if (this.propValvue == 'AttendanceReport') {
                    this.humanMenu = true;
                }
                if (this.propValvue == 'AddHolidayOfMonth') {
                    this.humanMenu = true;
                }
               
                if (this.propValvue == 'LogisticsListTransporter'
                    || this.propValvue == 'AddLogisticsTransporter'
                    || this.propValvue == 'LogisticsListClearanceAgent'
                    || this.propValvue == 'AddLogisticsClearanceAgent'
                    || this.propValvue == 'AddLogisticsShippingLinear'
                    || this.propValvue == 'LogisticsListShippingLinear') {
                    this.logisticMenu = true;
                }
                //if (this.propValvue == 'LogisticsListTransporter'
                //    || this.propValvue == 'AddLogisticsTransporter'
                //    || this.propValvue == 'LogisticsListClearanceAgent'
                //    || this.propValvue == 'AddLogisticsClearanceAgent'
                //    || this.propValvue == 'AddLogisticsShippingLinear'
                //    || this.propValvue == 'LogisticsListShippingLinear') {
                //    this.importExportMenu = true;
                //}
                if (this.propValvue == 'CompanyProfile'
                    || this.propValvue == 'Restore'
                    || this.propValvue == 'PushRecords'
                    || this.propValvue == 'PullRecords'
                    || this.propValvue == 'FlushDatabase'
                    || this.propValvue == 'Terminal'
                    || this.propValvue == 'BankPosTerminal'
                    || this.propValvue == 'MultiBarcodePrinting'
                    || this.propValvue == 'RackBarcodeCreate'
                    || this.propValvue == 'ApplicationUpdate'
                    || this.propValvue == 'Backup' || this.propValvue == 'aboutUs') {
                    this.systemMenu = true;
                }
                if (this.propValvue == 'SaleOrder'
                    || this.propValvue == 'AddRecipeNo'
                    || this.propValvue == 'RecipeNo'
                    || this.propValvue == 'AddProductionBatch'
                    || this.propValvue == 'ProductionBatch'
                    || this.propValvue == 'AddDispatchNote'
                    || this.propValvue == 'DispatchNotes'
                    || this.propValvue == 'AddSaleOrder') {
                    this.productionMenu = true;
                }
                
                localStorage.removeItem("BarcodeScan");
            },
            SupervisorLogin: function () {
                this.show = true;
            },
            onCloseEvent: function (flag) {
                if (flag) {
                    this.flush()
                }
                this.show = false
            },
            flush: function () {
                var root = this;
                this.loading1 = true;
                var token = '';
                if (root.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                root.loading1 = true;
                var records = 'Reset'
                root.$https
                    .get('/System/FlushRecords?records=' + records, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.status == 200) {
                            root.logout();
                            root.$swal({
                                title: "Success!",
                                text: "Flush data successfully",
                                type: 'error',
                                confirmButtonClass: "btn btn-Success",
                                buttonStyling: false,

                            });

                        }
                        root.loading1 = false;

                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });
                        root.loading1 = false;
                    });
            },
            setLocale(locale) {
                this.$i18n.locale = locale;
                if (locale == 'en') {
                    language.use(en)
                }
                else if (locale == 'pt') {
                    language.use(pt)
                }
                else {
                    language.use(ar)
                }

                localStorage.setItem('locales', locale);
                this.$router.go(this.$router.currentRoute.fullPath)

            },


            UserProfile: function () {
                this.$router.push('/RegisterUser');
            },
            logoutHistorySave: function () {

                this.loginHistory.userId = localStorage.getItem('UserID')
                this.loginHistory.companyId = localStorage.getItem('CompanyID')
                this.$https.post('/account/LoginHistory', this.loginHistory).then(function (response) {
                    if (response.data == 1)
                        console.log('Logout History save done');
                    else
                        console.log('Logout History not save due to some error ' + response.data);
                });
            },
            logout: function () {
                var root = this;
                //  var Swal = this.$swal
                var url = '/account/logout';
                this.$https.post(url, this.login).then(function (response) {

                    if (response.data == "Success") {
                        root.PaymentLimitIntervalClear();
                        root.logoutHistorySave();

                        var getLocale = localStorage.getItem('locales');

                        root.$session.destroy();
                        //document.cookie.split(';').forEach(cookie => document.cookie = cookie.replace(/^ +/, '').replace(/=.*/, `=;expires=${new Date(0).toUTCString()};path=/`));
                        localStorage.clear();

                        localStorage.setItem('locales', getLocale);

                        root.$router.push('/')

                    }
                    else {
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: 'Error Logging Out'
                            });
                    }

                });

            },
            GetAboutUsDetail: function () {
                var root = this;
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Company/AboutUs', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.isSuccess) {
                        root.aboutUsDetail = response.data.message
                        root.loading = false;
                    }
                    root.loading = false;
                });
            }
        },
        created() {
            this.GetAboutUsDetail()
            this.portugues = localStorage.getItem('Portugues') == "true" ? true : false;
            this.leftToRight = localStorage.getItem('LeftToRight') == "true" ? true : false;

            this.applicationName = localStorage.getItem('ApplicationName')
            //this.PaymentLimitIntervalFunc();
        },
        mounted: function () {


            this.companyId = localStorage.getItem('CompanyID')

            if (this.$session.exists()) {

                this.IsExpenseAccount = localStorage.getItem('IsExpenseAccount') == 'true' ? true : false;

                this.invoiveItem = localStorage.getItem('invoiveItem') == "true" ? true : false;
                this.invoiveBarCode = localStorage.getItem('invoiveBarCode') == "true" ? true : false;
                this.invoiveBarCodeItem = localStorage.getItem('invoiveBarCodeItem') == "true" ? true : false;
                this.saleOrderPerm = localStorage.getItem('saleOrderPerm') == "true" ? true : false;
                this.dayStart = localStorage.getItem('DayStart') == "true" ? true : false;
                this.isMasterProduct = localStorage.getItem('IsMasterProductPermission') == 'true' ? true : false;
                this.IsDailyExpense = localStorage.getItem('IsDailyExpense') == 'true' ? true : false;
                this.expenseBill = localStorage.getItem('expenseBill') == 'true' ? true : false;
                this.importExportSale = localStorage.getItem('ImportExportSale') == 'true' ? true : false;

                this.WholeSale = localStorage.getItem('BankDetail');


                this.DisplayUserName = localStorage.getItem('UserName');
                this.purchaseOrder = localStorage.getItem('PurchaseOrder') == 'true' ? true : false;
                this.role = localStorage.getItem('RoleName');
                this.isAccount = localStorage.getItem('isAccount');
                this.dashboard = this.$router.options.routes;

                this.isDayStart = localStorage.getItem('DayStart');
                this.nobleRole = localStorage.getItem('NobleRole');

            }
            if (!this.$session.exists()) {
                this.$router.push('/')
            }
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.simpleInvoice = localStorage.getItem('SimpleInvoice');
        },
        
    }

</script>
<style scoped>
    @media (min-width: 991px) {
        .sidebar-mini .MainPanelPaddingRight {
            right: 70px !important;
        }

        .main_panel_arabic {
            right: 225px !important;
        }
    }

    @media (min-width: 1400px) {
        .main_panel_arabic {
            right: 260px !important;
        }
    }

    .sidebar_arabic {
        border-top-left-radius: 50px;
        border-bottom-left-radius: 50px;
        border-top-right-radius: 0px !important;
        border-bottom-right-radius: 0px !important;
    }

    

    .linkhover:hover {
        cursor: pointer;
        color: #FFFFFF !important;
        border-radius: 0 !important;
    }

    .menu_txt {
        font-size: 14px;
    }

    .ar_sidebar_menu {
        float: right !important;
        margin-left: 12px !important;
        margin-right: 0 !important;
    }

    .ar_caret {
        left: 0 !important;
        right: auto !important;
    }

    .ar_mini_icon {
        float: right !important;
        text-align: right !important;
    }

    .on {
        display: none
    }
</style>
