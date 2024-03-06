using System.Text.Json;

namespace SimpleGIFMaker.DataSource.FileSystem.Utils
{
    internal static class JsonFile
    {
        public static void Save<T>(T dto, string path)
        {
            var str = JsonSerializer.Serialize(dto);
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (var sw = new StreamWriter(fs))
            {
                sw.Write(str);
            }
        }

        public static T? Load<T>(string path)
        {
            if (File.Exists(path) == false)
            {
                return default;
            }

            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (var sr = new StreamReader(fs))
            {
                var str = sr.ReadToEnd();
                return JsonSerializer.Deserialize<T>(str);
            }
        }

        //public static async Task SaveAsync<T>(T dto, string path)
        //{
        //    var str = JsonSerializer.Serialize(dto);
        //    using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
        //    using (var sw = new StreamWriter(fs))
        //    {
        //        await sw.WriteAsync(str);
        //    }
        //}

        //public static async Task<T?> LoadAsync<T>(string path)
        //{
        //    if (File.Exists(path) == false)
        //    {
        //        return default;
        //    }

        //    using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        //    using (var sr = new StreamReader(fs))
        //    {
        //        var str = await sr.ReadToEndAsync();
        //        return JsonSerializer.Deserialize<T>(str);
        //    }
        //}
    }
}
