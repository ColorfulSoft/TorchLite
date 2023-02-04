//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public unsafe partial class Storage
        {

            /// <summary>
            /// Converts the current storage to the torchlite.float32 data type.
            /// </summary>
            /// <returns>FloatStorage.</returns>
            public Storage @float()
            {
                var s = new Storage(this.size, torchlite.float32);
                var dst = (float*)s.data_ptr;
                var n = this.size;
                switch(this.dtype)
                {
                    case torchlite.float32:
                    {
                        var src = (float*)this.data_ptr;
                        for(int i = 0; i < n; ++i)
                        {
                            dst[i] = src[i];
                        }
                        return s;
                    }
                    case torchlite.int32:
                    {
                        var src = (int*)this.data_ptr;
                        for(int i = 0; i < n; ++i)
                        {
                            dst[i] = src[i];
                        }
                        return s;
                    }
                    case torchlite.@bool:
                    {
                        var src = (bool*)this.data_ptr;
                        for(int i = 0; i < n; ++i)
                        {
                            dst[i] = src[i] ? 1 : 0;
                        }
                        return s;
                    }
                    default:
                    {
                        throw new TypeAccessException(string.Format("Invalid type code {0}.", (byte)this.dtype));
                    }
                }
            }

        }

    }

}