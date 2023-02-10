//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System.Collections.Generic;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public partial class Tensor
        {

            /// <summary>
            /// Computes the gradient of current tensor w.r.t. graph leaves.
            /// The graph is differentiated using the chain rule. If the tensor is non-scalar (i.e. its
            /// data has more than one element) and requires gradient, the function additionally requires
            /// specifying gradient. It should be a tensor of matching type and location, that contains the
            /// gradient of the differentiated function w.r.t. self.
            /// This function accumulates gradients in the leaves - you might need to zero .grad attributes or
            /// set them to null before calling it.
            /// </summary>
            /// <param name="grad">Gradient w.r.t. the tensor. If it is a tensor, it will be automatically converted to a Tensor that does not require grad. Null values can be specified for scalar Tensors or ones that don’t require grad. If a null value would be acceptable then this argument is optional.</param>
            public void backward(Tensor grad = null)
            {
                if(!this.requires_grad)
                {
                    throw new ArgumentException("Element 0 of tensors does not require grad and does not have a grad_fn.");
                }
                if(this.shape.ndim != 0)
                {
                    if(grad == null)
                    {
                        throw new ArgumentException("Grad can be implicitly created only for scalar outputs");
                    }
                    if(grad.shape != this.shape)
                    {
                        throw new ArgumentException(string.Format("Mismatch in shape: grad_output has a shape of {0} and output has a shape of {1}.", grad.shape, this.shape));
                    }
                }
                // topological order all of the children in the graph
                var topo = new List<Tensor>();
                var visited = new HashSet<Tensor>();
                Action<Tensor> build_topo = null;
                build_topo = (Tensor v) =>
                {
                    if(!visited.Contains(v))
                    {
                        visited.Add(v);
                        if(v.parents != null)
                        {
                            foreach(var child in v.parents)
                            {
                                build_topo(child);
                            }
                        }
                        topo.Add(v);
                    }
                };
                build_topo(this);
                // go one variable at a time and apply the chain rule to get its gradient
                this.grad = grad ?? 1f;
                topo.Reverse();
                foreach(var v in topo)
                {
                    if(v.backward_fn != null)
                    {
                        v.backward_fn();
                    }
                }
            }

        }

    }

}