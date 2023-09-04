using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Final_Project_Game
{
    public partial class GameWindow : Form
    {
        //Objects and global variables used in the project
        //Initialize player object
        public Player p = new Player();

        //Initialize background object
        public Background bg = new Background();

        //Create random object for RNG
        public Random rnd = new Random();

        //List instantiated to store the obstacles in the game - will act as a queue
        public List<Obstacles> Obstacles;

        //Used to play the background music
        SoundPlayer musicPlayer = new SoundPlayer();

        //Variables will be used as a timer when obstacles can be generated - done so the obstacles are reasonably spaced out
        //Cooldown for obstacles to spawn is 20 ticks of the timer
        public int obstacleTimer = 20;
        //Counter used to determine if timer has been waited - will be incremented in the main loop
        public int obstacleCounter = 0;

        public GameWindow()
        {
            InitializeComponent();
            //Reduces flickering when updating screen
            this.DoubleBuffered = true;
        }

        //Called when screen loads
        private void GameWindow_Load(object sender, EventArgs e)
        {
            //Allows for transparent background for controls on the form
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            //Add the player's draw and hitbox to the form            
            this.Controls.Add(p.drawBox);
            //this.Controls.Add(p.hitbox);

            //Initialize the player object on the start of game
            p.initialization();

            //Initialize the list of obstacles - there will be a limit of 6 obstacles generated at a time
            Obstacles = new List<Obstacles>(4);

            //Get location of file for background music
            musicPlayer.SoundLocation = "..\\..\\Resources\\Sounds\\Game screen background music.wav";
            //Loads the music
            musicPlayer.Load();
            //Loops music
            musicPlayer.PlayLooping();
        }

        //Check keys pressed by user
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //User presses up arrow key to jump
            if (e.KeyCode == Keys.Up)
            {
                //Set limit to jump height
                //Make sure that player only continues jumping if they are holding the jump key
                if (p.jumpCount < 6 && !p.stopJump)
                {
                    p.jump = true;
                    //Player must not be on the ground anymore
                    p.onGround = false;
                }                
            }
            //User presses down arrow key to slide - can only slide when on the ground
            if (e.KeyCode == Keys.Down && p.onGround)
            {
                p.slide = true;
            }
            //Player can press escape key to pause game
            if (e.KeyCode == Keys.Escape && p.alive)
            {
                pause();
            }
        }

        //Check when user releases keys
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //Letting go of up key means player no longer jumps
            if (e.KeyCode == Keys.Up)
            {
                p.jump = false;
                //Prevent multiple jumping in air
                p.stopJump = true;
            }
            //Player no longer wants to slide
            if (e.KeyCode == Keys.Down)
            {
                p.slide = false;
            }
        }

        //When player clicks on pause button 
        private void PauseBtn_Click(object sender, EventArgs e)
        {
            if (p.alive)
            {
                pause();
            }            
        }

        //Main loop the game will run in - call functions to be done during game loop here
        private void GameLoop_Tick(object sender, EventArgs e)
        {
            //Runs main game functions if the player is alive
            if (p.alive)
            {
                //Scrolls background
                bg.scroll();
                //Draws the player's sprite
                p.draw();
                //Controls the player's movement
                p.playerMovement();
                //Checks collision detection of player
                p.collisionDetection();
                //Updates player's score
                p.scoreCount(PlayerScore, GameLoop);

                //Iterates through the obstacle list to perform functions the obstacle must do every loop
                //Objects read from the list are given the data type dynamic to allow for the subclasses' fields to be accesssed without specifying the type of obstacle since the obstacles are randomly generated
                foreach (Obstacles obstacle in Obstacles)
                {
                    //Moves the obstacle
                    obstacle.moveObstacle(obstacle.drawbox, obstacle.hitbox);

                    //Checks collision for the obstacle
                    if (obstacle.checkCollision(p.hitbox, obstacle.hitbox))
                    {
                        //Player hits the obstacle and their game is over - break used to make code more efficient so unessessary code doesn't need to run
                        p.alive = false;
                        break;
                    }

                    //Check if obstacle goes off screen
                    if (obstacle.drawbox.Location.X + obstacle.drawbox.Width < -5)
                    {
                        //Remove the obstacles since they are off screen
                        obstacle.removeObstacle(obstacle.drawbox, obstacle.hitbox, this);
                        //Set field to true so we know to remove the obstacle
                        obstacle.passed = true;
                    }
                }
                //Add new obstacle to game if the counter is at 0 and if the list is not full
                if (obstacleCounter == 0 && Obstacles.Count() < 4)
                {
                    //Call function to randomly generate an obstacle at a random x position off screen
                    generateObstacle(Obstacles);
                }
                //The timer has been waited and obstacle counter can be reset - set to negative one so when timer is incremented it is 0
                else if (obstacleCounter == obstacleTimer)
                {
                    obstacleCounter = -1;
                }
                //Increment timer
                obstacleCounter++;

                //Check if the farthest obstacle on the screen is off screen
                if (Obstacles[0].passed)
                {
                    //Remove the obstacle
                    Obstacles.RemoveAt(0);
                }

                overlayImages();
            }
            //Player is dead
            else
            {
                //Adds the current score to the file containing scores
                //Will sort and only store the top ten scores - this file will be read on the highscores screen
                highestScores(p.score);

                //Stops music
                musicPlayer.Stop();

                //********************************
                //Add code for some sort of transition exits for now
                //*************************************
                Application.Exit();
            }

            //Updates the screen
            Invalidate();
        }

        /***********************************************************************************************************************
         * Additional functions used outside of eventhandlers used on the form
         * *********************************************************************************************************************/

        //Function used to pause game
        private void pause()
        {
            //Disable main loop if it is currently running to pause game
            if (GameLoop.Enabled)
            {
                GameLoop.Enabled = false;
                //Pauses animation
                p.drawBox.Enabled = false;
                //Make sure the overlay is on top of all other graphics on screen
                PauseOverlay.BringToFront();
                //Display pause overlay
                PauseOverlay.Visible = true;
            }
            //Enable main loop if currently paused
            else
            {
                GameLoop.Enabled = true;
                //Resume animation
                p.drawBox.Enabled = true;
                //Hide pause overlay
                PauseOverlay.Visible = false;
            }
        }

        //Used to display background
        private void GameWindow_Paint(object sender, PaintEventArgs e)
        {
            //Draws the backgrounds to the form
            e.Graphics.DrawImage(bg.bg, bg.x1, -150);
            e.Graphics.DrawImage(bg.bg, bg.x2, -150);
        }

        //Function converts images of pictureboxes to bitmaps so they can be overlaid without "obliterating" each other
        private void overlayImages()
        {
            //Bitmap set as a transparent object
            //Rest of the images are drawn to this
            Bitmap parent = (Bitmap)Final_Project_Game.Properties.Resources.Transparent_background;
            //Create graphic object of the parentbox to draw other images to it
            Graphics g = Graphics.FromImage(parent);

            //Iterate through each picture box to add the images to the graphic
            foreach (dynamic x in this.Controls)
            {
                //Check if the control is a picturebox with images to be displayed
                if (x.Name != "PauseBtn" && x.Name != "ParentBox" && x.Name != "ScoreDisplay" && x.Name != "PauseOverlay")
                {                    
                    //Adds the image to the graphics at the correct location
                    g.DrawImage((Bitmap)x.Image, x.Location.X, x.Location.Y, x.Width, x.Height);
                }                
            }
            //Gets rid of graphic object (saves memory)
            g.Dispose();
            //Set picturebox's image to the merged bitmaps
            ParentBox.Image = parent;              
        }

        //Function randomly generates the next obstacle in the game
        //Takes the list of obstacles as a parameter
        //Randomly generates an obstacle and x position for that obstacle and adds it to the obstacles list
        private void generateObstacle(List<Obstacles> obstacles)
        {
            //Chooses a random obstacle to create - each number corresponds to an obstacle
            int randObstacle = rnd.Next(0, 6);
            //Chooses a random start location off screen within an interval - this makes it so the obstacles don't come at the same interval everytime
            int randx = rnd.Next(710, 810);
            //Stores the obstacle to added
            Obstacles obstacle;

            //Switch statement used to create the object - once the object is created, it is appended to the obstacles list
            switch (randObstacle)
            {
                //Next obstacle is the ground spike
                case 0:
                    obstacle = new groundSpikes(randx);
                    break;

                //Next obstacle is the saw
                case 1:
                    obstacle = new saws(randx);
                    break;

                //Next obstacle is the hanging trap
                case 2:
                    obstacle = new hangingTrap(randx);
                    break;

                //Next obstacle is the spike tower (slide)
                case 3:
                    obstacle = new spikeTowerSlide(randx);
                    break;

                //Next obstacle is the spike tower (jump)
                case 4:
                    obstacle = new spikeTowerJump(randx);
                    break;

                //Next obstacle is the falling trap
                default:
                    obstacle = new fallingTrap(randx);
                    break;
            }

            //Initialize the new obstacle
            //Set the correct location for the obstacle
            obstacle.setLocation(obstacle.drawbox, obstacle.hitbox, obstacle.x, obstacle.xPosHit, obstacle.yPosDraw, obstacle.yPosHit);
            //Add the obstacle to the screen
            obstacle.draw(obstacle.drawbox, obstacle.hitbox, this);
            //Add the obstacle to the list
            obstacles.Add(obstacle);
        }

        // The function receives the user's new score.
        // The function will read from the Scores.txt file, and then add on the new score to the end.
        // The scores array will be sorted.
        // The top 10 will be shown on the user interface, and overwrite the previous contents of Scores.txt.
        private void highestScores(int num)
        {
            // Converts the entire array of scores from strings to integers.
            int[] scores = Array.ConvertAll(File.ReadAllLines("..\\..\\Scores.txt"), int.Parse);

            /* Outputs to the screen what the Scores.txt file looked like (not necessary for the game).
            textBox1.AppendText("Before:");

            for (int i = 0; i < scores.Length; i++)
            {
                textBox1.AppendText(Environment.NewLine + scores[i]);
            }
            */

            // Adds on the new score which the player just got onto the end of the scores array.
            scores = scores.Concat(new int[] { num }).ToArray();

            // Sends the scores array to QSortPartition, which returns the scores array sorted from least to greatest.
            scores = QSortPartition(scores, 0, scores.Length - 1);

            // Clear all of the contents of the Scores.txt file.
            File.WriteAllText(".\\Scores.txt", String.Empty);

            // Output to the screen that the following lines will show what the new contents of Scores.txt are (not necessary for the game).
            //textBox1.AppendText(Environment.NewLine + Environment.NewLine + "After:");

            for (int i = scores.Length - 1; i > 0; i--)
            {
                //Overwrite the file first
                if (i == scores.Length - 1)
                {
                    File.WriteAllText("..\\..\\Scores.txt", scores[i].ToString() + Environment.NewLine);
                }
                //Append to the newly overwritten file
                else
                {
                    // Convert each score to a string before appending it onto a new line on the Scores.txt file.
                    File.AppendAllText("..\\..\\Scores.txt", scores[i].ToString() + Environment.NewLine);
                }                
                // Output to the screen what the new file contents are (not necessary for the game).
                //textBox1.AppendText(Environment.NewLine + scores[i].ToString());
            }
        }

        //Quicksort using partitions
        //Takes unorganized array and sorts the values from least to greatest
        //low -> starting index
        //high -> ending index
        static private int[] QSortPartition(int[] arr, int low, int high)
        {
            //Still a valid range for partition
            if (low < high)
            {
                //pi is partitioning index
                //After partition function has run, arr[pi] is the correct index of pivot
                int pi = Partition(arr, low, high);

                //Call function recursively to sort the array
                //Sorting numbers before pi
                QSortPartition(arr, low, pi - 1);
                //Sorting numbers after pi
                QSortPartition(arr, pi + 1, high);
            }

            return arr;
        }

        //Organizes given array into partitions around pivot (numbers less than pivot to the left of pivot and numbers greater than pivot to the right of pivot)
        //Low -> lowest index in partition being sorted
        //High -> highest index in partition being sorted
        //arr -> array to be sorted
        //Returns index of the pivot
        static private int Partition(int[] arr, int low, int high)
        {
            //Pivot is the last value in array
            int pivot = arr[high];

            //Index of the smaller element should be shifted to if lower than the pivot
            int i = low - 1;

            //Used to temporarily store values when swap occurs
            int temp;

            //Loop through partition to sort
            for (int j = low; j < high; j++)
            {
                //Current element is smaller than the pivot
                if (arr[j] < pivot)
                {
                    //Increment index of smaller element
                    i++;
                    //Swap current index with index of smaller element
                    temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }
            //Place pivot at correct index - the index after the numbers less than the pivot (i+1)
            //Value at i+1 swapped with the last index
            temp = arr[i + 1];
            arr[i + 1] = arr[high];
            arr[high] = temp;

            return i + 1;
        }

        //Function used to play sound effects
        //Takes the file path for the sound file to be played as a parameter
        private void playSound(String pathName)
        {
            //Create sound player object
            SoundPlayer sp = new SoundPlayer();

            //Get file and load it into the sound player
            sp.SoundLocation = pathName;
            sp.Load();

            //Play sound
            sp.Play();
        }

    }
}
