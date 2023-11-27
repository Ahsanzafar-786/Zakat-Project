<template>
    <div>
        <multiselect  v-model="DisplayValue" :options="options" :multiple="false"
            :placeholder="$t('General.SelectBenificary1')" track-by="name" :clear-on-select="false" :show-labels="false"
            label="name" v-bind:class="$i18n.locale == 'en' ? 'text-left ' : 'multiselect__placeholder12'">

        </multiselect>
    </div>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import Multiselect from 'vue-multiselect';
export default {
    props: ["values", "isTemporary", 'isMultiple'],
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
        // asyncFind: function (query) {
        //     debugger;
        //   let  filteredOptions=   this.options.filter(option =>
        //         option.name.toLowerCase().includes(query.toLowerCase()));

        //     // Sort the filtered options by beneficiaryId in ascending order
        //     filteredOptions.sort((a, b) => a.beneficiaryId - b.beneficiaryId);

        //     // Update the options data property with the sorted results
        //     this.options = filteredOptions;
        // },
       
        getData: function () {
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.options = [];
            this.$https.get('/Benificary/GetBenificaryList?isDropDown=true'+ '&isCode=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    response.data.results.forEach(function (cat) {
                        root.options.push({
                            id: cat.id,
                            name: cat.beneficiaryId,
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