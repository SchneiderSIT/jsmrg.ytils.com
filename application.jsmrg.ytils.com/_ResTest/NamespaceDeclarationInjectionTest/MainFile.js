(function(){

    "use strict";
    if (!window.YTILS) {

        /**jsmrg include NamespaceDeclaration.js */

        /**
         * This method verifies that jQuery is installed in a sufficient version.
         *
         * @return {boolean}
         */
        YTILS.dependency.jQuery.verify = function() {

            var RADIX = 10;
            var jQueryVersion = ($ && $.fn && $.fn.jquery) ? $.fn.jquery.split('.') : null;

            if (!(jQueryVersion && parseInt(jQueryVersion[0], RADIX) >= 1 && parseInt(jQueryVersion[1], RADIX) >= 12)) {

                window.console.log('jQuery must be installed in version 1.12+ to run YTILS.ExternalLinkage.');
                return false;
            }

            return true;
        };
    }

    YTILS.defaults.externalLinkage = { };
    YTILS.defaults.externalLinkage.whitelist = [ ];
    YTILS.defaults.externalLinkage.addWww = false;
    YTILS.defaults.externalLinkage.debug = false;

    /**
     * The ExternalLinkage constructor.
     *
     * whitelist is an optional string array of addresses or hostnames that should be not opened on a blank target. If
     * nothing is passed in here, the whitelist will be empty.
     *
     * addWww is an optional boolean parameter that defaults to false. If set to true, all entries in whitelist will be
     * completed by their corresponding 'www.'-prefixed version.
     *
     * The debug parameter is optional as well and defaults to to false. If switched on, the public methods will write
     * logs to window.console with the decisions that are made internally.
     *
     * @param {string[]} whitelist - [ ]
     * @param {boolean} addWww - false
     * @param {boolean} debug - false
     * @returns {YTILS.ExternalLinkage}
     * @constructor
     */
    YTILS.ExternalLinkage = function(whitelist, addWww, debug) {

        var whitelist_ = whitelist || YTILS.defaults.externalLinkage.whitelist;
        var addWww_ = addWww || YTILS.defaults.externalLinkage.addWww;
        var debug_ = debug || YTILS.defaults.externalLinkage.debug;
        var externalLinkage = { };
        var preparedWhitelist = [ ];
        var isWhiteListedOrAnchor;
        var prepareWhitelist;

        if (!YTILS.dependency.jQuery.verify()) {

            return null;
        }

        prepareWhitelist = function(element) {

            element = element.toLowerCase();

            preparedWhitelist.push(element);

            if (addWww_) {

                preparedWhitelist.push('www.' + element);
            }
        };

        /**
         * Determines if an anchor element has to be handled whitelisted or not. Jump anchors starting with '#' are
         * handled whitelisted as well.
         *
         * @param {object} a - The anchor element handle.
         * @returns {boolean}
         */
        isWhiteListedOrAnchor = function(a) {

            var aHref = a.href.toLowerCase();
            var aHrefWOHttp;
            var whitelistCheck;

            if (a.getAttribute('href').substr(0, 1) === '#') {

                return true;
            }

            if (aHref.substr(0, 8) === 'https://') {

                aHrefWOHttp = aHref.substr(8);

            } else if (aHref.substr(0, 7) === 'http://') {

                aHrefWOHttp = aHref.substr(7);
            }

            if (!aHrefWOHttp) {

                return false;
            }

            whitelistCheck = function(preparedWhitelistEntry) {

                return aHrefWOHttp.substr(0, preparedWhitelistEntry.length) === preparedWhitelistEntry;
            };

            return preparedWhitelist.some(whitelistCheck);
        };

        /**
         * Running this method will check all anchors that are detected by the given jQuery selector if they are
         * whitelisted or not. If the whitelist does not match, the anchor's target will be changed or set to '_blank'.
         * The default for selector is 'a' which means that are anchor will be checked.
         *
         * @param {string} selector - A jQuery selector to find the anchors that should be handled. 'a' is the default.
         */
        externalLinkage.openBlank = function(selector) {

            var selector_ = selector || 'a';

            if (debug_ && !preparedWhitelist.length) {

                window.console.log('The whitelist is empty. ' +
                    'openBlank() will handle all anchors found by selector, ignoring the jump anchors.');
            }

            // Even on empty whitelist we have to run the loop as we do not want to manipulate jump anchors.
            $(selector_).each(function() {

                if (!isWhiteListedOrAnchor(this)) {

                    $(this).attr('target', '_blank');

                    if (debug_) {

                        window.console.log('ExternalLinkage.openBlank() - Not whitelisted: ' + this.href);
                    }

                } else {

                    if (debug_) {

                        window.console.log('ExternalLinkage.openBlank() - Whitelisted: ' + this.href);
                    }
                }
            });
        };

        whitelist_.forEach(prepareWhitelist);

        //noinspection JSValidateTypes
        return externalLinkage;
    };

    /**
     * Factory method for YTILS.ExternalLinkage.
     *
     * @param {string[]} whitelist - [ ]
     * @param {boolean} addWww - false
     * @param {boolean} debug - false
     * @returns {YTILS.ExternalLinkage}
     */
    YTILS.create.externalLinkage = function(whitelist, addWww, debug) {

        var whitelist_ = whitelist || YTILS.defaults.externalLinkage.whitelist;
        var addWww_ = addWww || YTILS.defaults.externalLinkage.addWww;
        var debug_ = debug || YTILS.defaults.externalLinkage.debug;

        return new YTILS.ExternalLinkage(whitelist_, addWww_, debug_);
    };

}());