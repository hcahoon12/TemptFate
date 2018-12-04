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
	public class Mystic : Character
	{
		List<Buttons> comboOne = new List<Buttons>() { Buttons.A, Buttons.X, Buttons.Y };//y x a
		List<Buttons> comboTwo = new List<Buttons>() { Buttons.X, Buttons.X, Buttons.A };//a x x
		List<Buttons> comboThree = new List<Buttons>() { Buttons.Y, Buttons.A, Buttons.Y }; // y a y
		List<Buttons> specialShotLeft = new List<Buttons>() { Buttons.X, Buttons.DPadLeft, Buttons.DPadDown };//down left X
		List<Buttons> specialShotRight = new List<Buttons>() { Buttons.X, Buttons.DPadRight, Buttons.DPadDown };//down right X
		public Mystic(int x, int y) :base(new Rectangle(x, y, 100, 100), 6.6, 500)
		{
			facingLeft = true;
		}
		public override void LoadContent(ContentManager Content)
		{
			base.LoadContent(Content, "mysticwalkright", "mysticwalkleft", "ss (2)", "Knife","ss (2)");
		}
		public override void Update(GameTime gameTime, List<Line> Lines, GamePadState gamepadstate, Character enemy)
		{
			attackBox = new Rectangle(-300, 500, 20, 100);
			try
			{
				if (gamepadstate.IsButtonDown(Buttons.X))
				{
					damage = 5;
					if (facingRight == true)
					{
						attackBox = new Rectangle(hitbox.X + 100, hitbox.Y, 20, 100);
					}
					else
					{
						attackBox = new Rectangle(hitbox.X - 20, hitbox.Y, 20, 100);
					}
				}
				if (gamepadstate.IsButtonDown(Buttons.Y))
				{
					damage = 5;
					if (facingRight == true)
					{
						attackBox = new Rectangle(hitbox.X + 100, hitbox.Y, 20, 100);
					}
					else
					{
						attackBox = new Rectangle(hitbox.X - 20, hitbox.Y, 20, 100);
					}
				}
				if (gamepadstate.IsButtonDown(Buttons.A))
				{
					damage = 5;
					if (facingRight == true)
					{
						attackBox = new Rectangle(hitbox.X + 100, hitbox.Y, 20, 100);
					}
					else
					{
						attackBox = new Rectangle(hitbox.X - 20, hitbox.Y, 20, 100);
					}
				}
				if (comboOne[0] == Combos[0] && comboOne[1] == Combos[1] && comboOne[2] == Combos[2])
				{
					damage = 1;
					if (facingRight == true)
					{
						attackBox = new Rectangle(hitbox.X + 100, hitbox.Y, 20, 100);
					}
					else
					{
						attackBox = new Rectangle(hitbox.X - 20, hitbox.Y, 20, 100);
					}
				}
				if (comboTwo[0] == Combos[0] && comboTwo[1] == Combos[1] && comboTwo[2] == Combos[2])
				{
					damage = 1;
					if (facingRight == true)
					{
						attackBox = new Rectangle(hitbox.X + 100, hitbox.Y, 20, 100);
					}
					else
					{
						attackBox = new Rectangle(hitbox.X - 20, hitbox.Y, 20, 100);
					}
				}
				if (comboThree[0] == Combos[0] && comboThree[1] == Combos[1] && comboThree[2] == Combos[2])
				{
					damage = 1;
					if (facingRight == true)
					{
						attackBox = new Rectangle(hitbox.X + 100, hitbox.Y, 20, 100);
					}
					else
					{
						attackBox = new Rectangle(hitbox.X - 20, hitbox.Y, 20, 100);
					}
				}
				if (specialShotLeft[0] == Combos[0] && specialShotLeft[1] == Combos[1] && specialShotLeft[2] == Combos[2] || specialShotRight[0] == Combos[0] && specialShotRight[1] == Combos[1] && specialShotRight[2] == Combos[2])
				{
					shotDelay.Start();
					Shoot();
				}
				
			}

			catch (ArgumentOutOfRangeException ex) { }
			base.Update(gameTime, Lines, gamepadstate, enemy);
		}
		
	}
}
