namespace adminFlowerShop_Gr1.Helpper
{
    public class Utilities
    {
        //internal static string SEOUrl(string productName)
        //{
        //throw new NotImplementedException();
        //}

        internal static string SEOUrl(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            // Loại bỏ các ký tự không hợp lệ trong URL
            string cleanedInput = RemoveInvalidUrlCharacters(input);

            // Thay thế khoảng trắng bằng dấu gạch ngang
            string seoFriendlyUrl = cleanedInput.Replace(" ", "-");

            return seoFriendlyUrl;
        }

        internal static string RemoveInvalidUrlCharacters(string input)
        {
            // Triển khai logic loại bỏ các ký tự không hợp lệ trong URL ở đây
            // Ví dụ: loại bỏ các ký tự đặc biệt, ký tự tiếng Việt không dấu, v.v.
            // Điều này phụ thuộc vào yêu cầu cụ thể của bạn.

            // Ví dụ đơn giản: loại bỏ các ký tự đặc biệt
            string pattern = "[^a-zA-Z0-9]";
            return System.Text.RegularExpressions.Regex.Replace(input, pattern, "");
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

        internal static Task<string?> UploadFile(object fThumb, string v1, string v2)
        {
            throw new NotImplementedException();
        }
    }
}
