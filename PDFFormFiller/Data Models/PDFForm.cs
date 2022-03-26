using iText.Forms;
using iText.Kernel.Pdf;

namespace PDFFormFiller.Models
{
    public class PDFForm
    {
        #region Private Fields 

        private List<PDFFormField>? m_PDFFormFields;

        #endregion

        #region Public Properties

        /// <summary>
        /// Path where the template form exists. 
        /// NOTE: This is assigned during initialization of this object
        /// </summary>
        public string? FormPath { get; private set; }

        /// <summary>
        /// Path where the output file will be located.
        /// NOTE: This is assigned during initialization of this object
        /// </summary>
        public string? OutputPath { get; private set; }

        /// <summary>
        /// Determines whether or not the output file will contain editable fields or be flattened.
        /// NOTE: This is assigned during initialization of this object
        /// </summary>
        public bool OutputFlattened { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="formPath">Path where the template is located</param>
        /// <param name="outputPath">Path where the output file will be located</param>
        /// <param name="outputFlattened">Determines whether the output file will have flattened fields that cannot be edited</param>
        public PDFForm(string formPath, string outputPath, bool outputFlattened = false)
        {
            FormPath = formPath;
            OutputPath = outputPath;
            OutputFlattened = outputFlattened;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Add a <see cref="PDFFormField"/> to the <see cref="PDFForm"/>'s form field collection. 
        /// </summary>
        /// <param name="formField"></param>
        public void AddFormField(string fieldName, string fieldValue)
        {
            if(m_PDFFormFields is null)
            {
                m_PDFFormFields = new List<PDFFormField>(); 
            }

            m_PDFFormFields.Add(new PDFFormField(fieldName, fieldValue));
        }

        /// <summary>
        /// Add a <see cref="PDFFormField"/> to the <see cref="PDFForm"/> that checks a particular checkbox. 
        /// NOTE: There is no need to pass a value to the checkbox field since iText handles the addition of the
        /// field as a positive indication that the checkbox should be checked. 
        /// </summary>
        /// <param name="fieldName"></param>
        public void AddNewCheckboxChecked(string fieldName)
        {
            if(m_PDFFormFields is null)
            {
                m_PDFFormFields = new List<PDFFormField>(); 
            }

            m_PDFFormFields.Add(new PDFFormField(fieldName, string.Empty)); 
        }

        /// <summary>
        /// Called once all <see cref="PDFFormField"/> are added. This will stamp the data to the form and save the output file to the 
        /// <see cref="OutputPath"/>. 
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void Fill()
        {
            try
            {
                // Establish a reader and writer
                using PdfReader reader = new PdfReader(FormPath);
                using PdfWriter writer = new PdfWriter(OutputPath);
                PdfDocument doc = new PdfDocument(reader, writer); 

                // Establish a form
                var form = PdfAcroForm.GetAcroForm(doc, false);

                // Fill all fields within form
                foreach (PDFFormField field in m_PDFFormFields ?? new List<PDFFormField>())
                {
                    form.GetField(field.FieldName).SetValue(field.FieldValue); 
                }

                // If flattening is enabled, flatten fields to prevent editing
                if(OutputFlattened)
                {
                    form.FlattenFields(); 
                }

                // Close the document
                doc.Close(); 

                // Close the reader and writer
                reader.Close();
                writer.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message); 
            }
        }

        #endregion 
    }
}
