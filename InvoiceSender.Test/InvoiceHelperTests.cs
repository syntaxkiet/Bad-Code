namespace InvoiceSender.Test;
using System.Reflection;

public class InvoiceHelperTests
{
    [Fact]
    public void PrivateMethodTest()
    {
        // Arrange
        var db = new Database();
        var emailSender = new EmailSender();
        
        var helper = new InvoiceHelper(db, emailSender);
        
        var expected = "Dear Daniel,\n\nYour invoice of 10 is " +
                       "due in 10 days.\n\nBest regards,\nThe Invoice Team";
        // Act
        var actual = helper.TestGetInvoice("Daniel", 10, 10);

        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void PrivateMethodTestWithReflection()
    {
        // Arrange
        var db = new Database();
        var emailSender = new EmailSender();
        
        var helper = new InvoiceHelper(db, emailSender);
        
        var expected = "Dear Daniel,\n\nYour invoice of 10 is " +
                       "due in 10 days.\n\nBest regards,\nThe Invoice Team";
        var args = new object[] { "Daniel", 10m, 10 };
        
        // Act
        var privat = typeof(InvoiceHelper).GetMethod("GetInvoice", BindingFlags.NonPublic | BindingFlags.Instance);
        var actual = privat.Invoke(helper, args);
        
        // Assert
        Assert.Equal(expected, actual);
    }
}