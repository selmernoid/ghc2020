using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using Common;

namespace Task1
{

    class Program
    {
        //final Solution ... almost
        public static Dictionary<int, LibrariesCost> libraryCostsGlobal = new Dictionary<int, LibrariesCost>();


        public static int libraries, books;
        public static int[] booksCost;
        public static BitArray[] libBooks;
        public static int days;
        public static int[] librariesSignups;
        public static int[] librariesSpeed;

        public static string fileName;
        //static IEnumerable<int> ParseLine(string line) => line.Split(' ').Select(int.Parse);

        static void Main(string[] args)
        {
            Read(args);

            var result = GetAnswer();

            result = GetAnswerD();

            Output(result);
        }

        static void Read(string[] args)
        {
            fileName = args.Any() ? args[0] : "";
            if (!Path.IsPathFullyQualified(fileName))
            {
                fileName = Path.Combine(Environment.CurrentDirectory, fileName);
            }

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

            for (int i = 0; i < libraries; i++)
            {
                libBooks[i] = new BitArray(books, false);
                var infoLine = GetNextLine();
                var booksIds = GetNextLine();
                foreach (var bookId in booksIds)
                {
                    libBooks[i][bookId] = true;
                }

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

        static void Output(Answer result)
        {
            var writer = File.CreateText($"{Path.GetFileNameWithoutExtension(fileName)}_out.txt");
            var sb = new StringBuilder();
            sb.AppendLine(result.Libraries.Count.ToString());
            foreach (var library in result.Libraries)
            {
                sb.AppendLine($"{library.Id} {library.Books.Count}");
                sb.AppendJoin(' ', library.Books);
                sb.AppendLine();
            }
            writer.Write(sb);
            writer.Flush();
        }

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
            BitArray maskAlready = new BitArray(books, false);
            var libsSorted = new List<(int Key, int Value)>();
            for (int i = 0; i < libraries; i++)
            {
                libsSorted.Add((i, libBooks[i].Sum()));
            }

            var sortedArray = libsSorted.OrderByDescending(x=> x.Value).ToArray();

            var idx = 0;
            while (j > 0)
            {

                var lib = sortedArray[idx++];
//                var lib = libsSorted.Last();
//                libsSorted.Remove(lib.Key);

                var libraryAnswer = new LibraryAnswer
                {
                    Id = lib.Key,
                    Books = new List<int>()
                };
                for (int i = 0; i < books; i++)
                {
                    if (!maskAlready[i] && libBooks[lib.Key][i])
                        libraryAnswer.Books.Add(i);
                }
                result.Libraries.Add(libraryAnswer);
                maskAlready.Or(libBooks[lib.Key]);
                j -= librariesSignups[lib.Key];
            }
            return result;
        }


        public void AddToFinalLibrary(LibrariesCost libraryCostCurrent)
        {
            for (int i = 0; i < libraryCostsGlobal.Count; i++)
            {
                //from zero
                if (libraryCostsGlobal.TryGetValue(libraryCostCurrent.Days, out LibrariesCost existLibraryCost))
                {
                    if (existLibraryCost.Cost < libraryCostCurrent.Cost)
                        libraryCostsGlobal[libraryCostCurrent.Days] = libraryCostCurrent;
                }
                else //from anotherlibrary
                {
                    libraryCostsGlobal[libraryCostCurrent.Days] = libraryCostCurrent;
                    LibrariesCost librariesCostNew = new LibrariesCost()
                    {
                        Cost = libraryCostsGlobal[i].Cost + libraryCostCurrent.Cost,
                        Days = libraryCostsGlobal[i].Days + libraryCostCurrent.Days,
                        libraryIds = libraryCostsGlobal[i].libraryIds.Union(libraryCostCurrent.libraryIds).ToList()
                    };
                    if (libraryCostsGlobal.TryGetValue(librariesCostNew.Days, out LibrariesCost existLibraryCostSummarize))
                    {
                        if (existLibraryCostSummarize.Cost < librariesCostNew.Cost)
                            libraryCostsGlobal[librariesCostNew.Days] = librariesCostNew;
                    }
                    else
                        libraryCostsGlobal[librariesCostNew.Days] = librariesCostNew;
                }
            }
        }
    }

    public static class Extensions
    {
        static unsafe int ConvertBoolUnsafe(bool t) => *(byte*)(&t);

        public static int Sum(this BitArray bits)
        {
            var r = 0;
            for (int i = 0; i < bits.Count; i++)
            {
                r += ConvertBoolUnsafe(bits[i]);
            }

            return r;
        }
    }



    public class LibrariesCost
    {
        public List<int> libraryIds { get; set; }
        public string Id { get { return string.Join(",", libraryIds); } }
        public int Days { get; set; }
        public int Cost { get; set; }
    }





}
