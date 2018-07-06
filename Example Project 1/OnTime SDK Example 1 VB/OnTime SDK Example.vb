Imports OnTime.Extensions.SDK
Imports System.AddIn
Imports System.Windows.Forms

<AddIn("OnTime SDK Example 1", Description:="This is an example extension for the OnTime SDK.", Publisher:="Vesigo Studios", Version:="1.0.0.0")>
Public Class OnTimeSDKExample
    Inherits IndependentView

    Public Overrides Sub Initialize()
        ' Initialize the independent extension.
        InitializeExtension("Example", "This is the first example extension for the OnTime SDK.")

        ' Add a new button to the ribbon bar.
        Buttons.Add(New RibbonButton("Create Customer", "Create a new customer with the name ""Business, Inc.""", My.Resources.Example_1_Large_Image, My.Resources.Example_1_Small_Image, AddressOf CreateCustomer))
    End Sub

    ' Add message boxes.
    Private Sub CreateCustomer()
        ' Check if customer already exists.
        Dim customer = Data.GetCustomerList.Where(New Filter.Exact(CustomerColumn.Name, "Business, Inc.")).FirstOrDefault
        If customer IsNot Nothing Then
            ' Update primary contact name.
            customer.PrimaryContactName = "John Doe"

            ' Update the customer in the database.
            Data.UpdateCustomer(customer)

            ' Display a message box indicating that there a customer was updated
            MessageBox.Show("A customer with the name ""Business, Inc."" was updated.", "OnTime SDK Example 1", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            ' Create a new customer.
            customer = New Customer()
            customer.Name = "Business, Inc."
            customer.PrimaryContactName = "John Doe"
            customer.AddressLine1 = "100 Business Park Drive"
            customer.City = "Medford"
            customer.State = "OR"
            customer.PostalCode = "97504"
            customer.Country = "United States"
            customer.Phone = "514-555-5555"
            customer.Email = "john@business.com"
            customer.HasWebPortalAccess = True

            ' Add the customer to the database.
            Data.AddCustomer(customer)

            ' Display a message box indicating that there a customer was created
            MessageBox.Show("A customer with the name ""Business, Inc."" was created.", "OnTime SDK Example 1", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
End Class