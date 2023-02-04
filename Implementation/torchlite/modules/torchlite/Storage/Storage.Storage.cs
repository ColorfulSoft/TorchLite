//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System;
using System.Runtime.InteropServices;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public unsafe partial class Storage
        {

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

            /// <summary>
            /// Initializes the Storage object from the .NET array.
            /// </summary>
            /// <param name="array">.NET Array of float, int or bool data type.</param>
            /// <param name="dtype">Target data type. If null, inherited from array. Default: null.</param>
            public Storage(Array array, DType? dtype = null)
            {
                var element_type = array.GetType().GetElementType();
                var handle = GCHandle.Alloc(array, GCHandleType.Pinned);
                if(element_type == typeof(float))
                {
                    var src = (float*)handle.AddrOfPinnedObject();
                    var n = array.Length;
                    this.size = n;
                    this.dtype = dtype ?? torchlite.float32;
                    this.data_ptr = Marshal.AllocCoTaskMem(n * this.dtype.size()).ToPointer();
                    switch(this.dtype)
                    {
                        case torchlite.float32:
                        {
                            var dst = (float*)this.data_ptr;
                            for(int i = 0; i < n; ++i)
                            {
                                dst[i] = src[i];
                            }
                            break;
                        }
                        case torchlite.int32:
                        {
                            var dst = (int*)this.data_ptr;
                            for(int i = 0; i < n; ++i)
                            {
                                dst[i] = (int)src[i];
                            }
                            break;
                        }
                        case torchlite.@bool:
                        {
                            var dst = (bool*)this.data_ptr;
                            for(int i = 0; i < n; ++i)
                            {
                                dst[i] = src[i] != 0;
                            }
                            break;
                        }
                    }
                    handle.Free();
                    return;
                }
                if(array.GetType().GetElementType() == typeof(int))
                {
                    var src = (int*)handle.AddrOfPinnedObject();
                    var n = array.Length;
                    this.size = n;
                    this.dtype = dtype ?? torchlite.int32;
                    this.data_ptr = Marshal.AllocCoTaskMem(n * this.dtype.size()).ToPointer();
                    switch(this.dtype)
                    {
                        case torchlite.float32:
                        {
                            var dst = (float*)this.data_ptr;
                            for(int i = 0; i < n; ++i)
                            {
                                dst[i] = src[i];
                            }
                            break;
                        }
                        case torchlite.int32:
                        {
                            var dst = (int*)this.data_ptr;
                            for(int i = 0; i < n; ++i)
                            {
                                dst[i] = src[i];
                            }
                            break;
                        }
                        case torchlite.@bool:
                        {
                            var dst = (bool*)this.data_ptr;
                            for(int i = 0; i < n; ++i)
                            {
                                dst[i] = src[i] != 0;
                            }
                            break;
                        }
                    }
                    handle.Free();
                    return;
                }
                if(array.GetType().GetElementType() == typeof(bool))
                {
                    var src = (bool*)handle.AddrOfPinnedObject();
                    var n = array.Length;
                    this.size = n;
                    this.dtype = dtype ?? torchlite.@bool;
                    this.data_ptr = Marshal.AllocCoTaskMem(n * this.dtype.size()).ToPointer();
                    switch(this.dtype)
                    {
                        case torchlite.float32:
                        {
                            var dst = (float*)this.data_ptr;
                            for(int i = 0; i < n; ++i)
                            {
                                dst[i] = src[i] ? 1 : 0;
                            }
                            break;
                        }
                        case torchlite.int32:
                        {
                            var dst = (int*)this.data_ptr;
                            for(int i = 0; i < n; ++i)
                            {
                                dst[i] = src[i] ? 1 : 0;
                            }
                            break;
                        }
                        case torchlite.@bool:
                        {
                            var dst = (bool*)this.data_ptr;
                            for(int i = 0; i < n; ++i)
                            {
                                dst[i] = src[i];
                            }
                            break;
                        }
                    }
                    handle.Free();
                    return;
                }
                throw new ArgumentException("Array should be of float, int or bool data type.");
            }

        }

    }

}