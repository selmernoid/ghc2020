using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class OrderedList
    {
        private List<(int key, int value)> _list;

        public OrderedList(IEnumerable<(int key, int value)> list)
        {
            _list = list.OrderByDescending(x => x.value).ToList();
        }


        public void Add((int key, int value) item)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                if (item.value >= _list[i].value)
                {
                    _list.Insert(i, item);
                    return;
                }
            }
            _list.Add(item);
        }

        public void Remove(int key)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                var elem = _list[i];
                if (elem.key == key)
                {
                    _list.Remove(elem);
                    return;
                }
            }
        }
    }

}
