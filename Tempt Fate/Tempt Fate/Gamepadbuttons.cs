﻿using System;
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
		public Buttons? Update(GamePadState gamepadstate) //returns button if button that was just pressed 
		{
			Buttons? returnVal = null;
			newStart = gamepadstate.IsButtonDown(Buttons.Start);
			newA = gamepadstate.IsButtonDown(Buttons.A);
			newB = gamepadstate.IsButtonDown(Buttons.B);
			newX = gamepadstate.IsButtonDown(Buttons.X);
			newY = gamepadstate.IsButtonDown(Buttons.Y);
			newLeft = gamepadstate.IsButtonDown(Buttons.DPadLeft);
			newRight = gamepadstate.IsButtonDown(Buttons.DPadRight);
			newDown = gamepadstate.IsButtonDown(Buttons.DPadDown);
			newUp = gamepadstate.IsButtonDown(Buttons.DPadUp);
			//return buttons so combos can equal true
			if (newA == true && oldA == false)
			{
				returnVal = Buttons.A;
			}
			else if (newStart == true && newStart == false)
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
