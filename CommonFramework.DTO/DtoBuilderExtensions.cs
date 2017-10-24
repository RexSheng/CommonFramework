using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Reflection;
using CommonFramework.Core.Configure;
using AutoMapper.QueryableExtensions;

/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.DTO
{
    public static class DtoBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="additionalAction"></param>
        /// <param name="assemblyToScan"></param>
        public static void Config(Action<IMapperConfigurationExpression> additionalAction = null, params string[] assemblyToScan)
        {
            List<Assembly> assemblyList = new List<Assembly>();
            foreach (var profile in assemblyToScan)
            {
                assemblyList.Add(Assembly.Load(profile));
            }
            var allTypes = assemblyList
                .Where(a => a.GetName().Name != nameof(AutoMapper))
                .SelectMany(a => a.DefinedTypes)
                .ToArray();
            var profiles =
                allTypes
                .Where(t => !t.IsAbstract && t.GetCustomAttributes(typeof(DtoMapAttribute),true).Length>0);
            Mapper.Initialize(cfg =>
            {
                foreach (var profile in profiles.Select(t => t.AsType()))
                {
                    cfg.CreateAttributeMaps(profile);
                }
                if (additionalAction != null)
                    additionalAction(cfg);

                
            });
        }
         
        public static TDestination MapTo<TDestination>(this object o)
        {
            return Mapper.Map<TDestination>(o);
        }

        public static List<TDestination> MapToList<TDestination>(this object queryable)
        {
            return Mapper.Map<List<TDestination>>(queryable);
        }

        //public static IQueryable<TDestination> MapToQueryable<TSource,TDestination>(this IQueryable<TSource> queryable)
        //{
        //    return queryable.Select(m=>m.MapTo<TDestination>());
        //}

    }
}
