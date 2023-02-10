//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

namespace System.AI.Experimental
{

    public unsafe static partial class torchlite
    {

        /// <summary>
        /// Computes the inverse cosine of each element in input.
        /// </summary>
        /// <param name="input">Input tensor.</param>
        /// <returns>Tensor.</returns>
        public static Tensor acos(this Tensor input)
        {
            if(input.dtype == torchlite.@bool)
            {
                throw new NotImplementedException("torchlite.acos is not implemented for bool tensors.");
            }
            var output = new Tensor(input.shape, torchlite.float32, input.requires_grad);
            var dst = (float*)output.storage.data_ptr;
            var elements = output.storage.size;
            switch(input.dtype)
            {
                case torchlite.float32:
                {
                    var src = (float*)input.storage.data_ptr;
                    for(int i = 0; i < elements; ++i)
                    {
                        dst[i] = (float)Math.Acos(src[i]);
                    }
                    if(input.requires_grad)
                    {
                        output.parents = new []{input};
                        output.backward_fn = () =>
                        {
                            var dst_grad = (float*)output.grad.storage.data_ptr;
                            var src_grad = (float*)input.grad.storage.data_ptr;
                            for(int i = 0; i < elements; ++i)
                            {
                                src_grad[i] += (float)((-1f) / (Math.Sqrt(1f - src[i] * src[i])) * dst_grad[i]);
                            }
                        };
                    }
                    break;
                }
                case torchlite.int32:
                {
                    var src = (int*)input.storage.data_ptr;
                    for(int i = 0; i < elements; ++i)
                    {
                        dst[i] = (float)Math.Acos(src[i]);
                    }
                    break;
                }
            }
            return output;
        }

    }

}