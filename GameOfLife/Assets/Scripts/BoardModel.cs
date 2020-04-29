using EmanuelTavares.GameOfLife.Utils;

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

        // Private variables
        private readonly bool[,] _cellsStates = new bool[0, 0];

        public BoardModel(int numLines, int numColumns, ICellModel cellModelPrototype, float cellWidth = 1f, float cellHeight = 1f)
        {
            NumLines = numLines;
            NumColumns = numColumns;
            CellWidth = cellWidth;
            CellHeight = cellHeight;
            
            Cells = new ICellModel[NumLines, NumColumns];
            _cellsStates = new bool[NumLines, NumColumns];
            for (int i = 0; i < numLines; i++)
            {
                for (int j = 0; j < numColumns; j++)
                {
                    Cells[i, j] = cellModelPrototype.Clone();

                    Cells[i, j].IsAlive = RandomExt.GetNextBool();

                    _cellsStates[i, j] = Cells[i, j].IsAlive;
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
                    // count neighbours
                    int countAliveNeighbours = 0;

                    // 1
                    if (i - 1 >= 0 && _cellsStates[i - 1, j])
                    {
                        countAliveNeighbours++;

                        // 2
                        if (j - 1 >= 0 && _cellsStates[i - 1, j - 1])
                        {
                            countAliveNeighbours++;
                        }

                        // 3
                        if (j + 1 < CellHeight && _cellsStates[i - 1, j + 1])
                        {
                            countAliveNeighbours++;
                        }
                    }
                    // 4
                    if (i + 1 < CellWidth && _cellsStates[i + 1, j])
                    {
                        countAliveNeighbours++;

                        // 5
                        if (j - 1 >= 0 && _cellsStates[i + 1, j - 1])
                        {
                            countAliveNeighbours++;
                        }

                        // 6
                        if (j + 1 < CellHeight && _cellsStates[i + 1, j + 1])
                        {
                            countAliveNeighbours++;
                        }
                    }

                    // 7
                    if (j - 1 >= 0 && _cellsStates[i, j - 1])
                    {
                        countAliveNeighbours++;
                    }

                    // 8
                    if (j + 1 < CellHeight && _cellsStates[i, j + 1])
                    {
                        countAliveNeighbours++;
                    }

                    if (_cellsStates[i, j]) // is alive
                    {
                        // Any live cell with fewer than two live neighbours dies, as if by underpopulation.
                        if (countAliveNeighbours < 2)
                        {
                            nextCellsStates[i, j] = false;
                        }
                        // Any live cell with more than three live neighbours dies, as if by overpopulation.
                        if (countAliveNeighbours > 2)
                        {
                            nextCellsStates[i, j] = false;
                        }
                        // Any live cell with two or three live neighbours lives on to the next generation.
                        else
                        {
                            nextCellsStates[i, j] = true;
                        }
                    }
                    else if (countAliveNeighbours == 3) // Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
                    {
                        nextCellsStates[i, j] = true;
                    }
                }
            }

            for (int i = 0; i < NumLines; i++)
            {
                for (int j = 0; j < NumColumns; j++)
                {
                    _cellsStates[i, j] = nextCellsStates[i, j];
                    Cells[i, j].IsAlive = _cellsStates[i, j];
                }
            }
        }

        public void ResetModel()
        {
            for (int i = 0; i < NumLines; i++)
            {
                for (int j = 0; j < NumColumns; j++)
                {
                    _cellsStates[i, j] = false;
                    Cells[i, j].IsAlive = _cellsStates[i, j];
                }
            }
        }
    }
}