using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Project_Game
{
    //Background Class
    public class Background
    {
        //The background image used to scroll
        public Image bg = Properties.Resources.Forest_Background;

        //Stores the position for the bg to be painted at (there are two backgrounds side by side which give the endless scrolling effect
        //One spans entire visible screen, other starts off screen to the right
        public int x1 = 0;
        public int x2 = 930;

        //Method scrolls the background
        public void scroll()
        {
            //Once 1st background is off screen cycle it back to the right
            if (x1 <= -930)
            {
                x1 = 930;
            }
            //Once 2nd background is off screen, cycle it back to the right
            else if (x2 <= -930)
            {
                x2 = 930;
            }
            //Constantly scrolls the background by moving the x coordinate of each background to the left
            x1 -= 15;
            x2 -= 15;
        }
    }
}
