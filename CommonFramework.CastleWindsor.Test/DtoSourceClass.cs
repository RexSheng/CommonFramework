using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonFramework.DTO;

/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.CastleWindsor.Test
{
    [DtoMapTo(typeof(DtoDestinationClass))]
    [DtoMapFrom(typeof(UserInfo))]
    public class DtoSourceClass
    {
        public int SourceId { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    
     }

    public class DtoDestinationClass
    {
        public int DestinationId { get; set; }

        public string Name { get; set; }

        public string DestinationName { get; set; }
    }
}
