using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhangHenry.Lab2
{
    public enum Position
    {
        Left,
        Right,
        Top,
        Bottom,
    };

    public class MazeCell
    {
        //Position of the cell:
        public int row;
        public int column;

        //State of the cell
        private int[] wallState;
        
        public MazeCell(int row, int column)
        {
            wallState = new int[]
            {
                0, 0, 0, 0
            };
            this.row = row;
            this.column = column;
        }

        public Boolean HasLeftWalls()
        {
            return wallState[(int)Position.Left] == 0;
        }

        public Boolean HasRightWalls()
        {
            return wallState[(int)Position.Right] == 0;
        }

        public Boolean HasDownWalls()
        {
            return wallState[(int)Position.Bottom] == 0;
        }

        public Boolean HasUpWalls()
        {
            return wallState[(int)Position.Top] == 0;
        }

        public void RemoveUpWall()
        {
            wallState[(int)Position.Top] = 1;
        }

        public void RemoveDownWall()
        {
            wallState[(int)Position.Bottom] = 1;
        }
        public void RemoveLeftWall()
        {
            wallState[(int)Position.Left] = 1;
        }

        public void RemoveRightWall()
        {
            wallState[(int)Position.Right] = 1;
        }


    }
}
