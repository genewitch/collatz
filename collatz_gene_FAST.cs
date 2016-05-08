/********************************************************************************
/	Genewitch's collatz conjecture algorithm.				                    /
/       starting from n (where numbers before n have already passed)		    /
/       determine if the conjecture proves true for n.				            /
/       this algorithm short circuits if it encounters a known value	    	/
/	while computing. IE with n = 6 it stops on step 2, which		            /
/	is 3. (6, 3, 10, 5, 16, 8, 4, 2, 1) - because we KNOW that		            /
/	3 passes. It also skips all even numbers before the loop.								                                    /
/										                                        /
/	While this doesn't allow for epeen measurement, it does 		            /
/	immensely speed up the rate of computation for large numbers	        	/
/	- in the 60 billion range the max numbers of steps i saw was	        	/
/	454, less than half of the wikipedia article's claims. for the 	        	/
/	BOINC numbers, 1900 less than their stated steps.		                	/		
/	special thanks to randall of xkcd fame for inspiring me		            	/
/	to finally act like a comic character.					                    /
********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emil.GMP;
using System.IO;
using System.Diagnostics;
using System.Threading;


namespace collatz_gene
{
    class Program
    {
        public static void Main(string[] args)
        {
            BigInt working = new BigInt("1");         //operations performed on this number
            BigInt lastvalue = new BigInt("1");       //shortcircuit and debugging var

            ulong runningtally = 0;    //more statistical info
            //decimal runningtally = 0;    //more statistical info
            ulong count = 1;                         //debugging, statstical pump
            ulong highestcount = 1;                  //for display purposes only. do not eat.
            //bool shorted = false;                     //Did we short on this number?
            //string reason = "";                       //why'd we short: reasoning.
            StreamWriter f;
            StreamReader r = new StreamReader("lastnumber.txt"); //if this file doesn't exist, please make it, insert highest BOINC collatz number or whatever.
            string s = r.ReadLine();  //TODO loop read();
            BigInt currentvalue = new BigInt();
            //Launcher la = new Launcher();
            if (args.Length != 0)
            {
                if (BigInt.Compare(new BigInt(args[0]),new BigInt(s)) < 1)
                {
                    Console.WriteLine("warning, you've entered a number less than the lastnumber.txt file.");
                    Console.WriteLine("this will overwrite your saved progress. Continue?");
                    if (Console.ReadKey().Key.ToString().ToLower().Equals("y"))
                        currentvalue = new BigInt(args[0]);
                    else
                        return;
                }
                else
                {
                    Console.WriteLine("warning, you've entered a number higher than the lastnumber.txt file.");
                    Console.WriteLine("this will overwrite your saved progress. Continue?");
                    if (Console.ReadKey().Key.ToString().ToLower().Equals("y"))
                        currentvalue = new BigInt(args[0]);
                    else
                        return;
                }
            }
            else            
            currentvalue = new BigInt(s);    //working is set to this before while; grabbed from BOINC's result page as current "step leader"
            
            r.Close();
            if (currentvalue.IsEven)
                currentvalue += 1;
            Stopwatch timer = new Stopwatch();
            timer.Start();

            //TODO: see if the files can be read by multiple instances of this program. instant multithreading.
            //each instance should see if the others have gotten a larger number.

            while (true)
            {
                /*Thread firstThread = new Thread(new ThreadStart(la.compute));
                Thread secondThread = new Thread(new ThreadStart(la.compute));
                Thread thirdThread = new Thread(new ThreadStart(la.compute));


                firstThread.Start();
                secondThread.Start();
                thirdThread.Start();
               */
			   

                if ((runningtally % 500001) <= 0)
                {
                    //TODO: keep track of starting number, and do numbers/sec as well as track how many numbers it's done.
                    Console.Write(timer.ElapsedMilliseconds);
                    long ps = ((long)runningtally / (long)(timer.ElapsedMilliseconds+1));
                    Console.WriteLine("ms    steps/ms: " + ps + "    total steps: " + runningtally + ", last number's stepcount:" + count);
                    f = new StreamWriter("lastnumber.txt");
                    f.WriteLine(currentvalue);
                    f.Flush();
                    f.Close();

                }
                else if (count > highestcount)
                {
                    highestcount = count;
                    Console.WriteLine();
                    Console.WriteLine(DateTime.Now + " New record: " + highestcount + ", on: " + currentvalue + ".");
					f = new StreamWriter("moststeps.txt");
                    f.WriteLine(DateTime.Now + " New record: " + highestcount + ", on: " + currentvalue + ".");
                    f.Flush();
                    f.Close();
                    //Console.Clear();
                }

                // this section reserved for logging, statiscal collection and web page integration.
                /*if (shorted)
                    Console.WriteLine(currentvalue + " shorted: " + reason +  "; proceeding.");
                else
                    Console.Write("Number: " + currentvalue + ", steps: " + count);

                Console.WriteLine();
                       */

                lastvalue = currentvalue;  //reset all the vars for next iteration
                currentvalue += 2;
                //working = currentvalue;
                //shorted = false;
                count = 1;


                working = currentvalue;
                while (working > 1)
                {
                    if (working <= lastvalue)        //short, we've already confirmed lower number.
                    {
                        //shorted = true;
                        //reason = "Reached number < lastvalue ";
                        break;
                    }
                    else if (working.IsEven)  // Don't short (this was bug #0010
                    {
                        working = working / 2;
                    }
                    else                                                //odd, 3n+1
                    {
                        working = working * 3;                          //3n+1 will always be at least 4
                        working = working + 1;                          //so we can divide by 2, and get 2 as the smallest number
                        working = working / 2;                          // in practice this will never happen, bceause of the top
                    }                                                   // compare in the loop.
                    //Console.Clear();

                    count += 1;
                    runningtally += 1;

                }
                

            }
           /* Console.WriteLine();
            Console.WriteLine("highest count: " + highestcount);
            Console.ReadKey(); */
        }
        

    }
}
