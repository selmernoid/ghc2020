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
        public static int days;
        public static int[] librariesSignups;
        public static int[] librariesSpeed;

        public static string fileName;
        //static IEnumerable<int> ParseLine(string line) => line.Split(' ').Select(int.Parse);

        static void Main(string[] args)
        {
            fileName = args.Any() ? args[0] : "";
            fileName = @"C:\Users\strunin\Desktop\a_example.txt";
            Read();

            var result = GetAnswer();

            Output(result);
        }

        static void Read()
        {
            int[] ParseLine(string val) 
            {
                Console.WriteLine(val);
                return val.Split(' ').Select(int.Parse).ToArray();
            }
            using var reader = File.OpenText(fileName);

            int[] GetNextLine() => ParseLine(reader.ReadLine());
            
            var firstLine = GetNextLine();

            books = firstLine[0];
            libraries = firstLine[1];
            days = firstLine[2];

            //libraries = books = 100_000;;
            libBooks = new BitArray[libraries];
            librariesSpeed = new int[libraries];
            librariesSignups = new int[libraries];

            for (int i = 0; i < libraries; i++) {
                libBooks[i] = new BitArray(books, false);
                var firstLibraryLine = GetNextLine();

                //[0] is books count
                //var currentBooksCount = firstLibraryLine[0];
                librariesSignups[i] = firstLibraryLine[1];
                librariesSpeed[i] = firstLibraryLine[2];

                var booksIds = GetNextLine();
                foreach (var bookId in booksIds)
                {
                    libBooks[i][bookId] = true;
                }
            }
        }
        static void Output(object result) { }

        static object GetAnswer()
        {
            return null;
        }
    }
}
