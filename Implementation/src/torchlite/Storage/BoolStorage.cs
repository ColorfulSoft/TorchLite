//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        /// <summary>
        /// A torchlite.BoolStorage is a contiguous, one-dimensional array of elements of a
        /// torchlite.@bool.
        /// </summary>
        public unsafe sealed class BoolStorage : Storage, IList<bool>
        {

            #region properties

            /// <summary>
            /// Gets or sets the element at the specified index.
            /// </summary>
            public new bool this[int index]
            {

                get
                {
                    if((index < 0) || (index >= this.size))
                    {
                        throw new IndexOutOfRangeException(string.Format("Index {0} is out of range of a storage of size {1}.", index, this.size));
                    }
                    return *((bool*)this.data_ptr + index);
                }

                set
                {
                    if((index < 0) || (index >= this.size))
                    {
                        throw new IndexOutOfRangeException(string.Format("Index {0} is out of range of a storage of size {1}.", index, this.size));
                    }
                    *((bool*)this.data_ptr + index) = Convert.ToBoolean(value);
                }

            }

            /// <summary>
            /// Gets the number of elements contained in the ICollection&lt;T&gt;.
            /// </summary>
            int ICollection<bool>.Count
            {

                get
                {
                    return this.size;
                }

            }

            /// <summary>
            /// Gets a value indicating whether the ICollection&lt;T&gt; is read-only.
            /// </summary>
            bool ICollection<bool>.IsReadOnly
            {

                get
                {
                    return false;
                }

            }

            #endregion

            #region constructors

            /// <summary>
            /// Initializes the booling point storage of the specified size.
            /// </summary>
            /// <param name="size">The number of elements in the storage.</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public BoolStorage(int size) : base(size, torchlite.@bool)
            {
            }

            #endregion

            #region methods

            /// <summary>
            /// Adds an item to the ICollection&lt;T&gt;.
            /// </summary>
            /// <param name="item">The object to add to the ICollection&lt;T&gt;.</param>
            [Obsolete("ICollection<bool>.Add(bool) -> void method is not implemented for torchlite.BoolStorage.", true)]
            void ICollection<bool>.Add(bool item)
            {
                throw new NotSupportedException("ICollection<bool>.Add(bool) -> void method is not implemented for torchlite.BoolStorage.");
            }

            /// <summary>
            /// Removes all items from the ICollection&ltT&gt.
            /// </summary>
            [Obsolete("ICollection<bool>.Clear() -> void method is not implemented for torchlite.BoolStorage.", true)]
            void ICollection<bool>.Clear()
            {
                throw new NotSupportedException("ICollection<bool>.Clear() -> void method is not implemented for torchlite.BoolStorage.");
            }

            /// <summary>
            /// Determines whether the ICollection&lt;T&gt; contains a specific value.
            /// </summary>
            /// <param name="item">The object to locate in the ICollection&lt;T&gt;.</param>
            /// <returns>true if item is found in the ICollection&lt;T&gt;; otherwise, false.</returns>
            [Obsolete("ICollection<bool>.Contains(bool) -> bool method is not implemented for torchlite.BoolStorage.", true)]
            bool ICollection<bool>.Contains(bool item)
            {
                throw new NotSupportedException("ICollection<bool>.Contains(bool) -> bool method is not implemented for torchlite.BoolStorage.");
            }

            /// <summary>
            /// Copies the elements of the ICollection&lt;T&gt; to an Array, starting at a particular Array index.
            /// </summary>
            /// <param name="array">The one-dimensional Array that is the destination of the elements copied from ICollection&lt;T&gt;. The Array must have zero-based indexing.</param>
            /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
            void ICollection<bool>.CopyTo(bool[] array, int arrayIndex)
            {
                if(array == null)
                {
                    throw new ArgumentNullException("array is null.");
                }
                if(arrayIndex < 0)
                {
                    throw new ArgumentOutOfRangeException("arrayIndex is less than 0.");
                }
                if((this.size + arrayIndex) >= array.Length)
                {
                    throw new ArgumentException("The number of elements in the source ICollection<T> is greater than the available space from arrayIndex to the end of the destination array.");
                }
                var ptr = (bool*)this.data_ptr;
                for(int i = 0; i < this.size; ++i)
                {
                    array[arrayIndex + i] = ptr[i];
                }
            }

            /// <summary>
            /// Returns an enumerator that iterates through a collection.
            /// </summary>
            /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
            IEnumerator IEnumerable.GetEnumerator()
            {
                for(int i = 0; i < this.size; ++i)
                {
                    yield return this[i];
                }
            }

            /// <summary>
            /// Returns an enumerator that iterates through a collection.
            /// </summary>
            /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
            IEnumerator<bool> IEnumerable<bool>.GetEnumerator()
            {
                for(int i = 0; i < this.size; ++i)
                {
                    yield return this[i];
                }
            }

            /// <summary>
            /// Determines the index of a specific item in the IList&lt;T&gt;.
            /// </summary>
            /// <param name="item">The object to locate in the IList&lt;T&gt;.</param>
            /// <returns>The index of item if found in the list; otherwise, -1.</returns>
            [Obsolete("IList<bool>.Contains(bool) -> int method is not implemented for torchlite.BoolStorage.", true)]
            int IList<bool>.IndexOf(bool item)
            {
                throw new NotSupportedException("IList<bool>.Contains(bool) -> int method is not implemented for torchlite.BoolStorage.");
            }

            /// <summary>
            /// Inserts an item to the IList&lt;T&gt; at the specified index.
            /// </summary>
            /// <param name="index">The zero-based index at which item should be inserted.</param>
            /// <param name="item">The object to insert into the IList&lt;T&gt;.</param>
            [Obsolete("IList<bool>.Insert(int, bool) -> void method is not implemented for torchlite.BoolStorage.", true)]
            void IList<bool>.Insert(int index, bool item)
            {
                throw new NotSupportedException("IList<bool>.Insert(int, bool) -> void method is not implemented for torchlite.BoolStorage.");
            }

            /// <summary>
            /// Removes the first occurrence of a specific object from the ICollection&lt;T&gt;.
            /// </summary>
            /// <param name="item">The object to remove from the ICollection&lt;T&gt;.</param>
            /// <returns>true if item was successfully removed from the ICollection&lt;T&gt;; otherwise, false. This method also returns false if item is not found in the original ICollection&lt;T&gt;.</returns>
            [Obsolete("ICollection<bool>.Remove(bool) -> bool method is not implemented for torchlite.BoolStorage.", true)]
            bool ICollection<bool>.Remove(bool item)
            {
                throw new NotSupportedException("ICollection<bool>.Remove(bool) -> bool method is not implemented for torchlite.BoolStorage.");
            }

            /// <summary>
            /// Removes the IList&lt;T&gt; item at the specified index.
            /// </summary>
            /// <param name="index">The zero-based index of the item to remove.</param>
            [Obsolete("IList<bool>.RemoveAt(int) -> void method is not implemented for torchlite.BoolStorage.", true)]
            void IList<bool>.RemoveAt(int index)
            {
                throw new NotSupportedException("IList<bool>.RemoveAt(int) -> void method is not implemented for torchlite.BoolStorage.");
            }

            #endregion

        }

    }

}