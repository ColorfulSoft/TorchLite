//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2022. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public static partial class nn
        {

            /// <summary>
            /// A kind of Tensor that is to be considered a module parameter.
            /// Parameters are Tensor subclasses, that have a very special property when used with
            /// Module's - when they’re assigned as Module attributes they are automatically
            /// added to the list of its parameters, and will appear e.g. in parameters() iterator.
            /// Assigning a Tensor doesn’t have such effect.
            /// </summary>
            public sealed partial class Parameter : torchlite.Tensor
            {
            }

        }

    }

}