Imports OnTime.Extensions.SDK
Imports System.AddIn
Imports System.Threading
Imports System.Windows.Forms


<AddIn("OnTime SDK Example 3", Description:="This is an example extension for the OnTime SDK.", Publisher:="Vesigo Studios", Version:="1.0.0.0")>
Public Class OnTimeSDKExample
    Inherits TrackingView

    Public Overrides Sub Initialize()

        ' Initialize the tracking extension.
        InitializeTrackingExtension("Example", "This is an example extension for the OnTime SDK.")

        ' Initialize a ribbon button and add it to the ribbon bar.
        Buttons.Add(New RibbonButton("Export to CSV", "This is an example extension for the OnTime SDK. This extension will export all orders that have a weight value between 0 and 50.", My.Resources.Example_3_Large_Image, My.Resources.Example_3_Small_Image, AddressOf ExportOrders))

        ' Initialize a context menu button and add it to the context menu.
        ContextMenuButton = New ContextMenuButton("Export to CSV...", My.Resources.Example_3_Small_Image, True, AddressOf ExportOrders)
    End Sub

    Private Sub ExportOrders()
        ' Display message box to indicate orders will be exported
        If MessageBox.Show("You are about to export all orders with a weight between 0 and 50 to a CSV file. Do you wish to continue?", "OnTime SDK Example 3", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            ' Run synchronous
            ' ======================================
            ShowDialog()

            ' Run asynchronous on background thread
            ' ======================================
            ' ShowDialogAsync()
        End If
    End Sub

    Private Sub ShowDialog()
        ' Display initial UI to the user and begin processing orders.
        Dim progress As New ExportOrdersForm()
        progress.Extension = Me
        progress.ShowDialog()
        progress.Dispose()
    End Sub

    Private Sub ShowDialogAsync()
        Dim thread = New Thread(AddressOf ShowDialog)
        thread.SetApartmentState(ApartmentState.STA)
        thread.IsBackground = True
        thread.Start()
    End Sub
End Class