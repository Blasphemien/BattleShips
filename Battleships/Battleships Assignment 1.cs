using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Program: Battleships for Assignment 1 Programming
//Created by: Keil Carpenter
//Version: 1.0
//Release date: 20/5/2013

namespace Battleships
{
    public partial class BattleshipsAssignment1 : Form
    {
        int yCoordinate = 0;
        int xCoordinate = 0;
        int destroyed = 0;
        int remaining = 6;
        int hits = 0;
        int launched = 0;
        int miss = 0;
        int seconds = 0;
        int milliseconds = 0;
        int minutes = 0;
        
        //define our boats
        private const string CRUISER = "Cruiser";
        private int CRUISER_HITS = 3;

        private const string PATROLBOAT1 = "Patrol Boat1";
        private int PATROLBOAT1_HITS = 2;

        private const string PATROLBOAT2 = "Patrol Boat2";
        private int PATROLBOAT2_HITS = 2;

        private const string SUBMARINE = "Submarine";
        private int SUBMARINE_HITS = 2;

        private const string DESTROYER = "Destroyer";
        private int DESTROYER_HITS = 4;

        private const string BATTLESHIP = "Battleship";
        private int BATTLESHIP_HITS = 5;

        static string[,] myField = new string[10, 10];
        Label[,] labelGrid = new Label[10, 10];

        public static bool KeepRunning { get; set; }

        public BattleshipsAssignment1()
        {
            InitializeComponent();
            MessageBox.Show("1: Enter coordinates into text boxes" + "\n" + "2: Click the 'Launch' button" + "\n" + "To start a new game simply click the 'New Game' button at the bottom of the grid.");
        }
        private void BattleshipsAssignment1_Load(object sender, EventArgs e)
        {

            //boat healths
            CRUISER_HITS = 3;
            PATROLBOAT1_HITS = 2;
            PATROLBOAT2_HITS = 2;
            SUBMARINE_HITS = 2;
            DESTROYER_HITS = 4;
            BATTLESHIP_HITS = 5;

            yCoordinate = 0;
            xCoordinate = 0;
            hits = 0;
            launched = 0;
            destroyed = 0;
            miss = 0;
            remaining = 6;


            labelTotalLaunchedRec.Text = "0";
            labelHitsrecord.Text = "0";
            labelDestroyed.Text = "0";
            labelRemaining.Text = "6";
            labelMissesRec.Text = "0";
            labelMinutes.Text = "00";
            labelSeconds.Text = "00";
            labelMilliseconds.Text = "00";
            textCoordinatesX.Text = "";
            textCoordinatesY.Text = "";

            //sets the static positions for each vessel

            //Cruiser
            myField[8, 3] = CRUISER;
            myField[8, 4] = CRUISER;
            myField[8, 5] = CRUISER;
            //Patrol Boat1
            myField[3, 8] = PATROLBOAT1;
            myField[2, 8] = PATROLBOAT1;
            //patrol Boat2
            myField[6, 0] = PATROLBOAT2;
            myField[7, 0] = PATROLBOAT2;
            //Submarine
            myField[1, 2] = SUBMARINE;
            myField[0, 2] = SUBMARINE;
            //Destroyer
            myField[5, 4] = DESTROYER;
            myField[4, 4] = DESTROYER;
            myField[3, 4] = DESTROYER;
            myField[2, 4] = DESTROYER;
            //Battleship
            myField[6, 9] = BATTLESHIP;
            myField[6, 8] = BATTLESHIP;
            myField[6, 7] = BATTLESHIP;
            myField[6, 6] = BATTLESHIP;
            myField[6, 5] = BATTLESHIP;

          
            {
                labelBattleship.BackColor = Color.LightGreen;
                labelPatrolBoat1.BackColor = Color.LightGreen;
                labelPatrolBoat2.BackColor = Color.LightGreen;
                labelCrusier.BackColor = Color.LightGreen;
                labelDestroyer.BackColor = Color.LightGreen;
                labelSubmarine.BackColor = Color.LightGreen;
            }
        }

