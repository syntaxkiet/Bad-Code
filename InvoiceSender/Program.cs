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
        var invoicesSent = helper.Run(email);
        
        Console.WriteLine($"Sent {invoicesSent} invoices to {email}");

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
    
    // TODO: Test this method!
    public int Run(string email)
    {
        var user = _db.GetUser(email);
        // TODO: Enable several invoices per user
        // Could this be more than one?
        var invoice = _db.GetInvoice(user.Id);
        if (invoice is null)
            return 0;
        
        var dueDate = invoice.DueDate;
        var daysToDueDate = (dueDate - DateTime.Now).Days;

        var message = GetInvoice(user.Name, invoice.Amount, daysToDueDate);
        SendInvoice(message, user.Email);
        return 1;
    }

    // TODO: Test these two methods
    // They are private, so we can't test them directly, or can we?
    // TODO: How can we test these methods?
    // TODO: Should we test private methods?
    private string GetInvoice(string name, decimal amount, int daysToDueDate)
    {
        return $"Dear {name},\n\nYour invoice of {amount} is " +
               $"due in {daysToDueDate} days.\n\nBest regards,\nThe Invoice Team";
    }

    private void SendInvoice(string message, string email)
    {
        _emailSender.SendEmail(email, "Invoice Reminder", message);
    }
}