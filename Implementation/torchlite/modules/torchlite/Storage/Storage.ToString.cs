//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public unsafe partial class Storage
        {

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

        }

    }

}