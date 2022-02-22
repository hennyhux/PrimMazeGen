using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.WebCam;

namespace ZhangHenry.Lab2
{
    public class MazeManager : MonoBehaviour
    {
        [SerializeField] private int width; //x
        [SerializeField] private int height; // z
        [SerializeField] private Transform wallPrefab;
        [SerializeField] private Transform floorPrefab;
        private float tileSize = 1f;

        private Maze maze;

        void Start()
        {
            maze = new Maze(width, height);
            MazeGenerator.MazeGen.PrimMazeGen(maze, width, height);
            DrawMaze(maze);
        }

        private void DrawMaze(Maze maze)
        {
            for (int columns = 0; columns < width; ++columns)
            {
                for (int rows = 0; rows < height; ++rows)
                {
                    var cell = maze.GetMazeCell(rows, columns);
                    var floor = Instantiate(floorPrefab, transform);
                    floor.transform.localPosition = new Vector3(columns, 0, rows);
                    DrawNewCellWalls(cell, columns, rows);
                }
            }
        }

        private void DrawNewCellWalls(MazeCell cell, int columns, int rows)
        {
            if (cell.HasUpWalls())
            {
                var topWall = Instantiate(wallPrefab, transform);
                topWall.position = new Vector3(columns, 0, rows) + new Vector3(0, 0, tileSize / 2);
                topWall.localScale = new Vector3(tileSize, topWall.localScale.y, topWall.localScale.z);
            }

            if (cell.HasLeftWalls())
            {
                var leftWall = Instantiate(wallPrefab, transform);
                leftWall.position = new Vector3(columns, 0, rows) - new Vector3(tileSize / 2, 0, 0);
                leftWall.localScale = new Vector3(tileSize, leftWall.localScale.y, leftWall.localScale.z);
                leftWall.eulerAngles = new Vector3(0, 90, 0);
            }

            if (cell.HasRightWalls())
            {
                var rightWall = Instantiate(wallPrefab, transform);
                rightWall.localScale = new Vector3(tileSize, rightWall.localScale.y, rightWall.localScale.z);
                rightWall.position = new Vector3(columns, 0, rows) + new Vector3(tileSize / 2, 0, 0);
                rightWall.eulerAngles = new Vector3(0, -90, 0);
            }

            if (cell.HasDownWalls())
            {
                var bottomWall = Instantiate(wallPrefab, transform);
                bottomWall.position = new Vector3(columns, 0, rows) - new Vector3(0, 0, tileSize / 2);
                bottomWall.localScale = new Vector3(tileSize, bottomWall.localScale.y, bottomWall.localScale.z);
            }
        }

    }
}