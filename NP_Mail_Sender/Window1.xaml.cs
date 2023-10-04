using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NP_Mail_Sender
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        string Mail { get; set; }
        string Password { get; set; }
        int Num { get; set; }
        List<string> FileName { get; set; }
        public Window1(string mail, string password, int num)
        {
            InitializeComponent();
            Mail = mail;
            Password = password;
            Num = num;
            FileName = new List<string>();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                FileName.Add(dialog.FileName);
            }
            AddFiles.Text += dialog.FileName + "\n";
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // create new mail
            MailMessage mail = new MailMessage(Mail, toTxtBox.Text)
            {
                Subject = subjectTxtBox.Text,
                Body = $"<h1>My Mail Message from C#</h1><p>{bodyTxtBox.Text}</p>",
                IsBodyHtml = true,
                Priority = MailPriority.High
            };

            // add attachments
            var result = MessageBox.Show("Do you want to attach a file?", "Attach File", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                for (int i = 0; i < FileName.Count(); i++)
                {
                    mail.Attachments.Add(new Attachment(FileName[i]));
                }
            }
            string smtp = "";
            int port = 0;
            if(Num == 0)
            {
                smtp = "smtp.gmail.com";
                port = 587;
            }
            else
            {
                smtp = "smtp.office365.com";
                port = 587;
            }
            // send mail message
            SmtpClient client = new SmtpClient(smtp, port)
            {
                Credentials = new NetworkCredential(Mail, Password),
                EnableSsl = true
            };

            client.Send(mail);
        }
    }
}
