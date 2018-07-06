using OnTime.Extensions.SDK;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace OnTime_SDK_Example_3_CSharp
{
    public partial class ExportOrdersForm : Form
    {
        private string _fileName;
        private long _counter = 0;
        private long _time = 0;

        private OrderCollection _orders;
        public TrackingView Extension { get; set; }

        public ExportOrdersForm()
        {
            InitializeComponent();
        }

        private void frmProgressBar_Shown(object sender, EventArgs e)
        {
            // Open save file dialog.
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV File (*.csv)|*.csv";
            saveFileDialog.Title = "Save a CSV File";
            saveFileDialog.ShowDialog();

            // If file name is not empty.
            if (!string.IsNullOrEmpty(saveFileDialog.FileName))
            {
                _fileName = saveFileDialog.FileName;
                BackgroundWorker.RunWorkerAsync();
            }

            saveFileDialog.Dispose();
        }

        private void BackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            // Query database for all orders with a weight between 0 and 50.
            _orders = Extension.Data.GetOrderList.Where(new Filter.Range(Schema.OrderColumn.Weight, 0, 50));
            StreamWriter streamWriter = new StreamWriter(_fileName);

            // Add column headers.
            streamWriter.Write("\"Tracking Number\",\"Description\",\"Length\",\"Width\",\"Height\",\"Weight\",\"Declared Value\"\n");

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            // Add order data.
            foreach (Order order in _orders)
            {
                if (BackgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                streamWriter.Write("\"" + order.TrackingNumber + "\",\"" + order.Description + "\",\"" + order.Length + "\",\"" + order.Width + "\",\"" + order.Height + "\",\"" + order.Weight + "\",\"" + order.DeclaredValue + "\"\n");
                BackgroundWorker.ReportProgress(Convert.ToInt32((Convert.ToDouble(_counter) / Convert.ToDouble(_orders.Count)) * 100));
                _counter += 1;
                _time += stopWatch.ElapsedMilliseconds;
                stopWatch.Restart();
            }

            streamWriter.Close();
            streamWriter.Dispose();
        }

        private void BackgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            ProgressBar.Value = e.ProgressPercentage;

            if (_counter > 0)
            {
                TimeSpan timeSpan = TimeSpan.FromMilliseconds((Convert.ToDouble(_time) / Convert.ToDouble(_counter)) * (Convert.ToDouble(_orders.Count) - Convert.ToDouble(_counter)));
                if (timeSpan.Minutes > 0)
                {
                    LoadingLabel.Text = "Exporting order " + _counter + " of " + _orders.Count + " (" + Math.Ceiling(timeSpan.TotalMinutes) + " minutes remaining)";
                }
                else
                {
                    LoadingLabel.Text = "Exporting order " + _counter + " of " + _orders.Count + " (" + timeSpan.Seconds + " seconds remaining)";
                }
            }
            else
            {
                LoadingLabel.Text = "Exporting order " + _counter + " of " + _orders.Count + " to " + _fileName;
            }

            this.Text = "Export to CSV (" + e.ProgressPercentage.ToString("P0") + " complete)";
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("The operation was cancelled. " + _counter + " orders were exported to \"" + _fileName + "\".", "OnTime SDK Example 3", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Error != null)
            {
                MessageBox.Show("An error occured during the export operation.\n\nDetails: " + e.Error.Message, "OnTime SDK Example 3", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Successfully exported " + _counter + " orders to \"" + _fileName + "\".", "OnTime SDK Example 3", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Close();
        }

        private void frmProgressBar_FormClosing(object sender, FormClosingEventArgs e)
        {
            BackgroundWorker.CancelAsync();
        }
    }
}
