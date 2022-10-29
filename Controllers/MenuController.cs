using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers
{
    [Route("api/J1/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        int total = 0;

        [Route("{burger}/{drink}/{side}/{dessert}")]
        /*
        This program is rather simple. The integers burger, drink, side, and dessert all correspond to their sections of the menu.
        The integer total is the running total of calories for the order.
        I have 4 separate cascades of if statements: one for each of the 4 menu sections. The if statements add the value of a given item to the total.
        If an invalid integer is input for one of the menu items, "Input for {menu section} invalid. Please choose a whole number from 1 to 4" is output.
        If a non-integer is input, an error is thrown. 

        Examples:

        If 4/4/4/4 is input, "Your total calorie count is 0" is output.
        If 1/2/3/4 is input, "Your total calorie count is 691" is output. 
        If 30/2/3/4 is input, "Input for burger invalid. Please choose a whole number from 1 to 4" is output. 
        If 30/30/3/4 is input, "Input for burger invalid. Please choose a whole number from 1 to 4" is output, because the program checks for burger inputs first.
        If 3/40/3/4 is input, "Input for drink invalid. Please choose a whole number from 1 to 4" is output. 
        */
        public String Get(int burger, int drink, int side, int dessert)
        {
            if (burger == 4)
            {
                total += 0;
            }
            else if (burger == 3)
            {
                total += 420;
            }
            else if (burger == 2)
            {
                total += 431;
            }
            else if (burger == 1)
            {
                total += 461;
            }
            else 
            {
                return "Input for burger invalid. Please choose a whole number from 1 to 4";
            }

            if (drink == 4)
            {
                total += 0;
            }
            else if (drink == 3)
            {
                total += 118;
            }
            else if (drink == 2)
            {
                total += 160;
            }
            else if (drink == 1)
            {
                total += 130;
            }
            else
            {
                return "Input for drink invalid. Please choose a whole number from 1 to 4";
            }

            if (side == 4)
            {
                total += 0;
            }
            else if (side == 3)
            {
                total += 70;
            }
            else if (side == 2)
            {
                total += 57;
            }
            else if (side == 1)
            {
                total += 100;
            }
            else
            {
                return "Input for side invalid. Please choose a whole number from 1 to 4";
            }

            if (dessert == 4)
            {
                total += 0;
            }
            else if (dessert == 3)
            {
                total += 75;
            }
            else if (dessert == 2)
            {
                total += 266;
            }
            else if (dessert == 1)
            {
                total += 167;
            }
            else
            {
                return "Input for dessert invalid. Please choose a whole number from 1 to 4";
            }

            return "Your total calorie count is " + total;
        }
    }
}
