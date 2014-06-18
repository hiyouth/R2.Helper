using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace R2.Helper.Linq
{
    // <summary>
    ///  多条件动态查询
    /// </summary>
    public static class DynamicLinqExpressions//注意static静态类型
    {

        public static Expression<Func<T, bool>> True<T>()
        { 
            return f => true; 
        }
        public static Expression<Func<T, bool>> False<T>() 
        { 
            return f => false; 
        }

        //注意this
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
            (Expression.Or(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
            (Expression.And(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }
}
