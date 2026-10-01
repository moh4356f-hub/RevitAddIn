
namespace RevitAddIn.Forms
{
    public static class Custom
    {

    }

    public class FormResult
    {
        // Public properties
        public object Object { get; set; }
        public List<object> Objects { get; set; }
        public bool Cancelled { get; set; }
        public bool Valid { get; set; }
        public bool Affirmative { get; set; }

        // Constructor (default)
        public FormResult()
        {
            this.Object = null;
            this.Objects = new List<object>();
            this.Cancelled = true;
            this.Valid = false;
            this.Affirmative = false;
        }

        // Constructor (alternative)
        public FormResult(bool isValid)
        {
            this.Object = null;
            this.Objects = new List<object>();
            this.Cancelled = !isValid;
            this.Valid = isValid;
            this.Affirmative = isValid;
        }

        // Method
        public void SetToInvalid()
        {
            this.Cancelled = true;
            this.Valid = false;
            this.Affirmative = false;
        }
    }
}
