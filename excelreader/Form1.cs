using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;
using System.Data;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using Range = Microsoft.Office.Interop.Excel.Range;
using System.Data.Common;
using System.Text;
using System;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Linq.Expressions;
using ExcelReader.Services;
using ExcelReader.ApiServices;
using System.Diagnostics;

namespace ExcelReader
{
    public partial class ExcelReader : Form
    {
        private readonly AddGoodsFacade addGoodsFacade;

        bool exception, exceptionOverall = false;
        bool isSucced = true;

        string imgPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        LoggerService logger = new LoggerService();

        List<LiquidCE> liquids = new List<LiquidCE>();
        List<HookahTobaccoCE> tobacco = new List<HookahTobaccoCE>();
        List<ECigaretteCE> eCigarettes = new List<ECigaretteCE>();
        List<CoalCE> coals = new List<CoalCE>();
        List<PodCE> pods = new List<PodCE>();
        List<CartrigeAndVaporizerCE> cartriges = new List<CartrigeAndVaporizerCE>();

        public ExcelReader()
        {
            InitializeComponent();
            addGoodsFacade = new AddGoodsFacade();
        }

        private void btnLoadExcel_MouseClick(object sender, MouseEventArgs e)
        {
            ClearLists();
            exceptionOverall = false;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                switch (comboBoxDataType.SelectedItem)
                {
                    case "Рідина":
                        SetImgPath("Liquids");
                        LoadExcelDataToLiquid(filePath);
                        btnSendToApi.Enabled = true;
                        btnCheckImages.Enabled = true;
                        break;
                    case "Тютюн":
                        SetImgPath("HookahTobacco");
                        LoadExcelDataToTobacco(filePath);
                        btnSendToApi.Enabled = true;
                        btnCheckImages.Enabled = true;
                        break;
                    case "Одноразка":
                        SetImgPath("ECigarettes");
                        LoadExcelDataToECigarettes(filePath);
                        btnSendToApi.Enabled = true;
                        btnCheckImages.Enabled = true;
                        break;
                    case "Вугілля":
                        SetImgPath("Coals");
                        LoadExcelDataToCoal(filePath);                       
                        btnSendToApi.Enabled = true;
                        btnCheckImages.Enabled = true;
                        break;
                    case "POD":
                        SetImgPath("Pods");
                        LoadExcelDataToPods(filePath);
                        btnSendToApi.Enabled = true;
                        btnCheckImages.Enabled = true;
                        break;
                    case "Картридж":
                        SetImgPath("CartrigesAndVaporizers");
                        LoadExcelDataToCartrigesAndVaporizers(filePath);
                        btnSendToApi.Enabled = true;
                        btnCheckImages.Enabled = true;
                        break;
                }
            }
        }

        private void btnSendToApi_MouseClick(object sender, MouseEventArgs e)
        {
            switch (comboBoxDataType.SelectedItem)
            {
                case "Рідина":
                    addGoodsFacade.SendLiquids(liquids);
                    break;
                case "Тютюн":
                    addGoodsFacade.SendHookahTobacco(tobacco);
                    break;
                case "Одноразка":
                    addGoodsFacade.SendECigarettes(eCigarettes);
                    break;
                case "Вугілля":
                    addGoodsFacade.SendCoals(coals);
                    break;
                case "POD":
                    addGoodsFacade.SendPods(pods);
                    break;
                case "Картридж":
                    addGoodsFacade.SendCartriges(cartriges);
                    break;
            }
        }

