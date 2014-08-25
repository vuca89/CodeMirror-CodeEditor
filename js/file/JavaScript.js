/// <reference path="../libs/jquery-1.11.0.min.js" />

var editor = CodeMirror.fromTextArea($('textarea[id$="code"]')[0], {
    styleActiveLine: true,
    lineNumbers: true,
    lineWrapping: true,
    theme: 'blackboard',
    //add on matchBrackets
    matchBrackets: true,
    autoCloseBrackets: true,
    highlightSelectionMatches: { showToken: /\w/ },
    //check error
    lint: true,
    extraKeys: {
        "Ctrl-Q": function (cm) { cm.foldCode(cm.getCursor()); },
        "Ctrl-Space": "autocomplete",
        "Ctrl-H": "replace",
        //full screen
        "F11": function (cm) {
            cm.setOption("fullScreen", !cm.getOption("fullScreen"));
        },
        "Esc": function (cm) {
            if (cm.getOption("fullScreen")) cm.setOption("fullScreen", false);
        }
    },
    foldGutter: true,
    gutters: [
            //fold code
            "CodeMirror-linenumbers", "CodeMirror-foldgutter",
            //lint
            "CodeMirror-lint-markers"],
    mode: { name: "javascript", globalVars: true }
});