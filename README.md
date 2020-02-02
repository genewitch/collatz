# collatz FAST by genewitch

	Now in python3! Not sure if this is 64 bit capable. It produces the **exact same** 
	output as the C# version, so it is usable as a benchmark to compare against
	the windows version on other OSes.


Original readme:
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

##addendum readme:
Requires emil.GMP and libgmp.

You can pass a number on the command line (like from http://boinc.thesonntags.com/collatz/highest_steps.php)
or you can touch a file called lastnumber.txt and put a number in there.

output: step leaders go to mostcount.txt, last solved number is in lastnumber.txt, console displays some info every time the files are flushed.

## special  notes:
This software does not encounter the same "step count" difficulty as the BOINC software, so running through the highest stepcounts will not trigger on the same numbers listed on the above weblink. This software will correctly examine each number if started at the number 1, but since BOINC's numbers are crowdsourced and verified, you can start at the highest number listed and continue correctly. You can also test arbitrarily long numbers (up to about 20,000 digits or so verified in this release).

Please submit a pull request if you edit this code to support 64bits, or an issue if you know of a better "big number" library.
