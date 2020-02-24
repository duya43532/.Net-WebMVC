$(function () {
    var ajaxFormSubmit = function () {
        var $form = $(this);
        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        };

        $.ajax(options).done(function (data) {
            var $target = $($form.attr("data-staff-target"));
            $target.replaceWith(data);
        });
        return false;
    };

    var submutAutoCompleteForm = function (event, ui) {
        var $input = $(this);
        $input.val(ui.item.label);
        var $form = $input.parents("form:first");
        $form.submit();
    };

    var createAutoComplete = function () {
        var $input = $(this);
        var options = {
            select: submutAutoCompleteForm,
            source: $input.attr("data-staff-autocomplete")
        }
        $input.autocomplete(options);
    };

    var getPage = function () {
        var $a = $(this);
        var options = {
            url: $a.attr("href"),
            data: $("form").serialize(),
            type: "get"
        };
        $.ajax(options).done(function (data) {
            var target = $a.parents("div.pagedList").attr("data-staff-target");
            $(target).replaceWith(data);
        });
        return false;
    };

    $("form[data-staff-ajax='true']").submit(ajaxFormSubmit);
    $("input[data-staff-autocomplete]").each(createAutoComplete);
    $(".body-content").on("click", ".pagedList a", getPage);
});