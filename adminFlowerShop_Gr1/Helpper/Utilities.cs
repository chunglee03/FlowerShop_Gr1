using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace adminFlowerShop_Gr1.Helpper
{
    public static class Utilities
    {
        public static bool IsValidEmail(string email)
        {
            if (email.Trim().EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        //internal static string SEOUrl(string productName)
        //{
        public static string GetRandomKey(int length = 5)
        {
            string pattern = @"0123456789zxcvbnmasdfghjklqwertyuiop[]{}:~!#$%^&*()+";
            Random rd = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append(pattern[rd.Next(0, pattern.Length)]);
            }
            return sb.ToString();
        }
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

        public static int _UserID = 0;
        public static string _UserName = String.Empty;
        public static string _Email = string.Empty;
        public static string _Message = string.Empty;
        public static string _MessageEmail = String.Empty;
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }
        public static string MD5Password(string? text)
        {
            string str = MD5Hash(text);
            for (int i = 0; i <= 5; i++)
                str = MD5Hash(str + "_" + str);
            return str;
        }
        public static bool IsLogin()
        {
            if (string.IsNullOrEmpty(Utilities._UserName) || string.IsNullOrEmpty(Utilities._Email) || (Utilities._UserID <= 0))
                return false;
            return true;
        }
    }
}
