//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        /// <summary>
        /// Returns true if obj is a TorchLite tensor.
        /// </summary>
        /// <param name="obj">Object to test.</param>
        /// <returns>obj is Tensor</returns>
        public static bool is_tensor(this object obj)
        {
            return obj is Tensor;
        }

    }

}