        public void CheckBoats()
        //checks to see if it the coordinate is a boat if it is changes backcolour to red
        {

            Label thisLabel = (Label)Controls["label" + xCoordinate + "_" + yCoordinate + ""];
            //if grid is null then there is no boat set in array grid
            if (myField[xCoordinate, yCoordinate] != null)
            {
                //switch the boat

                //attack boat
                string thisBoat = myField[xCoordinate, yCoordinate];
                attackBoat(thisBoat);

                string boat_hit = myField[xCoordinate, yCoordinate];
                hits++;


                labelHitsrecord.Text = hits.ToString();
                launched++;
                labelTotalLaunchedRec.Text = launched.ToString();
                myField[xCoordinate, yCoordinate] = "h";
                thisLabel.BackColor = Color.Red;
            }

            else
            //If it is a miss changes backcolour to green
            {

                thisLabel.BackColor = Color.Green;
                launched++;
                miss++;
                labelMissesRec.Text = miss.ToString();
                labelTotalLaunchedRec.Text = launched.ToString();

            }
        }

        private void attackBoat(string thisBoat)
        {   
            //switch boat & decrease hits of boat health/spaces
            switch (thisBoat)
            {
                case CRUISER:
                    CRUISER_HITS--;
                    break;
                case PATROLBOAT1:
                    PATROLBOAT1_HITS--;
                    break;
                case PATROLBOAT2:
                    PATROLBOAT2_HITS--;
                    break;
                case SUBMARINE:
                    SUBMARINE_HITS--;
                    break;
                case DESTROYER:
                    DESTROYER_HITS--;
                    break;
                case BATTLESHIP:
                    BATTLESHIP_HITS--;
                    break;
                default:
                    //should not land here but just in case
                    Console.WriteLine("ERROR -> attackBoat() called couldnt attack boat -> " + thisBoat);
                    break;
            }

        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void buttonLaunch_Click(object sender, EventArgs e)
        {
            
            {
                try
                {
                    xCoordinate = Convert.ToInt32(textCoordinatesX.Text);
                    yCoordinate = Convert.ToInt32(textCoordinatesY.Text);
                    IsNumeric();
                }

                catch (FormatException)
                {
                    MessageBox.Show("Inputs must be an integer");
                    textCoordinatesX.Text = "";
                    textCoordinatesY.Text = "";
                    return;
                }

                if (IsNumeric() == false)
                {
                    MessageBox.Show("Inputs must be an integer");
                    return;
                }

                if (xCoordinate < 0 || yCoordinate < 0)
                {
                    MessageBox.Show("Please enter a valid set of coordinates");
                    xCoordinate = 0;
                    yCoordinate = 0;
                }

                else
                {
                    CheckBoats();
                    CheckDestroyed();
                }
                textCoordinatesX.Text = "";
                textCoordinatesY.Text = "";
            }
            if (remaining == 0)
            {
                timerRun.Stop();
                MessageBox.Show("You win with a total time of " + labelMinutes.Text + " Minute(s) " + labelSeconds.Text + " second(s) " + labelMilliseconds.Text + " millisecond(s) " + "\n" + "Total Misses: " + labelMissesRec.Text + "\n" + "Total shots fired: " + labelTotalLaunchedRec.Text + "\n" + "Thankyou for playing!");

            }
        }

        private void shipSunk()
        {
            //sinks & updates our stats
            destroyed++;
            remaining--;
            labelDestroyed.Text = destroyed.ToString();
            labelRemaining.Text = remaining.ToString();
        }
        private void CheckDestroyed()
        {
            //check destroyed boats health/hits and reset boat health
            if (CRUISER_HITS == 0)
            {
                shipSunk();
                labelCrusier.BackColor = Color.Red;
                MessageBox.Show("You sunk the Cruiser!");

                //make sure we dont display this message again sink boat
                CRUISER_HITS = -1;

            }


            else if (PATROLBOAT1_HITS == 0)
            {
                shipSunk();
                labelPatrolBoat1.BackColor = Color.Red;
                MessageBox.Show("Patrol(1) Boat destroyed");

                //sink boat
                PATROLBOAT1_HITS = -1;

            }
            else if (PATROLBOAT2_HITS == 0)
            {
                shipSunk();
                labelPatrolBoat2.BackColor = Color.Red;
                MessageBox.Show("Patrol Boat(2) destroyed");

                PATROLBOAT2_HITS = -1;
            }

            else if (SUBMARINE_HITS == 0)
            {
                shipSunk();
                labelSubmarine.BackColor = Color.Red;
                MessageBox.Show("Submarine destroyed");
                SUBMARINE_HITS = -1;
            }
            else if (DESTROYER_HITS == 0)
            {
                shipSunk();
                labelDestroyer.BackColor = Color.Red;
                MessageBox.Show("Destroyer destroyed");
                DESTROYER_HITS = -1;

            }
            else if (BATTLESHIP_HITS == 0)
            {
                shipSunk();
                labelBattleship.BackColor = Color.Red;
                MessageBox.Show("Battleship destroyed");

                BATTLESHIP_HITS = -1;

            }
        }

        public bool IsNumeric()
        {
            try
            {
                string tempStringX = xCoordinate.ToString();
                string tempStringY = yCoordinate.ToString();

                if (Char.IsDigit(tempStringX, 0) && Char.IsDigit(tempStringY, 0))
                {

                    return true;
                }

                else
                {
                    return false;
                }

            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Inputs must be an integer");
                textCoordinatesX.Text = "";
                textCoordinatesY.Text = "";
            }
            return false;
        }



        private void buttonRestart_Click(object sender, EventArgs e)
        {
            //stops timer and resets values back to 0
            
            timerRun.Stop();
            minutes = 0;
            seconds = 0;
            milliseconds = 0;
            
            //re insert boats into grid
            //Reset the grid labels 
            //label0_0    -> labely_x
            Array.Clear(myField, 0, 100);

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    Control ourLabel = Controls["label" + y + "_" + x];
                    Label thisLabel = (Label)ourLabel;
                    thisLabel.BackColor = Color.FromName("ControlLight");
                }

                {
                    labelBattleship.BackColor = Color.LightGreen;
                    labelPatrolBoat1.BackColor = Color.LightGreen;
                    labelPatrolBoat2.BackColor = Color.LightGreen;
                    labelCrusier.BackColor = Color.LightGreen;
                    labelDestroyer.BackColor = Color.LightGreen;
                    labelSubmarine.BackColor = Color.LightGreen;
                }
            }

