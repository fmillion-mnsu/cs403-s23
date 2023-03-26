# Week 3 Presentation Guides

This week's presentations focus on C# object oriented programming. You have likely worked with object-oriented programming before, so consider some of this content a review, but also a deeper dive into the concepts of OOP and polymorphism.

**Wednesday**:

- [Week 3 Presentation Guides](#week-3-presentation-guides)
  - [Classes (and Subclasses and Abstract Classes)](#classes-and-subclasses-and-abstract-classes)
  - [Interfaces](#interfaces)
  - [Constructors and Destructors (Finalizers)](#constructors-and-destructors-finalizers)
  - [Properties](#properties)
  - [Special Topic: C# with Docker](#special-topic-c-with-docker)
  - [The new, override and virtual keywords](#the-new-override-and-virtual-keywords)
  - [Access Modifiers](#access-modifiers)
  - [Advanced Topic: Sealed Classes and Nested Classes](#advanced-topic-sealed-classes-and-nested-classes)
  - [Advanced Topic: Generics](#advanced-topic-generics)

**Friday**:

- [Week 3 Presentation Guides](#week-3-presentation-guides)
  - [Classes (and Subclasses and Abstract Classes)](#classes-and-subclasses-and-abstract-classes)
  - [Interfaces](#interfaces)
  - [Constructors and Destructors (Finalizers)](#constructors-and-destructors-finalizers)
  - [Properties](#properties)
  - [Special Topic: C# with Docker](#special-topic-c-with-docker)
  - [The new, override and virtual keywords](#the-new-override-and-virtual-keywords)
  - [Access Modifiers](#access-modifiers)
  - [Advanced Topic: Sealed Classes and Nested Classes](#advanced-topic-sealed-classes-and-nested-classes)
  - [Advanced Topic: Generics](#advanced-topic-generics)

## Classes (and Subclasses and Abstract Classes)

C# follows the pattern of many other languages for classes: a class is a block of code that represents an object that can be instantiated. C# classes can contain methods, fields (variables that are public), or properties (items with getters and/or setters). 

C# allows a class to be subclassed, but each class may only inherit from *one* class. This is in contrast to a language like Python, which allows a class to inherit from any number of classes. (C# implements a type of multiple-inheritance through interfaces, which is covered in another presentation.)

Another feature often available in strongly-typed, compiled languages like C# is abstract classes. Abstract classes may not be directly instantiated; only subclasses of the abstract class may be instantiated. This can be used to force the programmer to implement their own implementation of the class, or when it is not sensible for the superclass to be directly instantiated based on the data model.  A simple example might be a class that handles encryption; suppose the class is called `EncryptionProvider` and it exposes a set of methods and properties. Subclasses of `EncryptionProvider` implement specific encryption algorithms, for example `RSAEncryptionProvider`. In this case it would not make sense to directly instantiate the `EncryptionProvider` class, since it doesn't actually represent a specific encryption algorithm. In this case, `EncryptionProvider` could be declared as an abstract class.

For your presentation, please cover:

* A basic review of classes. This need only be a slide or two and can be a simple definition and/or example. Assume everyone has seen classes before; you're just setting the stage for the C# specific content.
* How to write a basic class in C#. Choose your own sample class - try to be creative (the typical "Animal" example with a "Dog" subclass is very common - see if you can come up with something original!)
* How to subclass a class in C#.
* How to declare a class as abstract in C#, and what this means for a class which subclasses the class.
  * Particularly important is declaring methods as `abstract`, and then using `override` to implement those methods in the subclass.

Sources to get you started (but please do seek out and use other sources as well!):

* [C# Classes and Objects](https://www.w3schools.com/cs/cs_classes.php) at W3Schools.
* [Classes](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/classes) at Microsoft.
* [abstract Keyword](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/abstract) at Microsoft.

## Interfaces

A C# *interface* is a type of language construct that allows you to define a set of methods and/or properties that a class must include if it **implements** the interface. An interface can be treated like a type - you can cast a class that implements the interface to a variable typed as the interface type, and then you can call the interface's methods on that reference. (You can't call other class methods; only the ones the interface implements!)

Interfaces are typically used to describe "what a class can do" rather than "what a class represents". If the action could be described as an adjective ending in "able", it's a good target for an interface. Examples might be "readable", "writable", "sortable", "queryable" and so on. The code for an interface itself does not define any implementation of the methods; it only defines the method names, return types, and parameters. It's up to any class that implements the interface to provide an implementation of the methods in code.

The idea is that any class that implements the interface can interact with any code that only cares about the interface's functionality. If you have some code that can handle *any* object that "can be queried", then that code could use an interface reference so that it can work with *any* instance of *any* class that implements that interface. This way, the code needn't worry about implementation details; it only needs to know that "whatever instance I've been passed knows how to do the things the interface defines, and that's all I care about." 

Interfaces are one way that C# allows polymorphism - code can change function based on the references it has been provided with. Interfaces also can form the foundation for dependency injection - a class or method can accept an instance of an interface type, and the caller can provide the code with any instance implementing the interface.

Another important note about C# (and similarly Java) is that, while a C# class can only inherit from one class, it can implement as many interfaces as it needs or wants to.

In C#, it's common to prefix interface names with a capital I. For example, `IQueryable` and `IReadable` would be typical interface names. This isn't required by the language, but it is a convention that is seen throughout the .NET framework libraries and it is a good idea to use it in your own code as well.

For your presentation, please cover:

* What is an interface? How does it differ from a class?
* How to write an interface in C#
* How to work with an interface - declare a variable as an interface type, cast an instance to the interface type, and work with the interface methods

Sources to get you started (but please do seek out and use other sources as well!):

* [C# Interfaces](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/interfaces) at Microsoft.
  * The example on this page uses the `<T>` syntax for defining a type-agnostic method. We'll cover this next week in Advanced Polymorphism.
* [Interfaces](https://www.w3schools.com/cs/cs_interface.php) at W3Schools.

## Constructors and Destructors (Finalizers)

A *constructor* is a piece of code that runs each time a class is instantiated. 

A constructor can require parameters, which would mean the code instantiating the class must provide those parameters. This is a common method for providing required initialization information for the class.

A *destructor*, sometimes referred to in C# as a **finalizer**, runs when an instance is destroyed. This typically occurs if the instance falls out of scope (e.g. you instantiated the class in a method, and your method ends, so the class is no longer in scope), or it can occur in classes that implement the `using` pattern, where the class requires the chance to do some cleanup work prior to being destroyed (e.g. it must finish writing to an open file and close the file, or it must gracefully close a network connection).

A destructor cannot require any parameters, since it is almost never directly called by code, but instead is automatically ran by the runtime.

You can provide multiple constructors using *method overloading*, a strategy where you define the same method multiple times but with different parameters each time. The definition that is used depends on on which parameters your code passes to the method.

Finally, note that constructors don't have a return type like `void` or `string`. They are simply defined as a public method with the name of the class; for example, a class named "Course" would define its constructor as follows:

    public Course() {
        // code
    }

For your presentation, please cover:

* What are constructors and destructors? Why are they useful?
* How do you write a constructor in C#?
* If you have knowledge of other languages (for example, Python), you can include a comparison with that language (e.g. in Python the constructor is always called `__init__`).

Sources to get you started (but please do seek out and use other sources as well!):

* [Constructors](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/constructors) at Microsoft.
* [Finalizers](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/finalizers) at Microsoft.
* [Constructors vs. Destructors](https://www.c-sharpcorner.com/article/constructor-vs-destructor-c-sharp/) at C# Corner.

## Properties

A **property** is a class field that is implemented with getters and setters. Getters and setters are blocks of code that are used to either provide a value (getter) or accept a value (setter) from the calling code. A property can be treated like a class variable from other code. 

It is possible to have a property that only implements a getter (a read-only property) or only implements a setter (a write-only property). There are many scenarios where this may be useful. 

To implement a property, you start by defining the type and name, just as you would with a class variable, and then you provide the getter and setter in code blocks:

    public string ExampleProperty 
    {
      get {
        return "This is an example property.";
      }
      set {
        Debug.WriteLine("The ExampleProperty was set to " + value);
      }
    }

(Note that in the setter, the pseudo-parameter `value` is used to represent the value that the property was set to by outside code.)

To make a property without a getter or setter, you simply exclude the `get` or `set` block of code.

There is also a convenience method for defining a simple property that has a very straightforward getter and setter (that simply gets or retrieves a value from a local, private variable). If you simply include `get;` or `set;`, the C# compiler will automatically generate a local variable as a backing store and implement a simple getter and setter:

    public string ExampleProperty { get; set; }

There is one situation where this can be quite useful; you can define either `get` or `set` as `private`. This allows you to make a property that acts like a variable, but that only you can read or write from within the class:

    public string ExampleProperty { get; private set; } // this property can only be read from outside code, but you can set it yourself in code within the class.

A common reason for using a property is so that you can validate values being set, or so that you can transform data as it is being read. You could, for example, ensure that any code setting a value on the property cannot set it to a number that is out of range:

    public double MilesDriven 
    {
      set 
      {
        if (value < 0) 
        {
          throw new ArgumentOutOfRangeException("Miles driven must be greater than or equal to 0!");
        }
        this._MilesDriven = value;
      }
    }

Alternatively, you could write a custom implementation to turn a numeric value stored in the class into a meaningful string value (such as converting an integer into a number of bytes):

    public double Feet 
    {
      get 
      {
        return this.Inches / 12.0;
      }
    }

The latest beta C# iteration allows a "required" field on properties. Since this is not currently in mainstream use, you do not need to cover it.

For your presentation, please cover:

* What is a property? How does it differ from a standard class variable?
* Give some examples of where properties are useful (many given in this text, and also some on the resources below, and also try to come up with one of your own!)
* How to write a property in a class (getter and/or setter)
* The use of the empty getter/setter (e.g. `get; private set;`)

Sources to get you started (but please do seek out and use other sources as well!):

* [C# Properties](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties) at Microsoft.
* [Properties in C#](https://learn.microsoft.com/en-us/dotnet/csharp/properties) at Microsoft.
* [Property in C#](https://www.c-sharpcorner.com/article/understanding-properties-in-C-Sharp/) at C# Corner.

## Special Topic: C# with Docker

This week we will have a presentation about how you can build and run your C# code in a Docker container (on Linux or Mac). This content won't be on any exam, but if you're using a Mac or Linux machine or have experience with Docker, or want to use an alternative environment like Visual Studio Code or JetBrains Rider, this lecture may be of use to you!

## The new, override and virtual keywords

When a class inherits a parent class, the new class has the ability to override the code from the parent class. This is a core property of polymorphism - a derived class is syntactically and programmatically compatible with its parent class, and a piece of code that expects the parent class is able to work with an instance of the derived class without modification:

    public class ParentClass 
    {
      public void MyMethod()
      {
        Console.WriteLine("Parent Class method".);
      }
    }

    public class ChildClass : ParentClass
    {
      public void MyMethod() 
      {
        Console.WriteLine("Child Class method.");
      }
    }

    public class Program
    {
      public static void Main() 
      {

        ParentClass myInstance = new ChildClass();

        // Even though we defined the type of myInstance to be ParentClass, it is able
        // to reference an instance of ChildClass, since ChildClass inherits from
        // ParentClass.

      }
    }

The topic of this discussion is what happens when you have an instance of type `ChildClass`, but it is being referenced in code by a variable of type `ParentClass`.

If you use the above code and run `myInstance.MyMethod();`, you will see "Parent Class method". Why is this, when the method was defined in the child class?

The reason is that, by default, C# treats a method in a child class with the same name as one in the parent class as a `new` method. A `new` method on a child class is essentially ignored if the reference to the instance is of the type of the parent class. (You can, and should, explicitly define the method in the child class as `new` if you actually expect and want this behavior.)

To change this, there are two ways you can do it:

1. You can define the method in the parent class as `virtual`. In this case, any subclass deriving from the parent class with a method with the name signature as one in the parent class will implicitly *override* the method in the parent class. Even if the instance is of type `ParentClass`, calling its `MyMethod` method will call the *child* class's method.
2. You can define the method in the child class as `override`. This strategy does not require the parent class to indicate that it expects its method to be overriden, and allows the child class to explicitly declare it wants to override the parent class's method.

In the case of an overriden method or a method declared virtual in the parent, the child class can explicitly call the parent class's method by using the `base` keyword. This cannot be done from *outside* the class, but it can be done from the class itself if the class requires the functionality of the parent class in an overriden method.

Here is an example illustrating the various strategies:
    
    public class ParentClass
    {
        public void MyBaseMethod()
        {
            Console.WriteLine("Parent Class Base method.");
        }
        public void MyMethod()
        {
            Console.WriteLine("Parent Class method.");
        }
        public virtual void MyVirtualMethod()
        {
            Console.WriteLine("Parent Class virtual method.");
        }
    }

    public class ChildClass : ParentClass
    {
        public void MyBaseMethod()
        {
            Console.WriteLine("Child Class base method.");
        }
        public new void MyMethod()
        {
            Console.WriteLine("Child Class method.");
        }
        public override void MyVirtualMethod()
        {
            Console.WriteLine("Child Class virtual method.");
        }
        public void ParentBaseVirtualMethod()
        {
            base.MyVirtualMethod();
        }
    }

    public class Program
    {
        public static void Main()
        {

            ParentClass myParentInstance = new ChildClass();
            ChildClass myChildInstance = new ChildClass();

            myParentInstance.MyBaseMethod();    // prints "Parent Class base method" - because the parent
                                                // doesn't define it as virtual and the child doesn't define 
                                                // it as override, so the parent class method is called.

            myParentInstance.MyMethod();        // prints "Parent Class method" - because the child 
                                                // defines the method as new, so the parent's method 
                                                // is not overriden.

            myChildInstance.MyMethod();         // prints "Child Class method" - because the child
                                                // defines the method as new, so the parent's method
                                                // is not executed on the child class.

            myParentInstance.MyVirtualMethod(); // prints "Child Class virtual method" - because the parent
                                                // class defines the method as virtual, and thus the child's
                                                // implementation implicitly overrides the parent implementation.

            myChildInstance.ParentBaseVirtualMethod();
                                                // prints "Parent Class virtual method" - the method uses the
                                                // "base" keyword to call the parent's virtual method, even
                                                // though the child class has overriden the method.
        }
    }

An important use case of the `override` keyword is overriding the `ToString()` method that is present on all instances of all objects. THe `ToString()` method is defined on the `Object` class, which all classes in .NET implicitly inherit from. The `ToString()` method is defined in the `object` class as `virtual`; this means that your implementation of the `ToString()` method needs to be declared with `override`.

For your presentation, please cover:

* What are the `new`, `override` and `virtual` keywords? How can you define them?
* Give some examples of where you would use the keywords in creating a class and subclass. Please try to use an example other than the one included in this resource guide!
* The default behavior of overriding a method not declared as virtual (i.e. it is implied that the subclass method is `new`).

Sources to get you started (but please do seek out and use other sources as well!):

* [Polymorphism: Versioning with the Override and New keywords](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/versioning-with-the-override-and-new-keywords) and [Polymorphism: Knowing When to Use Override and New Keywords](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/knowing-when-to-use-override-and-new-keywords) at Microsoft.
* [override](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/override), [new](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/new-modifier) and [virtual](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/virtual) (C# Reference) at Microsoft.
* [Virtual Method in C#](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/virtual) at C# Corner.
* [Understanding virtual, override and new keywords in C#](https://www.dotnettricks.com/learn/csharp/understanding-virtual-override-and-new-keyword-in-csharp) at dotnettricks.com

## Access Modifiers

Throughout the code we've been reviewing so far, you have likely seen references to keywords like `public`, `private`, or `internal`. These keywords, along with `protected` and `file`, are known as accesss modifierse. Access modifiers control what other code is able to access the code or data behind the identifier.

The following is a summary of how access modifiers work when applied to the members of a class:

* `private` means that the method, property or variable is only accessible from code within the class itself. It is not possible for code which instantiates the class to directly access such methods or values.
* `public` means that the method, property or variable is accessible to any other code which is accessing an instance of the class. This is the typical access modifier for public API methods or properties.
* `internal` means that the method, property or variable is accessible only to other code in the same assembly or program. This modifier is common in a library of code, where other entities within the library need to access the method or value, but you do not want another codebase that references your library to be able to access those members.
* `protected` means that the method, property or variable is accessible only to the class itself *or to any classes that derive from it*. A `private` member is not directly accessible to subclasses; a `protected` member is.
* `file` is more rarely seen and means that the method, property or variable is accessible only to code contained within the same code file.

You can combine certain modifiers together for additional effect:

* `protected internal` means that the member is only accessible to any code within the current assembly *and* to classes that subclass the class, even if those subclasses are in other assemblies.
* `private protected` means that the member is only accessible to the class itself or to derived classes within the same assembly. 

You can also apply access modifiers to classes, interfaces and so on. In this case, the meaning may change slightly, but the basic definition is the same. For example, an `internal` class can only be used by code in the same assembly, and a `public` class is accessible and can be used by any code that references it.

For your presentation, please cover:

* The four main access modifiers (all except `file`), their basic definition and the effect they have on methods and other members of a class.
* How to use two extra combinations of the four modifiers to generate six total levels of accessibility.
* Try to come up with a simple scenario in which you might use each modifier.

Sources to get you started (but please do seek out and use other sources as well!):

* [Access Modifiers](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/access-modifiers) at Microsoft.
* [Accessibility Levels](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/accessibility-levels) at Microsoft.
* [C# Access Modifiers](https://www.w3schools.com/cs/cs_access_modifiers.php) at W3Schools.


## Advanced Topic: Sealed Classes and Nested Classes

These two topics are somewhat unrelated but each individual topic doesn't warrant an entire presentation, so they are combined into one presentation.

A **sealed class** is one that has been defined with the `sealed` keyword. A sealed class may not be inherited from. 

A **sealed method** is a class method that has been defined with the `sealed` keyword. A sealed method explicitly forbids subclasses from implementing any kind of override on that method. It stands to reason that a `sealed` method cannot be defined as `virtual` or `abstract`.

It is generally not a good idea to seal classes or methods unless not doing so has the potential for subclasses to significantly and detrimentally alter the behavior of the class. For example, if you have a method that performs some form of encryption algorithm, and the encryption algorithm has been validated and rigorously tested for security, allowing a subclass to override the encryption algorithm could introduce a security vulnerability. 

A **nested class** is a class that is defined within another class. It is a syntactic feature that lets you define a class as part of another class. This nested class passes down to inherited classes as well. Nested classes don't necessarily offer any extra functionality, but can be used to logically group classes that are only relevant within another class.  There is no other connection between an instance of a nested class and an instance of the class nesting that class; they are treqted as completely separate classes and instances from the perspective of the runtime.

For your presentation, please cover:

* What is a sealed class? Give an example of why you might use one. Try to come up with at least one other example than the one in this resource guide.
* What is a sealed method? Why might you use one?
* What is a nested class? Does the nested class add any extra functionality, or is it just a syntactic convenience for code readability?

Sources to get you started (but please do seek out and use other sources as well!):

* [sealed modifier](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/sealed) (C# Reference) at Microsoft.
* [Nested Classes](https://www.geeksforgeeks.org/nested-classes-in-c-sharp/) at GeeksForGeeks.

## Advanced Topic: Generics

A generic is a unique C# feature that lets you make a generic class or method that is able to handle any type, specified by other code that is instantiating the class or calling the method.

A very common use case of this, that we will be studying next week, is generic lists and similar data structures. In C#, you can define a `List` of any type; for example, you can define a list of decimal values with `List<decimal>` or a list of strings with `List<string>`. You can even define a list of class instances, such as `List<Course>`. The `List` class contains methods like `Add` or `Remove`, and these methods expect a parameter of the type of the list. On an instance of `List<string>`, the `Add` method needs to be provided with a `string`; on a `List<Course>`, the `Add` method needs an instance of the class `Course`.

To write a C# class that accepts a generic type, you use the `T` placeholder. You place the `T` inside of angle brackets (`<T>`) immediately following the name of the class or method. You can then use `T` as a parameter type on any method within the class, or on the method's parameters.

This is an extremely simple example of a `List`-style class that implements generics. This class has nowhere near as much functionality as the actual C# `List<T>` class, but it illustrates how generics work.


    public class SimpleTenItemList<T>
    {
        int arrayPointer = 0;
        T[] items = new T[10];

        public void Add(T item)
        {
            if (arrayPointer >= 10)
            {
                throw new InvalidOperationException("The list is full!");
            }
            this.items[this.arrayPointer] = item;
            this.arrayPointer++;
        }
        public T Get(int index)
        {
            return this.items[index];
        }

        public void Clear()
        {
            this.items = new T[10];
            this.arrayPointer = 0;
        }
    }

This class is a list with a fixed size of ten items. When code instantiates the list, it must provide the type along with the instantiation:

    public SimpleTenItemList<string> myList = new SimpleTenItemList<string>();

Notice how the parameter for the `Add` method and the return type for the `Get` method is `T`. This is the generic type that is given when the class is initialized. 

It's also possible to specify more than one generic type:

    public class SimpleTenItemPairs<T1, T2> ...

This is seen in the C# `Tuple` and `Dictionary` classes. The `Dictionary` class lets you specify the type of both the key and the value. The `Tuple` class lets you specify up to seven types for items in the object. 

You will likely not need to a generic class if you are a C# beginner, or if you are not writing library code that will be used by other programs or developers. However, knowing how these classes are written will help you understand how to use them.

In your presentation, please cover:

* What is a generic type: provide a concise definition of the concept.
* See if you can think of an example beyond a simple array or list where you might want to use a generic type. 

Sources to get you started (but please do seek out and use other sources as well!):

* [Generic classes and methods](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/generics) at Microsoft.
* [C# Generics](https://www.tutorialsteacher.com/csharp/csharp-generics) at TutorialsTeacher.
