// Header scroll class
$(window).scroll(function () {
	if ($(this).scrollTop() > 100) {
		$('#header').addClass('header-scrolled');
	} else {
		$('#header').removeClass('header-scrolled');
	}
});


$(function () {
	//二维码显示隐藏
	var timer = null;
	$(".wechat-hide").each(function() {
		$(this).mouseover(function(){
			var me = $(this);
	        clearInterval(timer);
	        timer = setInterval(function(args) {
	           me.children('.wechat-item').show();
	        },50);
	    }); 
	    $(this).mouseout(function(){
	    	var me = $(this);
	        clearInterval(timer);
	        timer = setInterval(function(args) {
	            me.children('.wechat-item').hide();
	        },50);
	    }); 
	});
	function showTooltip(){
		var flag =window.localStorage.getItem('isAccept');
		if(flag=='true'){
			$('.ad-tooltip').hide();
		}else{
			$('.ad-tooltip').show();
		}
	}
	showTooltip();
	$(".ad-tooltip-accept").click(function () {  
		window.localStorage.setItem('isAccept','true');
		$('.ad-tooltip').fadeOut();
	})
	$(".ad-tooltip-close").click(function(){
		$(this).parent().hide();
		window.localStorage.setItem('isAccept','false');
	});



	/*
	Timeline - Load More
	*/
	var timelineLoadMore = {

		pages: 0,
		currentPage: 1,
		$wrapper: $('#timelineLoadMoreWrapper'),
		$btn: $('#timelineLoadMore'),
		$loader: $('#timelineLoadMoreLoader'),

		build: function() {

			var self = this

			self.pages = self.$wrapper.data('total-pages');

			if(self.pages <= 1) {

				self.$btn.remove();
				return;

			} else {

				self.$btn.on('click', function() {
					self.loadMore();
				});

				// Infinite Scroll
				if(self.$btn.hasClass('btn-portfolio-infinite-scroll')) {
					self.$btn.appear(function() {
						self.$btn.trigger('click');
					}, {
						data: undefined,
						one: false,
						accX: 0,
						accY: 0
					});
				}

			}

		},
		loadMore: function() {

			var self = this;

			// Set Height For Loader
			self.$loader.css({
				height: self.$btn.outerHeight(true)
			});

			self.$btn.hide();
			self.$loader.show();

			// Ajax
			$.ajax({
				url: self.$wrapper.data('ajax-path') + (parseInt(self.currentPage)+1),
				complete: function(data) {

					var $items = $(data.responseText);

					setTimeout(function() {

						self.$wrapper.append($items)
						self.currentPage++;

						if(self.currentPage < self.pages) {
							self.$btn.show().blur();
						} else {
							self.$btn.remove();
						}

						// Carousel
						$(function() {
							$('[data-plugin-carousel]:not(.manual), .owl-carousel:not(.manual)').each(function() {
								var $this = $(this),
									opts;

								var pluginOptions = theme.fn.getOptions($this.data('plugin-options'));
								if (pluginOptions)
									opts = pluginOptions;

								$this.themePluginCarousel(opts);
							});
						});

						// Hover 3d
						$(function() {
							if ($.isFunction($.fn['hover3d'])) {

								$(function() {
									$('.hover-effect-3d').each(function() {
										var $this = $(this);

										$this.hover3d({
											selector: ".image-frame"
										});
									});
								});

							}
						});

						// Timeline Filter
						if( $('#timelineFilter').get(0) ) {
							var category = $('#timelineFilter > li > a.active').data('filter-category'),
								timelineBox = $('#timelineFilterContent').find('.timeline-box');

							if( category == '.all' ) {
								category = timelineBox;
							}

							timelineWithFilter.organizeBoxes( category, timelineBox );
						}

						self.$loader.hide();

					}, 1000);

				}
			});

		}

	}


	if($('#timelineLoadMoreWrapper').get(0)) {
		timelineLoadMore.build();
	}

	//移入变色
	$(".function-image").mouseover(function(event) {
		$(this).css('filter','grayscale(0)');
	});
	$(".function-image").mouseout(function(event) {
		$(this).css('filter','grayscale(1)');
	});


}); 