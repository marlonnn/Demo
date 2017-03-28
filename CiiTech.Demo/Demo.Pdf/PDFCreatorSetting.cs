using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.Pdf
{
    public sealed class PDFCreatorSetting
    {
        private static PDFCreatorSetting _instance = new PDFCreatorSetting();
        public static PDFCreatorSetting Instance
        {
            get
            {
                return _instance;
            }
        }

        public bool Initialized
        {
            get
            {
                try
                {
                    return _pdfCreator != null && _pdfCreator.cProgramIsRunning;
                }
                catch (Exception)   // fix bug 5527
                {
                    return false;
                }
            }
        }


        /// <summary>
        /// if close pdf creator after print completed
        /// </summary>
        public bool CloseAfterReady
        {
            get;
            set;
        }

        private bool _toClose;

        // timer used for close pdf creator after print completed
        private System.Windows.Forms.Timer _timer;

        private PDFCreatorSetting()
        {
            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 500;
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Enabled = true;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (_toClose)
            {
                _toClose = false;
                ClosePDFCreator();
            }
        }

        /// <summary>
        /// close pdf creator after done pdf export
        /// </summary>
        public void ClosePDFCreator()
        {
            _pErr = null;
            try
            {
                if (_pdfCreator != null)
                {
                    _pdfCreator.cClose();
                    int i = 0;
                    while (_pdfCreator.cProgramIsRunning && i++ < 20)
                    {
                        System.Windows.Forms.Application.DoEvents();
                        System.Threading.Thread.Sleep(100);
                    }
                }
            }
            catch { }

            if (_pdfCreator != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(_pdfCreator);                
                _pdfCreator = null;
            }
        }

        private PDFCreator.clsPDFCreator _pdfCreator;
        private PDFCreator.clsPDFCreatorError _pErr;

        /// <summary>
        /// new pdf creator and start it
        /// </summary>
        /// <param name="closeWhenExist"></param>
        private bool StartPDFCreator(bool restartWhenExist)
        {
            if (!PrinterExists()) return false;

            if (Initialized) return true;

            try
            {
                _pErr = new PDFCreator.clsPDFCreatorError();
                if (_pdfCreator != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(_pdfCreator);
                _pdfCreator = new PDFCreator.clsPDFCreator();
                _pdfCreator.eError += new PDFCreator.__clsPDFCreator_eErrorEventHandler(_PDFCreator_eError);
                _pdfCreator.eReady += new PDFCreator.__clsPDFCreator_eReadyEventHandler(_PDFCreator_eReady);
                bool startSuccessful = _pdfCreator.cStart("/NoProcessingAtStartup", false);

                if (restartWhenExist && !startSuccessful && _pErr.Number == 2)   // if already running by manual
                {
                    ClosePDFCreator();
                    StartPDFCreator(false);
                }
            }
            catch
            {
                return false;
            }

            return Initialized;
        }

        public bool StartPDFCreator()
        {
            return StartPDFCreator(true);
        }

        private void _PDFCreator_eReady()
        {
            OnPDFCreatorReady();

            if (CloseAfterReady)
            {
                _toClose = true;
            }
        }

        /// <summary>
        /// set pdf creator options
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <param name="isAutoSave">0--false ,not 0 ---true</param>
        public bool SetCreatorOptions(string filePath, string fileName, int isAutoSave,int isAutoStart)
        {
            StartPDFCreator();
            if (!Initialized) return false;

            var opt = _pdfCreator.cOptions;
            opt.UseAutosave = isAutoSave;
            opt.UseAutosaveDirectory = isAutoSave;
            opt.AutosaveDirectory = filePath;
            opt.AutosaveFormat = 0;
            opt.AutosaveFilename = fileName;
            opt.AutosaveStartStandardProgram = isAutoStart;
            opt.PDFCompressionColorCompression = 1; // compress color image
            opt.PDFCompressionColorCompressionChoice = 0;   // compress quality auto
            _pdfCreator.cOptions = opt;
            _pdfCreator.cClearCache();
            _pdfCreator.cPrinterStop = false;   // in case it is stopped

            return true;
        }

        private bool PrinterExists()
        {
            System.Drawing.Printing.PrinterSettings.StringCollection snames = System.Drawing.Printing.PrinterSettings.InstalledPrinters;
            foreach (string s in snames)
            {
                if (s.ToLower().Trim() == "pdfcreator")
                {
                    return true;
                }
            }
            return false;
        }

        private void _PDFCreator_eError()
        {
            _pErr = _pdfCreator.cError;
        }

        public event EventHandler PDFCreatorReady;

        private void OnPDFCreatorReady()
        {
            if (PDFCreatorReady != null)
            {
                PDFCreatorReady(this, new EventArgs());
            }
        }

    }
}
