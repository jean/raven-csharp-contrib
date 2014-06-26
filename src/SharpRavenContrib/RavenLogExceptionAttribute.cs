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
        public RavenLogExceptionAttribute(string dsn) : base(dsn)
        {
        }

        public override void OnException(MethodExecutionArgs args)
        {
            var client = GetClient();
            client.CaptureException(args.Exception);
            base.OnException(args);
        }
    }
}