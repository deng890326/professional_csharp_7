using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter05.List.Generic
{
    public interface IMyList<T> : IEnumerable<T>
    {
        T? First { get; }
        T? Last { get; }
        void AddLast(in T item);
    }

    public class LinkedList<T> : IMyList<T>
    {
        private readonly LinkedListHead _listHead; //哨兵作用，不存储实际内容

        public LinkedList()
        {
            _listHead = new LinkedListHead();
            _listHead.Prev = _listHead.Next = _listHead;
        }

        public T? First
        {
            get
            {
                LinkedListNode<T>? firstNode = _listHead.Next as LinkedListNode<T>;
                return firstNode != null ? firstNode.Value : default(T); // 有bug，值类型的列表是空的时候会返回0
            }
        }

        public T? Last
        {
            get
            {
                LinkedListNode<T>? lastNode = _listHead.Prev as LinkedListNode<T>;
                return lastNode != null ? lastNode.Value : default(T); // 有bug，值类型的列表是空的时候会返回0
            }
        }

        public void AddLast(in T item)
        {
            LinkedListNode<T> newNode = new LinkedListNode<T>(item);

            LinkedListHead currentLast = _listHead.Prev;
            currentLast.Next = newNode;
            newNode.Prev = currentLast;
            newNode.Next = _listHead;
            _listHead.Prev = newNode;
        }

        public IEnumerator<T> GetEnumerator()
        {
            LinkedListHead currentNode = _listHead.Next;
            //while (!ReferenceEquals(currentNode, _listHead))
            while (currentNode != _listHead)
            {
                var node = currentNode as LinkedListNode<T>;
                yield return node != null ? node.Value : default(T);
                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class LinkedListHead
    {
        public LinkedListHead()
        {
            Prev = Next = this;
        }

        internal LinkedListHead Prev { get; set; }

        internal LinkedListHead Next { get; set; }
    }

    internal class LinkedListNode<T> : LinkedListHead
    {
        public LinkedListNode(T value)
        {
            Value = value;
        }

        internal T Value { get; set; }
    }
}
