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
		bool newB;
		bool newX;
		bool newY;
		bool newLeft;
		bool newRight;
		bool newDown;
		bool oldA;
		bool oldB;
		bool oldX;
		bool oldY;
		bool oldLeft;
		bool oldRight;
		bool oldDown;
		public Gamepadbuttons()
		{
			oldA = false;
			oldB = false;
			oldDown = false;
			oldLeft = false;
			oldRight = false;
			oldX = false;
			oldY = false;
		}
		public Buttons? Update(GamePadState gamepadstate) //returns button if button that was just pressed 
		{
			Buttons? returnVal = null;
			newA = gamepadstate.IsButtonDown(Buttons.A);
			if (newA == true && oldA == false)
			{
				returnVal= Buttons.A;
			}
			oldA = newA;
			return returnVal; 
		}
	}
}
