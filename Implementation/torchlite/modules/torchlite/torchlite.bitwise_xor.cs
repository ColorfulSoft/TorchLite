//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        /// <summary>
        /// Computes the bitwise XOR of input and other.
        /// The input tensor must be of integral or Boolean types.
        /// For bool tensors, it computes the logical XOR.
        /// </summary>
        /// <param name="input">The first input tensor.</param>
        /// <param name="other">The second input tensor.</param>
        /// <returns>Tensor.</returns>
        public static Tensor bitwise_xor(this Tensor input, Tensor other)
        {
            if(input.dtype.is_floating_point() || other.dtype.is_floating_point())
            {
                throw new ArgumentException("torchlite.bitwise_xor is not implemented for floating point tensors.");
            }
            return input ^ other;
        }

    }

}