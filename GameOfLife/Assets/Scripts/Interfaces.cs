using UnityEngine;

namespace EmanuelTavares.GameOfLife.Models
{
    public interface IBoardModel
    {
        int NumColumns { get; }
        int NumLines { get; }
        float CellWidth { get; }
        float CellHeight { get; }
        ICellModel[,] Cells { get; }
    }

    public interface ICellModel
    {
        bool IsAlive { get; set; }
        ICellModel Clone();
        GameObject GameObject { get; }
        Transform Transform { get; }
    }
}
