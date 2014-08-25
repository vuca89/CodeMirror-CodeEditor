/// <reference path="../libs/jquery-1.11.0.min.js" />

CodeMirror.commands.autocomplete = function (cm) {
    var doc = cm.getDoc();
    var POS = doc.getCursor();
    var mode = CodeMirror.innerMode(cm.getMode(), cm.getTokenAt(POS).state).mode.name;

    if (mode == 'xml') { //html depends on xml
        CodeMirror.showHint(cm, CodeMirror.hint.html);
    } else if (mode == 'javascript') {
        CodeMirror.showHint(cm, CodeMirror.hint.javascript);
    } else if (mode == 'css') {
        CodeMirror.showHint(cm, CodeMirror.hint.css);
    }
};

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
    mode: { name: "css" }
});

$(function () {

});