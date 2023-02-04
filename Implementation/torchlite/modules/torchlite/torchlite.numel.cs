//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        /// <summary>
        /// Calculates the number of elements in the tensor.
        /// </summary>
        /// <param name="input">Tensor.</param>
        /// <returns>Number of elements in the input tensor.</returns>
        public static int numel(this Tensor input)
        {
            return input.shape.numel();
        }

    }

}