//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System.Runtime.InteropServices;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public unsafe partial class Tensor
        {

            /// <summary>
            /// Returns a Tensor object for the specified .NET array.
            /// </summary>
            /// <param name="array">.NET multidimensional array of float, int or bool type.</param>
            /// <param name="dtype">Target tensor's data type.</param>
            /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
            /// <returns>Tensor.</returns>
            public static Tensor from_dotnet(Array array,
                                             DType? dtype = null,
                                             bool requires_grad = false)
            {
                var shape = new int[array.Rank];
                for(int i = 0; i < shape.Length; ++i)
                {
                    shape[i] = array.GetLength(i);
                }
                Tensor t = null;
                var n = array.Length;
                var handle = GCHandle.Alloc(array, GCHandleType.Pinned);
                if(array.GetType().GetElementType() == typeof(float))
                {
                    var src = (float*)handle.AddrOfPinnedObject();
                    dtype = dtype ?? torchlite.float32;
                    t = new Tensor(shape, dtype, requires_grad);
                    switch(dtype)
                    {
                        case torchlite.float32:
                        {
                            var dst = (float*)t.storage.data_ptr;
                            for(int i = 0; i < n; ++i)
                            {
                                dst[i] = src[i];
                            }
                            break;
                        }
                        case torchlite.int32:
                        {
                            var dst = (int*)t.storage.data_ptr;
                            for(int i = 0; i < n; ++i)
                            {
                                dst[i] = (int)src[i];
                            }
                            break;
                        }
                        case torchlite.@bool:
                        {
                            var dst = (bool*)t.storage.data_ptr;
                            for(int i = 0; i < n; ++i)
                            {
                                dst[i] = src[i] != 0;
                            }
                            break;
                        }
                    }
                    handle.Free();
                    return t;
                }
                if(array.GetType().GetElementType() == typeof(int))
                {
                    var src = (int*)handle.AddrOfPinnedObject();
                    dtype = dtype ?? torchlite.int32;
                    t = new Tensor(shape, dtype, requires_grad);
                    switch(dtype)
                    {
                        case torchlite.float32:
                        {
                            var dst = (float*)t.storage.data_ptr;
                            for(int i = 0; i < n; ++i)
                            {
                                dst[i] = src[i];
                            }
                            break;
                        }
                        case torchlite.int32:
                        {
                            var dst = (int*)t.storage.data_ptr;
                            for(int i = 0; i < n; ++i)
                            {
                                dst[i] = src[i];
                            }
                            break;
                        }
                        case torchlite.@bool:
                        {
                            var dst = (bool*)t.storage.data_ptr;
                            for(int i = 0; i < n; ++i)
                            {
                                dst[i] = src[i] != 0;
                            }
                            break;
                        }
                    }
                    handle.Free();
                    return t;
                }
                if(array.GetType().GetElementType() == typeof(bool))
                {
                    var src = (bool*)handle.AddrOfPinnedObject();
                    dtype = dtype ?? torchlite.@bool;
                    t = new Tensor(shape, dtype, requires_grad);
                    switch(dtype)
                    {
                        case torchlite.float32:
                        {
                            var dst = (float*)t.storage.data_ptr;
                            for(int i = 0; i < n; ++i)
                            {
                                dst[i] = src[i] ? 1 : 0;
                            }
                            break;
                        }
                        case torchlite.int32:
                        {
                            var dst = (int*)t.storage.data_ptr;
                            for(int i = 0; i < n; ++i)
                            {
                                dst[i] = src[i] ? 1 : 0;
                            }
                            break;
                        }
                        case torchlite.@bool:
                        {
                            var dst = (bool*)t.storage.data_ptr;
                            for(int i = 0; i < n; ++i)
                            {
                                dst[i] = src[i];
                            }
                            break;
                        }
                    }
                    handle.Free();
                    return t;
                }
                handle.Free();
                throw new ArgumentException("Array should be of float, int or bool data type.");
            }

        }

    }

}