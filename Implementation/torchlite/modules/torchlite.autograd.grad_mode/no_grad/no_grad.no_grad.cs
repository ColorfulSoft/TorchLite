//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public static partial class autograd
        {

            public static partial class grad_mode
            {

                public sealed partial class no_grad
                {

                    /// <summary>
                    /// Sets the context manager when creating.
                    /// </summary>
                    public no_grad()
                    {
                        torchlite.__no_grad = true;
                    }

                }

            }

        }

    }

}