# Week 3 Presentation Guides

This week's presentations focus on C# object oriented programming. You have likely worked with object-oriented programming before, so consider some of this content a review, but also a deeper dive into the concepts of OOP and polymorphism.

**Wednesday**:

- [Collections][#collections]
- [LINQ Basic Syntax](#linq-basic-syntax)
- [LINQ Method Syntax](#linq-method-syntax)

**Friday**:

Friday's content will be posted by Tuesday.

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
