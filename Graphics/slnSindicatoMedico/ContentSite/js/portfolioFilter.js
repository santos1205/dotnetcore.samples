jQuery(function () {
	"use strict";
    
    /*global jQuery, $*/
	jQuery(window).load(function () {
		//Initialize filterizr with default options
        jQuery('.portfolio-items').filterizr({
			layout: 'packed'
		});
		
		// Filterizr nav
		jQuery('.portfolio-nav li').on('click', function() {
			jQuery(".portfolio-nav li").removeClass("active");
			jQuery(this).addClass("active");
		});
	});
	
}());