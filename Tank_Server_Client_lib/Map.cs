using System;
using System.Collections.Generic;
using System.Text;

namespace Tank_Server_Client_lib
{
    public class Map
    {
        public int wight { get; set; }
        public int height { get; set; }

        public char[,] map { get; set; }

        public Map(int wight, int height)
        {

            this.wight = wight;
            this.height = height;
        }


        public void DefaultMapCreate()
        {
            this.map = new char[this.wight,this.height];
            int rows = this.map.GetUpperBound(0) + 1;    // количество строк
            int columns = this.map.Length / rows;        // количество столбцов
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    this.map[i, j] = ' ';
                }

            }

            for (int i = 0; i < this.wight; i++)
            {
                this.map[0,i] = 'X';
            }

            for (int i = 0; i < this.wight; i++)
            {
                this.map[this.height-1, i] = 'X';
            }

            for (int i = 0; i < this.height; i++)
            {
                this.map[i,0] = 'X';
            }

            for (int i = 0; i < this.height; i++)
            {
                this.map[i, this.wight-1] = 'X';
            }
        }

        


    }
}
