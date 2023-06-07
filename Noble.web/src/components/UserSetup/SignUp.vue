<template>
    <div class="row">        
        <div class="col-lg-12">
            <div class="row">
                <div class="col-lg-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('SignUp.SignUpDetails') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('SignUp.SignUpDetails') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a  v-on:click="AddSignup" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1" v-if ="roleName != 'User'">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/dashboard')" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                                    {{ $t('Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-header">
                    <div class="input-group">
                        <button class="btn btn-secondary" type="button" id="button-addon1"><i class="fas fa-search"></i></button>
                        <input v-model="searchQuery" type="text" class="form-control" :placeholder="$t('SignUp.SearchBYNameAndEmail')" aria-label="Example text with button addon" aria-describedby="button-addon1">
                    </div>
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>#</th>

                                    <th>
                                        {{ $t('SignUp.USERNAME') }}
                                    </th>
                                    <th>
                                        {{ $t('SignUp.EMAILID') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(details,index) in resultQuery" v-bind:key="details.id">
                                    <td>
                                        {{index+1}}
                                    </td>
                                    <td >
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditInfo(details.id,false)">{{details.fullName}}</a>
                                        </strong>
                                    </td>
                                    <td>{{details.email}}</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>

                </div>
            </div>
        </div>
    </div>
   
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        name: 'signUp',
        data: function () {
            return {
                searchQuery: '',
                show: false,
                loading: false,
                loginList:[

                ],
                user: {
                    id: '',
                    isActive: '',
                    isUser: true,
                    user:'',
                },

            }
        },
        computed: {
            resultQuery: function () {
                var root = this;
                if (this.searchQuery) {
                    return this.loginList.filter((city) => {
                        return root.searchQuery.toLowerCase().split(' ').every(v => city.email.toLowerCase().includes(v) || city.fullName.toLowerCase().includes(v))
                    })
                } else {
                    return root.loginList;
                }
            },
        },

        methods: {
            AddSignup: function () {
               this.$router.push('/AddSignUp') 
            },
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            GetData: function () {
                var root = this;
                var url = '/account/UserList';
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                root.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.loginList = response.data;
                    }
                });
            },
           
            EditInfo: function (id) {
                
                var root = this;
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/account/UserDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                      
                         {
                            root.$router.push({
                                path: '/AddSignUp',
                                query: { data: response.data }
                            })
                        }
                        
                    }
                });
                
            },
            DeleteData: function (id) {
                var root = this;
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/account/UserDelete?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        
                        root.$swal.fire({
                            icon:'warning',
                            title:'Deleted Successfully',
                            showConfirmButton: false,
                            timer: 800,
                            timerProgressBar: true,
                        });
                        root.GetData();
                    }
                });
            },

           

        },
       
        mounted: function () {
            this.GetData();
            this.roleName = localStorage.getItem('RoleName');
        }
    }
</script>