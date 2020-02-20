using System;
using System.Collections.Generic;
using System.Text;

namespace Common {
    public class Answer {
        public List<LibraryAnswer> Libraries { get; set; }
    }

    public class LibraryAnswer
    {
        public int Id { get; set; }
        public List<int> Books { get; set; }
    }
}
