<template>
    <div>
        <div class="wrapper ">
            <div class="main-panel">
                <nav class="navbar navbar-expand-lg navbar-fixed-top navbar-transparent">
                    <div class="container-fluid">
                        <div class="navbar-wrapper">
                            <div class="navbar-minimize">
                                <button id="minimizeSidebar" class="btn btn-icon ">
                                    <i class="nc-icon nc-minimal-right text-center visible-on-sidebar-mini"></i>
                                    <i class="nc-icon nc-minimal-left text-center visible-on-sidebar-regular"></i>
                                </button>
                            </div>
                            <div class="navbar-toggle">
                                <button type="button" class="navbar-toggler">
                                    <span class="navbar-toggler-bar bar1"></span>
                                    <span class="navbar-toggler-bar bar2"></span>
                                    <span class="navbar-toggler-bar bar3"></span>
                                </button>
                            </div>
                            <a class="navbar-brand">POS</a>
                        </div>
                    </div>

                    <!-- TO SWITCH LANGUAGE -->
                    <div class="row">
                        <!--<div class="col-md-6">
                            <button type="button" class="btn btn-warning  btn-block mb-3" v-on:click="logout">Logout</button>
                        </div>-->
                        <div class="col-md-12 text-right">
                            <select v-model="$i18n.locale" class="form-control" @click.prevent="setLocale($i18n.locale)">
                                <option v-for="(lang, i) in langs" :key="`Lang${i}`" :value="lang">
                                    {{ lang.toUpperCase() }}
                                </option>
                            </select>
                        </div>
                    </div>
                </nav>
                <router-view :key="$route.fullPath"></router-view>
            </div>
        </div>



    </div>
</template>
<script>

    export default {
        name: 'locale-changer',
        data() {
            return { langs: ['en', 'ar'] }
        },
        methods: {
            setLocale(locale) {
                
                this.$i18n.locale = locale

            },
            logout: function () {

                this.$session.destroy();
                //document.cookie.split(';').forEach(cookie => document.cookie = cookie.replace(/^ +/, '').replace(/=.*/, `=;expires=${new Date(0).toUTCString()};path=/`));

                this.$router.push('/')
            }
        },
        beforeCreate: function () {

            if (!this.$session.exists()) {
                this.$router.push('/')
            }
        }

    }
</script>
