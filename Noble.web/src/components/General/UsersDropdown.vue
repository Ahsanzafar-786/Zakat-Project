<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :multiple="false"  :placeholder="$t('SelectUser')" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true" >
            <p slot="noResult" class="text-danger">{{ $t('UsersDropdown.NoUserFound') }}</p>
            <!-- <span slot="noResult" class="btn btn-primary " v-on:click="AddRole('Add')">Create new Role</span><br /> -->
        </multiselect>
    </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'
    import clickMixin from '@/Mixins/clickMixin'

    export default {
        mixins: [clickMixin],

        name: 'userRolesdropdown',
        props: ["values", "isloginhistory", "isTransfer", "isSupervisor", 'alluser', 'cashallocation'],
        components: {
            Multiselect
        },
        data: function () {
            return {
                options: [],
                value: '',
                show: false,
                type: '',
                render: 0
            }
        },
        methods: {
            EmptyRecord: function () {
                
                this.DisplayValue='';

            
            },
            getData: function () {
                var root = this;
                var token = '';
                
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/account/UserList', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        
                         {
                            response.data.forEach(function (result) {
                                {
                                    root.options.push({
                                        id: result.id,
                                        name: result.fullName
                                    })
                                }
                            })
                        }
                        
                    }
                }).then(function () {
                    if (root.values != undefined && root.values != '' && root.values != null) {
                        root.value = root.options.find(function (x) {
                            return x.id == root.values;
                        })
                    }
                }).then(function () {
                    root.value = root.options.find(function (x) {
                        return x.id == root.values;
                    })
                });
            }
        },
        computed: {
            DisplayValue: {
                get: function () {
                    if (this.value != "" || this.value != undefined) {
                        return this.value;
                    }
                    return this.values;
                },
                set: function (value) {
                    if(value != null)
                    {
                        if (this.isloginhistory) {
                            this.value = value;
                            this.$emit("input", value);
                        }
                        else {

                            if (this.isTransfer) {
                                this.value = value;
                                this.$emit("input", value.name);
                            }
                            else if (this.isSupervisor) {
                                this.value = value;
                                this.$emit("input", value.name);
                            }
                            else {
                                this.value = value;
                                this.$emit("input", value.id);
                            }

                        }
                        
                    }
                    else{
                        this.value = value;
                        this.$emit("input", null);
                    }
                }
            }
        },
        mounted: function () {
            this.getData();
        }
    }
</script>