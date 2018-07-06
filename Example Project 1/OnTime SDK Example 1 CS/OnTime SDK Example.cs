using OnTime.Extensions.SDK;
using System.AddIn;
using System.Linq;
using System.Windows.Forms;

namespace OnTime_SDK_Example_1_CSharp
{
    [AddIn("OnTime SDK Example 1 CSharp", Description = "This is an example extension for the OnTime SDK.", Publisher = "Vesigo Studios", Version = "1.0.0.0")]
    public class OnTimeSDKExample : IndependentView
    {
        public override void Initialize()
        {
            // Initialize the independent extension.
            InitializeExtension("OnTime SDK Example 1 CSharp", "This is an example extension for the OnTime SDK.");

            // Add a new button to the ribbon bar.
            Buttons.Add(new RibbonButton("Create Customer", "Create a new customer.", Properties.Resources.Example_1_Large_Image, Properties.Resources.Example_1_Small_Image, CreateCustomer));
        }

        public void CreateCustomer()
        {
            // Check if customer already exists.
            Customer customer = Data.GetCustomerList.Where(new Filter.Exact(Schema.CustomerColumn.Name, "Business, Inc.")).FirstOrDefault();
            if (customer != null)
            {
                // Update primary contact name.
                customer.PrimaryContactName = "John Doe";

                // Update the customer in the database.
                Data.UpdateCustomer(customer);

                // Display a message box indicating that there a customer was updated
                MessageBox.Show("A customer with the name \"Business, Inc.\" was updated.", "OnTime SDK Example 1", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Create a new customer.
                customer = new Customer();
                customer.Name = "Business, Inc.";
                customer.PrimaryContactName = "John Doe";
                customer.AddressLine1 = "100 Business Park Drive";
                customer.City = "Medford";
                customer.State = "OR";
                customer.PostalCode = "97504";
                customer.Country = "United States";
                customer.Phone = "514-555-5555";
                customer.Email = "john@business.com";
                customer.HasWebPortalAccess = true;

                // Add the customer to the database.
                Data.AddCustomer(customer);

                // Display a message box indicating that there a customer was created
                MessageBox.Show("A customer with the name \"Business, Inc.\" was created.", "OnTime SDK Example 1", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}