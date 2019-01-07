using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;

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
			try {
				using (var game = new Game1())
				{
					
					game.Run();

				}
			}
			catch (Exception ex)
			{
				DialogResult dialogResult = MessageBox.Show(ex.ToString(), "Send email?", MessageBoxButtons.YesNo);
				if (dialogResult == DialogResult.Yes)
				{
					MailMessage mail = new MailMessage("you@yourcompany.com", "user@hotmail.com");
					SmtpClient client = new SmtpClient();
					client.Port = 25;
					client.DeliveryMethod = SmtpDeliveryMethod.Network;
					client.UseDefaultCredentials = false;
					client.Host = "smtp.gmail.com";
					mail.Subject = "this is a test email.";
					mail.Body = "this is my test email body";
					client.Send(mail);
				}
				else if (dialogResult == DialogResult.No)
				{
					//do something else
				}
				
			}
        }
    }
#endif
}
