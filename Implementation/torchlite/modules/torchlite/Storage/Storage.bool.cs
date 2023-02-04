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
            /// Converts the current storage to the torchlite.bool data type.
            /// </summary>
            /// <returns>BoolStorage.</returns>
            public Storage @bool()
            {
                var s = new Storage(this.size, torchlite.@bool);
                var dst = (bool*)s.data_ptr;
                var n = this.size;
                switch(this.dtype)
                {
                    case torchlite.float32:
                    {
                        var src = (float*)this.data_ptr;
                        for(int i = 0; i < n; ++i)
                        {
                            dst[i] = src[i] != 0;
                        }
                        return s;
                    }
                    case torchlite.int32:
                    {
                        var src = (int*)this.data_ptr;
                        for(int i = 0; i < n; ++i)
                        {
                            dst[i] = src[i] != 0;
                        }
                        return s;
                    }
                    case torchlite.@bool:
                    {
                        var src = (bool*)this.data_ptr;
                        for(int i = 0; i < n; ++i)
                        {
                            dst[i] = src[i];
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