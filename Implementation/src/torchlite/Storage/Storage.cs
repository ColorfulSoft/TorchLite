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
        /// A torchlite.Storage is a contiguous, one-dimensional array of elements of a particular
        /// torchlite.dtype. It can be given any torchlite.dtype, and the internal data will be
        /// interpreted appropriately.
        /// </summary>
        public unsafe class Storage : IList<object>
        {

            #region properties

            /// <summary>
            /// A pointer to storage's data.
            /// </summary>
            public void* data_ptr
            {

                get;

                private set;

            }

            /// <summary>
            /// Storage's size.
            /// </summary>
            public int size
            {

                get;

                private set;

            }

            /// <summary>
            /// Storage's data type.
            /// </summary>
            public DType dtype
            {

                get;

                private set;

            }

            /// <summary>
            /// Gets or sets the element at the specified index.
            /// </summary>
            public object this[int index]
            {

                get
                {
                    if((index < 0) || (index >= this.size))
                    {
                        throw new IndexOutOfRangeException(string.Format("Index {0} is out of range of a storage of size {1}.", index, this.size));
                    }
                    switch(this.dtype)
                    {
                        case torchlite.float32:
                        {
                            return *((float*)this.data_ptr + index);
                        }
                        case torchlite.int32:
                        {
                            return *((int*)this.data_ptr + index);
                        }
                        case torchlite.@bool:
                        {
                            return *((bool*)this.data_ptr + index);
                        }
                        default:
                        {
                            throw new TypeAccessException(string.Format("Invalid type code {0}.", (byte)this.dtype));
                        }
                    }
                }

                set
                {
                    if((index < 0) || (index >= this.size))
                    {
                        throw new IndexOutOfRangeException(string.Format("Index {0} is out of range of a storage of size {1}.", index, this.size));
                    }
                    switch(this.dtype)
                    {
                        case torchlite.float32:
                        {
                            *((float*)this.data_ptr + index) = Convert.ToSingle(value);
                            return;
                        }
                        case torchlite.int32:
                        {
                            *((int*)this.data_ptr + index) = Convert.ToInt32(value);
                            return;
                        }
                        case torchlite.@bool:
                        {
                            *((bool*)this.data_ptr + index) = Convert.ToBoolean(value);
                            return;
                        }
                        default:
                        {
                            throw new TypeAccessException(string.Format("Invalid type code {0}.", (byte)this.dtype));
                        }
                    }
                }

            }

            /// <summary>
            /// Gets the number of elements contained in the ICollection&lt;T&gt;.
            /// </summary>
            int ICollection<object>.Count
            {

                get
                {
                    return this.size;
                }

            }

            /// <summary>
            /// Gets a value indicating whether the ICollection&lt;T&gt; is read-only.
            /// </summary>
            bool ICollection<object>.IsReadOnly
            {

                get
                {
                    return false;
                }

            }

            #endregion

            #region constructors

            /// <summary>
            /// Initializes the storage of the specified size and data type.
            /// </summary>
            /// <param name="size">The number of elements in the storage.</param>
            /// <param name="dtype">Storage data type. If null, torchlite.default_dtype will be set. Default: null.</param>
            public Storage(int size, DType? dtype = null)
            {
                this.dtype = dtype ?? torchlite.default_dtype;
                this.size = size;
                this.data_ptr = Marshal.AllocCoTaskMem(size * this.dtype.size()).ToPointer();
            }

            #endregion

            #region destructor

            /// <summary>
            /// Releases unmanaged resources.
            /// </summary>
            ~Storage()
            {
                if(this.data_ptr != null)
                {
                    Marshal.FreeCoTaskMem((IntPtr)this.data_ptr);
                    this.data_ptr = null;
                }
            }

            #endregion

            #region methods

            /// <summary>
            /// Adds an item to the ICollection&lt;T&gt;.
            /// </summary>
            /// <param name="item">The object to add to the ICollection&lt;T&gt;.</param>
            [Obsolete("ICollection<object>.Add(object) -> void method is not implemented for torchlite.Storage.", true)]
            void ICollection<object>.Add(object item)
            {
                throw new NotSupportedException("ICollection<object>.Add(object) -> void method is not implemented for torchlite.Storage.");
            }

            /// <summary>
            /// Removes all items from the ICollection&ltT&gt.
            /// </summary>
            [Obsolete("ICollection<object>.Clear() -> void method is not implemented for torchlite.Storage.", true)]
            void ICollection<object>.Clear()
            {
                throw new NotSupportedException("ICollection<object>.Clear() -> void method is not implemented for torchlite.Storage.");
            }

            /// <summary>
            /// Determines whether the ICollection&lt;T&gt; contains a specific value.
            /// </summary>
            /// <param name="item">The object to locate in the ICollection&lt;T&gt;.</param>
            /// <returns>true if item is found in the ICollection&lt;T&gt;; otherwise, false.</returns>
            [Obsolete("ICollection<object>.Contains(object) -> bool method is not implemented for torchlite.Storage.", true)]
            bool ICollection<object>.Contains(object item)
            {
                throw new NotSupportedException("ICollection<object>.Contains(object) -> bool method is not implemented for torchlite.Storage.");
            }

            /// <summary>
            /// Copies the elements of the ICollection&lt;T&gt; to an Array, starting at a particular Array index.
            /// </summary>
            /// <param name="array">The one-dimensional Array that is the destination of the elements copied from ICollection&lt;T&gt;. The Array must have zero-based indexing.</param>
            /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
            void ICollection<object>.CopyTo(object[] array, int arrayIndex)
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
                switch(this.dtype)
                {
                    case torchlite.float32:
                    {
                        var ptr = (float*)this.data_ptr;
                        for(int i = 0; i < this.size; ++i)
                        {
                            array[arrayIndex + i] = ptr[i];
                        }
                        return;
                    }
                    case torchlite.int32:
                    {
                        var ptr = (int*)this.data_ptr;
                        for(int i = 0; i < this.size; ++i)
                        {
                            array[arrayIndex + i] = ptr[i];
                        }
                        return;
                    }
                    case torchlite.@bool:
                    {
                        var ptr = (bool*)this.data_ptr;
                        for(int i = 0; i < this.size; ++i)
                        {
                            array[arrayIndex + i] = ptr[i];
                        }
                        return;
                    }
                    default:
                    {
                        throw new TypeAccessException(string.Format("Invalid type code {0}.", (byte)this.dtype));
                    }
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
            IEnumerator<object> IEnumerable<object>.GetEnumerator()
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
            [Obsolete("IList<object>.Contains(object) -> int method is not implemented for torchlite.Storage.", true)]
            int IList<object>.IndexOf(object item)
            {
                throw new NotSupportedException("IList<object>.Contains(object) -> int method is not implemented for torchlite.Storage.");
            }

            /// <summary>
            /// Inserts an item to the IList&lt;T&gt; at the specified index.
            /// </summary>
            /// <param name="index">The zero-based index at which item should be inserted.</param>
            /// <param name="item">The object to insert into the IList&lt;T&gt;.</param>
            [Obsolete("IList<object>.Insert(int, object) -> void method is not implemented for torchlite.Storage.", true)]
            void IList<object>.Insert(int index, object item)
            {
                throw new NotSupportedException("IList<object>.Insert(int, object) -> void method is not implemented for torchlite.Storage.");
            }

            /// <summary>
            /// Removes the first occurrence of a specific object from the ICollection&lt;T&gt;.
            /// </summary>
            /// <param name="item">The object to remove from the ICollection&lt;T&gt;.</param>
            /// <returns>true if item was successfully removed from the ICollection&lt;T&gt;; otherwise, false. This method also returns false if item is not found in the original ICollection&lt;T&gt;.</returns>
            [Obsolete("ICollection<object>.Remove(object) -> bool method is not implemented for torchlite.Storage.", true)]
            bool ICollection<object>.Remove(object item)
            {
                throw new NotSupportedException("ICollection<object>.Remove(object) -> bool method is not implemented for torchlite.Storage.");
            }

            /// <summary>
            /// Removes the IList&lt;T&gt; item at the specified index.
            /// </summary>
            /// <param name="index">The zero-based index of the item to remove.</param>
            [Obsolete("IList<object>.RemoveAt(int) -> void method is not implemented for torchlite.Storage.", true)]
            void IList<object>.RemoveAt(int index)
            {
                throw new NotSupportedException("IList<object>.RemoveAt(int) -> void method is not implemented for torchlite.Storage.");
            }

            /// <summary>
            /// Converts the current storage object to a string representation.
            /// </summary>
            /// <returns>String representation of the storage.</returns>
            public override string ToString()
            {
                string str = "";
                switch(this.dtype)
                {
                    case torchlite.float32:
                    {
                        var ptr = (float*)this.data_ptr;
                        for(int i = 0; i < this.size; ++i)
                        {
                            str += string.Format(" {0}\n", ptr[i]);
                        }
                        str += string.Format("[torchlite.FloatStorage of size {0}]", this.size);
                        return str;
                    }
                    case torchlite.int32:
                    {
                        var ptr = (int*)this.data_ptr;
                        for(int i = 0; i < this.size; ++i)
                        {
                            str += string.Format(" {0}\n", ptr[i]);
                        }
                        str += string.Format("[torchlite.IntStorage of size {0}]", this.size);
                        return str;
                    }
                    case torchlite.@bool:
                    {
                        var ptr = (bool*)this.data_ptr;
                        for(int i = 0; i < this.size; ++i)
                        {
                            str += string.Format(" {0}\n", ptr[i]);
                        }
                        str += string.Format("[torchlite.BoolStorage of size {0}]", this.size);
                        return str;
                    }
                    default:
                    {
                        throw new TypeAccessException(string.Format("Invalid type code {0}.", (byte)this.dtype));
                    }
                }
            }

            #endregion

        }

    }

}