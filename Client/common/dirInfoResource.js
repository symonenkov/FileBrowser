(function () {
    "use strict";

    angular
		.module("common.services")
        .factory("dirInfoResource",
                 ["$resource",
                  "appSettings",
                    dirInfoResource])
						
    function dirInfoResource($resource, appSettings) {

        return $resource(appSettings.serverPath + "/api/getdirinfo/", {}, {
            post: {
                method: "POST",
                isArray: false,
                headers: { 'Content-Type': 'application/json; charset=UTF-8' }
            },
        });
    }

}());