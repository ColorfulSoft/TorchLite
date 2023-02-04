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

        }

    }

}