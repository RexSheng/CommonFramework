﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonFramework.MySql.PagedList
{
    public class CommonPagedList<T> :List<T>
        where T:class
    {
        public int TotalItemCount { get; set; }

        public int PageSize { get; set; }
        

        public int PageIndex { get; set; }

        public int TotalPageCount { get; set; }

        public CommonPagedList(IQueryable<T> list, int pageIndex, int pageSize)
        {
            if (pageIndex <= 0)
                pageIndex = 1;
            this.TotalItemCount = list.Count();
            this.PageSize = pageSize;
            this.TotalPageCount = this.TotalItemCount % this.PageSize == 0 ? this.TotalItemCount / this.PageSize : this.TotalItemCount / this.PageSize + 1;
            pageIndex = pageIndex > this.TotalPageCount ? this.TotalPageCount : pageIndex;
            this.PageIndex = pageIndex;
            if (this.PageIndex > this.TotalPageCount)
                this.PageIndex = this.TotalPageCount;
            var query = list.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            AddRange(query);
        }

        public CommonPagedList(IEnumerable<T> list, int pageIndex, int pageSize, int totalItemCount)
        {
            if (pageIndex <= 0)
                pageIndex = 1;
            this.TotalItemCount = totalItemCount;
            this.TotalPageCount = totalItemCount % pageSize == 0 ? totalItemCount / pageSize : totalItemCount / pageSize + 1;
            pageIndex = pageIndex > this.TotalPageCount ? this.TotalPageCount : pageIndex;
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            AddRange(list);
        }
    }
}