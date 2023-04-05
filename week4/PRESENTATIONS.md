# Week 4 Presentation Guides

This week's presentations focus on C# object oriented programming. You have likely worked with object-oriented programming before, so consider some of this content a review, but also a deeper dive into the concepts of OOP and polymorphism.

**Wednesday**:

- [Collections](#collections)
- [LINQ Basic Syntax](#linq-basic-syntax)
- [LINQ Method Syntax](#linq-method-syntax)

**Friday**:

- [LINQ Comprehensions](#linq-comprehensions)
- [LINQ Aggregate Methods](#linq-aggregate-methods)
- [Object-Relational Mappers](#object-relational-mappers)

## Collections

In C#, objects that contain a set of other objects are known as *collections*. 

An array is a specific type of collection - it is typically used to hold a fixed number of instances or values. For example, you can define an array of `int`s that will hold five values like this:

    int[] values = new int[5];

When you declare an array in this way, all of the values are initialized to their *default value*. For numeric value types this is 0, and for strings it is the empty string.

You can reference the items in an array by their numeric index, starting at 0. This is very similar to many other programming languages. For example, to print the first item of the above array:

    Console.WriteLine(values[0]);

and to print the last value:

    Console.WriteLine(values[4]);

As with many other languages, note that the last item of the array is always one number less than the number of items in the array, since the array index starts at 0.

You can make arrays out of any C# type. For example, if you have written a class called MyClass, you can create an array of instances of your class:

    MyClass[] manyInstancesOfMyClass = new MyClass[16];

Arrays are excellent for storing a known number of elements. However, while the elements of the array are mutable, the array itself is not. This means that you cannot resize the array; you can only manipulate the items within it. To resize the array, you would need to make a new array and then copy the values from the original array to the new array:

    int[] biggerArray = new int[16];
    Array.Copy(values, 0, biggerArray, 0, values.Length);

A C# alternative to fixed-size arrays is generic collections. C# has many generic collection types, the simplest of which is the `List`. A List works similarly to an array, but you can dynamically add and remove elements from the list. Lists are backed by arrays, but C# will automatically grow the array for you as you add new items. 

A List is a generic class, meaning it accepts a type in angle brackets. The list will contain elements of that type (or any compatible type). For example:

    List<int> valueList = new List<int>();

Note that you do not need to specify the size of the list - the list will automatically expand to hold the items you add. (In fact, lists and most other .NET framework collection types grow by prime numbers - i.e. the list will grow first to a size of 2, then 3, then 5, 7, 11, and so on, as you add items and as the space becomes necessary.)

To add an item to a list:

    valueList.Add(1); // add the integer "1" to the list

and to remove an item by its index:

    valueList.RemoveAt(0); // removes the first item from the list

You can also remove an item by value, which will remove the first occurrence of the value in the list:

    valueList.Remove(1);

There are many other methods available on most collections, among them `Find` (searches the list for an item and returns the first occurrence), `Reverse` (reverses the order of items in the list), `InsertRange` (allows you to insert multiple items at once by providing another List or similar enumerable object) and so on. A full list of methods of `List` is in the sources below.

Once you have items in a `List`, you can also treat them similarly to an array; for example, you can reference an item in a list by index:

    Console.WriteLine(valueList[0]);

Other common collection types in C# include the `Dictionary`, which is a list of key-value pairs; the `Tuple`, which lets you store groups of values of differing or similar types, and `Stack`, which works like a `List` but adds methods to push and pop values onto and off of the stack in a last-in first-out order. 

### Enumerable Initialization Syntax

Most enumerable types can be loaded with data at creation time. This applies to both fixed size arrays and dynamic lists. 

To provide the values for a list in code, simply follow the definition of the type with a brace (`{`) and then provide the items one by one, separated by commas. The items should be instances of the item type of the array or list. Close the list with a (`}`) and then with a semicolon to end the statement.

For example:

    var numbersList = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

Mote that we did not need to provide the length of the list, since the compiler can figure it out simply based on what values we provided. 

This also works for a list:

    var myList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

### IEnumerable

All collections and arrays implement the `IEnumerable` interface. This interface stipulates that the class must offer a way to iterate over a set of values. It specifies a single method, `GetEnumerator`, which is expected to return an instance of a class implementing the `IEnumerator` interface.

### IEnumerator

The `IEnumerator` interface is the interface that actually specifies the methods used to *iterate* over a collection. It only specifies two methods, `MoveNext` and `Reset`, and one property, `Current`. The idea is that you can call `MoveNext` to move to the next item in the list, and then you can retrieve that item with `Current`. You can keep doing this until the enumerator runs out of items to return, at which time the `MoveNext` method will return False to let you know there are no more items. `Reset` simply tells the enumerator to start over at the beginning of the list.

The `foreach` syntax in C# basically provides a shortcut to executing `MoveNext` and reading with `Current` and also handling the end-of-list condition. For example, you could use this code to print all the items in a list:

    var iterator = values.GetEnumerator();
    while (true)
    {
        if (!iterator.Next)
        {
            break; // no more items, exit loop
        }
        Console.WriteLine(iterator.Current); // print current item
    }

But you could simply this code to simply:

    foreach (int num in values) 
    {
        Console.WriteLine(num);
    }

For your presentation, please cover:

- What is a collection?
- How does a collection differ from an array?
- What does the IEnumerable interface specify? The IEnumerator interface?
- The `foreach` syntax
- Discuss how these constructs work in another programming language of your choice.

Sources to get you started (but please do seek out and use other sources as well!):

- [C# Collections](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/collections)
- [Generic (<T>) List class](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1?view=net-7.0)
- [IEnumerable interface](https://learn.microsoft.com/en-us/dotnet/api/system.collections.ienumerable?view=net-8.0)
- [IEnumerable interface](https://learn.microsoft.com/en-us/dotnet/api/system.collections.ienumerator?view=net-8.0)

## LINQ Basic Syntax

C# has a unique language feature known as LINQ (Language INtegrated Query). This language allows you to express various types of queries against objects containing lists of values or objects. The queries look vaguely similar to SQL queries (but are not the same). In fact, LINQ queries can sometimes look out of place in C# code, because they don't use the same coding style as C# does.

LINQ statements can be written one of two ways: in LINQ syntax (as was discussed in the last paragraph) or in method notation. In method notation, queries are expressed by using a chain of method calls, and the code looks more "C#-esque". However, method call queries can quickly become cumbersome and difficult to understand. LINQ queries are therefore considered the preferred option when it is possible to use them (but there are a few specific situations where you cannot use LINQ queries).

Suppose we have a list of the numbers 1 through 10:

    var myList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

Suppose we want to get a new list that contains only the numbers greater than 5:

    var myNewList = from num in myList 
                    where num > 5
                    select num;

`myNewList` will contain a `List<int>` instance that contains five records - the numbers 6 through 10. Notice that the original list is unaffected.

When it gets interesting is when you start to use this syntax with more complex types. Suppose we have this class:

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName  { get; set; }
        public int Age          { get; set; }
        public Person(string _FirstName, string _LastName, int _Age) 
        {
            this.FirstName = _FirstName;
            this.LastName = _LastName;
            this.Age = _Age;
        }
    }

And now we make a list of instances of this class:

    var personList = new List<Person>() {
        new Person("Harry", "Potter", 14),
        new Person("Hermione", "Granger", 14),
        new Person("Cedric", "Diggory", 17)
    };

Even though these are simple C# class instances with data fields, we can now perform complex queries on the data:

    var eligibleForTriWizardTournament = from p in personList
                                         where p.Age >= 17
                                         select p;

We can even select specific fields from the list. This is where using `var` comes in handy, because C# can generate an *anonymous type* for the response. An anonymous type is one that is not explicitly defined in your code, but is inferred from the results of a LINQ query.

    var eligibleForTriWizardTournament = from p in personList
                                         where p.Age >= 17
                                         select new { p.FirstName, p.LastName };

Or you can process the fields and produce your own output:

    var eligibleForTriWizardTournament = from p in personList
                                         where p.Age >= 17
                                         select $"{p.FirstName} {p.LastName}, age {p.Age}";

For your presentation, please cover:

- What is LINQ syntax?
- Provide some simple examples of how LINQ syntax might be used.
- LINQ is relatively unique to C#. Do some investigating to see if any other language has any similar in-language constructs. (The method notation, which we will look at on Friday, might be more similar to something implemented in a non-C# language.)
- You don't need to cover anything more advanced than a simple `select` and a `where` clause. Presentations later this week and into next week will cover more advanced LINQ syntax and usage.

Sources to get you started (but please do seek out and use other sources as well!):

- [Language-integrated Query](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/) at Microsoft.
- [LINQ Query Syntax](https://www.tutorialsteacher.com/linq/linq-query-syntax) at TutorialsTeacher.

## LINQ Method Syntax

The LINQ query syntax is generally the preferred way to write LINQ queries in your code. However, the LINQ syntax is just syntactic sugar; the .NET framework parses your LINQ query and generates an appropriate chain of *method calls* on the object in question.

All LINQ operations are simply methods on the collection object. Collection objects have many interfaces they can implement, depending on the style of collection. For example, the `IList` interface specifies methods like `Add`, `Clear`, `IndexOf`, and `RemoveAt`. Any collection object that can "behave like a List" implements `IList`. The most obvious example is, of course, the `List` itself. (However, other collection types also might implement `IList` so they can behave like lists for certain operations.)

Most of the LINQ methods are implemented as *extension methods* on the `IQueryable` interface. You can view a fairly comprehensive list [here](https://learn.microsoft.com/en-us/dotnet/api/system.linq.iqueryable-1?view=net-7.0).

Suppose we have a LINQ query that looks like this:

    var eligibleForTriWizardTournament = from p in personList
                                         where p.Age >= 17
                                         select p;

To run this query, C# will first examine and parse your LINQ query code. Based on several factors, C# will then translate the query into multiple method calls - essentially, it *transpiles* (to convert from one programming language to another) the LINQ query into standard C# code. This operation is controlled by many pieces - for example, if you're using LINQ queries on objects which are connected to a database, the LINQ queries might be converted to actual SQL statements and run again the database server; in this case, the LINQ queries are converted to method calls on the object containing the data.

The above query can be manually written as method calls like this:

    var eligibleForTriWizardTournament = personList.Where(p => p.Age >= 17);

The syntax of using the `=>` operator is known as *lambda syntax*. It is an inline way to define a method that returns a single value from a single expression. In this case, the `Where` method expects to be given a reference to a *method* that it can call for each item in the list; if that method returns True, the item will be included in the result, and if the method returns False, the item will not be included. In this case, we indicate that we want to write a lambda method using the variable name `p`, and then we write our expression involving `p` as `p.Age >= 17`. Each item in `personList` will be tested as the value `p` and the comparison will be made.

To make this a bit clearer, another way you could write the above code is like this:

    public static bool LambdaFunction(Person p)
    {
        return p.Age >= 17;
    }

    public static void Main() 
    {
        var eligibleForTriWizardTournament = personList.Where(LambdaFunction);
    }

This will accomplish the exact same effect, but the above syntax is much more concise. And, since you often won't need to reuse the code in that method that's just returning the result of one expression, you don't need to bother with defining a method.

The lambda syntax can be a bit confusing to wrap your head around. One way to help understand it is to realize that, like Python, C# does support methods as variables - in Python this is sometimes referred to functions being *first class*. C++ and other pointer-based languages use a similar construct - you can pass around the address of the code of a function and treat it like a function using function pointers. In this case, we are defining a function that accepts one parameter and runs a single expression and returns its result. 

### Why would you use method syntax if LINQ syntax is better?

There are a small number of LINQ operations that do not have an equivalent expression in LINQ syntax. One such operation is the `Count()` method, which simply returns the number of items in a collection. When you need to use these methods, you can either combine LINQ syntax with method syntax:

    var eligibleStudentCount = from p in personList
                               where p.Age >= 17
                               select p.Count();

or you can rewrite the entire query using method syntax:

    var eligibleForTriWizardTournament = personList.Where(p => p.Age >= 17).Count();

While you may not often need to use method syntax for writing LINQ queries, it is still good to know how it works and how you can utilize it yourself. 

For your presentation, please cover:

- Discuss LINQ method syntax and compare it with LINQ query syntax.
- Provide at least one other example of a query you might write using method syntax. It need not be a complex query - a simple `Where` query like in this example will suffice. We will be looking at more advanced queries after the presentations and on Friday.
- Provide a brief explanation of lambda syntax.
- Investigate if any other language has similar constructs to LINQ method syntax. While other languages may not have LINQ inline query syntax, the method-style syntax is sometimes seen in database connectivity libraries for other languages.

Sources to get you started (but please do seek out and use other sources as well!):

- [LINQ Method Syntax](https://www.tutorialsteacher.com/linq/linq-method-syntax) at TutorialsTeacher.
- [Query Syntax and Method Syntax](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/query-syntax-and-method-syntax-in-linq) at Microsoft.
- [Lambda expressions](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions) at Microsoft.
- [How to write a lambda expression](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions)

## LINQ Comprehensions

In Python, the *comprehension* is a useful feature that lets you compose a list or dictionary inline, by iterating over an existing list or other iterable with a transformation and an (optional) filter. 

A very simple Python example might look like this:

    myList = ['Apples', 'Bananas', 'Grapes', 'Oranges']
    myUppercaseList = [x.upper() for x in myList]

    # myUppercaseList is ['APPLES', 'BANANAS', 'GRAPES', 'ORANGES']

Since you can specify any evaluable expression inside the list comprehension, you could even write a function in Python and use it in the list comprehension:

    def modifyString(s):
        return "Modified: " + s

    myList = ['Apples', 'Bananas', 'Grapes', 'Oranges']
    myModifiedList = [modifyString(s) for x in myList]

    # myModifiedList is ['Modified: Apples', 'Modified: Bananas', 'Modified: Grapes', 'Modified: Oranges']
    
You can achieve the same behavior in C# using LINQ. Here is an example of uppercasing all of the strings in a list, using both LINQ Query syntax and method syntax:

    List<string> myList = new List<string> { "Apples", "Bananas", "Grapes", "Oranges" };

    // LINQ query syntax
    List<string> myUppercaseList = from x in myList select x.ToUpper();

    // LINQ method syntax
    List<string> myUppercaseList2 = myList.Select(x => x.ToUpper());

(Side note: All single-dimension enumerables like `List`s have a `ToArray()` method that can convert the `List` to a fixed-size array: `string[] myArray = myList.ToArray();`)

The key takeaway here is that LINQ, especially when working with C# types like `List`s and similar, allows you to easily transform the data by specifying the operation to perform one time. 

You can also use LINQ to run a custom method:

    public static string modifyString(string s) 
    {
        return "Modified: " + s;
    }

    public static void Main()
    {

        List<string> myList = new List<string> { "Apples", "Bananas", "Grapes", "Oranges" };

        // LINQ query syntax
        List<string> myUppercaseList = from x in myList select modifyString(x);

        // LINQ method syntax
        List<string> myUppercaseList2 = myList.Select(x => modifyString(x));

    }

Much like how Python has an `if` clause for list comprehensions:

    myFilteredList = [x for x in myList if len(x) > 6]

C# can do this as well using the `Where` operator that we have already seen:

    List<string> myFilteredList = from x in myList
                                  where x.Length() > 6
                                  select x;
    
    // or

    List<string> myFilteredList = myList.Where(x => x.Length() > 6);

(Note: With method syntax, you can omit a final `Select` if you are simply returning the results of the previous method. However you must include a final `select` in LINQ query syntax.)

Another comprehension that Python has is the dictionary comprehension. It is similar to a list comprehension, however you can programmatically specify both the key and the value to use in a dictionary.

    myDict = {x[0]: x for x in myList}

    # myDict will contain: {'A': 'Apples', 'B': 'Bananas', 'G': 'Grapes', 'O': 'Oranges'}

You can do this in C# as well using LINQ. This is actually one scenario where you must use method syntax:

    Dictionary<string, string> myDict = myList.ToDictionary(x => x.Substring(0,1), x);

You can actually combine both types of syntax by wrapping the LINQ query syntax inside parentheses:

    Dictionary<string, string> myDict = (from x in myList
                                         where x.Length() > 6
                                         select x).ToDictionary(x => x.Substrung(0,1), x);

LINQ in C# is actually significantly more powerful than comprehensions in Python. For example, since you can chain multiple conditions and selectors in LINQ, you can run complex queries involving multiple `Where` statements more concisely. In Python, you typically would need to do such a complex query using multiple nested list comprehensions or multiple steps. Additionally, Python comprehensions do not support advanced LINQ querying like joins and aggregate operators. These techniques are in another presentation.

For your presentation, please cover:

- Provide an example of list and dictionary comprehension in C# using LINQ. Compare and contrast this with Python's implementation.
- Compare and contrast the functionality of C# and Python comprehensions.

Sources to get you started (but please do seek out and use other sources as well!):

- [Python List Comprehensions for C#](https://markheath.net/post/python-list-comprehensions-and)
- [List Comprehensions in C#](https://thestandardoutput.com/posts/list-comprehensions-in-c/) at TheStandardOutput. (Note: This link might present a certificate error - the site admin has apparently not renewed their Lets Encrypt certificate. It should be safe to proceed to the site.)
- [Example of a C# Dictionary Comprehension](https://gist.github.com/built/2896420)

## LINQ Aggregate Methods

LINQ offers some methods that allow you to perform *aggregate* queries against a list. These methods return a single value. This is in contrast to most LINQ methods, which return a new collection - for example, `Where` returns a new collection that contains those items that satisfy the condition given in the expression. Even if only one entry matches, you will still receive a collection with one item in it. However, aggregate methods do not return lists or collections, they return an individual value.

Here is a list of the built-in LINQ aggregate methods:

- [`Aggregate`] (returns the type of the collection) - Accepts a method that accepts two parameters and returns one value. This method is called repeatedly, starting with the first two elements. Each output is fed as the first output into the next iteration; the result of calling the method for the first two items is used as the first item along with the third item for the next call, and so on. A common use case is to combine a list of strings with a custom separator.
- [`Count`] (returns `int`) - Returns an integer representing the number of items in the collection.
- [`Max`] and [`Min`] (returns a numeric type) - For collections containing numbers or other items that can be compared with greater-than or less-than logical operators, returns the maximum or minimum value item from the collection.
- [`Sum`] (returns a numeric type) - Returns the sum of all items. For numeric types this is simply the total of all items summed. 
- [`Average`] (returns a numeric type) - Returns the mathematical average of a collection of numeric values.
- [`All`] (returns `bool`) - Returns `true` if all elements in the list match a condition, otherwise `false`.
- [`Any`] (returns `bool`) - Returns `true` if any element in the list matches a condition, otherwise `false`.
- [`First`] (returns the type of the collection) - Returns the first item in the collection. 
  - The related method `FirstOrDefault` will return a default value - typically `null` - if there are no items in the collection. The `First` method will raise an exception if the collection has no items.
  - There is also `Last` and `LastOrDefault`.
  - You can also use the `Single` method, which works the same as `First` but will throw an exception if more than one item in the collection satisfies the condition. 

Many of these methods are self-explanatory. For example, `myList.Count()` simply returns an integer representing the number of items in the collection. (You can use the `Length` property on arrays and strings, but you can't use it on `List`, `Dictionary` and similar collections.) The `Max`, `Min`, `Sum` and `Average` methods return a numeric value based on the type of the values in the list.

The methods `All` and `Any` expect a lambda expression inside of the method call. This lambda expression is expected to return a boolean value. The expression is called for every item in the list (for `All`) and for every item in the list until the expression returns `true` (for `Any`). For example, if you wanted to know if all numbers in a list are greater than 10, you could use: `myList.All(x => x > 10);`.

The `Aggregate` method can be put to some interesting uses. A very simple example is to concatenate all the elements in a list of `string`s using a custom join string. (Python has a nice function for doing this - something like `", ".join(myStrings)` joins all the strings in a list with a comma and a space (`, `).) However, it's not limited to this - you can do any operation that accepts two values and returns one value of the collection's type.

The common string concatenation example looks like this:

    List<string> myStrings = new List<string> { "Apples", "Bananas", Grapes", "Oranges" };

    string joinedString = myStrings.Aggregate( (x, y) => x + ", " + y );

This is a new kind of lambda expression - one that accepts multiple parameters. You can simply think of it as a method that accepts two parameters - `x` and `y`. The lambda expression in this example takes the first parameter `x`, adds a `", "`, and then adds the parameter `y`. 

The `Aggregate` method will cause the lambda expression to be called `n - 1` times (with `n` being the size of the collection). The first time through, the first and second items of the collection will be used. The return value of this first call will then be used as the first value in the next call, along with the third method. That result will be the first value in the next call along with the fourth method, and so on.

Another way that you could write the code for `Aggregate` looks like this, which should help make it easier to understand:

    public static string AggregateMethod(string x, string y)
    {
        return x + ", " + y;
    }

    public static void Main() 
    {
        List<string> myStrings = new List<string> { "Apples", "Bananas", Grapes", "Oranges" };

        int listLength = myStrings.Count();
        listLength--; // subtract one from the length

        string result = myStrings[0];

        for (int n = 0; n < listLength; n++)
        {
            result = AggregateMethod(result, n + 1);
        }

        Console.WriteLine(result);
    }

For your presentation, please cover:

- Discuss the various aggregating LINQ methods
- Provide some example use cases of a few of the methods

Sources to get you started (but please do seek out and use other sources as well!):

- [LINQ Aggregate Functions](https://dotnettutorials.net/lesson/linq-aggregate-functions/)
- [All](https://dotnettutorials.net/lesson/linq-all-operator/) and [Any](https://dotnettutorials.net/lesson/linq-any-operator/) methods
- [First / FirstOrDefault / Last / LastOrDefault / Single / SingleOrDefault](https://riptutorial.com/csharp/example/329/first--firstordefault--last--lastordefault--single--and-singleordefault)

## Object-Relational Mappers

Using LINQ to query C# data objects is very useful, but where this technique gets *especially* powerful is when you introduce a database server. Many programming languages have options for providing access to a database; a common strategy for doing this is to use an *object-relational mapper*. An ORM provides a translation layer between the programming language and the database - a popular approach is to represent database tables as classes, while the ORM does the "heavy lifting" of populating collections of instances of those classes which directly represent data in the database tables.

C#'s LINQ system allows libraries to write their own strategies and methods for implementing query methods. The methods we have been using up to this point work against built-in data objects. With an ORM, however, LINQ queries can be directly translated internally to SQL queries and run against the database - effectively, this means you don't have to write any SQL to access an SQL database!

Quite possibly the most popular ORM for C# is Microsoft's [Entity Framework](https://learn.microsoft.com/en-us/ef/). Entity Framework is optimized to work with Microsoft SQL Server, but it is also possible to use it with many other database systems, such as MySQL, Postgres, or the file-based SQL Server Compact. There are also extensions for other systems like SQLite and Oracle Database. Using LINQ to query databases via Entity Framework is known as "LINQ-to-entities". Entity Framework is definitely a complex topic and an entire course could be taught on it, so this discussion will be relatively surface-level. 

Using Entity Framework involves producing classes for all of the tables in your database, and then setting up the actual connection to the database. EF comes with tools that can automatically generate C# classes based on an analysis of a live database, or you can manually create the classes yourself. (The classes do not need to exactly conform to the database tables - you can add extra methods, properties, and so on, or you can exclude fields that you "don't care" about. The only caveat is that you can't have missing fields that require values (e.g. `NOT NULL`) if you intend to use Entity Framework to insert records.)

Suppose we have a database table that looks like this:

    CREATE TABLE Product (
        id INT NOT NULL PRIMARY KEY,
        name VARCHAR(255) NOT NULL,
        price DECIMAL NOT NULL,
        taxable BIT NOT NULL DEFAULT 0
    )

You can probably already imagine how such a table could be very easily represented as a C# class:

    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public bool taxable { get; set; }
    }

And then you could imagine that the entire `Product` table can be represented as a C# array or list:

    List<Product> productTable = ...

From here, you could start imagining some queries you might want to run against the Product table. 

    List<Product> expensiveProducts = from product in productTable
                                      where product.price > 99.9
                                      select product;

    List<Product> taxableProducts = from product in productTable
                                    where product.taxable
                                    select product;

    List<string> productStrings = from product in productTable
                                  select $"{product.name} - {product.price.ToString("C")}";

    // or

    List<Product> expensiveProducts2 = productTable.Where(x => x.price > 99.9);

    List<Product> taxableProducts2 = productTable.Where(x => x.taxable == true);

    List<string> productStrings2 = productTable.Select(x => $"{x.name} - {x.price.ToString("C")}");

Entity Framework (and ORMs in general) also handle foreign-key relationships. The way this is typically done for a one-to-many relationship is to include a backreference to the "one" side of the join on the entity for the "many" side, and a List referencing back to entities on the "many" side on the "one" side.

In the previous example, let's add a SaleLine table, where we store a list of items that are included with each sale of products - sort of like a point-of-sale checkout application. 

    CREATE TABLE SaleLine (
        id INT NOT NULL PRIMARY KEY,
        product INT NOT NULL,
        qty INT NOT NULL DEFAULT = 1,
        FOREIGN KEY (product) REFERENCES Product(id)
    )

(For simplicity, I'm not including a `Sale` table, although in practice you would definitely have such a table with an ID per transaction that the `SaleLine` table can reference.)

The class for SaleLine would look like this:

    public class SaleLine 
    {
        public int id { get; set; }
        public Product product { get; set; }
        public int qty { get; set; }
    }

and the class for Product now looks like this:

    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public bool taxable { get; set; }
        public List<SaleLine> SaleLines { get; set; }
    }

This now affords you the ability to do complex queries across table joins without having to express joins in LINQ. For example:

    SaleLine sl = saleLineTable.First(x => x.id == 1);

    Console.WriteLine($"Sale line ID #1 is for: {sl.product.name}");

In this example, we accessed the `name` column of the prodct record that matches the foreign key stored in the SaleLine record. We can similarly go the other way:

    List<int> saleLinesForProduct = productTable.First(x => x.id == 5).SaleLine.Select(y => y.id);

This query is a bit more complex. If we work from left to right:

- `productTable` starts us off with the entire `Product` table.
- `First` selects the first (and only) entry in the `Product`  table with an `id` of `5`. This will return a `Product` instance, *not* a list of `Product` instances - so we can now directly access the fields of that instance.
- `SaleeLine` is the `List<SaleLine>` field of the `Product `instance - the list of all `SaleLine` records that reference this `Product`.
- And finally, `Select` reaches into each `SaleLine` record and gets its id. The final result will be a list of `int` values (since `id` is an `int`).

From here, you can start to imagine the kidns of complex queries that can be written. And the best part is you don't even need to know SQL and you definitely don't need to write `JOIN` clauses!

You can also use Entity Framework to add records, delete records or even update records. For example, you can insert a record to the database by simply doing:

    productTable.Add(new Product { name = 'Widget', price = 5.99M, taxable = true });

You can directly edit an instance of a class representing a record in the database:

    var myProduct = productTable.First(x => x.id == 10);
    myProduct.name = "New Widget";

You would then use a `SaveChanges` method on your database connection to actually cause the update to be written to the database.

And finally, you can delete records:

    var myProduct = productTable.First(x => x.id == 10);
    productTable.Remove(myProduct);

This is an extremely light overview of the capabiities of Entity Framework - if you have some database background, you can really leverage the power of the ORM to do things like calling stored procedures by simply calling methods in C#, accessing views as dynamically-generated classes, and so on.

For your presentationm, please cover:

- What is an ORM and what advantages does it offer to the programmer
- A conceptual overview of Entity Framework. (You do not need to provide a tutorial on how to use it specifically and you don't need to create any databases - unless you really want to. We're only looking for an explanation of what the framework is and how it relates to LINQ.)

Sources to get you started (but please do seek out and use other sources as well!):

- [LINQ-to-Entities Examples and Tutorial](https://www.entityframeworktutorial.net/querying-entity-graph-in-entity-framework.aspx)
- [Introduction to LINQ with Entity Framework in Visual Studio](https://www.c-sharpcorner.com/article/introduction-to-linq-with-entity-framework-in-visual-studio2/)
