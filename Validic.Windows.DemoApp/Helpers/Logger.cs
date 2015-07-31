//using Validic.Mobile.Core.Utils;

using Validic.Logging;

namespace Validic.Windows.DemoApp.Helpers
{
    public class Logger : ILog
    {
        private readonly string _tag;

        public Logger(string tag)
        {
            _tag = tag;
        }

        public void Debug(string message)
        {
            System.Diagnostics.Debug.WriteLine("Debug : Tag={0}, Message={1}", _tag, message);
        }

        public void Error(string message)
        {
            System.Diagnostics.Debug.WriteLine("Error : Tag={0}, Message={1}", _tag, message);
        }

        public void Verbose(string message)
        {
            System.Diagnostics.Debug.WriteLine("Verbose : Tag={0}, Message={1}", _tag, message);
        }

        public void Warn(string message)
        {
            System.Diagnostics.Debug.WriteLine("Warn : Tag={0}, Message={1}", _tag, message);
        }

        public void Debug(string format, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine("Debug : Tag={0}, Message={1}", _tag, string.Format(format, args));
        }

        public void Error(string format, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine("Error : Tag={0}, Message={1}", _tag, string.Format(format, args));
        }

        public void Verbose(string format, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine("Verbose : Tag={0}, Message={1}", _tag, string.Format(format, args));
        }

        public void Warn(string format, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine("Warn : Tag={0}, Message={1}", _tag, string.Format(format, args));
        }
    }
}