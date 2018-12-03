using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace TrainProject
{
    public partial class MailForm : Form
    {
        public MailForm(List<string> MailBody)
        {
            InitializeComponent();

            textMailBody.Text = "Ich habe diese Verbindungen gefunden:\n";

            foreach (var item in MailBody)
            {
                textMailBody.Text += item + "\n";
            }
        }

        private void btnSendMail_Click(object sender, EventArgs e)
        {
            var fromAddress = new MailAddress(textFromMail.Text);
            var fromPassword = textPassword.Text;
            var toAddress = new MailAddress(textToMail.Text);

            string subject = textMailSubject.Text;
            string body = textMailBody.Text;

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)

            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })

            try
            {
                smtp.Send(message);
                    this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Sieht so aus als ob es einen Fehler gab, bitte überprüfen sie ihr Passwort.\n Falls es dies nicht ist, " +
                    "bitte überprüfen sie, dass ihre Sicherheitseinstellung auf der Mail für \"Weniger sichere Apps zulassen\" auf \"an\" ist", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
