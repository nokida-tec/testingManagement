using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XT_CETC23.INTransfer;
namespace XT_CETC23.Common
{
    class ExportExcel
    {
        static ExportExcel exportExcel;
        ExcelPackage package;
        ExcelWorksheet worksheet;
        delegate void getMessage(string message);
        getMessage GetMessage;
        public static ExportExcel GetInstanse(IDatabaseForm iDataBaseForm)
        {
            if (exportExcel == null)
            {
                exportExcel = new ExportExcel(iDataBaseForm);
            }
            return exportExcel;
        }
        ExportExcel(IDatabaseForm iDataBaseForm)
        {
            GetMessage = iDataBaseForm.getStatus;
        }
        public void ExportTable(DataTable dt, string savaName)
        {
            try
            {//新建一个 Excel 工作簿
                package = new ExcelPackage();

                // 添加一个 sheet 表
                worksheet = package.Workbook.Worksheets.Add(dt.TableName);

                int rowIndex = 1;   // 起始行为 1
                int colIndex = 1;   // 起始列为 1

                //设置列名
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cells[rowIndex, colIndex + i].Value = dt.Columns[i].ColumnName;

                    //自动调整列宽，也可以指定最小宽度和最大宽度
                    worksheet.Column(colIndex + i).AutoFit();
                }

                // 跳过第一列列名
                rowIndex++;

                //写入数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        worksheet.Cells[rowIndex + i, colIndex + j].Value = dt.Rows[i][j].ToString();
                    }

                    //自动调整行高
                    worksheet.Row(rowIndex + i).CustomHeight = true;
                }

                //设置字体，也可以是中文，比如：宋体
                worksheet.Cells.Style.Font.Name = "Arial";

                //字体加粗
                worksheet.Cells.Style.Font.Bold = true;

                //字体大小
                worksheet.Cells.Style.Font.Size = 12;

                //字体颜色
                worksheet.Cells.Style.Font.Color.SetColor(System.Drawing.Color.Black);

                //单元格背景样式，要设置背景颜色必须先设置背景样式
                worksheet.Cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                //单元格背景颜色
                worksheet.Cells.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);

                //设置单元格所有边框样式和颜色
                //worksheet.Cells.Style.Border=(ExcelBorderStyle.Thin, System.Drawing.ColorTranslator.FromHtml("#0097DD"));

                //单独设置单元格四边框 Top、Bottom、Left、Right 的样式和颜色
                worksheet.Cells.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                worksheet.Cells.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                worksheet.Cells.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                //垂直居中
                worksheet.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                //水平居中
                worksheet.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //单元格是否自动换行
                worksheet.Cells.Style.WrapText = false;

                //设置单元格格式为文本
                worksheet.Cells.Style.Numberformat.Format = "@";

                //单元格自动适应大小
                worksheet.Cells.Style.ShrinkToFit = true;


                ////第一种保存方式
                //string path1 = HttpContext.Current.Server.MapPath("Export/");
                //string filePath1 = path1 + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".xlsx";
                //FileStream fileStream1 = new FileStream(filePath1, FileMode.Create);
                ////保存至指定文件
                //FileInfo fileInfo = new FileInfo(filePath1);
                //package.SaveAs(fileInfo);

                //第二种保存方式
                string fileName = ShowSaveFileDialog(savaName);         //文件的保存路径和文件名
                                                                        //string filePath2 = fileName + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".xlsx";
                FileStream fileStream2 = new FileStream(fileName, FileMode.Create);
                //写入文件流
                package.SaveAs(fileStream2);


                //创建一个内存流，然后转换为字节数组，输出到浏览器下载
                //MemoryStream ms = new MemoryStream();
                //package.SaveAs(ms);
                //byte[] bytes = ms.ToArray();

                //也可以直接获取流
                //Stream stream = package.Stream;

                //也可以直接获取字节数组
                //byte[] bytes = package.GetAsByteArray();

                ////调用下面的方法输出到浏览器下载
                //OutputClient(bytes);

                worksheet.Cells.Dispose();
                package.Dispose();
                GetMessage(savaName + "保存成功");
            }catch
            {
                GetMessage(savaName + "保存失败");
            }
        }
        private string ShowSaveFileDialog(string savaName)
        {
            string localFilePath = "";// fileNameExt, newFileName, FilePath; 
            SaveFileDialog sfd = new SaveFileDialog();//保存文件窗口
                                                      //设置文件类型 
            sfd.FileName = savaName+"-"+ DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            sfd.Filter = "Excel文件(*.xlsx)|*.xlsx";//保存类型为EXCEL
            //保存对话框是否记忆上次打开的目录 
            sfd.RestoreDirectory = true;


            /*/设置文件类型
            //书写规则例如：txt files(*.txt)|*.txt
            sfd.Filter = "txt files(*.txt)|*.txt|xls files(*.xls)|*.xls|All files(*.*)|*.*";
            //设置默认文件名（可以不设置）
            sfd.FileName = "siling-Data";
            //主设置默认文件extension（可以不设置）
            sfd.DefaultExt = "xml";
            //获取或设置一个值，该值指示如果用户省略扩展名，文件对话框是否自动在文件名中添加扩展名。（可以不设置）
            sfd.AddExtension = true;
            //设置默认文件类型显示顺序（可以不设置）
            sfd.FilterIndex = 2;
            //保存对话框是否记忆上次打开的目录
            sfd.RestoreDirectory = true;*/
            //点了保存按钮进入 
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                localFilePath = sfd.FileName.ToString(); //获得文件路径 
            }
            return localFilePath;//返回值为地址
        }
    }
}
