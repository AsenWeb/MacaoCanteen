using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace TicketPrint
{
    public class PrintDraw
    {
        private Graphics Gp_Content;


        public PrintDraw(int PaperWidht, int PaperHeight)
        {


        }

        public void SetGraphics(Graphics Gp)
        {
            Gp = Gp_Content;
        }

        public void DrawBody(string Content,Font Font=null,StringFormat StrFormat=null)
        {
         

        }

        public void DrawLine()
        {

        }

    }
}
