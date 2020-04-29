using UnityEngine;
using UnityEngine.Events;

namespace EmanuelTavares.GameOfLife.Models
{
    public interface IBoardModel
    {
        int NumColumns { get; }
        int NumLines { get; }
        float CellWidth { get; }
        float CellHeight { get; }
        ICellModel[,] Cells { get; }

        void UpdateModel();
        void ResetModel();
    }

    public interface ICellModel
    {
        bool IsAlive { get; set; }
        ICellModel Clone();
        GameObject GameObject { get; }
        Transform Transform { get; }
        UnityEvent<ICellModel> OnClick { get; }
    }

    public interface IBoardController
    {
        int SimulationStep { get; }

        void Play();
        void Pause();
        void Stop();
    }
}
