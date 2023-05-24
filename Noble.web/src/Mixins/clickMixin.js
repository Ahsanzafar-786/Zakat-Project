export default {

    methods: {
        isValid: function (x) {
           
            var storedColors = JSON.parse(localStorage.getItem("permission"));
            if (x === 'Noble Admin') {
                return true;
            }
            else if (storedColors != undefined) {


                var isFound = storedColors.find(function (item) {
                    if (item.permissionCategory === x) {
                        return true;

                    }
                    return false;
                });
                if (isFound !== undefined) {
                    return true;
                }
                else {
                    return false;
                }
            }

        },
        isLeftToRight: function () {
            return localStorage.getItem('LeftToRight') == "true" ? true : false;
        },
    }
 
}

