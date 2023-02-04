//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2022. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System;
using System.Reflection;
using System.Collections.Generic;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public static partial class nn
        {

            public partial class Module
            {

                public IEnumerable<Parameter> parameters(bool recurse = true)
                {
                    var parameters = new List<Parameter>();
                    var type = this.GetType();
                    var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                    foreach(var field in fields)
                    {
                        var value = field.GetValue(this);
                        if(value is Parameter)
                        {
                            parameters.Add((Parameter)value);
                        }
                        if(value is Module)
                        {
                            if(recurse)
                            {
                                parameters.AddRange(((Module)value).parameters());
                            }
                        }
                    }
                    return parameters;
                }

            }

        }

    }

}