﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.Core.Extension
{
    public static class ObjectHelper
    {
        public static T As<T>(this object obj)
            where T : class
        {
            return (T)obj;
        }
    }
}
