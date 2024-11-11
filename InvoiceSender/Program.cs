// This is NOT how you should be coding!

namespace InvoiceSender;

internal class Program
{
    public static void Main(string[] args)
    {
        var db = new Database();
        var emailSender = new EmailSender();
        
        var helper = new InvoiceHelper(db, emailSender);

        var email = "john@example.com";
        helper.Run(email);

    }
}

public class InvoiceHelper
{
    private readonly IDatabase _db;
    private readonly IEmailSender _emailSender;

    public InvoiceHelper(IDatabase db, IEmailSender emailSender)
    {
        _db = db;
        _emailSender = emailSender;
    }
    
    public void Run(string email)
    {
        var user = _db.GetUser(email);
        var invoice = _db.GetInvoice(user.Id);
        var dueDate = invoice.DueDate;
        var daysToDueDate = (dueDate - DateTime.Now).Days;

        var message = GetInvoice(user.Name, invoice.Amount, daysToDueDate);
        SendInvoice(message, user.Email);
    }

    // TODO: Test these two methods
    public string GetInvoice(string name, decimal amount, int daysToDueDate)
    {
        return $"Dear {name},\n\nYour invoice of {amount} is " +
               $"due in {daysToDueDate} days.\n\nBest regards,\nThe Invoice Team";
    }

    public void SendInvoice(string message, string email)
    {
        _emailSender.SendEmail(email, "Invoice Reminder", message);
    }
}