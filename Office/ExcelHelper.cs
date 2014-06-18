using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Collections.Generic;
using System;

namespace R2.Helper.Office
{
    public class ExcelHelper
    {
        private Excel._Application excelApp;
        private string fileName = string.Empty;
        private Excel.WorkbookClass wbclass;
        private Excel.Worksheet activatedWorksheet;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_filename"></param>
        public ExcelHelper(string _filename)
        {
            excelApp = new Excel.Application();
            object objOpt = System.Reflection.Missing.Value;
            wbclass = (Excel.WorkbookClass)excelApp.Workbooks.Open(_filename, objOpt, false, objOpt, objOpt, objOpt, true, objOpt, objOpt, true, objOpt, objOpt, objOpt, objOpt, objOpt);
        }

        /// <summary>
        /// 获取所有sheet的名称列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetSheetNames()
        {
            List<string> list = new List<string>();
            Excel.Sheets sheets = wbclass.Worksheets;
            string sheetNams = string.Empty;
            foreach (Excel.Worksheet sheet in sheets)
            {
                list.Add(sheet.Name);
            }
            return list;
        }

        /// <summary>
        /// 根据sheet页名称获取sheet对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Excel.Worksheet GetWorksheetByName(string name)
        {
            Excel.Worksheet sheet = null;
            Excel.Sheets sheets = wbclass.Worksheets;
            foreach (Excel.Worksheet s in sheets)
            {
                if (s.Name == name)
                {
                    sheet = s;
                    break;
                }
            }
            return sheet;
        }

        /// <summary>
        /// 获取单元格内容
        /// </summary>
        /// <param name="sheetName">sheet名称</param>
        /// <param name="startCell">起始单元格</param>
        /// <param name="endCell">结束单元格</param>
        /// <returns></returns>
        public Array GetContent(string sheetName,string startCell,string endCell)
        {
            Excel.Worksheet sheet = GetWorksheetByName(sheetName);
            //获取A1 到AM24范围的单元格
            Excel.Range rang = sheet.get_Range(startCell, endCell);
            //读一个单元格内容
            //sheet.get_Range("A1", Type.Missing);
            //不为空的区域,列,行数目
            //   int l = sheet.UsedRange.Columns.Count;
            // int w = sheet.UsedRange.Rows.Count;
            //  object[,] dell = sheet.UsedRange.get_Value(Missing.Value) as object[,];
            System.Array values = (Array)rang.Cells.Value2;
            return values;
        }

        /// <summary>
        /// 页是否激活
        /// </summary>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public Boolean ActivateWorkSheet(string sheetName)
        {
            Excel.Worksheet sheet = null;
            sheet = this.GetWorksheetByName(sheetName);
            this.activatedWorksheet = sheet;
            return sheet == null ? false : true;
        }

        /// <summary>
        /// 插入一行
        /// </summary>
        /// <param name="startCell"></param>
        /// <param name="endCell"></param>
        /// <param name="datas"></param>
        public void InsertRow(string startCell,string endCell,String[] datas)
        {
            Excel.Range rng = this.activatedWorksheet.get_Range(startCell, endCell);
            rng.Value2 = datas;
            //for (int i = 0; i < rng.Cells.Count; i++)
            //{
            //    ((Excel.Range)rng.Cells[0, i]).Value2 = datas[i];
            //}
        }

        /// <summary>
        /// 另存Excel
        /// </summary>
        /// <param name="filePath"></param>
        public void SaveAs(string filePath)
        {
            wbclass.SaveAs(filePath);
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            excelApp.Quit();
            excelApp = null;
        }

    }
}