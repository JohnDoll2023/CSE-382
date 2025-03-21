﻿
using System;

using NumberProperties;
using Primes;

namespace Primes
{
    public class PrimesDriver
    {
        public static void Main(String[] args)
        {
            Console.WriteLine("Enter positive integers");
            String[] toks = Console.ReadLine().Split(' ');
            foreach (String tok in toks)
            {
                int v = Int32.Parse(tok);
                Console.WriteLine("{0} is prime {1}", v, IntegerProperties.IsPrime(v));
            }
        }
    }
}