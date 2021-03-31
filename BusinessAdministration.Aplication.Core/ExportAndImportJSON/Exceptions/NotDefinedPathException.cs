using BusinessAdministration.Aplication.Core.Base.Exceptions;

namespace BusinessAdministration.Aplication.Core.ExportAndImportJSON.Exceptions
{
    public class NotDefinedPathException : SypException
    {
        public NotDefinedPathException() { }
        public NotDefinedPathException(string message) : base(message) { }
    }
}
