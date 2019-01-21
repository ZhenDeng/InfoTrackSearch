// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('.searchbutton button').click(
    function () {
        var flag = true;
        $('.resulttable').html('');
        if ($('#inputkeywords').val() === '' || $('#inputurl').val() === '') {
            flag = false;
        }

        if (!flag) {
            //show error msg popup when any field is empty
            $('.searchbutton button').showValidator('Please Fill All Fields', 'error', 'right');
        } else {
            var keywords = $('#inputkeywords').val();
            var url = $('#inputurl').val();
            var index = [];
            var urls = [];
            var tempHtml = '';
            if (!validateURL(url)) {
                //show error msg popup when input url format is wrong
                $('.searchbutton button').showValidator('Please Input right url string', 'error', 'right');
            } else {
                //use ajax to call api to get indexs and urls
                $.ajax({
                    type: "GET",
                    url: 'api/InfoSearch/ResultList?keywords=' + keywords + '&url=' + url,
                    async: false,
                    success: function (data) {
                        if (data) {
                            index = data.indexList;
                            urls = data.urlList;
                        }
                    },
                    error: function (request, status, error) {
                        console.error(error);
                    }
                });

                //show success msg popup and append table of result list to home page
                $('.searchbutton button').showValidator('Search Successfully', 'success', 'right');
                tempHtml += '<p class="resultmsg">Target url appears ' + index.length + ' time(s) in search result</p>';
                tempHtml += '<table class="table">';
                tempHtml += '<thead><tr><th scope="col" width="20%">Search Result Index</th><th scope="col" width="80%">Url</th></tr></thead>';
                tempHtml += '<tbody class="tablebody">';
                for (var i = 0; i < index.length; i++) {
                    tempHtml += '<tr>';
                    tempHtml += '<td>' + index[i] + '</td>';
                    tempHtml += '<td><a href="https://' + urls[i].replace('https://', '').replace('http://', '').replace(' ', '') + '" target="_blank">' + urls[i].replace('https://', '').replace('http://', '') + '</a></td>';
                    tempHtml += '</tr>';
                }
                tempHtml += '</tbody>';
                tempHtml += '</table>';
                $('.resulttable').html(tempHtml);
            }
        }
    }
);

//validate url input
function validateURL(textval) {
    var urlregex = new RegExp("^([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&amp;%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{2}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&amp;%\$#\=~_\-]+))*$");
    return urlregex.test(textval);
}
