//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System;
using System.Reflection;
using System.Diagnostics;

using no_grad = System.AI.Experimental.torchlite.autograd.grad_mode.no_grad;

namespace System.AI.Experimental
{

    /// <summary>
    /// The torchlite package contains data structures for multi-dimensional tensors and defines
    /// mathematical operations over these tensors. Additionally, it provides many utilities for
    /// efficient serializing of Tensors, and other useful utilities.
    /// </summary>
    public static partial class torchlite
    {

        #region constants

        /// <summary>
        /// 32-bit floating point. Alias for torchlite.float.
        /// </summary>
        public const DType float32 = DType.float32;

        /// <summary>
        /// 32-bit floating point. Alias for torchlite.float32.
        /// </summary>
        public const DType @float = DType.float32;

        /// <summary>
        /// 32-bit signed integer. Alias for torchlite.int.
        /// </summary>
        public const DType int32 = DType.int32;

        /// <summary>
        /// 32-bit signed integer. Alias for torchlite.int32.
        /// </summary>
        public const DType @int = DType.int32;

        /// <summary>
        /// Boolean.
        /// </summary>
        public const DType @bool = DType.@bool;

        #endregion

        #region fields

        /// <summary>
        /// Random number generator.
        /// </summary>
        [ThreadStatic]
        private static Random __rand;

        /// <summary>
        /// No_grad context manager state.
        /// </summary>
        [ThreadStatic]
        private static bool? __no_grad;

        /// <summary>
        /// Default data type for tensors.
        /// </summary>
        [ThreadStatic]
        private static DType? __default_dtype;

        #endregion

        #region properties

        /// <summary>
        /// Current torchlite version.
        /// </summary>
        public static Version version
        {

            get;

            private set;

        }

        /// <summary>
        /// Gets or sets the default data type for tensors.
        /// </summary>
        public static DType default_dtype
        {

            get
            {
                // If default_dtype is not initialized in current thread, initialize it first.
                if(torchlite.__default_dtype == null)
                {
                    torchlite.__default_dtype = torchlite.float32;
                }
                return torchlite.__default_dtype.Value;
            }

            set
            {
                torchlite.__default_dtype = value;
            }

        }

        /// <summary>
        /// Returns no_grad context manager.
        /// </summary>
        public static no_grad no_grad
        {

            get
            {
                return new no_grad();
            }

        }

        /// <summary>
        /// Returns a value indicating whether the autograd mechanism is active at the call point.
        /// </summary>
        public static bool grad_enabled
        {

            get
            {
                // If no_grad state is not defined in current thread, initialize it first.
                if(torchlite.__no_grad == null)
                {
                    torchlite.__no_grad = false;
                }
                // If there are active no_grad manager, return false.
                if(torchlite.__no_grad.Value)
                {
                    return false;
                }
                // Check, if the main assembly marked as no_grad.
                var asm = Assembly.GetEntryAssembly();
                var attributes = asm.CustomAttributes;
                foreach(var attr in attributes)
                {
                    if(attr.AttributeType == typeof(no_grad))
                    {
                        return false;
                    }
                }
                // Check stack trace. Return false, if any method in trace is marked as no_grad.
                var t = new StackTrace();
                var fc = t.FrameCount;
                for(int i = 0; i < fc; ++i)
                {
                    var method = t.GetFrame(i).GetMethod();
                    attributes = method.CustomAttributes;
                    foreach(var attr in attributes)
                    {
                        if(attr.AttributeType == typeof(no_grad))
                        {
                            return false;
                        }
                    }
                    attributes = method.DeclaringType.CustomAttributes;
                    foreach(var attr in attributes)
                    {
                        if(attr.AttributeType == typeof(no_grad))
                        {
                            return false;
                        }
                    }
                }
                // Return true, if there are no no_grad attributes or active context managers.
                return true;
            }

        }

        #endregion

    }

}