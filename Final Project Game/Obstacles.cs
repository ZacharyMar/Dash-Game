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
    //Obstacle parent class
    public class Obstacles
    {
        //Fields
        //The x coordinate of the obstacle
        public int x { get; set; }
        //Stores whether the obstacle is off screen
        public bool passed = false;

        //Initialize variables needed in the subclasses
        //The superclass itself does not need these variables, but every instance of the subclasses do
        //y coordinates the obstacle is supposed to be
        public int yPosDraw = 0;
        public int yPosHit = 0;
        //X coordinate for hitbox
        public int xPosHit = 0;
        //Draw and hitboxes
        public PictureBox drawbox = new PictureBox();
        public PictureBox hitbox = new PictureBox();


        //Default constructor - calls when no constructor class was called specifically when instance of object is created
        public Obstacles()
        {
            //Placeholders for now
            x = 0;
        }

        //Instace constructor
        //Takes x and y coordinate the obstacle should be placed at as parameters and assigns these values object fields
        public Obstacles(int x)
        {
            this.x = x;
        }

        //Function sets the location of the draw box and hitbox of the obstacle
        //Takes the x and y coordinates for the draw and hitbox as parameters - these are used to create the initial location of the obstacle
        //Should be called on initialization of obstacle
        public void setLocation(PictureBox drawbox, PictureBox hitbox, int xDraw, int xHit, int yDraw, int yHit)
        {
            //Set initial location
            drawbox.Location = new Point(xDraw, yDraw);
            hitbox.Location = new Point(xHit, yHit);
        }

        //Collision detection for obstacle
        //Takes the player's hitbox and the obstacle's hitbox as parameters
        //Returns boolean of whether the player collided with the obstacle
        public bool checkCollision(PictureBox playerhit, PictureBox obstaclehit)
        {
            //Checks whether the bounds of the player hitbox collided with the bounds of the obstacle hitbox
            return obstaclehit.Bounds.IntersectsWith(playerhit.Bounds);
        }

        //Removes obstacle from form when it goes past the player - this should help save on processing
        public void removeObstacle(PictureBox drawbox, PictureBox hitbox, Form screen)
        {
            //Remove the hitbox and drawbox of the obstacle from the form
            screen.Controls.Remove(drawbox);
            //screen.Controls.Remove(hitbox);
        }

        //Adds the obstacle's draw and hitbox to the screen - only needs to be called once the object is created
        public void draw(PictureBox drawbox, PictureBox hitbox, Form screen)
        {
            //Adds the draw and hitbox to the form            
            screen.Controls.Add(drawbox);
            //screen.Controls.Add(hitbox);
        }

        //Moves the obstacle to the left - keep the value to the same rate that the background moves so it looks smoother
        //Take's the obstacle's draw and hit box as parameters
        public void moveObstacle(PictureBox drawbox, PictureBox hitbox)
        {
            //Update the location of the draw and hitbox
            //Check if it is the falling trap
            //Obstacle is the falling trap so it must be moved left and down
            if ((String)drawbox.Tag == "fall")
            {
                drawbox.Location = new Point(drawbox.Location.X - 15, drawbox.Location.Y + 10);
                hitbox.Location = new Point(hitbox.Location.X - 15, hitbox.Location.Y + 10);
            }
            //Not the falling trap so obstacle only needs to be moved to the left
            else
            {
                drawbox.Location = new Point(drawbox.Location.X - 15, drawbox.Location.Y);
                hitbox.Location = new Point(hitbox.Location.X - 15, hitbox.Location.Y);
            }
        }
    }
}
