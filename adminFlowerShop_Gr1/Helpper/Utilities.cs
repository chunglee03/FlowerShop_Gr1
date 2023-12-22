using System.Text.RegularExpressions;

namespace adminFlowerShop_Gr1.Helpper
{
    public class Utilities
    {
        //internal static string SEOUrl(string productName)
        //{
        //throw new NotImplementedException();
        //}
        public static void CreateIfMissing(string path)
        {
            bool exist = Directory.Exists(path);
            if (!exist)
                Directory.CreateDirectory(path);
        }
        public static string SEOUrl(string url)
        {
            url = url.ToLower();
            url = Regex.Replace(url, @"[áàạảãâấầậẩẫăắằặẳẵ]", "a");
            url = Regex.Replace(url, @"[éèẹẻẽêếềệểễ]", "e");
            url = Regex.Replace(url, @"[óòọỏõôốồộổỗơớờợởỡ]", "o");
            url = Regex.Replace(url, @"[úùụủũưứừựửữ]", "u");
            url = Regex.Replace(url, @"[íìịỉĩ]", "i");
            url = Regex.Replace(url, @"[ýỳỵỷỹ]", "y");
            url = Regex.Replace(url, @"[đ]", "d");

            url = Regex.Replace(url.Trim(), @"[^0-9a-z-\s]", "").Trim();
            url = Regex.Replace(url.Trim(), @"\s+", "-");
            url = Regex.Replace(url, @"\s", "-");
            while (true)
            {
                if (url.IndexOf("--") != -1)
                {
                    url = url.Replace("--", "-");
                }
                else
                {
                    break;
                }
            }
            return url;
        }

        //l static string ToTitlecase(string productName)
        //{
        //throw new NotImplementedException();
        //
        internal static string ToTitlecase(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            // Triển khai logic chuyển đổi về dạng title case ở đây
            // Ví dụ: sử dụng CultureInfo để xử lý đúng theo quy tắc viết hoa
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
        }

        //internal static Task<string?> UploadFile(object fThumb, string v1, string v2)
        //{
        //throw new NotImplementedException();
        //}

        public static async Task<string> UploadFile(Microsoft.AspNetCore.Http.IFormFile file, string sDirectory, string newName)
        {
            try
            {
                if (newName == null) newName = file.FileName;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", sDirectory);
                CreateIfMissing(path);
                string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", sDirectory, newName);
                var supportedTypes = new[] { "jpg", "png", "jpeg", "PNG", "JPG", "JPEG", "gif" };
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt.ToLower()))
                {
                    return null;
                }
                else
                {
                    using (var stream = new FileStream(pathFile, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return newName;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static string getCurrentDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
