<template>
    <modal :show="show" v-if=" isValid('CanAddBrand') || isValid('CanEditBrand') ">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type == 'Edit'">
                    {{ $t('addBrand.UpdateProductBrand') }}
                </h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>
                    {{ $t('addBrand.CreateProductBrand') }}
                </h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div :key="render" class="form-group has-label col-sm-12 "
                        v-bind:class="{ 'has-danger': $v.brand.code.$error }">
                        <label class="text  font-weight-bolder">
                            {{ $t('addBrand.Code') }}:<span class="text-danger">
                                *
                            </span>
                        </label>
                        <input disabled class="form-control" v-model="$v.brand.code.$model" type="text" />
                        <span v-if="$v.brand.code.$error" class="error">
                            <span v-if="!$v.brand.code.maxLength">{{ $t('addBrand.CodeLength') }}</span>
                        </span>
                    </div>
                    <div v-if="english == 'true'" class="form-group has-label col-sm-12 "
                        v-bind:class="{ 'has-danger': $v.brand.name.$error }">
                        <label class="text  font-weight-bolder">
                            {{ $t('addBrand.BrandName') | englishLanguage }}: <span class="text-danger"> *</span>
                        </label>
                        <input class="form-control" v-model="$v.brand.name.$model" type="text" />
                        <span v-if="$v.brand.name.$error" class="error">
                            <span v-if="!$v.brand.name.required"> {{ $t('addBrand.NameRequired') }}</span>
                            <span v-if="!$v.brand.name.maxLength">{{ $t('addBrand.NameLength') }}</span>
                        </span>
                    </div>
                    <div v-if="isOtherLang()" class="form-group has-label col-sm-12 "
                        v-bind:class="{ 'has-danger': $v.brand.nameArabic.$error }">
                        <label class="text  font-weight-bolder">
                            {{ $t('addBrand.BrandNameArabic') | arabicLanguage }}:
                            <span class="text-danger"> *</span>
                        </label>
                        <input class="form-control  " v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'"
                            v-model="$v.brand.nameArabic.$model" type="text" />
                        <span v-if="$v.brand.nameArabic.$error" class="error">
                            <span v-if="!$v.brand.nameArabic.required"> {{ $t('addBrand.NameRequired') }}</span>
                            <span v-if="!$v.brand.nameArabic.maxLength">{{ $t('addBrand.NameLength') }}</span>
                        </span>
                    </div>

                    <div class="form-group has-label col-sm-12 "
                        v-bind:class="{ 'has-danger': $v.brand.description.$error }">
                        <label class="text  font-weight-bolder"> {{ $t('addBrand.Description') }}: </label>
                        <textarea rows="3" class="form-control" v-model="$v.brand.description.$model" type="text" />
                        <span v-if="$v.brand.description.$error" class="error">
                            {{ $t('addBrand.descriptionLength') }}
                        </span>
                    </div>

                    <div class="form-group col-md-4">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="brand.isActive">
                            <label for="inlineCheckbox1"> {{ $t('addBrand.Active') }} </label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveBrand"
                    v-bind:disabled="$v.brand.$invalid" v-if="type != 'Edit' && isValid('CanAddSubCategory')">
                    {{ $t('addBrand.btnSave') }}
                </button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveBrand"
                    v-bind:disabled="$v.brand.$invalid" v-if="type == 'Edit' && isValid('CanEditBrand')">
                    {{ $t('addBrand.btnUpdate') }}
                </button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">
                    {{
                            $t('addBrand.btnClear')
                    }}
                </button>
            </div>
            <loading :active.sync="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>
    </modal>
    <acessdenied v-else :model=true></acessdenied>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import 'vue-loading-overlay/dist/vue-loading.css';
import { maxLength, requiredIf } from "vuelidate/lib/validators"
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/vue-loading.css';
export default {
    mixins: [clickMixin],
    props: ['show', 'brand', 'type'],
    components: {
        Loading
    },
    data: function () {
        return {
            arabic: '',
            english: '',
            render: 0,
            loading: false,
        }
    },
    validations: {
        brand: {
            name: {
                maxLength: maxLength(250)
            },
            code: {

                maxLength: maxLength(30)
            },
            nameArabic: {
                required: requiredIf((x) => {
                    if (x.name == '' || x.name == null)
                        return true;
                    return false;
                }),
                maxLength: maxLength(250)
            },
            description: {
                maxLength: maxLength(500)
            }
        }
    },
    methods: {
        close: function () {
            this.$emit('close');
        },
        GetAutoCodeGenerator: function () {

            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            this.$https.get('/Product/BrandCode', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                if (response.data != null) {
                    root.brand.code = response.data;
                    root.render++;
                }
            });
        },
        SaveBrand: function () {
            var root = this;
            this.loading = true;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            this.$https.post('/Product/SaveBrand', this.brand, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data.isSuccess == true) {
                        if (root.type != "Edit") {

                            root.$swal({
                                title: this.$t('addBrand.Saved'),
                                text: this.$t('addBrand.SavedSuccessfully'),
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });

                            root.close();
                        }
                        else {

                            root.$swal({
                                title: this.$t('addBrand.Update'),
                                text: this.$t('addBrand.UpdateSucessfully'),
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.close();

                        }
                    }
                    else {
                        root.$swal({
                            title: this.$t('addBrand.Error'),
                            text: this.$t('addBrand.BrandNameExist'),
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });
                    }
                })
                .catch(error => {
                    console.log(error)
                    root.$swal.fire(
                        {
                            icon: 'error',
                            title: this.$t('addBrand.SomethingWrong'),
                            text: error.response.data,

                            showConfirmButton: false,
                            timer: 5000,
                            timerProgressBar: true,
                        });

                    root.loading = false
                })
                .finally(() => root.loading = false);
        }
    },
    mounted: function () {
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        if (this.brand.id == '00000000-0000-0000-0000-000000000000' || this.brand.id == undefined || this.brand.id == '')
            this.GetAutoCodeGenerator();

    }
}
</script>
