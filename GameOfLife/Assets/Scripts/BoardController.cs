using EmanuelTavares.GameOfLife.Models;
using System;
using UnityEngine;

namespace EmanuelTavares.GameOfLife.Controllers
{
    public class BoardController : MonoBehaviour, IBoardController
    {
        // Serialized Fields
        [Range(0.15f, 2f)][SerializeField] private float _maxTimeBetweenUpdates = 1f;
        [Range(3, 100)][SerializeField] private int _numLines = 25;
        [Range(3, 100)][SerializeField] private int _numColumns = 25;
        [Range(0.15f, 5f)] [SerializeField] private float _cellWidth = 0.25f;
        [Range(0.15f, 5f)] [SerializeField] private float _cellHeight = 0.25f;
        [SerializeField] private bool _paused = false;
        #pragma warning disable CS0649
        [SerializeField] private GameObject _cellPrefab;
        [SerializeField] private TMPro.TextMeshProUGUI _simulationStepText;
        #pragma warning restore CS0649

        // Private variables
        private IBoardModel _boardModel = default;
        private ICellModel _cellModelPrototype = default;
        private float _elapsedTimeSinceLastUpdate = 0f;

        // Properties
        public int SimulationStep { get; private set; }

        protected virtual void OnEnable()
        {
            if (_cellModelPrototype == null)
            {
                _cellModelPrototype = _cellPrefab.GetComponent<ICellModel>();
            }

            BuildBoard(_numLines, _numColumns, _cellModelPrototype, _cellWidth, _cellHeight);

            _simulationStepText.text = string.Format("Simulation Step: {0}", SimulationStep.ToString());

            if (!_paused)
            {
                Play();
            }
        }

        protected virtual void Update()
        {
            if (!_paused)
            {
                _elapsedTimeSinceLastUpdate += Time.deltaTime;

                if (_elapsedTimeSinceLastUpdate >= _maxTimeBetweenUpdates)
                {
                    _elapsedTimeSinceLastUpdate -= _maxTimeBetweenUpdates;
                    _boardModel.UpdateModel();
                    SimulationStep += 1;

                    _simulationStepText.text = string.Format("Simulation Step: {0}", SimulationStep.ToString());
                }
            }
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
                    cellModelInstance.OnClick.AddListener(OnClick);
                    cellModelInstance.Transform.localScale = new Vector3(cellWidth, cellHeight, 1f);
                    cellModelInstance.GameObject.name = string.Format("Cell [{0}:{1}]", i, j);

                    Vector3 cellPosition = transform.TransformPoint(new Vector3(x, y, transform.localPosition.z));
                    cellModelInstance.Transform.localPosition = cellPosition;
                    cellModelInstance.Transform.parent = transform;
                }
            }
        }

        private void OnClick(ICellModel cellModel)
        {
            if (_paused)
            {
                cellModel.IsAlive = !cellModel.IsAlive;
            }
        }

        public void Play()
        {
            _paused = false;
        }

        public void Pause()
        {
            _paused = true;
        }

        public void Stop()
        {
            _boardModel.ResetModel();
            
            // Update simulation step
            SimulationStep = 0;
            _simulationStepText.text = string.Format("Simulation Step: {0}", SimulationStep.ToString());

            _paused = true;
        }
    }
}