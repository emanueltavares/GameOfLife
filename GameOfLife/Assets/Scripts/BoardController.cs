using EmanuelTavares.GameOfLife.Models;
using UnityEngine;

namespace EmanuelTavares.GameOfLife.Controllers
{
    public class BoardController : MonoBehaviour
    {
        // Serialized Fields
        [SerializeField] private int _numLines;
        [SerializeField] private int _numColumns;
        [SerializeField] private float _cellWidth;
        [SerializeField] private float _cellHeight;
        [SerializeField] private GameObject _cellPrefab;

        // Private variables
        private IBoardModel _boardModel;

        protected virtual void OnEnable()
        {
            BuildBoard(_numLines, _numColumns, _cellWidth, _cellHeight);
        }

        private void BuildBoard(int numLines, int numColumns, float cellWidth = 1f, float cellHeight = 1f)
        {
            _boardModel = new BoardModel(numLines, numColumns, cellWidth, cellHeight);

            float halfWidth = cellWidth * (numColumns - 1) * 0.5f;
            float halfHeight = cellHeight * (numLines - 1) * 0.5f;
            for (int i = 0; i < numLines; i++)
            {
                for (int j = 0; j < numColumns; j++)
                {
                    GameObject cellInstance = Instantiate(_cellPrefab, transform, true);
                    cellInstance.transform.localScale = new Vector3(cellWidth, cellHeight, 1f);

                    float x = Mathf.Lerp(-halfWidth, halfWidth, j / (numColumns - 1f));
                    float y = Mathf.Lerp(halfHeight, -halfHeight, i / (numLines - 1f));
                    cellInstance.name = string.Format("{0}:{1}", i, j);
                    cellInstance.transform.localPosition = new Vector3(x, y, transform.localPosition.z);
                }
            }
        }
    }
}