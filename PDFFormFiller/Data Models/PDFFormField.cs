namespace PDFFormFiller.Models
{
    internal class PDFFormField
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="fieldName">Name of the field as assigned in the template PDF</param>
        /// <param name="fieldValue">Value of the field once the data is stamped to the PDF form</param>
        public PDFFormField(string fieldName, string fieldValue)
        {
            FieldName = fieldName;
            FieldValue = fieldValue;    
        }

        /// <summary>
        /// Name of the particular field
        /// </summary>
        public string? FieldName { get; init; }

        /// <summary>
        /// Value of the particular field
        /// </summary>
        public string? FieldValue { get; init; }
    }
}
