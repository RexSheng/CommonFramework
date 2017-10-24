using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.DTO
{
    internal static class DtoMapConfigurationExtensions
    {
       public static void CreateAttributeMaps(this IMapperConfigurationExpression configuration, Type type)
        {
            foreach (var autoMapAttribute in type.GetTypeInfo().GetCustomAttributes<DtoMapAttribute>())
            {
                autoMapAttribute.CreateMap(configuration, type);
            }
        }
    }
}
