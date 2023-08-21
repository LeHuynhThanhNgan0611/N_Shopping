/*
Copyright (c) 2003-2014, CKSource - Frederico Knabben. All rights reserved.
For licensing, see license.txt or http://cksource.com/ckfinder/license
*/

CKFinder.customConfig = function( config )
{

	// http://docs.cksource.com/ckfinder_2.x_api/symbols/CKFinder.config.html

	// Sample configuration options:
	config.uiColor = '#BDE31E';

    config.language = 'vi';
	config.removePlugins = 'help';

    config.filebrowserBrowseUrl = "/ckfinder/ckfinder.html";
    config.filebrowserImageBrowseUrl = "/ckfinder/ckfinder.html?type=Images";
    config.filebrowserFlashBrowseUrl = "/ckfinder/ckfinder.html?type=Flash";
    config.filebrowserUploadUrl = "/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files";
    config.filebrowserImageUploadUrl = "/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images";
    config.filebrowserFlashUploadUrl = "/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash";

};
