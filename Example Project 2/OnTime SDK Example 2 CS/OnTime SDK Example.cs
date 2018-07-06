using OnTime.Extensions.SDK;
using System;
using System.AddIn;
using System.IO;
using System.Windows.Forms;

namespace OnTime_SDK_Example_2_CSharp
{
    [AddIn("OnTime SDK Example 2", Description = "This is an example extension for the OnTime SDK.", Publisher = "Vesigo Studios", Version = "1.0.0.0")]
    public class OnTimeSDKExample : BillingView
    {
        public override void Initialize()
        {
            // Initialize the invoice extension.
            InitializeInvoiceExtension("Example", "This is an example extension for the OnTime SDK.", "Exported to CSV", true);

            // Initialize a ribbon button and add it to the ribbon bar.
            InvoiceButtons.Add(new RibbonButton("Export to CSV", "This is an example extension for the OnTime SDK.", Properties.Resources.Example_2_Large_Image, Properties.Resources.Example_2_Small_Image, ExportInvoices));

            // Initialize a context menu button and add it to the context menu.
            InvoiceContextMenuButton = new ContextMenuButton("Export to CSV...", Properties.Resources.Example_2_Small_Image, true, ExportInvoices);
        }

        public void ExportInvoices()
        {
            // Open save file dialog.
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV File (*.csv)|*.csv";
            saveFileDialog.Title = "Save a CSV file.";
            saveFileDialog.ShowDialog();

            // If file name is not empty.
            if (saveFileDialog.FileName != "")
            {
                StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName);

                // Add column headers.
                streamWriter.Write("\"Invoice Number\",\"Date\",\"Due Date\",\"Customer Name\",\"Total Amount\"" + Environment.NewLine);

                // Add invoice data.
                foreach (Invoice invoice in SelectedInvoices)
                {
                    streamWriter.Write("\"" + invoice.InvoiceNumber + "\",\"" + invoice.Date + "\",\"" + invoice.DueDate + "\",\"" + invoice.Customer.Name + "\",\"" + invoice.TotalAmount.ToString("c") + "\"" + Environment.NewLine);
                    
                    // Indicate that the invoice was exported by the extension.
                    Data.MarkInvoiceTransferred(invoice.ID, true);
                }

                MessageBox.Show("Successfully exported " + SelectedInvoices.Count + " invoices to \"" + saveFileDialog.FileName + "\".");

                streamWriter.Close();
                streamWriter.Dispose();
            }

            saveFileDialog.Dispose();
        }
    }
}
