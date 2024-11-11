// This is NOT how you should be coding!

using InvoiceSender;

var db = new Database();
var emailSender = new EmailSender();
var user = db.GetUser("john@example.com");
var invoice = db.GetInvoice(user.Id);
var dueDate = invoice.DueDate;
var daysToDueDate = (dueDate - DateTime.Now).Days;
var message = $"Dear {user.Name},\n\nYour invoice of {invoice.Amount} is due in {daysToDueDate} days.\n\nBest regards,\nThe Invoice Team";
emailSender.SendEmail(user.Email, "Invoice Reminder", message);