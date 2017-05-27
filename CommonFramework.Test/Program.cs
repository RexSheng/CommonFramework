using CommonFramework.SqlServer.PagedList;
//using CommonFramework.Test.SqlServer;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
//using CommonFramework.Test.MySql;
using CommonFramework.Test.Oracle;
using CommonFramework.Test.Redis;
using CommonFramework.Test.Helper;

namespace CommonFramework.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Email();
        }
        /* sqlserver
        static void SqlServer() {
            UnityContainer container = new UnityContainer();
            container.RegisterType<IUserDao, UserDao>();

            var context1 = new DbContextProvider<WebAPIDemoEntities>("WebAPIDemoEntities").Context;
            var uu1 = context1.UserInfo.ToList();
            var context2 = new DbContextProvider<WebAPIDemoEntities>("WebAPIDemoEntities2").Context;
            var uu2 = context2.UserInfo.ToList();
            var context3 = new DbContextProvider<WebAPIDemoEntities>().GetDbContext("WebAPIDemoEntities2");
            var uu3 = context3.UserInfo.ToList();



            IBaseDao<UserInfo> user = new BaseDao<UserInfo>();
            var aaaUUU = user.GetAllList();

            IUserDao _user = container.Resolve<IUserDao>();
            var context = _user.GetContext();
            //var a3 = context.R_UserRole.ToList();
            //var a4 = context.R_UserRole.Include("UserInfo").ToList();
            var a5 = _user.GetAll();
            var a6 = _user.GetAllIncluding(new Expression<Func<UserInfo, object>>[] { m => m.R_UserRole });
            var a7 = a5.ToList();
            var a8 = a6.Where(m => m.Id < 10).ToList();
            var a = _user.GetAllList();
            var a2 = context.UserInfo.ToList();
            var b = _user.GetAll().Where(m => m.Id < 18).OrderBy(m => m.age).ToCommonPagedList(5000, 1000);
            var c = _user.ToCommonPagedList(m => m.age, false, 2, 10);
            var d = _user.ToCommonPagedList("WebAPIDemoEntities2", m => m.age, false, 2, 10);
            var testGroup = _user.GetAll().GroupBy(m => m.age).Select(m => new { age = m.Key, count = m.Count() }).ToList();
            ////事务demo
            //using (TransactionScope trans = new TransactionScope(TransactionScopeOption.Required)) {
            //    var useradd1 = new UserInfo() { Name = "Transa" };
            //    _user.Add(useradd1);
            //    var useradd2 = new UserInfo() { Name = "Transa2" };
            //    _user.Add("WebAPIDemoEntities2", useradd2);
            //    trans.Complete();
            //}
        }
        */

        //static void MySql() {
        //    IMySqlAdoDao dao = new MySqlAdoDao();
        //    var a=dao.GetDataTable("select deviceno,devicename from ps_device");
             
        //    IBaseDao<testuser> user = new BaseDao<testuser>();
        //    var aaaUUU = user.GetAllList();

        //    var context = user.GetContext();

        //    var a2 = context.testuser.ToList();
        //    var b = user.GetAll().Where(m => m.id < 18).OrderBy(m => m.age).ToCommonPagedList(5000, 1000);
        //    var c = user.ToCommonPagedList(m => m.age, false, 2, 10);

        //    var testGroup = user.GetAll().GroupBy(m => m.age).Select(m => new { age = m.Key, count = m.Count() }).ToList();
        //}

        static void Oracle()
        {
            IOracleHelper dao = new OracleHelper();
            var a = dao.GetDataTable("select OBCC,CCCC,HEADDATE from QDATM.RPT01_CAC where ROWNUM<10");

            IBaseDao<RPT01_CAC> user = new BaseDao<RPT01_CAC>();
            var aaaUUU = user.GetAllList(m => m.ODATE == "20170411");

            var context = user.GetContext();

            var b = user.GetAll().Where(m => m.HEADDATE.CompareTo("20170411")>=0).OrderBy(m => m.HEADDATE).ToCommonPagedList(5000, 1000);
            var c = user.ToCommonPagedList(m => m.HEADDATE, false, 2, 10);

            var testGroup = user.GetAll().GroupBy(m => m.HEADDATE).Select(m => new { age = m.Key, count = m.Count() }).ToList();
        }

        static void Redis() {
            RedisHelper.SetList<string>(new List<string>() { "12dadsfas", "34sadafadf" }, "testkey");
        }

        static void Email() {
            for (int i = 10; i < 20; i++) {
                EmailHelper.SendEmail(" @qq.com", " @qq.com", "小可爱" + (i + 1), "<h1>你好！！</h1>");
            }
                
        }
    }
}
