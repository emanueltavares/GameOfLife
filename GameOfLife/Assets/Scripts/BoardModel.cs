namespace EmanuelTavares.GameOfLife.Models
{
    public class BoardModel : IBoardModel
    {
        // Properties
        public int NumColumns { get; private set; }
        public int NumLines { get; private set; }
        public float CellWidth { get; private set; }
        public float CellHeight { get; private set; }
        public bool[,] Cells { get; private set; }

        public BoardModel(int numlines, int numColumns, float cellWidth = 1f, float cellHeight = 1f)
        {
            NumLines = numlines;
            NumColumns = numColumns;
            CellWidth = cellWidth;
            CellHeight = cellHeight;
            Cells = new bool[NumLines, NumColumns];
        }
    }
}