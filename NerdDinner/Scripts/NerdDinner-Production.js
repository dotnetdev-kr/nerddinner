/// <reference path="jquery-1.3.2-vsdoc.js" />
/// <reference path="jquery-1.3.2.js" />
/// <reference path="jquery-1.3.2.js" />

/// TODO: This will be removed soon by the designer so the XHTML will be cleaner.

$(document).ready(function() {

	$("#main").addClass("clearfix");

	if(document.location.href.substr(document.location.href.length-1,1) === "/")
	{
	    $("#header").after('<div id="hm-masthead"></div>');
	    $("#searchBox").appendTo("#hm-masthead");
	    $("#hm-masthead").after('<div id="main-top"></div>');
	}
});
