//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        /// <summary>
        /// Element-wise arctangent of input_i / other_i with consideration of the quadrant.
        /// </summary>
        /// <param name="input">The first input tensor.</param>
        /// <param name="other">The second input tensor.</param>
        /// <returns>Tensor.</returns>
        public static Tensor atan2(this Tensor input, Tensor other)
        {
            return torchlite.atan(input / other);
        }

    }

}