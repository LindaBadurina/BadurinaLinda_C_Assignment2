using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers
{
    [Route("api/J3/[controller]")]
    [ApiController]
    public class CellPhoneMessagingController : ControllerBase
    {

        /*
         I chose the J3 problem from the same 2006 contest from which the other assignment questions were provided. 
        You can find it on page 6 of this pdf:

        https://cemc.math.uwaterloo.ca/contests/computing/past_ccc_contests/2006/stage1/juniorEn.pdf

        My program has 2 methods: Get() and Conversion(). Conversion() converts an input character into a value, 1, 2, 3, 4, or -1.
        The values correspond to how many key presses it would take to input each letter. -1 corresponds to an invalid character input. (if the input string contains a character that is not a lower-case English ASCII letter)
        Get(), of course, does the rest of the work required in the output specification: given an input of lower-case English letters, a single integer should be output that represents the amount of seconds it would take 
        Joe (the character in the problem description) to type it. According to Joe, each key press takes about 1 second, and if 1 number must be accessed twice to type two consecutive letters, (for instance, typing "ab")
        there is a 2-second pause after inputting the first letter before Joe can punch in the second letter. 

        I only coded this program to allow for lower-case English letters, since the problem output specification only seemed to include those characters, in particular. 
        Capital letters and any other ASCII characters included at any point during the input string will result in a total output of -1. 

        Finally, the problem description requires that the program read multiple lines of words (terminated with 'halt'), and compute the input time for each line of the paragraph.
        Of course, given that the input here is a single string in a URL, I'm not totally certain how to do that, since if I press shift+enter in a URL bar, it doesn't type a line break into the URL bar, but loads a new browser window.
        Since the input cannot look the exact same as that given in the problem description, I've modified my program to only a single line at a time, not terminated by "halt".
        To try and make up for that, it accepts spaces. (which count as a single button press on their own separate button, implied by the example image in the problem description, but not seemingly required in the output specifications)

        Examples:

        If 75 is input, -1 is output.
        If a is input, 1 is output.
        If cell is input, 13 is output. 
        If abba is input, 12 is output.
        If www is input, 7 is output.
        If zzz is input, 16 is output.
        If zZz is input, -1 is output.
        If zwxy is input, 16 is output.
        */
        [Route("{id}")]
        public int Get(String id) 
        {
            //Total is the running total that will be returned at the end of the method.
            int total = 0;
            //This loop iterates through the input string (titled id as part of perceived convention)
            for (int i = 0; i < id.Length; i++) 
            {
                //thisChar is the character the loop currently points to.
                char thisChar = id[i];
                //thisValue is thisChar converted to a value that corresponds to how many key presses it would take to produce that character on Joe's phone.
                int thisValue = Conversion(thisChar);
                //Outputting -1 if a character is invalid
                if (thisValue == -1) 
                {
                    total = -1;
                    break;
                }
                //The amount of keypresses required to input thisChar into Joe's phone is added to the running total keypresses required for the input string.
                total += thisValue;

                /*An if statement that checks to see if we've iterated to the final character of the input string. I've put three if statements inside it.
                I could have made them all one gigantic if statement, but that would have been much, much harder to read.*/
                if (i < id.Length - 1) 
                {
                    //nextChar is the next character in the string after the one held by thisChar.
                    char nextChar = id[i + 1];
                    //nextValue is the amount of key presses required to produce  the letter represented by nextChar
                    int nextValue = Conversion(nextChar);
                    /*This first if statement adds a 2-second pause after a key input if: thisChar is the first letter listed on a phone button, and nextChar
                     appears later on the same button, OR if nextChar is the same letter as thisChar, regardless of where
                    on a phone button thisChar resides (first, second, third, or fourth letter on the button, doesn't matter)
                     */
                    if ((thisValue == 1 && nextValue > 1) && (nextChar == thisChar + 1 || nextChar == thisChar + 2 || nextChar == thisChar + 3) || nextChar == thisChar)
                    {
                        //There are debugging statements in each of these if statements, because all of them had to be tested and modified. Chaining logical statements together can get very confusing.
                        //System.Diagnostics.Debug.WriteLine("thisValue = " + thisValue + " nextValue = " + nextValue);
                        total += 2;
                    }
                    /*
                    This second if statement adds a 2-second pause if thisChar is the 2nd or 3rd letter on a button, and nextChar is another letter on the same button of value 2 or greater
                    (the original idea for this if statement was to cover exclusively middle letters on each button, but the logic doesn't quite match up to that, and I'd rather just describe
                    the logic differently than rewrite it, if the program works properly)
                    */
                    //Had to do some serious debugging in the line below because the input "zwxy" yielded the wrong output
                    //System.Diagnostics.Debug.WriteLine("thisChar == " + thisChar + " nextChar == " + nextChar + " thisValue == " + thisValue + "nextValue == " + nextValue + " " + (thisValue == 2 && (nextValue == 3 || nextValue == 4) && (nextChar == thisChar + 1 || nextChar == thisChar + 2)));
                    if ((thisValue == 2 &&  (nextValue == 3 || nextValue == 4) && (nextChar == thisChar + 1 || nextChar == thisChar + 2)) || (thisValue == 3 && (nextValue == 2 || nextValue == 4) && (nextChar == thisChar + 1 || nextChar == thisChar - 1))) 
                    {
                       //System.Diagnostics.Debug.WriteLine("It's because of this");
                        total += 2;
                    }
                    /*
                     This final if statement adds the 2-second pause between letters if thisChar is the 2nd, 3rd, or 4th letter on a button, and nextChar is the first letter on the same button.
                     */
                    if ((thisValue == 2 && nextChar == thisChar - 1) || (thisValue == 3 && nextChar == thisChar - 2) || (thisValue == 4 && nextChar == thisChar - 3)) 
                    {
                        //System.Diagnostics.Debug.WriteLine("Actually, this");
                        total += 2;
                    }

                }

            }
            return total;
        }

        /*This method simply converts a character to a value that corresponds to how many button presses it would take to input the character on Joe's phone.
         I tried to use modular arithmetic to derive each letter's value, so that I didn't have to directly assign a value to each individual letter, but
        unfortunately, since two of the buttons have 4 letters each, that doesn't work after the first 17 characters. Furthermore, a mistake I'd made earlier was using
        (c - 100) mod 3 to determine a letter's value. I should have used c - 97 mod 3, because the ASCII value for 'b' is 98, and (98-100) mod 3 is 2, but for the rest of the
        middle letters before 'r', (c - 100) mod 3 = 1. I ended up just individually assigning b a value of 2 before I figured out the easier way to fix that mistake. 
         */
        public int Conversion(char c) 
        {
            //Int value is the final value to be returned
            int value = 0;
            //Individually assigning a value of 4 to z and s, the only 2 letters to show up in the 4th position on any button
            if(c == 115 || c == 122) 
            {
                value = 4;
            }
            //Giving a value of 1 to every 1st number on every button
            else if ((c < 114 && c > 96 && (c - 100) % 3 == 0) || c == 116 || c == 119 || c == 32)
            {
                value = 1;
            }
            //Giving a value of 2 to every 2nd letter on every button
            else if ((c < 114 && c > 96 && (c - 100) % 3 == 1) || c == 120 || c == 117 || c == 98)
            {
                value = 2;
            }
            //Giving a value of 3 to every other lower-case English letter
            else if (c > 96 && c < 123)
            {
                value = 3;
            }
            //Giving a value of -1 to every invalid character.
            else
            {
                value = -1;
            }


            return value;
        }
    }
}
