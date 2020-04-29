using UnityEngine;

namespace EmanuelTavares.GameOfLife.Models
{
    public class BoardModel : IBoardModel
    {
        // Properties
        public int NumColumns { get; private set; }
        public int NumLines { get; private set; }
        public float CellWidth { get; private set; }
        public float CellHeight { get; private set; }
        public ICellModel[,] Cells { get; private set; }

        public BoardModel(int numLines, int numColumns, ICellModel cellModelPrototype, float cellWidth = 1f, float cellHeight = 1f)
        {
            NumLines = numLines;
            NumColumns = numColumns;
            CellWidth = cellWidth;
            CellHeight = cellHeight;
            
            Cells = new ICellModel[NumLines, NumColumns];
            for (int i = 0; i < numLines; i++)
            {
                for (int j = 0; j < numColumns; j++)
                {
                    Cells[i, j] = cellModelPrototype.Clone();
                    //Cells[i, j].IsAlive = RandomExt.GetNextBool();
                }
            }
        }

        public void UpdateModel()
        {
            bool[,] nextCellsStates = new bool[NumLines, NumColumns];

            for (int i = 0; i < NumLines; i++)
            {
                for (int j = 0; j < NumColumns; j++)
                {
                    //string debugMessage = string.Format( "{0}:{1} ", i, j);

                    // count neighbours
                    int liveCells = 0;

                    int top = i - 1;
                    int bottom = i + 1;
                    int left = j - 1;
                    int right = j + 1;
                    
                    if (top >= 0)
                    {
                        // 1
                        if (Cells[top, j].IsAlive)
                        {
                            liveCells++;
                            //debugMessage += string.Format("{0}:{1} ", top, j);
                        }

                        // 2
                        if (left >= 0 && Cells[top, left].IsAlive)
                        {
                            liveCells++;
                            //debugMessage += string.Format("{0}:{1} ", top, left);
                        }

                        // 3
                        if (right < NumColumns && Cells[top, right].IsAlive)
                        {
                            liveCells++;
                            //debugMessage += string.Format("{0}:{1} ", top, right);
                        }
                    }

                    if (bottom < NumLines)
                    {
                        // 4
                        if (Cells[bottom, j].IsAlive)
                        {
                            liveCells++;
                            //debugMessage += string.Format("{0}:{1} ", bottom, j);
                        }

                        // 5
                        if (left >= 0 && Cells[bottom, left].IsAlive)
                        {
                            liveCells++;
                            //debugMessage += string.Format("{0}:{1} ", bottom, left);
                        }

                        // 6
                        if (right < NumColumns && Cells[bottom, right].IsAlive)
                        {
                            liveCells++;
                            //debugMessage += string.Format("{0}:{1} ", bottom, right);
                        }
                    }

                    // 7
                    if (left >= 0 && Cells[i, left].IsAlive)
                    {
                        liveCells++;
                        //debugMessage += string.Format("{0}:{1} ", i, left);
                    }

                    // 8
                    if (right < NumColumns && Cells[i, right].IsAlive)
                    {
                        liveCells++;
                        //debugMessage += string.Format("{0}:{1} ", i, right);
                    }

                    
                    if (Cells[i, j].IsAlive) // is alive
                    {
                        // Any live cell with fewer than two live neighbours dies, as if by underpopulation.
                        // Any live cell with more than three live neighbours dies, as if by overpopulation.
                        // Any live cell with two or three live neighbours lives on to the next generation.
                        if (liveCells == 2 || liveCells == 3)
                        {
                            nextCellsStates[i, j] = true;
                        }

                    }
                    else if (liveCells == 3) // Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
                    {
                        nextCellsStates[i, j] = true;
                    }
                }
            }

            for (int i = 0; i < NumLines; i++)
            {
                for (int j = 0; j < NumColumns; j++)
                {
                    Cells[i, j].IsAlive = nextCellsStates[i, j];
                }
            }
        }

        public void ResetModel()
        {
            for (int i = 0; i < NumLines; i++)
            {
                for (int j = 0; j < NumColumns; j++)
                {
                    Cells[i, j].IsAlive = false;
                }
            }
        }
    }
}