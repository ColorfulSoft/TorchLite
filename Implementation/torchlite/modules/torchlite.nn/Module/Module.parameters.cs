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

                /// <summary>
                /// Returns an iterator over module parameters. This is typically passed to an optimizer.
                /// </summary>
                /// <param name="recurse">If True, then yields parameters of this module and all submodules. Otherwise, yields only parameters that are direct members of this module.</param>
                /// <returns>IEnumerable&lt;Parameter&gt;</returns>
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