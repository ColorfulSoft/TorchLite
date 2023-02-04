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
        /// Returns the corresponding .NET data type.
        /// </summary>
        /// <param name="dtype">Data type.</param>
        /// <returns>.NET data type.</returns>
        public static Type dotnet(this DType dtype)
        {
            switch(dtype)
            {
                case torchlite.float32:
                {
                    return typeof(float);
                }
                case torchlite.int32:
                {
                    return typeof(int);
                }
                case torchlite.@bool:
                {
                    return typeof(bool);
                }
                default:
                {
                    throw new TypeAccessException(string.Format("Unknown data type with code {0}.", (byte)dtype));
                }
            }
        }

    }

}