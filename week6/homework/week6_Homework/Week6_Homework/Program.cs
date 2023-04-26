// PLEASE KEEP THE FOLLOWING HEADER INTACT IN YOUR SUBMITTED CODE other than adding your name
// after "Programmer:".

/*
 * CS 403 - Week 6 Homework
 * Programmer: YOUR NAME HERE
 */

/*
 * This code implements a simple cryptographic encryption benchmark tool.
 * The idea is that the program will generate random data and then encrypt it.
 * This is a highly parallelizable operaiton since we are already individually encrypting
 * blocks of data.
 * 
 * The current code is single-threaded. 
 * Your task: Use any approach of your choice to get the code to run in a multi-threaded fashion.
 *
 * Important: Run the code BEFORE you implement your method so you can compare the
 * execution time before and after you add threading!
 * 
 * Alternatively, refactor the project so that the current code is in one method, and
 * your implementation is in another method; then you can run each method and compare
 * the time taken within the program itself!
 *
 * You can use:
 *   * Manually creating and scheduling threads
 *   * Parallel.For
 *   * ThreadPool
 *   * Parallel LINQ
 *   * ... or some other method!
 *
 * We do not care about the return values of the encryption function.
 * 
 * You will know that you have succeeded if the task completion time is *faster* when you have implemented
 * your threading strategy!
 * 
 * (Note that this code does NOT require you to use locks. However, you DO need to have a strategy for making
 * sure you know when the thread(s) have finished executing so you can measure the total execution wall clock 
 * time.)
 */

using System.Diagnostics;
using System.Security.Cryptography;

namespace Week6_Homework
{

    public class EncryptionOperation
    {
        byte[] plaintext;

        /// <summary>
        /// Instantiate an EncryptionOperation object by specifying a byte array to encrypt.
        /// </summary>
        /// <param name="bytesToEncrypt"></param>
        public EncryptionOperation(byte[] bytesToEncrypt)
        {
            this.plaintext = bytesToEncrypt;
        }

        /// <summary>
        /// Instantiate an EncryptionOperation object by generating 16MiB of random data to encrypt.
        /// </summary>
        public EncryptionOperation()
        {
            // Generate 16 MiB of random data
            this.plaintext = new byte[1 << 24]; // 2^24, or 16777216 bytes
            RandomNumberGenerator.Fill(this.plaintext);
        }

        /// <summary>
        /// Encrypt the bytes with a randomly generated key.
        /// </summary>
        /// <returns>The randomly generated encrypted data.</returns>
        public byte[] Encrypt()
        { 
            TripleDES tripleDES = TripleDES.Create();
            tripleDES.GenerateKey();
            tripleDES.GenerateIV();
            return tripleDES.EncryptCbc(this.plaintext, tripleDES.IV);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting encryption benchmark operation.");

            // The Stopwatch is another way to measure the time taken by a process. It's a somewhat
            // cleaner alternative to storing a DateTime object and comparing when done.
            Stopwatch st = new Stopwatch();
            st.Start();
            
            // Run the encryption operation 32 times.
            // This will compute the time taken to encrypt 512MB of data.
            for (int n = 0; n < 32; n++) 
            {
                new EncryptionOperation().Encrypt();
            }
            st.Stop();

            // On the test environment, this program takes about 23.6 seconds to run in single-threaded mode.
            // However, make sure you run this on YOUR system to determine your single-threaded baseline.
            Console.WriteLine($"The operation took {st.ElapsedMilliseconds} milliseconds.");
        }
    }
}