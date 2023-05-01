

using System.Data.Common;
using System.Data.SQLite;

ReadData(CreateConnection());
//InsertCustomer(CreateConnection());
RemoveCustomer(CreateConnection());
//FindCustomer(CreateConnection());

static SQLiteConnection CreateConnection()
{
    SQLiteConnection connection = new SQLiteConnection("Data Source=mydb.db; Version = 3; New = True; Compress = True;");

    try
    {
        connection.Open();
        Console.WriteLine("DB found.");

    }
    catch
    {
        Console.WriteLine("DB not found.");
    }
    return connection;
}

static void ReadData(SQLiteConnection myConnection)
{
    Console.Clear();
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myConnection.CreateCommand();
    command.CommandText = "SELECT rowid, * FROM customer";
        
        //"SELECT customer.firstName, customer.lastName, status.statustype " +
        //"FROM customerStatus " +
        //"Join customer on customer.rowid = customerid " +
        //"JOIN status on status.rowid = customerStatus.statusId " +
        //"ORDER BY status.statustype";

    reader = command.ExecuteReader();

    while (reader.Read())
    {
        string readerRowId = reader["rowid"].ToString();
        string readerStringFirstName = reader.GetString(1);
        string readerStringLastName = reader.GetString(2);
        string readerStringDoB = reader.GetString(3);

        Console.WriteLine($"{readerRowId}. Full name: {readerStringFirstName} {readerStringLastName}; DoB: {readerStringDoB}");
    }

    myConnection.Close();
}

static void InsertCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;
    string fName, lName, dob;

    Console.WriteLine("Enter first name:");
    fName = Console.ReadLine();
    Console.WriteLine("Enter last name:");
    lName = Console.ReadLine();
    Console.WriteLine("Enter date of birth (mm-dd-yyyy):");
    dob = Console.ReadLine();


    command = myConnection.CreateCommand();

    command.CommandText = $"INSERT INTO customer(firstName, lastName, dateOfBirth) " +
        $" VALUES ('{fName}', '{lName}','{dob}')";

    int rowInserted = command.ExecuteNonQuery();
    Console.WriteLine($"Row inserted: {rowInserted}");


    ReadData(myConnection);
}

static void RemoveCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;

    string idToDelete;
    Console.WriteLine("Enter an id to delete a customer:");
    idToDelete = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"Delete FROM customer Where rowid = {idToDelete}";
    int rowRemoved = command.ExecuteNonQuery();
    Console.WriteLine($"{rowRemoved} was removed from the table customer.");

    ReadData(myConnection);

}


//static void FindCustomer(SQLiteConnection myConnection)
//{
//    SQLiteCommand command;

//    string customerToFind;
//    Console.WriteLine("Enter customer Full Name");
//    customerToFind = Console.ReadLine();

//    command = myConnection.CreateCommand();
//    command.CommandText = $"Find customer Whos Name = {customerToFind}";
//    int FindCustomer = command.ExecuteNonQuery();
//    Console.WriteLine($" It is {customerToFind}");

//    ReadData(myConnection);

//}