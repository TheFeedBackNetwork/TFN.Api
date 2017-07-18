namespace TFN.Infrastructure.Interfaces.Components
{
    public interface IQueryCursorComponent
    {
        bool HasResponseCursor();
        string GetResponseCursor();
        void SetResponseCursor(string cursor);

        bool HasRequestCursor();
        string GetRequestCursor();
        void SetRequestCursor(string cursor);
    }
}