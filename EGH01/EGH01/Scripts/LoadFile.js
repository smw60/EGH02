function LoadFile(file) {
    var ext = file.match(/[^.]+$/); // расширение файла, после точки
    if (ext == 'css') {
        var link = document.createElement("link");
        link.setAttribute("rel", "stylesheet");
        link.setAttribute("type", "text/css");
        link.setAttribute("href", file);
    }
    if (ext == 'js') {
        var link = document.createElement("script");
        link.setAttribute("type", "text/javascript");
        link.setAttribute("src", file);
    }
    document.getElementsByTagName("head")[0].appendChild(link)
}