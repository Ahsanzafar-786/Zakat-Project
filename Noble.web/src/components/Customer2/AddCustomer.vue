<template>
    <div class="row" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-if="isValid('CanAddCustomer') || isValid('CanEditCustomer') ">
        <div class="col-md-12 ml-auto mr-auto card" >

            <div>


                <div class="card-header">
                    <span class="card-title  ml-4 DayHeading " v-if="Customer.id== '00000000-0000-0000-0000-000000000000'">{{ $t('AddCustomer.AddCustomer') }}</span>
                    <span class="card-title  ml-4 DayHeading" v-else>{{ $t('AddCustomer.UpdateCustomer') }}</span>
                </div>
                <div class=" pt-3" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                    <ul class="nav nav-tabs" data-tabs="tabs">
                        <li class="nav-item"><a class="nav-link" v-bind:class="{active:active == 'PersonalInfo'}" v-on:click="makeActive('PersonalInfo')" id="v-pills-home-tab" data-toggle="pill" href="#v-pills-home" role="tab" aria-controls="v-pills-home" aria-selected="true">{{ $t('AddCustomer.CustomerProfile') }}</a></li>
                        <li class="nav-item"><a class="nav-link" v-bind:class="{active:active == 'CommercialInformation'}" v-on:click="makeActive('CommercialInformation')" id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile" role="tab" aria-controls="v-pills-profile" aria-selected="false">{{ $t('AddCustomer.PaymentTerms') }}</a></li>
                        <!--<li class="nav-item"><a class="nav-link" v-bind:class="{active:active == 'Attachment'}" v-on:click="makeActive('Attachment')" id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile" role="tab" aria-controls="v-pills-profile" aria-selected="false">{{ $t('AddCustomer.Attachments') }}</a></li>-->
                    </ul>
                </div>
                <div class="tab-content" id="nav-tabContent" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                    <div v-if="active == 'PersonalInfo'">
                        <div class="card-body ">
                            <!--<h5 class="card-title ">{{ $t('AddCustomer.CustomerProfile') }}</h5>-->
                            <div class="row ">
                                <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4">
                                    <label>{{ $t('AddCustomer.CustomerCode') }} :<span class="text-danger"> *</span></label>
                                    <div v-bind:class="{'has-danger' : $v.Customer.code.$error}">
                                        <input readonly class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="$v.Customer.code.$model" />
                                        <span v-if="$v.Customer.code.$error" class="error text-danger">
                                        </span>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4">
                                    <label>{{ $t('AddCustomer.RegistrationDate') }}  :</label>
                                    <div v-bind:class="{'has-danger' : $v.Customer.registrationDate.$error}">
                                        <datepicker v-model="$v.Customer.registrationDate.$model" :key="daterander"></datepicker>
                                        <span v-if="$v.Customer.registrationDate.$error" class="error text-danger">
                                        </span>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4">
                                    <label> {{ $t('AddCustomer.CustomerType') }}   :</label>
                                    <div>
                                        <multiselect v-model="Customer.category" :options="options" :show-labels="false" placeholder="Select Type">
                                        </multiselect>

                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4">
                                    <label> {{ $t('AddCustomer.CustomerCategory') }}:<span class="text-danger"> *</span></label>
                                    <div v-bind:class="{'has-danger' : $v.Customer.customerType.$error}">

                                        <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight()) " v-model="$v.Customer.customerType.$model" :options="['Individual', 'Establishment', 'Company','Factory','Manufacturer','Government']" :show-labels="false" placeholder="Select Type">
                                        </multiselect>
                                        <multiselect v-else v-model="$v.Customer.customerType.$model" :options="['فرد', 'مؤسسة', 'شركة','مصنع','الصانع','حكومة']" :show-labels="false" v-bind:placeholder="$t('AddCustomer.SelectOption')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                        </multiselect>
                                        <span v-if="$v.Customer.customerType.$error" class="error text-danger">

                                        </span>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4">
                                    <label> Customer Group   :</label>
                                    <div>
                                        <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="Customer.customerGroup" />

                                    </div>
                                </div>
                                <div v-if="english=='true'" class="col-xs-12 col-sm-6 col-md-4 col-lg-4">
                                    <label>{{ $t('AddCustomer.CustomerName(English)') |englishLanguage}} :<span class="text-danger"> *</span></label>
                                    <div v-bind:class="{'has-danger' : $v.Customer.englishName.$error}">
                                        <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="$v.Customer.englishName.$model" />
                                        <span v-if="$v.Customer.englishName.$error" class="error text-danger">
                                            <span v-if="!$v.Customer.englishName.required">{{ $t('AddCustomer.EngValidation') }} </span>
                                            <span v-if="!$v.Customer.englishName.maxLength">{{ $t('AddCustomer.EngMax') }} </span>
                                        </span>
                                    </div>
                                </div>
                                <div v-if="isOtherLang()" class="col-xs-12 col-sm-6 col-md-4 col-lg-4">
                                    <label>{{ $t('AddCustomer.CustomerName(Arabic)') |arabicLanguage}} :<span class="text-danger"> *</span></label>
                                    <div v-bind:class="{'has-danger' : $v.Customer.arabicName.$error}">
                                        <input class="form-control "  v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'"  v-model="$v.Customer.arabicName.$model" />
                                        <span v-if="$v.Customer.arabicName.$error" class="error text-danger">

                                            <span v-if="!$v.Customer.arabicName.maxLength">{{ $t('AddCustomer.ArMax') }} </span>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4">
                                    <label>{{ $t('AddCustomer.CommercialRegistrationNo') }} :<span class="text-danger"> *</span></label>
                                    <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="Customer.commercialRegistrationNo" />
                                </div>

                                <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4">
                                    <label>{{ $t('AddCustomer.VAT/NTN/Tax No') }} :<span class="text-danger"> *</span></label>
                                    <div v-bind:class="{'has-danger' : $v.Customer.vatNo.$error}">
                                        <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="$v.Customer.vatNo.$model" />
                                        <span v-if="$v.Customer.vatNo.$error" class="error text-danger">
                                            <span v-if="!$v.Customer.vatNo.required">{{ $t('AddCustomer.VatNoRequired') }}</span>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4">
                                    <label>{{ $t('AddCustomer.Email') }}  :</label>
                                    <div v-bind:class="{'has-danger' : $v.Customer.email.$error}">
                                        <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="$v.Customer.email.$model" />
                                        <span v-if="$v.Customer.email.$error" class="error text-danger">
                                        </span>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4">
                                    <label>{{ $t('AddCustomer.ContactPerson1') }} :</label>
                                    <div v-bind:class="{'has-danger' : $v.Customer.contactPerson1.$error}">
                                        <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="$v.Customer.contactPerson1.$model" />
                                        <span v-if="$v.Customer.contactPerson1.$error" class="error text-danger">
                                        </span>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4">
                                    <label>{{ $t('AddCustomer.ContactNumber1') }} :</label>
                                    <div v-bind:class="{'has-danger' : $v.Customer.contactNo1.$error}">
                                        <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="$v.Customer.contactNo1.$model" />
                                        <span v-if="$v.Customer.contactNo1.$error" class="error text-danger">
                                        </span>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4">
                                    <label>{{ $t('AddCustomer.ContactPerson2') }} :</label>
                                    <div v-bind:class="{'has-danger' : $v.Customer.contactPerson2.$error}">
                                        <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="$v.Customer.contactPerson2.$model" />
                                        <span v-if="$v.Customer.contactPerson2.$error" class="error text-danger">

                                        </span>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4">
                                    <label>{{ $t('AddCustomer.ContactNumber2') }} :</label>
                                    <div v-bind:class="{'has-danger' : $v.Customer.contactNo2.$error}">
                                        <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="$v.Customer.contactNo2.$model" />
                                        <span v-if="$v.Customer.contactNo2.$error" class="error text-danger">

                                        </span>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4">
                                    <label>{{ $t('AddCustomer.City') }}  :</label>
                                    <div v-bind:class="{'has-danger' : $v.Customer.city.$error}">
                                        <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="$v.Customer.city.$model" />
                                        <span v-if="$v.Customer.city.$error" class="error text-danger">

                                        </span>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4">
                                    <label>Area / District  :</label>
                                    <div>
                                        <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="Customer.area" />

                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4">
                                    <label>Website :</label>
                                    <div>
                                        <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="Customer.website" />

                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4">
                                    <label>{{ $t('AddCustomer.Country') }}  :</label>
                                    <div v-bind:class="{'has-danger' : $v.Customer.country.$error}">
                                        <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="$v.Customer.country.$model" />
                                        <span v-if="$v.Customer.country.$error" class="error text-danger">
                                        </span>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4">
                                    <label>{{ $t('AddCustomer.Address') }}  :</label>
                                    <div v-bind:class="{'has-danger' : $v.Customer.address.$error}">
                                        <textarea class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="$v.Customer.address.$model" />
                                        <span v-if="$v.Customer.address.$error" class="error text-danger">
                                        </span>
                                    </div>
                                </div>
                                <!--<div class="col-xs-12 col-sm-6 col-md-4 col-lg-4">
                            <label>{{ $t('Customer.IsExpiry') }}  :</label>
                            <div>
                                <div class="row pl-3">
                                    <b-form-checkbox size="lg" v-on:checked="Customer.isExpiry == false" class="mt-1 col-sm-1 ">Yes</b-form-checkbox>
                                </div>

                            </div>
                        </div>-->
                                <div class="col-sm-1 mt-3">
                                    <label>{{ $t('AddCustomer.Active') }}  :</label> <toggle-button v-model="Customer.isActive" class="ml-2" color="#3178F6" v-bind:key="isActiveRander" />
                                </div>
                                <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4" v-if="!Customer.isActive">
                                    <label>Reason /Comment :</label>
                                    <div>
                                        <textarea class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="Customer.comments" />

                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>

                    <div v-if="active == 'CommercialInformation'">
                        <!--<h5 class="card-title pl-3">{{ $t('AddCustomer.PaymentTerms') }}</h5>-->

                        <div class="card-body ">
                            <div class="row">
                                <div class="col-md-8">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label>{{ $t('AddCustomer.PaymentTerms') }} :<span class="text-danger"> *</span></label>
                                            <div v-bind:class="{'has-danger' : $v.Customer.paymentTerms.$error}">

                                                <link rel="stylesheet" href="https://unpkg.com/vue-multiselect@2.1.0/dist/vue-multiselect.min.css">
                                                <multiselect v-model="$v.Customer.paymentTerms.$model" :preselect-first="true" v-if="($i18n.locale == 'en' ||isLeftToRight()) " :options="[ 'Cash', 'Credit']" :show-labels="false" placeholder="Select Type">
                                                </multiselect>
                                                <multiselect v-else v-model="$v.Customer.paymentTerms.$model" :preselect-first="true" :options="[ 'نقد', 'آجل']" :show-labels="false" v-bind:placeholder="$t('AddCustomer.SelectOption')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                                </multiselect>
                                                <span v-if="$v.Customer.paymentTerms.$error" class="error text-danger">
                                                    <span v-if="!$v.Customer.paymentTerms.required">{{ $t('AddCustomer.PaymentValidation') }} </span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-md-6" v-if="Customer.paymentTerms=='Credit'">
                                            <label>{{ $t('AddCustomer.CreditLimit') }} :</label>
                                            <input class="form-control " v-model="Customer.creditLimit" type="number" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" />
                                        </div>
                                        <div class="col-md-6" v-if="Customer.paymentTerms=='Credit'">
                                            <label>{{ $t('AddCustomer.CreditPeriod') }}:</label>
                                            <input class="form-control " type="number" v-model="Customer.creditPeriod" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" />
                                        </div>
                                        <div class="col-md-6">
                                            <label>{{ $t('AddCustomer.DeliveryTerm') }} :</label>
                                            <div v-bind:class="{'has-danger' : $v.Customer.deliveryTerm.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="$v.Customer.deliveryTerm.$model" />
                                                <span v-if="$v.Customer.deliveryTerm.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <label>{{ $t('AddCustomer.Remarks') }} :</label>
                                            <textarea class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="Customer.remarks" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--<div v-if="active == 'Attachment'">-->
                        <!--<h5 class="">{{ $t('AddCustomer.Attachments') }}</h5>-->
                        <!--<div class="" v-if="(Customer.contactAttachments[0].commercialRegistration == '' &&
                                                 Customer.contactAttachments[0].vatCertificate == '' &&
                                                 Customer.contactAttachments[0].zakatCertificate == '') || isAddAttachment">

                            <div class="card-body ">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <label>{{ $t('AddCustomer.CommercialRegistration') }}:</label>
                                        <div>
                                            <span>
                                                <input ref="imgupload1" type="file" id="file-input"
                                                       @change="uploadImage('commercialRegistration')"
                                                       name="image" style="opacity:1;padding:25px">
                                            </span>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <label>{{ $t('AddCustomer.VAT/NTN/TaxCertificate') }}:</label>
                                        <div>
                                            <span>
                                                <input ref="imgupload2" type="file" id="file-input"
                                                       @change="uploadImage('vatCertificate')" name="image" style="opacity:1;padding:25px">
                                            </span>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <label>{{ $t('AddCustomer.ZakatCertificate') }} :</label>
                                        <div>
                                            <span>
                                                <input ref="imgupload3" type="file" id="file-input"
                                                       @change="uploadImage('zakatCertificate')"
                                                       name="image" style="opacity:1;padding:25px">
                                            </span>
                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>
                        <div class="" v-else>
                            <div class="col-md-12">

                                <div class="col-sm-6 float-right">
                                    <a href="javascript:void(0)" class="btn btn-outline-primary  float-right" v-on:click="isAddAttachment=true"><i class="fa fa-upload"></i> Upload</a>
                                </div>
                            </div>
                            <div class="card-body ">
                                <div class=" table-responsive">
                                    <table class="table ">
                                        <thead class="m-0">
                                            <tr>
                                                <th>#</th>
                                                <th>{{ $t('AddCustomer.Date') }} </th>
                                                <th>{{ $t('AddCustomer.CommercialRegistration') }} </th>
                                                <th>{{ $t('AddCustomer.VAT/NTN/TaxCertificate') }}</th>
                                                <th>{{ $t('AddCustomer.ZakatCertificate') }}</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="(contact,index) in Customer.contactAttachments" v-bind:key="index">
                                                <td>
                                                    {{index+1}}
                                                </td>
                                                <th>{{contact.date  | formatDate}}</th>
                                                <td>
                                                    <button class="btn btn-primary  btn-icon mr-2"
                                                            v-if="contact.commercialRegistration != ''"
                                                            v-on:click="DownloadAttachment(contact.commercialRegistration)">
                                                        <i class="fa fa-download"></i>
                                                    </button>
                                                </td>
                                                <td>
                                                    <button class="btn btn-primary  btn-icon mr-2"
                                                            v-if="contact.vatCertificate != ''"
                                                            v-on:click="DownloadAttachment(contact.vatCertificate)">
                                                        <i class="fa fa-download"></i>
                                                    </button>
                                                </td>
                                                <td>
                                                    <button class="btn btn-primary  btn-icon mr-2"
                                                            v-if="contact.zakatCertificate != ''"
                                                            v-on:click="DownloadAttachment(contact.zakatCertificate)">
                                                        <i class="fa fa-download"></i>
                                                    </button>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="card-footer">
                                <div class="float-left">
                                    <span v-if="currentPage===1 && rowCount === 0">  {{ $t('Pagination.ShowingEntries') }}</span>
                                    <span v-else-if="currentPage===1 && rowCount < 5">  {{ $t('Pagination.Showing') }} {{currentPage}}  {{ $t('Pagination.to') }} {{rowCount}}  {{ $t('Pagination.of') }} {{rowCount}}  {{ $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage===1 && rowCount >= 6  "> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*10}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage===1"> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*10}} of {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage!==1 && currentPage!==pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*10)-9}} {{ $t('Pagination.to') }} {{currentPage*10}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*10)-9}} {{ $t('Pagination.to') }} {{rowCount}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                </div>
                                <div class="float-right">
                                    <div class="overflow-auto" v-on:click="GetContactAttachments(search, currentPage, Customer.id)">
                                        <b-pagination pills size="lg" v-model="currentPage"
                                                      :total-rows="rowCount"
                                                      :per-page="5"
                                                      first-text="First"
                                                      prev-text="Previous"
                                                      next-text="Next"
                                                      last-text="Last"></b-pagination>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>-->
                </div>
                <div class="card-footer">
                    <div class="row">
                        <div v-if="!loading" class=" col-md-12 arabicLanguage" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            <button class="btn btn-primary mr-2 float-left" v-on:click="Attachment()">
                                {{ $t('PurchaseView.Attachment') }}
                            </button>

                            <button class="btn btn-primary   mr-2" :disabled="$v.Customer.$invalid" v-on:click="SaveCustomer" v-if="Customer.id=='00000000-0000-0000-0000-000000000000' && isValid('CanAddCustomer')"><i class="far fa-save"></i> {{ $t('AddCustomer.btnSave') }}</button>
                            <button class="btn btn-primary   mr-2" :disabled="$v.Customer.$invalid" v-on:click="SaveCustomer" v-if="Customer.id!='00000000-0000-0000-0000-000000000000' && isValid('CanEditCustomer')"><i class="far fa-save"></i> {{ $t('AddCustomer.btnUpdate') }}</button>

                            <button class="btn btn-danger   mr-2" v-on:click="Cancel">{{ $t('AddCustomer.btnClear') }}</button>
                        </div>
                        <div class="card-footer col-md-3 " v-else>
                            <loading :active.sync="loading" :can-cancel="true" :is-full-page="true"></loading>
                        </div>
                    </div>
                </div>
            </div>

            <bulk-attachment :attachmentList="Customer.attachmentList" :show="show" v-if="show" @close="attachmentSave" />
        </div>
    </div>
    <div v-else> <acessdenied></acessdenied></div>


</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Multiselect from 'vue-multiselect'
    import moment from 'moment'
    import { required, maxLength, requiredIf, helpers } from 'vuelidate/lib/validators';
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/vue-loading.css';
    // Import Vue FilePond
    const validCustomerEnglishName = helpers.regex("mob", /^[a-zA-Z\s.]*$/);


    // Register file type validation plugin

    export default ({
        mixins: [clickMixin],
        components: {
            Loading,
            Multiselect,

        },
        data: function () {
            return {
                arabic: '',
                show:false,
                b2b:false,
                b2c:false,
                english: '',
                myFiles: ['index.html'],
                value: '',
                active: 'PersonalInfo',
                Customer: {
                    id: '00000000-0000-0000-0000-000000000000',
                    code: '',
                    comments: '',
                    registrationDate: '',
                    category: '',
                    customerType: '',
                    customerGroup: '',
                    website: '',
                    statusDate: '',
                    area: '',
                    englishName: '',
                    arabicName: '',
                    mobileNo: '',
                    commercialRegistrationNo: '',
                    vatNo: '',
                    email: '',
                    contactPerson1: '',
                    contactNo1: '',
                    contactPerson2: '',
                    contactNo2: '',
                    country: '',
                    city: '',
                    address: '',
                    isExpiry: '',
                    paymentTerms: '',
                    deliveryTerm: '',
                    remarks: '',
                    creditLimit: '',
                    creditPeriod: '',
                    creditDays: '',
                    commercialRegistration: '',
                    vatCertificate: '',
                    zakatCertificate: '',
                    isCustomer: true,
                    isActive: true,
                    contactAttachments: [{
                        commercialRegistration: '',
                        vatCertificate: '',
                        zakatCertificate: '',
                    }],
                    attachmentList:[]
                },
                loading: false,
                daterander: 0,
                isAddAttachment: false,
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                search: '',
                options:[],
                isActiveRander: 0,
                language: 'Nothing',

            }
        },
        validations: {
            Customer:
            {
                code: {},
                registrationDate: {},
                category: {  },
                customerType: { required },
                englishName: {
                    required: requiredIf((x) => {
                        if (x.arabicName == '' || x.arabicName == null)
                            return true;
                        return false;
                    }),
                    maxLength: maxLength(250),
                    validCustomerEnglishName
                },
                arabicName: {
                    maxLength: maxLength(250)
                },
                vatNo: {
                    required: requiredIf((x) => {
                        if (x.commercialRegistrationNo == '' || x.commercialRegistrationNo == null)
                            return true;
                        return false;
                    }),
                    maxLength: maxLength(250)
                },
                mobile: {},
                email: {},
                contactPerson1: {},
                contactNo1: {},
                contactPerson2: {},
                contactNo2: {},
                country: {},
                city: {},
                address: {},
                creditLimit: {},
                creditDays: {},
                paymentTerms: { required},
                deliveryTerm: {},
                remarks: {},
            }
        },
        methods: {
            Attachment: function () {
                this.show = true;
            },

            attachmentSave: function (attachment) {
                this.Customer.attachmentList = attachment;
                this.show = false;
            },

            languageChange: function (lan) {
                if (this.language == lan) {
                    if (this.Customer.id == '00000000-0000-0000-0000-000000000000') {

                        var getLocale = this.$i18n.locale;
                        this.language = getLocale;

                        this.$router.go('/addCustomer2');
                    }
                    else {

                        this.$swal({
                            title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'You cannot Change language during update, otherwise your current page data will be lose!' : 'لا يمكنك تغيير اللغة أثناء التحديث ، وإلا ستفقد بيانات صفحتك الحالية!',
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            icon: 'error',
                            timer: 4000,
                            timerProgressBar: true,
                        });
                    }
                }


            },
            DeleteAttachment(path, type) {
                var root = this;

                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Contact/DeleteAttachment?path=' + path, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        if (response.data != null) {
                            if (type == 'commercialRegistration') {
                                root.Customer.commercialRegistration = response.data;
                                root.Customer.contactAttachments[0].commercialRegistration = response.data;
                            }

                            if (type == 'vatCertificate') {
                                root.Customer.vatCertificate = response.data;
                                root.Customer.contactAttachments[0].vatCertificate = response.data;
                            }

                            if (type == 'zakatCertificate') {
                                root.Customer.zakatCertificate = response.data;
                                root.Customer.contactAttachments[0].zakatCertificate = response.data;
                            }
                        }
                    }
                });
            },
            uploadImage(type) {

                var root = this;
                var token = '';
                if (root.$session.exists()) {
                    token = localStorage.getItem('token');
                }

                var file = null;

                if (type == 'commercialRegistration') {
                    file = this.$refs.imgupload1.files;
                }

                if (type == 'vatCertificate') {
                    file = this.$refs.imgupload2.files;
                }

                if (type == 'zakatCertificate') {
                    file = this.$refs.imgupload3.files;
                }
                var fileData = new FormData();
                for (var k = 0; k < file.length; k++) {
                    fileData.append("files", file[k]);
                }
                root.$https.post('/Company/UploadFilesAsync', fileData, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            if (type == 'commercialRegistration') {
                                root.Customer.commercialRegistration = response.data;
                            }

                            if (type == 'vatCertificate') {
                                root.Customer.vatCertificate = response.data;
                            }

                            if (type == 'zakatCertificate') {
                                root.Customer.zakatCertificate = response.data;
                            }
                        }
                    },
                        function () {
                            this.loading = false;
                            root.$swal({
                                title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'There is something wrong. Please contact to support.' : 'هناك شيء ما خاطئ. يرجى الاتصال للدعم.',
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                buttonsStyling: false
                            });
                        });
            },

            DownloadAttachment(path) {
                var root = this;
                var token = '';
                if (root.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                var ext = path.split('.')[1];
                root.$https.get('/Contact/DownloadFile?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` }, responseType: 'blob' })
                    .then(function (response) {
                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;
                        link.setAttribute('download', 'file.' + ext);
                        document.body.appendChild(link);
                        link.click();
                    });
            },

            GetAutoCodeGenerator: function () {
                var root = this;
                var token = '';
                if (root.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                root.$https
                    .get('/Contact/AutoGenerateCode?isCustomer=true'+ '&isCashCustomer=' + false, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data != null) {
                            root.Customer.code = response.data.contact;

                        }
                    });
            },
            Cancel: function () {
                if (this.isValid('CanViewCustomer')) {
                    this.$router.push({
                        path: '/Customer2',

                    })
                }
                else {
                    this.$router.go();
                }
               
            },

            makeActive: function (tab) {

                this.active = tab;
            },
            SaveCustomer: function () {
                this.loading = true;
                var root = this;
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }

                root.$https
                    .post('/Contact/SaveContact', this.Customer, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Add") {
                            root.loading = false
                            root.info = response.data.bpi

                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully' : 'حفظ بنجاح',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved' : 'تم الحفظ',
                                type: 'success',
                                confirmButtonClass: "btn btn-success",
                                buttonStyling: false,
                                icon: 'success',
                                timer: 1500,
                                timerProgressBar: true,

                            }).then(function (ok) {
                                if (ok != null) {
                                    if (root.isValid('CanViewCustomer')) {
                                        root.$router.push({
                                            path: '/Customer2',

                                        })
                                    }
                                    else {
                                        root.$router.go();
                                    }
                                    //root.$router.push('/Customer2');
                                }
                            });
                        }
                        else if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Update") {
                            root.loading = false
                            root.info = response.data.bpi

                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully' : 'حفظ بنجاح',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved' : 'تم الحفظ',
                                type: 'success',
                                confirmButtonClass: "btn btn-success",
                                buttonStyling: false,
                                icon: 'success',
                                timer: 1500,
                                timerProgressBar: true,

                            }).then(function (ok) {
                                if (ok != null) {
                                    if (root.isValid('CanViewCustomer')) {
                                        root.$router.push({
                                            path: '/Customer2',

                                        })
                                    }
                                    else {
                                        root.$router.go();
                                    }
                                    //root.$router.push('/Customer2');
                                }
                            });
                        }
                        else {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'There is something wrong. Please contact to support.' : 'هناك شيء ما خاطئ. يرجى الاتصال للدعم.',
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                icon: 'error',
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
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false)
            },

            GetContactAttachments: function (search, currentPage, id) {
                var root = this;
                var url = '/contact/GetContactAttachments?searchTerm=' + search + '&pageNumber=' + currentPage + '&id=' + id;
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                root.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.results.length > 0) {
                        root.Customer.contactAttachments = response.data.results;
                        root.Customer.commercialRegistration = response.data.results[0].commercialRegistration;
                        root.Customer.vatCertificate = response.data.results[0].vatCertificate;
                        root.Customer.zakatCertificate = response.data.results[0].zakatCertificate;
                    }
                    else {
                        root.Customer.contactAttachments = [{
                            commercialRegistration: '',
                            vatCertificate: '',
                            zakatCertificate: '',
                        }]
                    }

                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;
                });
            }
        },
        created: function () {

            this.$emit('input', this.$route.name)
            if (this.$route.query.data != undefined) {
                this.Customer = this.$route.query.data;
                this.GetContactAttachments(this.search, 1, this.Customer.id);
                this.daterander++;
                this.isActiveRander++;
            }
        },

        filters: {
            formatDate: function (value) {
                return moment(value).format("DD MMM yyyy hh:mm");
            }
        },

        mounted: function () {
            this.language = this.$i18n.locale;
            this.b2b = localStorage.getItem('b2b') == 'true' ? true : false;
            this.b2c = localStorage.getItem('b2c') == 'true' ? true : false;
            if (this.b2b && this.b2c) {
                this.options = ['B2B – Business to Business', 'B2C – Business to Client']
            }
            else if (this.b2b) {
                this.options = ['B2B – Business to Business']
            }
            else if (this.b2c) {
                this.options = ['B2C – Business to Client']
            }
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            if (this.$route.query.data == '00000000-0000-0000-0000-000000000000' || this.$route.query.data == undefined || this.$route.query.data == '') {
                this.GetAutoCodeGenerator();
                this.Customer.registrationDate = moment().format('llll');
                if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                    this.Customer.paymentTerms = 'Cash';
                }
                else {
                    this.Customer.paymentTerms = 'آجل';
                }
                this.daterander++;
            }
        }
    })

</script>