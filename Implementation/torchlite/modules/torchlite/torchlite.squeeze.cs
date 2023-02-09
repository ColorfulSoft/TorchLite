//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System.Collections.Generic;

namespace System.AI.Experimental
{

    public unsafe static partial class torchlite
    {

        /// <summary>
        /// Returns a tensor with all the dimensions of input of size 1 removed.
        /// </summary>
        /// <param name="input">The input tensor.</param>
        /// <param name="dim">If given, the input will be squeezed only in this dimension.</param>
        /// <returns>Tensor object.</returns>
        public static Tensor squeeze(this Tensor input, int? dim = null)
        {
            var t = new Tensor();
            t.storage = input.storage;
            var shape = new List<int>();
            var ndim = input.shape.ndim;
            var input_shape = input.shape.data_ptr;
            for(int i = 0; i < ndim; ++i)
            {
                if(input_shape[i] == 1)
                {
                    if(dim == null)
                    {
                        continue;
                    }
                    if(dim == i)
                    {
                        continue;
                    }
                }
                shape.Add(input_shape[i]);
            }
            t.shape = new Size(shape);
            if(input.requires_grad && torchlite.grad_enabled)
            {
                t.__grad = torchlite.zeros(t.shape);
                t.__parents = new []{input};
                var numel = t.shape.numel();
                t.backward_fn = () =>
                {
                    var src_grad = (float*)input.grad.storage.data_ptr;
                    var dst_grad = (float*)t.grad.storage.data_ptr;
                    for(int i = 0; i < numel; ++i)
                    {
                        src_grad[i] += dst_grad[i];
                    }
                };
            }
            return t;
        }

    }

}