<template>
    <div>
        <multiselect v-model="DisplayValue" v-if="isDisable == 'true'"  disabled :options="options" :searchable="false" :multiple="false" :placeholder="$t('General.SelectExpenseCategory')" track-by="name" :clear-on-select="false" :show-labels="false" label="name" v-bind:class="$i18n.locale == 'en' ? 'text-left ' : 'multiselect__placeholder12'">

        </multiselect>      
        <multiselect v-model="DisplayValue" v-else :options="options" :searchable="false" :multiple="false" :placeholder="$t('General.SelectExpenseCategory')" track-by="name" :clear-on-select="false" :show-labels="false" label="name" v-bind:class="$i18n.locale == 'en' ? 'text-left ' : 'multiselect__placeholder12'">
        </multiselect>      
    </div>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import Multiselect from 'vue-multiselect';
export default {
    props: ["values", "isDisable", 'isMultiple'],
    mixins: [clickMixin],

    components: {
        Multiselect
    },
    data: function () {
        return {
            arabic: '',
            english: '',
            options: [],
            value: '',
            show: false,
            loading: false,
            type: '',
        }
    },
    methods: {
        Remove: function () {
            this.value='';

        },
        getData: function () {
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.options = [];
            this.$https.get('/Benificary/GetExpenseCategoryList?isDropDown=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    response.data.results.forEach(function (cat) {
                        root.options.push({
                            id: cat.id,
                            name: cat.categoryName,
                         
                        })
                    })
                }
            }).then(function () {
                root.value = root.options.find(function (x) {
                     
                    return x.id == root.values;
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
                if (value == null || value == undefined) {
                    this.$emit('input', value);

                }
                else {
                    if (this.isMultiple == true) {
                        this.$emit('input', value);
                    }
                    else {
                        this.$emit('input', value.id);
                    }

                }
            }
        }
    },
    mounted: function () {
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        this.getData();
    },
}
</script>