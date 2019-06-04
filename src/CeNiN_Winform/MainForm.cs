using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeNiN_Winform
{
    public class MainForm
    {
        public delegate void ShowImage(byte[] image);
        public static event ShowImage OnImageReconizeed;
        public static void ImageLoad(byte[] image)
        {
            if (OnImageReconizeed != null)
            {
                OnImageReconizeed(image);
            }

        }
        public delegate void ShowMsg(string msg);
        public static event ShowMsg MsgSend;
        public static void MessageSend(string msg)
        {
            if (MsgSend != null)
            {
                MsgSend(msg);
            }

        }
    }
}
