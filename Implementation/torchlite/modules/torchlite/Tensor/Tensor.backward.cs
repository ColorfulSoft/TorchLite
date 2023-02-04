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
                        if(v.__parents != null)
                        {
                            foreach(var child in v.__parents)
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