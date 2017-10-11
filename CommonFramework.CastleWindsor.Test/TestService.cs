using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonFramework.CastleWindsor.Test
{
    public class TestService : ITestService
    {
        public List<string> getList()
        {
            return new List<string>() { "12", "cd" };
        }
    }

    public class TestService2 : ITestService
    {
        public List<string> getList()
        {
            return new List<string>() { "332", "3fs","测试" };
        }
    }
}
