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
		List<Buttons> comboOne = new List<Buttons>() {Buttons.A , Buttons.X , Buttons.B };//bxa
		List<Buttons> comboTwo = new List<Buttons>() { Buttons.X, Buttons.DPadLeft, Buttons.DPadDown };//down left X
		public Titan(int x, int y):base(new Rectangle(x, y, 100, 100), 6.6, 50)
		{

		}
		public override void LoadContent(ContentManager Content)
		{
			base.LoadContent(Content, "TestMystic", "ss (2)", "TestMystic" , "Knife");
		}
		public override void Update(GameTime gameTime, List<Line> Lines, GamePadState gamepadstate)
		{
			try
			{
				if (comboOne[0] == Combos[0] && comboOne[1] == Combos[1] && comboOne[2] == Combos[2])
				{
					hitbox.X = 600;
					hitbox.Y = 100;
				}
				if (comboTwo[0] == Combos[0] && comboTwo[1] == Combos[1] && comboTwo[2] == Combos[2])
				{
					Shoot();
				}
			} catch (ArgumentOutOfRangeException ex) {}
			
			base.Update(gameTime, Lines, gamepadstate);
		}
		
		public void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
		}
	}
}
