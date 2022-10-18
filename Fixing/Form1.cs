using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace Fixing
{
    // Главное окно приложения
    public partial class Form1 : Form
    {
        public SecItemsData secItemsData;
        public List<ShortSecItemInfo> oldMIRPValues;
        #region Private Members
        private ContextMenuStrip listboxContextMenu;
        private int FilterOperatorMinCount = 3;
        #endregion
        public Form1()
        {
            InitializeComponent();
            LoadBrokers();
            secItemsData = new SecItemsData();
        }

        // Список уполномоченнвх организаций
        public List<BrokerItem> ReadBrokersList(string strXlsFileName)
        {
            List<BrokerItem> xlsRows = new List<BrokerItem>();

            XSSFWorkbook xssWorkbook = null;
            using (var stream = new FileStream(strXlsFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                try
                {
                    xssWorkbook = new XSSFWorkbook(stream);
                    ISheet sheet = xssWorkbook.GetSheetAt(0);
                    int rowCount = sheet.LastRowNum;
                    for (int i = 1; i <= rowCount; i++)
                    {
                        IRow row = sheet.GetRow(i);

                        string strName;
                        string strCode;
                        string strDescription;

                        ICell cell = null;

                        cell = row.GetCell(0);
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            strCode = cell.ToString();
                        }
                        else strCode = "";

                        cell = row.GetCell(1);
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            strName = cell.ToString();
                        }
                        else strName = "";

                        cell = row.GetCell(2);
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            strDescription = cell.ToString();
                        }
                        else strDescription = "";


                        if (strCode.Length > 0)
                        {
                            xlsRows.Add(new BrokerItem(strCode, strName, strDescription));
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Excel read error");
                }
            }

            return xlsRows;
        }

        public SecItemsData ReadXLS(string strFileName, string strBrokerCode)
        {
            //List<SecItem> xlsRows = new List<SecItem>();
            //SecItemsData secItemsData = new SecItemsData();
            XSSFWorkbook xssWorkbook = null;
            using (var stream = new FileStream(strFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                try
                {
                    xssWorkbook = new XSSFWorkbook(stream);
                    ISheet sheet = xssWorkbook.GetSheetAt(0);
                    int rowCount = sheet.LastRowNum;
                    for (int i = 1; i <= rowCount; i++)
                    {
                        IRow row = sheet.GetRow(i);

                        string strName;
                        string strISIN;
                        decimal dBid;
                        decimal dAsk;
                        ICell cell = null; ;

                        cell = row.GetCell(0);
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            strName = cell.ToString().Trim();
                        }
                        else strName = "";

                        cell = row.GetCell(1);
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            strISIN = cell.ToString().Trim();
                        }
                        else strISIN = "";

                        cell = row.GetCell(2);
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            string sValue = cell.ToString().Trim();
                            if(sValue.Contains(".")) sValue = sValue.Replace(".", ",");
                            try
                            {
                                dBid = System.Convert.ToDecimal(sValue);
                            }
                            catch(Exception ex)
                            { dBid = 0; }
                        }
                        else dBid = 0; 

                        cell = row.GetCell(3);
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            string sValue = cell.ToString().Trim();
                            if (sValue.Contains(".")) sValue = sValue.Replace(".", ",");
                            try
                            {
                                dAsk = System.Convert.ToDecimal(sValue);
                            }
                            catch (Exception ex)
                            { dAsk = 0; }
                        }
                        else dAsk = 0; 

                        if (strISIN.Length > 0 && dBid > 0 && dAsk > 0)
                        {
                            secItemsData.AddSecItemInfo(strBrokerCode, strName, strISIN, dBid, dAsk); 
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Excel read error");
                }

            }

            return secItemsData;
        }

        private void LoadBrokers()
        {
            List<BrokerItem> xlsBrokers = ReadBrokersList("Справочник.xlsx");
            foreach(BrokerItem bItem in xlsBrokers)
            {
                ComboboxItem item = new ComboboxItem();
                item.Name = bItem.Name;
                item.Code = bItem.Code;
                comboBrokers.Items.Add(item);
            }
            comboBrokers.SelectedIndex = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if(! System.IO.File.Exists(textBoxFileName.Text))
            {
                MessageBox.Show("Файл " + textBoxFileName.Text + " не существует");
                return;
            }
            string strFileExtension = System.IO.Path.GetExtension(textBoxFileName.Text);
            if (!(strFileExtension.Equals(".xlsx") || (strFileExtension.Equals(".csv"))))
            {
                MessageBox.Show("Неверный тип файла: " + strFileExtension +".\r\nВозможна загрузка только XSLX или CSV");
                return;
            }

            ComboboxItem comboItem = (ComboboxItem) comboBrokers.SelectedItem.Copy();
            if (comboItem == null) return;
            string strBrokerCode = (string) comboItem.Code;

            //Проверяем на повторную загрузку
            foreach(object oi in listBoxLoaded.Items)
            {
                ListBoxItem li = (ListBoxItem)oi;
                if(li.Code == strBrokerCode)
                {
                    MessageBox.Show("Данные оператора уже загружены");
                    return;
                }
            }


            //List<SecItem> xlsRows = ReadXLS(textBoxFileName.Text, strBrokerCode);
            //int rowCount = xlsRows.Count;
            if (strFileExtension.Equals(".xlsx")) ReadXLS(textBoxFileName.Text, strBrokerCode);
            else if (strFileExtension.Equals(".csv"))
            {
                CSVReader csvReader = new CSVReader();
                csvReader.ReadCSV(textBoxFileName.Text, strBrokerCode, secItemsData);
            }

            ListBoxItem lbItem = new ListBoxItem();
            lbItem.Name = comboItem.Name;
            lbItem.Code = comboItem.Code;
            lbItem.Count = secItemsData.CountForOperator(comboItem.Code);

            listBoxLoaded.Items.Add(lbItem);
            //comboBrokers.Items.Remove(comboBrokers.SelectedItem);
            UpdateDataGrid(checkBoxFilter.Checked);
           
        }

        private void UpdateDataGrid(bool bFilter = false)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            DataGridViewTextBoxColumn colName = new DataGridViewTextBoxColumn();
            colName.HeaderText = "Name";
            colName.Width = 200;
            dataGridView1.Columns.Add(colName);

            DataGridViewTextBoxColumn colISIN = new DataGridViewTextBoxColumn();
            colISIN.HeaderText = "ISIN";
            dataGridView1.Columns.Add(colISIN);

            DataGridViewTextBoxColumn colCount = new DataGridViewTextBoxColumn();
            colCount.HeaderText = "Всего";
            colCount.Width = 50;
            dataGridView1.Columns.Add(colCount);

            DataGridViewTextBoxColumn colMIRP = new DataGridViewTextBoxColumn();
            colMIRP.HeaderText = "MIRP";
            colMIRP.Width = 50;
            dataGridView1.Columns.Add(colMIRP);

            DataGridViewTextBoxColumn colDelta = new DataGridViewTextBoxColumn();
            colDelta.HeaderText = "Изм.";
            colDelta.Width = 50;
            dataGridView1.Columns.Add(colDelta);
            int columnBidNum = 0;
            int columnAskNum = 0;
            foreach (ListBoxItem listBoxItem in listBoxLoaded.Items)
            {
                // Добавляем столбцы для данных брокерам
                string strCurrentBrokerName = listBoxItem.ToString();
                string strCurrentBrokerCode = listBoxItem.Code;
                DataGridViewTextBoxColumn colBid = new DataGridViewTextBoxColumn();
                colBid.HeaderText = strCurrentBrokerName + "\r\nBid";
                colBid.Tag = new ColumnTag(strCurrentBrokerCode, "Bid");
                dataGridView1.Columns.Add(colBid);

                columnBidNum = dataGridView1.Columns.Count - 1;
                DataGridViewTextBoxColumn colAsk = new DataGridViewTextBoxColumn();
                colAsk.HeaderText = " \r\nAsk";
                colAsk.Tag = new ColumnTag(strCurrentBrokerCode, "Ask");
                dataGridView1.Columns.Add(colAsk);
                columnAskNum = dataGridView1.Columns.Count - 1;
                //dataGridView1.Rows.Add("", "", "Котировоки", strBrokerCode);
            }

            int secItemsCount = 0;
            int secItemsCountFiltered = 0;
            foreach (SecItemInfo si in secItemsData.secItemData)
            {
                // Apply filter
                secItemsCount++;
                //
                int numColumns = listBoxLoaded.Items.Count * 2 + 5;
                object[] gridRow = new object[numColumns];
                gridRow[0] = si.Name;
                gridRow[1] = si.ISIN;
                if (si.MIRP > 0) gridRow[3] = si.MIRP.ToString("0.0000").Replace(".", ","); // format...
                int secCount = 0;
                List<int> redColumms = new List<int>();
                foreach (BrokerItemInfo bInfo in si.brokerItemInfo)
                {
                    string strBrokerBid = bInfo.Bid.ToString();
                    string strBrokerAsk = bInfo.Ask.ToString();
                    string strCurrBrokerCode = bInfo.BrokerCode;

                    for (int i = 5; i < dataGridView1.Columns.Count; i++)
                    {
                        DataGridViewTextBoxColumn col = (DataGridViewTextBoxColumn)dataGridView1.Columns[i];
                        ColumnTag cTag = (ColumnTag)col.Tag;
                        if (cTag.BrokerCode.Equals(strCurrBrokerCode))
                        {
                            if (cTag.ColumnType.Equals("Bid"))
                            {
                                gridRow[i] = strBrokerBid;
                                if (bInfo.Bid >= bInfo.Ask) redColumms.Add(i);
                                secCount++;
                            }
                            else if (cTag.ColumnType.Equals("Ask"))
                            {
                                gridRow[i] = strBrokerAsk;
                                if (bInfo.Bid >= bInfo.Ask) redColumms.Add(i);
                            }
                        }
                    }
                }
                gridRow[2] = secCount.ToString();
                int iRow = 0;
                if (bFilter)
                {
                    if (secCount >= FilterOperatorMinCount)
                    {
                        iRow = dataGridView1.Rows.Add(gridRow);
                        secItemsCountFiltered++;
                    }
                }
                else iRow = dataGridView1.Rows.Add(gridRow);
                foreach (int iRed in redColumms)
                {
                    dataGridView1.Rows[iRow].Cells[iRed].Style.ForeColor = Color.Red;
                }
            }

            //for (int iRow = 0; iRow < dataGridView1.Rows.Count; iRow++)
            //{
            //    for (int iCol = 5; iCol < dataGridView1.ColumnCount; iCol += 2)
            //    {
            //        dataGridView1.Rows[iRow].Cells[iCol].
            //    }
            //}

            CalcDelta(oldMIRPValues);
            if (checkBoxFilter.Checked) labelCount.Text = secItemsCountFiltered.ToString() + "(" + secItemsCount.ToString() + ")";
            else labelCount.Text = secItemsCount.ToString();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //assign a contextmenustrip
            listboxContextMenu = new ContextMenuStrip();
            listboxContextMenu.Opening += new CancelEventHandler(listboxContextMenu_Opening);
            listboxContextMenu.Click += new EventHandler(listboxContextMenu_Click);
            listBoxLoaded.ContextMenuStrip = listboxContextMenu;
            if(FilterOperatorMinCount> 1)
               checkBoxFilter.Text = "Показывать бумаги, котируемые не менее " + FilterOperatorMinCount.ToString() + " операторами";
            else checkBoxFilter.Text = "Показывать бумаги, котируемые не менее " + FilterOperatorMinCount.ToString() + " оператором";
        }

        private void listBoxLoaded_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //select the item under the mouse pointer
                listBoxLoaded.SelectedIndex = listBoxLoaded.IndexFromPoint(e.Location);
                if (listBoxLoaded.SelectedIndex != -1)
                {
                    listboxContextMenu.Show();
                }
            }
        }

        private void listboxContextMenu_Opening(object sender, CancelEventArgs e)
        {
            listboxContextMenu.Items.Clear();
            //clear the menu and add custom items
            if (listBoxLoaded.SelectedItem != null)
            {
                listboxContextMenu.Items.Add(string.Format("Удалить - {0}", listBoxLoaded.SelectedItem.ToString()));
            }
        }

        private void listboxContextMenu_Click(object sender, EventArgs e)
        {
            if (listBoxLoaded.SelectedItem != null)
            {
                DialogResult dialogResult = MessageBox.Show(string.Format("Удалить данные оператора - {0}?",  listBoxLoaded.SelectedItem.ToString()), "Удаление данные оператора", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    // Удаление данных оператора
                    ListBoxItem li = (ListBoxItem) listBoxLoaded.SelectedItem;
                    int deletedRecords = ClearOperatorData(li.Code);
                }
            }
        }

        private int  ClearOperatorData(string strOperatorCode)
        {
            int deletedRecords = 0;
            deletedRecords = secItemsData.ClearOperatorData(strOperatorCode);
            return deletedRecords;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.AddExtension=true;
            ofd.DefaultExt = "xlsx";
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.Title = "Выберите файл с данными для загрузки";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBoxFileName.Text = ofd.FileName;
            }
        }

        private List<ShortSecItemInfo> LoadMIRPFromXLS(string strFileName)
        {
            List<ShortSecItemInfo> listSecInfo = new List<ShortSecItemInfo>();
            XSSFWorkbook workbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                workbook = new XSSFWorkbook(file);
            }

            ISheet sheet = workbook.GetSheet("MIRP");
            if (sheet == null)
            {
                MessageBox.Show("Не найдена закладка MIRP - данные не загружены");
                return null;
            }
            for (int row = 2; row <= sheet.LastRowNum; row++)
            {
                if (sheet.GetRow(row) != null) //null is when the row only contains empty cells 
                {
                    string strISIN = sheet.GetRow(row).GetCell(1).StringCellValue;

                    decimal decMIRP = 0;
                    string strMIRP = null;
                    try
                    {
                        string sVal = sheet.GetRow(row).GetCell(3).StringCellValue;
                        decMIRP = (decimal) System.Convert.ToDecimal(sVal);
                        strMIRP = null;
                    }
                    catch(Exception ex) {
                        try
                        {
                            strMIRP = sheet.GetRow(row).GetCell(3).StringCellValue;
                        }
                        catch (Exception ex2) {
                            string err2 = ex.Message;
                            strMIRP = null;
                        }
                        string err = ex.Message;
                    }

                    if(strMIRP != null)
                    {
                        try
                        {
                            decMIRP = System.Convert.ToDecimal(strMIRP);
                        }
                        catch (Exception ex3) {
                            string err3 = ex3.Message;
                        }
                    }
                    if (decMIRP > 0) listSecInfo.Add(new ShortSecItemInfo(strISIN, decMIRP));
                }
            }

            return listSecInfo;
        }

        private void CalcDelta(List<ShortSecItemInfo> listSecInfo)
        {
            if (listSecInfo == null) return;
            for(int i=0; i < dataGridView1.Rows.Count; i++)
            {
                string strMIRP = null;

                try { strMIRP = dataGridView1.Rows[i].Cells[3].Value.ToString(); }
                catch (Exception ex) { strMIRP = ""; }
                if (dataGridView1.Rows[i].Cells[1].Value != null)
                {
                    string strISIN = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    if (strMIRP.Length > 0)
                    {
                        decimal decCurrentValue = System.Convert.ToDecimal(strMIRP);
                        // Ищем в списке значений ISIN
                        foreach (ShortSecItemInfo si in listSecInfo)
                        {
                            if (si.ISIN.Equals(strISIN))
                            {
                                decimal delta = decCurrentValue - si.MIRP;
                                dataGridView1.Rows[i].Cells[4].Value = delta.ToString("0.####");
                                if ((delta >= 2) || (delta <= -2))
                                {
                                    dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.Red;
                                }
                                else if ((delta >= 1) || (delta <= -1))
                                {
                                    dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.Yellow;
                                }
                                else
                                {
                                    dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.Green;
                                }
                            }
                        }
                    }
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Выбор файла для расчёта относительнного изменения величины MIRP
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.AddExtension = true;
            ofd.DefaultExt = "xlsx";
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.Title = "Выберите файл для сравнения изменений MIRP";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                labelPrevFile.Text = ofd.FileName;
                // Загружаем значение ISIN-MIRP из из файла
                oldMIRPValues = LoadMIRPFromXLS(ofd.FileName);
                CalcDelta(oldMIRPValues);
            }
            else labelPrevFile.Text = "нет файла";
        }

        private bool SaveToExcel(string strFileName)
        {

            IWorkbook workbook = new XSSFWorkbook();

            IFont boldFont = workbook.CreateFont();
            boldFont.IsBold = true;

            ICellStyle headerStyle1 = workbook.CreateCellStyle();
            headerStyle1.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            headerStyle1.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            headerStyle1.FillPattern = FillPattern.SolidForeground;
            headerStyle1.SetFont(boldFont);

            // White column headers to the workbook
            ISheet sheet = workbook.CreateSheet("MIRP");
            IRow rowHeader = sheet.CreateRow(0);
           
            ICell HeaderCell = rowHeader.CreateCell(0);
            HeaderCell.SetCellValue("Name");
            HeaderCell.CellStyle = headerStyle1;

            HeaderCell = rowHeader.CreateCell(1);
            HeaderCell.SetCellValue("ISIN");
            HeaderCell.CellStyle = headerStyle1;

            HeaderCell = rowHeader.CreateCell(2);
            HeaderCell.SetCellValue("Всего");
            HeaderCell.CellStyle = headerStyle1;

            HeaderCell = rowHeader.CreateCell(3);
            HeaderCell.SetCellValue("MIRP");
            HeaderCell.CellStyle = headerStyle1;


            HeaderCell = rowHeader.CreateCell(4);
            HeaderCell.SetCellValue("Изм.");
            HeaderCell.CellStyle = headerStyle1;

            int i = 5;
            

            foreach (ListBoxItem listBoxItem in listBoxLoaded.Items)
            {
                // Добавляем столбцы для данных брокерам
                string strCurrentBrokerName = listBoxItem.ToString();
                string strCurrentBrokerCode = listBoxItem.Code;

                HeaderCell = rowHeader.CreateCell(i++);
                HeaderCell.SetCellValue(strCurrentBrokerCode);
                HeaderCell.CellStyle = headerStyle1;
                HeaderCell = rowHeader.CreateCell(i++);
                HeaderCell.SetBlank();
                HeaderCell.CellStyle = headerStyle1;
            }

            rowHeader = sheet.CreateRow(1);
            
            ICellStyle headerStyle2 = workbook.CreateCellStyle();
            headerStyle2.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            headerStyle2.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            headerStyle2.FillPattern = FillPattern.SolidForeground;
            headerStyle2.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium;


            ICellStyle redStyle = workbook.CreateCellStyle();
            redStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            redStyle.FillForegroundColor = IndexedColors.Red.Index;
            redStyle.FillPattern = FillPattern.SolidForeground;

            ICellStyle yellowStyle = workbook.CreateCellStyle();
            yellowStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            yellowStyle.FillForegroundColor = IndexedColors.Yellow.Index;
            yellowStyle.FillPattern = FillPattern.SolidForeground;

            ICellStyle greenStyle = workbook.CreateCellStyle();
            greenStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            greenStyle.FillForegroundColor = IndexedColors.Green.Index;
            greenStyle.FillPattern = FillPattern.SolidForeground;

            for (i = 0; i < 5; i++)
            {
                HeaderCell = rowHeader.CreateCell(i);
                HeaderCell.SetBlank();
                HeaderCell.CellStyle = headerStyle2;
            }

            i = 5;
            foreach (ListBoxItem listBoxItem in listBoxLoaded.Items)
            {
                HeaderCell = rowHeader.CreateCell(i++);
                HeaderCell.SetCellValue("Bid");
                HeaderCell.CellStyle = headerStyle2;

                HeaderCell = rowHeader.CreateCell(i++);
                HeaderCell.SetCellValue("Ask");
                HeaderCell.CellStyle = headerStyle2;
            }

            for (i = 0; i < dataGridView1.RowCount; i++)
            {
                DataGridViewRow dataRow = dataGridView1.Rows[i];
                IRow row = sheet.CreateRow(i+2);
                for (int j=0; j < dataGridView1.Columns.Count; j++)
                {
                    DataGridViewCell dataCell = dataRow.Cells[j];
                    ICell cell = row.CreateCell(j);
                    if (dataCell.Value != null)
                    {
                        try
                        {
                            string s = dataCell.FormattedValue.ToString();
                            if (j > 2) cell.SetCellType(CellType.Numeric);
                            cell.SetCellType(CellType.String);
                            if (j == 4)
                            {
                                //Изменение 
                                if (dataCell.Style.BackColor == Color.Red)
                                {
                                    cell.CellStyle = redStyle;
                                }
                                else if (dataCell.Style.BackColor == Color.Yellow)
                                {
                                    cell.CellStyle = yellowStyle;
                                }
                                else if (dataCell.Style.BackColor == Color.Green)
                                {
                                    cell.CellStyle = greenStyle;
                                }
                            }

                            if (j == 3)
                            {
                                string sVal = dataCell.FormattedValue.ToString();
                                string sFormattedVal = "";
                                decimal dVal = -1;
                                try
                                {
                                    dVal = System.Convert.ToDecimal(sVal);
                                    sFormattedVal = String.Format("{0:0.0000}", dVal);
                                    cell.SetCellValue(sFormattedVal);
                                }
                                catch(Exception ex)
                                {
                                    //
                                }
                            }
                            else cell.SetCellValue(dataCell.FormattedValue.ToString());
                            

                            cell.CellStyle.ShrinkToFit = true;
                            cell.CellStyle.VerticalAlignment = VerticalAlignment.Center;
                        }
                        catch(Exception ex)
                        {
                            cell.SetBlank();
                        }
                    }
                }
            }
            // ... write data to the workbook ...
            //IRow row = sheet.CreateRow(1);
            //ICell cell = row.CreateCell(0);
            //cell.SetCellValue("Hello");
            //cell = row.CreateCell(1);
            //cell.SetCellValue("World");

            using (FileStream stream = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
            {
                try
                {
                    sheet.SetColumnWidth(0, 40 * 256);
                    sheet.SetColumnWidth(1, 14 * 256);
                    workbook.Write(stream);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении данных " + ex.Message);
                    return false;
                }
            }
            return true;
        }
        private void buttonSaveExcel_Click(object sender, EventArgs e)
        {
            // Сохранение данных в Excel
            // Выбор файла для сохранения данных
            string strFileName = null;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.AddExtension = true;
            ofd.DefaultExt = "xlsx";
            ofd.CheckFileExists = false;
            ofd.CheckPathExists = true;
            ofd.Title = "Выберите файл для сохранения данных";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                strFileName = ofd.FileName;
            }
            else strFileName = null;

            if(strFileName == null)
            {
                MessageBox.Show("Сохранение данных отменено");
            }
            else
            {
                if (SaveToExcel(strFileName)) MessageBox.Show("Данные сохранены в файл " + strFileName);
            }

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxLoaded.SelectedItem != null)
            {
                DialogResult dialogResult = MessageBox.Show(string.Format("Удалить данные оператора - {0}?", listBoxLoaded.SelectedItem.ToString()), "Удаление данные оператора", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    // Удаление данных оператора
                    ListBoxItem li = (ListBoxItem)listBoxLoaded.SelectedItem;
                    int deletedRecords = ClearOperatorData(li.Code);
                    listBoxLoaded.Items.Remove(listBoxLoaded.SelectedItem);
                    UpdateDataGrid(checkBoxFilter.Checked);
                    //comboBrokers.Items.Remove(comboBrokers.SelectedItem);
                }
            }
            else MessageBox.Show("Выберите в списке оператора");
        }

        private void checkBoxFilter_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDataGrid(checkBoxFilter.Checked);
        }

        private void checkExcludeMaxMin_CheckedChanged(object sender, EventArgs e)
        {
            secItemsData.excludeMaxMinForMIRP = checkExcludeMaxMin.Checked;
            UpdateDataGrid(checkBoxFilter.Checked);
        }
    }
}
