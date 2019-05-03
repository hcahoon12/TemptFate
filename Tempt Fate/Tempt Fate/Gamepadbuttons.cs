using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Tempt_Fate
{
	public class Gamepadbuttons
	{
		public bool newRightTrigger;
		bool oldRightTrigger;
		bool newA;
		bool newStart;
		public bool newB;
		bool newX;
		bool newY;
		public bool newLeft;
		public bool newRight;
		bool newDown;
		public bool newUp;
		bool oldA;
		bool oldB;
		bool oldX;
		bool oldY;
		bool oldLeft;
		bool oldRight;
		bool oldDown;
		bool oldStart;
		bool oldUp;
        Keys Up;
		Keys right;
		Keys left;
		Keys down;
		Keys start;
		Keys a;
		Keys x;
		Keys y;
		Keys righttrigger;
		Keys b;
		Keys Up2;
		Keys right2;
		Keys left2;
		Keys a2;
		Keys x2;
		Keys y2;
		Keys righttrigger2;
		Keys b2;

		public Gamepadbuttons()
		{
			oldA = false;
			oldB = false;
			oldDown = false;
			oldLeft = false;
			oldRight = false;
			oldX = false;
			oldY = false;
			oldStart = false;
			oldUp = false;
		}

		//All the keyboard stuff was added for testing in case you didnt have a controller 

		private void setKeys(int playerNum)
		{
			if (playerNum == 1)
			{
				Up = Keys.W;
				start = Keys.Q;
				right = Keys.D;
				left = Keys.A;
				down = Keys.S;
				a = Keys.C;
				x = Keys.V;
				y = Keys.B;
				b = Keys.N;
				righttrigger = Keys.E;
			}
			else
			{
				Up = Keys.Up;
				right = Keys.Right;
				left = Keys.Left;
				down = Keys.Down;
				a = Keys.NumPad1;
				start = Keys.Space;
				x = Keys.NumPad2;
				y = Keys.NumPad3;
				b = Keys.NumPad0;
				righttrigger = Keys.RightShift;
			}

		}
		public Buttons? Update(GamePadState gamepadstate, int playerNum) //returns button if button that was just pressed 
		{
			Buttons? returnVal = null;
			setKeys(playerNum);
			KeyboardState kbstate = Keyboard.GetState();
			newRightTrigger = gamepadstate.IsButtonDown(Buttons.RightTrigger) || kbstate.IsKeyDown(righttrigger);
			newStart = gamepadstate.IsButtonDown(Buttons.Start) || kbstate.IsKeyDown(start);
			newA = gamepadstate.IsButtonDown(Buttons.A) || kbstate.IsKeyDown(a);
			newB = gamepadstate.IsButtonDown(Buttons.B) || kbstate.IsKeyDown(b);
			newX = gamepadstate.IsButtonDown(Buttons.X) || kbstate.IsKeyDown(x);
			newY = gamepadstate.IsButtonDown(Buttons.Y) || kbstate.IsKeyDown(y);
			newLeft = gamepadstate.IsButtonDown(Buttons.DPadLeft) || kbstate.IsKeyDown(left);
			newRight = gamepadstate.IsButtonDown(Buttons.DPadRight) || kbstate.IsKeyDown(right);
			newDown = gamepadstate.IsButtonDown(Buttons.DPadDown) || kbstate.IsKeyDown(down);
			newUp = gamepadstate.IsButtonDown(Buttons.DPadUp) || kbstate.IsKeyDown(Up);
            //return buttons so combos can equal true
            if (newA == true && oldA == false)
            {
                returnVal = Buttons.A;
            }
            else if (newStart == true && oldStart == false)
			{
				returnVal = Buttons.Start;
			}
			else if (newRightTrigger == true && oldRightTrigger == false)
			{
				returnVal = Buttons.RightTrigger;
			}
			else if (newB == true && oldB == false)
			{
				returnVal = Buttons.B;
			}
			else if (newX == true && oldX == false)
			{
				returnVal = Buttons.X;
			}
			else if (newY == true && oldY == false)
			{
				returnVal = Buttons.Y;
			}
			else if (newLeft == true && oldLeft == false)
			{
				returnVal = Buttons.DPadLeft;
			}
			else if (newRight == true && oldRight == false)
			{
				returnVal = Buttons.DPadRight;
			}
			else if (newDown == true && oldDown == false)
			{
				returnVal = Buttons.DPadDown;
			}
			else if (newUp == true && oldUp == false)
			{
				returnVal = Buttons.DPadUp;
			}
			oldA = newA;
			oldB = newB;
			oldX = newX;
			oldRight = newRight;
			oldLeft = newLeft;
			oldY = newY;
			oldDown = newDown;
			oldUp = newUp;
			oldStart = newStart;
			oldRightTrigger = newRightTrigger;
			return returnVal; 
		}
	}
}
