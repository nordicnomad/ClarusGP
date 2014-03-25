//Blog specific javascript only. Included on the blog detail pages as well. 
// TEST BOTH FOR ERRORS WHEN MAKING ADDITIONS / CHANGES!

$(document).ready(function() {
    var controller = $.superscrollorama({
        triggerAtCenter: false,
        playoutAnimations: true
    });
    //for the cool bifolding banner thing
    controller.addTween(
        '#blogFold',
        (new TimelineLite())
            .append([
                TweenMax.fromTo($('#banner'), 1, 
                    {css:{top: 0}, immediateRender:true}, 
                    {css:{top: 400}}),
                TweenMax.fromTo($('#tagline'), 1, 
                    {css:{top: 0}, immediateRender:true}, 
                    {css:{top: 200}})
                ]),
            1000 // scroll duration of tween
    );
});


