(function () {
    "use strict";
    angular
        .module("directoryBrowser")
        .controller("dirInfoListCtrl",
                     ["dirInfoResource",
                         dirInfoListCtrl]);

    function dirInfoListCtrl(dirInfoResource) {
        var vm = this;

        dirInfoResource.post({}, "'listOfDrives'", function (data) {
            vm.dirInfos = data;
        });

        vm.OpenDirectory = function (path) {

            if (path[0] == "\\") {
                dirInfoResource.post({}, "'" + path.substring(1).replace(/\\/g, "\\\\") + "'", function (data) {
                    vm.dirInfos = data;
                });
            }

            dirInfoResource.post({}, "'" + path.replace(/\\/g, "\\\\") + "'", function (data) {
                vm.dirInfos = data;
            });   
        }

        vm.ReturnToParentDirectory = function (parent, current) {

            if (parent == current) {
                dirInfoResource.post({}, "'listOfDrives'", function (data) {
                    vm.dirInfos = data;
                });
            }
            else {

                dirInfoResource.post({}, "'" + parent.replace(/\\/g, "\\\\") + "'", function (data) {
                    vm.dirInfos = data;
                });
            }
        }

    }
}());