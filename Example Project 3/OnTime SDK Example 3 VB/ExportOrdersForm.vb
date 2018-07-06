Imports OnTime.Extensions.SDK
Imports System.IO
Imports System.Windows.Forms

Public Class ExportOrdersForm
    Private _fileName As String
    Private _counter As Integer = 0
    Private _time As Integer = 0
    Private _orders As OrderCollection

    Public Property Extension As TrackingView

    Private Sub frmProgressBar_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        ' Open save file dialog.
        Dim saveFileDialog As New SaveFileDialog
        saveFileDialog.Filter = "CSV File (*.csv)|*.csv"
        saveFileDialog.Title = "Save a CSV File"
        saveFileDialog.ShowDialog()

        ' If file name is not empty.
        If saveFileDialog.FileName <> "" Then
            _fileName = saveFileDialog.FileName
            BackgroundWorker.RunWorkerAsync()
        End If

        saveFileDialog.Dispose()
    End Sub

    Private Sub BackgroundWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker.DoWork
        ' Query database for all orders with a weight between 0 and 50.
        _orders = Extension.Data.GetOrderList.Where(New Filter.Range(OrderColumn.Weight, 0, 50))
        Dim streamWriter As New StreamWriter(_fileName)

        ' Add column headers.
        streamWriter.Write("""Tracking Number"",""Description"",""Length"",""Width"",""Height"",""Weight"",""Declared Value""" & vbCrLf)

        Dim stopWatch As New Stopwatch
        stopWatch.Start()

        ' Add order data.
        For Each order In _orders
            If BackgroundWorker.CancellationPending Then
                e.Cancel = True
                Exit For
            End If
            streamWriter.Write("""" & order.TrackingNumber & """,""" & order.Description & """,""" & order.Length & """,""" & order.Width & """,""" & order.Height & """,""" & order.Weight & """,""" & order.DeclaredValue & """" & vbCrLf)
            BackgroundWorker.ReportProgress((Convert.ToDouble(_counter) / Convert.ToDouble(_orders.Count)) * 100)
            _counter += 1
            _time += stopWatch.ElapsedMilliseconds
            stopWatch.Restart()
        Next

        streamWriter.Close()
        streamWriter.Dispose()
    End Sub

    Private Sub BackgroundWorker_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker.ProgressChanged
        ProgressBar.Value = e.ProgressPercentage

        If _counter > 0 Then
            Dim timeSpan As TimeSpan = timeSpan.FromMilliseconds((Convert.ToDouble(_time) / Convert.ToDouble(_counter)) * (Convert.ToDouble(_orders.Count) - Convert.ToDouble(_counter)))
            If timeSpan.Minutes > 0 Then
                LoadingLabel.Text = "Exporting order " & _counter & " of " & _orders.Count & " (" & Math.Ceiling(timeSpan.TotalMinutes) & " minutes remaining)"
            Else
                LoadingLabel.Text = "Exporting order " & _counter & " of " & _orders.Count & " (" & timeSpan.Seconds & " seconds remaining)"
            End If
        Else
            LoadingLabel.Text = "Exporting order " & _counter & " of " & _orders.Count & " to " & _fileName
        End If

        Me.Text = "Export to CSV (" & FormatPercent(e.ProgressPercentage / 100, 0) & " complete)"
    End Sub

    Private Sub BackgroundWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker.RunWorkerCompleted
        If e.Cancelled Then
            MessageBox.Show("The operation was cancelled. " & _counter & " orders were exported to """ & _fileName & """.", "OnTime SDK Example 3", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ElseIf e.Error IsNot Nothing Then
            MessageBox.Show("An error occured during the export operation." & vbCrLf & vbCrLf & "Details: " & e.Error.Message, "OnTime SDK Example 3", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            MessageBox.Show("Successfully exported " & _counter & " orders to """ & _fileName & """.", "OnTime SDK Example 3", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        Me.Close()
    End Sub

    Private Sub frmProgressBar_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        BackgroundWorker.CancelAsync()
    End Sub
End Class