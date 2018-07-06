using OnTime.Extensions.SDK;
using System.AddIn;
using System.Threading;
using System.Windows.Forms;

namespace OnTime_SDK_Example_3_CSharp
{
    [AddIn("OnTime SDK Example 3", Description = "This is an example extension for the OnTime SDK.", Publisher = "Vesigo Studios", Version = "1.0.0.0")]
    public class OnTimeSDKExample : TrackingView
    {
        public override void Initialize()
        {
            // Initialize the tracking extension.
            InitializeTrackingExtension("Example", "This is an example extension for the OnTime SDK.");

            // Initialize a ribbon button and add it to the ribbon bar.
            Buttons.Add(new RibbonButton("Export to CSV", "This is an example extension for the OnTime SDK. This extension will export all orders that have a weight value between 0 and 50.", Properties.Resources.Example_3_Large_Image, Properties.Resources.Example_3_Small_Image, ExportOrders));

            // Initialize a context menu button and add it to the context menu.
            ContextMenuButton = new ContextMenuButton("Export to CSV...", Properties.Resources.Example_3_Small_Image, true, ExportOrders);
        }


        private void ExportOrders()
        {
            // Display message box to indicate orders will be exported.
            if (MessageBox.Show("You are about to export all orders with a weight between 0 and 50 to a CSV file. Do you wish to continue?", "OnTime SDK Example 3", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Run synchronous
                // ======================================
                ShowDialog();

                // Run asynchronous on background thread
                // ======================================
                // ShowDialogAsync();
            }
        }

        private void ShowDialog()
        {
            // Display initial UI to the user and begin processing orders.
            ExportOrdersForm progress = new ExportOrdersForm();
            progress.Extension = this;
            progress.ShowDialog();
            progress.Dispose();
        }

        private void ShowDialogAsync()
        {
            Thread thread = new Thread(ShowDialog);
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();
        }
    }
}
