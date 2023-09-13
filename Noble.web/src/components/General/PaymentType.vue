<template>
    <div>
        <multiselect v-bind:disabled="isDisable" v-model="DisplayValue" :options="options" :searchable="false" :multiple="false" :placeholder="$t('General.PaymentType')" track-by="name" :clear-on-select="false" :show-labels="false" label="name"   v-bind:class="$i18n.locale == 'en' ? 'text-left ' : 'multiselect__placeholder12'" >

        </multiselect>      
    </div>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import Multiselect from 'vue-multiselect';
export default {
    props: ["values", "isTemporary", 'isMultiple','isDisable','dailyPayment'],
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
        GetSalaryOfSelected: function () {
                    return this.value;
            },
        getData: function () {
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.options = [];
            debugger;
            this.$https.get('/Benificary/GetPaymentTypeList?isDropDown=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    response.data.results.forEach(function (cat) {
                        root.options.push({
                            id: cat.id,
                            code: cat.code,
                            name: root.$i18n.locale == 'en'? cat.name:cat.nameAr
                        })
                    })
                }
            }).then(function () {
                if(root.dailyPayment)
                { debugger;
                    root.value = root.options.find(function (x) {
                     
                     return x.code == 13;
                    });
                    root.$emit('input',  root.value.id);

                }
                else
                {
                    root.value = root.options.find(function (x) {
                     
                     return x.id == root.values;
                 })
                }
                
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
<style  >
.multiselect__placeholder12 >.multiselect__tags {
    text-align: right !important;
}

.multiselect__placeholder12 >.multiselect__content-wrapper>.multiselect__content {
    text-align: right !important;
}
</style>