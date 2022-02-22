using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Debug = System.Diagnostics.Debug;
using Random = UnityEngine.Random;

namespace ZhangHenry.Lab2
{
    public class MazeGenerator 
    {
        public static MazeGenerator MazeGen = new MazeGenerator();

        private Maze maze;
        private HashSet<MazeCell> passageSet;
        private MazeCell currentCell;

        private HashSet<MazeCell> frontierSet;
        private MazeCell currentFrontierCell;

        private int mazeWidth;
        private int mazeHeight; 

        private MazeGenerator()
        {
            passageSet = new HashSet<MazeCell>();
            frontierSet = new HashSet<MazeCell>();
        }

        public void PrimMazeGen(Maze maze, int width, int height)
        {
            this.maze = maze;
            //First, we need to pick a random point 
            int randomColumn = Random.Range(0, width - 1);
            int randomRow = Random.Range(0, height - 1);

            mazeWidth = width;
            mazeHeight = height;

            currentCell = maze.GetMazeCell(randomColumn, randomRow);
            passageSet.Add(currentCell);
            FindFrontierCellsInGrid(currentCell);

            while (frontierSet.Count != 0)
            {
                currentFrontierCell = frontierSet.ElementAt(Random.Range(0, frontierSet.Count));
                RemoveWalls(currentFrontierCell, FindFrontierAdjacent());
                passageSet.Add(currentFrontierCell);
                FindFrontierCellsInGrid(currentFrontierCell);
                frontierSet.Remove(currentFrontierCell);
            }

            AcquireExit();

        }

        private void AcquireExit()
        {
            int randomX = Random.Range(0, mazeWidth);
            int randomY = Random.Range(0, mazeHeight);
            maze.GetMazeCell(0, randomX).RemoveDownWall();
            maze.GetMazeCell(0, randomX).RemoveUpWall();

            maze.GetMazeCell(randomY, 0).RemoveRightWall();
            maze.GetMazeCell(randomY, 0).RemoveLeftWall();
        }


        private MazeCell FindFrontierAdjacent()
        {
            HashSet<MazeCell> frontierPassageNeigbours = new HashSet<MazeCell>();
            foreach (var cell in passageSet)
            {
                if (cell.column + 1 == currentFrontierCell.column && cell.row == currentFrontierCell.row) // if the frontier is right of passage node 
                {
                    frontierPassageNeigbours.Add(cell);
                }

                if (cell.column - 1 == currentFrontierCell.column && cell.row == currentFrontierCell.row) // if the frontier is left of passage node 
                {
                    frontierPassageNeigbours.Add(cell);
                }

                if (cell.column == currentFrontierCell.column && cell.row + 1 == currentFrontierCell.row) // if the frontier is above of passage node 
                {
                    frontierPassageNeigbours.Add(cell);
                }

                if (cell.column == currentFrontierCell.column && cell.row - 1 == currentFrontierCell.row) // if the frontier is below of passage node 
                {
                    frontierPassageNeigbours.Add(cell);
                }
            }

            return frontierPassageNeigbours.ElementAt(Random.Range(0, frontierPassageNeigbours.Count)); // returns a random passage node that is bordering the frontier node 
        }

        private void RemoveWalls(MazeCell currentFrontierCell, MazeCell passCell)
        {
            if (currentFrontierCell.column > passCell.column && currentFrontierCell.HasLeftWalls() && passCell.HasRightWalls())
            {
                currentFrontierCell.RemoveLeftWall();
                passCell.RemoveRightWall();
            }

            if (currentFrontierCell.column < passCell.column && currentFrontierCell.HasRightWalls() && passCell.HasLeftWalls())
            {
                currentFrontierCell.RemoveRightWall();
                passCell.RemoveLeftWall();
            }

            if (currentFrontierCell.row > passCell.row && currentFrontierCell.HasDownWalls() && passCell.HasUpWalls() && currentFrontierCell.column == passCell.column)
            {
                currentFrontierCell.RemoveDownWall();
                passCell.RemoveUpWall();
            }

            if (currentFrontierCell.row < passCell.row && currentFrontierCell.HasUpWalls() && passCell.HasDownWalls() && currentFrontierCell.column == passCell.column)
            {
                currentFrontierCell.RemoveUpWall();
                passCell.RemoveDownWall();
            }
        }

        private void FindFrontierCellsInGrid(MazeCell currentMazeCell)
        {

            if (maze.inCell(currentMazeCell.row + 1, currentMazeCell.column) &&
                !passageSet.Contains(maze.GetMazeCell(currentMazeCell.row + 1, currentMazeCell.column)))
            {
                frontierSet.Add(maze.GetMazeCell(currentMazeCell.row + 1, currentMazeCell.column));
            }

            if (maze.inCell(currentMazeCell.row - 1, currentMazeCell.column) &&
                !passageSet.Contains(maze.GetMazeCell(currentMazeCell.row - 1, currentMazeCell.column)))
            {
                frontierSet.Add(maze.GetMazeCell(currentMazeCell.row - 1, currentMazeCell.column));
            }

            if (maze.inCell(currentMazeCell.row, currentMazeCell.column + 1) &&
                !passageSet.Contains(maze.GetMazeCell(currentMazeCell.row, currentMazeCell.column + 1)))
            {
                frontierSet.Add(maze.GetMazeCell(currentMazeCell.row, currentMazeCell.column + 1));
            }

            if (maze.inCell(currentMazeCell.row, currentMazeCell.column - 1) &&
                !passageSet.Contains(maze.GetMazeCell(currentMazeCell.row, currentMazeCell.column - 1)))
            {
                frontierSet.Add(maze.GetMazeCell(currentMazeCell.row, currentMazeCell.column - 1));
            }
        }
    }
}
