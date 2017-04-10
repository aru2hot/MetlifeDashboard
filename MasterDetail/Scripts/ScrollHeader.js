function FreezeGrid(GridId, isChildGrid, ManualScrollHeight) {
    var ScrollHeight = ManualScrollHeight;
    var grid = $('[id$=' + GridId + ']');

    var gridWidth = grid[grid.length - 1].offsetWidth;
    var gridHeight = grid[grid.length - 1].offsetHeight;
    var headerCellWidths = new Array();
    var headerRow = $(grid).find('tr:first');
    $('th', $(headerRow)).each(function (i) {
        headerCellWidths[i] = $(this)[0].offsetWidth;

    });
    $(grid).parent().append("<div></div>");
    var parentDiv = grid[grid.length - 1].parentNode;

    var table = document.createElement("table");
    for (i = 0; i < grid[grid.length - 1].attributes.length; i++) {
        if (grid[grid.length - 1].attributes[i].specified && grid[grid.length - 1].attributes[i].name != "id") {
            table.setAttribute(grid[grid.length - 1].attributes[i].name, grid[grid.length - 1].attributes[i].value);
        }
    }
    table.style.cssText = grid[grid.length - 1].style.cssText;
    table.style.width = gridWidth + "px";
    table.appendChild(document.createElement("tbody"));
    table.getElementsByTagName("tbody")[0].appendChild(grid[grid.length - 1].getElementsByTagName("TR")[0]);
    var cells = table.getElementsByTagName("TH");

    var gridRow = grid[grid.length - 1].getElementsByTagName("TR")[0];
    for (var i = 0; i < cells.length; i++) {
        var width;
        if (headerCellWidths[i] > gridRow.getElementsByTagName("TD")[i].offsetWidth) {
            width = headerCellWidths[i];
        }
        else {
            width = gridRow.getElementsByTagName("TD")[i].offsetWidth;
        }
        cells[i].style.width = parseInt(width) + "px";
        gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width) + "px";
    }
    parentDiv.removeChild(grid[grid.length - 1]);

    var dummyHeader = document.createElement("div");
    dummyHeader.appendChild(table);
    parentDiv.appendChild(dummyHeader);
    var scrollableDiv = document.createElement("div");
    if (parseInt(gridHeight) > ScrollHeight) {
        gridWidth = parseInt(gridWidth) + 17;
    }
    
    scrollableDiv.style.cssText = "overflow:auto;max-height:" + ScrollHeight + "px;width:" + gridWidth + "px";
    scrollableDiv.appendChild(grid[grid.length - 1]);
    parentDiv.appendChild(scrollableDiv);
}