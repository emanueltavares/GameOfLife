using EmanuelTavares.GameOfLife.Models;
using UnityEngine;

namespace EmanuelTavares.GameOfLife.Controllers
{
    public class BoardController : MonoBehaviour
    {
        // Serialized Fields
        #pragma warning disable CS0649
        [SerializeField] private int _numLines;
        [SerializeField] private int _numColumns;
        [SerializeField] private float _cellWidth;
        [SerializeField] private float _cellHeight;
        [SerializeField] private GameObject _cellPrefab;
        #pragma warning restore CS0649

        // Private variables
        private IBoardModel _boardModel = default;
        private ICellModel _cellModelPrototype = default;

        protected virtual void OnEnable()
        {
            if (_cellModelPrototype == null)
            {
                _cellModelPrototype = _cellPrefab.GetComponent<ICellModel>();
            }

            BuildBoard(_numLines, _numColumns, _cellModelPrototype, _cellWidth, _cellHeight);
        }

        private void BuildBoard(int numLines, int numColumns, ICellModel cellModelPrototype, float cellWidth = 1f, float cellHeight = 1f)
        {
            _boardModel = new BoardModel(numLines, numColumns, cellModelPrototype, cellWidth, cellHeight);

            float halfWidth = cellWidth * (numColumns - 1) * 0.5f;
            float halfHeight = cellHeight * (numLines - 1) * 0.5f;
            for (int i = 0; i < numLines; i++)
            {
                for (int j = 0; j < numColumns; j++)
                {
                    float x = Mathf.Lerp(-halfWidth, halfWidth, j / (numColumns - 1f));
                    float y = Mathf.Lerp(halfHeight, -halfHeight, i / (numLines - 1f));

                    ICellModel cellModelInstance = _boardModel.Cells[i, j];
                    cellModelInstance.Transform.localScale = new Vector3(cellWidth, cellHeight, 1f);
                    cellModelInstance.GameObject.name = string.Format("Cell [{0}:{1}]", i, j);

                    Vector3 cellPosition = transform.TransformPoint(new Vector3(x, y, transform.localPosition.z));
                    cellModelInstance.Transform.localPosition = cellPosition;
                    cellModelInstance.Transform.parent = transform;
                }
            }
        }
    }
}