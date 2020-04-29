namespace EmanuelTavares.GameOfLife.Models
{
    public interface IBoardModel
    {
        int NumColumns { get; }
        int NumLines { get; }
        float CellWidth { get; }
        float CellHeight { get; }
        bool[,] Cells { get; }
    }
}
