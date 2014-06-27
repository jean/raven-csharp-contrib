using System;
using System.Diagnostics;

using PostSharp.Aspects;
using PostSharp.Extensibility;

using SharpRaven.Data;

namespace SharpRaven
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Assembly | AttributeTargets.Method,
                    Inherited = true)]
    [MulticastAttributeUsage(MulticastTargets.Method)]
    public class RavenLogExecutionTimeAttribute : BaseRavenLogAttribute
    {        
        private readonly TimeSpan _threshold;

        public RavenLogExecutionTimeAttribute(uint thresholdMs)
            : this(null, thresholdMs)
        {
        }

        public RavenLogExecutionTimeAttribute(string dsn, uint thresholdMs) 
            : base(dsn)
        {
            _threshold = TimeSpan.FromMilliseconds(thresholdMs);
        }
        
        public override void OnEntry(MethodExecutionArgs args)
        {
            if (!IsDisabled)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                args.MethodExecutionTag = stopwatch;
            }

            base.OnEntry(args);
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            if (!IsDisabled)
            {
                var stopwatch = (Stopwatch)args.MethodExecutionTag;
                var elapsed = stopwatch.Elapsed;

                if (elapsed > _threshold)
                {
                    var message = string.Format(
                        "[{0}.{1}] took [{2}] to execute!",
                        args.Method.DeclaringType.FullName,
                        args.Method.Name,
                        elapsed);

                    Client.CaptureMessage(message, ErrorLevel.Warning);
                }
            }

            base.OnExit(args);
        }
    }
}