        private void btnCheckImages_MouseClick(object sender, MouseEventArgs e)
        {
            isSucced = true;
            switch (comboBoxDataType.SelectedItem)
            {
                case "Рідина":
                    foreach (var liquid in liquids)
                    {
                        ImgCheck(liquid.ImageUrl, liquid.Name);
                    }
                    break;
                case "Тютюн":
                    foreach (var tobacc in tobacco)
                    {
                        ImgCheck(tobacc.ImageUrl, tobacc.Name);
                    }
                    break;
                case "Одноразка":
                    foreach (var eCigarette in eCigarettes)
                    {
                        ImgCheck(eCigarette.ImageUrl, eCigarette.Name);
                    }
                    break;
                case "Вугілля":
                    foreach (var coal in coals)
                    {
                        ImgCheck(coal.ImageUrl, coal.Name);
                    }
                    break;
                case "POD":
                    foreach (var pod in pods)
                    {
                        ImgCheck(pod.ImageUrl, pod.Name);
                    }
                    break;
                case "Картридж":
                    foreach (var cartrige in cartriges)
                    {
                        ImgCheck(cartrige.ImageUrl, cartrige.Name);
                    }
                    break;
            }
            ImageCheckLog(isSucced);
        }

        private void btnChangeImgPath_MouseClick(object sender, MouseEventArgs e)
        {
            btnSendToApi.Enabled = false;
            btnCheckImages.Enabled = false;
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                   txtBoxImgPath.Text = fbd.SelectedPath + "\\";
                   imgPath = fbd.SelectedPath + "\\";
                }
            }
        }

        #region Liquid
        private void LoadExcelDataToLiquid(string filePath)
        {
            exception = false;
            Application excel = new Application();
            Workbook workbook = excel.Workbooks.Open(filePath);
            Worksheet worksheet = workbook.Sheets[1];
            Range range = worksheet.UsedRange;

            CreateTable(range);

            for (int row = 2; row <= range.Rows.Count; row++)
            {
                exception = false;
                LiquidCE liquid = new LiquidCE();
                for (int column = 1; column <= range.Columns.Count; column++)
                {
                    try
                    {
                        switch (column)
                        {
                            case 1:
                                liquid.Name = range.Cells[row, column].Value2;
                                break;
                            case 2:
                                liquid.ProducerName = range.Cells[row, column].Value2;
                                break;
                            case 3:
                                liquid.ImageUrl = imgPath + range.Cells[row, column].Value2;
                                break;
                            case 4:
                                liquid.Price = Convert.ToInt16(range.Cells[row, column].Value2);
                                break;
                            case 5:
                                liquid.Capacity = Convert.ToByte(range.Cells[row, column].Value2);
                                break;
                            case 6:
                                liquid.NicotineType = range.Cells[row, column].Value2;
                                break;
                            case 7:
                                liquid.TasteGroup = range.Cells[row, column].Value2;
                                break;
                            case 8:
                                liquid.Taste = range.Cells[row, column].Value2;
                                break;
                            case 9:
                                liquid.NicotineStrength = Convert.ToByte(range.Cells[row, column].Value2);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Log("ERROR! " + ex.Message + " Object - " + liquid.Name + ", Category - " + liquid.Category + ".");
                        exception = true;
                        exceptionOverall = true;
                        break;
                    }

                }
                if (exception)
                {
                    continue;
                }
                else
                {
                    liquids.Add(liquid);
                    logger.Log("Додано товар категорії " + liquid.Category + ", Name - " + liquid.Name + " | ProducerName - " + liquid.ProducerName + " | ImageUrl - " + liquid.ImageUrl + " | Price - " + liquid.Price + " | Capacity - " + liquid.Capacity + " | NicotineType - " + liquid.NicotineType + " | TasteGroup - " + liquid.TasteGroup + " | Taste - " + liquid.Taste + " | NicotineStrength - " + liquid.NicotineStrength);
                }
            }
            if (exceptionOverall)
            {
                MsgBoxError();
            }
            workbook.Close(false);
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
        }
        #endregion
        #region Tobacco
        private void LoadExcelDataToTobacco(string filePath)
        {
            exception = false;
            Application excel = new Application();
            Workbook workbook = excel.Workbooks.Open(filePath);
            Worksheet worksheet = workbook.Sheets[1];
            Range range = worksheet.UsedRange;

            CreateTable(range);

            for (int row = 2; row <= range.Rows.Count; row++)
            {
                exception = false;
                HookahTobaccoCE tobacc = new HookahTobaccoCE();
                for (int column = 1; column <= range.Columns.Count; column++)
                {
                    try
                    {
                        switch (column)
                        {
                            case 1:
                                tobacc.Name = range.Cells[row, column].Value2;
                                break;
                            case 2:
                                tobacc.ProducerName = range.Cells[row, column].Value2;
                                break;
                            case 3:
                                tobacc.ImageUrl = imgPath + range.Cells[row, column].Value2;
                                break;
                            case 4:
                                tobacc.Price = Convert.ToInt16(range.Cells[row, column].Value2);
                                break;
                            case 5:
                                tobacc.Sweet = Convert.ToByte(range.Cells[row, column].Value2);
                                break;
                            case 6:
                                tobacc.Sour = Convert.ToByte(range.Cells[row, column].Value2);
                                break;
                            case 7:
                                tobacc.Spicy = Convert.ToByte(range.Cells[row, column].Value2);
                                break;
                            case 8:
                                tobacc.Fresh = Convert.ToByte(range.Cells[row, column].Value2);
                                break;
                            case 9:
                                tobacc.Taste = range.Cells[row, column].Value2;
                                break;
                            case 10:
                                tobacc.Strength = range.Cells[row, column].Value2;
                                break;
                            case 11:
                                tobacc.Weight = Convert.ToDouble(range.Cells[row, column].Value2);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Log("ERROR! " + ex.Message + " Object - " + tobacc.Name + ", Category - " + tobacc.Category + ".");
                        exception = true;
                        exceptionOverall = true;
                        break;
                    }

                }
                if (exception)
                {
                    continue;
                }
                else
                {
                    tobacco.Add(tobacc);
                    logger.Log("Додано товар категорії " + tobacc.Category + ", Name - " + tobacc.Name + " | ProducerName - " + tobacc.ProducerName + " | ImageUrl - " + tobacc.ImageUrl + " | Price - " + tobacc.Price + " | Sweet - " + tobacc.Sweet + " | Sour - " + tobacc.Sour + " | Fresh - " + tobacc.Fresh + " | Taste - " + tobacc.Taste + " | Spicy - " + tobacc.Spicy + " | Taste - " + tobacc.Taste + " | Strength - " + tobacc.Strength + " | Weight - " + tobacc.Weight);
                }

            }
            if (exceptionOverall)
            {
                MsgBoxError();
            }
            workbook.Close(false);
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
        }
        #endregion
        #region ECigarettes
        private void LoadExcelDataToECigarettes(string filePath)
        {
            exception = false;
            Application excel = new Application();
            Workbook workbook = excel.Workbooks.Open(filePath);
            Worksheet worksheet = workbook.Sheets[1];
            Range range = worksheet.UsedRange;

            CreateTable(range);

            for (int row = 2; row <= range.Rows.Count; row++)
            {
                exception = false;
                ECigaretteCE eCigarette = new ECigaretteCE();
                for (int column = 1; column <= range.Columns.Count; column++)
                {
                    try
                    {
                        switch (column)
                        {
                            case 1:
                                eCigarette.Name = range.Cells[row, column].Value2;
                                break;
                            case 2:
                                eCigarette.ProducerName = range.Cells[row, column].Value2;
                                break;
                            case 3:
                                eCigarette.ImageUrl =  imgPath + range.Cells[row, column].Value2;
                                break;
                            case 4:
                                eCigarette.Price = Convert.ToInt16(range.Cells[row, column].Value2);
                                break;
                            case 5:
                                eCigarette.Sweet = Convert.ToByte(range.Cells[row, column].Value2);
                                break;
                            case 6:
                                eCigarette.Sour = Convert.ToByte(range.Cells[row, column].Value2);
                                break;
                            case 7:
                                eCigarette.Fresh = Convert.ToByte(range.Cells[row, column].Value2);
                                break;
                            case 8:
                                eCigarette.Spicy = Convert.ToByte(range.Cells[row, column].Value2);
                                break;
                            case 9:
                                eCigarette.Taste = range.Cells[row, column].Value2;
                                break;
                            case 10:
                                eCigarette.EvaporatorVolume = Convert.ToByte(range.Cells[row, column].Value2);
                                break;
                            case 11:
                                eCigarette.BattareyCapacity = Convert.ToInt16(range.Cells[row, column].Value2);
                                break;
                            case 12:
                                eCigarette.PuffsCount = Convert.ToInt32(range.Cells[row, column].Value2);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Log("ERROR! " + ex.Message + " Object - " + eCigarette.Name + ", Category - " + eCigarette.Category + ".");
                        exception = true;
                        exceptionOverall = true;
                        break;
                    }
                }
                if (exception)
                {
                    continue;
                }
                else
                {
                    eCigarettes.Add(eCigarette);
                    logger.Log("Додано товар категорії " + eCigarette.Category + ", Name - " + eCigarette.Name + " | ProducerName - " + eCigarette.ProducerName + " | ImageUrl - " + eCigarette.ImageUrl + " | Price - " + eCigarette.Price + " | Sweet - " + eCigarette.Sweet + " | Sour - " + eCigarette.Sour + " | Fresh - " + eCigarette.Fresh + " | Spicy - " + eCigarette.Spicy + " | Taste - " + eCigarette.Taste + " | EvaporatorVolume - " + eCigarette.EvaporatorVolume + " | BattareyCapacity - " + eCigarette.BattareyCapacity + " | PuffsCount - " + eCigarette.PuffsCount);
                }

            }
            if (exceptionOverall)
            {
                MsgBoxError();
            }
            workbook.Close(false);
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
        }
        #endregion
        #region Coal
        private void LoadExcelDataToCoal(string filePath)
        {
            exception = false;
            Application excel = new Application();
            Workbook workbook = excel.Workbooks.Open(filePath);
            Worksheet worksheet = workbook.Sheets[1];
            Range range = worksheet.UsedRange;

            CreateTable(range);

            for (int row = 2; row <= range.Rows.Count; row++)
            {
                exception = false;
                CoalCE coal = new CoalCE();
                for (int column = 1; column <= range.Columns.Count; column++)
                {
                    try
                    {
                        switch (column)
                        {
                            case 1:
                                coal.Name = range.Cells[row, column].Value2;
                                break;
                            case 2:
                                coal.ProducerName = range.Cells[row, column].Value2;
                                break;
                            case 3:
                                coal.ImageUrl = imgPath + range.Cells[row, column].Value2;
                                break;
                            case 4:
                                coal.Price = Convert.ToInt16(range.Cells[row, column].Value2);
                                break;
                            case 5:
                                coal.Type = range.Cells[row, column].Value2;
                                break;
                            case 6:
                                coal.Weight = Convert.ToDouble(range.Cells[row, column].Value2);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Log("ERROR! " + ex.Message + " Object - " + coal.Name + ", Category - " + coal.Category + ".");
                        exception = true;
                        exceptionOverall = true;
                        break;
                    }
                }
                if (exception)
                {
                    continue;
                }
                else
                {
                    coals.Add(coal);
                    logger.Log("Додано товар категорії " + coal.Category + ", Name - " + coal.Name + " | ProducerName - " + coal.ProducerName + " | ImageUrl - " + coal.ImageUrl + " | Price - " + coal.Price + " | Type - " + coal.Type + " | Weight - " + coal.Weight);
                }

            }
            if (exceptionOverall)
            {
                MsgBoxError();
            }
            workbook.Close(false);
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
        }
        #endregion
        #region Pods
        private void LoadExcelDataToPods(string filePath)
        {
            exception = false;
            Application excel = new Application();
            Workbook workbook = excel.Workbooks.Open(filePath);
            Worksheet worksheet = workbook.Sheets[1];
            Range range = worksheet.UsedRange;

            CreateTable(range);

            for (int row = 2; row <= range.Rows.Count; row++)
            {
                exception = false;
                PodCE pod = new PodCE();
                for (int column = 1; column <= range.Columns.Count; column++)
                {
                    try
                    {
                        switch (column)
                        {
                            case 1:
                                pod.Name = range.Cells[row, column].Value2;
                                break;
                            case 2:
                                pod.ProducerName = range.Cells[row, column].Value2;
                                break;
                            case 3:
                                pod.ImageUrl = imgPath + range.Cells[row, column].Value2;
                                break;
                            case 4:
                                pod.Price = Convert.ToInt16(range.Cells[row, column].Value2);
                                break;
                            case 5:
                                pod.Weight = Convert.ToDouble(range.Cells[row, column].Value2);
                                break;
                            case 6:
                                pod.Material = range.Cells[row, column].Value2;
                                break;
                            case 7:
                                pod.Battarey = Convert.ToInt16(range.Cells[row, column].Value2);
                                break;
                            case 8:
                                pod.CartrigeCapacity = Convert.ToDouble(range.Cells[row, column].Value2);
                                break;
                            case 9:
                                pod.EvaporatorResistance = Convert.ToDouble(range.Cells[row, column].Value2);
                                break;
                            case 10:
                                pod.Power = Convert.ToString(range.Cells[row, column].Value2);
                                break;
                            case 11:
                                pod.Port = range.Cells[row, column].Value2;
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Log("ERROR! " + ex.Message + " Object - " + pod.Name + ", Category - " + pod.Category + "." + " Cell value - " + range.Cells[row, column].Value2);
                        exception = true;
                        exceptionOverall = true;
                        break;
                    }
                }
                if (exception)
                {
                    continue;
                }
                else
                {
                    pods.Add(pod);
                    logger.Log("Додано товар категорії " + pod.Category + ", Name - " + pod.Name + " | ProducerName - " + pod.ProducerName + " | ImageUrl - " + pod.ImageUrl + " | Price - " + pod.Price + " | Weight - " + pod.Weight + " | Material - " + pod.Material + " | Battarey - " + pod.Battarey + " | CartrigeCapacity - " + pod.CartrigeCapacity + " | EvaporatorResistance - " + pod.EvaporatorResistance + " | Power - " + pod.Power + " | Port - " + pod.Port);
                }

            }
            if (exceptionOverall)
            {
                MsgBoxError();
            }
            workbook.Close(false);
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
        }
        #endregion
        #region CartrigesAndVaporizers
        private void LoadExcelDataToCartrigesAndVaporizers(string filePath)
        {
            exception = false;
            Application excel = new Application();
            Workbook workbook = excel.Workbooks.Open(filePath);
            Worksheet worksheet = workbook.Sheets[1];
            Range range = worksheet.UsedRange;

            CreateTable(range);

            for (int row = 2; row <= range.Rows.Count; row++)
            {
                exception = false;
                CartrigeAndVaporizerCE cartrige = new CartrigeAndVaporizerCE();
                for (int column = 1; column <= range.Columns.Count; column++)
                {
                    try
                    {
                        switch (column)
                        {
                            case 1:
                                cartrige.Name = range.Cells[row, column].Value2;
                                break;
                            case 2:
                                cartrige.ProducerName = range.Cells[row, column].Value2;
                                break;
                            case 3:
                                cartrige.ImageUrl = imgPath + range.Cells[row, column].Value2;
                                break;
                            case 4:
                                cartrige.Price = Convert.ToInt16(range.Cells[row, column].Value2);
                                break;
                            case 5:
                                cartrige.CartrigeCapacity = Convert.ToDouble(range.Cells[row, column].Value2);
                                break;
                            case 6:
                                cartrige.SpiralType = range.Cells[row, column].Value2;
                                break;
                            case 7:
                                cartrige.IsVaporizer = Convert.ToBoolean(range.Cells[row, column].Value2);
                                break;
                            case 8:
                                cartrige.Resistance = Convert.ToDouble(range.Cells[row, column].Value2);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Log("ERROR! " + ex.Message + " Object - " + cartrige.Name + ", Category - " + cartrige.Category + ".");
                        exception = true;
                        exceptionOverall = true;
                        break;
                    }
                }
                if (exception)
                {
                    continue;
                }
                else
                {
                    switch (cartrige.IsVaporizer)
                    {
                        case true:
                            cartriges.Add(cartrige);
                            logger.Log("Додано товар категорії " + cartrige.Category + ", Name - " + cartrige.Name + " | ProducerName - " + cartrige.ProducerName + " | ImageUrl - " + cartrige.ImageUrl + " | Price - " + cartrige.Price + " | SpiralType - " + cartrige.SpiralType + " | IsVaporizer - " + cartrige.IsVaporizer + " | Resistance - " + cartrige.Resistance);
                            break;
                        case false:
                            cartriges.Add(cartrige);
                            logger.Log("Додано товар категорії " + cartrige.Category + ", Name - " + cartrige.Name + " | ProducerName - " + cartrige.ProducerName + " | ImageUrl - " + cartrige.ImageUrl + " | Price - " + cartrige.Price + " | CartrigeCapacity - " + cartrige.CartrigeCapacity + " | IsVaporizer - " + cartrige.IsVaporizer + " | Resistance - " + cartrige.Resistance);
                            break;
                    }
                }

            }
            if (exceptionOverall)
            {
                MsgBoxError();
            }
            workbook.Close(false);
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
        }
        #endregion
        #region Buttons logic
        private void ExcelReader_Load(object sender, EventArgs e)
        {
            btnLoadExcel.Enabled = false;
            btnSendToApi.Enabled = false;
            btnCheckImages.Enabled = false;
        }

        private void comboBoxDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxDataType.SelectedIndex == -1)
            {
                btnLoadExcel.Enabled = false;
                btnSendToApi.Enabled = false;
            }
            else
            {
                btnLoadExcel.Enabled = true;
            }
        }

        private void btnOpenLogFile_MouseClick(object sender, MouseEventArgs e)
        {
            if (File.Exists(logger.Path()))
            {
                Notepad();
            }
            else
            {
                logger.Log("Created log file.");
                Notepad();
            }
        }

        #endregion
        #region Methods
        public void CreateTable(Range range)
        {
            DataTable dataTable = new DataTable();

            for (int i = 1; i <= range.Columns.Count; i++)
            {
                DataColumn column = new DataColumn(range.Cells[1, i].Value2.ToString());
                dataTable.Columns.Add(column);
            }

            for (int row = 2; row <= range.Rows.Count; row++)
            {
                DataRow dataRow = dataTable.NewRow();
                for (int column = 1; column <= range.Columns.Count; column++)
                {
                    dataRow[column - 1] = range.Cells[row, column].Value2;
                }
                dataTable.Rows.Add(dataRow);
            }

            dataGridViewExcel.DataSource = dataTable;
        }
        public void MsgBoxError()
        {
            DialogResult result = MessageBox.Show(
                    "There was an error during adding object. Check the log file. Do you want to open it immediately?",
                    "Error!",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
            {
                Notepad();
            }
        }

        public void ImgCheck(string imgUrl, string name)
        {
                try
                {
                    FileStream stream = File.OpenRead(imgUrl);
                }
                catch (Exception ex)
                {
                    logger.Log("ERROR while checking images! " + "ObjectName - " + name + " ImageURL - " + imgUrl);
                    isSucced = false;
                }

        }

        public void Notepad()
        {
            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo("Notepad.Exe", logger.Path());
            p.StartInfo = psi;
            p.Start();
        }

        public void ImageCheckLog(bool isSucced)
        {
            if (isSucced)
            {
                logger.Log("All images is checked succefully! There were no errors.");
                DialogResult result = MessageBox.Show(
                        "All images is checked succefully! There were no errors. ",
                        "Info",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);
            }
            else
            {
                DialogResult result = MessageBox.Show(
                                    "There was an error during checking images. " + "Check the log file. Do you want to open it immediately?",
                                    "Error!",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Error,
                                    MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    Notepad();
                }
            }
        }

        public void ClearLists()
        {
            liquids.Clear();
            cartriges.Clear();
            eCigarettes.Clear();
            pods.Clear();
            coals.Clear();
            tobacco.Clear();
        }

        public void SetImgPath(string catrgory)
        {
            if (imgPath == Environment.GetFolderPath(Environment.SpecialFolder.Desktop))
            {
                imgPath = imgPath + $"\\{catrgory}\\";
                txtBoxImgPath.Text = imgPath;
            }
            else
            {
                txtBoxImgPath.Text = imgPath;
            }
        }

        #endregion
    }
}