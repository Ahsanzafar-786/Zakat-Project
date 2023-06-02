import Vue from 'vue'
import VueRouter from 'vue-router'
import VueSession from 'vue-session'


import Login from '../views/Login.vue'
import Dashboard from '../views/Dashboard.vue'
import NewPassword from '../views/NewPassword.vue'
import ResetPassword from '../views/ResetPassword.vue'

//Profile
import RegisterUser from '../components/RegisterUser.vue'

//Company
import company from '../components/Company/Company.vue'
import addcompany from '../components/Company/AddCompany.vue'

//User Setup
import signup from '../components/UserSetup/SignUp.vue'
import addsignup from '../components/UserSetup/AddSignUp.vue'

//Benificary
import benificaries from '../components/Benificary/Benificaries.vue';

//Authorized Persons
import authorizedpersons from '../components/AuthorizedPersons/AuthorizedPersons.vue'

//Approval Persons
import approvalpersons from '../components/ApprovalPersons/ApprovalPersons.vue'

//Authorized Persons
import chartDashboard from '../components/Dashboard/Dashboard.vue'


//Benificary Note
import benificarynote from '../components/BenificaryNote/BenificaryNote.vue'

//Charity Resources
import charityresource from '../components/CharityResources/CharityResources.vue'

//Payment Type
import paymenttype from '../components/PaymentType/PaymentType.vue'

//Funds
import funds from '../components/Funds/Funds.vue'

//Payment
import payment from '../components/Payment/Payment.vue';
import addpayment from '../components/Payment/AddPayment.vue';

//LedgerReport
import ledgerreport from '../components/Reports/LedgerReport.vue'

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
                    path: '/NewPassword',
                    name: 'NewPassword',
                    component: NewPassword
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
                    path: '/dashboard',
                    name: 'Dashboard',
                    component: Dashboard,
                    children: [
                        {
                            path:'/company',
                            name:'company',
                            component:company,
                        },
                        {
                            path:'/addcompany',
                            name:'addcompany',
                            component:addcompany,
                        },
                        {
                            path:'/signup',
                            name:'signup',
                            component:signup,
                        },
                        {
                            path:'/addsignup',
                            name:'addsignup',
                            component:addsignup,
                        },
                        {
                            path: '/registeruser',
                            name: 'RegisterUser',
                            component: RegisterUser
                        },
                        {
                            path: '/benificaries',
                            name: 'benificaries',
                            component: benificaries
                        },
                        {
                            path: '/authorizedpersons',
                            name: 'authorizedpersons',
                            component: authorizedpersons
                        },
                        {
                            path: '/ChartDashboard',
                            name: 'ChartDashboard',
                            component: chartDashboard
                        },
                        {
                            path: '/benificarynote',
                            name: 'benificarynote',
                            component: benificarynote
                        },
                        {
                            path: '/charityresource',
                            name: 'charityresource',
                            component: charityresource
                        },
                        {
                            path: '/approvalpersons',
                            name: 'approvalpersons',
                            component: approvalpersons
                        },
                        {
                            path: '/paymenttype',
                            name: 'paymenttype',
                            component: paymenttype
                        },
                        {
                            path: '/funds',
                            name: 'funds',
                            component: funds
                        },
                        {
                            path: '/payment',
                            name: 'payment',
                            component: payment
                        },
                        {
                            path: '/addpayment',
                            name: 'addpayment',
                            component: addpayment
                        },
                        {
                            path: '/ledgerreport',
                            name: 'ledgerreport',
                            component: ledgerreport
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
        console.log(from.name);

    }
    next();
});