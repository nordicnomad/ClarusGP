/* Mobile nav closes on click */
$(document).ready(function() {
	$('.nav').click( function() {
        $('#dropyDowny').addClass('collapse');
        $('.nav-collapse').removeClass('in').css('height', '0');
    });
});

/*horizontal parallax slider */

$(document).ready(function() {

    $('#da-slider').cslider();
    
});
    


/* vertical accordion function */
$(document).ready(function() {
	$('#va-accordion').vaccordion({
		expandedHeight	: 550,
		animSpeed		: 500,
		animEasing		: 'jswing',
		animOpacity		: 0.4
	});
});


/* slit slider anonymous function */
$(document).ready(function() {

	var Page = (function() {

		var $nav = $( '#nav-dots > span' ),
			slitslider = $( '#slider' ).slitslider( {
				onBeforeChange : function( slide, pos ) {

					$nav.removeClass( 'nav-dot-current' );
					$nav.eq( pos ).addClass( 'nav-dot-current' );

				}
			} ),

			init = function() {

				initEvents();
			},
			initEvents = function() {
				$nav.each( function( i ) {
					$( this ).on( 'click', function( event ) {
						var $dot = $( this );
						if( !slitslider.isActive() ) {
							$nav.removeClass( 'nav-dot-current' );
							$dot.addClass( 'nav-dot-current' );
						}
						
						slitslider.jump( i + 1 );
						return false;
					});
				});
			};
			return { init : init };
	})();

	Page.init();

	/**
	 * Notes: 
	 * 
	 * example how to add items:
	 */

	/*
	
	var $items  = $('<div class="sl-slide sl-slide-color-2" data-orientation="horizontal" data-slice1-rotation="-5" data-slice2-rotation="10" data-slice1-scale="2" data-slice2-scale="1"><div class="sl-slide-inner bg-1"><div class="sl-deco" data-icon="t"></div><h2>some text</h2><blockquote><p>bla bla</p><cite>Margi Clarke</cite></blockquote></div></div>');
	
	// call the plugin's add method
	ss.add($items);

	*/

});
/* End slit slider anonymous function */

/*sticky nav call*/
$(document).ready(function(){
	$('#navbar').waypoint('sticky');
});


/*consulting scroll effects*/

//$(document).ready(function() {
//	var controller = $.superscrollorama();
//	controller.addTween('#consultHead', TweenMax.from( $('#consultHead'), .5, {css:{opacity: 0}}));
//	controller.addTween('#consult1', TweenMax.from( $('#consult1'), .75, {css:{right:'2000px'}, ease:Quad.easeInOut}));
//	controller.addTween('#consult2', TweenMax.from( $('#consult2'), .75, {css:{right:'2000px'}, ease:Quad.easeInOut}));
//	controller.addTween('#consult3', TweenMax.from( $('#consult3'), .75, {css:{right:'2000px'}, ease:Quad.easeInOut}));
//});

/* Target, tween object(tween target, duration, additional effects, offset, reverse)*/


//Career Controls Reset Button
$(function () {
    //click event for the clear button
    $('#clear').click(function () {
        //clear all the controls, resubmit the form
        $('#search').val('');
        $('#Category').val('');
        $('#Location').val('');

        //submit the form
        document.forms[0].submit();
    });
});



