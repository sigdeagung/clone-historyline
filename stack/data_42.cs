using SendFileTo;

namespace TestSendTo
{
    public partial class Form1 : Form
    {
        private void btnSend_Click(object sender, EventArgs e)
        {
            MAPI mapi = new MAPI();

            mapi.AddAttachment("c:\\temp\\file1.txt");
            mapi.AddAttachment("c:\\temp\\file2.txt");
            mapi.AddRecipientTo("person1@somewhere.com");
            mapi.AddRecipientTo("person2@somewhere.com");
            mapi.SendMailPopup("testing", "body text");

            // Or if you want try and do a direct send without displaying the 
            // mail dialog mapi.SendMailDirect("testing", "body text");
        }
    }
}
message.Attachments.Add(new Attachment(yourAttachmentPath));
public static void CreateMessageWithAttachment(string server)
{
    // Specify the file to be attached and sent.
    // This example assumes that a file named Data.xls exists in the
    // current working directory.
    string file = "data.xls";
    // Create a message and set up the recipients.
    MailMessage message = new MailMessage(
       "jane@contoso.com",
       "ben@contoso.com",
       "Quarterly data report.",
       "See the attached spreadsheet.");

    // Create  the file attachment for this e-mail message.
    Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
    // Add time stamp information for the file.
    ContentDisposition disposition = data.ContentDisposition;
    disposition.CreationDate = System.IO.File.GetCreationTime(file);
    disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
    disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
    // Add the file attachment to this e-mail message.
    message.Attachments.Add(data);

    //Send the message.
    SmtpClient client = new SmtpClient(server);
    // Add credentials if the SMTP server requires them.
    client.Credentials = CredentialCache.DefaultNetworkCredentials;

    try 
    {
      client.Send(message);
    }
    catch (Exception ex) 
    {
      Console.WriteLine("Exception caught in CreateMessageWithAttachment(): {0}", ex.ToString());              
    }
    data.Dispose();
}