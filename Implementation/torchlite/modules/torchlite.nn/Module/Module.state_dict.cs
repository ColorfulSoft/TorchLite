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

                public Dictionary<string, Tensor> state_dict(bool recurse = true)
                {
                    var state_dict = new Dictionary<string, Tensor>();
                    var type = this.GetType();
                    var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                    foreach(var field in fields)
                    {
                        var value = field.GetValue(this);
                        if(value is Parameter)
                        {
                            state_dict.Add(field.Name, (Tensor)value);
                        }
                        if(value is Module)
                        {
                            if(recurse)
                            {
                                var @base = field.Name;
                                var children = ((Module)value).state_dict();
                                foreach(var child in children)
                                {
                                    state_dict.Add(@base + '.' + child.Key, child.Value);
                                }
                            }
                        }
                    }
                    return state_dict;
                }

            }

        }

    }

}