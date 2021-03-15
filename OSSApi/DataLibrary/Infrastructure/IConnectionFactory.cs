using System.Data;

using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Infrastructure {
    interface IConnectionFactory {
        public IDbConnection GetConnection();
    }
}
