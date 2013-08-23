using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using org.in2bits.MyXls;

namespace MyXls.SL2
{
    public class SortedListEnumerator<TIndex, TItems> : IEnumerator<KeyValuePair<TIndex, TItems>>
    {
        private readonly int _count;
        private readonly List<TIndex> _sortedKeys;
        private readonly SortedList<TIndex, TItems> _list;

        internal SortedListEnumerator(List<TIndex> sortedKeys, SortedList<TIndex, TItems> list)
        {
            _sortedKeys = sortedKeys;
            _list = list;
            _count = list.Count;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            //NO-OP
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. 
        ///                 </exception>
        public bool MoveNext()
        {
            if ((_count - 1) == _currentIndex)
                return false;
            _currentIndex++;
            var key = _sortedKeys[_currentIndex];
            var value = _list[key];
            _current = new KeyValuePair<TIndex, TItems>(key, value);
            return true;
        }

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. 
        ///                 </exception>
        public void Reset()
        {
            _currentIndex = -1;
        }

        private int _currentIndex = -1;
        private KeyValuePair<TIndex, TItems> _current;
        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        /// <returns>
        /// The element in the collection at the current position of the enumerator.
        /// </returns>
        public KeyValuePair<TIndex, TItems> Current
        {
            get
            {
                if (-1 == _currentIndex)
                    throw new ArgumentOutOfRangeException();
                return _current;
            }
        }

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        /// <returns>
        /// The current element in the collection.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.
        ///                     -or- 
        ///                     The collection was modified after the enumerator was created.
        ///                 </exception>
        object IEnumerator.Current
        {
            get { return Current; }
        }
    }
}
