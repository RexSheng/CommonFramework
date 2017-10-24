using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.DTO
{
    public class DtoMapAttribute : Attribute
    {
        public Type[] _targetTypes { get; private set; }
        public DtoMapAttribute(params Type[] targetTypes)
        {
            _targetTypes = targetTypes;
        }

        public virtual void CreateMap(IMapperConfigurationExpression cfg, Type type)
        {
            foreach (var targetType in _targetTypes)
            {
                cfg.CreateMap(type, targetType);
                cfg.CreateMap(targetType, type);
            }
        }
    }

    public class DtoMapToAttribute : DtoMapAttribute
    { 
        public DtoMapToAttribute(params Type[] targetTypes) : base(targetTypes)
        {

        }
    

        public override void CreateMap(IMapperConfigurationExpression cfg,Type type)
        {
            foreach(var targetType in _targetTypes)
            {
                cfg.CreateMap(type, targetType);
            }
        }
    }

    public class DtoMapFromAttribute : DtoMapAttribute
    { 
        public DtoMapFromAttribute(params Type[] targetTypes) : base(targetTypes)
        {

        }
         

        public override void CreateMap(IMapperConfigurationExpression cfg, Type type)
        {
            foreach (var targetType in _targetTypes)
            {
                cfg.CreateMap(targetType, type);
            }
        }
    }
}
