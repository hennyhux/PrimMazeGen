using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ZhangHenry.Lab2
{
    public class Maze
    {
        //Internal representation of the maze, the grids are represented with a 2D array of Cell- 
        //which indicates how many walls the cell has 
        private MazeCell[,] newMaze;

        private int mazeWidth;
        private int mazeHeight;


        public Maze(int width, int height)
        {

            this.mazeWidth = width;
            this.mazeHeight = height;
            newMaze = new MazeCell[height, width];
            for (int column = 0; column < width; ++column)
            {
                for (int row = 0; row < height; ++row)
                {
                    newMaze[row, column] = new MazeCell(row, column);
                }
            }
        }


        public MazeCell GetMazeCell(int row, int column)
        {
            return newMaze[row, column];
        }

        public Boolean inCell(int row, int column)
        {
            return (column < mazeWidth && column >= 0) && (row < mazeHeight && row >= 0);
        }

    }
}
