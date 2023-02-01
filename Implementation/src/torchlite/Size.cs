//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        /// <summary>
        /// Represents the dimensions of the tensor.
        /// </summary>
        public unsafe sealed class Size : IList<int>
        {

            #region properties

            /// <summary>
            /// A pointer to dimensions array.
            /// </summary>
            public int* data_ptr
            {

                get;

                private set;

            }

            /// <summary>
            /// The number of dimensions.
            /// </summary>
            public int ndim
            {

                get;

                private set;

            }

            /// <summary>
            /// Gets or sets the element at the specified index.
            /// </summary>
            public int this[int index]
            {

                get
                {
                    if((index < 0) || (index >= this.ndim))
                    {
                        throw new ArgumentOutOfRangeException(string.Format("Index {0} is out of range of a storage of size {1}.", index, this.ndim));
                    }
                    return this.data_ptr[index];
                }

                set
                {
                    throw new NotSupportedException("torchlite.Size is immutable.");
                }

            }

            /// <summary>
            /// Gets the number of elements contained in the ICollection&lt;T&gt;.
            /// </summary>
            int ICollection<int>.Count
            {

                get
                {
                    return this.ndim;
                }

            }

            /// <summary>
            /// Gets a value indicating whether the ICollection&lt;T&gt; is read-only.
            /// </summary>
            bool ICollection<int>.IsReadOnly
            {

                get
                {
                    return true;
                }

            }

            #endregion

            #region constructors

            /// <summary>
            /// Creates a size with dimensions from an IList&lt;int&gt; object.
            /// </summary>
            /// <param name="shape">Dimensions.</param>
            public Size(IList<int> shape)
            {
                if(shape == null)
                {
                    this.ndim = 0;
                    this.data_ptr = null;
                    return;
                }
                if(shape.Count > 8)
                {
                    throw new ArgumentException("TorchLite does not support tensors with more than 8 dimensions. Use Torch.NET instead.");
                }
                this.ndim = shape.Count;
                this.data_ptr = (int*)Marshal.AllocHGlobal(shape.Count * sizeof(int));
                for(int i = 0; i < this.ndim; ++i)
                {
                    if(shape[i] <= 0)
                    {
                        throw new ArgumentException(string.Format("Value {0} is invalid for {1} dimension.", shape[i], i));
                    }
                    this.data_ptr[i] = shape[i];
                }
            }

            /// <summary>
            /// Creates a size with specified dimensions.
            /// </summary>
            /// <param name="shape">Dimensions.</param>
            public Size(params int[] shape) : this((IList<int>)shape)
            {
            }

            #endregion

            #region destructor

            /// <summary>
            /// Releases unmanaged resources.
            /// </summary>
            ~Size()
            {
                if(this.data_ptr != null)
                {
                    Marshal.FreeHGlobal((IntPtr)this.data_ptr);
                    this.data_ptr = null;
                }
            }

            #endregion

            #region methods

            /// <summary>
            /// Calculates the number of elements in the tensor with the current size.
            /// </summary>
            /// <returns>Number of elements.</returns>
            public int numel()
            {
                int numel = 1;
                for(int i = 0; i < this.ndim; ++i)
                {
                    numel *= this.data_ptr[i];
                }
                return numel;
            }

            /// <summary>
            /// Adds an item to the ICollection&lt;T&gt;.
            /// </summary>
            /// <param name="item">The object to add to the ICollection&lt;T&gt;.</param>
            [Obsolete("ICollection<int>.Add(int) -> void method is not implemented for torchlite.Size.", true)]
            void ICollection<int>.Add(int item)
            {
                throw new NotSupportedException("ICollection<int>.Add(int) -> void method is not implemented for torchlite.Size.");
            }

            /// <summary>
            /// Removes all items from the ICollection&ltT&gt.
            /// </summary>
            [Obsolete("ICollection<int>.Clear() -> void method is not implemented for torchlite.Size.", true)]
            void ICollection<int>.Clear()
            {
                throw new NotSupportedException("ICollection<int>.Clear() -> void method is not implemented for torchlite.Size.");
            }

            /// <summary>
            /// Determines whether the ICollection&lt;T&gt; contains a specific value.
            /// </summary>
            /// <param name="item">The object to locate in the ICollection&lt;T&gt;.</param>
            /// <returns>true if item is found in the ICollection&lt;T&gt;; otherwise, false.</returns>
            [Obsolete("ICollection<int>.Contains(int) -> bool method is not implemented for torchlite.Size.", true)]
            bool ICollection<int>.Contains(int item)
            {
                throw new NotSupportedException("ICollection<int>.Contains(int) -> bool method is not implemented for torchlite.Size.");
            }

            /// <summary>
            /// Copies the elements of the ICollection&lt;T&gt; to an Array, starting at a particular Array index.
            /// </summary>
            /// <param name="array">The one-dimensional Array that is the destination of the elements copied from ICollection&lt;T&gt;. The Array must have zero-based indexing.</param>
            /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
            void ICollection<int>.CopyTo(int[] array, int arrayIndex)
            {
                if(array == null)
                {
                    throw new ArgumentNullException("array is null.");
                }
                if(arrayIndex < 0)
                {
                    throw new ArgumentOutOfRangeException("arrayIndex is less than 0.");
                }
                if((this.ndim + arrayIndex) >= array.Length)
                {
                    throw new ArgumentException("The number of elements in the source ICollection<T> is greater than the available space from arrayIndex to the end of the destination array.");
                }
                for(int i = 0; i < this.ndim; ++i)
                {
                    array[arrayIndex + i] = this.data_ptr[i];
                }
            }

            /// <summary>
            /// Returns an enumerator that iterates through a collection.
            /// </summary>
            /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
            IEnumerator IEnumerable.GetEnumerator()
            {
                for(int i = 0; i < this.ndim; ++i)
                {
                    yield return this[i];
                }
            }

            /// <summary>
            /// Returns an enumerator that iterates through a collection.
            /// </summary>
            /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
            IEnumerator<int> IEnumerable<int>.GetEnumerator()
            {
                for(int i = 0; i < this.ndim; ++i)
                {
                    yield return this[i];
                }
            }

            /// <summary>
            /// Determines the index of a specific item in the IList&lt;T&gt;.
            /// </summary>
            /// <param name="item">The object to locate in the IList&lt;T&gt;.</param>
            /// <returns>The index of item if found in the list; otherwise, -1.</returns>
            [Obsolete("IList<int>.Contains(int) -> int method is not implemented for torchlite.Size.", true)]
            int IList<int>.IndexOf(int item)
            {
                throw new NotSupportedException("IList<int>.Contains(int) -> int method is not implemented for torchlite.Size.");
            }

            /// <summary>
            /// Inserts an item to the IList&lt;T&gt; at the specified index.
            /// </summary>
            /// <param name="index">The zero-based index at which item should be inserted.</param>
            /// <param name="item">The object to insert into the IList&lt;T&gt;.</param>
            [Obsolete("IList<int>.Insert(int, int) -> void method is not implemented for torchlite.Size.", true)]
            void IList<int>.Insert(int index, int item)
            {
                throw new NotSupportedException("IList<int>.Insert(int, int) -> void method is not implemented for torchlite.Size.");
            }

            /// <summary>
            /// Removes the first occurrence of a specific object from the ICollection&lt;T&gt;.
            /// </summary>
            /// <param name="item">The object to remove from the ICollection&lt;T&gt;.</param>
            /// <returns>true if item was successfully removed from the ICollection&lt;T&gt;; otherwise, false. This method also returns false if item is not found in the original ICollection&lt;T&gt;.</returns>
            [Obsolete("ICollection<int>.Remove(int) -> bool method is not implemented for torchlite.Size.", true)]
            bool ICollection<int>.Remove(int item)
            {
                throw new NotSupportedException("ICollection<int>.Remove(int) -> bool method is not implemented for torchlite.Size.");
            }

            /// <summary>
            /// Removes the IList&lt;T&gt; item at the specified index.
            /// </summary>
            /// <param name="index">The zero-based index of the item to remove.</param>
            [Obsolete("IList<int>.RemoveAt(int) -> void method is not implemented for torchlite.Size.", true)]
            void IList<int>.RemoveAt(int index)
            {
                throw new NotSupportedException("IList<int>.RemoveAt(int) -> void method is not implemented for torchlite.Size.");
            }

            /// <summary>
            /// Converts the current size object to a string representation.
            /// </summary>
            /// <returns>String representation of the size.</returns>
            public override string ToString()
            {
                string str = "torch.Size([";
                for(int i = 0; i < this.ndim; ++i)
                {
                    str += this.data_ptr[i].ToString();
                    if((i + 1) < this.ndim)
                    {
                        str += ", ";
                    }
                }
                return str + "])";
            }

            #endregion

            #region operators

            /// <summary>
            /// Compares two sizes.
            /// </summary>
            /// <param name="left">Left size object.</param>
            /// <param name="right">Right size object.</param>
            /// <returns>True if sizes are equal.</returns>
            public static bool operator ==(Size left, Size right)
            {
                if(left.ndim != right.ndim)
                {
                    return false;
                }
                for(int i = 0; i < left.ndim; ++i)
                {
                    if(left.data_ptr[i] != right.data_ptr[i])
                    {
                        return false;
                    }
                }
                return true;
            }

            /// <summary>
            /// Compares two sizes.
            /// </summary>
            /// <param name="left">Left size object.</param>
            /// <param name="right">Right size object.</param>
            /// <returns>True if sizes are not equal.</returns>
            public static bool operator !=(Size left, Size right)
            {
                return !(left == right);
            }

            #endregion

        }

    }

}