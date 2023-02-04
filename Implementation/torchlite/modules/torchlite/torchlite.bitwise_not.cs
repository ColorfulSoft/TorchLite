//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

namespace System.AI.Experimental
{

    public unsafe static partial class torchlite
    {

        /// <summary>
        /// Computes the bitwise NOT of the given input tensor.
        /// The input tensor must be of integral or Boolean types.
        /// For bool tensors, it computes the logical NOT.
        /// </summary>
        /// <param name="input">Input tensor.</param>
        /// <returns>Tensor.</returns>
        public static Tensor bitwise_not(this Tensor input)
        {
            return ~input;
        }

    }

}