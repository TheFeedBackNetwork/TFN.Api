﻿using System;
using TFN.Infrastructure.Interfaces.Components;

namespace TFN.Infrastructure.Components
{
    public class QueryCursorComponent : IQueryCursorComponent
    {
        private string Cursor { get; set; } 
        public QueryCursorComponent()
        {
            Cursor = null;          
        }
        public string GetCursor()
        {
            return Cursor;
        }

        public bool HasCursor()
        {
            return !String.IsNullOrEmpty(Cursor);
        }

        public void SetCursor(string cursor)
        {
            Cursor = cursor;
        }
    }
}