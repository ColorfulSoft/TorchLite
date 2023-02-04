//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public unsafe partial class Size
        {

            /// <summary>
            /// Compares two sizes.
            /// </summary>
            /// <param name="left">Left size object.</param>
            /// <param name="right">Right size object.</param>
            /// <returns>True if sizes are equal.</returns>
            public static bool operator ==(Size left, Size right)
            {
                if(left.ndim != right.ndim)
                {
                    return false;
                }
                for(int i = 0; i < left.ndim; ++i)
                {
                    if(left.data_ptr[i] != right.data_ptr[i])
                    {
                        return false;
                    }
                }
                return true;
            }

        }

    }

}