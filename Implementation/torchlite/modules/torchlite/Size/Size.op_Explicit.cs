//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System.Collections.Generic;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public partial class Size
        {

            /// <summary>
            /// Explicitly converts a Size object to an int array.
            /// </summary>
            /// <param name="size">Size object.</param>
            /// <returns>System.Int32 array.</returns>
            public static explicit operator int[](Size size)
            {
                var arr = new int[size.ndim];
                ((IList<int>)size).CopyTo(arr, 0);
                return arr;
            }

        }

    }

}