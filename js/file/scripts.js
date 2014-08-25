/// <reference path="../libs/jquery-1.11.0.min.js" />

$(function () {
    $("#menu-toggle").click(function (e) {
        e.preventDefault();
        $("#wrapper").toggleClass("toggled");
    });

    //tree//<i class="glyphicon glyphicon-plus"></i>
    $('#tree li:has(ul)').each(function () {
        $(this).addClass('parent_li').find(' > span').attr('title', 'Collapse this branch');
        var liadd = $('<li style="display:none"><a class="add btn-success"><i class="glyphicon glyphicon-plus"></i></a></li>');
        liadd.find('a.add').click(function () {
            var active = $('#tree').find('.marked');
            if (active.length > 0)
                active.removeClass('marked');
            $(this).addClass('marked');
            $('#modalnewfile').modal('show');
            setTimeout(function () {
                $('#txtfilename').val('');
                $('#txtfilename').focus();
            }, 500);
        });
        $(this).children('ul:eq(0)').append(liadd);
    });

    $('#tree li.parent_li > span').on('click', function (e) {
        var children = $(this).parent('li.parent_li').find(' > ul > li');
        if (children.is(":visible")) {
            children.hide('fast');
            $(this).attr('title', 'Expand').find(' > i').addClass('glyphicon-folder-close').removeClass('glyphicon-folder-open');
        } else {
            children.show('fast');
            $(this).attr('title', 'Collapse').find(' > i').addClass('glyphicon-folder-open').removeClass('glyphicon-folder-close');
        }
        e.stopPropagation();
    });

    var fileColl = $('#jsfile li > a.item,#cssfile li > a.item');
    if (fileColl.length > 0) {
        fileColl.each(function () {
            BindLinkClick(this);
        });
    }

    $('#modalnewfile .btn-success').click(function () {
        if ($('#txtfilename').val().length > 0) {
            var marked = $('#tree .marked');
            if (marked.length > 0) {
                var type = marked.closest('li[id]').attr('id');
                var ext = '';
                if (typeof type !== 'undefined') {
                    if (type.indexOf('js') === 0)
                        ext = '.js';
                    else if (type.indexOf('css') === 0)
                        ext = '.css';
                }
                var active = $('#tree').find('.active');
                if (active.length > 0)
                    active.removeClass('active');
                var newli = $('<li style=""><a class="item active" href="javascript:void(0);">' +
                     $('#txtfilename').val() + ext + '</a></li>');
                BindLinkClick(newli.find('a')[0]);
                newli.insertBefore(marked.parent());
                $('#savefile,#deletefile,#renamefile').removeClass('disabled');
                $('#txtfilename').val('');
                $('#modalnewfile').modal('hide');
            }
        }
    });

    $('#modalrename .btn-warning').click(function () {
        if ($('#txtnewname').val().length > 0) {
            var active = $('#tree .active');
            if (active.length > 0) {
                var datapath = active.closest('.parent_li').attr('datapath');
                var dataid = (typeof datapath !== 'undefined' ? datapath : '') + '/' + $.trim(active.text());
                var type = active.closest('li[id]').attr('id');
                var ext = '';
                if (typeof type !== 'undefined') {
                    if (type.indexOf('js') === 0)
                        ext = '.js';
                    else if (type.indexOf('css') === 0)
                        ext = '.css';
                }
                var name = $('#txtnewname').val();
                if (name.lastIndexOf(ext) !== (name.length - ext.length))
                    name = name + ext;

                ajaxGet('SaveContent.aspx', 't=' + (typeof type !== 'undefined' ? type : '') +
                 '&id=' + dataid + '&name=' + name, function (data) {
                     if (typeof data !== 'undefined' && data.length > 0 && data.toLowerCase() === 'true') {
                         active.text(name);
                         $('#txtnewname').val('');
                         $('#modalrename').modal('hide');
                     }
                     else {
                         $('#sucesstitle').text('There was an error when rename file (' + dataid + ').');
                         $('#modalsuccess').modal('show');
                     }
                 });
            }
        }
    });

    $('#modaldelete .btn-danger').click(function () {
        var active = $('#tree').find('.active');
        if (active.length > 0) {
            var datapath = active.closest('.parent_li').attr('datapath');
            var dataid = (typeof datapath !== 'undefined' ? datapath : '') + '/' + $.trim(active.text());
            var type = active.closest('li[id]').attr('id');
            ajaxGet('DeleteContent.aspx', 't=' + (typeof type !== 'undefined' ? type : '') +
                 '&id=' + dataid, function (data) {
                     $('#modaldelete').modal('hide');
                     if (typeof data !== 'undefined' && data.length > 0 && data.toLowerCase() === 'true') {
                         var active = $('#tree').find('.active');
                         if (active.length > 0) {
                             active.fadeOut('slow', function () {
                                 $(this).remove();
                             });
                         }
                         $('#savefile,#deletefile,#renamefile').addClass('disabled');
                         $('#sucesstitle').text('The file (' + dataid + ') was deleted.');
                     }
                     else
                         $('#sucesstitle').text('There was an error when delete file (' + dataid + ').');
                     $('#modalsuccess').modal('show');
                 });
        }
    });

    $('#savefile').click(function () {
        if ($(this).hasClass('disabled'))
            return;
        var active = $('#tree').find('.active');
        if (active.length > 0) {
            var datapath = active.closest('.parent_li').attr('datapath');
            var dataid = (typeof datapath !== 'undefined' ? datapath : '') + '/' + $.trim(active.text());
            var type = active.closest('li[id]').attr('id');
            ajaxGet('SaveContent.aspx', 't=' + (typeof type !== 'undefined' ? type : '') +
                 '&id=' + dataid + (typeof editor !== 'undefined' ? ('&data=' + encodeURIComponent(editor.getValue())) : ''), function (data) {
                     if (typeof data !== 'undefined' && data.length > 0 && data.toLowerCase() === 'true')
                         $('#sucesstitle').text('The file (' + dataid + ') was saved.');
                     else
                         $('#sucesstitle').text('There was an error when process file (' + dataid + ').');
                     $('#modalsuccess').modal('show');
                 });
        }
    });

    $('#deletefile').click(function () {
        if ($(this).hasClass('disabled'))
            return;
        var active = $('#tree .active');
        if (active.length > 0) {
            var datapath = active.closest('.parent_li').attr('datapath');
            var dataid = (typeof datapath !== 'undefined' ? datapath : '') + '/' + $.trim(active.text());
            $('#deletetitle').text('Are you sure to delete the file (' + dataid + ')');
            $('#modaldelete').modal('show');
        }
    });

    $('#renamefile').click(function () {
        var active = $('#tree .active');
        if (active.length > 0) {
            $('#modalrename').modal('show');
            $('#txtnewname').val($.trim(active.text()));

            setTimeout(function () {
                $('#txtnewname').focus();
            }, 500);
        }
    });
});

