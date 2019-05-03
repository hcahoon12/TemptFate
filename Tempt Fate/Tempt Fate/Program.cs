using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace Tempt_Fate
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
			//try catch is if crashes the player can send an email explaining the problem
			try
			{
				using (var game = new Game1())
				{
					game.Run();
				}
			}
			catch (Exception ex)
			{
				var errorfile = File.Create("C:\\TemptFate\\TemptFate\\Tempt Fate\\Tempt Fate\\bin\\Windows\\x86\\Debug\\errorTemptFate");
				errorfile.Close();
				File.WriteAllText("C:\\TemptFate\\TemptFate\\Tempt Fate\\Tempt Fate\\bin\\Windows\\x86\\Debug\\errorTemptFate", ex.Message);
				DialogResult dialogResult = MessageBox.Show(ex.ToString(), "Send email?", MessageBoxButtons.YesNo);
				if (dialogResult == DialogResult.Yes)
				{
					MailMessage mail = new MailMessage("Farviewstudio12@gmail.com", "Farviewstudio12@gmail.com");
					SmtpClient client = new SmtpClient();
					NetworkCredential loginInfo = new NetworkCredential("Farviewstudio12@gmail.com", "hcahoon12"); // password for connection smtp if u dont have have then pass blank
					client.UseDefaultCredentials = true;
					client.Credentials = loginInfo;
					client.Host = "smtp.gmail.com";
					client.EnableSsl = true;
					mail.Subject = "Crash in Tempt Fate";
					mail.Body = ex.ToString();
					client.Send(mail);
				}
				else if (dialogResult == DialogResult.No)
				{
					Environment.Exit(0);
				}
			}
		}
	}
#endif
}
