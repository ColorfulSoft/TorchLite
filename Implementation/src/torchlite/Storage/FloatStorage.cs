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
        /// A torchlite.FloatStorage is a contiguous, one-dimensional array of elements of a
        /// torchlite.float32.
        /// </summary>
        public unsafe sealed class FloatStorage : Storage, IList<float>
        {

            #region properties

            /// <summary>
            /// Gets or sets the element at the specified index.
            /// </summary>
            public new float this[int index]
            {

                get
                {
                    if((index < 0) || (index >= this.size))
                    {
                        throw new IndexOutOfRangeException(string.Format("Index {0} is out of range of a storage of size {1}.", index, this.size));
                    }
                    return *((float*)this.data_ptr + index);
                }

                set
                {
                    if((index < 0) || (index >= this.size))
                    {
                        throw new IndexOutOfRangeException(string.Format("Index {0} is out of range of a storage of size {1}.", index, this.size));
                    }
                    *((float*)this.data_ptr + index) = Convert.ToSingle(value);
                }

            }

            /// <summary>
            /// Gets the number of elements contained in the ICollection&lt;T&gt;.
            /// </summary>
            int ICollection<float>.Count
            {

                get
                {
                    return this.size;
                }

            }

            /// <summary>
            /// Gets a value indicating whether the ICollection&lt;T&gt; is read-only.
            /// </summary>
            bool ICollection<float>.IsReadOnly
            {

                get
                {
                    return false;
                }

            }

            #endregion

            #region constructors

            /// <summary>
            /// Initializes the floating point storage of the specified size.
            /// </summary>
            /// <param name="size">The number of elements in the storage.</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public FloatStorage(int size) : base(size, torchlite.float32)
            {
            }

            #endregion

            #region methods

            /// <summary>
            /// Adds an item to the ICollection&lt;T&gt;.
            /// </summary>
            /// <param name="item">The object to add to the ICollection&lt;T&gt;.</param>
            [Obsolete("ICollection<float>.Add(float) -> void method is not implemented for torchlite.FloatStorage.", true)]
            void ICollection<float>.Add(float item)
            {
                throw new NotSupportedException("ICollection<float>.Add(float) -> void method is not implemented for torchlite.FloatStorage.");
            }

            /// <summary>
            /// Removes all items from the ICollection&ltT&gt.
            /// </summary>
            [Obsolete("ICollection<float>.Clear() -> void method is not implemented for torchlite.FloatStorage.", true)]
            void ICollection<float>.Clear()
            {
                throw new NotSupportedException("ICollection<float>.Clear() -> void method is not implemented for torchlite.FloatStorage.");
            }

            /// <summary>
            /// Determines whether the ICollection&lt;T&gt; contains a specific value.
            /// </summary>
            /// <param name="item">The object to locate in the ICollection&lt;T&gt;.</param>
            /// <returns>true if item is found in the ICollection&lt;T&gt;; otherwise, false.</returns>
            [Obsolete("ICollection<float>.Contains(float) -> bool method is not implemented for torchlite.FloatStorage.", true)]
            bool ICollection<float>.Contains(float item)
            {
                throw new NotSupportedException("ICollection<float>.Contains(float) -> bool method is not implemented for torchlite.FloatStorage.");
            }

            /// <summary>
            /// Copies the elements of the ICollection&lt;T&gt; to an Array, starting at a particular Array index.
            /// </summary>
            /// <param name="array">The one-dimensional Array that is the destination of the elements copied from ICollection&lt;T&gt;. The Array must have zero-based indexing.</param>
            /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
            void ICollection<float>.CopyTo(float[] array, int arrayIndex)
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
                var ptr = (float*)this.data_ptr;
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
            IEnumerator<float> IEnumerable<float>.GetEnumerator()
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
            [Obsolete("IList<float>.Contains(float) -> int method is not implemented for torchlite.FloatStorage.", true)]
            int IList<float>.IndexOf(float item)
            {
                throw new NotSupportedException("IList<float>.Contains(float) -> int method is not implemented for torchlite.FloatStorage.");
            }

            /// <summary>
            /// Inserts an item to the IList&lt;T&gt; at the specified index.
            /// </summary>
            /// <param name="index">The zero-based index at which item should be inserted.</param>
            /// <param name="item">The object to insert into the IList&lt;T&gt;.</param>
            [Obsolete("IList<float>.Insert(int, float) -> void method is not implemented for torchlite.FloatStorage.", true)]
            void IList<float>.Insert(int index, float item)
            {
                throw new NotSupportedException("IList<float>.Insert(int, float) -> void method is not implemented for torchlite.FloatStorage.");
            }

            /// <summary>
            /// Removes the first occurrence of a specific object from the ICollection&lt;T&gt;.
            /// </summary>
            /// <param name="item">The object to remove from the ICollection&lt;T&gt;.</param>
            /// <returns>true if item was successfully removed from the ICollection&lt;T&gt;; otherwise, false. This method also returns false if item is not found in the original ICollection&lt;T&gt;.</returns>
            [Obsolete("ICollection<float>.Remove(float) -> bool method is not implemented for torchlite.FloatStorage.", true)]
            bool ICollection<float>.Remove(float item)
            {
                throw new NotSupportedException("ICollection<float>.Remove(float) -> bool method is not implemented for torchlite.FloatStorage.");
            }

            /// <summary>
            /// Removes the IList&lt;T&gt; item at the specified index.
            /// </summary>
            /// <param name="index">The zero-based index of the item to remove.</param>
            [Obsolete("IList<float>.RemoveAt(int) -> void method is not implemented for torchlite.FloatStorage.", true)]
            void IList<float>.RemoveAt(int index)
            {
                throw new NotSupportedException("IList<float>.RemoveAt(int) -> void method is not implemented for torchlite.FloatStorage.");
            }

            #endregion

        }

    }

}