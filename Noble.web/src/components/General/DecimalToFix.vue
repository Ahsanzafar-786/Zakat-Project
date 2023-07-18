<template>
    
    <input type="text" v-bind:disabled="disable" v-shortkey.avoid class="form-control RemovePadding" v-bind:class="textAlignLeft? 'text-start ' : 'tex-end '" v-model="displayValue" @blur="isInputActive = false" @focus="isInputActive=true" @click="$event.target.select()" />

</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'

    export default {
        name: 'DecimalDropdown',
        props: ["value", "salePriceCheck", "disable" ,"isQunatity", "textAlignLeft"],
        mixins: [clickMixin],

        data: function () {
            return {
                isInputActive: false,
                currency: '',
            }
        },

        computed: {
            displayValue: {
                get: function () {
                    

                    if (this.isInputActive) {

                        // if (/^[0-9\u0660-\u0669]+$/.test(this.value) == true) {
                        //     this.value = this.value.replace(/[٠-٩]/g, d => "٠١٢٣٤٥٦٧٨٩".indexOf(d)).replace(/[۰-۹]/g, d => "۰۱۲۳۴۵۶۷۸۹".indexOf(d));

                        // }
                        // Cursor is inside the input field. unformat display value for user
                        return this.value.toString()
                    } else {
                      
                        if (this.salePriceCheck) {

                            this.$emit('CheckSalePrice', this.value);

                        }
                        // User is not modifying now. Format display value for user interface
                        if(this.isQunatity)
                        {
                            return parseFloat(this.value);

                        }
                        else
                        {
                            return parseFloat(this.value).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");

                        }
                    }
                },
                set: function (modifiedValue) {
                    
                    if(/^[0-9\u0660-\u0669]+$/.test(modifiedValue)==true)
                {
                    modifiedValue = modifiedValue.replace(/[٠-٩]/g, d => "٠١٢٣٤٥٦٧٨٩".indexOf(d)).replace(/[۰-۹]/g, d => "۰۱۲۳۴۵۶۷۸۹".indexOf(d));
                 
                }
                    // Recalculate value after ignoring "$" and "," in user input
                    let newValue = parseFloat(modifiedValue.replace(/[^\d\.]/g, "")); // eslint-disable-line
                    // Ensure that it is not NaN
                    if (isNaN(newValue)) {
                        newValue = 0
                    }
                    // Note: we cannot set this.value as it is a "prop". It needs to be passed to parent component
                    // $emit the event so that parent component gets it
                    this.$emit('input', newValue)
                }
            }
        },
        mounted: function () {
            this.currency = localStorage.getItem('currency');
        }
    }
</script>
<style scoped>
@media (max-width: 1180px) {
   

    .RemovePadding {
        padding: 0px !important;
    }
}
</style>
