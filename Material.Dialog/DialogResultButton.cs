using System;
using System.Collections.Generic;
using System.Text;

namespace Material.Dialog
{
    public class DialogResultButton
    {
        private string m_Result = "None";
        public string Result { get => m_Result; set => m_Result = value; }
         
        private object m_Content = "Action";
        public object Content { get => m_Content; set => m_Content = value; }
    }
}
