using System;
using System.Windows.Forms;

namespace BangOnTekCommon
{
    public class WaitCursor : IDisposable
    {
        private readonly Cursor _oldCursor;

        public WaitCursor()
        {
            _oldCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
        }
        public void Dispose()
        {
            Cursor.Current = _oldCursor;
        }
    }
}
