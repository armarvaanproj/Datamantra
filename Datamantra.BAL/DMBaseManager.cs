using Datamantra.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datamantra.BAL
{
    public class DMBaseManager: BaseManager<IDataContext>
    {
        public DMBaseManager(IContextFactory factory) : base(factory)
        {
        }
    }
}
