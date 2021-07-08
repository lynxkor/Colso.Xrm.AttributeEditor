using System;

namespace Colso.Xrm.AttributeEditor.AppCode
{
    public class StatusMessageEventArgs : EventArgs
    {
        public StatusMessageEventArgs(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
}