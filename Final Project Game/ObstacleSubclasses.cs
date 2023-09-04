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
    //Child classes derived from obstacles class - each class is specific to each type of obstacle
    //Class for ground spikes
    public class groundSpikes : Obstacles
    {
        //Constructor initializes all needed variables
        public groundSpikes(int x)
        {
            this.x = x;
            this.xPosHit = x + 24;
            this.yPosDraw = 316;
            this.yPosHit = 328;
            this.drawbox.Name = "groundSpikeDrawbox";
            this.drawbox.Size = new Size(122, 87);
            this.drawbox.Image = Final_Project_Game.Properties.Resources.Ground_Spikes;
            this.drawbox.BackColor = Color.Transparent;
            this.hitbox.Name = "groundSpikeHitbox";
            this.hitbox.Size = new Size(65, 39);
            this.hitbox.BackColor = Color.Transparent;
            this.drawbox.Visible = false;
            //Turn on hitbox visibility - comment out to hide
            //this.hitbox.BackColor = Color.Blue;
        }
    }

    //Class for saws
    public class saws : Obstacles
    {
        //Constructor initializes all needed variables
        public saws(int x)
        {
            this.x = x;
            this.xPosHit = x + 11;
            this.yPosDraw = 172;
            this.yPosHit = 172;
            this.drawbox.Name = "SawsDrawbox";
            this.drawbox.Size = new Size(76, 207);
            this.drawbox.Image = Final_Project_Game.Properties.Resources.Saws;
            this.drawbox.BackColor = Color.Transparent;
            this.hitbox.Name = "SawsHitbox";
            this.hitbox.Size = new Size(50, 195);
            this.hitbox.BackColor = Color.Transparent;
            this.drawbox.Visible = false;
            //Turn on hitbox visibility - comment out to hide
            //this.hitbox.BackColor = Color.Blue;
        }
    }

    //Class for hanging trap
    public class hangingTrap : Obstacles
    {
        //Constructor initializes all needed variables
        public hangingTrap(int x)
        {
            this.x = x;
            this.xPosHit = x;
            this.yPosDraw = -4;
            this.yPosHit = 181;
            this.drawbox.Name = "HangingTrapDrawbox";
            this.drawbox.Size = new Size(80, 321);
            this.drawbox.Image = Final_Project_Game.Properties.Resources.Hanging_Trap;
            this.drawbox.BackColor = Color.Transparent;
            this.hitbox.Name = "HangingTrapHitbox";
            this.hitbox.Size = new Size(70, 136);
            this.hitbox.BackColor = Color.Transparent;
            this.drawbox.Visible = false;
            //Turn on hitbox visibility - comment out to hide
            //this.hitbox.BackColor = Color.Blue;
        }
    }

    //Class for Spike tower (slide version)
    public class spikeTowerSlide : Obstacles
    {
        //Constructor initializes all needed variables
        public spikeTowerSlide(int x)
        {
            this.x = x;
            this.xPosHit = x;
            this.yPosDraw = -2;
            this.yPosHit = -2;
            this.drawbox.Name = "spikeTowerSlidepDrawbox";
            this.drawbox.Size = new Size(91, 203);
            this.drawbox.Image = Final_Project_Game.Properties.Resources.SpikeTower_Slide;
            this.drawbox.BackColor = Color.Transparent;
            this.hitbox.Name = "spikeTowerSlideHitbox";
            this.hitbox.Size = new Size(78, 203);
            this.hitbox.BackColor = Color.Transparent;
            this.drawbox.Visible = false;
            //Turn on hitbox visibility - comment out to hide
            //this.hitbox.BackColor = Color.Blue;
        }
    }

    //Class for Spike tower (jump version)
    public class spikeTowerJump : Obstacles
    {
        //Constructor initializes all needed variables
        public spikeTowerJump(int x)
        {
            this.x = x;
            this.xPosHit = x + 5;
            this.yPosDraw = 162;
            this.yPosHit = 162;
            this.drawbox.Name = "spikeTowerJumpDrawbox";
            this.drawbox.Size = new Size(96, 204);
            this.drawbox.Image = Final_Project_Game.Properties.Resources.SpikeTowerJump;
            this.drawbox.BackColor = Color.Transparent;
            this.hitbox.Name = "spikeTowerJumpHitbox";
            this.hitbox.Size = new Size(62, 203);
            this.hitbox.BackColor = Color.Transparent;
            this.drawbox.Visible = false;
            //Turn on hitbox visibility - comment out to hide
            //this.hitbox.BackColor = Color.Blue;
        }
    }

    //Class for falling trap
    public class fallingTrap : Obstacles
    {
        //Constructor initializes all needed variables
        public fallingTrap(int x)
        {
            this.x = x;
            this.xPosHit = x + 21;
            this.yPosDraw = -280;
            this.yPosHit = -280;
            this.drawbox.Name = "FallingTrapDrawbox";
            this.drawbox.Size = new Size(99, 100);
            this.drawbox.Image = Final_Project_Game.Properties.Resources.Falling_Trap;
            this.drawbox.BackColor = Color.Transparent;
            //Tag given to this trap's drawbox so the moveObstacle method in the super class knows to move it down as well
            this.drawbox.Tag = "fall";
            this.hitbox.Name = "FallingTrapHitbox";
            this.hitbox.Size = new Size(64, 100);
            this.hitbox.BackColor = Color.Transparent;
            this.drawbox.Visible = false;
            //Turn on hitbox visibility - comment out to hide
            //this.hitbox.BackColor = Color.Blue;
        }
    }
}
