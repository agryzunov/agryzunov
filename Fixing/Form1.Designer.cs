namespace Fixing
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnIsin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnBid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAsk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comboBrokers = new System.Windows.Forms.ComboBox();
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonSelectFile = new System.Windows.Forms.Button();
            this.listBoxLoaded = new System.Windows.Forms.ListBox();
            this.labelLoaded = new System.Windows.Forms.Label();
            this.buttonSaveExcel = new System.Windows.Forms.Button();
            this.buttonCalcDelta = new System.Windows.Forms.Button();
            this.labelPrevFile = new System.Windows.Forms.Label();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.labelCount = new System.Windows.Forms.Label();
            this.checkBoxFilter = new System.Windows.Forms.CheckBox();
            this.labelAmount = new System.Windows.Forms.Label();
            this.checkExcludeMaxMin = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(604, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 21);
            this.button1.TabIndex = 0;
            this.button1.Text = "Загрузить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnName,
            this.ColumnIsin,
            this.ColumnBid,
            this.ColumnAsk});
            this.dataGridView1.Location = new System.Drawing.Point(121, 86);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(729, 338);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // ColumnName
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ColumnName.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnName.HeaderText = "Имя";
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.ReadOnly = true;
            this.ColumnName.Width = 200;
            // 
            // ColumnIsin
            // 
            this.ColumnIsin.HeaderText = "ISIN";
            this.ColumnIsin.Name = "ColumnIsin";
            this.ColumnIsin.ReadOnly = true;
            // 
            // ColumnBid
            // 
            dataGridViewCellStyle2.Format = "N3";
            dataGridViewCellStyle2.NullValue = null;
            this.ColumnBid.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnBid.HeaderText = "Bid";
            this.ColumnBid.Name = "ColumnBid";
            this.ColumnBid.ReadOnly = true;
            this.ColumnBid.Width = 70;
            // 
            // ColumnAsk
            // 
            dataGridViewCellStyle3.Format = "N3";
            dataGridViewCellStyle3.NullValue = null;
            this.ColumnAsk.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnAsk.HeaderText = "Ask";
            this.ColumnAsk.Name = "ColumnAsk";
            this.ColumnAsk.ReadOnly = true;
            this.ColumnAsk.Width = 70;
            // 
            // comboBrokers
            // 
            this.comboBrokers.FormattingEnabled = true;
            this.comboBrokers.Location = new System.Drawing.Point(9, 26);
            this.comboBrokers.Margin = new System.Windows.Forms.Padding(2);
            this.comboBrokers.Name = "comboBrokers";
            this.comboBrokers.Size = new System.Drawing.Size(107, 21);
            this.comboBrokers.TabIndex = 2;
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Location = new System.Drawing.Point(121, 26);
            this.textBoxFileName.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.Size = new System.Drawing.Size(449, 20);
            this.textBoxFileName.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(121, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Файл для загрузки";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Оператор";
            // 
            // buttonSelectFile
            // 
            this.buttonSelectFile.Location = new System.Drawing.Point(574, 24);
            this.buttonSelectFile.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSelectFile.Name = "buttonSelectFile";
            this.buttonSelectFile.Size = new System.Drawing.Size(26, 21);
            this.buttonSelectFile.TabIndex = 6;
            this.buttonSelectFile.Text = "...";
            this.buttonSelectFile.UseVisualStyleBackColor = true;
            this.buttonSelectFile.Click += new System.EventHandler(this.buttonSelectFile_Click);
            // 
            // listBoxLoaded
            // 
            this.listBoxLoaded.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxLoaded.FormattingEnabled = true;
            this.listBoxLoaded.Location = new System.Drawing.Point(9, 82);
            this.listBoxLoaded.Name = "listBoxLoaded";
            this.listBoxLoaded.Size = new System.Drawing.Size(106, 342);
            this.listBoxLoaded.TabIndex = 7;
            // 
            // labelLoaded
            // 
            this.labelLoaded.AutoSize = true;
            this.labelLoaded.Location = new System.Drawing.Point(10, 65);
            this.labelLoaded.Name = "labelLoaded";
            this.labelLoaded.Size = new System.Drawing.Size(64, 13);
            this.labelLoaded.TabIndex = 8;
            this.labelLoaded.Text = "Загружены";
            // 
            // buttonSaveExcel
            // 
            this.buttonSaveExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveExcel.Location = new System.Drawing.Point(744, 427);
            this.buttonSaveExcel.Name = "buttonSaveExcel";
            this.buttonSaveExcel.Size = new System.Drawing.Size(106, 23);
            this.buttonSaveExcel.TabIndex = 9;
            this.buttonSaveExcel.Text = "В Excel";
            this.buttonSaveExcel.UseVisualStyleBackColor = true;
            this.buttonSaveExcel.Click += new System.EventHandler(this.buttonSaveExcel_Click);
            // 
            // buttonCalcDelta
            // 
            this.buttonCalcDelta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCalcDelta.Location = new System.Drawing.Point(121, 427);
            this.buttonCalcDelta.Name = "buttonCalcDelta";
            this.buttonCalcDelta.Size = new System.Drawing.Size(128, 23);
            this.buttonCalcDelta.TabIndex = 10;
            this.buttonCalcDelta.Text = "Расчёт изменений от";
            this.buttonCalcDelta.UseVisualStyleBackColor = true;
            this.buttonCalcDelta.Click += new System.EventHandler(this.button2_Click);
            // 
            // labelPrevFile
            // 
            this.labelPrevFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelPrevFile.AutoSize = true;
            this.labelPrevFile.Location = new System.Drawing.Point(255, 432);
            this.labelPrevFile.Name = "labelPrevFile";
            this.labelPrevFile.Size = new System.Drawing.Size(59, 13);
            this.labelPrevFile.TabIndex = 11;
            this.labelPrevFile.Text = "нет файла";
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDelete.Location = new System.Drawing.Point(13, 427);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(102, 23);
            this.buttonDelete.TabIndex = 12;
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(164, 65);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(13, 13);
            this.labelCount.TabIndex = 13;
            this.labelCount.Text = "0";
            // 
            // checkBoxFilter
            // 
            this.checkBoxFilter.AutoSize = true;
            this.checkBoxFilter.Location = new System.Drawing.Point(234, 49);
            this.checkBoxFilter.Name = "checkBoxFilter";
            this.checkBoxFilter.Size = new System.Drawing.Size(325, 17);
            this.checkBoxFilter.TabIndex = 14;
            this.checkBoxFilter.Text = "Показывать бумаги, котируемые не менее ? операторами";
            this.checkBoxFilter.UseVisualStyleBackColor = true;
            this.checkBoxFilter.CheckedChanged += new System.EventHandler(this.checkBoxFilter_CheckedChanged);
            // 
            // labelAmount
            // 
            this.labelAmount.AutoSize = true;
            this.labelAmount.Location = new System.Drawing.Point(121, 65);
            this.labelAmount.Name = "labelAmount";
            this.labelAmount.Size = new System.Drawing.Size(40, 13);
            this.labelAmount.TabIndex = 15;
            this.labelAmount.Text = "Всего:";
            // 
            // checkExcludeMaxMin
            // 
            this.checkExcludeMaxMin.AutoSize = true;
            this.checkExcludeMaxMin.Checked = true;
            this.checkExcludeMaxMin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkExcludeMaxMin.Location = new System.Drawing.Point(234, 65);
            this.checkExcludeMaxMin.Margin = new System.Windows.Forms.Padding(2);
            this.checkExcludeMaxMin.Name = "checkExcludeMaxMin";
            this.checkExcludeMaxMin.Size = new System.Drawing.Size(447, 17);
            this.checkExcludeMaxMin.TabIndex = 16;
            this.checkExcludeMaxMin.Text = "Отбрасывать максимальное и минимальное значение, если операторов больше 5";
            this.checkExcludeMaxMin.UseVisualStyleBackColor = true;
            this.checkExcludeMaxMin.CheckedChanged += new System.EventHandler(this.checkExcludeMaxMin_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 454);
            this.Controls.Add(this.checkExcludeMaxMin);
            this.Controls.Add(this.labelAmount);
            this.Controls.Add(this.checkBoxFilter);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.labelPrevFile);
            this.Controls.Add(this.buttonCalcDelta);
            this.Controls.Add(this.buttonSaveExcel);
            this.Controls.Add(this.labelLoaded);
            this.Controls.Add(this.listBoxLoaded);
            this.Controls.Add(this.buttonSelectFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxFileName);
            this.Controls.Add(this.comboBrokers);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "НФА. Расчёт фиксинга";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnIsin;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnBid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAsk;
        private System.Windows.Forms.ComboBox comboBrokers;
        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonSelectFile;
        private System.Windows.Forms.ListBox listBoxLoaded;
        private System.Windows.Forms.Label labelLoaded;
        private System.Windows.Forms.Button buttonSaveExcel;
        private System.Windows.Forms.Button buttonCalcDelta;
        private System.Windows.Forms.Label labelPrevFile;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.CheckBox checkBoxFilter;
        private System.Windows.Forms.Label labelAmount;
        private System.Windows.Forms.CheckBox checkExcludeMaxMin;
    }
}

