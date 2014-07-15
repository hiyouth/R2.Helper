using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace R2.Helper.GIS
{
    public class LonLatHelper
    {
        /// <summary>
        /// 将ddmmss 形式的经纬度转换到以度为单位的表达形式
        /// 如1212518.91 代表121d 25m 18.91s
        /// </summary>
        /// <param name="d_m_s"></param>
        /// <returns></returns>
        public static double ConvertToDegreeStyle(string d_m_s)
        {
            var fentodu = 1 / 60;
            var miaoTodu = 1 / 3600;
            try
            {
                double[] dms = GetDMS(d_m_s);
                double degree = dms[0] + dms[1] * fentodu + dms[2] * miaoTodu;
                return degree;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static double[] GetDMS(string d_m_s)
        {
            if (!String.IsNullOrEmpty(d_m_s))
            {
                string s;
                string m;
                string d;
                if (!d_m_s.Contains("."))
                {
                     s = d_m_s.Substring(d_m_s.Length - 2, d_m_s.Length);
                     m = d_m_s.Substring(d_m_s.Length - 4, d_m_s.Length);
                     d = d_m_s.Substring(0, d_m_s.Length - 4);
                }
                else
                {
                    // 秒为小数的情况
                    string[] splits = d_m_s.Split('.');

                    //取整数部分后2位
                     s = splits[0].Substring(splits[0].Length - 2, splits[0].Length);
                     s = s + "." + splits[1];
                     m = splits[0].Substring(splits[0].Length - 4, splits[0].Length);
                     d = splits[0].Substring(0, splits.Length - 4);
                }
                double sN = double.Parse(s);
                double mN = double.Parse(m);
                double dN = double.Parse(d);
                return new double[] { dN, mN, sN };
            }
            else
            {
                throw new Exception("输入的度分秒为空值或格式不正确");
            }
        }

        /// <summary>
        /// 将ddmmss 形式的经纬度转换到以度为单位的double型数值
        /// 如121-25-18 代表121d 25m 18s
        /// </summary>
        /// <param name="d_m_s"></param>
        /// <returns></returns>
        public static double ConvertToDegreeStyleFromString(string d_m_s)
        {
            double fentodu = 1.0 / 60;
            double miaoTodu = 1.0 / 3600;
            try
            {
                double[] dms = GetDegreeFromString(d_m_s);
                double degree = dms[2] + dms[1] * fentodu + dms[0] * miaoTodu;
                return degree;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 度分秒的转换 格式为：112-30-19  江西经纬度格式
        /// </summary>
        /// <param name="d_m_s"></param>
        /// <returns></returns>
        public static double[] GetDegreeFromString(string d_m_s)
        {
            if (!String.IsNullOrEmpty(d_m_s))
            {
                string s;
                string m;
                string d;
                string[] tempArr = d_m_s.Split(new Char[] { '-' });
                //if (!d_m_s.Contains("."))
                //{
                //s = d_m_s.Substring(d_m_s.Length - 2, d_m_s.Length);
                //m = d_m_s.Substring(d_m_s.Length - 4, d_m_s.Length);
                //d = d_m_s.Substring(0, d_m_s.Length - 4);
                //}
                //else
                //{
                //    // 秒为小数的情况
                //    string[] splits = d_m_s.Split('.');

                //    //取整数部分后2位
                //    s = splits[0].Substring(splits[0].Length - 2, splits[0].Length);
                //    s = s + "." + splits[1];
                //    m = splits[0].Substring(splits[0].Length - 4, splits[0].Length);
                //    d = splits[0].Substring(0, splits.Length - 4);
                //}
                double sN = double.Parse(tempArr[0]);
                double mN = double.Parse(tempArr[1]);
                double dN = double.Parse(tempArr[2]);
                return new double[] { dN, mN, sN };
            }
            else
            {
                throw new Exception("输入的度分秒为空值或格式不正确");
            }
        }

        /// <summary>
        /// 两点之间距离
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static double DistanceBetTwoPoints(double x1,double y1,double x2,double y2)
        {
            double var1 = Math.Pow((x1-x2),2);
            double var2 = Math.Pow((y1 - y2), 2);
            return Math.Sqrt(var1 + var2);
        }

        /// <summary>
        /// 得到一个委托，表示判断哪些点在矩形内
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static Func<double,double,Boolean> GetExpressionLonLatInRect(double x1, double x2, double y1, double y2)
        {
            return (lon,lat)=>    lon > x1 &&
                                      lon< x2 &&
                                      lat > y1 &&
                                      lat < y2;
        }

        /// <summary>
        /// 判断点是否在矩形内
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        /// <returns></returns>
        public static Boolean GetLonLatIsInRect(double x1, double x2, double y1, double y2,double lon,double lat)
        {
            bool isInRect =lon >= x1 &&
                                      lon <= x2 &&
                                      lat >= y1 &&
                                      lat <= y2;
            return isInRect;
        }

        /// <summary>
        /// 判断点是否在圆内
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radious"></param>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        /// <returns></returns>
        public static Boolean GetLonLatIsInCircle(double x,double y,double radius,double lon,double lat)
        {
            bool isInCircle = LonLatHelper.DistanceBetTwoPoints(x, y, lon, lat) <= radius;
            return isInCircle;
        }

    }
}

