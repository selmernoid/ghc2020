using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public struct LibraryInfo
    {
        public int Days { get; set; }
        public int Total { get; set; }
    }
    public class Library
    {
        public int Id;
        public int SignUpDays;
        public int BooksPerDay;
        public int[] Books;

        public LibraryInfo GetLibraryInfo => new LibraryInfo()
        {
            Days = SignUpDays,
            Total = Books.Sum()
        };
    }


}
