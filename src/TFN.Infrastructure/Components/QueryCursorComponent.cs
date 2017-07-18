using System;
using TFN.Infrastructure.Interfaces.Components;

namespace TFN.Infrastructure.Components
{
    public class QueryCursorComponent : IQueryCursorComponent
    {
        private static string ResponseCursor { get; set; }
        private static string RequestCursor { get; set; }
        public QueryCursorComponent()
        {
            ResponseCursor = null;
            RequestCursor = null;
        }
        public string GetResponseCursor()
        {
            return ResponseCursor;
        }

        public bool HasResponseCursor()
        {
            return !String.IsNullOrEmpty(ResponseCursor);
        }

        public void SetResponseCursor(string cursor)
        {
            ResponseCursor = Base64Encode(cursor);
        }

        public string GetRequestCursor()
        {
            return RequestCursor;
        }

        public bool HasRequestCursor()
        {
            return !String.IsNullOrEmpty(RequestCursor);
        }

        public void SetRequestCursor(string cursor)
        {
            RequestCursor = Base64Decode(cursor);
        }

        public static string Base64Encode(string plainText)
        {
            if (string.IsNullOrWhiteSpace(plainText))
            {
                return null;
            }

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            if (String.IsNullOrWhiteSpace(base64EncodedData))
            {
                return null;
            }

            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}