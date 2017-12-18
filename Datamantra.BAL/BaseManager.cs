using Datamantra.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datamantra.BAL
{
    public class BaseManager<TContext> : IDisposable where TContext : IDataContext
    {
        private IContextFactory factory;

        //Private Members
        private DataContext _dmContext;

        public BaseManager(IContextFactory factory)
        {
            _dmContext = new Lazy<IDmDataContext>(factory.GetDataContext);
        }

        public DataContext DmDataContext { get { return _dmContext ?? (_dmContext = new DataContext()); } }

        public void Dispose()
        {
            if (_dmContext != null)
                _dmContext.Dispose();

        }
    }
}
