//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        /// <summary>
        /// Returns a tensor that is a transposed version of input. The given dimensions dim0 and dim1 are swapped.
        /// </summary>
        /// <param name="input">The input tensor.</param>
        /// <param name="dim0">The first dimension to be transposed.</param>
        /// <param name="dim1">The second dimension to be transposed.</param>
        /// <returns>Tensor.</returns>
        public static Tensor transpose(this Tensor input, int dim0, int dim1)
        {
            var dims = new int[input.shape.ndim];
            for(int i = 0; i < dims.Length; ++i)
            {
                dims[i] = i;
            }
            if((dim0 >= dims.Length) || (dim0 < 0))
            {
                throw new ArgumentException(string.Format("Dimension out of range (expected to be in range of [-{0}, {1}], but got {2})", dims.Length, dims.Length - 1, dim0));
            }
            if((dim1 >= dims.Length) || (dim1 < 0))
            {
                throw new ArgumentException(string.Format("Dimension out of range (expected to be in range of [-{0}, {1}], but got {2})", dims.Length, dims.Length - 1, dim1));
            }
            if(dim0 < 0)
            {
                dim0 += dims.Length;
            }
            if(dim1 < 0)
            {
                dim1 += dims.Length;
            }
            var t = dims[dim0];
            dims[dim0] = dims[dim1];
            dims[dim1] = t;
            return torchlite.permute(input, dims);
        }

    }

}