function BindLinkClick(link) {
    $(link).click(function (e) {
        if ($(link).hasClass('active'))
            return false;
        var active = $('#tree .active');
        if (active.length > 0)
            active.removeClass('active');
        $(link).addClass('active');

        $('#savefile,#deletefile,#renamefile').removeClass('disabled');

        var datapath = $(link).closest('.parent_li').attr('datapath');
        var dataid = (typeof datapath !== 'undefined' ? datapath : '') + '/' + $.trim($(link).text());
        var type = $(link).closest('li[id]').attr('id');
        ajaxGet('GetContent.aspx', 't=' + (typeof type !== 'undefined' ? type : '') +
             '&id=' + dataid, function (data) {
                 if (typeof editor !== 'undefined' && typeof type !== 'undefined') {
                     var curType = editor.getMode().name;
                     //console.log('curType : ', curType, ', type : ', type);
                     //same as editor mode
                     if ((type.indexOf('css') === 0 && curType.toLowerCase() === 'css') ||
                         (type.indexOf('js') === 0 && curType.toLowerCase() === 'javascript')) {
                         editor.setValue(data);
                     }
                     else {
                         //change mode
                         editor.setOption('lint', false);
                         setTimeout(function () {
                             try {
                                 if (curType.toLowerCase() === 'css')
                                     editor.setOption('mode', { name: 'javascript', globalVars: true });
                                 else
                                     editor.setOption('mode', { name: 'css' });
                                 editor.setValue(data);
                                 editor.setOption('lint', true);
                             }
                             catch (ex) {
                                 alert('error : ' + ex.message);
                             }
                         }, 800);
                     }
                 }
             });
        e.stopPropagation();
    });
}

var ajRequest;
function ajaxGet(urlGet, dataValue, callback) {
    /// <summary>Get data from server by jQuery.</summary>
    //console.log('ajaxGet : ', dataValue);
    if (!$('form').hasClass('progressClass')) {
        ajRequest = $.ajax({
            type: "post",
            async: true,
            url: urlGet,
            //traditional: false,
            //dataType: "json",
            data: dataValue,
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            beforeSend: function () {
                $('form').addClass('progressClass');
            },
            success: function (data) {
                $('form').removeClass('progressClass');
                if (typeof callback === 'function')
                    callback(data);
            },
            error: function () {
                $('form').removeClass('progressClass');
            }
        });
    }
    else {
        console.log('Can\'t load data while process');
    }
}