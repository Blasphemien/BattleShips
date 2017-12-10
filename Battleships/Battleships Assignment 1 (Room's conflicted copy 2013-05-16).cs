using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleships
{
    public partial class BattleshipsAssignment1 : Form
    {
        int yCoordinate = 0;
        int xCoordinate = 0;
        int lastMove = -1;
        int shipsTerminated = 0;
        int shipsExisting = 5;
        int movesExecuted = 0;
        int hits = 0;

        static string[,] myField = new string[10, 10];

        Label[,] labelGrid = new Label[10, 10];
        public BattleshipsAssignment1()
        {
            InitializeComponent();
        }

        private void BattleshipsAssignment1_Load(object sender, EventArgs e)
        {
            //sets the static positions for each vessel

            //Cruiser
            myField[8, 3] = "b";
            myField[8, 5] = "b";
            myField[8, 6] = "b";

            //Patrol Boats
            myField[3, 8] = "b";
            myField[2, 8] = "b";

            myField[6, 0] = "b";
            myField[7, 0] = "b";

            //Submarine
            myField[1, 2] = "b";
            myField[0, 2] = "b";

            //Destroyer
            myField[5, 4] = "b";
            myField[4, 4] = "b";
            myField[3, 4] = "b";
            myField[2, 4] = "b";

            //Battleship
            myField[6, 9] = "b";
            myField[6, 8] = "b";
            myField[6, 7] = "b";
            myField[6, 6] = "b";
            myField[6, 5] = "b";
        }

        public void CheckBoats()
        {
            //checks to see if it is a boat
            if (myField[xCoordinate, yCoordinate] == "b")
            {
                myField[xCoordinate, yCoordinate] = "h";
                labelGrid[xCoordinate, yCoordinate].BackColor = System.Drawing.Color.Red;
            }

            else
            {
                //if it is the sea

                labelGrid[xCoordinate, yCoordinate].BackColor = System.Drawing.Color.Green;
            }
            lastMove = (xCoordinate + yCoordinate);

        }


        

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void buttonLaunch_Click(object sender, EventArgs e)
        {
            try
            {

                xCoordinate = Convert.ToInt32(textCoordinatesX.Text);
                yCoordinate = Convert.ToInt32(textCoordinatesY.Text);

                IsNumeric();
            }
            catch (FormatException)
            {
                MessageBox.Show("Inputs must be an interger");
                textCoordinatesX.Text = "";
                textCoordinatesY.Text = "";
                return;
            }

            if (IsNumeric() == false)
            {
                MessageBox.Show("Inputs must be an interger");
                return;
            }

         
            Int32.TryParse(textCoordinatesX.Text, out xCoordinate);
            Int32.TryParse(textCoordinatesY.Text, out yCoordinate);
            MessageBox.Show("" +xCoordinate + yCoordinate);



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
                MessageBox.Show("Inputs must be an interger");
                textCoordinatesX.Text = "";
                textCoordinatesY.Text = "";
            }
            return false;


        }
    }
}