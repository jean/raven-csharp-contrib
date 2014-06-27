using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Linq;
using System.Reflection;

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

        private string _dsn;
        [NonSerialized] private bool _isDisabled;
        [NonSerialized] private RavenClient _client;

        protected BaseRavenLogAttribute(string dsn = null)
        {
            _dsn = dsn;
        }

        protected bool IsDisabled
        {
            get
            {
                return _isDisabled;
            }
        }

        protected RavenClient Client
        {
            get
            {
                return _client;
            }
        }

        /// <summary>
        /// If DSN is not provided via constructor, then try to look it up via AppSettings at runtime
        /// </summary>
        public override void RuntimeInitialize(MethodBase method)
        {
            base.RuntimeInitialize(method);

            if (_dsn == null)
            {
                var values = ConfigurationManager.AppSettings.GetValues("SentryDsn");
                _dsn = values != null ? values.FirstOrDefault() : null;
            }

            if (_dsn == null)
            {
                _isDisabled = true;
            }
            else
            {
                _client = GetClient(_dsn);
            }
        }

        private static RavenClient GetClient(string dsn)
        {
            return RavenClients.GetOrAdd(dsn, x => new RavenClient(x));
        }
    }
}