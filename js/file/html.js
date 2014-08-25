/// <reference path="../libs/jquery-1.11.0.min.js" />

var editor = CodeMirror.fromTextArea($('textarea[id$="code"]')[0], {
    styleActiveLine: true,
    lineNumbers: true,
    lineWrapping: true,
    mode: 'htmlembedded',
    theme: 'blackboard',
    //add on matchBrackets
    matchBrackets: true,
    matchTags: { bothTags: true },
    autoCloseTags: true,
    autoCloseBrackets: true,
    highlightSelectionMatches: { showToken: /\w/ },
    extraKeys: {
        //fold code
        "Ctrl-Q": function (cm) { cm.foldCode(cm.getCursor()); },
        "Ctrl-J": "toMatchingTag",
        "Ctrl-Space": "autocomplete",
        "Ctrl-H": "replace",
        //"Ctrl-F": "search",
        //full screen
        "F11": function (cm) {
            cm.setOption("fullScreen", !cm.getOption("fullScreen"));
        },
        "Esc": function (cm) {
            if (cm.getOption("fullScreen")) cm.setOption("fullScreen", false);
        }
    },
    //fold code
    foldGutter: true,
    gutters: [
        //fold code
        "CodeMirror-linenumbers", "CodeMirror-foldgutter"]
});

$(function () {
});