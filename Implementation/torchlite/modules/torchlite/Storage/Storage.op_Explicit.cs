//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System.Runtime.InteropServices;
namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public unsafe partial class Storage
        {

            /// <summary>
            /// Explicitly converts the Storage object to a float .NET array.
            /// </summary>
            /// <param name="storage">Storage object.</param>
            /// <returns>.NET float array.</returns>
            public static explicit operator float[](Storage storage)
            {
                var array = new float[storage.size];
                var n = storage.size;
                fixed(float* dst = array)
                {
                    switch(storage.dtype)
                    {
                        case torchlite.float32:
                        {
                            var src = (float*)storage.data_ptr;
                            for(int i = 0; i < n; ++i)
                            {
                                dst[i] = src[i];
                            }
                            break;
                        }
                        case torchlite.int32:
                        {
                            var src = (int*)storage.data_ptr;
                            for(int i = 0; i < n; ++i)
                            {
                                dst[i] = src[i];
                            }
                            break;
                        }
                        case torchlite.@bool:
                        {
                            var src = (bool*)storage.data_ptr;
                            for(int i = 0; i < n; ++i)
                            {
                                dst[i] = src[i] ? 1 : 0;
                            }
                            break;
                        }
                    }
                }
                return array;
            }

            /// <summary>
            /// Explicitly converts the Storage object to an int .NET array.
            /// </summary>
            /// <param name="storage">Storage object.</param>
            /// <returns>.NET int array.</returns>
            public static explicit operator int[](Storage storage)
            {
                var array = new int[storage.size];
                var n = storage.size;
                fixed(int* dst = array)
                {
                    switch(storage.dtype)
                    {
                        case torchlite.float32:
                        {
                            var src = (float*)storage.data_ptr;
                            for(int i = 0; i < n; ++i)
                            {
                                dst[i] = (int)src[i];
                            }
                            break;
                        }
                        case torchlite.int32:
                        {
                            var src = (int*)storage.data_ptr;
                            for(int i = 0; i < n; ++i)
                            {
                                dst[i] = src[i];
                            }
                            break;
                        }
                        case torchlite.@bool:
                        {
                            var src = (bool*)storage.data_ptr;
                            for(int i = 0; i < n; ++i)
                            {
                                dst[i] = src[i] ? 1 : 0;
                            }
                            break;
                        }
                    }
                }
                return array;
            }

            /// <summary>
            /// Explicitly converts the Storage object to a bool .NET array.
            /// </summary>
            /// <param name="storage">Storage object.</param>
            /// <returns>.NET bool array.</returns>
            public static explicit operator bool[](Storage storage)
            {
                var array = new bool[storage.size];
                var n = storage.size;
                fixed(bool* dst = array)
                {
                    switch(storage.dtype)
                    {
                        case torchlite.float32:
                        {
                            var src = (float*)storage.data_ptr;
                            for(int i = 0; i < n; ++i)
                            {
                                dst[i] = src[i] != 0;
                            }
                            break;
                        }
                        case torchlite.int32:
                        {
                            var src = (int*)storage.data_ptr;
                            for(int i = 0; i < n; ++i)
                            {
                                dst[i] = src[i] != 0;
                            }
                            break;
                        }
                        case torchlite.@bool:
                        {
                            var src = (bool*)storage.data_ptr;
                            for(int i = 0; i < n; ++i)
                            {
                                dst[i] = src[i];
                            }
                            break;
                        }
                    }
                }
                return array;
            }

            /// <summary>
            /// Explicitly converts the Storage object to a .NET array.
            /// </summary>
            /// <param name="storage">Storage object.</param>
            /// <returns>.NET array.</returns>
            public static explicit operator Array(Storage storage)
            {
                var array = Array.CreateInstance(storage.dtype.dotnet(), storage.size);
                var src = (byte*)storage.data_ptr;
                var handle = GCHandle.Alloc(array, GCHandleType.Pinned);
                var dst = (byte*)handle.AddrOfPinnedObject();
                var nbytes = storage.size * storage.dtype.size();
                for(int i = 0; i < nbytes; ++i)
                {
                    dst[i] = src[i];
                }
                handle.Free();
                return array;
            }

        }

    }

}