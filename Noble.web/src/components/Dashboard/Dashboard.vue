<template>
    <div class="container-fluid">
        <!-- Page-Title -->
        <div class="row">
            <div class="col-sm-12">
                <div class="page-title-box">
                    <div class="row">
                        <div class="col">
                            <h4 class="page-title">{{ $t('Analytics.Analytics') }}</h4>
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Home') }}</a></li>
                                <li class="breadcrumb-item active">{{ $t('Analytics.Dashboard') }}</li>
                            </ol>
                        </div>
                        <!--end col-->
                        <div class="col-auto align-self-center">
                            <a href="#" class="btn btn-sm btn-outline-primary" id="Dash_Date">
                                <span class="ay-name" id="Day_Name">Today:</span>&nbsp;
                                <span class="" id="Select_date">{{ date }}</span>
                                <i data-feather="calendar" class="align-self-center icon-xs ms-1"></i>
                            </a>

                        </div>
                        <!--end col-->
                    </div>
                    <!--end row-->
                </div>
                <!--end page-title-box-->
            </div>
            <!--end col-->
        </div>
        <!--end row-->
        <!-- end page title end breadcrumb -->
        <div class="row">
            <div class="col-lg-9">
                <div class="row justify-content-center">
                    

                        <div class="row">
                            <div class="col-lg-5   form-group">
                                <label class="text  font-weight-bolder">
                                    {{ $t('Benificary.FromDate') }}
                                </label>
                                <datepicker v-model="fromDate1" :key="render" />

                            </div>
                            <div class="col-lg-5   form-group">
                                <label class="text  font-weight-bolder">
                                    {{ $t('Benificary.ToDate') }}
                                </label>
                                <datepicker v-model="toDate1" :key="render" />
                            </div>

                            <div class="col-lg-2 pt-2 mt-2 form-group">
                                <button class="btn btn-primary" @click="GetOpeningBalanceQuery">Fetch Data</button>
                            </div>
                        </div>


                        
                           
                                <div class="col-md-6 col-lg-3">
                                    <div class="card report-card">
                                        <div class="card-body">
                                            <div class="row d-flex justify-content-center">
                                                <div class="row">
                                                    <p class="text-dark mb-0 fw-semibold">
                                                        Opening Balnece</p>
                                                    <h3 class="m-0">{{ dashboard3.openingBalance }}</h3>
                                                </div>
                                                <div class="col-auto align-self-center">
                                                    <div class="report-main-icon bg-light-alt">
                                                        <i data-feather="users"
                                                            class="align-self-center text-muted icon-sm"></i>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--end card-body-->
                                    </div>
                                    <!--end card-->
                                </div>
                                <div class="col-md-6 col-lg-3">
                                    <div class="card report-card">
                                        <div class="card-body">
                                            <div class="row d-flex justify-content-center">
                                                <div class="row">
                                                    <p class="text-dark mb-0 fw-semibold">
                                                        Funds Recevied</p>
                                                    <h3 class="m-0">{{ dashboard3.totalFunds }}</h3>

                                                </div>
                                                <div class="col-auto align-self-center">
                                                    <div class="report-main-icon bg-light-alt">
                                                        <i data-feather="users" class="align-self-center text-muted icon-sm"></i>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--end card-body-->
                                    </div>
                                    <!--end card-->
                                </div>
                                <div class="col-md-6 col-lg-3">
                                    <div class="card report-card">
                                        <div class="card-body">
                                            <div class="row d-flex justify-content-center">
                                                <div class="row">
                                                    <p class="text-dark mb-0 fw-semibold">
                                                        beneficiary payment
                                                    </p>
                                                    <h3 class="m-0">{{ dashboard3.beneficiaryPayment }}</h3>

                                                </div>
                                                <div class="col-auto align-self-center">
                                                    <div class="report-main-icon bg-light-alt">
                                                        <i data-feather="users"
                                                            class="align-self-center text-muted icon-sm"></i>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--end card-body-->
                                    </div>
                                    <!--end card-->
                                </div>
                                <div class="col-md-6 col-lg-3">
                                    <div class="card report-card">
                                        <div class="card-body">
                                            <div class="row d-flex justify-content-center">
                                                <div class="row">
                                                    <p class="text-dark mb-0 fw-semibold">
                                                        Closing Balnce</p>
                                                    <h3 class="m-0">{{ dashboard3.closingBalance }}</h3>
                                                </div>
                                                <div class="col-auto align-self-center">
                                                    <div class="report-main-icon bg-light-alt">
                                                        <i data-feather="users"
                                                            class="align-self-center text-muted icon-sm"></i>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--end card-body-->
                                    </div>
                                    <!--end card-->
                                </div>



                            
                        

                    


                    <div class="col-md-6 col-lg-3">
                        <div class="card report-card">
                            <div class="card-body">
                                <div class="row d-flex justify-content-center">
                                    <div class="row">
                                        <p class="text-dark mb-0 fw-semibold">{{ $t('Analytics.Users') }}</p>
                                        <h3 class="m-0">{{ dashboard.totalUser }}</h3>
                                    </div>
                                    <div class="col-auto align-self-center">
                                        <div class="report-main-icon bg-light-alt">
                                            <i data-feather="users" class="align-self-center text-muted icon-sm"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end card-body-->
                        </div>
                        <!--end card-->
                    </div>
                    <!--end col-->
                    <div class="col-md-6 col-lg-3">
                        <div class="card report-card">
                            <div class="card-body">
                                <div class="row d-flex justify-content-center">
                                    <div class="row">
                                        <p class="text-dark mb-0 fw-semibold">{{ $t('Analytics.Benificary') }}</p>
                                        <h3 class="m-0">{{ dashboard.totalBenificary }}</h3>
                                    </div>
                                    <div class="col-auto align-self-center">
                                        <div class="report-main-icon bg-light-alt">
                                            <i data-feather="users" class="align-self-center text-muted icon-sm"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end card-body-->
                        </div>
                        <!--end card-->
                    </div>
                    <div class="col-md-6 col-lg-3">
                        <div class="card report-card">
                            <div class="card-body">
                                <div class="row d-flex justify-content-center">
                                    <div class="row">
                                        <p class="text-dark mb-0 fw-semibold">{{ $t('Analytics.RegisterBenificary') }}</p>
                                        <h3 class="m-0">{{ dashboard.registerBenificary }}</h3>

                                    </div>
                                    <div class="col-auto align-self-center">
                                        <div class="report-main-icon bg-light-alt">
                                            <i data-feather="users" class="align-self-center text-muted icon-sm"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end card-body-->
                        </div>
                        <!--end card-->
                    </div>
                    <div class="col-md-6 col-lg-3">
                        <div class="card report-card">
                            <div class="card-body">
                                <div class="row d-flex justify-content-center">
                                    <div class="row">
                                        <p class="text-dark mb-0 fw-semibold">{{ $t('Analytics.Un-RegisterBenificary') }}
                                        </p>
                                        <h3 class="m-0">{{ dashboard.unRegisterBenificary }}</h3>

                                    </div>
                                    <div class="col-auto align-self-center">
                                        <div class="report-main-icon bg-light-alt">
                                            <i data-feather="users" class="align-self-center text-muted icon-sm"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end card-body-->
                        </div>
                        <!--end card-->
                    </div>
                    <div class="col-md-6 col-lg-3">
                        <div class="card report-card">
                            <div class="card-body">
                                <div class="row d-flex justify-content-center">
                                    <div class="row">
                                        <p class="text-dark mb-0 fw-semibold">{{ $t('Analytics.AuthorizePerson') }}</p>
                                        <h3 class="m-0">{{ dashboard.totalAuthorizePerson }}</h3>
                                    </div>
                                    <div class="col-auto align-self-center">
                                        <div class="report-main-icon bg-light-alt">
                                            <i data-feather="users" class="align-self-center text-muted icon-sm"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end card-body-->
                        </div>
                        <!--end card-->
                    </div>
                    <div class="col-md-6 col-lg-3">
                        <div class="card report-card">
                            <div class="card-body">
                                <div class="row d-flex justify-content-center">
                                    <div class="row">
                                        <!-- <p class="text-dark mb-0 fw-semibold">{{ $t('ApprovalPerson.ApprovalPerson') }}</p> -->
                                        <p class="text-dark mb-0 fw-semibold">InComing </p>
                                        <h3 class="m-0">{{ dashboard.cashierTotalIncoming }}</h3>
                                    </div>
                                    <div class="col-auto align-self-center">
                                        <div class="report-main-icon bg-light-alt">
                                            <i data-feather="users" class="align-self-center text-muted icon-sm"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end card-body-->
                        </div>
                        <!--end card-->
                    </div>
                    <!--end col-->
                    <div class="col-md-6 col-lg-3" v-if="rolename == 'Admin'">
                        <div class="card report-card">
                            <div class="card-body">
                                <div class="row d-flex justify-content-center">
                                    <div class="row">
                                        <p class="text-dark mb-0 fw-semibold">{{ $t('Analytics.Resource') }}</p>
                                        <h3 class="m-0">{{ dashboard.totalResources }}</h3>

                                    </div>
                                    <div class="col-auto align-self-center">
                                        <div class="report-main-icon bg-light-alt">
                                            <i data-feather="briefcase" class="align-self-center text-muted icon-sm"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end card-body-->
                        </div>
                        <!--end card-->
                    </div>
                    <div class="col-md-6 col-lg-3" v-if="rolename == 'Cashier'">
                        <div class="card report-card">
                            <div class="card-body">
                                <div class="row d-flex justify-content-center">
                                    <div class="row">
                                        <p class="text-dark mb-0 fw-semibold">{{ $t('Analytics.Resource') }}</p>
                                        <h3 class="m-0">{{ dashboard.cashierTotalIncoming }}</h3>

                                    </div>
                                    <div class="col-auto align-self-center">
                                        <div class="report-main-icon bg-light-alt">
                                            <i data-feather="briefcase" class="align-self-center text-muted icon-sm"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end card-body-->
                        </div>
                        <!--end card-->
                    </div>
                    <div class="col-md-6 col-lg-3" v-if="rolename == 'Admin'">
                        <div class="card report-card">
                            <div class="card-body">
                                <div class="row d-flex justify-content-center">
                                    <div class="row">
                                        <p class="text-dark mb-0 fw-semibold">{{ $t('Analytics.Outgoing') }}</p>
                                        <h3 class="m-0">{{ dashboard.totalOutgoing }}</h3>
                                    </div>
                                    <div class="col-auto align-self-center">
                                        <div class="report-main-icon bg-light-alt">
                                            <i data-feather="briefcase" class="align-self-center text-muted icon-sm"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end card-body-->
                        </div>
                        <!--end card-->
                    </div>
                    <div class="col-md-6 col-lg-3" v-if="rolename == 'Cashier'">
                        <div class="card report-card">
                            <div class="card-body">
                                <div class="row d-flex justify-content-center">
                                    <div class="row">
                                        <p class="text-dark mb-0 fw-semibold">{{ $t('Analytics.Outgoing') }}</p>
                                        <h3 class="m-0">{{ dashboard.cashierTotalOutgoing }}</h3>
                                    </div>
                                    <div class="col-auto align-self-center">
                                        <div class="report-main-icon bg-light-alt">
                                            <i data-feather="briefcase" class="align-self-center text-muted icon-sm"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end card-body-->
                        </div>
                        <!--end card-->
                    </div>

                    <!--end col-->
                </div>
            </div>
            <!--end col-->
            <div class="col-lg-3">
                <div class="card">
                    <div class="card-header">
                        <div class="row align-items-center">
                            <div class="col">
                                <h4 class="card-title">{{ $t('Analytics.BenificaryType') }}</h4>
                            </div>
                        </div>
                        <!--end row-->
                    </div>
                    <!--end card-header-->
                    <div class="card-body">
                        <div class="text-center">
                            <apexchart type="polarArea" width="260" :options="chartOptions2" :series="series2"></apexchart>
                            <h6 class="bg-light-alt py-3 px-2 mb-0">
                                <i data-feather="calendar" class="align-self-center icon-xs me-1"></i>
                                {{ date }}
                            </h6>
                        </div>

                        <!--end /div-->
                    </div>
                    <!--end card-body-->
                </div>
                <!--end card-->
            </div>

            <div class="col-lg-9">
                <ul class="nav nav-pills nav-justified" role="tablist">
                    <li class="nav-item waves-effect waves-light">
                        <a class="nav-link active" data-bs-toggle="tab" href="#profile-1" role="tab" aria-selected="true">{{
                            $t('Analytics.BenificaryType') }}</a>
                    </li>
                    <li class="nav-item waves-effect waves-light">
                        <a class="nav-link" data-bs-toggle="tab" href="#home-1" role="tab" aria-selected="false"
                            v-on:click="GetYearWiseTransaction()">{{ $t('Analytics.YearWisePaymentOutgoing') }}</a>
                    </li>

                    <li class="nav-item waves-effect waves-light">
                        <a class="nav-link" data-bs-toggle="tab" href="#settings-1" role="tab" aria-selected="false"
                            v-on:click="GetPaymentTypeWiseTransaction()">{{ $t('Analytics.PaymentTypesWiseoutgoing') }}</a>
                    </li>
                    <!-- <li class="nav-item waves-effect waves-light">
                        <a class="nav-link" data-bs-toggle="tab" href="#settings-2" role="tab" aria-selected="false">Opening
                            Balance</a>
                    </li> -->
                </ul>
                <div class="tab-content">
                    <div class="tab-pane p-3" id="home-1" role="tabpanel">
                        <div class="card">
                            <div class="card-header">
                                <div class="col-auto">
                                    <label>Select Year:</label>
                                    <datepicker v-model="selectedYear" v-on:input="SelectedYear2(selectedYear)"
                                        :type="'year'" />
                                </div>
                            </div>
                            <div class="card-body">
                                <apexchart type="line" height="350" :options="chartOptions" :series="series" :key="reender">
                                </apexchart>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane p-3 active" id="profile-1" role="tabpanel">
                        <!--end row-->
                        <div class="card">
                            <div class="card-header">
                                <div class="row align-items-center">
                                    <div class="col">
                                        <h4 class="card-title">{{ $t('Analytics.BenificaryType') }}</h4>
                                    </div>
                                    <!--end col-->
                                    <div class="col-auto">
                                        <!-- <div class="dropdown">
                                    <a href="#" class="btn btn-sm btn-outline-light dropdown-toggle"
                                        data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                        All<i class="las la-angle-down ms-1"></i>
                                    </a>
                                    <div class="dropdown-menu dropdown-menu-end">
                                        <a class="dropdown-item" href="#">Indefinate</a>
                                        <a class="dropdown-item" href="#">Customize</a>
                                    </div>
                                </div> -->
                                    </div>
                                    <!--end col-->
                                </div>
                                <!--end row-->
                            </div>
                            <!--end card-header-->
                            <div class="card-body">
                                <div class="text-center">
                                    <apexchart type="line" height="350" :options="chartOptions3" :series="series3"
                                        :key="render">
                                    </apexchart>
                                </div>

                                <!--end /div-->
                            </div>
                            <!--end card-body-->
                        </div>
                    </div>
                    <div class="tab-pane p-3" id="settings-1" role="tabpanel">
                        <div class="card">
                            <div class="card-header">
                                <div class="row align-items-center">
                                    <div class="col">
                                        <h4 class="card-title">{{ $t('Analytics.PaymentTypesWiseoutgoing') }}</h4>
                                    </div>
                                    <!--end col-->
                                    <!-- <div class="col-auto">
                                <div class="dropdown">
                                    <a href="#" class="btn btn-sm btn-outline-light dropdown-toggle"
                                        data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                        All<i class="las la-angle-down ms-1"></i>
                                    </a>
                                    <div class="dropdown-menu dropdown-menu-end">
                                        <a class="dropdown-item" href="#">Indefinate</a>
                                        <a class="dropdown-item" href="#">Customize</a>
                                    </div>
                                </div>
                            </div> -->
                                    <!--end col-->
                                </div>
                                <!--end row-->
                            </div>
                            <!--end card-header-->
                            <div class="card-body">
                                <div class="text-center">
                                    <apexchart type="line" height="350" :options="chartOptions4" :series="series4"
                                        :key="reeender">
                                    </apexchart>
                                </div>

                                <!--end /div-->
                            </div>
                            <!--end card-body-->
                        </div>
                    </div>
                    <div class="tab-pane p-3 " id="settings-2" role="tabpanel">
                        <div class="row mt-3">
                            <div class="col-lg-12">
                                <div class="row">
                                    <div class="col-lg-12">


                                        <div class="card">

                                            <div class="row">
                                                <div class="col-lg-5 pl-2 mt-3 form-group">
                                                    <label class="text  font-weight-bolder">
                                                        {{ $t('Benificary.FromDate') }}
                                                    </label>
                                                    <datepicker v-model="fromDate1" :key="render" />

                                                </div>
                                                <div class="col-lg-5  mt-3 form-group">
                                                    <label class="text  font-weight-bolder">
                                                        {{ $t('Benificary.ToDate') }}
                                                    </label>
                                                    <datepicker v-model="toDate1" :key="render" />
                                                </div>

                                                <div class="col-lg-2 pt-2 mt-4 form-group">
                                                    <button class="btn btn-primary" @click="GetOpeningBalanceQuery">Fetch
                                                        Data</button>
                                                </div>
                                            </div>


                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-md-6 col-lg-3">
                                                        <div class="card report-card">
                                                            <div class="card-body">
                                                                <div class="row d-flex justify-content-center">
                                                                    <div class="row">
                                                                        <p class="text-dark mb-0 fw-semibold">
                                                                            Opening Balnece</p>
                                                                        <h3 class="m-0">{{ dashboard3.openingBalance }}</h3>
                                                                    </div>
                                                                    <div class="col-auto align-self-center">
                                                                        <div class="report-main-icon bg-light-alt">
                                                                            <i data-feather="users"
                                                                                class="align-self-center text-muted icon-sm"></i>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <!--end card-body-->
                                                        </div>
                                                        <!--end card-->
                                                    </div>
                                                    <div class="col-md-6 col-lg-3">
                                                        <div class="card report-card">
                                                            <div class="card-body">
                                                                <div class="row d-flex justify-content-center">
                                                                    <div class="row">
                                                                        <p class="text-dark mb-0 fw-semibold">
                                                                            Funds Recevied</p>
                                                                        <h3 class="m-0">{{ dashboard3.totalFunds }}</h3>

                                                                    </div>
                                                                    <div class="col-auto align-self-center">
                                                                        <div class="report-main-icon bg-light-alt">
                                                                            <i data-feather="users"
                                                                                class="align-self-center text-muted icon-sm"></i>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <!--end card-body-->
                                                        </div>
                                                        <!--end card-->
                                                    </div>
                                                    <div class="col-md-6 col-lg-3">
                                                        <div class="card report-card">
                                                            <div class="card-body">
                                                                <div class="row d-flex justify-content-center">
                                                                    <div class="row">
                                                                        <p class="text-dark mb-0 fw-semibold">
                                                                            beneficiary payment
                                                                        </p>
                                                                        <h3 class="m-0">{{ dashboard3.beneficiaryPayment }}
                                                                        </h3>

                                                                    </div>
                                                                    <div class="col-auto align-self-center">
                                                                        <div class="report-main-icon bg-light-alt">
                                                                            <i data-feather="users"
                                                                                class="align-self-center text-muted icon-sm"></i>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <!--end card-body-->
                                                        </div>
                                                        <!--end card-->
                                                    </div>
                                                    <div class="col-md-6 col-lg-3">
                                                        <div class="card report-card">
                                                            <div class="card-body">
                                                                <div class="row d-flex justify-content-center">
                                                                    <div class="row">
                                                                        <p class="text-dark mb-0 fw-semibold">
                                                                            Closing Balnce</p>
                                                                        <h3 class="m-0">{{ dashboard3.closingBalance }}</h3>
                                                                    </div>
                                                                    <div class="col-auto align-self-center">
                                                                        <div class="report-main-icon bg-light-alt">
                                                                            <i data-feather="users"
                                                                                class="align-self-center text-muted icon-sm"></i>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <!--end card-body-->
                                                        </div>
                                                        <!--end card-->
                                                    </div>



                                                </div>
                                            </div>

                                        </div>
                                        <!--end row-->
                                    </div>
                                </div>
                            </div><!--end col-->
                        </div>
                    </div>
                </div>
            </div>


        </div>
        <!--end row-->
        <loading :active.sync="loading" :can-cancel="true" :is-full-page="false"></loading>
    </div>
