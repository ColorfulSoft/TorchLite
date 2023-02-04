//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        /// <summary>
        /// Performs the element-wise division of tensor1 by tensor2,
        /// multiply the result by the scalar value and add it to input.
        /// </summary>
        /// <param name="input">The tensor to be added.</param>
        /// <param name="tensor1">The numerator tensor.</param>
        /// <param name="tensor2">The denominator tensor.</param>
        /// <param name="value">Multiplier for tensor1/tensor2.</param>
        /// <returns>Tensor.</returns>
        public static Tensor addcdiv(this Tensor input,
                                     Tensor tensor1,
                                     Tensor tensor2,
                                     object value = null)
        {
            value = value ?? 1;
            return torchlite.add(input, tensor1 / tensor2, value);
        }

    }

}