# collatz FAST by genewitch
********************************************************************************
	Genewitch's collatz conjecture algorithm.				                    
		starting from n (where numbers before n have already passed)		    
		determine if the conjecture proves true for n.				            
		this algorithm short circuits if it encounters a known value	    	
		while computing. IE with n = 6 it stops on step 2, which		            
		is 3. (6, 3, 10, 5, 16, 8, 4, 2, 1) - because we KNOW that		            
		3 passes. It also skips all even numbers before the loop.								                                    
										                                        
	While this doesn't allow for epeen measurement, it does 		            
		immensely speed up the rate of computation for large numbers	        	
		- in the 60 billion range the max numbers of steps i saw was	        	
		454, less than half of the wikipedia article's claims. for the 	        	
		BOINC numbers, 1900 less than their stated steps.		                			
		special thanks to randall of xkcd fame for inspiring me		            	
		to finally act like a comic character.					                    
********************************************************************************

Requires emil.GMP and libgmp.

You can pass a number on the command line (like from http://boinc.thesonntags.com/collatz/highest_steps.php)
or you can touch a file called lastnumber.txt and put a number in there.

output: step leaders go to mostcount.txt, last solved number is in lastnumber.txt, console displays some info every time the files are flushed.
