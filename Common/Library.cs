using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class Library
    {
        private int[] _daysDistribution;
        public Library(int id, int signUpDays, int booksPerDay, int[] books)
        {

            //_daysDistribution = books.Select((value, index) => (value, index)).GroupBy((x) => x.index);
        }
        private long? _libraryValue;

        public int Id;
        public int SignUpDays;
        public int BooksPerDay;
        public int[] Books;

        //private int[] DaysDistribution { }

        public long LibraryValue
        {
            get
            {
                if (!_libraryValue.HasValue)
                {
                    _libraryValue = Books.Sum();
                }
                return _libraryValue.Value;

            }
        }

        //public int[,] GetSplitedArrays(int[] array, int sizeOfEach)
        //{
        //    var quantityOfColumns = array.Length / sizeOfEach;
        //    int[,] result = new int[quantityOfColumns + (array.Length%sizeOfEach == 0 ? 0 : 1), sizeOfEach];
        //    for (var i = 0; i < array.Length;)
        //    {
        //        for (int j = 0; j < sizeOfEach; j++, i++)
        //        {

        //        }
        //    }
        //}
    }

   
}
