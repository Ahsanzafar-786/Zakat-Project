<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :multiple="false" @search-change="asyncFind"
            :placeholder="$t('General.SelectBenificary')" track-by="name" :clear-on-select="false" :show-labels="false"
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
            searchOptions: [],
            value: '',
            show: false,
            loading: false,
            type: '',
        }
    },
    methods: {
        asyncFind: function (searchQuery) {
            debugger;
            if (searchQuery == null || searchQuery == '') return this.options;

            let isNumericQuery = !isNaN(searchQuery) && !isNaN(parseFloat(searchQuery));

            let filtered = this.options.filter(option => {
                if (isNumericQuery) {
                    debugger;
                    let rec = option.beneficiaryId.toString() === searchQuery;
                    return rec;
                } else {
                    return option.name.toLowerCase().includes(searchQuery.toLowerCase());
                }
            });
            if (isNumericQuery) {
    // Convert searchQuery to a number for accurate comparisons
    let numericQuery = parseInt(searchQuery, 10);

    return filtered.sort((a, b) => {
        let aId = parseInt(a.beneficiaryId, 10);
        let bId = parseInt(b.beneficiaryId, 10);

        // Check for exact match
        let aMatch = aId === numericQuery;
        let bMatch = bId === numericQuery;

        if (aMatch && bMatch) {
            // Both are exact matches, so order doesn't matter
            return 0;
        } else if (aMatch) {
            // 'a' is an exact match, prioritize it
            return -1;
        } else if (bMatch) {
            // 'b' is an exact match, prioritize it
            return 1;
        } else {
            // Neither are exact matches, sort by ascending beneficiaryId
            return aId - bId;
        }
    });
} else {
    // For name search, return filtered results as is
    return filtered;
}


        },

        getData: function () {
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.options = [];
            this.$https.get('/Benificary/GetBenificaryList?isDropDown=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    response.data.results.forEach(function (cat) {
                        var name = cat.name == '' ? cat.nameAr : cat.name;
                        var benficaryId = cat.beneficiaryId;
                        root.options.push({
                            id: cat.id,
                            beneficiaryId: cat.beneficiaryId,
                            name: benficaryId + ' ' + '-' + ' ' + name,
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