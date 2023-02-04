//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        /// <summary>
        /// Checks whether the specified data type is a boolean.
        /// </summary>
        /// <param name="dtype">Data type.</param>
        /// <returns>true, if dtype is boolean.</returns>
        public static bool is_boolean(this DType dtype)
        {
            return dtype == torchlite.@bool;
        }

    }

}