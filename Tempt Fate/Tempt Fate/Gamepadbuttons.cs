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
	class Gamepadbuttons
	{
		bool newA;
		bool newStart;
		bool newB;
		bool newX;
		bool newY;
		bool newLeft;
		bool newRight;
		bool newDown;
		bool newUp;
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
				start = Keys.Space;
				right = Keys.D;
				left = Keys.A;
				down = Keys.S;
				a = Keys.C;
				x = Keys.V;
				y = Keys.B;
				b = Keys.N;
			}
		}
		public Buttons? Update(GamePadState gamepadstate, int playerNum) //returns button if button that was just pressed 
		{
			Buttons? returnVal = null;
			setKeys(playerNum);
			KeyboardState kbstate = Keyboard.GetState();
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
			return returnVal; 
		}
	}
}
