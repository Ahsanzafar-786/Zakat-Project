import Vue from 'vue'
import VueRouter from 'vue-router'



import Login from '../views/Login.vue'
import Dashboard from '../views/Dashboard.vue'
import NewPassword from '../views/NewPassword.vue'
import ResetPassword from '../views/ResetPassword.vue'


import clickMixin from '@/Mixins/clickMixin'
Vue.use(clickMixin);

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
                        // {
                        //     path:'/HrDashboard',
                        //     name:'HrDashboard',
                        //     component:HrDashboard,
                        // },
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
    console.log(from.name);
    next();
});