using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonFramework.Oracle.EntityFramework
{
    public class CommonInterface : ICommonInterface
    {
        public Type[] GetAllChildrenInterface(Type type)
        {
            List<Type> result = new List<Type>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var t in assembly.GetTypes().Where(m => m.IsInterface && m.GetInterfaces().Contains(type)))
                {

                    result.Add(t);

                }
            }
            return result.ToArray();
        }

        public Type[] GetAllChildrenClass(Type type)
        {
            List<Type> result = new List<Type>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var t in assembly.GetTypes().Where(m => m.IsClass && m.GetInterfaces().Contains(type)))
                {

                    result.Add(t);

                }
            }
            return result.ToArray();
        }


        public List<Type[]> GetAllChildren(Type type)
        {
            List<Type[]> result = new List<Type[]>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var t in assembly.GetTypes().Where(m => m.IsClass && m.GetInterfaces().Contains(type)))
                {
                    foreach(var inter in t.GetInterfaces()){
                        Type[] temp = new Type[] { inter, t };
                        result.Add(temp);
                    }
                }
            }
            return result;
        }
    }
}
