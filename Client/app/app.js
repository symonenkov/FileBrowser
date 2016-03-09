(function () {
    "use strict";

    var app = angular.module("directoryBrowser",
                            ["common.services"]);

    app.config(['$resourceProvider', function ($resourceProvider) {
        // Don't strip trailing slashes from calculated URLs
        $resourceProvider.defaults.stripTrailingSlashes = false;
    }]);

}());

