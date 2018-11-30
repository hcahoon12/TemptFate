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
	public class Titan : Character
	{
		List<Buttons> comboOne = new List<Buttons>() {Buttons.A , Buttons.X , Buttons.Y };//y x a
		List<Buttons> comboTwo = new List<Buttons>() { Buttons.X, Buttons.DPadLeft, Buttons.DPadDown };//down left X
		List<Buttons> comboThree = new List<Buttons>() { Buttons.X, Buttons.DPadRight, Buttons.DPadDown };//down right X
		public Titan(int x, int y):base(new Rectangle(x, y, 100, 100), 6.6, 50)
		{
			firstCombo = false;
		}
		public override void LoadContent(ContentManager Content)
		{
			base.LoadContent(Content, "TestMystic", "ss (2)", "TestMystic" , "Knife","ss");
		}
		//might make a facing left aND RIGHT FUNCTION To change combos 
		public override void Update(GameTime gameTime, List<Line> Lines, GamePadState gamepadstate, Character enemy)
		{
			attackBox = new Rectangle(-300, 500, 20, 100);
			UpdateShot();
			try
			{
				if (comboOne[0] == Combos[0] && comboOne[1] == Combos[1] && comboOne[2] == Combos[2])
				{
					firstCombo = true;
					//set combo animation
					damage = 1;
					attackBox = new Rectangle(hitbox.X+100, hitbox.Y, 20,100);
				}
				if (comboTwo[0] == Combos[0] && comboTwo[1] == Combos[1] && comboTwo[2] == Combos[2] || comboThree[0] == Combos[0] && comboThree[1] == Combos[1] && comboThree[2] == Combos[2])
				{
					shotDelay.Start();
					Shoot();
				}
				
			} catch (ArgumentOutOfRangeException ex) {}
			
			base.Update(gameTime, Lines, gamepadstate, enemy);
		}
	
	}
}
