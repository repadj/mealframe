namespace App.DAL.EF.DataSeeding;

public class InitialData
{
    public static readonly (string username, string firstName, string lastName, string password, Guid? id)[]
        Users =
        [
            ("test@account.com", "test", "account", "Testing123@", null),
        ];
}