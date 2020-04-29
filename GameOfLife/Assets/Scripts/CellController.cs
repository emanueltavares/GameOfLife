using EmanuelTavares.GameOfLife.Models;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace EmanuelTavares.GameOfLife.Controllers
{
    public class CellController : MonoBehaviour, ICellModel
    {
        // Serialized Fields
        #pragma warning disable CS0649
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Material _aliveCellMaterial;
        [SerializeField] private Material _deadCellMaterial;
        [SerializeField] private bool _isAlive = false;
        #pragma warning restore CS0649

        // Properties
        public bool IsAlive
        {
            get { return _isAlive; }
            set 
            { 
                _isAlive = value; 
                if (_isAlive)
                {
                    _renderer.material = _aliveCellMaterial;
                }
                else
                {
                    _renderer.material = _deadCellMaterial;
                }
            }
        }
        public int Column { get; private set; }
        public int Line { get; private set; }
        public GameObject GameObject { get => gameObject; }
        public Transform Transform { get => transform; }
        public UnityEvent<ICellModel> OnClick { get; private set; } = new CellModelClickEvent();

        // Methods
        protected virtual void OnValidate()
        {
            if (_isAlive)
            {
                _renderer.material = _aliveCellMaterial;
            }
            else
            {
                _renderer.material = _deadCellMaterial;
            }
        }

        protected virtual void OnDisable()
        {
            OnClick?.RemoveAllListeners();
        }

        protected virtual void OnMouseUpAsButton()
        {
            OnClick?.Invoke(this);
        }

        public ICellModel Clone()
        {
            return Instantiate(this);
        }
    }

    public class CellModelClickEvent : UnityEvent<ICellModel> { }
}