</template>

<script>
import clickMixin from '@/Mixins/clickMixin'
import moment from "moment";
//import 'moment/locale/en'; // Import Arabic locale

import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/vue-loading.css';

export default {

    mixins: [clickMixin],
    components: {
        Loading,
    },
    data: function () {

        return {
            dashboard1: '',
            dashboard3: '',
            paymentWiseTransactionList: '',
            loading: false,
            userId: '',
            rolename: '',
            dashboard: '',
            active: 'Dashboard',
            overView: 'Monthly',
            currency: '',
            randerChart: 0,
            income: 0,
            date: '',
            render: 0,
            reender: 0,
            reeender: 0,
            randerDropdown: 0,
            fromDate: moment().format("DD MMM YYYY"),
            toDate: Date(),
            fromDate1: '',
            toDate1: '',
            selectedYear: null,
            years: [],
            series: [{
                name: 'Total Amount',
                data: []
            }],
            chartOptions: {
                chart: {
                    height: 350,
                    type: 'line',
                    zoom: {
                        enabled: false
                    }
                },
                dataLabels: {
                    enabled: false
                },
                stroke: {
                    curve: 'straight'
                },
                title: {
                    text: 'Year Wise Payment Outgoing',
                    align: 'left'
                },
                grid: {
                    row: {
                        colors: ['#f3f3f3', 'transparent'], // takes an array which will be repeated on columns
                        opacity: 0.5
                    },
                },
                xaxis: {
                    categories: [],
                }
            },
            series2: [],
            chartOptions2: {
                chart: {
                    width: 380,
                    type: 'polarArea'
                },
                labels: ['Total', 'Register', 'UnRegister'],
                fill: {
                    opacity: 1
                },
                stroke: {
                    width: 1,
                    colors: undefined
                },
                yaxis: {
                    show: false
                },
                legend: {
                    position: 'bottom'
                },
                plotOptions: {
                    polarArea: {
                        rings: {
                            strokeWidth: 0
                        },
                        spokes: {
                            strokeWidth: 0
                        },
                    }
                },
                theme: {
                    monochrome: {
                        enabled: true,
                        shadeTo: 'light',
                        shadeIntensity: 0.6
                    }
                }
            },
            series3: [{
                name: 'Indefinate',
                type: 'column',
                data: []
            }, {
                name: 'Customize',
                type: 'line',
                data: []
            }],
            chartOptions3: {
                chart: {
                    height: 350,
                    type: 'line',
                },
                stroke: {
                    width: [0, 4]
                },
                title: {
                    text: ''
                },
                dataLabels: {
                    enabled: false,
                    // enabledOnSeries: [1]
                },
                xaxis: {
                    categories: ['One Time', '1 Month', '2 Months', '3 Months', '4 Months', '5 Months', '6 Months', '7 Months', '8 Months', '9 Months', '10 Months', '11 Months', '12 Months'],
                },
                yaxis: [{
                    title: {
                        text: 'Indefinate',
                    },

                },
                {
                    opposite: true,
                    title: {
                        text: 'Customize',
                    }
                }]
            },
            series4: [{
                name: 'Total Amount',
                type: 'column',
                data: []
            }],
            chartOptions4: {
                chart: {
                    height: 350,
                    type: 'line',

                },
                stroke: {
                    width: [0, 4]
                },
                dataLabels: {
                    enabled: false
                },

                title: {
                    text: 'Payment Type Wise Outgoing',
                    align: 'left'
                },
                xaxis: {
                    categories: [],
                }
            },

        }
    },
    watch: {

    },
    methods: {

        SelectedYear2: function (year) {
            this.getDashboardChartsData(year);
        },

        makeActive: function (item) {

            this.active = item;
        },
        OverViewFilterFunction: function () {

        },

        getDate: function (date) {
            return moment(date).format('l');
        },

        GetYearWiseTransaction: function () {
            var currentDate = moment();
            var date = currentDate.format('DD MMM YYYY');
            this.getDashboardChartsData(date);
        },




        getDashboardData: function () {
            var root = this;
            var token = '';
            this.loading = true;
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }

            this.userId = localStorage.getItem('UserId');

            root.$https.get(`Benificary/GetDashboardDetail?userId=` + this.userId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.loading = false;

                    root.dashboard = response.data;

                    root.series3[0].data = [];
                    root.series3[1].data = [];
                    root.chartOptions3.xaxis.categories = [];

                    response.data.benificaryPaymentType.forEach(function (result) {
                        root.series3[0].data.push(result.indefinate);
                        root.series3[1].data.push(result.customize);
                        root.chartOptions3.xaxis.categories.push(result.paymentType);
                    });

                    root.series2.push(response.data.totalBenificary, response.data.registerBenificary, response.data.unRegisterBenificary);
                }
                root.loading = false;
                root.render++;
            }).catch(function (error) {
                console.error(error);
            });
        },

        getDashboardChartsData: function (year) {
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            this.loading = true;
            root.$https.get(`Benificary/GetDashboardChartsDetail?year=` + year, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.loading = false;

                    root.dashboard1 = response.data;
                    root.series[0].data = [];
                    root.chartOptions.xaxis.categories = [];
                    response.data.monthList.forEach(function (result) {
                        root.series[0].data.push(result.amount);
                        root.chartOptions.xaxis.categories.push(result.monthName);
                    });

                }
                root.reender++;
            }).catch(function (error) {
                console.error(error);
            });

        },

        GetPaymentTypeWiseTransaction: function () {
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }

            this.loading = true;
            root.$https.get(`Benificary/PaymentTypeWiseTransaction`, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.loading = false;

                    root.paymentWiseTransactionList = response.data;
                    root.series4[0].data = [];
                    root.chartOptions4.xaxis.categories = [];

                    root.paymentWiseTransactionList.forEach(function (result) {
                        root.series4[0].data.push(result.amount);
                        root.chartOptions4.xaxis.categories.push(result.paymentTypeName);
                    });

                }

                root.reeender++;
            }).catch(function (error) {
                console.error(error);
            });

        },




        GetOpeningBalanceQuery: function () {
            debugger;
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            this.loading = true;

            if (this.fromDate1 && this.toDate1) {
                root.$https.get('Benificary/OpeningBalanceQuery?fromDate1=' + this.fromDate1 + '&toDate1=' + this.toDate1, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.loading = false;
                        debugger;

                        root.dashboard3 = response.data;



                    }

                    root.reeender++;
                }).catch(function (error) {
                    console.error(error);
                });

            }

        }



    },

    created: function () {

    },
    mounted: function () {

        this.currency = localStorage.getItem('currency');

        this.fromDate = moment().startOf('month').format("DD MMM YYYY");
        this.fromDate1 = moment().subtract(1, 'months').format("DD MMM YYYY");
        this.toDate1 = moment().format("DD MMM YYYY");

        if (this.$session.exists()) {
            this.userId = localStorage.getItem('UserId');
            this.employeeId = localStorage.getItem('EmployeeId');
            this.fromDate = moment().startOf('month').format("DD MMM YYYY");

        }

        this.search = moment().format("DD MMM YYYY");

        this.chartbymonth = moment().format("DD MMM YYYY");
        this.getDashboardData();

        this.GetOpeningBalanceQuery();

        this.rolename = localStorage.getItem('RoleName')

        // this.years.push(moment().year());
        // this.years.push(moment().subtract(1, 'year').year());
        // this.years.push(moment().subtract(2, 'year').year());
        // this.years.push(moment().subtract(3, 'year').year());
        // this.years.push(moment().subtract(4, 'year').year());
        // this.years.push(moment().subtract(5, 'year').year());

        this.GetPaymentTypeWiseTransaction();



        const now = new Date();
        this.date = now.toLocaleDateString('en-US', {
            month: 'short',
            day: 'numeric',
            year: 'numeric'
        });
    },
}
</script>
