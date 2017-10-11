using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonFramework.Unity.Test
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
            return new List<string>() { "12", "cd","你好" };
        }
    }
}
