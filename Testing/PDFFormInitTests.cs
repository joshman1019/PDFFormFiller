using PDFFormFiller.Models;
using System.Diagnostics;
using Xunit;

namespace Testing
{
    public class PDFFormInitTests
    {
        private const string PDFFORM_NAME = "IVCNOH.pdf"; 
        private string m_MockFormPath = Path.Combine("TestForm", PDFFORM_NAME);
        private string m_MockOutputPath = Path.Combine(Path.GetTempPath(), "IVCNOH.pdf"); 

        public PDFForm CreatePDFFormMock()
        {
            return new PDFForm(m_MockFormPath, m_MockOutputPath); 
        }

        [Fact]
        public void TestMockFormExists()
        {
            Assert.True(File.Exists(m_MockFormPath)); 
        }

        [Fact]
        public void TestPDFFormCreatedProperly()
        {
            // Tests that the form is not null, has properly assigned input and output paths
            var form = CreatePDFFormMock();
            Assert.NotNull(form); 
            Assert.True(form.FormPath == m_MockFormPath);
            Assert.True(form.OutputPath == m_MockOutputPath);
        }

        [Fact]
        public void CreateTestFormAndVerifyExists()
        {
            var form = CreatePDFFormMock();
            form.AddFormField("CountyName", "HENDERSON");
            form.AddNewCheckboxChecked("District");
            form.AddFormField("RespondentName", "Joshua Santiago");
            form.Fill();
            Debug.WriteLine(form.OutputPath); 
        }
    }
}
