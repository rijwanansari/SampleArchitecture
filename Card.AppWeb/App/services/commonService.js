app.service('common', [ function () {
    var common = {};
    common.CheckFileExtension = function (extension) {

        if (extension == 'js' || extension == 'exe' || extension == 'vss') {           
            swal("Warning!", "File extension with .exe, .js, .vss not allowed", "warning");           
            return true;
        }      
    }
    return common;
}]);
