using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace R2.Helper.Linq
{
    public class LinqEntityHelper
    {
        /// <summary>
        /// 获取指定分页条件的实体集合，page从1开始
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="pageIndex"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static IQueryable<T> GetEntitySetByPage<T>(IQueryable<T> list, int numOnePage, int pageIndex)
        {
            if (pageIndex < 1 || numOnePage < 1)
                throw new Exception(@"分页参数输入有误，请检查参数;numOnePage,pageIndex均不允许小于1");
            return list.Skip(numOnePage * (pageIndex - 1)).Take(numOnePage).Select(x => x);
        }

        /// <summary>
        /// 通用动态表达式树生成方法
        /// </summary>
        /// <typeparam name="T">相关实体</typeparam>
        /// <typeparam name="U">检索关键字类型</typeparam>
        /// <param name="key"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static Expression<Func<T, Boolean>> GetExpressionForSingle<T, U>(U key,
            Expression<Func<T, Boolean>> selector) where U:class
        {
            var eps = DynamicLinqExpressions.True<T>();
            if (key != null)
            {
                eps = eps.And(selector);
            }
            return eps;
        }
    }

}
