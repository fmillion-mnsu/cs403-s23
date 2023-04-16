# Week 6 Resources and Presentation Guides

This week's presentations will discuss multithreading, parallelism, threading and similar concepts related to *parallel computing*. 

**Wednesday**:

- [Overview of Parallel Computing](#overview-of-parallel-computing)
- [C# Synchronization Primitives](#c-synchronization-primitives)
- [Basic C# Threading](#basic-c-threading)

**Friday**:

- [C# Concurrent Collections](#c-concurrent-collections)
- [C# Parallel Tasks](#c-parallel-tasks)
- [C# async programming](#c-async-programming)
- [Debugging Multithreaded Code](#debugging-multithreaded-code-in-c)

## Overview of Parallel Computing

Note: This discussion uses terms like "parallel computing", "multithreaded processing" etc. synonomously. I'm referring to the act of designing a computer program so that it can run multiple *threads* at the same time, a *thread* simply being a path of execution through a set of instructions.

### A short history of parallel computing on personal computers

Historically - until the mid-2000s - personal computers only consisted of a single CPU core. "The CPU" referred to this single processing core. Multi-processor systems are nothing new and have been in use in scientific and high-performance computing for decades. However, it was very uncommon to see a "home" computer with more than one CPU core.

However, in the mid-2000s, things started to change. First, Intel introduced their "Symmetric Multi-Threading" (SMT) technology, allowing a single CPU core to share its resources among two *threads* of execution. While this did not create *true* parallelism, it was the start of awareness of multi-threaded computing in typical home use scenarios. Not long after, both Intel and AMD released processors with dual cores on a single CPU. Combining this with Intel's SMT technology, today we have processors in personal computers with 8 or more cores, and high-end personal computers can have 16 or even 20 cores.

Initially, these multiple cores were touted as a way to run multiple programs simultaneously without loss of performance (although that ability is also largely dependent on other  factors such as memory and disk speed). However, it did not take long for the theories of multithreaded computing that were already well-established in computer science to start becoming relevant in personal computing. The idea was that a single piece of software, and indeed even a single operation, can take advantage of multiple cores to accelerate its performance. Processes such as digital media encoding, graphic design, software development and more all can benefit from being able to run multiple tasks at the same time. 

Today, we have all but reached the practical limit of single-core processing speed. Modern computer manufacturing technology is creating chips with electrical pathways only tens of atoms in width, and CPU speeds cannot get much faster without violating the laws of physics. How do we keep technology moving forward? By using *more than one* CPU core at a time! 

### How do we do parallel computing?

In its simplest form, you can do "parallel computing" by simply running more than one program at a time - much like was advertised as an early advantage of multicore CPUs for home use. However, where parallel computing starts to get interesting is when you use programs that are designed to work faster or better with multiple CPU cores. Doing this can often be much more challenging than it sounds - it's very often not as simple as "do two things at once".

As a very simple example, let's assume we have 1,000 sets of numbers. We want to get the maximum value of all of the averages of those sets. Without multithreading, we might do the following:

* One by one, calculate the average of each set and store those results in an array.
* Then calculate the maximum value contained in that array.

This sounds very reasonable and straightforward. Now, let's assume that it takes 1 second to compute the average of one set of numbers. If we have 1,000 sets of numbers, we can calculate that to do this on a single-core CPU, it would take us 1,000 seconds (plus a negligible amount of time to determine the maximum value of the results).

However, what if we had an 8-core CPU? What if we could compute the average of *eight* sets of numbers at once? We've now taken an operation that takes 1,000 seconds and reduced it to only 125 seconds! ... Right?

*Almost*. Note that we said that we store the results of each calculation in an array. If we have eight cores all trying to add values to one array, *what happens if two cores happen to finish a calculation at the exact same time, and both try to add a value to the array at the exact same moment?* It is easy to assume "both values will get added to the array", but in many cases this is not true; depending on the internal workings of how the language implements the array, you may end up with only one of the two values on the array, or you may even crash the program!

The way that we avoid this problem is by using various thread control objects, such as *locks*, *semaphores*, *mutexes* and so on. Other presentations will cover the details of these (and using them in C#), but for now let's just talk about the *lock* - a very common and relatively simple threading construct.

A *lock* is an object in your programming language that is used to *synchronize* the operation of multiple threads (cores). From a very high-level perspective, every time you want to make use of a resource that is shared across multiple threads, you first *acquire* the lock; then, you can do whatever it is you want to do, and you finally *release* the lock. Once the lock is acquired, any other thread trying to acquire the lock will have to *wait* until the first thread releases the lock. Think of it like "having the floor" in a large group of people having a conversation or debate - when you have the floor, you may speak and others are to listen; when you yield the floor to someone else, you now stay quiet while someone else takes the floor to speak.

There are many other constructs used to achieve maximum performance in threading, but the lock is the easiest to conceptualize when you're starting out with the idea of multithreaded, parallel programming. As we go on, you'll learn more about the theories of parallel programming, and you'll also learn how to implement these theories in C# to actually write multi-threaded code.

### A better analogy

Here's a way that you can start to conceptualize the idea of multithreaded / parallel programming. Suppose you want to prepare an extravagant meal for your family, roommates, friends, etc. The recipe is many steps long. For example:

* Preheat oven.
* Boil water.
* Mix several ingredients together in a bowl.
* Add the boiling water to the bowl and pour the mixture into a baking pan.
* Bake the mixture.
* Mix some more ingredients into another bowl.
* After the food has baked, top with the second mixture.

If *you* were a "single core", you would only be able to do these items in order. You would first preheat the oven. Once the oven has finished preheating, you would fill a pot and boil some water. You would then wait for the water to finish boiling. Once it has boiled, you would then mix the ingredients. By now the water has probably cooled off, so you bring it back to a boil and mix the water. Then you put the mixture in the oven and wait for it to cook. Finally, once the timer goes off, you remove the mixture from the oven, grab another bowl, mix the ingredients and pour the mixture over the food.

However, looking at these instructions, you can probably already see many things you could do simultaneously. You could simultaneously preheat the oven, boil the water and mix the ingredients in a bowl. Then, you'd wait for the water to finish boiling (if it hasn't already) and you'd mix the water in. Then, you'd wait for the oven to preheat (if it hasn't already) and then bake the mixture. While it's baking, you'd grab another bowl and mix the second set of ingredients. Finally, once the cooking is done, you'd add the topping.

Notice however that there are some *dependencies* that pop out in the instructions. For example, you can't add the water to the ingredient mixture until it is boiling. You can't start cooking the mixture until the oven is preheated. And so on. 

In a multithreaded program, you could implement these dependencies via locks. When the oven starts preheating, you would acquire a lock on the oven object. The oven would not release the lock until it has preheated. Same goes for the boiling water and the cooking itself!

Here's some pseudocode that will help this make sense:

    void preheatOven() 
    {
        o.Lock.Acquire();
        o.StartHeating();
        while (o.Temperature < 350) 
        {
            // wait.
        }
        o.StopHeating();
        o.Lock.Release();
    }

    void boilWater()
    {
        sp.Lock.Acquire();
        sp.TurnOnHeat();
        while (!sp.Contents.Boiling)
        {
            // wait.
        }
        sp.TurnOffHeat();
        sp.Lock.Release();
    }
    
    void CookDinner()
    {
        Oven o = new Oven();
        Saucepan sp = new Saucepan();
        Bowl b = new Bowl();

        using a Thread
        {
            o.preheatOven();
        }

        using a Thread
        {
            sp.BoilWater();
        }

        b.AddIngredients();
        b.MixIngredients();

        sp.Lock.Acquire(); // this will PAUSE execution if the water isn't boiling yet!
        sp.Contents.AddTo(b);
        sp.Lock.Release();

        o.Lock.Acquire(); // this will PAUSE execution if the oven isn't preheated yet!
        o.Insert(b);
        o.Cook();

        // ...
    }

This code is C# style, but is far from "valid" C#. However, in the upcoming presentations we'll see this sort of scenario actually written in C#.

Naturally, C# isn't going to be used to cook your dinner this way (unless perhaps you design a cooking robot that runs C# code!). However, the same concept comes into play in programming many times - there are many things can be done simultaneously, but the dependencies can create problems if they are not accounted for.

In your presentation, please cover:

- What is parallel computing? Why does it matter?
- Why can't we just "do two things at once"? Give one example of where something could go wrong if we don't implement proper handling of multithreaded code.
- Very brief overview of how a lock object helps the problem of concurrency - the next presentation will cover threading objects in more detail
- Try to come up with your own real-world analogy to explain parallel execution and multithreading, similar to the cooking example here.

Your presentation doesn't need to include any demos or code.

Sources to get you started (but please do seek out and use other sources as well!):

- [Introduction to Parallel Computing](https://www.geeksforgeeks.org/introduction-to-parallel-computing/#) at GeeksForGeeks. Note that the kind of parallelism we are discussing is their third point - *task-level* parallelism. 
- [What is Parallel Programming and Multithreading?](https://www.perforce.com/blog/qac/multithreading-parallel-programming-c-cpp). This article talks about C/C++; don't worry about the language specifics, focus on the theory and concept.

## C# Synchronization Primitives

There are three main types of objects used for *thread synchronization*. These objects are all very closely related and perform similar functions, but differ in their scope. A program or a piece of code that fully and appropriately uses thread synchronization is said to be *thread-safe*. You may see this term used in reference to programming libraries - a developer might tell you that their library is *thread-safe*, meaning that you generally don't need to worry about using locks and similar objects when using the library. (There are some exceptions to this however!)

The three main types are as follows:

* **Lock**: A simple object that has two methods: `Acquire` and `Release`. When some code *acquires* the lock, it has exclusive access to it - any other code that tries to simultaneously acquire the lock will either have to wait for the lock to be *released*, or alternatively acquiring the lock can simply raise an exception that the other code needs to handle appropriately.
* **Semaphore**: A semaphore is similar to a lock, except that it can be acquired by more than one thread of code at once. A semaphore with a maximum count of one behaves essentially the same as a lock. With a semaphore, each call to `Acquire` will increment a counter by one, while each call to `Release` will decrement the counter. If the counter already equals the maximum allowed acquires for the semaphore, calls to `Acquire` will behave just like a lock that has already been acquired - that is, it will either fail or it will wait for one other thread to release the semaphore.
* **Mutex** (mutual exclusion): A mutex is also similar to a lock, however it applies *system-wide*, rather than just to your own application. In C#, your application runs within an *application domain*, and all threads your application runs exist within that domain. However, other applications create their own domains, and thus your code can't control what *other* programs might do on the system's threads. Mutexes are useful when your code is accessing resources outside of your system, such as network resources or hardware devices. Mutexes are not as commonly seen in managed C# code, but they are extremely common in software such as device drivers, where it is important to ensure that multiple threads of code are not simultaneously trying to work with a hardware device. (Imagine if two games both tried to tell your graphics card to draw on-screen at the same time - it would look pretty ugly! Mutexes would allow the system to pause the rendering engine for one game while another is drawing, so that the drawing commands don't get overlapped and confused.)

C# also offers one more synchronation class, the **EventWaitHandle**: This type is essentially the opposite of a lock. In this case, the object starts in the "locked" or "waiting state", and trying to acquire it will immediately pause the thread. However, another thread can then *signal* the EventWaitHandle object, which will release the thread that is waiting on it. The name of the type, "EventWait", describes its functionality - a thread *waits* for an *event* signalled by another thread. There are subclass versions of the `EventWaitHandle` class: the `AutoResetEvent` class and the `ManualResetEvent` class. These two subclasses simply define the behavior after the event is signalled - a `ManualResetEvent` object must first be *reset* prior to being signalled again, while an `AutoResetEvent` object automatically resets itself, ready to be signalled again as necessary.

C# has specific classes for `Semaphore`, `Mutex` and `EventWaitHandle` (and its subclasses). However, C# does not have an explicit `Lock` object type. Instead, you use a language block known as a `lock` block, and the object you lock against can actually be *any* C# object instance. However, for many reasons it is best (and safest!) to simply declare an object of type `object`. (Recall that every single class in C#, even `string`, derives from the `object` class. You absolutely can instantiate a plain `object` - it won't be very useful, but in this case it is quite useful since we won't have to worry that using the `object` for locking will potentially interfere with any other behavior of the class.) You can also declare the object as `readonly` (essentially a `const` object), so that nothing else can interfere with the object. 

Since threading can be quite a complex topic, you don't need to present any specific code for *how* to do threading in C# - that will be in another presentation. Your presentation's goal is to explain the various types of thread synchronization available in C#. Additionally, you should try looking into threading *basics* in another language (e.g. Python). Don't delve too deeply into the topic - it can get complex very quickly. But, for example, Python *Does* have an explicit `Lock` class that you use for locking, while C# does not. Python also has `Semaphore` objects but lacks system-wide `Mutex` objects and has no `EventWaitHandle` equivalent. Another example is JavaScript, which implements the concept of *web workers* - similar to the `Thread` in C#, i.e. tasks that run in the background.

In your presentation, please cover:

- What are the four core types of threading synchronzation classes or objects in C#? Provide a brief description of each.
- Compare C# to another programming language's thread synchronization primitives, such as Python or Java.

Sources to get you started (but please do seek out and use other sources as well!):

- [Parallel Programming in .NET](https://learn.microsoft.com/en-us/dotnet/standard/parallel-programming/) at Microsoft.
- [lock statement](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/lock), [Semaphore](https://learn.microsoft.com/en-us/dotnet/api/system.threading.semaphore?view=net-7.0), [Mutex](https://learn.microsoft.com/en-us/dotnet/api/system.threading.mutex?view=net-7.0) and [EventWaitHandle](https://learn.microsoft.com/en-us/dotnet/api/system.threading.eventwaithandle?view=net-7.0) in the C# Reference.
- For other languages: [Python threading](https://docs.python.org/3/library/threading.html), [Java Threads](https://www.w3schools.com/java/java_threads.asp) or [JavaScript Web Workers](https://medium.com/techtrument/multithreading-javascript-46156179cf9a).

## Basic C# Threading

We've now learned one of the most important aspects of parallel programming - ensuring that the various threads are synchronized, and that resources that are sensitive to having multiple threads messing with them at the same time are protected using locks and similar classes. Let's actually implement some threading in C#!

The first thing we have to learn is how to actually get C# to run more than one thread of code at the same time. In C#, there are many ways to do this, but all of them ultimately use the `System.Threading.Thread` class in some fashion. The `Thread` class represents a method that executes on its own thread - in other words, separately from the main execution thread.  Therefore, that's where we'll start - we will create `Thread` objects and run them. We will start by simply having the `Thread` objects pause for some random amount of time. While this isn't a good demonstration of how to *speed up performance* of code (we'll see that shortly), it illustrates the idea of multiple things happening at once.

Creating a thread object is quite straightforward. First, add a `using` statement for `System.Threading`, and then create an instance of the `Thread` class. The initializer for the `Thread` instance takes an instance of a class known as `ThreadStart`. The `ThreadStart` class is essentially a combination of a pointer to a method along with any other parameters needed to start the method, such as any parmaeters it requires. 

The simplest kind of method to use for threading is one that takes no parameters and returns no value. Starting a thread with such a method is very straightforward, so we'll start there.

Here is some sample code that will start multiple threads at the same time on your system. 

    using System.Threading;

    namespace ThreadPlayground
    {
        internal class Program
        {

            // This is the lock object - it will be used to ensure only one
            // thread can write to the console at a time.
            public static readonly object ConsoleLock = new object();

            static void ThreadMethod()
            {
                Random rnd = new Random();
                int millisecondsToDelay = rnd.Next(1000, 5000);

                lock (ConsoleLock)
                {
                    Console.WriteLine($"This thread will pause for {millisecondsToDelay} milliseconds.");
                }

                Thread.Sleep(millisecondsToDelay);

                lock (ConsoleLock)
                {
                    Console.WriteLine($"Thread has paused for {millisecondsToDelay} milliseconds.");
                }

            }

            static void Main(string[] args)
            {

                List<Thread> threads = new List<Thread>();

                // Create 10 threads
                for (int i = 0; i < 10; i++)
                {
                    threads.Add(new Thread(new ThreadStart(ThreadMethod)));
                }

                // Start all 10 threads - Note the "cool" use of LINQ here!
                threads.ForEach(x => x.Start());

                // The Join() method on a thread will pause until the given thread
                // finishes. Since we want to wait for *all* threads to finish, we
                // can simply Join() on each thread sequentially. This works because
                // a thread that has already finished will not pause at all on Join().
                threads.ForEach(x => x.Join());

            }
        }
    }

This code makes use of a `lock`, and as a bonus it even uses a List (an enumerable) and a bit of LINQ. :-)

When you run the program, you'll notice that all of the threads "wait" at the same time. The entire program will only take as long as the slowest thread takes to run. While all we're doing is `Thread.Sleep()`, imagine if the thread was performing a very compute-intensive process. To illustrate, look at this code, which would be the non-threaded equivalent of the above:


    namespace ThreadPlayground
    {
        internal class Program
        {

            static void Main(string[] args)
            {
                Random rnd = new Random();

                for (int i = 0; i < 10; i++)
                {
                    int millisecondsToDelay = rnd.Next(1000, 5000);

                    Console.WriteLine($"This thread will pause for {millisecondsToDelay} milliseconds.");

                    Thread.Sleep(millisecondsToDelay);

                    Console.WriteLine($"Thread has paused for {millisecondsToDelay} milliseconds.");
                }
            }
        }
    }

This code is *simpler* and *shorter*, but try running it - each "thread" will simply run sequentially, and the total time taken will be the *sum* of all of the threads' wait times!

Now that we've seen how threading can speed up computation, let's take a more literal example. In a previous discussion we mentioned the idea of sorting lists of millions of items. Let's try actually implementing that in C#. First, we'll implement it without threading - we want to find the maximum value in a list of averages from 10 lists of 50,000,000 numbers each. (We're using larger numbers because modern computers can actually do this pretty fast - you can also change the numbers around if you want.)

    using System.Threading;

    namespace ThreadPlayground
    {
        internal class Program
        {

            static void Main(string[] args)
            {

                DateTime startedTime = DateTime.UtcNow;

                List<int> Averages = new List<int>();

                // 10 iterations
                for (int i = 0; i < 100; i++)
                {
                    Random rnd = new Random();
                    List<int> Randoms = Enumerable
                        .Range(0, 50000000)
                        .Select(x => rnd.Next(0,int.MaxValue))
                        .ToList();
                    int avg = (int)Randoms.Average();
                    Averages.Add(avg);
                }

                int max = Averages.Max();

                Console.WriteLine($"Largest average: {max}");

                TimeSpan timeTaken = DateTime.UtcNow - startedTime;

                Console.WriteLine($"Time taken: {timeTaken.TotalMilliseconds/1000:0.##} seconds");
            }
        }
    }

On my computer, this operation took, on average, about 9.4 seconds.

Now, let's rewrite the code to make it threaded:

    using System.Threading;

    namespace ThreadPlayground
    {
        internal class Program
        {

            static List<int> Averages = new List<int>();
            public static readonly object LockObject = new object();

            static void ThreadMethod()
            {
                Random rnd = new Random();
                List<int> Randoms = Enumerable
                    .Range(0, 50000000)
                    .Select(x => rnd.Next(0, int.MaxValue))
                    .ToList();
                int avg = (int)Randoms.Average();

                lock (LockObject)
                {
                    Averages.Add(avg);
                }
            }
            static void Main(string[] args)
            {

                DateTime startedTime = DateTime.UtcNow;

                List<Thread> threads = new List<Thread>();

                // 10 threads
                for (int i = 0; i < 10; i++)
                {
                    threads.Add(new Thread(new ThreadStart(ThreadMethod)));
                }

                // Start all threads - Note the "cool" use of LINQ here!
                threads.ForEach(x => x.Start());

                // Wait for threads
                threads.ForEach(x => x.Join());

                int max = Averages.Max();

                Console.WriteLine($"Largest average: {max}");

                TimeSpan timeTaken = DateTime.UtcNow - startedTime;

                Console.WriteLine($"Time taken: {timeTaken.TotalMilliseconds/1000:0.##} seconds");
            }
        }
    }

When I ran this code, it took my computer about 6.4 seconds. Definitely faster.

*But Wait!* Why isn't it *much* faster? I have four cores in my system - I should have seen about 2 seconds or so, right?

Ah yes, I should. There is a reason for that, and it's that I basically tried to *overuse* my system. If you have multiple processor cores, and you run one thread per core, things will run along extremely quickly. However, if you try to run more *threads* than you have *cores*, you will see significant drops in performance. I tried changing the code to do 500 threads with 1,000,000 numbers each, and the program actually took *over 17 seconds* to run!

Before multi-core processors, we still had threads. Threading is what allowed you to run multiple programs at once on your computer, even without multiple cores. Even in 2002, you could play an MP3 song on your computer while you wrote a document in Word, or you could download a file from the Internet while reading a web page at the same time. This worked because of threading. However, if we don't have as many *cores* as we have *threads*, the CPU cores must regularly perform what is known as a *context switch*. In a sense, it basically stores the entire state of the processor core to memory, then switches to another thread by loading in a different state. To provide the "illusion" of multiple processes running simultaneously on one core, this can happen hundreds of times per second. But each context switch takes *time*. In the example where I run 500 threads on a four-core CPU, I'm asking each CPU core to run *125 threads* at a time. (And that says nothing for all of the other threads that may already be running on my system from Windows and other applications, and indeed Visual Studio itself!) This means that the CPU is spending a *lot* of time doing context switches, so much so in fact that the total time taken to do 500 simultaneous threads is actually *more* than just doing the operations sequentially on one thread.

We could write our own code to limit the number of threads to the number of CPU cores, but there's a better way. On Friday, we'll hear about C#'s `Parallel` class - it's basically a "thread runner" that handles things like this for us. We get to specify how many threads should run in parallel (and we can query the system to determine how many CPU cores we have to do that), and we can even simplify adding all those results to the `Averages` list. Stay tuned!

In your presentation, please cover:

- A basic sample of threading. You can use the example in this section, or you can come up with your own. However, try playing around with the numbers a bit and see how the number of threads affects the runtime. Make larger arrays of numbers and run fewer threads, and do the opposite. Compare this to the same changes when running the program without threading.
- Explain how the code works at a high level. Note the `lock` statement and how it is using the `LockObject` object we created. Also note how there is a separate method that is called for each thread. (You can also point out that we're using LINQ and collections here as well - a nice back-reference to our previous weeks' content!)

Sources to get you started (but please do seek out and use other sources as well!):

- [Threads in C#](https://www.c-sharpcorner.com/article/Threads-in-CSharp/)
- [The C# lock statement](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/lock).
- [Why too many threads hurts performance?](https://www.codeguru.com/cplusplus/why-too-many-threads-hurts-performance-and-what-to-do-about-it/)

## C# Concurrent Collections

coming soon

## C# Parallel Tasks and LINQ

coming soon

## C# async programming

## Debugging Multithreaded Code in C#

coming soon
