using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datamantra.Common
{
    [Serializable()]
    public class CookieSessionItems
    {
        public string EmailAddress { get; set; }
        public bool RememberMe { get; set; }
        public string CoBrowse { get; set; }
        public string EncryptedSession { get; set; }
        public string SessionObject { get; set; }
    }
}
