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
                    <div class="col-md-6 col-lg-3">
                        <div class="card report-card">
                            <div class="card-body">
                                <div class="row d-flex justify-content-center">
                                    <div class="col">
                                        <p class="text-dark mb-0 fw-semibold">{{ $t('Analytics.Users') }}</p>
                                        <h3 class="m-0">{{ dashboard.totalUser }}</h3>
                                        <p class="mb-0 text-truncate text-muted"><span class="text-success"><i
                                                    class="mdi mdi-trending-up"></i>8.5%</span> New Sessions Today</p>
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
                                    <div class="col">
                                        <p class="text-dark mb-0 fw-semibold">{{ $t('Analytics.Benificary') }}</p>
                                        <h3 class="m-0">{{ dashboard.totalBenificary }}</h3>
                                        <p class="mb-0 text-truncate text-muted"><span class="text-success"><i
                                                    class="mdi mdi-trending-up"></i>1.5%</span> Weekly Avg.Sessions</p>
                                    </div>
                                    <div class="col-auto align-self-center">
                                        <div class="report-main-icon bg-light-alt">
                                            <i data-feather="clock" class="align-self-center text-muted icon-sm"></i>
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
                                    <div class="col">
                                        <p class="text-dark mb-0 fw-semibold">{{ $t('Analytics.RegisterBenificary') }}</p>
                                        <h3 class="m-0">{{ dashboard.registerBenificary }}</h3>
                                        <p class="mb-0 text-truncate text-muted"><span class="text-success"><i
                                                    class="mdi mdi-trending-up"></i>1.5%</span> Weekly Avg.Sessions</p>
                                    </div>
                                    <div class="col-auto align-self-center">
                                        <div class="report-main-icon bg-light-alt">
                                            <i data-feather="clock" class="align-self-center text-muted icon-sm"></i>
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
                                    <div class="col">
                                        <p class="text-dark mb-0 fw-semibold">{{ $t('Analytics.Un-RegisterBenificary') }}
                                        </p>
                                        <h3 class="m-0">{{ dashboard.unRegisterBenificary }}</h3>
                                        <p class="mb-0 text-truncate text-muted"><span class="text-success"><i
                                                    class="mdi mdi-trending-up"></i>1.5%</span> Weekly Avg.Sessions</p>
                                    </div>
                                    <div class="col-auto align-self-center">
                                        <div class="report-main-icon bg-light-alt">
                                            <i data-feather="clock" class="align-self-center text-muted icon-sm"></i>
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
                                    <div class="col">
                                        <p class="text-dark mb-0 fw-semibold">{{ $t('Analytics.OneTimeBenificary') }}</p>
                                        <h3 class="m-0">{{ dashboard.oneTimeBenificary }}</h3>
                                        <p class="mb-0 text-truncate text-muted"><span class="text-success"><i
                                                    class="mdi mdi-trending-up"></i>1.5%</span> Weekly Avg.Sessions</p>
                                    </div>
                                    <div class="col-auto align-self-center">
                                        <div class="report-main-icon bg-light-alt">
                                            <i data-feather="clock" class="align-self-center text-muted icon-sm"></i>
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
                                    <div class="col">
                                        <p class="text-dark mb-0 fw-semibold">{{ $t('Analytics.MonthlyBenificary') }}</p>
                                        <h3 class="m-0">{{ dashboard.monthlyBenificary }}</h3>
                                        <p class="mb-0 text-truncate text-muted"><span class="text-success"><i
                                                    class="mdi mdi-trending-up"></i>1.5%</span> Weekly Avg.Sessions</p>
                                    </div>
                                    <div class="col-auto align-self-center">
                                        <div class="report-main-icon bg-light-alt">
                                            <i data-feather="clock" class="align-self-center text-muted icon-sm"></i>
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
                                    <div class="col">
                                        <p class="text-dark mb-0 fw-semibold">{{ $t('Analytics.AuthorizePerson') }}</p>
                                        <h3 class="m-0">{{ dashboard.totalAuthorizePerson }}</h3>
                                        <p class="mb-0 text-truncate text-muted"><span class="text-danger"><i
                                                    class="mdi mdi-trending-down"></i>35%</span> Bounce Rate Weekly</p>
                                    </div>
                                    <div class="col-auto align-self-center">
                                        <div class="report-main-icon bg-light-alt">
                                            <i data-feather="activity" class="align-self-center text-muted icon-sm"></i>
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
                                    <div class="col">
                                        <p class="text-dark mb-0 fw-semibold">{{ $t('Analytics.Resource') }}</p>
                                        <h3 class="m-0">{{ dashboard.totalResources }}</h3>
                                        <p class="mb-0 text-truncate text-muted"><span class="text-success"><i
                                                    class="mdi mdi-trending-up"></i>10.5%</span> Completions Weekly</p>
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
                <!--end row-->
                <div class="card">
                    <div class="card-header">
                        <div class="row align-items-center">
                            <div class="col">
                                <h4 class="card-title">{{ $t('Analytics.BenificaryType') }}</h4>
                            </div>
                            <!--end col-->
                            <div class="col-auto">
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
                            </div>
                            <!--end col-->
                        </div>
                        <!--end row-->
                    </div>
                    <!--end card-header-->
                    <div class="card-body">
                        <div class="text-center">
                            <apexchart type="line" height="350" :options="chartOptions3" :series="series3"></apexchart>
                            <!-- <h6 class="bg-light-alt py-3 px-2 mb-0">
                                <i data-feather="calendar" class="align-self-center icon-xs me-1"></i>
                                01 January 2020 to 31 December 2020
                            </h6> -->
                        </div>

                        <!--end /div-->
                    </div>
                    <!--end card-body-->
                </div>

                <!--end card-->
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
                <div class="card">
                    <div class="card-header">
                        <div class="row align-items-center">
                            <div class="col">
                                <h4 class="card-title">{{ $t('Analytics.AudienceOverview') }}</h4>
                            </div>
                            <!--end col-->
                            <!-- <div class="col-auto">
                                <div class="dropdown">
                                    <a href="#" class="btn btn-sm btn-outline-light dropdown-toggle"
                                        data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                        Select Year<i class="las la-angle-down ms-1"></i>
                                    </a>
                                    
                                </div>
                            </div> -->
                            <!--end col-->
                        </div>
                        <!--end row-->
                    </div>
                    <!--end card-header-->
                    <div class="card-body">
                        <div class="">
                            <apexchart type="line" height="350" :options="chartOptions" :series="series" :key="render">
                            </apexchart>
                        </div>
                    </div>
                    <!--end card-body-->
                </div>
                <!--end card-->
            </div>
            <!--end col-->
        </div>
        <!--end row-->

    </div>
