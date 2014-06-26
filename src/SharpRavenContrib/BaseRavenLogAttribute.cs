using System;
using System.Collections.Concurrent;

using PostSharp.Aspects;
using PostSharp.Extensibility;

namespace SharpRaven
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Assembly | AttributeTargets.Method,
                    Inherited = true)]
    [MulticastAttributeUsage(MulticastTargets.Method)]
    public abstract class BaseRavenLogAttribute : OnMethodBoundaryAspect
    {
        private static readonly ConcurrentDictionary<string, RavenClient> RavenClients =
            new ConcurrentDictionary<string, RavenClient>();

        private readonly string _dsn;

        protected BaseRavenLogAttribute(string dsn)
        {
            _dsn = dsn;
        }

        public RavenClient GetClient()
        {
            return RavenClients.GetOrAdd(_dsn, dsn => new RavenClient(dsn));
        }
    }
}
