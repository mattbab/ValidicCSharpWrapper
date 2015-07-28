namespace Validic.Core.Model
{
    public class ValidicResult<T>
    {
        public Summary Summary { get; set; }
        public T Object { get; set; }
    }
}