</template>

<script>
import clickMixin from '@/Mixins/clickMixin'
import moment from "moment";

export default {

    mixins: [clickMixin],

    data: function () {

        return {

            dashboard: '',
            active: 'Dashboard',
            overView: 'Monthly',
            currency: '',
            randerChart: 0,
            income: 0,
            date: '',
            render: 0,
            randerDropdown: 0,
            fromDate: moment().format("DD MMM YYYY"),
            toDate: Date(),
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
                    text: 'All Years Charity',
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
                labels: ['Total', 'Register', 'UnRegister', 'One Time', 'Monthly'],
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
                    text: 'Traffic Sources'
                },
                dataLabels: {
                    enabled: true,
                    // enabledOnSeries: [1]
                },
                xaxis: {
                    categories: ['One Time', '1 Month', '2 Months', '3 Months', '4 Months', '5 Months', '6 Months', '7 Months', '8 Months', '9 Months', '10 Months', '11 Months', '12 Months'],
                },
                yaxis: [{
                    title: {
                        text: 'Indefinate',
                    },

                }, {
                    opposite: true,
                    title: {
                        text: 'Customize',
                    }
                }]
            },

        }
    },
    watch: {

    },
    methods: {
        makeActive: function (item) {

            this.active = item;
        },
        OverViewFilterFunction: function () {

        },

        getDate: function (date) {
            return moment(date).format('l');
        },



        getDashboardData: function () {
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            debugger;



            root.$https.get(`Benificary/GetDashboardDetail`, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.dashboard = response.data;
                    root.series[0].data = [];
                    root.chartOptions.xaxis.categories = [];
                    root.series3[0].data = [];
                    root.series3[1].data = [];
                    root.chartOptions3.xaxis.categories = [];

                    response.data.monthList.forEach(function (result) {
                        root.series[0].data.push(result.amount);
                        root.chartOptions.xaxis.categories.push(result.monthName);
                    });

                    response.data.benificaryPaymentType.forEach(function (result) {
                        root.series3[0].data.push(result.indefinate);
                        root.series3[1].data.push(result.customize);
                        //root.chartOptions3.xaxis.categories.push(result.paymentType);
                    });

                    root.series2.push(response.data.totalBenificary, response.data.registerBenificary, response.data.unRegisterBenificary, response.data.oneTimeBenificary, response.data.monthlyBenificary);
                }
                root.loading = false;
                root.render++;
            }).catch(function (error) {
                console.error(error);
            });

        }

    },

    created: function () {

    },
    mounted: function () {

        this.currency = localStorage.getItem('currency');

        this.fromDate = moment().startOf('month').format("DD MMM YYYY");

        if (this.$session.exists()) {
            this.userID = localStorage.getItem('UserID');
            this.employeeId = localStorage.getItem('EmployeeId');
            this.fromDate = moment().startOf('month').format("DD MMM YYYY");

        }

        this.search = moment().format("DD MMM YYYY");

        this.chartbymonth = moment().format("DD MMM YYYY");
        this.getDashboardData();




        const now = new Date();
        this.date = now.toLocaleDateString('en-US', {
            month: 'short',
            day: 'numeric',
            year: 'numeric'
        });
    },
}
</script>
