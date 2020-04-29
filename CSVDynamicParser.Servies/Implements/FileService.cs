using CSVDynamicParser.Common;
using CSVDynamicParser.Common.Exceptions;
using CSVDynamicParser.Servies.Interfaces;
using CSVDynamicParser.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CSVDynamicParser.Servies.Implements
{
    public class FileService : IFileService
    {
        public Guid Save(HttpPostedFileBase file)
        {
            var guid = Guid.NewGuid();
            var uniqueFileName = $"{guid}.csv";
            var filePath = $"{Environment.CurrentDirectory}upload/csv/";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            filePath = $"{filePath}{uniqueFileName}";
            file.SaveAs(filePath);
            return guid;
        }

        public UploadedFile ReadCSVHeader(bool hasHeader, HttpPostedFileBase file)
        {
            var guid = Save(file);
            var res = new UploadedFile() { Id = guid };
            var filePath = $"{Environment.CurrentDirectory}upload/csv/{guid}.csv";
            using (CsvFileReader reader = new CsvFileReader(filePath))
            {
                CsvRow row = new CsvRow();
                reader.ReadRow(row);
                foreach (var field in row)
                {
                    var header = new HeaderItem()
                    {
                        Index = row.IndexOf(field)
                    };
                    if (hasHeader)
                    {
                        header.Name = field;
                    }
                    else
                    {
                        header.Name = $"column{row.IndexOf(field) + 1}";
                    }
                    res.Headers.Add(header);
                }
            }
            return res;
        }

        public HeaderParameter IfColumnMapped(CSVConvertParameter parameter, int index)
        {
            return parameter.Headers.FirstOrDefault(x => x.Value != null && x.Index == index);
        }

        public DataColumn CreateColumn(CSVConvertParameter parameter, string sColumn, int index)
        {
            DataColumn column = null;
            var mapped = IfColumnMapped(parameter, index);
            if (mapped != null)
            {
                column = new DataColumn();
                var type = mapped.Value.dataType.ToString();
                if (type == "Number")
                {
                    type = "Int32";
                }
                column.DataType = Type.GetType($"System.{type}");
                column.AllowDBNull = !mapped.Value.required;
                if (mapped.Value.size > 0)
                {
                    column.MaxLength = mapped.Value.size;
                }
                string sColumnValue = sColumn;
                if (!parameter.HasHeader)
                {
                    sColumnValue = $"column{index + 1}";
                }
                column.ColumnName = sColumnValue;
            }
            return column;
        }

        public void PopulateRow(CSVConvertParameter parameter, CsvRow row, Dictionary<int, string> columnMap, DataRow dr, int rowIndex)
        {
            foreach (var sColumn in row)
            {
                var currentIndex = row.IndexOf(sColumn);
                var mapped = IfColumnMapped(parameter, currentIndex);
                if (mapped != null)
                {
                    var columnName = columnMap.FirstOrDefault(x => x.Key == currentIndex);
                    if (!string.IsNullOrEmpty(columnName.Value))
                    {
                        // required fields
                        if (mapped.Value.required && string.IsNullOrEmpty(sColumn))
                        {
                            // throw custom exception
                            throw new ParseException($"Column {row.IndexOf(sColumn)} not allow null values", new ParseExceptionParameter()
                            {
                                csv = row,
                                columnIndex = row.IndexOf(sColumn),
                                rowIndex = rowIndex + 1
                            });
                        }
                        else if (mapped.Value.size > 0 && sColumn.Length > mapped.Value.size
                            && mapped.Value.dataType == DataTypeEnum.String)
                        {
                            // throw custom exception
                            throw new ParseException($"The data length of Column {row.IndexOf(sColumn)} is larger the configuration value: Max Length <= {mapped.Value.size} ",
                                new ParseExceptionParameter()
                                {
                                    csv = row,
                                    columnIndex = row.IndexOf(sColumn),
                                    rowIndex = rowIndex + 1
                                });
                        }
                        else
                        {
                            switch (mapped.Value.dataType)
                            {
                                case DataTypeEnum.Number:
                                    int intvalue = 0;
                                    var parsed = Int32.TryParse(sColumn, out intvalue);
                                    if (!parsed)
                                    {
                                        throw new ParseException($"The Column {row.IndexOf(sColumn)} can't be parsed to : Int32 ",
                                        new ParseExceptionParameter()
                                        {
                                            csv = row,
                                            columnIndex = row.IndexOf(sColumn),
                                            rowIndex = rowIndex + 1
                                        });
                                    }
                                    break;
                                case DataTypeEnum.Decimal:
                                    Decimal decimalvalue = 0;
                                    var parsedecimal = Decimal.TryParse(sColumn, out decimalvalue);
                                    if (!parsedecimal)
                                    {
                                        throw new ParseException($"The Column {row.IndexOf(sColumn)} can't be parsed to : Decimal ",
                                        new ParseExceptionParameter()
                                        {
                                            csv = row,
                                            columnIndex = row.IndexOf(sColumn),
                                            rowIndex = rowIndex + 1
                                        });
                                    }
                                    break;
                            }
                            dr[columnName.Value] = sColumn;
                        }
                    }
                }
            }
        }

        public DataTable ConvertCSVResult(CSVConvertParameter parameter)
        {
            var filePath = $"{Environment.CurrentDirectory}upload/csv/{parameter.id}.csv";
            DataTable dt = new DataTable("csvTable");
            using (CsvFileReader reader = new CsvFileReader(filePath))
            {
                CsvRow row = new CsvRow();
                int rowcount = 0;
                var columnMap = new Dictionary<int, string>();
                while (reader.ReadRow(row))
                {
                    DataRow dr = dt.NewRow();
                    if (rowcount == 0)
                    {
                        foreach (var sColumn in row)
                        {
                            var column = CreateColumn(parameter, sColumn, row.IndexOf(sColumn));
                            if (column != null)
                            {
                                columnMap.Add(row.IndexOf(sColumn), column.ColumnName);
                                dt.Columns.Add(column);
                            }
                        }
                        if (!parameter.HasHeader)
                        {
                            PopulateRow(parameter, row, columnMap, dr, rowcount);
                            dt.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        PopulateRow(parameter, row, columnMap, dr, rowcount);
                        dt.Rows.Add(dr);
                    }
                    rowcount++;
                }
            }
            return dt;
        }

        public DataTableViewModel GetDataTableResult(DataTable dt)
        {
            var res = new DataTableViewModel();
            foreach (DataColumn column in dt.Columns)
            {
                res.headers.Add(column.ColumnName);
            }
            foreach (DataRow dr in dt.Rows)
            {
                var row = new DataRowViewModel();
                row.values = dr.ItemArray.Select(x =>
                {
                    return x.ToString();
                }).ToList();
                res.rows.Add(row);
            }
            return res;
        }
    }
}
