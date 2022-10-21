$(document).ready(function() {
  var margin_left = 0;
  $('#prev').on('click', function(e) {
    e.preventDefault();
    if (margin_left != 0) {
      margin_left = margin_left + 190;
      $('ul#csx-chips-menu-slider').animate({
        'margin-left': margin_left
      }, 300);
    }
  });

  $('#next').on('click', function(e) {
    e.preventDefault();
    margin_left = margin_left - 190;
    $('ul#csx-chips-menu-slider').animate({
      'margin-left': margin_left
    }, 300);
  });
});