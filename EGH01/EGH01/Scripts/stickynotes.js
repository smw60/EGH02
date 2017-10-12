zIndex = 10;
var notes=new Array();
var NoteIndex = 1;
function addNote(data) {
    $('#notes')
        .append('\
            <div class="sticky-note-pre ui-widget-content" id="note' + (NoteIndex) + '">\
                <div class="handle">&nbsp;<div class="close" onclick="NoteIndex=' + (NoteIndex++) + ';$(\'#delNoteModal\').modal(\'show\');">&times;</div></div>\
                <div contenteditable class="contents">'+data+'</div>\
            </div>\
         ')
        .find('.sticky-note-pre')
            //.position where we want it
        .end()
    //.do something else to $('#notes')
    ;
    $('.sticky-note-pre')
        /*.draggable({
            handle: '.handle'
        })
        .resizable({
            resize: function () {
                var n = $(this);
                n.find('.contents').css({
                    width: n.width() - 40,
                    height: n.height() - 60
                });
            }
        })*/
        .bind('click hover focus mousedown', function () {
            $(this)
                .css('zIndex', zIndex++)
                .find('.ui-icon')
                    .css('zIndex', zIndex++)
                .end()
            ;
        })
        /*.dblclick(function () {
            $(this).remove();
        })*/
        .addClass('sticky-note')
        .removeClass('sticky-note-pre')
    ;
}
$('#newNote').click(function () {
    addNote('Введите текст');
});
function saveNotes() {
    var savedNotes = [], i, note;

    $('.sticky-note').each(function () {
        savedNotes.push($(this).find('.contents').html());
    });

    $.cookie("notes", savedNotes, {})

}
$('#save').click(function () {
    saveNotes();
});

if ($.cookie("notes") != undefined && $.cookie("notes") != "") {
    notes = $.cookie("notes").split(',');
    for (i = 0; i < notes.length; i++) {
        addNote(notes[i]);
    }
}

$('#commitDelNote').click(function () {
    $('#note' + NoteIndex).remove();
    saveNotes();
    $('#delNoteModal').modal('hide');
});

$(window).on('unload', function () {
    saveNotes();
});

$('#commitLeaveNote').click(function () {
    saveNotes();
    $('#leaveNoteModal').modal('hide');
});