using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using Common;

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
            if (!Path.IsPathFullyQualified(fileName))
            {
                fileName = Path.Combine(Environment.CurrentDirectory, fileName);
            }
            Read();

            var result = GetAnswer();
            
            result = GetAnswerD();

            Output(result);
        }

        static void Read()
        {
            int[] ParseLine(string val) => val.Split(' ').Select(int.Parse).ToArray();
            using var reader = File.OpenText(fileName);

            int[] GetNextLine() => ParseLine(reader.ReadLine());
            
            var firstLine = GetNextLine();

            books = firstLine[0];
            libraries = firstLine[1];
            days = firstLine[2];
            booksCost = GetNextLine();
            
            //libraries = books = 100_000;;
            libBooks = new BitArray[libraries];
            librariesSpeed = new int[libraries];
            librariesSignups = new int[libraries];

            for (int i = 0; i < libraries; i++) {
                libBooks[i] = new BitArray(books, false);
                var booksIds = GetNextLine();
                foreach (var bookId in booksIds)
                {
                    libBooks[i][bookId] = true;
                }
                var infoLine = GetNextLine();

                //[0] is books count
                //var currentBooksCount = infoLine[0];
                librariesSignups[i] = infoLine[1];
                librariesSpeed[i] = infoLine[2];
            }
            //foreach (var arr in libBooks)
            //{
            //    foreach (var bit in arr)
            //    {
            //        Console.Out.Write($"{bit} ");
            //    }
            //    Console.WriteLine();
            //}
        }
        static void Output(Answer result) { }

        static Answer GetAnswer()
        {
            return null;
        }
        static Answer GetAnswerD()
        {
            var result = new Answer
            {
                Libraries = new List<LibraryAnswer>()
            };
            int j = days;
            BitArray maskAlready = new BitArray(books, true);
            while (j > 0)
            {
                var maxBooks = -1;
                var maxId = -1;
                BitArray maxMask = null;

                for (int i = 0; i < libraries; i++)
                {
                    var copy = new BitArray(maskAlready);
                    var maskCur = maskAlready.And(libBooks[i]);
                    var val = maskCur.Sum();
                    if (val > maxBooks)
                    {
                        maxBooks = val;
                        maxId = i;
                        maxMask = maskCur;
                    }
                }

                var libraryAnswer = new LibraryAnswer
                {
                    Id = maxId,
                    Books = new List<int>()
                };
                for (int i = 0; i < books; i++)
                {
                    if (maxMask[i])
                        libraryAnswer.Books.Add(i);
                }
                result.Libraries.Add(libraryAnswer);
                j -= 2;
            }
            return result;
        }
    }


    public static class Extensions {
        static unsafe int ConvertBoolUnsafe(bool t) => *(byte*)(&t);

        public static int Sum(this BitArray bits) {
            var r = 0;
            for (int i = 0; i < bits.Count; i++) {
                r += ConvertBoolUnsafe(bits[i]);
            }

            return r;
        }
    }
}
