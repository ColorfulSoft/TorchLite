//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        /// <summary>
        /// Computes the bitwise OR of input and other.
        /// The input tensor must be of integral or Boolean types.
        /// For bool tensors, it computes the logical OR.
        /// </summary>
        /// <param name="input">The first input tensor.</param>
        /// <param name="other">The second input tensor.</param>
        /// <returns>Tensor.</returns>
        public static Tensor bitwise_or(this Tensor input, Tensor other)
        {
            return input | other;
        }

    }

}