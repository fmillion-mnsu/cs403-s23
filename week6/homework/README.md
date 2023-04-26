## Homework

This week's homework focuses on multithreaded code. The program is a simple encryption benchmark program, and your job is to speed it up by using multithreading.

From the homework assignment:


>  This code implements a simple cryptographic encryption benchmark tool.
>  The idea is that the program will generate random data and then encrypt it.
>  This is a highly parallelizable operaiton since we are already individually encrypting
>  blocks of data.
>  
>  The current code is single-threaded. 
>  Your task: Use any approach of your choice to get the code to run in a multi-threaded fashion.
> 
>  Important: Run the code BEFORE you implement your method so you can compare the
>  execution time before and after you add threading!
>  
>  Alternatively, refactor the project so that the current code is in one method, and
>  your implementation is in another method; then you can run each method and compare
>  the time taken within the program itself!
> 
>  You can use:
>    * Manually creating and scheduling threads
>    * Parallel.For
>    * ThreadPool
>    * Parallel LINQ
>    * ... or some other method!
> 
>  We do not care about the return values of the encryption function.
>  
>  You will know that you have succeeded if the task completion time is *faster* when you have implemented
>  your threading strategy!
>  
>  (Note that this code does NOT require you to use locks. However, you DO need to have a strategy for making
>  sure you know when the thread(s) have finished executing so you can measure the total execution wall clock 
>  time.)

One important point: If you do not have a multi-core processor in your computer, *you won't see performance improvements even if you implement multithreading*. It's a safe bet that you have a multi-core processor - they've been commonplace since 2006 and standard since 2009 - but *just in case* you're on a single-core setup (maybe you're using a virtual machine to run Visual Studio and don't have it configured to use multiple cores) please be aware of this. If you are in such a situation and can't address it by adding more cores, come talk to me and I can get you access to a temporary cloud workstation to write and test your code on.

We will explore this homework on both Wednesday and Friday - make sure you come to class! 
