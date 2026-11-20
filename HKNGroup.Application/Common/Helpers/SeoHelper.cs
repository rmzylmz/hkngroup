using System.Text.RegularExpressions;

namespace HKNGroup.Application.Common.Helpers
{
    public static class SeoHelper
    {
        public static string ToSeoUrl(string? incomingText)
        {
            if (string.IsNullOrWhiteSpace(incomingText))
                return string.Empty;

            incomingText = incomingText.Trim();

            // Replace Turkish specific characters
            incomingText = incomingText.Replace("ş", "s")
                                       .Replace("Ş", "s")
                                       .Replace("İ", "i")
                                       .Replace("I", "i")
                                       .Replace("ı", "i")
                                       .Replace("ö", "o")
                                       .Replace("Ö", "o")
                                       .Replace("ü", "u")
                                       .Replace("Ü", "u")
                                       .Replace("Ç", "c")
                                       .Replace("ç", "c")
                                       .Replace("ğ", "g")
                                       .Replace("Ğ", "g");

            incomingText = incomingText.ToLowerInvariant();

            // Handle special symbols
            incomingText = incomingText.Replace("#", "-sharp");
            incomingText = incomingText.Replace("&", "-and-");

            // Replace all non-alphanumeric characters with a dash
            string encodedUrl = Regex.Replace(incomingText, @"[^a-z0-9]", "-");

            // Replace multiple consecutive dashes with a single dash
            encodedUrl = Regex.Replace(encodedUrl, @"-+", "-");

            // Trim leading and trailing dashes
            return encodedUrl.Trim('-');
        }
    }
}

