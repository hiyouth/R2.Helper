using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R2.Helper.GIS
{
    /// <summary>
    ///  负责窗口坐标和逻辑坐标之间的转换
    /// </summary>
    public class CoordinateConverter
    {
        public double[] LogicExtent { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        /// <summary>
        /// 构造函数，默认逻辑范围和高宽是同比例，即逻辑范围是修正后的范围
        /// 
        /// </summary>
        /// <param name="logicExtent"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public CoordinateConverter(double[] logicExtent, int width, int height)
        {
            LogicExtent = logicExtent;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// 根据高宽修正逻辑范围,以左上角为原点修正
        /// </summary>
        /// <returns></returns>
        public static double[] AdjustLogicExtent(double[] logicExtent,int width,int height)
        {
            double xmin = logicExtent[0];
            double ymin = logicExtent[1];
            double xmax = logicExtent[2];
            double ymax = logicExtent[3];
            double xResolution = (xmax - xmin) / width;
            double yResolution = (ymax - ymin) / height;
            double resolution = (xResolution - yResolution) > 0 ? xResolution : yResolution;
            double newXmin = xmin;
            double newYmin = ymax - resolution * height;
            double newXmax = xmin + resolution * width;
            double newYmax = ymax;
            return new double[] {newXmin,newYmin,newXmax,newYmax};
        }

        /// <summary>
        /// 逻辑坐标转换为窗口坐标，这里认为左上角为窗口坐标的原点，即（0，0）
        /// </summary>
        /// <param name="logicPnt"></param>
        /// <returns></returns>
        public double[] ConvertToWindow(double[] logicPnt)
        {
            double xmin = LogicExtent[0];
            double ymin = LogicExtent[1];
            double xmax = LogicExtent[2];
            double ymax = LogicExtent[3];
            double resolution = (xmax - xmin) / Width;
            double lx = logicPnt[0] - xmin;
            double ly = logicPnt[1] - ymax;
            double wx = lx / resolution;
            double yx = -(ly / resolution);
            return new double[] {wx,yx};
        }
    }
}
