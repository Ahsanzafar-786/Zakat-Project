<template>
    <div v-if="isDisabled">
        <!--Disable Date With 00 00 Date Fomrat-->
        <el-date-picker v-model="inputValue" v-bind:type="date" v-bind:placeholder="$t('DatePicker.PickDate')" style="width: 100%; " format='00 000 0000' :picker-options="pickerOptions" disabled>
        </el-date-picker>
    </div>
    <div v-else-if="isDisable">
        <!--Disable Date-->
        <div v-if="type=='month'">
            <el-date-picker v-model="DisplayValue" v-bind:type="type" disabled format='MMMM yyyy ' v-bind:placeholder="$t('DatePicker.SelectMonth')" style="width: 100%; ">
            </el-date-picker>
    
        </div>
        <div v-else>
            <el-date-picker v-model="DisplayValue" v-bind:type="date" disabled v-bind:placeholder="$t('DatePicker.PickDate')" style="width: 100%;  " format='dd MMM yyyy' :picker-options="pickerOptions">
            </el-date-picker>
    
        </div>
    
    </div>
    <div v-else>
        <div v-if="type=='month'">
            <el-date-picker v-model="DisplayValue" v-bind:type="type" format='MMMM yyyy ' v-bind:placeholder="$t('DatePicker.SelectMonth')" style="width: 100%; ">
            </el-date-picker>
    
        </div>
        <div v-else-if="type=='year'">
            <el-date-picker v-model="DisplayValue" v-bind:type="type" format=' yyyy ' v-bind:placeholder="'Select Year'" style="width: 100%; ">
            </el-date-picker>
    
        </div>
        <div v-else>
            <el-date-picker v-model="DisplayValue" type="date" v-bind:placeholder="$t('DatePicker.PickDate')" style="width: 100%; " :picker-options="pickerOptions">
            </el-date-picker>
        </div>
    
    </div>
    </template>
    
    <script>
    import moment from 'moment'
    import 'element-ui/lib/theme-chalk/index.css';
    export default {
        props: ['value', 'dropdowndatecss', 'isDisabled', 'isDisable', 'type'],
        data: function () {
            return {
                dropdownDatecss: "",
                inputValue: '',
                editField: '',
                pickerOptions: {
                    shortcuts: [{
                            text: this.$t('DateFilter.Today'),
                            onClick(picker) {
                                picker.$emit('pick', new Date());
                            }
                        },
                        {
                            text: this.$t('DateFilter.Yesterday'),
                            onClick(picker) {
                                const date = new Date();
                                date.setTime(date.getTime() - 3600 * 1000 * 24);
                                picker.$emit('pick', date);
                            }
                        },
                        {
                            text: this.$t('DateFilter.Aweekago'),
                            onClick(picker) {
                                const date = new Date();
                                date.setTime(date.getTime() - 3600 * 1000 * 24 * 7);
                                picker.$emit('pick', date);
                            }
                        },
                        {
                            text: this.$t('DateFilter.AMonthago'),
                            onClick(picker) {
                                const date = new Date();
    
                                var d = date.getDate();
                                date.setFullYear(date.getFullYear() + -1);
                                if (date.getDate() != d) {
                                    date.setDate(0);
                                }
                                picker.$emit('pick', date);
                            }
                        },
                        {
                            text: this.$t('DateFilter.Quarterago'),
    
                            onClick(picker) {
                                const date = new Date();
                                var d = date.getDate();
                                date.setMonth(date.getMonth() + -4);
                                if (date.getDate() != d) {
                                    date.setDate(0);
                                }
                                picker.$emit('pick', date);
                            }
                        },
                        {
                            text: this.$t('DateFilter.HalfYear'),
                            onClick(picker) {
                                const date = new Date();
                                var d = date.getDate();
                                date.setMonth(date.getMonth() + -6);
                                if (date.getDate() != d) {
                                    date.setDate(0);
                                }
                                picker.$emit('pick', date);
                            }
                        },
                        {
                            text: this.$t('DateFilter.AYearago'),
                            onClick(picker) {
                                const date = new Date();
                                var d = date.getDate();
                                date.setFullYear(date.getFullYear() + -1);
                                if (date.getDate() != d) {
                                    date.setDate(0);
                                }
                                picker.$emit('pick', date);
                            }
                        }
                    ]
                },
            }
        },
        computed: {
            DisplayValue: {
                get: function () {
                    if (this.inputValue != "" || this.inputValue!= undefined) {
                        return this.inputValue;
                    }
                    return this.values;
                },
                set: function (value) {
                    this.inputValue = value;
                    this.$emit('input', moment(String(this.inputValue)).format('DD MMM YYYY'));
                }
            }
        },
        methods: {
    
        },
        mounted: function () {
            this.dropdownDatecss = this.dropdowndatecss;
            this.inputValue = this.value;
        },
        // updated: function () {
        //     var input = "";
        //     if (this.inputValue != "" && this.inputValue != null && this.inputValue != undefined) {
        //         input = moment(String(this.inputValue)).format('DD MMM YYYY');
        //     }
        //     this.inputValue = input;
        //     this.$emit('input', this.inputValue);
        // }
    }
    </script>
    