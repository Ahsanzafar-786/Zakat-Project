<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('AddPayment.AddPayment') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Home') }}</a>
                                    </li>
                                    <li class="breadcrumb-item active">{{ $t('AddPayment.AddPayment') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="GotoPage('/dashboard')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
                <hr />
                <div class="col-md-12">
                    <div class="form-group has-label col-sm-6 ">
                        <div class="row">
                            <div class="col-sm-5 text-md-end align-middle">
                                <label class="text  font-weight-bolder">
                                    {{ $t('AddPayment.Benificary') }}:<span class="text-danger"> *</span>
                                </label>
                            </div>
                            <div class="col-sm-7">
                                <benificary v-model="addPayment.benificaryId" v-on:input="EditBenificary(addPayment.benificaryId)" />
                                <a v-if="addPayment.benificaryId == ''" href="javascript:void()" class="text-secondary">Benificary Details</a>
                                <a v-else href="javascript:void()" class="text-primary" data-bs-toggle="offcanvas" ref="offcanvasRight" data-bs-target="#offcanvasRight" aria-controls="offcanvasRight">Benificary Details</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasRight" aria-labelledby="offcanvasRightLabel" style="width: 500px !important;">
                                <div class="offcanvas-header">
                                    <h5 id="offcanvasRightLabel" class="m-0">{{ $t('AddPayment.BenificaryDetails') }}</h5>
                                    <button type="button" class="btn-close text-reset filter-green " data-bs-dismiss="offcanvas" aria-label="Close"></button>
                                </div>
                                <div class="offcanvas-body">
                                    <div class="row">
                                        <div class="col-lg-12 form-group">
                                            <label> {{ $t('AddSale.CustomerId') }}:</label>
                                            <input type="text" class="form-control"   />
                                        </div>
                                        <div class="col-lg-12 form-group">
                                            <label>{{ $t('AddSale.NameCustomer') }} :</label>
                                            <input type="text" class="form-control"   />
                                        </div>
                                        <div class="col-lg-12 form-group">
                                            <label>{{ $t('AddCustomer.CommercialRegistrationNo') }} :</label>
                                            <input type="text" class="form-control"  />
                                        </div>
                                        <div class="col-lg-12 form-group">
                                            <label>{{ $t('AddCustomer.VAT/NTN/Tax No') }} :</label>
                                            <input type="text" class="form-control"  />
                                        </div>
                                        <div class="col-lg-12 form-group">
                                            <label>{{ $t('AddSale.Mobile') }} :</label>
                                            <input type="text" class="form-control"  />
                                        </div>

                                        <div class="col-lg-12 form-group">
                                            <label>{{ $t('AddCustomer.Email') }} :</label>
                                            <input type="text" class="form-control" />
                                        </div>

                                        <div class="col-lg-12 form-group">
                                            <label>{{ $t('AddSale.CustomerAddress') }} :</label>
                                            <textarea rows="3"  class="form-control"> </textarea>
                                        </div>
                                        

                                    </div>
                                </div>
                            </div>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    data: function () {
        return {
            arabic: '',
            english: '',
            benificaryDetails:{},
            addPayment: {
                benificaryId: '',
            }
        }
    },
    watch: {
        search: function (val) {
            this.GetPayment(val, 1);
        },
    },
    methods: {
        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        EditBenificary: function (Id) {
            debugger;
            var root = this;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Benificary/GetBenificaryDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data) {
                        root.benificaryDetails = response.data;
                    } else {
                        console.log("error: something wrong from db.");
                    }
                },
                    function (error) {
                        this.loading = false;
                        console.log(error);
                    });

        }





    },

    created: function () {
        this.$emit('input', this.$route.name);
    },
    mounted: function () {
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
    }
}
</script>