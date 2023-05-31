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
                <div class="col-md-7">
                    <div class="form-group has-label col-sm-12 ">
                        <div class="row">
                            <div class="col-sm-5 text-md-end align-middle">
                                <label class="text  font-weight-bolder">
                                    {{ $t('AddPayment.Benificary') }}:<span class="text-danger"> *</span>
                                </label>
                            </div>
                            <div class="col-sm-7">
                                <benificary v-model="addPayment.benificaryId"
                                    v-on:input="EditBenificary(addPayment.benificaryId)" />
                                <a v-if="addPayment.benificaryId == '' || addPayment.benificaryId == null"
                                    href="javascript:void()" class="text-secondary">Benificary Details</a>
                                <a v-else href="javascript:void()" class="text-primary" data-bs-toggle="offcanvas"
                                    ref="offcanvasRight" data-bs-target="#offcanvasRight"
                                    aria-controls="offcanvasRight">Benificary Details</a>
                            </div>
                        </div>
                    </div>
                    <div class="form-group has-label col-sm-12 ">
                        <div class="row">
                            <div class="col-sm-5 text-md-end align-middle">
                                <label class="text  font-weight-bolder">
                                    {{ $t('AddPayment.Amount') }}:<span class="text-danger"> *</span>
                                </label>
                            </div>
                            <div class="col-sm-7">
                                <input type="number" class="form-control" v-model="addPayment.amount" readonly />
                            </div>
                        </div>
                    </div>
                    <div class="form-group has-label col-sm-12 ">
                        <div class="row">
                            <div class="col-sm-7 form-group"
                                v-if="addPayment.benificaryId != '' && addPayment.benificaryId != null">
                                <label>Start Month:</label>
                                <datepicker v-model="brand.startDate" :type="'month'" v-bind:key="rander" />

                            </div>
                        </div>
                    </div>
                    <div class="form-group has-label col-sm-12 ">
                        <div class="row">
                            <div class="col-sm-7 form-group"
                                v-if="addPayment.benificaryId != '' && addPayment.benificaryId != null">
                                <label>End Month:</label>
                                <datepicker v-model="brand.endDate" :type="'month'" v-bind:key="rander" />

                            </div>
                        </div>
                    </div>


                    <div class="form-group has-label col-sm-12 ">
                        <div class="row">
                            <div class="col-sm-5 text-md-end align-middle">
                                <label class="text  font-weight-bolder">
                                    {{ $t('AddPayment.Cashier') }}:
                                </label>
                            </div>
                            <div class="col-sm-7">
                                <input type="text" class="form-control" v-model="cashierName" readonly />
                            </div>
                        </div>
                    </div>
                    <div class="form-group has-label col-sm-12 ">
                        <div class="row">
                            <div class="col-sm-5 text-md-end align-middle">
                                <label class="text  font-weight-bolder">
                                    {{ $t('AddPayment.Month') }}:
                                </label>
                            </div>
                            <div class="col-sm-7">
                                <datepicker :type="'month'" v-model="addPayment.month" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group has-label col-sm-12 ">
                        <div class="row">
                            <div class="col-sm-5 text-md-end align-middle">
                                <label class="text  font-weight-bolder">
                                    {{ $t('AddPayment.Year') }}:
                                </label>
                            </div>
                            <div class="col-sm-7">
                                <input type="text" class="form-control" v-model="addPayment.year" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group has-label col-sm-12 ">
                        <div class="row">
                            <div class="col-sm-5 text-md-end align-middle">
                                <label class="text  font-weight-bolder">
                                    {{ $t('AddPayment.Period') }}:
                                </label>
                            </div>
                            <div class="col-sm-7">
                                <input type="text" class="form-control" v-model="addPayment.period" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-5" v-if="addPayment.benificaryId != '' && addPayment.benificaryId != null">
                    <div class="row">
                        <div class=" col-sm-6 " >
                            <label class="rounded text-white bg-primary px-2">Advance Payment: {{ brand.advancePayment
                        }}</label>
                        </div>
                        <div class=" col-sm-6 " >
                            <label class="rounded text-white bg-primary px-2">Payment Type: {{ brand.paymentTypeName }}</label>
                        </div>
                        <div class=" col-sm-6 pt-1" >
                            <label class="rounded text-white bg-primary px-2">Approval Person: {{ brand.approvalPersonName }}</label>
                        </div>
                        <div class=" col-sm-6 pt-1" >
                            <label class="rounded text-white bg-primary px-2">Start Month:{{ brand.startMonth }}</label>
                        </div>
                    </div>
                 
                    <!-- <div class="mt-1 col-sm-12">
                        <label class="rounded text-white bg-primary px-2">Approval Person: {{ brand.approvalPersonName
                        }}</label>
                    </div>
                    <div class="mt-1 col-sm-12">
                        <label class="rounded text-white bg-primary px-2">Start Month: {{ brand.startMonth }}</label>
                    </div> -->
                    <!-- <div class="mt-1 col-sm-12">
                        <label class="rounded text-white bg-primary px-2">Start Date: {{ brand.startDate }}</label>
                    </div>
                    <div class="mt-1 col-sm-12">
                        <label class="rounded text-white bg-primary px-2">End Date: {{ brand.endDate }}</label>
                    </div> -->
                </div>
                <div class="col-lg-12 invoice-btn-fixed-bottom">
                    <div class="button-items">
                        <button class="btn btn-outline-primary  mr-2">
                            <i v-on:click="SavePayment()" class="far fa-save"></i>
                            <span>
                                {{ $t('Save') }}
                            </span>
                        </button>
                        <button class="btn btn-danger mr-2" v-on:click="GotoPage('/dashboard')">
                            {{ $t('Cancel') }}
                        </button>
                    </div>
                </div>
                <div class="offcanvas offcanvas-end p-0" tabindex="-1" id="offcanvasRight"
                    aria-labelledby="offcanvasRightLabel" style="width: 500px !important;">
                    <div class="offcanvas-header">
                        <h5 id="offcanvasRightLabel" class="m-0">{{ $t('AddPayment.BenificaryDetails') }}</h5>
                        <button type="button" class="btn-close text-reset filter-green " data-bs-dismiss="offcanvas"
                            aria-label="Close"></button>
                    </div>
                    <div class="offcanvas-body">
                        <div class="row">
                            <div class="col-lg-12 form-group">
                                <label> {{ $t('AddBenificary.Name') }}:</label>
                                <input type="text" class="form-control" v-model="brand.name" />
                            </div>
                            <div class="col-lg-12 form-group">
                                <label>{{ $t('AddBenificary.NameArabic') }} :</label>
                                <input type="text" class="form-control" v-model="brand.nameAr" />
                            </div>
                            <div class="col-lg-12 form-group">
                                <label>{{ $t('AddBenificary.ID') }} :</label>
                                <input type="text" class="form-control" v-model="brand.ugamaNo" />
                            </div>
                            <div class="col-lg-12 form-group">
                                <label>{{ $t('AddBenificary.PassportNo') }} :</label>
                                <input type="text" class="form-control" v-model="brand.passportNo" />
                            </div>
                            <div class="col-lg-12 form-group">
                                <label>{{ $t('AddBenificary.Nationality') }} :</label>
                                <input type="text" class="form-control" v-model="brand.nationality" />
                            </div>

                            <div class="col-lg-12 form-group">
                                <label>{{ $t('AddBenificary.Gender') }} :</label>
                                <input type="text" class="form-control" v-model="brand.gender" />
                            </div>
                            <div class="col-lg-12 form-group">
                                <label>{{ $t('AddBenificary.ContactNo') }} :</label>
                                <input type="text" class="form-control" v-model="brand.phoneNo" />
                            </div>

                            <div class="col-lg-12 form-group">
                                <label>{{ $t('AddBenificary.Address') }} :</label>
                                <textarea rows="3" class="form-control" v-model="brand.address">  </textarea>
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
            rander: 0,
            arabic: '',
            english: '',
            brand: {},
            cashierName: '',
            addPayment: {
                benificaryId: '',
                amount: '',
                userId: '',
                month: '',
                year: '',
                period: '',
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
                        root.brand = response.data;
                        root.addPayment.amount = response.data.amountPerMonth;
                        root.rander++;
                    } else {
                        console.log("error: something wrong from db.");
                    }
                },
                    function (error) {
                        this.loading = false;
                        console.log(error);
                    });

        },
        SavePayment: function () {

            var root = this;
            this.loading = true;
            var token = '';
            if (this.$session.exists()) {
                token = localStorage.getItem('token');
            }
            this.$https.post('/Benificary/SavePayments', this.addPayment, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data.isSuccess == true) {
                        if (root.type != "Edit") {

                            root.$swal({
                                title: 'Save',
                                text: response.data.isUpdate,
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
                                title: 'Update',
                                text: response.data.isUpdate,
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
                            title: 'Error',
                            text: response.data.isUpdate,
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
                            title: 'Something Went Wrong',
                            text: error.response.data.isUpdate,
                            showConfirmButton: false,
                            timer: 5000,
                            timerProgressBar: true,
                        });

                    root.loading = false
                })
                .finally(() => root.loading = false);
        }




    },

    created: function () {
        this.$emit('input', this.$route.name);
    },
    mounted: function () {
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        this.addPayment.userId = localStorage.getItem('UserId');
        this.cashierName = localStorage.getItem('UserName');
    }
}
</script>