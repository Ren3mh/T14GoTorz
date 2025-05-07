using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Data;

namespace ModelsTesting
{
    public class GotorzInMemoryContext
    {
        public static GotorzContext GetMemoryContext()
        {
            var options = new DbContextOptionsBuilder<GotorzContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
            .Options; return new GotorzContext(options);
        }
    }

}
