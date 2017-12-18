using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datamantra.DAL
{
    public interface IContextFactory
    {
        IDmDataContext GetDataContext();

        TContext GetContext<TContext>() where TContext : IDataContext;
    }
}
