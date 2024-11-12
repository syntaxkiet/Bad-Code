namespace InvoiceSender.Test;
using System.Reflection;

public class InvoiceHelperTests
{
    [Theory]
    [InlineData("daniel@mindfulstack.se", 1)]
    [InlineData("ruben@mindfulstack.se", 0)]
    public void SendsCorrectNumberOfEmails(string email, int expected)
    {
        var db = new TestDatabase();
        var emailSender = new TestEmailSender();

        var sut = new InvoiceHelper(db, emailSender);
        // Act
        var actual = sut.Run(email);

        // Assert
        Assert.Equal(expected, actual.Count);
    }
    
    [Fact]
    public void SendsCorrectMessage()
    {
        // Arrange
        var db = new TestDatabase();
        var emailSender = new TestEmailSender();
        var expected = "Dear Daniel,\n\nYour invoice of 10 is " +
                       "due in 10 days.\n\nBest regards,\nThe Invoice Team";

        var sut = new InvoiceHelper(db, emailSender);
        // Act
        var actual = sut.Run("daniel@mindfulstack.se");

        // Assert
        Assert.Equal(expected, actual[0]);
    }

    
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

internal class TestDatabase : IDatabase
{
    public User GetUser(string email)
    {
        switch (email)
        {
            case "daniel@mindfulstack.se":
                return new User
                {
                    Id = 1,
                    Name = "Daniel",
                    Email = email
                };
            case "ruben@mindfulstack.se":
                return new User
                {
                    Id = 2,
                    Name = "Ruben",
                    Email = email
                };
            default:
                return new User
                {
                    Id = 0,
                    Name = "Unknown",
                    Email = email
                };
        }
    }

    public Invoice? GetInvoice(int userId)
    {
        switch (userId)
        {
            case 1:
                return new Invoice
                {
                    Amount = 10,
                    DueDate = DateTime.Now.AddDays(10)
                };
            default:
                return null;
        }

    }
}

internal class TestEmailSender : IEmailSender
{
    public void SendEmail(string email, string subject, string messsage)
    {
        // Do nothing
    }
}