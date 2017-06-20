//View Model
function LiveViewModel() {
    this.TestText = ko.observable("Bert");
}

$(function () {
    initializeSliders();

    ko.applyBindings(new LiveViewModel());

    var connection = $.hubConnection("http://localhost:17757");
    //var connection = $.hubConnection("http://racetimecoreapi");
    var testHub = connection.createHubProxy('assettoCorsaHub');

    //Register Events
    testHub.on('lapCompleted', LapCompletedEvent);
    testHub.on('lapUpdated', LapUpdatedEvent);
    testHub.on('newLap', NewLapEvent);
    testHub.on('newCollision', NewCollisionEvent);
    testHub.on('newCompetitor', NewCompetitorEvent);
    testHub.on('updatedCompetitor', CompetitorUpdatedEvent);
    testHub.on('disconnectedCompetitor', CompetitorDisconnectedEvent);

    connection.start()
        .done(function () {
            console.log('Now connected, connection ID=' + connection.id);
            testHub.invoke('try').done(function () {
                console.log('Invocation of NewContosoChatMessage succeeded');
            }).fail(function (error) {
                console.log('Invocation of NewContosoChatMessage failed. Error: ' + error);
            });
        })
        .fail(function () { console.log('Could not connect'); });
});



// Server Push Events
function LapCompletedEvent(data) {
    console.log("Lap Completed Recieved");
    console.log(data);
}

function LapUpdatedEvent(data) {
    console.log("Lap Updated Recieved");
    console.log(data);
}

function NewLapEvent(data) {
    console.log("New Lap Recieved");
    console.log(data);
}

function NewCollisionEvent(data) {
    console.log("New Collision Recieved");
    console.log(data);
}

function NewCompetitorEvent(data) {
    console.log("New Competitor Recieved");
    console.log(data);
}

function CompetitorUpdatedEvent(data) {
    console.log("Competitor Updated Recieved");
    console.log(data);
}

function CompetitorDisconnectedEvent(data) {
    console.log("Competitor Disconnected Recieved");
    console.log(data);
}



function initializeSliders() {
    $("#range_cols").ionRangeSlider({
        min: 1,
        max: 3,
        from: 2,
        onChange: function (data) {
            updateColCount(data);
        }
    });

    $("#range_col1").ionRangeSlider({
        min: 1,
        max: 12,
        from: 8,
        step: 1,
        from_max: 11,
        onChange: function (data) {
            updateSliderCol1(data);
        }
    });

    $("#range_col2").ionRangeSlider({
        type: "double",
        min: 1,
        max: 12,
        from: 8,
        to: 12,
        step: 1,
        from_fixed: true,
        to_min: 9,
        to_max: 12,
        onChange: function (data) {
            updateSliderCol2(data);
        }
    });

    $("#range_col3").ionRangeSlider({
        type: "double",
        min: 1,
        max: 12,
        from: 10,
        to: 12,
        step: 1
    });


    $("#range_container_3").hide();
}

function updateColCount(data) {
    if (data.from == 1) {
        $("#range_container_2").hide();
        $("#range_container_3").hide();

        var slider = $("#range_col1").data("ionRangeSlider");
        slider.update({
            min: 1,
            max: 12,
            from: 12,
            from_min: 12,
            from_max: 12,
            step: 1
        });

    }
    else if (data.from == 2) {
        $("#range_container_2").show();
        $("#range_container_3").hide();

        var slider = $("#range_col1").data("ionRangeSlider");
        slider.update({
            min: 1,
            max: 12,
            from: 8,
            from_min: 0,
            from_max: 11,
            step: 1
        });

        var slider = $("#range_col2").data("ionRangeSlider");
        slider.update({
            type: "double",
            min: 1,
            max: 12,
            from: 8,
            to: 12,
            step: 1,
            from_fixed: true,
            to_min: 9,
            to_max: 12
        });
    }
    else {
        $("#range_container_2").show();
        $("#range_container_3").show();

        var slider = $("#range_col1").data("ionRangeSlider");
        slider.update({
            min: 1,
            max: 12,
            from: 6,
            from_min: 0,
            from_max: 10,
            step: 1
        });

        var slider = $("#range_col2").data("ionRangeSlider");
        slider.update({
            type: "double",
            min: 1,
            max: 12,
            from: 6,
            to: 9,
            step: 1,
            from_fixed: true,
            to_min: 7,
            to_max: 11
        });

        var slider = $("#range_col3").data("ionRangeSlider");
        slider.update({
            type: "double",
            min: 1,
            max: 12,
            from: 9,
            to: 12,
            step: 1,
            from_fixed: true,
            to_min: 10,
            to_max: 12
        });
    }
    updateGridDisplay();

}

function updateSliderCol1(data) {
    var slider = $("#range_cols").data("ionRangeSlider");
    if (slider.old_from == 2) {
        var updateSlider = $("#range_col2").data("ionRangeSlider");
        updateSlider.update({
            type: "double",
            min: 1,
            max: 12,
            from: data.from,
            to: 12,
            step: 1,
            from_fixed: true,
            to_min: 12,
            to_max: 12
        });
    }
    else if (slider.old_from == 3) {
        var updateSlider = $("#range_col2").data("ionRangeSlider");
        var updateSlider2 = $("#range_col3").data("ionRangeSlider");

        updateSlider.update({
            type: "double",
            min: 1,
            max: 12,
            from: data.from,
            step: 1,
            from_fixed: true,
            to_min: data.from + 1,
            to_max: 11
        });

        updateSlider2.update({
            type: "double",
            min: 1,
            max: 12,
            from: updateSlider.old_to,
            to: 12,
            step: 1,
            from_fixed: true,
            to_min: 12,
            to_max: 12
        });
    }

    updateGridDisplay();
}

function updateSliderCol2(data) {
    var slider = $("#range_cols").data("ionRangeSlider");
    if (slider.old_from == 3) {
        var updateSlider = $("#range_col3").data("ionRangeSlider");

        updateSlider.update({
            type: "double",
            min: 1,
            max: 12,
            from: data.to,
            to: 12,
            step: 1,
            from_fixed: true,
            to_min: 12,
            to_max: 12
        });
    }
    updateGridDisplay();

}

function updateGridDisplay() {
    var slider = $("#range_cols").data("ionRangeSlider");
    if (slider.old_from == 1) {
        document.getElementById('col1').className = "col-md-12 column sortable";
        $('#col2').hide();
        $('#col3').hide();
    }
    else if (slider.old_from == 2) {
        $('#col2').show();
        $('#col3').hide();

        var slider = $("#range_col1").data("ionRangeSlider");
        document.getElementById('col1').className = "col-md-" + slider.old_from + " column sortable";

        var slider = $("#range_col2").data("ionRangeSlider");
        var value = slider.result.to - slider.result.from;
        document.getElementById('col2').className = "col-md-" + value + " column sortable";
    }

    else if (slider.old_from == 3) {
        $('#col2').show();
        $('#col3').show();

        var slider = $("#range_col1").data("ionRangeSlider");
        document.getElementById('col1').className = "col-md-" + slider.old_from + " column sortable";

        slider = $("#range_col2").data("ionRangeSlider");
        value = slider.result.to - slider.result.from;
        document.getElementById('col2').className = "col-md-" + value + " column sortable";

        slider = $("#range_col3").data("ionRangeSlider");
        value = slider.result.to - slider.result.from;
        document.getElementById('col3').className = "col-md-" + value + " column sortable";
    }
}