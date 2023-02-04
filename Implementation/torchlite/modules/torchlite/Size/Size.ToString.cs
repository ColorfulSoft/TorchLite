//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public unsafe partial class Size
        {

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

        }

    }

}