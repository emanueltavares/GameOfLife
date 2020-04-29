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

                    // get random value between 0 and 2 (exclusive). If equal to 0, 
                    Cells[i, j].IsAlive = RandomExt.GetNextBool(); 
                }
            }
        }
    }
}