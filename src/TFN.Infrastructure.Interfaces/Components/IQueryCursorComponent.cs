namespace TFN.Infrastructure.Interfaces.Components
{
    public interface IQueryCursorComponent
    {
        bool HasCursor();
        string GetCursor();
        void SetCursor(string cursor);       
    }
}