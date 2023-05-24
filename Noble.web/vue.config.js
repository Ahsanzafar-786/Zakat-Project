module.exports = {
    devServer: {
        
        proxy: 'http://localhost:8080'
    },
    pluginOptions: {
        i18n: {
            locale: 'en',
            fallbackLocale: 'en',
            localeDir: 'locales',
            enableInSFC: false
        },
        electronBuilder: {
            builderOptions: {
                productName: "NOBLEPOS-ERP",
                appId: 'NOBLEPOS-ERP',
                win: {
                    "target": [
                        "nsis",
                    ],
                    icon: 'public/pos.ico'
                },
                "nsis": {
                    "installerIcon": "public/pos.ico",
                    "uninstallerIcon": "public/pos.ico",
                    "uninstallDisplayName": "NOBLEPOS-ERP",
                    "oneClick": false,
                    "allowToChangeInstallationDirectory": true
                }
            },
        },
    }
}