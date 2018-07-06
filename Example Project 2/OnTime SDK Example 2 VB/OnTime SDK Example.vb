Imports OnTime.Extensions.SDK
Imports System.AddIn
Imports System.IO
Imports System.Windows.Forms

<AddIn("OnTime SDK Example 2", Description:="This is an example extension for the OnTime SDK.", Publisher:="Vesigo Studios", Version:="1.0.0.0")>
Public Class OnTimeSDKExample
    Inherits BillingView

    Public Overrides Sub Initialize()
        ' Initialize the invoice extension.
        InitializeInvoiceExtension("Example", "This is an example extension for the OnTime SDK.", "Exported to CSV", True)

        ' Initialize a ribbon button and add it to the invoice ribbon bar.
        InvoiceButtons.Add(New RibbonButton("Export to CSV", "This is an example extension for the OnTime SDK.", My.Resources.Example_2_Large_Image, My.Resources.Example_2_Small_Image, AddressOf ExportInvoices))

        ' Initialize a context menu button and add it to the invoice context menu.
        InvoiceContextMenuButton = New ContextMenuButton("Export to CSV...", My.Resources.Example_2_Small_Image, True, AddressOf ExportInvoices)
    End Sub

    Private Sub ExportInvoices()
        ' Open save file dialog.
        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Filter = "CSV File (*.csv)|*.csv"
        saveFileDialog.Title = "Save a CSV File"
        saveFileDialog.ShowDialog()

        ' If file name is not empty.
        If saveFileDialog.FileName <> "" Then
            Dim streamWriter As New StreamWriter(saveFileDialog.FileName)

            ' Add column headers.
            streamWriter.Write("""Invoice Number"",""Date"",""Due Date"",""Customer Name"",""Total Amount""" & vbCrLf)

            ' Add invoice data.
            For Each invoice In SelectedInvoices
                streamWriter.Write("""" & invoice.InvoiceNumber & """,""" & invoice.Date & """,""" & invoice.DueDate & """,""" & invoice.Customer.Name & """,""" & invoice.TotalAmount.ToString("c") & """" & vbCrLf)
                ' Indicate that the invoice was exported by the extension.
                Data.MarkInvoiceTransferred(invoice.ID, True)
            Next

            MessageBox.Show("Successfully exported " & SelectedInvoices.Count.ToString("N0") & " invoices to """ & saveFileDialog.FileName & """.")

            streamWriter.Close()
            streamWriter.Dispose()
        End If

        saveFileDialog.Dispose()
    End Sub
End Class