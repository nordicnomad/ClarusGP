$(document).ready(function(){
	$('#navtestButton').click(function(){
		if ($('navtestDropDown').hasClass('slidesIn') === false) {
			$('#navtestDropDown').addClass('slidesIn');
		}
		else {
			$('navtestDropDown').removeClass('slidesIn');
		}
	});
});