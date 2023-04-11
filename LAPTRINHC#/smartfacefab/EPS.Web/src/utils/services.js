import '@/assets/jquery/jquery-ajax-blob-arraybuffer'
import saveAs from 'file-saver'
import i18n from '@/libs/i18n';
var isDevEnv = process.env.NODE_ENV == "development";

var services = {
    SERVICE_END_POINT: isDevEnv ? 'http://localhost:1938/' : '/Service',
    TOKEN_END_POINT: isDevEnv ? 'http://localhost:1938/api/token/auth' : '/Service/api/token/auth',
    API_END_POINT: isDevEnv ? 'http://localhost:1938/api/' : '/Service/api/',
    clientId: 'IAC_Cloud',
    clientSecret: '1a82f1d60ba6353bb64a8fb4b05e4bc4',
    getAccessToken: function () {
        return JSON.parse(localStorage.getItem("_ATFactoryAccessToken"));
    },
    setAccessToken: function (token) {
        return localStorage.setItem("_ATFactoryAccessToken", JSON.stringify(token));
    },
    clearAccessToken: function () {
        localStorage.removeItem("_ATFactoryAccessToken");
    },
    getAjaxOptions: function () {
        var option = {
            //dataType: "json",
            contentType: 'application/json',//"application/x-www-form-urlencoded",
            cache: false,
            crossDomain: true,
            async: true,
            timeout: 600000
        };

        if (this.getAccessToken()) {
            option.headers = {
                'Authorization': 'Bearer ' + this.getAccessToken().access_token
            };
        }

        return option;
    },
    request: function (requestType, url, data, options) {
        var self = this;
        var showErrorMsg = true;

        // check token expiration
        var token = self.getAccessToken();

        if (token) {
            if (new Date(token['expires']) <= new Date() && token.refresh_token) {
                this.refreshToken(token.refresh_token);
            }
        }

        var ajaxOptions = self.getAjaxOptions(data);

        if (url.indexOf('http') != 0) {
            url = this.API_END_POINT + url;
        }

        ajaxOptions.async = true;
        ajaxOptions.url = url;
        ajaxOptions.type = requestType;

        if (options) $.extend(ajaxOptions, options);

        if (ajaxOptions.type.toUpperCase() == 'GET') {
            ajaxOptions.data = data;
        } else {
            ajaxOptions.data = data ? JSON.stringify(data) : null;
        }

        return $.ajax(ajaxOptions)
            .always(function () {
            })
            .fail(function (error) {
                if (error.statusText == "timeout") {
                    if (showErrorMsg) {
                        alert.error('Operation timeout. Please try again later.');
                    }
                } else {
                    if (showErrorMsg) {
                        if (error.responseJSON) {
                            let message = error.responseJSON.ExceptionMessage || error.responseJSON.Message || error.responseJSON.error
                                || (typeof (error.responseJSON) == "string" ? error.responseJSON : JSON.stringify(error.responseJSON));
                            let translateMsg = i18n.t(message);
                            alert(translateMsg);
                        } else if (error.responseText) {
                            let translateMsg = i18n.t(error.responseText);
                            alert(translateMsg);
                        }
                    }
                }
            });
    },
    get: function (url, data, options) {
        return this.request('GET', url, data, options);
    },
    post: function (url, data, options) {
        return this.request('POST', url, data, options);
    },
    put: function (url, data, options) {
        return this.request('PUT', url, data, options);
    },
    delete: function (url, options) {
        return this.request('DELETE', url, null, options);
    },
    // services
    login: function (username, password) {
        var self = this;
        var data = {
            username: username,
            password: password,
            grant_type: 'password',
            client_id: this.clientId,
            client_secret: this.clientSecret
        };
        return $.ajax({
            type: 'POST',
            async: true,
            data: JSON.stringify(data),
            url: this.TOKEN_END_POINT,
            contentType: "application/json",
            dataType: 'Json',
            crossDomain: true,
            timeout: 300000
        }).done(function (token) {
            self.setAccessToken(token);
        });
    },
    refreshToken: function (refreshToken) {
        var self = this;
        // remove current access token
        self.clearAccessToken();

        // refresh the access token
        var data = {
            grant_type: 'refresh_token',
            refresh_token: refreshToken,
            client_id: this.clientId,
            client_secret: this.clientSecret
        };

        return $.ajax({
            type: 'POST',
            async: false,
            data: JSON.stringify(data),
            url: this.TOKEN_END_POINT,
            contentType: "application/json",
            dataType: 'Json',
            crossDomain: true,
            timeout: 300000
        }).done(function (token) {
            self.setAccessToken(token);
        });
    },
    logout: function () {
        var self = this;
        var accessToken = this.getAccessToken();

        // refresh the access token
        var data = {
            grant_type: 'invalidate_token',
            refresh_token: accessToken.refresh_token,
            client_id: this.clientId,
            client_secret: this.clientSecret
        };

        self.clearAccessToken();
        //return $.ajax({
        //    type: 'POST',
        //    async: true,
        //    data: data,
        //    url: this.TOKEN_END_POINT,
        //    contentType: "application/x-www-form-urlencoded",
        //    //dataType: 'Json',
        //    crossDomain: true,
        //    timeout: 300000
        //}).done(function (token) {
        //});
    },
    upload: function (formdata) {
        //return this.post(`${this.API_END_POINT}file`, formdata, true, {
        //    processData: false,
        //    contentType: false,
        //    enctype: 'multipart/form-data'
        //});

        // check token expiration
        var token = this.getAccessToken();
        if (token) {
            if (new Date(token['expires']) <= new Date() && token.refresh_token) {
                this.refreshToken(token.refresh_token);
            }
        }

        token = this.getAccessToken();
        return $.ajax({
            type: 'POST',
            async: true,
            processData: false,
            contentType: false,
            enctype: 'multipart/form-data',
            data: formdata,
            url: this.API_END_POINT + 'file',
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
        });
    },
    getResourceUrl: function (path) {
        if (path) {
            return this.SERVICE_END_POINT + "/" + this.STOREDCALLS_FOLDER + "/" + path.substring(this.EMAS_STOREDCALLS_FOLDER.length);
        } else {
            return '';
        }
    },
    getUploadResourceUrl: function (path) {
        if (path) {
            return this.SERVICE_END_POINT + "/" + this.UPLOADEDCALLS_FOLDER + "/" + path.substring(this.EMAS_UPLOADEDCALLS_FOLDER.length);
        } else {
            return '';
        }
    },
    encodeURIComponent: function (obj) {
        var str = [];
        for (var p in obj)
            if (obj.hasOwnProperty(p)) {
                str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
            }
        return str.join("&");
    },
    downloadCall: function (callId) {
        return this.get(`Call/DownloadAudio?Id=${callId}`, null, true, {
            contentType: 'application/octet-stream',
            dataType: 'arraybuffer'
        }).done(function (data, textStatus, jqXHR) {
            var blob = new Blob(
                [data],
                { type: jqXHR.getResponseHeader('content-type') }
            );
            saveAs(blob, jqXHR.getResponseHeader('Filename'));
        });
    },
    downloadCsv: function (url, params) {
        return this.get(url, params, true, {
            contentType: 'application/csv',
            dataType: 'arraybuffer'
        }).done(function (data, textStatus, jqXHR) {
            var blob = new Blob(
                [data],
                { type: jqXHR.getResponseHeader('content-type') }
            );
            saveAs(blob, jqXHR.getResponseHeader('Filename'));
        });
    }
};
export default services;
