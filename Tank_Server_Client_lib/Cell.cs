using System;
using System.Collections.Generic;
using System.Text;

namespace Tank_Server_Client_lib
{
    public class Cell
    {
        public int wight { get; set; }
        public int height { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public System.Drawing.Rectangle rectangle {get;set;}
        public Cell(int wight,int height,int X,int Y)
        {

            this.wight = wight;
            this.height = height;
            this.X = X;
            this.Y = Y;
            this.rectangle = new System.Drawing.Rectangle(X,Y,wight,height);
        }

    }
}
