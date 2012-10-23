/*
 * jquery.flexislider.js v0.1 - jQuery script
 * Copyright (c) 2009 Barry Roodt (http://calisza.wordpress.com)
 *
 * Licensed under the New BSD license.
 *
 * This script slides a list of images from right to left across the window.
 * An example can be found at http://flexidev.co.za/projects/flexislider
 * Please check http://code.google.com/p/flexidev/downloads/ for the latest version
 *
 */

	var speed = 50;
	var pic, numImgs, arrLeft, i, totalWidth, n, myInterval; 

$(window).load(function(){
	pic = $("#slider").children("img");
	numImgs = pic.length;
	arrLeft = new Array(numImgs);
	
	for (i=0;i<numImgs;i++){
		
		totalWidth=0;
		for(n=0;n<i;n++){
			totalWidth += $(pic[n]).width();
		}
		
		arrLeft[i] = totalWidth;
		$(pic[i]).css("left",totalWidth);
	}
	
	myInterval = setInterval("flexiScroll()",speed);
	$('#imageloader').hide();
	$(pic).show();	
});

function flexiScroll(){

	for (i=0;i<numImgs;i++){
		arrLeft[i] -= 1;		

		if (arrLeft[i] == -($(pic[i]).width())){	
			totalWidth = 0;	
			for (n=0;n<numImgs;n++){
				if (n!=i){	
					totalWidth += $(pic[n]).width();
				}			
			}	
			arrLeft[i] =  totalWidth;	
		}					
		$(pic[i]).css("left",arrLeft[i]);
	}
}