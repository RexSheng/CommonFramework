using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonFramework.MySql.EntityFramework
{
    public interface ICommonInterface
    {
        Type[] GetAllChildrenInterface(Type type);

        Type[] GetAllChildrenClass(Type type);

        List<Type[]> GetAllChildren(Type type);
    }
}
