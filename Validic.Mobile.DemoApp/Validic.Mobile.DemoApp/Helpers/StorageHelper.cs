using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLStorage;

namespace Validic.Mobile.DemoApp.Helpers
{
    public class StorageHelper
    {
        static byte[] GetBytes(string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        static string GetString(byte[] bytes)
        {
            var chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        public static byte[] StringToByteArray(string text)
        {
            return text.ToCharArray().Select(c => (byte)c).ToArray();
        }

        public static string ByteArrayToString(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                sb.Append((char)b);
            }
            return sb.ToString();
        }

        public static async Task<Stream> GetFileStreamAsync(string folderName, string fileName)
        {
            var storage = PCLStorage.FileSystem.Current;
            var folder = await storage.LocalStorage.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
            var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            var stream = await file.OpenAsync(FileAccess.ReadAndWrite);
            return stream;
        }

        public static async Task WriteFileAsync(string folderName, string fileName, string text)
        {
            var stream = await GetFileStreamAsync(folderName, fileName);
            var bytes = StringToByteArray(text);
            await stream.WriteAsync(bytes, 0, bytes.Length);
            await stream.FlushAsync();
            stream.Dispose();
        }
        public static async Task<string> ReadFileAsync(string folderName, string fileName)
        {
            var stream = await GetFileStreamAsync(folderName, fileName);
            var bytes =  new byte[stream.Length];
            await stream.ReadAsync(bytes, 0, bytes.Length);
            stream.Dispose();
            var text = ByteArrayToString(bytes);
            return text;
        }
    }
}
