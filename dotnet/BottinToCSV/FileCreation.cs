using System.Text;

namespace BottinToCSV
{
    public class FileCreation
    {
        String newFile = @"C:\Users\lalutalu\Desktop\Produits.csv";
        String separator = ",";
        StringBuilder outputString = new StringBuilder();
        String[] headings = { "handleId", "fieldType", "name", "description", "productImageUrl", "collection", "sku", "ribbon", "price", "surcharge", "visible", "discountMode", "discountValue", "inventory", "weight", "cost", "productOptionName1", "productOptionType1", "productOptionDescription1", "productOptionName2", "productOptionType2", "productOptionDescription2", "productOptionName3", "productOptionType3", "productOptionDescription3", "productOptionName4", "productOptionType4", "productOptionDescription4", "productOptionName5", "productOptionType5", "productOptionDescription5", "productOptionName6", "productOptionType6", "productOptionDescription6", "additionalInfoTitle1", "additionalInfoDescription1", "additionalInfoTitle2", "additionalInfoDescription2", "additionalInfoTitle3", "additionalInfoDescription3", "additionalInfoTitle4", "additionalInfoDescription4", "additionalInfoTitle5", "additionalInfoDescription5", "additionalInfoTitle6", "additionalInfoDescription6", "customTextField1", "customTextCharLimit1", "customTextMandatory1", "customTextField2", "customTextCharLimit2", "customTextMandatory2", "brand" };
        //output.AppendLine(string.Join(separator, headings));
        //        foreach (Student student in students)
        //{
        //        String[] newLine = { student.StudentId.ToString(), student.FirstName, student.LastName, student.Dob };
        //        output.AppendLine(string.Join(separator, newLine));
        //}
        //    try
        //{
        //    File.AppendAllText(file, output.ToString());
        //}
        //catch(Exception ex)
        //{
        //    Console.WriteLine("Data could not be written to the CSV file.");
        //return;
        //}
        //Console.WriteLine("The data has been successfully saved to the CSV file");

    }
}
