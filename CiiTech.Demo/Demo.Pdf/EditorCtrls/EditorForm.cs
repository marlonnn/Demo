using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.Pdf.EditorCtrls
{
    public partial class EditorForm : DevComponents.DotNetBar.Office2007Form
    {
        private string currentFile;

        public string CurrentFile
        {
            get { return this.currentFile; }
        }

        private SaveFileDialog SaveFileDialog;
        private OpenFileDialog OpenFileDialog;
        private FontDialog FontDialog;
        private ColorDialog ColorDialog;
        private PrintPreviewDialog PrintPreviewDialog;
        public int CheckPrint { private set; get; }

        public EditorForm()
        {
            InitializeComponent();
            currentFile = "";
            this.Text = "Editor: New Document";

            this.SaveFileDialog = new SaveFileDialog();
            this.OpenFileDialog = new OpenFileDialog();
            this.FontDialog = new FontDialog();
            this.ColorDialog = new ColorDialog();
            PrintPreviewDialog = new PrintPreviewDialog();

            ExportPdfFilePath = System.Environment.CurrentDirectory;

            this.Load += EditorForm_Load;
        }

        private void EditorForm_Load(object sender, EventArgs e)
        {
            PDFCreatorSetting.Instance.PDFCreatorReady += new EventHandler(Instance_PDFCreatorReady);
        }

        private void OpenFile()
        {
            try
            {
                OpenFileDialog.Title = "RTE - Open File";
                OpenFileDialog.DefaultExt = "rtf";
                OpenFileDialog.Filter = "Rich Text Files|*.rtf|Text Files|*.txt|HTML Files|*.htm|All Files|*.*";
                OpenFileDialog.FilterIndex = 1;
                OpenFileDialog.FileName = string.Empty;

                if (OpenFileDialog.ShowDialog() == DialogResult.OK)
                {

                    if (OpenFileDialog.FileName == "")
                    {
                        return;
                    }

                    string strExt = System.IO.Path.GetExtension(OpenFileDialog.FileName);
                    strExt = strExt.ToUpper();

                    if (strExt == ".RTF")
                    {
                        rtbDoc.LoadFile(OpenFileDialog.FileName, RichTextBoxStreamType.RichText);
                    }
                    else
                    {
                        System.IO.StreamReader txtReader;
                        txtReader = new System.IO.StreamReader(OpenFileDialog.FileName);
                        rtbDoc.Text = txtReader.ReadToEnd();
                        txtReader.Close();
                        txtReader = null;
                        rtbDoc.SelectionStart = 0;
                        rtbDoc.SelectionLength = 0;
                    }

                    currentFile = OpenFileDialog.FileName;
                    rtbDoc.Modified = false;
                    this.Text = "Editor: " + currentFile.ToString();
                }
                else
                {
                    MessageBox.Show("Open File request cancelled by user.", "Cancelled");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void rtbDoc_SelectionChanged(object sender, EventArgs e)
        {
            //tbrBold.Checked = rtbDoc.SelectionFont.Bold;
            //tbrItalic.Checked = rtbDoc.SelectionFont.Italic;
            //tbrUnderline.Checked = rtbDoc.SelectionFont.Underline;
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (rtbDoc.Modified == true)
                {
                    System.Windows.Forms.DialogResult answer;
                    answer = MessageBox.Show("Save current document before creating new document?", "Unsaved Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == System.Windows.Forms.DialogResult.No)
                    {
                        currentFile = "";
                        this.Text = "Editor: New Document";
                        rtbDoc.Modified = false;
                        rtbDoc.Clear();
                        return;
                    }
                    else
                    {
                        tsbSave_Click(this, new EventArgs());
                        rtbDoc.Modified = false;
                        rtbDoc.Clear();
                        currentFile = "";
                        this.Text = "Editor: New Document";
                        return;
                    }
                }
                else
                {
                    currentFile = "";
                    this.Text = "Editor: New Document";
                    rtbDoc.Modified = false;
                    rtbDoc.Clear();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void tsbOpen_Click(object sender, EventArgs e)
        {
            try
            {
                if (rtbDoc.Modified == true)
                {
                    System.Windows.Forms.DialogResult answer;
                    answer = MessageBox.Show("Save current file before opening another document?", "Unsaved Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == System.Windows.Forms.DialogResult.No)
                    {
                        rtbDoc.Modified = false;
                        OpenFile();
                    }
                    else
                    {
                        tsbSave_Click(this, new EventArgs());
                        OpenFile();
                    }
                }
                else
                {
                    OpenFile();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentFile == string.Empty)
                {
                    tsbSaveAs_Click(this, e);
                    return;
                }

                try
                {
                    string strExt;
                    strExt = System.IO.Path.GetExtension(currentFile);
                    strExt = strExt.ToUpper();
                    if (strExt == ".RTF")
                    {
                        rtbDoc.SaveFile(currentFile);
                    }
                    else
                    {
                        System.IO.StreamWriter txtWriter;
                        txtWriter = new System.IO.StreamWriter(currentFile);
                        txtWriter.Write(rtbDoc.Text);
                        txtWriter.Close();
                        txtWriter = null;
                        rtbDoc.SelectionStart = 0;
                        rtbDoc.SelectionLength = 0;
                    }

                    this.Text = "Editor: " + currentFile.ToString();
                    rtbDoc.Modified = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "File Save Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void tsbSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog.Title = "RTE - Save File";
                SaveFileDialog.DefaultExt = "rtf";
                SaveFileDialog.Filter = "Rich Text Files|*.rtf|Text Files|*.txt|HTML Files|*.htm|All Files|*.*";
                SaveFileDialog.FilterIndex = 1;

                if (SaveFileDialog.ShowDialog() == DialogResult.OK)
                {

                    if (SaveFileDialog.FileName == "")
                    {
                        return;
                    }

                    string strExt;
                    strExt = System.IO.Path.GetExtension(SaveFileDialog.FileName);
                    strExt = strExt.ToUpper();

                    if (strExt == ".RTF")
                    {
                        rtbDoc.SaveFile(SaveFileDialog.FileName, RichTextBoxStreamType.RichText);
                    }
                    else
                    {
                        System.IO.StreamWriter txtWriter;
                        txtWriter = new System.IO.StreamWriter(SaveFileDialog.FileName);
                        txtWriter.Write(rtbDoc.Text);
                        txtWriter.Close();
                        txtWriter = null;
                        rtbDoc.SelectionStart = 0;
                        rtbDoc.SelectionLength = 0;
                    }

                    currentFile = SaveFileDialog.FileName;
                    rtbDoc.Modified = false;
                    this.Text = "Editor: " + currentFile.ToString();
                    MessageBox.Show(currentFile.ToString() + " saved.", "File Save");
                }
                else
                {
                    MessageBox.Show("Save File request cancelled by user.", "Cancelled");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void tsbBold_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(rtbDoc.SelectionFont == null))
                {
                    System.Drawing.Font currentFont = rtbDoc.SelectionFont;
                    System.Drawing.FontStyle newFontStyle;

                    newFontStyle = rtbDoc.SelectionFont.Style ^ FontStyle.Bold;

                    rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void tsbItalic_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(rtbDoc.SelectionFont == null))
                {
                    System.Drawing.Font currentFont = rtbDoc.SelectionFont;
                    System.Drawing.FontStyle newFontStyle;

                    newFontStyle = rtbDoc.SelectionFont.Style ^ FontStyle.Italic;

                    rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void tsbUnderLine_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(rtbDoc.SelectionFont == null))
                {
                    System.Drawing.Font currentFont = rtbDoc.SelectionFont;
                    System.Drawing.FontStyle newFontStyle;

                    newFontStyle = rtbDoc.SelectionFont.Style ^ FontStyle.Underline;

                    rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void tsbFont_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(rtbDoc.SelectionFont == null))
                {
                    FontDialog.Font = rtbDoc.SelectionFont;
                }
                else
                {
                    FontDialog.Font = null;
                }
                FontDialog.ShowApply = true;
                if (FontDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    rtbDoc.SelectionFont = FontDialog.Font;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void tsbColor_Click(object sender, EventArgs e)
        {
            try
            {
                ColorDialog.Color = rtbDoc.ForeColor;
                if (ColorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    rtbDoc.SelectionColor = ColorDialog.Color;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void tsbLeft_Click(object sender, EventArgs e)
        {
            rtbDoc.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void tsbCenter_Click(object sender, EventArgs e)
        {
            rtbDoc.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void tsbRight_Click(object sender, EventArgs e)
        {
            rtbDoc.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {

        }

        private void tsbPrintPreview_Click(object sender, EventArgs e)
        {
            try
            {
                PrintPreviewDialog.Document = printDocument;
                PrintPreviewDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private string _exportPdfFilePath;
        /// <summary>
        /// gets or sets default export pdf file path
        public string ExportPdfFilePath
        {
            get
            {
                return this._exportPdfFilePath;
            }
            set
            {
                if (_exportPdfFilePath == value) return;
                _exportPdfFilePath = value;
            }
        }

        private void tsbPDF_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = ExportPdfFilePath;
            sfd.Filter = "PDF file | *.pdf";
            sfd.FileName = "New Document" + ".pdf";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string fileName = System.IO.Path.GetFileName(sfd.FileName);
                string filePath = sfd.FileName.Substring(0, sfd.FileName.IndexOf(fileName));
                string basePath = filePath + fileName;
                if (fileName.Substring(fileName.Length - 4, 4) == ".pdf")
                {
                    fileName = fileName.Substring(0, fileName.Length - 4);
                }

                int aotostart = 1;
                if (WordsIScn(fileName) || WordsIScn(filePath))
                {
                    filePath = Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 3) + "temp\\";
                    fileName = "temp";
                    aotostart = 0;
                }

                if (PrintPDF(fileName, filePath, basePath, aotostart))
                {
                    ExportPdfFilePath = Path.GetDirectoryName(sfd.FileName);
                }
            }
            sfd.Dispose();
        }

        private int _lastPrintPage = 1;
        private int _pagesIndex = 0;
        private bool PrintPDF(string fileName, string filePath, string basepath, int aotostart)
        {
            _pagesIndex = 0;
            PrintDocument printDocument = new PrintDocument();
            printDocument.DefaultPageSettings.Landscape = true;
            try { printDocument.DefaultPageSettings.PaperSize.RawKind = (int)PaperKind.A4; }
            catch { }   // will throw exception when no default printer is set
            printDocument.PrintController = new StandardPrintController();
            printDocument.DocumentName = "Cii-tech Report";// "ACEA Report";
            printDocument.PrintPage += new PrintPageEventHandler(_printDocument_PrintPage);
            if (PDFCreatorSetting.Instance.SetCreatorOptions(filePath, fileName, 1, aotostart))
            {
                printDocument.PrinterSettings.PrinterName = "PDFCreator";
            }
            else
            {
                //MessageBox.Show(Res.Report.PrintSettings_Print_Printer_does_not_exist__, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // if not auto start, store the file path, will start the file in function MoveAndOpenPdfFile
            _fileToMove = aotostart == 1 ? null : new string[] { filePath + fileName + ".pdf", basepath };

            PDFCreatorSetting.Instance.CloseAfterReady = true;
            printDocument.Print();
            printDocument.Dispose();
            return true;
        }

        // this filed is used to store pdf print file path for moving and opening
        private string[] _fileToMove;

        private void Instance_PDFCreatorReady(object sender, EventArgs e)
        {
            if (_fileToMove != null)
            {
                var tmp = _fileToMove;
                _fileToMove = null;
                MoveAndOpenPdfFile(tmp[0], tmp[1], true);
            }
        }

        public static void MoveAndOpenPdfFile(string oldPath, string newPath, bool openFile)
        {
            if (string.IsNullOrEmpty(oldPath) || string.IsNullOrEmpty(newPath)) return;

            try
            {
                if (File.Exists(newPath))
                    File.Delete(newPath);

                File.Move(oldPath, newPath);
                File.Delete(oldPath);
                if (openFile)
                    Process.Start(newPath, "");
            }
            catch
            {
                return;
            }
        }

        public void _printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            PrintPage(e);
        }

        private void PrintPage(PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            if (_pagesIndex > _lastPrintPage) return;

            //if (_pagesIndex % 27 == 0)
            //{ //if pages index is too large, refresh report
            //    reportLayout.AutoScrollPosition = new Point(0, -(_pagesIndex * 1169));
            //    AdjustPagePosition();
            //    ReportOperateManage.SetSeletectedPage();
            //}

            PrintReport(g);

            if (_pagesIndex == _lastPrintPage)
            {
                e.HasMorePages = false;
                _pagesIndex = 0;
            }
            else
            {
                e.HasMorePages = true;
                _pagesIndex++;
            }
        }

        private void PrintReport(Graphics g)
        {
            this.Invoke(new Action(() =>
            {
                rtbDoc.Draw(g, rtbDoc.Bounds);
                //reportPageUI.DrawPageHeadAndTail(g);

                //for (int i = reportPageUI.Controls.Count - 1; i >= 0; i--)
                //{
                //    ReportCtrl rCtrl = reportPageUI.Controls[i] as ReportCtrl;
                //    if (rCtrl != null)
                //    {
                //        rCtrl.IsPrint = true;
                //        rCtrl.Draw(g, rCtrl.ReportItem.Bounds);
                //        rCtrl.IsPrint = false;
                //    }
                //}
            }));
        }

        public bool WordsIScn(string words)
        {
            string TmmP;
            for (int i = 0; i < words.Length; i++)
            {
                TmmP = words.Substring(i, 1);
                byte[] sarr = System.Text.Encoding.GetEncoding("gb2312").GetBytes(TmmP);
                if (sarr.Length == 2)
                {
                    return true;
                }
            }
            return false;
        }

        private void tsbPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog.Title = "RTE - Insert Image File";
            OpenFileDialog.DefaultExt = "rtf";
            OpenFileDialog.Filter = "Png Files|*.png|Bitmap Files|*.bmp|JPEG Files|*.jpg|GIF Files|*.gif|All Files|*.*";
            OpenFileDialog.FilterIndex = 1;
            OpenFileDialog.ShowDialog();

            if (OpenFileDialog.FileName == "")
            {
                return;
            }

            try
            {
                string strImagePath = OpenFileDialog.FileName;
                Image img;
                img = Image.FromFile(strImagePath);
                Clipboard.SetDataObject(img);
                DataFormats.Format df;
                df = DataFormats.GetFormat(DataFormats.Bitmap);
                if (this.rtbDoc.CanPaste(df))
                {
                    this.rtbDoc.Paste(df);
                }
            }
            catch
            {
                MessageBox.Show("Unable to insert image format selected.", "RTE - Paste", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            CheckPrint = 0;
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            CheckPrint = rtbDoc.Print(CheckPrint, rtbDoc.TextLength, e);

            if (CheckPrint < rtbDoc.TextLength)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }
        }
    }
}
