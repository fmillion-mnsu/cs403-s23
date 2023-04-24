# Week 6 Resources and Presentation Guides

This week's presentations will discuss multithreading, parallelism, threading and similar concepts related to *parallel computing*. 

**Wednesday**:

- [Overview of Parallel Computing](#overview-of-parallel-computing)
- [C# Synchronization Primitives](#c-synchronization-primitives)
- [Basic C# Threading](#basic-c-threading)
- [Thread Pools](#thread-pools)

**Friday**:

- [Parallel Tasks](#c-parallel-tasks)
- [Concurrent Collections](#c-concurrent-collections)
- [Parallel LINQ](#c-parallel-linq-and-wrapping-up-multithreading)
- [C# async programming](#c-async-programming)

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

There's actually another type of "multithreaded" programming that is closely related to threading, which is known as *asynchronous programming*. This strategy is not concerned with maximizing *performance*, but rather it is concerned with maximizing *responsiveness and availability*. Async programming can seem just as complex as threading and there will be a separate presentation on Friday to cover it.

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

There is a reason for that, and it's that I basically tried to *overuse* my system. If you have multiple processor cores, and you run one thread per core, things will run along extremely quickly. However, if you try to run more *threads* than you have *cores*, you will see significant drops in performance. I tried changing the code to do 500 threads with 1,000,000 numbers each, and the program actually took *over 17 seconds* to run!

Before multi-core processors, we still had threads. Threading is what allowed you to run multiple programs at once on your computer, even without multiple cores. Even in 2002, you could play an MP3 song on your computer while you wrote a document in Word, or you could download a file from the Internet while reading a web page at the same time. This worked because of threading. However, if we don't have as many *cores* as we have *threads*, the CPU cores must regularly perform what is known as a *context switch*. In a sense, it basically stores the entire state of the processor core to memory, then switches to another thread by loading in a different state. To provide the "illusion" of multiple processes running simultaneously on one core, this can happen hundreds of times per second. But each context switch takes *time*. In the example where I run 500 threads on a four-core CPU, I'm asking each CPU core to run *125 threads* at a time. (And that says nothing for all of the other threads that may already be running on my system from Windows and other applications, and indeed Visual Studio itself!) This means that the CPU is spending a *lot* of time doing context switches, so much so in fact that the total time taken to do 500 simultaneous threads is actually *more* than just doing the operations sequentially on one thread.

We could write our own code to limit the number of threads to the number of CPU cores, but there's a better way. Another presentation will cover the `ThreadPool`, which lets the .NET framework handle putting various tasks on threads. You can specify a number of threads (typically the number of CPU cores or threads in your computer), provide the jobs you want to run at any time, and let .NET handle actually scheduling and executing the tasks on the threads. More on this to come!

In your presentation, please cover:

- A basic sample of threading. You can use the example in this section, or you can come up with your own. However, try playing around with the numbers a bit and see how the number of threads affects the runtime. Make larger arrays of numbers and run fewer threads, and do the opposite. Compare this to the same changes when running the program without threading.
- Explain how the code works at a high level. Note the `lock` statement and how it is using the `LockObject` object we created. Also note how there is a separate method that is called for each thread. (You can also point out that we're using LINQ and collections here as well - a nice back-reference to our previous weeks' content!)

Sources to get you started (but please do seek out and use other sources as well!):

- [Threads in C#](https://www.c-sharpcorner.com/article/Threads-in-CSharp/)
- [The C# lock statement](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/lock).
- [Why too many threads hurts performance?](https://www.codeguru.com/cplusplus/why-too-many-threads-hurts-performance-and-what-to-do-about-it/)

## Thread Pools

In a previous presentation, we saw how you can achieve running multiple tasks at the same time within your program. However, we saw that if you try to "overprovision" your system - trying to run *too many* threads at once - you can actually see a *drop* in performance. This is because, as we mentioned, the act of *switching* threads on a single CPU core takes time, and putting too many threads on one core actually slows down the overall execution time since the core is taking so much time to switch threads.

Now, we'll learn how you can alleviate this problem by controlling how many threads your code runs on. You can certainly write your own code to track threads (and in other languages, you may have to do just that), but the .NET framework offers us a nice convenient way to manage threads and tasks.

Before we cover the .NET `ThreadPool` though, and in the vain of this course, let's first discuss the theory behind how you would write your own library to manage threads.

### A library for thread management

In our previous example, we had 1,000 tasks to run, but on my system we only have eight cores to run them on. In that example, we simply spawned 1,000 threads and started them all at once. We found that the process took significantly *longer* than simply running the code on a single thread, one item at a time, sequentially. The first thing we need to do is think about how we might control the threads, so that only eight threads are actually running at one time.

Here is some pseudo-code that explains the basic principle of doing this:

    Assume n = number of thread we want to run simultaneously.

    Create n threads.

    For each task we want to run,
        add the task to a list.
    
    For each task in the list...
        While true:
            Look for a free thread that isn't running any code.
            If one is found, 
                put the task on that thread,
                start the thread, and
                continue the foreach loop.
            Else,
                Wait 100 milliseconds.
            Continue the while loop.
    
    For each thread,
        wait for the thread to finish.
    
What's happening here is that we create only enough thread objects to run the number of threads we want to run. Then we iterate over all of the tasks we need to run. Each time, we look through our threads to see if there is a thread that is not currently executing any code; if so, we tell that thread to start executing the task and then go on to the next task. However, if all threads are taken, we wait a short time and then we again look for a free thread. This keeps happening until a free thread is found. Ultimately, if you follow the logic, you'll see that by the time we reach the end of the code, all the threads have finished running.

You certainly could write this code yourself in C#. However, we're going to use a built-in .NET framework class known as the `ThreadPool` to accomplish the same effect without having to "re-invent the wheel".

### The `ThreadPool` class

The `ThreadPool` class is a way for us to run many threads at the same time and to not have to deal with scheduling execution on the threads manually. `ThreadPool` is a *global static* class - we do not need to instantiate it. It's sort of like `Console` - it's always available as long as you import its namespace.

First, we tell `ThreadPool` how many threads we want it to run:

    // Tell the system to only allow 8 threads to run.
    // The second parameter is for I/O threads, we can just set that to 8 since we're not using much I/O.
    ThreadPool.SetMaxThreads(8, 8);

Then we can simply queue work items on the thread pool directly. The thread pool is basically always "running" - as soon as it gets tasks to do, it starts doing them.

    int threadCount = 1000;
    for (int i = 0; i < threadCount; i++)
    {
        ThreadPool.QueueUserWorkItem(ThreadMethod);
    }

One caveat is that the `ThreadPool` does not have any built-in method for tracking the threads themselves - that is, there's no "wait for all threads" functionality. So we need to provide that ourselves. Luckily, we can use another threading primitive - the `CountdownEvent` - to achieve this for us. A `CountdownEvent` is a thread-safe object that implements a countdown - you set it to an initial value and it is decremented each time it is "signalled". Once the countdown event reaches 0, it "unlocks" - and you can call a method that waits for this unlock to occur.

Here is the code from the previous presentation, but updated to use a `ThreadPool`:

    internal class Program
    {

        static List<int> Averages = new List<int>();
        public static readonly object LockObject = new object();

        public static CountdownEvent ce;

        static void ThreadMethod(object? stateInfo)
        {
            Random rnd = new Random();
            List<int> Randoms = Enumerable
                .Range(0, 500000)
                .Select(x => rnd.Next(0, int.MaxValue))
                .ToList();
            int avg = (int)Randoms.Average();

            lock (LockObject)
            {
                Averages.Add(avg);
            }

            ce.Signal(); // decrement the countdown
        }
        static void Main(string[] args)
        {

            ConcurrentDictionary<int, int> testDict = new ConcurrentDictionary<int, int>();

            testDict.AddOrUpdate(1, x => 1, (x, y) => x + 1);

            DateTime startedTime = DateTime.UtcNow;

            // Tell the system to only allow 8 threads to run.
            // The second parameter is for I/O threads, we can just set that to 8 since we're not using much I/O.
            ThreadPool.SetMaxThreads(4, 4);

            // number of threads
            int threadCount = 1000;
            ce = new CountdownEvent(threadCount);
            for (int i = 0; i < threadCount; i++)
            {
                ThreadPool.QueueUserWorkItem(ThreadMethod);
            }

            ce.Wait(); // wait for threads to finish

            int max = Averages.Max();

            Console.WriteLine($"Largest average: {max}");

            TimeSpan timeTaken = DateTime.UtcNow - startedTime;

            Console.WriteLine($"Time taken: {timeTaken.TotalMilliseconds/1000:0.##} seconds");
        }
    }

On my system, this code ran in under 3 seconds. Mission accomplished - we've accelerated our code by using multiple threads!

You may wonder why we don't get a one-to-one increase in performance - why doesn't running the code on eight threads result in an 8x speedup? The two main reasons are thread context switching (like we already discussed) and thread synchronization (i.e. `lock`s and similar.) Even though we are now only running one thread per core, we are still not the only application running on the system - we're still competing with all of the other programs running on the system, and that involves thread context switches! This is unavoidable, and the truth is that you will rarely ever see a 100% per-thread performance improvement.

In your presentation, please cover:

Sources to get you started (but please do seek out and use other sources as well!):

## C# Parallel Tasks

We've looked at two ways to run multiple pieces of code at the same time. Now we'll see yet another way to accomplish it. C# has a `Parallel` class that lets us run multiple operations in...you guessed it...parallel!

The advantage of the `Parallel` class is that we don't need to worry about tracking the completion. If we make a call to a method in the `Parallel` class, it will kick off the threads, wait for them to complete, and then the method will return. So we don't need to use a separate object such as a `CountdownEvent` to track the events and their completion.

Here's the code:

    internal class Program
    {

        static readonly List<int> Averages = new();
        public static readonly object LockObject = new();

        static void ThreadMethod()
        {
            Random rnd = new();
            List<int> Randoms = Enumerable
                .Range(0, 500000)
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

            // number of threads
            int threadCount = 1000;

            Parallel.For(0, threadCount, i => ThreadMethod());

            int max = Averages.Max();

            Console.WriteLine($"Largest average: {max}");

            TimeSpan timeTaken = DateTime.UtcNow - startedTime;

            Console.WriteLine($"Time taken: {timeTaken.TotalMilliseconds/1000:0.##} seconds");
        }
    }

The key statement in this code is the `Parallel.For` statement. The `For` statement accepts three parameters - the starting value for the `For` iteration, the ending value (exclusive), and a method to execute. The method is actually a *delegate*, a topic that we'll look at more in Week 7, but for now, it is sufficient to understand that it is a method that is being passed as a parameter to a method, similar to how we passed methods in LINQ. The `i` parameter is the value in the `For` loop - since we don't need it, we simply call the `ThreadMethod` inside the lambda function.

What's nice about the `Parallel` class is that the .NET Framework can optimize how many threads are running and work to improve the performance. In the previous section, where we used a `ThreadPool`, my system ran the code in about 2.9 seconds. However, with `Parallel`, the code runs in only 2.1 seconds. This seems like a small improvement, but it is only 72% of the speed of the `ThreadPool` strategy!

`Parallel` also offers a `ForEach` statement. This works similarly to `Parallel.For`, but it accepts an iterable and passes one item from that iterable into the method each time a thread is started. Using `Parallel.ForEach`, you can mutltithread bulk processing of items in a collection.

In your presentation, please cover:

Sources to get you started (but please do seek out and use other sources as well!):

## C# Concurrent Collections

We'll take a brief break from working on our little multithreaded code example to talk about another aspect of threaded programming in C#.

In the code example we've been working with, we use a `lock` statement to protect accesses to a `List`. This is because, with multiple entities accessing the list, we could run into a situation where multiple threads are simultaneously trying to access the `List`. Since this is a rather common scenario, C# provides some classes that can alleviate the problem for us, by automatically wrapping calls that manipulate the collection in `lock` statements internally (i.e. you don't have to write `lock` blocks, because the class's own code includes them wherever the data in the collection is changed.)

The `System.Collections.Concurrent` namespace includes a handful of generic collection types that support thread-safety:

* `ConcurrentBag` - roughly the equivalent of a `List` - an unordered collection of objects. 
* `ConcurrentDictionary` - equivalent of a `Dictionary`.
* `ConcurrentStack` and `ConcurrentQueue` - implementations of a *LIFO* and *FIFO* list. A Stack accepts items and returns them in reverse order (imagine physically stacking objects vertically), and a FIFO accepts items and returns them in order (imagine an assembly line).

Let's talk about one scenario in which we would want to use the thread safe versions of a collection - in this case, a `Dictionary`. In a typical dictionary, a common pattern of access might be to first check whether a key exists in the dictionary; if not, we add an item to the dictionary using that key, but if so, we can react differently - change the key before we add, present an error to the user, etc. 

This code might look like this:

    if (testDict.ContainsKey(1))
    {
        Console.WriteLine("Entry already exists.");
    }
    else
    {
        testDict.Add(1, "newValue");
    }

This looks fine on the surface, and indeed works fine when you're not dealing with multithreasding. However, in a multithreaded scenario, *imagine what might happen if another thread just so happened to add a value with the key `1` to the dictionary immediately after the `testDict.ContainsKey` if statement runs, but* ***before*** *the `testDict.Add` method is called!*

In that scenario, an error would occur, and it wouldn't even be immediately obvious why. You checked to see if the key existed, it didn't, so you tried to add it...and suddenly it does already exist!

This is a perfect scenario in which we need to use thread safety. One simple method is to wrap the entire block above in a `lock` statement. However, since scenarios like this are quite common, C# also offers us the `ConcurrentDictionary` class. It subclasses `Dictionary`, so it can mostly be used exactly like a dictionary. However, it offers thread-safe methods for doing operations like the above one. 

For example, `ConcurrentDictionary` adds an `AddOrUpdate` method. Using that method, you can specify two things - a value for if the key does not exist, and a lambda function that will be called if the value already does exist. For example:

    myConcurrentDict.AddOrUpdate(1,"mewValue", (key, value) => { Console.WriteLine("Entry already exists."); return value; });

In this case we print to the console and return the same value, so the result would be the same as above. However, we could also use this method to store an *incrementing* value. 

    // Non-thread-safe version
    if (testDict.ContainsKey(1))
    {
        testDict[1] = (testDict[1] + 1);
    }
    else
    {
        testDict.Add(1, 1);
    }

    // Thread-safe version
    testDict.AddOrUpdate(1, 1, (k, v) => k + 1);

Also note that the thread-safe code can be written even more concisely!

These collection objects implement an interface known as `IProducerConsumerCollection`. This is an access paradigm in which different threads *produce* messages and data, and other threads *consume* the data. This is covered in more detail when we talk about async programming. A simple example of using the produer/consumer pattern is in the online resources below; it's essentially an implementation of a `Queue`, where messages are posted by one thread and read by another. We won't cover the actual implementation of this in C#, but you should mention the consumer/producer pattern conceptually.

In our next presentation we'll return to optimizing and improving our multithreaded code sample -- by using LINQ!

In your presentation, please cover:

- The main concurrent collection classes in C#
- One scenario where you'd use them - you can use the dictionary scenario as described here.
- A high-level description of the producer/consumer pattern. (No code required, but if you do write and demo some code, even better!)

Sources to get you started (but please do seek out and use other sources as well!):

- [Thread-safe collections](https://learn.microsoft.com/en-us/dotnet/standard/collections/thread-safe/) at Microsoft.
- [System.Collections.Concurrent namespace](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent?view=net-8.0) in C# Reference.
- [Concurrent collections in C#](https://code-maze.com/csharp-concurrent-collections/) at Code Maze.

## C# Parallel LINQ and Wrapping Up Multithreading

Now we'll connect what we've learned about parallel programming in C# to what we learned about LINQ and querying. The nice part of this discussion is that there's actually very little you need to learn! Once you've worked with LINQ, you already know most of what you need to know to use Parallel LINQ! This is one case where the .NET Framework handles a lot of the dirty work for you - you don't have to use the `Parallel` class directly (although PLINQ is using it in the background) and you don't need to do anything other than slightly modify your LINQ queries to take advantage of parallelism!

Imagine we wanted to convert our simple program that calculates a running average to use LINQ. This would be relatively straightforward, since LINQ offers a nice way to generate a list of numbers. It's basically the same as Python's `range` statement:

    Enumerable.Range(0, 1000); // creates an enumerable object that generates numbers from 0 up to 999.

This method only generates an *enumerator* - essentially a class instance that generates the numbers as you request them. The numbers themselves aren't actually stored in memory - only the current number is stored. However, we can convert this to an actual list of numbers stored in memory by simply using `ToList` or `ToArray`:

    int[] numbers = Enumerable.Range(0, 1000).ToArray();

    // or

    List<int> numbers = Enumerable.Range(0, 1000).ToList();

In our code sample, we don't actually care about the number itself, but a list of 1,000 numbers can serve as an enumerable object that our program can use to know that it needs to run the thread 1,000 times. 

Here's how we can rework the code from the previous example to use LINQ:

    internal class Program
    {

        static readonly List<int> Averages = new();
        public static readonly object LockObject = new();

        static void ThreadMethod()
        {
            Random rnd = new();
            List<int> Randoms = Enumerable
                .Range(0, 500000)
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

            // number of threads
            int threadCount = 1000;

            // Use Enumerable.Range to make a list of 1,000 numbers
            int[] instances = Enumerable.Range(0, threadCount).ToArray();

            DateTime startedTime = DateTime.UtcNow;

            // A little LINQ "abuse" that causes the 'void' method to run for each
            // item in the list.
            var junk = instances.Select(x =>
            {
                ThreadMethod();
                return 0;
            }).ToArray(); // we need to use ToArray() to force LINQ to actually evaluate
                          // the array - due to lazy loading, if we don't do this, the
                          // ThreadMethod will never be called.

            int max = Averages.Max();

            Console.WriteLine($"Largest average: {max}");

            TimeSpan timeTaken = DateTime.UtcNow - startedTime;

            Console.WriteLine($"Time taken: {timeTaken.TotalMilliseconds/1000:0.##} seconds");
        }
    }

The changes here are that we are using LINQ to generate a list of 1,000 objects, and that we are then using a trick to design a LINQ select query that will actually run `ThreadMethod()` for each item in the list.

Actually, let's see if we can do better. Maybe we don't need to use a `lock` at all?...


    internal class Program
    {

        static int ThreadMethod()
        {
            Random rnd = new();
            List<int> Randoms = Enumerable
                .Range(0, 500000)
                .Select(x => rnd.Next(0, int.MaxValue))
                .ToList();
            int avg = (int)Randoms.Average();

            return avg;
        }
        static void Main(string[] args)
        {

            // number of threads
            int threadCount = 1000;

            // Use Enumerable.Range to make a list of 1,000 numbers
            int[] instances = Enumerable.Range(0, threadCount).ToArray();

            DateTime startedTime = DateTime.UtcNow;

            // A little LINQ "abuse" that causes the 'void' method to run for each
            // item in the list.
            var Averages = instances.Select(x => ThreadMethod()).ToList();

            int max = Averages.Max();

            Console.WriteLine($"Largest average: {max}");

            TimeSpan timeTaken = DateTime.UtcNow - startedTime;

            Console.WriteLine($"Time taken: {timeTaken.TotalMilliseconds/1000:0.##} seconds");
        }
    }

There, now we've eliminated the need to manually add items to the `Averages` list. This code is looking more "LINQ-friendly" now!

Except that when I run this code, we're back to single-threaded performance. Typically, LINQ runs on a single thread. However, Parallel LINQ can bring back the performance gains we saw when we used the `Parallel` class. And guess what... it's as simple as adding *one statement*!

All we need to do is change

    var Averages = instances.Select(x => ThreadMethod()).ToList();

to

    var Averages = instances.AsParallel().Select(x => ThreadMethod()).ToList();

You can also use it in query syntax calls:

    for x in Averages.AsParallel()
        select ThreadMethod();

Before I added this single method call, the code took 8 seconds to run on my machine. After adding it, it was cut down to 3 seconds. Still not *quite* as fast as using `Parallel.For`, but there are also other considerations - LINQ can definitely take up some CPU time, and the fact that we're letting LINQ handle the thread synchronization could be a factor too. But you definitely can't beat the ease with which we made this code parallel!

There are other methods that are part of Parallel LINQ. For example, you can insert `AsSequential` method calls into your query chain to force parts of the query to run without parallelism. Or, you can use `AsOrdered` to ensure that the results of the parallelized query won't become scrambled in the results. (Remember that when we work with multiple threads, the threads can come back with results at any time, and it's hard to impossible to predict exactly when that will occur. Using `AsOrdered` may slow down the query but it will ensure that the results look just like if you had run the query without parallelism). You can also use `WithDegreeOfParallelism` to explicitly specify how many threads you want to run (normally .NET figures this out on its own based on your system).

One final aspect of PLINQ that's worth noting: it is unlikely that using parallel LINQ to access data on a *database server* is going to yield any performance improvements. This is because, even if you have multiple processors and threads in your system, the speed of processing a database query is dependent on the database *server*, not on your local machine. If you have a database server running on a single-core system and you access it with a system with 32 cores, your database queries will still only perform as they would on a single-core system (and vice-versa - even if you have a single-core system, if the database system is multi-threaded, you will see those benefits in the speed of your queries regardless). Because of this, PLINQ is only useful when you are processing data *within C# itself*. This is why the `AsParallel` method is an "opt-in" strategy - you only invoke parallelism when it is useful to do so, because as we've seen, parallelism can actually *slow down* code that is not optimized for parallel processing.

To summarize:

* If you're working with code that simply needs to perform an operation on many elements, setting up the code to be LINQ-friendly and then using Parallel LINQ is the easiest and most straightforward way to gain multithreaded performance.
* Code needing more complex operations can often be parallelized using `Parallel.For` or `Parallel.ForEach`.
* If you have *extremely* complex code where multiple threads are all doing very different things, `ThreadPool`s and manually managing `Thread`s may be the way to go.

What I hope you're starting to discover is that modern languages, such as .NET, offer a huge amount of functionality as part of the language and its standard library. Everything we have been learning about parallel programming can be generalized to any programming language that implements the concept of a thread. However, many languages simply offer threading primitives and don't provide much else to you - you have to write all of the code to actually handle threads yourself. This is a big part of why we started with a simple straightforward method, looked at its disadvantages, and then worked our way up to where we are now. Notice that in each section the code got a bit more compact and concise, and at the same time the performance kept improving. This is because a language like C#, and the accompanying features of the .NET Framework, has solved many of these issues for us and offers us nicely-designed programming constructs that we can use to quickly and easily take advantage of parallel programming. Imagine having to actually write all of the code to handle the different memory locations for each thread, scheduling execution, and so on! (If you were using a language like C, you'd be doing a lot more of it on your own - or you'd have to find a third-party library to do it for you!)

As we wrap up talking about parallel programming, there's one other aspect we're going to cover in the final presentation for this content: async programming. Unlike parallel execution for improved performance, async programming is more about *responsiveness* than *performance*. Stay tuned!

In your presentation, please cover:

- What is Parallel LINQ? How is it similar and different to traditional LINQ?
- How can you add parallelism to a LINQ query? (hint - it's one change!)
- Discuss a couple of the other options for Parallel LINQ and how they are useful.

Sources to get you started (but please do seek out and use other sources as well!):

- [Parallel LINQ](https://learn.microsoft.com/en-us/dotnet/standard/parallel-programming/introduction-to-plinq) at Microsoft.
- The [AsParallel](https://learn.microsoft.com/en-us/dotnet/api/system.linq.parallelenumerable.asparallel?view=net-7.0) method.
- [A beginner's guide to PLINQ](https://www.codeguru.com/csharp/a-beginners-guide-to-plinq/).

## C# async programming

Ok, here we go. Async programming. This is a topic that can be very confusing - and partly it's because the language constructs themselves are a bit confusing, since we have to understand a few different things things at once.

First, what is an async method? Basically, it's a task that's meant to run in the background. We've covered ways to do this with threads. But what makes an async method different is that it returns a value, and it's expected that it will be doing something that will take some amount of time. However, during that task's operation, we don't want the program to "lock up" while it waits for the result. Syntactically, you can have async methods that return `void`, but at that point they're really no different than threads, and you could just use a normal `ThreadPool` to execute them in the background.

Have you ever been using a program and suddenly the entire program becomes completely unresponsive? On a Mac, you might get "the spinning beachball", and on Windows, you might get "the spinning blue circle" and the application might fade in color to let you know it's "stuck". This is the sort of thing that can happen if the developer of the program did not implement async programming. (It's also possible that it's many other bugs, but one of the main goals of async programming is to eliminate this problem.)

Let's design a completely useless scenario that still demonstrates where async programmin comes into play. Suppose we make a "fidget keyboard" program - you can type letters and it makes things pop up on the screen. However, while this is happening some other process is running in the background. 

We'll start with this basic program:

    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                Console.WriteLine("You pressed key: " + cki.Key.ToString());
                if (cki.Key == ConsoleKey.Q)
                {
                    Console.WriteLine("Exiting.");
                    return;
                }
            }
        }
    }

If you run this program, it will simply print out each key scancode as you press keys on your keyboard. If you press the `q` key, the program exits.

Now, let's imagine we are adding in some task that runs in the background and is expected to take some time.

    internal class Program
    {
        static void Main(string[] args)
        {
            PretendToDownload();

            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                Console.WriteLine("You pressed key: " + cki.Key.ToString());
                if (cki.Key == ConsoleKey.Q)
                {
                    Console.WriteLine("Exiting.");
                    return;
                }
            }
        }

        static void PretendToDownload()
        {
            Console.WriteLine("Pretending to download a file...");
            Thread.Sleep(10000);
            Console.WriteLine("Download finished.");
            return;
        }
    }

Run this program and notice that, while the file is downloading, you cannot press any keys. (Well, you can, but the "user interface" - in this case, the console - won't actually show any of the keys you pressed until after the download "finishes"). Once the download "finishes", all the keys you pressed will suddenly all happen at once.

I'm sure you've seen this happen on your computer. Something is taking a while, you get impatient, so you do it again. And then in the end it actually happens twice. Or three times. Or more times depending on how impatient you were. :-)

We could solve this by using threading. We could put the download on a background thread and let it run. However, that requires us to delve into `ThreadPool`s or managing `Thread`s or similar constructs. That could be a lot of work for a simple task like this. And we're not really concerned with improving *performance* of the download or the program as a whole, we're simply trying to have the program not "block" during the download. This is where `async` comes in.

Async programming has two main keywords: `async` and `await`. The `async` keyword defines a method as asynchronous - this means that it will essentially automatically be run on its own thread. Async methods generally always return `Task` objects - `Task` objects are similar to delegates (again, we'll see those next week) in that they represent a method that is being run. In essence, an async method returns an instance of a method. It sounds kind of weird, but that's essentially what's happening!

To make our method asynchronous, we will write a "wrapper" method that 

    static async Task PretendToDownloadAsync()
    {
        await Task.Run(() => PretendToDownload());
        return;
    }
    
This syntax might seem a bit confusing. The method signature looks like we should be returning a `Task` object, but we just returned nothing - essentially we returned `void`. Why does this work? Because the `async` keyword tells the C# compiler that this method, by design, actually returns a `Task`. If we wanted to return a value from the method rather than nothing, we'd use generics to return a `Task` with an associated type, for example: `Task<int>`.

Now, let's talk about that `await` statement. `await` is similar to a `Thread.Join` call - it tells the code to stop and "await" the results of some other thread. However, we can't "await" something that isn't being performed in the background!

To make our call to `PretendToDownload` actually run in the background, we use a `Task`. Recall that we said that a `Task` is basically a pointer to a method. Using the `Run` method of a `Task` causes the task to be run on another thread. We can then use the `await` keyword to tell the current code to "await" the `Task`'s completion. In this case, we simply use a lambda function to call the PretendToDownload method.

Here's what we have now:

internal class Program
    {
        static void Main(string[] args)
        {
            PretendToDownloadAsync();

            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                Console.WriteLine("You pressed key: " + cki.Key.ToString());
                if (cki.Key == ConsoleKey.Q)
                {
                    Console.WriteLine("Exiting.");
                    return;
                }
            }
        }

        static async Task PretendToDownloadAsync()
        {
            await Task.Run(() => PretendToDownload());
            return;
        }

        static void PretendToDownload()
        {
            Console.WriteLine("Pretending to download a file...");
            Thread.Sleep(10000);
            Console.WriteLine("Download finished.");
            return;
        }
    }

If you run this code, you'll notice that even though the download task is pausing for 10 seconds, you're still able to press keys and see them appear immediately on the console. 

If you were to pretend that the console were an actual graphical application, not using `async` would cause the entire program to freeze up while the download was underway, much like how the console stopped printing your keystrokes. For you, the user, this means a spinning beachball or a spinning blue ring. Not fun! But with `async` tasks, we can easily push background tasks onto a thread without having to actually write any threading code of our own.

Many methods in the C# standard library have `Async` versions, which function very much like the wrapper we wrote for our method. The convention in C# and .NET is that any method that is defined as `async` has a name ending with `Async`. While this isn't strictly required, it's a good idea to follow the naming standards when writing your own methods.

Async programming can be a tricky topic since there is a lot of indirection going on. You have methods being passed around like parameters and you have threads being created in the background for the background. Don't feel bad if you can't quite grasp async programming right up front - it's definitely a tricky topic and you'll want to practice with it if you design applications that would benefit from it. This discussion is only meant to serve as a brief introduction and to help you start to understand what's actually going on with async programming.

Async programming won't show up on the homework.

In your presentation, please cover:

- What is async programming? How does it compare to using threads? (hint: it's typically used for application responsiveness rather than performance)
- How do you make a method `async`? What is the return type of `async` methods?
- What does `await` do when you use it on a running async method?
- See if you can come up with a simple scenario where async programming would be useful. You don't have to code it, but just describe it.

Sources to get you started (but please do seek out and use other sources as well!):

- [Asynchronous programming](https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/async-scenarios) at Microsoft.
- [Task-based Asynchronous Pattern](https://learn.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/task-based-asynchronous-pattern-tap) at Microsoft - the theory behind async programming.
- [Using Task.Run and Async/Await](https://www.pluralsight.com/guides/using-task-run-async-await).
