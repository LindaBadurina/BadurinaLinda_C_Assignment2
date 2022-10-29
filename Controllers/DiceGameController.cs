using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Schema;

namespace Assignment2.Controllers
{
    [Route("api/J2/[controller]")]
    [ApiController]
    public class DiceGame : ControllerBase
    {
        [Route("{m}/{n}")]
        /*
        Ints m and n match the variables given in the assignment description. nCounter is a counter I use to iterate backward through n while I
        iterate fordward through m to find all possible combinations of rolls that add to 10. I do that using two nested loops.
        The outer loop counts up to m from 1 (using i, a conventional name for loop counters), and 
        the inner loop counts down from n to 0 (using j, another conventional loop counter name). When there is a number such that the i + j = 10,
        I add 1 to the running total, which is held by a variable named total. I count down from n using nCounter instead of n. The reason why, is so that
        every time I find a number j that adds to the current i, (the number the outer loop is currently pointed to) I set nCounter to j. This makes it so that
        the next time the inner loop is entered, I don't have to count down through numbers that will be too high to add to the next i in order to get to 10.
        Once the outer loop has been exited, I have my number! I output a different string for when there is only 1 total way to reach 10 than I do for when there 
        are more or fewer ways to reach 10 than 1, (since nobody says, "There are 1 total ways...") and my program is complete.  
         */
        public String Get(int m, int n) 
        {
            //Debugging described inside the loop
            //System.Diagnostics.Debug.WriteLine("It did get here");

            /*Checking if either die is set to an invalid number, 
             * since the assignment specification describes both dice as having sides of natural numbers greater-than-or-equal-to 1*/
            if (m < 1 || n < 1) 
            {
                //System.Diagnostics.Debug.WriteLine("m = " + m + ", n = " + n);
                return "There are 0 total ways to get the sum 10";
            }

            /*Checking a special case so that the program doesn't have to enter the loop
            (if one of the dice has 1 side (what does that even mean? lol) and the other has 9 or more sides)*/
            if (((m == 1) && (n >= 9)) || ((n == 1) && (m >= 9)))
            {
                return "There is 1 total way to get the sum 10";
            }
            int total = 0;
            int nCounter = n;
            for (int i = 1; i < m + 1; i++) 
            {
                for (int j = nCounter; j > 0; j--) 
                {
                    //I had to debug, because at first, the outer loop wouldn't iterate all the way to the value of m. So if m were 5 and n were 5, I would have a result of 0,
                    //because the outer loop would only go up to 4.
                    //System.Diagnostics.Debug.WriteLine("i = " + i + ", j = " + j);
                    if ((i + j) == 10) 
                    {

                        //System.Diagnostics.Debug.WriteLine("i = " + i + ", j = " + j);
                        /* Setting nCounter to the current value of j so that we don't have to loop
                        all the way back through the rest of n the next time we get to this inner loop */
                        total += 1;
                        nCounter = j - 1;
                        break;
                    }
                }
            }

            //Originally, I thought I had to make two loops and then delete the extra time double 5s were counted if both dice were greater-than-or-equal-to 5.
            //Then the program started counting everything twice because I had two loops! So I commented it all out. 
            /*
            for (int k = 1; k < n + 1; k++) 
            {
                for (int q = mCounter; q > 0; q--) 
                {
                    if ((k + q) == 10) 
                    {
                        System.Diagnostics.Debug.WriteLine("k = " + k + ", q = " + q);
                        total += 1;
                    }
                }
            }*/

            /*if ((m >= 5) && (n >= 5))
            {
                total -= 1;
            }*/

            if (total == 1) 
            {
                return "There is 1 total way to get the sum 10";
            }
            return "There are " + total + " total ways to get the sum 10";
        }
    }
}
