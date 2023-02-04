//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System;
using System.Reflection;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        /// <summary>
        /// Initializes torchlite module before usage.
        /// </summary>
        static torchlite()
        {
            // fields
            torchlite.__rand = new Random();
            torchlite.__no_grad = false;
            torchlite.__default_dtype = torchlite.float32;
            // properties
            torchlite.version = Assembly.GetExecutingAssembly().GetName().Version;
        }

    }

}