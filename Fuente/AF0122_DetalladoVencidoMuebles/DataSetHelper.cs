using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ExcelLibrary.SpreadSheet;
using System.IO;

namespace AF0122_DetalladoVencidoMuebles
{
    class DataSetHelper
    {
        public static DataSet CreateDataSet(string filePath)
        {
            DataSet dataSet = new DataSet();
            foreach (Worksheet ws in Workbook.Load(filePath).Worksheets)
            {
                DataTable table = DataSetHelper.PopulateDataTable(ws);
                dataSet.Tables.Add(table);
            }
            return dataSet;
        }

        public static DataTable CreateDataTable(string filePath, string sheetName)
        {
            foreach (Worksheet ws in Workbook.Load(filePath).Worksheets)
            {
                if (ws.Name.Equals(sheetName))
                    return DataSetHelper.PopulateDataTable(ws);
            }
            return (DataTable)null;
        }

        private static DataTable PopulateDataTable(Worksheet ws)
        {
            CellCollection cellCollection = ws.Cells;
            DataTable dataTable = new DataTable(ws.Name);
            for (int index = 0; index <= cellCollection.LastColIndex; ++index)
                dataTable.Columns.Add(cellCollection[0, index].StringValue, typeof(string));
            for (int index1 = 1; index1 <= cellCollection.LastRowIndex; ++index1)
            {
                DataRow row = dataTable.NewRow();
                for (int index2 = 0; index2 <= cellCollection.LastColIndex; ++index2)
                    row[index2] = (object)cellCollection[index1, index2].StringValue;
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }

        public static void CreateWorkbook(string filePath, DataSet dataset)
        {
            if (dataset.Tables.Count == 0)
                throw new ArgumentException("DataSet needs to have at least one DataTable", "dataset");
            Workbook workbook = new Workbook();
            foreach (DataTable dataTable in (InternalDataCollectionBase)dataset.Tables)
            {
                Worksheet worksheet = new Worksheet(dataTable.TableName);
                for (int index1 = 0; index1 < dataTable.Columns.Count; ++index1)
                {
                    worksheet.Cells[0, index1] = new Cell((object)dataTable.Columns[index1].ColumnName);
                    for (int index2 = 0; index2 < dataTable.Rows.Count; ++index2)
                        worksheet.Cells[index2 + 1, index1] = new Cell(dataTable.Rows[index2][index1]);
                }
                workbook.Worksheets.Add(worksheet);
            }
            workbook.Save(filePath);
        }

        public static void CreateWorkbook(Stream stream, DataSet dataset)
        {
            if (dataset.Tables.Count == 0)
                throw new ArgumentException("DataSet needs to have at least one DataTable", "dataset");
            Workbook workbook = new Workbook();
            foreach (DataTable dataTable in (InternalDataCollectionBase)dataset.Tables)
            {
                Worksheet worksheet = new Worksheet(dataTable.TableName);
                for (int index1 = 0; index1 < dataTable.Columns.Count; ++index1)
                {
                    worksheet.Cells[0, index1] = new Cell((object)dataTable.Columns[index1].ColumnName);
                    for (int index2 = 0; index2 < dataTable.Rows.Count; ++index2)
                        worksheet.Cells[index2 + 1, index1] = new Cell(dataTable.Rows[index2][index1]);
                }
                workbook.Worksheets.Add(worksheet);
            }
            workbook.SaveToStream(stream);
        }
    }
}
