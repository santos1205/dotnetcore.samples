function legend(parent, data) {
    legend(parent, data, null);
}

function legend(parent, data, chart) {
    parent.className = 'legend row';
    var datas = data.hasOwnProperty('datasets') ? data.datasets : data;

    console.log(parent);
    // remove possible children of the parent
    while(parent.hasChildNodes()) {
        parent.removeChild(parent.lastChild);
    }

    var show = chart ? showTooltip : noop;
    datas.forEach(function(d, i) {
        //span to div: legend appears to all element (color-sample and text-node)
        var title = document.createElement('div');
        title.className = 'title col-md-6';
        title.id = "itemLegenda"+i;
        parent.appendChild(title);

        var colorSample = document.createElement('div');
        colorSample.className = 'color-sample';
        colorSample.style.backgroundColor = d.hasOwnProperty('strokeColor') ? d.strokeColor : d.color;
        colorSample.style.borderColor = d.hasOwnProperty('fillColor') ? d.fillColor : d.color;
        title.appendChild(colorSample);

        var text = document.createTextNode(d.label);
        text.className = 'text-node';
        title.appendChild(text);

        var node = document.createElement("span");
        var id = "itemLegenda" + i;
        node.appendChild(text);
        document.getElementById(id).appendChild(node);
       

        show(chart, title, i);
    });
}

//add events to legend that show tool tips on chart
function showTooltip(chart, elem, indexChartSegment){
    var helpers = Chart.helpers;

    var segments = chart.segments;
    //Only chart with segments
    if(typeof segments != 'undefined'){
        helpers.addEvent(elem, 'mouseover', function(){
            var segment = segments[indexChartSegment];
            segment.save();
            segment.fillColor = segment.highlightColor;
            chart.showTooltip([segment]);
            segment.restore();
        });

        helpers.addEvent(elem, 'mouseout', function(){
            chart.draw();
        });
    }
}

function noop() {}
