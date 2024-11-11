// This is NOT how you should be coding!

namespace InvoiceSender;

internal class Program
{
    public static void Main(string[] args)
    {
        var db = new Database();
        var emailSender = new EmailSender();
        var user = db.GetUser("john@example.com");
        var invoice = db.GetInvoice(user.Id);
        var dueDate = invoice.DueDate;
        var daysToDueDate = (dueDate - DateTime.Now).Days;
        
        var helper = new InvoiceHelper();
        
        var message = helper.GetInvoice(user.Name, invoice.Amount, daysToDueDate);
        helper.SendInvoice(message, user.Email, emailSender);
    }
}

public class InvoiceHelper()
{
    // public InvoiceHelper()
    // {
    //     
    // }

    // TODO: Test these two methods
    public string GetInvoice(string name, decimal amount, int daysToDueDate)
    {
        return $"Dear {name},\n\nYour invoice of {amount} is " +
                      $"due in {daysToDueDate} days.\n\nBest regards,\nThe Invoice Team";
    }

    public void SendInvoice(string message, string email, IEmailSender emailSender)
    {
        emailSender.SendEmail(email, "Invoice Reminder", message);
    }
}