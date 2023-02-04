//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

namespace System.AI.Experimental
{

    public unsafe static partial class torchlite
    {

        /// <summary>
        /// Returns a new tensor with the ceil of the elements of input,
        /// the smallest integer greater than or equal to each element.
        /// </summary>
        /// <param name="input">Input tensor.</param>
        /// <returns>Tensor.</returns>
        public static Tensor ceil(this Tensor input)
        {
            if(input.dtype == torchlite.@bool)
            {
                throw new NotImplementedException("torchlite.ceil is not implemented for bool tensors.");
            }
            var output = new Tensor(input.shape, input.dtype, input.requires_grad);
            var elements = output.storage.size;
            switch(input.dtype)
            {
                case torchlite.float32:
                {
                    var src = (float*)input.storage.data_ptr;
                    var dst = (float*)output.storage.data_ptr;
                    for(int i = 0; i < elements; ++i)
                    {
                        dst[i] = (float)Math.Ceiling(src[i]);
                    }
                    if(input.requires_grad)
                    {
                        output.__parents = new []{input};
                        output.backward_fn = () =>
                        {
                        };
                    }
                    break;
                }
                case torchlite.int32:
                {
                    var src = (int*)input.storage.data_ptr;
                    var dst = (int*)output.storage.data_ptr;
                    for(int i = 0; i < elements; ++i)
                    {
                        dst[i] = src[i];
                    }
                    break;
                }
            }
            return output;
        }

    }

}