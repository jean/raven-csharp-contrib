using System;

using PostSharp.Aspects;
using PostSharp.Extensibility;

namespace SharpRaven
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Assembly | AttributeTargets.Method,
                    Inherited = true)]
    [MulticastAttributeUsage(MulticastTargets.Method)]
    public class RavenLogExceptionAttribute : BaseRavenLogAttribute
    {
        public RavenLogExceptionAttribute(string dsn = null) : base(dsn)
        {
        }

        public override void OnException(MethodExecutionArgs args)
        {
            if (!IsDisabled)
            {
                Client.CaptureException(args.Exception);
            }

            base.OnException(args);
        }
    }
}