<template>
    <div>
        <multiselect v-bind:disabled="isDisable" v-model="DisplayValue" :options="options" :multiple="false"  :placeholder="$t('RolesDropdown.SelectUser')" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true" >
        </multiselect>
    </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'
    import clickMixin from '@/Mixins/clickMixin'

    export default {
        mixins: [clickMixin],

        name: 'userRolesdropdown',
        props: ["values","isDisable"],
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
            getData: function () {
                 
                var root = this;
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                
                root.$https.get('/account/RolesList', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                         
                        
                        response.data.forEach(function (result) {
                            root.options.push({
                                id: result.id,
                                name: result.name
                            })
                       })
                    }
                }).then(function () {
                    root.value = root.options.find(function (x) {
                        return x.name == root.values;
                    })
                   
                });
            },
            
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
                    this.value = value;
                    this.$emit('input', value.name);
                }
            }
        },
        mounted: function () {
            this.getData();
        }
    }
</script>