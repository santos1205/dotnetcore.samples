/*
	Author: MegaPixels
	Template: Mega | Responsive HTML Template
	Version: 1.0
*/
	
"use strict";



/*global jQuery, $*/
jQuery(document).ready(function(){
	
	// Bootstrap hover dropdown menu
	jQuery('ul.nav li.dropdown').hover(function() {
		jQuery(this).find('.dropdown-menu').stop(true, true).fadeIn(500);
	}, function() {
		jQuery(this).find('.dropdown-menu').stop(true, true).fadeOut(500);
	});
	
	// select2 - topbar
	function languageFlag (language) {
		if (!language.id) { return language.text; }
		var $language = $(
			'<span><img src="images/flags/' + language.element.value.toLowerCase() + '.png" class="img-flag" /> ' + language.text + '</span>'
		);
		return $language;
	};
	jQuery(".mega-multilang-option").select2({
		templateResult: languageFlag,
		templateSelection: languageFlag,
	});
	
	// header slider
	$('.header-slider').bxSlider({
		mode:'fade',
		pager: false,
		prevText: '',   
		nextText: '',
		onSliderLoad: function(currentIndex) {     
			$('.header-slider').children().eq(currentIndex + 0).addClass('active-slide');
		  },
		  onSlideAfter: function($slideElement){
			$('.header-slider').children().removeClass('active-slide');
			$slideElement.addClass('active-slide');
		}
	});
	
	// item slider
	jQuery('.item-slider').slick({
		dots: false,
		infinite: true,
		speed: 300,
		slidesToShow: 1,
		slidesToScroll: 1,
		responsive: [
			{
				breakpoint: 1024,
				settings: {
					slidesToShow: 1,
					slidesToScroll: 1,
					infinite: true,
					dots: false
				}
			},
			{
				breakpoint: 769,
				settings: {
					slidesToShow: 2,
					slidesToScroll: 1
				}
			},
			{
				breakpoint: 480,
				settings: {
					slidesToShow: 1,
					slidesToScroll: 1
			  }
			}
		]
	});
	
	//Product Cart counter
	jQuery('<div class="cart-counter-nav"><div class="cart-counter-button cart-counter-up"></div><div class="cart-counter-button cart-counter-down"></div></div>').insertAfter('.cart-counter input');
	jQuery('.cart-counter').each(function() {
	  var spinner = jQuery(this),
		input = spinner.find('input[type="number"]'),
		btnUp = spinner.find('.cart-counter-up'),
		btnDown = spinner.find('.cart-counter-down'),
		min = input.attr('min'),
		max = input.attr('max');

	  btnUp.click(function() {
		var oldValue = parseFloat(input.val());
		if (oldValue >= max) {
		  var newVal = oldValue;
		} else {
		  var newVal = oldValue + 1;
		}
		spinner.find("input").val(newVal);
		spinner.find("input").trigger("change");
	  });

	  btnDown.click(function() {
		var oldValue = parseFloat(input.val());
		if (oldValue <= min) {
		  var newVal = oldValue;
		} else {
		  var newVal = oldValue - 1;
		}
		spinner.find("input").val(newVal);
		spinner.find("input").trigger("change");
	  });

	});
	
	// animate skillbar
	jQuery('.skill-bar').each(function(){
		jQuery(this).appear(function() {
		  jQuery(this).find('.skill-bar-count').animate({
		  width:jQuery(this).attr('data-percent')
		  },2000);
		});
	});
	
	// Number Counter
	(function() {
	  var Core = {
		initialized : false,
		initialize : function() {
		  if (this.initialized)
			return;
		  this.initialized = true;
		  this.build();
		},
		build : function() {
		  this.animations();
		},
		animations : function() {
		  // Count To
		  $(".number-counters [data-to]").each(function() {
			var $this = $(this);
			$this.appear(function() {
			  $this.countTo({});
			}, {
			  accX : 0,
			  accY : -100
			});
		  });
		},
	  };
	  Core.initialize();
	})();
	 
});