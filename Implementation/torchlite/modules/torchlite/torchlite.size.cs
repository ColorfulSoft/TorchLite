//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        /// <summary>
        /// Returns the size of the data type in bytes.
        /// </summary>
        /// <param name="dtype">Data type.</param>
        /// <returns>Size in bytes.</returns>
        public static int size(this DType dtype)
        {
            switch(dtype)
            {
                case torchlite.@bool:
                {
                    return 1;
                }
                case torchlite.float32:
                case torchlite.int32:
                {
                    return 4;
                }
                default:
                {
                    throw new TypeAccessException(string.Format("Unknown data type with code {0}.", (byte)dtype));
                }
            }
        }

    }

}