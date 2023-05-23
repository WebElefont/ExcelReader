namespace ExcelReader
{
    partial class ExcelReader
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridViewExcel=new DataGridView();
            btnLoadExcel=new Button();
            label1=new Label();
            comboBoxDataType=new ComboBox();
            btnSendToApi=new Button();
            btnCheckImages=new Button();
            btnOpenLogFile=new Button();
            lblImagePath=new Label();
            txtBoxImgPath=new TextBox();
            btnChangeImgPath=new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewExcel).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewExcel
            // 
            dataGridViewExcel.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewExcel.Location=new Point(37, 44);
            dataGridViewExcel.Name="dataGridViewExcel";
            dataGridViewExcel.RowTemplate.Height=25;
            dataGridViewExcel.Size=new Size(755, 341);
            dataGridViewExcel.TabIndex=0;
            // 
            // btnLoadExcel
            // 
            btnLoadExcel.Location=new Point(396, 391);
            btnLoadExcel.Name="btnLoadExcel";
            btnLoadExcel.Size=new Size(75, 23);
            btnLoadExcel.TabIndex=1;
            btnLoadExcel.Text="Load FILE";
            btnLoadExcel.UseVisualStyleBackColor=true;
            btnLoadExcel.MouseClick+=btnLoadExcel_MouseClick;
            // 
            // label1
            // 
            label1.AutoSize=true;
            label1.Location=new Point(285, 15);
            label1.Name="label1";
            label1.Size=new Size(151, 15);
            label1.TabIndex=2;
            label1.Text="Choose type of insert data: ";
            // 
            // comboBoxDataType
            // 
            comboBoxDataType.DropDownStyle=ComboBoxStyle.DropDownList;
            comboBoxDataType.FormattingEnabled=true;
            comboBoxDataType.Items.AddRange(new object[] { "Картридж", "Вугілля", "Одноразка", "Тютюн", "Рідина", "POD" });
            comboBoxDataType.Location=new Point(442, 12);
            comboBoxDataType.Name="comboBoxDataType";
            comboBoxDataType.Size=new Size(121, 23);
            comboBoxDataType.TabIndex=3;
            comboBoxDataType.SelectedIndexChanged+=comboBoxDataType_SelectedIndexChanged;
            // 
            // btnSendToApi
            // 
            btnSendToApi.Location=new Point(388, 421);
            btnSendToApi.Name="btnSendToApi";
            btnSendToApi.Size=new Size(87, 23);
            btnSendToApi.TabIndex=4;
            btnSendToApi.Text="SEND TO API";
            btnSendToApi.UseVisualStyleBackColor=true;
            btnSendToApi.MouseClick+=btnSendToApi_MouseClick;
            // 
            // btnCheckImages
            // 
            btnCheckImages.Location=new Point(383, 450);
            btnCheckImages.Name="btnCheckImages";
            btnCheckImages.Size=new Size(98, 23);
            btnCheckImages.TabIndex=5;
            btnCheckImages.Text="Check Images";
            btnCheckImages.UseVisualStyleBackColor=true;
            btnCheckImages.MouseClick+=btnCheckImages_MouseClick;
            // 
            // btnOpenLogFile
            // 
            btnOpenLogFile.Location=new Point(396, 479);
            btnOpenLogFile.Name="btnOpenLogFile";
            btnOpenLogFile.Size=new Size(75, 23);
            btnOpenLogFile.TabIndex=6;
            btnOpenLogFile.Text="Open LOG";
            btnOpenLogFile.UseVisualStyleBackColor=true;
            btnOpenLogFile.MouseClick+=btnOpenLogFile_MouseClick;
            // 
            // lblImagePath
            // 
            lblImagePath.AutoSize=true;
            lblImagePath.Location=new Point(33, 511);
            lblImagePath.Name="lblImagePath";
            lblImagePath.Size=new Size(125, 15);
            lblImagePath.TabIndex=7;
            lblImagePath.Text="Current images PATH: ";
            // 
            // txtBoxImgPath
            // 
            txtBoxImgPath.Location=new Point(164, 508);
            txtBoxImgPath.Name="txtBoxImgPath";
            txtBoxImgPath.Size=new Size(628, 23);
            txtBoxImgPath.TabIndex=8;
            // 
            // btnChangeImgPath
            // 
            btnChangeImgPath.Location=new Point(372, 537);
            btnChangeImgPath.Name="btnChangeImgPath";
            btnChangeImgPath.Size=new Size(120, 23);
            btnChangeImgPath.TabIndex=9;
            btnChangeImgPath.Text="Change imgs PATH";
            btnChangeImgPath.UseVisualStyleBackColor=true;
            btnChangeImgPath.MouseClick+=btnChangeImgPath_MouseClick;
            // 
            // ExcelReader
            // 
            AutoScaleDimensions=new SizeF(7F, 15F);
            AutoScaleMode=AutoScaleMode.Font;
            ClientSize=new Size(821, 632);
            Controls.Add(btnChangeImgPath);
            Controls.Add(txtBoxImgPath);
            Controls.Add(lblImagePath);
            Controls.Add(btnOpenLogFile);
            Controls.Add(btnCheckImages);
            Controls.Add(btnSendToApi);
            Controls.Add(comboBoxDataType);
            Controls.Add(label1);
            Controls.Add(btnLoadExcel);
            Controls.Add(dataGridViewExcel);
            Name="ExcelReader";
            Text="Form1";
            Load+=ExcelReader_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewExcel).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewExcel;
        private Button btnLoadExcel;
        private Label label1;
        private ComboBox comboBoxDataType;
        private Button btnSendToApi;
        private Button btnCheckImages;
        private Button btnOpenLogFile;
        private Label lblImagePath;
        private TextBox txtBoxImgPath;
        private Button btnChangeImgPath;
    }
}