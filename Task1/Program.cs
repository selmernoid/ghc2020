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
        public  static int libraries, books, D;
        public static int[] booksCost;
        public static BitArray[] libBooks;

        public static string fileName;
        //static IEnumerable<int> ParseLine(string line) => line.Split(' ').Select(int.Parse);

        static void Main(string[] args)
        {
            fileName = args.Any() ? args[0] : "";

            Read();

            var result = GetAnswer();
            
            result = GetAnswerD();

            Output(result);
        }

        static void Read() {
            libraries = books = 100_000;
            libBooks = new BitArray[libraries];
            for (int i = 0; i < libraries; i++) {
                libBooks[i] = new BitArray(books, false);
            }
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
            int j = D;
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
