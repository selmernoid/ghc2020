using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Linq;

namespace Task1
{
    
    class Program
    {
        public  static int libraries, books;
        public static int[] booksCost;
        public static BitArray[] libBooks;

        public static string fileName;
        //static IEnumerable<int> ParseLine(string line) => line.Split(' ').Select(int.Parse);

        static void Main(string[] args)
        {
            fileName = args.Any() ? args[0] : "";

            Read();

            var result = GetAnswer();

            Output(result);
        }

        static void Read() {
            libraries = books = 100_000;
            libBooks = new BitArray[libraries];
            for (int i = 0; i < libraries; i++) {
                libBooks[i] = new BitArray(books, false);
            }
        }
        static void Output(object result) { }

        static object GetAnswer()
        {
            return null;
        }
    }
}
