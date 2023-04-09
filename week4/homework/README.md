## Homework

For Week 4, you will be expanding upon your Week 3 homework. No new code is provided - just a few new tasks.

For this week, please use LINQ wherever possible. You may use either query syntax or method syntax - it is your choice. How you implement these featureas is also your choice, but make sure you test your code and *consider edge cases, invalid input and so on* to make sure your code is robust!

This exercise is also to help you think about how to approach programming. You can use any strategy for implementing these features. However, do your best to explore the most efficient and "future-proof" strategy for implementing the functionality.

* Provide a mechanism to print out the current transaction while it is underway - not just afterwards as a receipt. 
* Following on to the previous feature, provide a function to allow a product to be *removed* from the transaction while it is still underway. 
* Rework the application so that multiple transactions can be placed, and transactions that have already been placed can be viewed. For example, rewrite the main method so that the program asks the user whether they want to start a new transction or view existing transactions.
* Along with the previous functionality, add a function to print out the total of all transactions that have occurred. (Hint: add a property or method to your transaction class and then use LINQ to Sum() it on all transaction instances.)
* Devise and implement a strategy for retrieving the product database from an external source, rather than having it stored as static values within the program.
  * One option that will be available to you is to use Entity Framework Core to connect to a database. If you go this route, you do not need to worry about writing the data to a CSV or similar file and parsing it yourself. 
    * If you're interested, [here](https://www.entityframeworktutorial.net/efcore/create-model-for-existing-database-in-ef-core.aspx) is a good place to get started.
    * You'll also need to use [NuGet](https://learn.microsoft.com/en-us/nuget/quickstart/install-and-use-a-package-in-visual-studio) to install the `EntityFrameworkCore.SqlServer` package, which will give you the tools you need to access the Entity Framework commands. You may also need additional packages - see [this documentation](https://learn.microsoft.com/en-us/ef/core/managing-schemas/scaffolding/?tabs=dotnet-core-cli) for more information.
  * Alternatively you can store the data in a text file (such as a CSV flat file) and write code that reads the product database in at the start of the program. If you do this, make sure to include your data file with your submission!
  * This strategy is open-ended - for example, if you are so inclined, you could also store a text file on a web server and retrieve the file using C#'s web request functions. The core requirement is that the product database *not* be part of the C# code - that you not use the array/collection initialization syntax to populate the product list.

One more note - Week 5 will also build on Week 4's code, so keep your code around for next week as well!

Also, a reminder - please zip only the C# project folder, not the entire CS403 repository. 

Good Luck!

## Database Credentials

If you decide to go with Entity Framework, here is the information you need:

> Database Server Hostname: campus-quest.com
> Database Server Port: 22222
> Username: student
> Password: cs403!
> Connection string: Data Source=campus-quest.com,22222;User ID=student;Password=cs403!;Initial Catalog=ConvenienceStore
>

(Note that for SQL Server, you specify the port number using a comma rather than a colon after the hostname.)

You can also use [SQL Server Management Studio](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16) or [Azure Data Studio](https://azure.microsoft.com/en-us/products/data-studio) to connect to the database and explore it yourself prior to setting it up in your project.

The database has other tables such as Category that you don't need to worry about for this project.
