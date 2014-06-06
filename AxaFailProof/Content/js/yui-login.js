YUI.add('login', function (Y) {

    // This module assumes the openid flow takes place via a popup that cannot be accessed directly.
    // Given this condition, and to maintain simplicity, it doesn't attempt to verfiy the assertion.
    // If you modify this script to use a file that processes the assertion directly, you should verify the assertion.
    // @see http://openid.net/specs/openid-authentication-2_0.html#verification
    // Licensed under Yahoo! BSD
    // @see http://gist.github.com/375593
    Y.namespace('login');

    Y.login.cookieName = 'login_session';
    Y.login.sessionReadyEventName = 'login:sessionReady';

    // This function constructs an openid login url for Yahoo!, and opens a popup to this location
    // @param {string} returnUrl is the url to redirect to after the user goes through the login flow.  Defaults to document.location.href
    Y.login.popup = function (returnUrl) {

        returnUrl = returnUrl || document.location.href;

        // @see http://openid.net/specs/openid-authentication-2_0.html#realms
        var realm = returnUrl.match(/(http[s]?:\/\/[^\/?]+)/)[0];

        // Load openid login flow in popup window
        // @see http://developer.yahoo.com/openid/
        var url = 'https://open.login.yahooapis.com/openid/op/auth?' + Y.QueryString.stringify({
            'openid.return_to': returnUrl,
            'openid.mode': 'checkid_setup',
            'openid.identity': 'http://specs.openid.net/auth/2.0/identifier_select',
            'openid.ns': 'http://specs.openid.net/auth/2.0',
            'openid.realm': realm,
            'openid.claimed_id': 'http://specs.openid.net/auth/2.0/identifier_select'
        });

        var name = 'login';
        var params = 'toolbar=1,scrollbars=1,location=1,statusbar=1,menubar=0,resizable=1, width=500,height=500,left=200,top=200';
        var popup = window.open(url, name, params);

        // Poll popup every 300 msec to see if it's redirected back to this domain
        // @see http://developer.yahoo.com/yui/3/api/YUI.html#method_later
        var timer = Y.later(300, Y, function () {

            // If user's closed window, stop timer
            if (popup.closed) {
                timer.cancel();
                return;
            }

            // Wrap in try/catch to avoid fatal cross-domain exceptions
            try {

                // Openid's response is called the "assertion"
                // @see http://openid.net/specs/openid-authentication-2_0.html#positive_assertions
                var assertion = popup.location.href.split('?')[1];
                assertion = Y.QueryString.parse(assertion);

                // Extract the user-specific info
                var session = {
                    'username': assertion['openid.identity']
                };

                var json = Y.JSON.stringify(session);

                // Cache the session in a cookie
                Y.Cookie.set(Y.login.cookieName, json);

                // Notify anyone who's listening in this yui sandbox that the session's ready
                Y.fire(Y.login.sessionReadyEventName);

                // Stop polling popup & close it
                timer.cancel();
                saveUserDetails(session['username'], "", "", "Yahoo");
                popup.close();

            } catch (e) {
                Y.log(e);
            }

        }, '', true);

        /* // Self-close login window if user hasn't completed flow in 30 sec
        Y.later(30000, Y, function () {
        timer.cancel();
        popup.close();
        });*/
    };

    // This function creates markup and event handling for a login button
    // @param {string} id is the id of the DOM element to insert login button into
    // @param {string} html is an optional param for custom button markup
    // @throws Error if an id is not passed in
    // @throws Error if there is no DOM element with the id passed in
    Y.login.renderLoginButton = function (id, html) {

        if (!id) {
            throw new Error('Y.login.renderLoginButton - A DOM element id is a required argument');
        }

        var button = Y.one('#' + id);

        if (!button) {
            throw new Error('Y.login.renderLoginButton - No DOM element with id "' + id + '" found');
        }

        html = html || '<img name="submit" type="image" class="btn-yahoo" value="Submit" style="cursor:pointer" src="/Content/images/btn_yahoo.png"/>';

        button.set('innerHTML', html);

        Y.on(Y.login.sessionReadyEventName, function () {

            // login handling is async, so remove handler after auth is complete
            Y.Event.purgeElement(button);

            //Y.login.renderLogoutButton(id);
        });

        // check for previously saved session
        var session = Y.Cookie.get(Y.login.cookieName);

        // if there is a session, fire session ready event and exit early
        if (session) {
            Y.fire(Y.login.sessionReadyEventName);
            return;
        }

        button.on('click', function (e) {
            Y.login.popup();
        });

    };

    // This function creates markup and event handling for a logout button
    // @param {string} id is the id of the DOM element to insert login button into
    // @param {string} html is an optional param for custom button markup
    // @throws Error if an id is not passed in
    // @throws Error if there is no DOM element with the id passed in
    Y.login.renderLogoutButton = function (id, html) {

        if (!id) {
            throw new Error('Y.login.renderLogoutButton - A DOM element id is a required argument');
        }

        var button = Y.one('#' + id);

        if (!button) {
            throw new Error('Y.login.renderLogoutButton - No DOM element with id "' + id + '" found');
        }

        //fetch username for display to make the login/logout a bit more realistic
        var json = Y.Cookie.get(Y.login.cookieName);
        var session = Y.JSON.parse(json);

        html = html || 'User id: ' + session['username'] + '<br><a href="#">log out</a>';

        button.set('innerHTML', html);

        var handler = button.on('click', function (e) {

            e.preventDefault();

            // remove session cookie
            Y.Cookie.remove(Y.login.cookieName);

            // remove logout click handler
            Y.Event.purgeElement(button);

            // restore login button
            Y.login.renderLoginButton(id);

        });

    };

}, '', { requires: ['cookie', 'event', 'querystring', 'json', 'node'] });