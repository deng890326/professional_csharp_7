using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter05.List
{
    public interface IMyList : IEnumerable
    {
        object? First { get; }
        object? Last { get; }
        void AddLast(in object item);
    }

    public class LinkedList : IMyList
    {
        private readonly LinkedListNode _listHead; //哨兵作用，不存储实际内容

        public LinkedList()
        {
            _listHead = new LinkedListNode();
            _listHead.Prev = _listHead.Next = _listHead;
        }

        public object? First => _listHead.Next.Value;

        public object? Last => _listHead.Prev.Value;

        public void AddLast(in object item)
        {
            LinkedListNode newNode = new LinkedListNode();
            newNode.Value = item;

            LinkedListNode currentLast = _listHead.Prev;
            currentLast.Next = newNode;
            newNode.Prev = currentLast;
            newNode.Next = _listHead;
            _listHead.Prev = newNode;
        }

        public IEnumerator GetEnumerator()
        {
            LinkedListNode currentNode = _listHead.Next;
            //while (!ReferenceEquals(currentNode, _listHead))
            while (currentNode != _listHead)
            {
                yield return currentNode.Value;
                currentNode = currentNode.Next;
            }
        }
    }

    internal class LinkedListNode
    {
        public LinkedListNode()
        {
            Prev = Next = this;
        }

        internal object? Value { get; set; }

        internal LinkedListNode Prev { get; set; }

        internal LinkedListNode Next { get; set; }
    }
}
