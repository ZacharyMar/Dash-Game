using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Project_Game
{
    public class Player
    {
        //Attributes
        //Determines if jumping
        public bool jump = false;
        //Prevents player from jumping multiple times in the air by stopping their jump ability once they release jump
        public bool stopJump = false;
        //Determines if sliding
        public bool slide = false;
        //Determines if the player is on the ground
        public bool onGround = true;
        //Check if player is alive
        public bool alive = true;
        //Player's vertical velocity
        public int jumpvel = 9;
        //Used to limit jump height
        public int jumpCount = 0;
        //Score the player currently has
        public int score = 0;
        //Used to keep track of animation frames - initialized at 0 since it starts at the start of the array of frames
        public int aniFrame = 0;
        //Cooldown timer for when score can be given to player
        public int scoreCooldown = 5;
        //Used as a timer to keep track when to award player a score
        public int scoreTimer = 0;

        //Arrays store the frames of each animation
        Image[] runCycle;
        Image[] takeOff;
        Image[] somersault;
        Image[] falling;
        Image[] slideF;
        Image[] slideEnd;

        //Player's draw box
        public PictureBox drawBox = new PictureBox
        {
            //Attributes associated with the box
            Name = "playerDrawBox",
            Size = new Size(100, 74),
            Location = new Point(27, 290),
            Image = Final_Project_Game.Properties.Resources.Running_Cycle,
            //Tag used to keep track of the animation to display
            Tag = "running",
            BackColor = Color.Transparent,
            Visible = false,
        };

        //Player's hitbox
        public PictureBox hitbox = new PictureBox
        {
            Name = "playerHitbox",
            Size = new Size(43, 62),
            Location = new Point(60, 302),
            BackColor = Color.Transparent,
            Visible = false,
        };

        //Constructor
        public Player()
        {
            //Sets the value for each array of frames for animation
            this.runCycle = this.getFrames(Final_Project_Game.Properties.Resources.Running_Cycle);
            this.takeOff = this.getFrames(Final_Project_Game.Properties.Resources.JumpTakeoff);
            this.somersault = this.getFrames(Final_Project_Game.Properties.Resources.Somersault);
            this.falling = this.getFrames(Final_Project_Game.Properties.Resources.Falling);
            this.slideF = this.getFrames(Final_Project_Game.Properties.Resources.Slide);
            this.slideEnd = this.getFrames(Final_Project_Game.Properties.Resources.SlideEnd);
        }
        
        //Initilization function resets variables to correct starting values
        public void initialization()
        {
            //Reset attributes
            jump = false;
            slide = false;
            onGround = true;
            alive = true;
            jumpvel = 9;
            jumpCount = 0;
            score = 0;

            //Reset drawbox and hitbox for player
            drawBox.Location = new Point(27, 290); //Change location to wherever the player will initially start
            hitbox.Location = new Point(60, 302);
            hitbox.Size = new Size(43, 62);
            drawBox.Image = Final_Project_Game.Properties.Resources.Running_Cycle;
            drawBox.Tag = "running";
        }

        //Draws the correct sprite for the player
        public void draw()
        {
            //Drawing for takeoff with jump
            //3 frame animation
            if (jump)
            {
                //Last animation played was running so the frame counter will be reset
                if((String)drawBox.Tag == "running")
                {
                    aniFrame = 0;
                }

                //Displays the takeoff animation
                if (aniFrame < takeOff.Length && (String)drawBox.Tag != "jumpstill")
                {
                    //Displays correct frame
                    drawBox.Image = takeOff[aniFrame];
                    //Update tag of drawBox
                    drawBox.Tag = "jump";
                    //Increment counter
                    aniFrame++;
                    //takeoff animation is over so still jumping image should be displayed
                    if (aniFrame >= takeOff.Length)
                    {
                        //Reset counter
                        aniFrame = 0;
                        //Update tag
                        drawBox.Tag = "jumpstill";
                    }
                }
                //Hold final jump frame as still image since takeoff animation is over
                else
                {
                    drawBox.Image = Final_Project_Game.Properties.Resources.JumpStillFrame;
                }
            }
            //Somersault and landing animation
            //4 frame somersault animation
            //Landing animation loops
            else if (!jump && !onGround)
            {
                //Update if not displaying somersault
                if ((String)drawBox.Tag != "falling" && aniFrame < somersault.Length)
                {
                    //Update drawbox
                    drawBox.Image = somersault[aniFrame];
                    //Update tag
                    drawBox.Tag = "somersault";
                    //Increment animation frames
                    aniFrame++;

                    //Somersault is over and now falling animation should be looped
                    if (aniFrame >= somersault.Length)
                    {
                        //Reset frame counter
                        aniFrame = 0;
                        //Set tag to falling so somersault animation does not play
                        drawBox.Tag = "falling";
                    }
                }
                //Display falling animation
                else
                {
                    //Update drawbox
                    drawBox.Image = falling[aniFrame];
                    //Update tag
                    drawBox.Tag = "falling";
                    //Increment counter
                    aniFrame++;
                    //Continue to loop animation
                    if (aniFrame >= falling.Length)
                    {
                        aniFrame = 0;
                    }
                }
            }
            //Drawing sprite for player when sliding
            //Looping gif
            else if (slide)
            {
                //If previous animation was running, or any type of falling - frame counter will be reset
                if ((String)drawBox.Tag == "running" || (String)drawBox.Tag == "falling" || (String)drawBox.Tag == "somersault")
                {
                    aniFrame = 0;
                }

                // Update drawbox
                drawBox.Image = slideF[aniFrame];
                //Update tag to current animation
                drawBox.Tag = "slide";
                //Increment frame count
                aniFrame++;
                //Reset animation loop
                if (aniFrame >= slideF.Length)
                {
                    //Reset frame counter
                    aniFrame = 0;
                }
            }
            //Play animation when getting out of slide
            //2 frame animation
            else if (!slide && (String)drawBox.Tag != "running")
            {
                //Reset frame counter if previously sliding or falling (looping animations may cause other animations to start at wrong frame)
                if ((String)drawBox.Tag == "slide" || (String)drawBox.Tag == "falling" || (String)drawBox.Tag == "somersault")
                {
                    aniFrame = 0;
                }

                drawBox.Image = slideEnd[aniFrame];
                //Update tag
                drawBox.Tag = "slideEnd";
                //Increment frame counter
                aniFrame++;
                //Reset frame counter
                //Slide end animation is over and the player is back to running
                if (aniFrame >= slideEnd.Length)
                {
                    aniFrame = 0;
                    drawBox.Tag = "running";
                }
            }
            //Otherwise running
            //Only update if the current animation displayed is not the running cycle
            //Looping gif
            else
            {
                //Reset frame counter if previously falling
                if((String)drawBox.Tag == "falling")
                {
                    aniFrame = 0;
                }

                //Display run cycle
                drawBox.Image = runCycle[aniFrame];
                //Update tag
                drawBox.Tag = "running";
                //Increment frame counter
                aniFrame++;
                //Reset frame counter to continue looping running cycle
                if (aniFrame >= runCycle.Length)
                {
                    aniFrame = 0;
                }
            }
            //Fill in hitbox so it is visible - comment out to hide
            //hitbox.BackColor = Color.Red;
        }

        //Function correctly positions player and player's hitbox to action performed
        public void playerMovement()
        {
            //Player is jumping
            if (jump)
            {
                //Going up on jump
                if (jumpCount < 8)
                {
                    //Update location of the boxes
                    drawBox.Location = new Point(drawBox.Location.X, drawBox.Location.Y - (jumpvel * jumpvel));
                    hitbox.Location = new Point(hitbox.Location.X, hitbox.Location.Y - (jumpvel * jumpvel));

                    //Increment jump counter
                    jumpCount++;
                }
                //Player reaches max height of jump
                else
                {
                    jump = false;
                }

                //Increment jump velocity (gets weaker as player approaches max height of jump
                jumpvel--;
            }
            //Player is in the air
            else if (!onGround)
            {
                //Update location of the boxes
                drawBox.Location = new Point(drawBox.Location.X, drawBox.Location.Y + (jumpvel * jumpvel));
                hitbox.Location = new Point(hitbox.Location.X, hitbox.Location.Y + (jumpvel * jumpvel));

                //Increment jump velocity - start slow then get faster
                jumpvel++;
            }
            //Player is on the ground
            else if (onGround)
            {
                //Reset jumpCount so player can jump again
                jumpCount = 0;
                //Reset jump velocity
                jumpvel = 9;
                //Allow player to jump again
                stopJump = false;
            }

            //Player is sliding
            if (slide)
            {
                //Change hitbox of player when sliding
                hitbox.Size = new Size(52, 34);
                //Move hitbox down to ground
                hitbox.Location = new Point(hitbox.Location.X, hitbox.Location.Y + 28);
            }
            //Player is not sliding
            //Only reverts hitbox if the hitbox was not already the standing hitbox
            else if (!slide && hitbox.Height != 62)
            {
                //Readjust hitbox to correct height for standing hitbox
                hitbox.Location = new Point(hitbox.Location.X, hitbox.Location.Y - 28);
                hitbox.Size = new Size(43, 62);
            }
        }

        //Checks for when player collides with obstacles
        public void collisionDetection()
        {
            //Collision with ground - ground on the background is located at yPos = 364
            if (hitbox.Location.Y + hitbox.Height > 364)
            {
                //Player is now on the ground
                onGround = true;
                //Places boxes at the correct height
                hitbox.Top = 364 - hitbox.Height;
                drawBox.Top = 364 - drawBox.Height;
            }
        }

        //Used to increase score of player
        public void scoreCount(Label display, Timer timer)
        {
            //Increase score once cooldown is over
            if (scoreTimer == 0)
            {
                score++;

                //Every 100 score the game will speed up - this is done by increasing the frequency of the timer
                if (score % 50 == 0 && timer.Interval - 5 > 0)
                {
                    timer.Interval = timer.Interval - 5;
                }
            }
            //Reset timer once cooldown has been waited
            else if (scoreTimer == scoreCooldown)
            {
                scoreTimer = -1;
            }
            //Increment timer
            scoreTimer++;

            //Display score to screen
            display.Text = Convert.ToString(score);
        }

        //Used to get each frame of the animated gifs of the player
        Image[] getFrames(Image originalImg)
        {
            //Gets number of frames in the gif
            int numberOfFrames = originalImg.GetFrameCount(FrameDimension.Time);
            //Intializes array to store the frames
            Image[] frames = new Image[numberOfFrames];

            //Iterate through gif to add each frame to the array
            for (int i = 0; i < numberOfFrames; i++)
            {
                originalImg.SelectActiveFrame(FrameDimension.Time, i);
                frames[i] = ((Image)originalImg.Clone());
            }

            //Returns array of the frames of animation
            return frames;
        }
    }
}