            //recall init load class
            BattleshipsAssignment1_Load(null, null);

            xCoordinate = 0;
            yCoordinate = 0;
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //re insert boats into grid
            //Reset the grid labels 
            //label0_0    -> labely_x
            Array.Clear(myField, 0, 100);

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    Control ourLabel = Controls["label" + y + "_" + x];
                    Label thisLabel = (Label)ourLabel;
                    thisLabel.BackColor = Color.FromName("ControlLight");
                }

                {
                    labelBattleship.BackColor = Color.LightGreen;
                    labelPatrolBoat1.BackColor = Color.LightGreen;
                    labelPatrolBoat2.BackColor = Color.LightGreen;
                    labelCrusier.BackColor = Color.LightGreen;
                    labelDestroyer.BackColor = Color.LightGreen;
                    labelSubmarine.BackColor = Color.LightGreen;
                }
            }

            //recall init load class
            BattleshipsAssignment1_Load(null, null);

            xCoordinate = 0;
            yCoordinate = 0;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Program name: Battleships" + "\n" + "Version: 1.0" + "\n" + "Release date: 20/05/2013" + "\n" + "This game was developed by Keil Carpenter for a programming assignment");
        }

        private void instructionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1: Enter coordinates into text boxes" + "\n" + "2: Hit Launch" + "\n" + "To start a new game simply click the 'New Game' button at the bottom of the grid.");
        }

        private void buttonClose_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void timerRun_Tick(object sender, EventArgs e)
        //Sets time so >9 milliseconds = seconds++ and >59 seconds = minutes++
        {
            {
                milliseconds++;
                {
                    if (milliseconds == 9)
                    {
                        milliseconds = 0;
                        seconds = seconds + 1;
                        labelSeconds.Text = seconds.ToString();
                        
                        if (seconds == 60)
                        {
                            seconds = 0;
                            minutes++;
                            labelMinutes.Text = minutes.ToString();
                        }
                    }
                    labelMilliseconds.Text = milliseconds.ToString();
                    {

                    }
                }





            }


        }

        private void textCoordinatesX_TextChanged(object sender, EventArgs e)
        //starts timer once input has been made into textCoordinatesX
        {
            timerRun.Start();
        }
    }
}