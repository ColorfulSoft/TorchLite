//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System;
using System.Collections.Generic;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        /// <summary>
        /// A torchlite.Storage is a contiguous, one-dimensional array of elements of a particular
        /// torchlite.dtype. It can be given any torchlite.dtype, and the internal data will be
        /// interpreted appropriately.
        /// </summary>
        public unsafe partial class Storage : IList<object>
        {

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

        }

    }

}