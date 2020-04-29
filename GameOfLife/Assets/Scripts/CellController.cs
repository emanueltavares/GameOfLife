﻿using EmanuelTavares.GameOfLife.Models;
using UnityEngine;

namespace EmanuelTavares.GameOfLife.Controllers
{
    public class CellController : MonoBehaviour, ICellModel
    {
        // Serialized Fields
        #pragma warning disable CS0649
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Material _aliveCellMaterial;
        [SerializeField] private Material _deadCellMaterial;
        #pragma warning restore CS0649

        // Private variables
        private bool _isAlive = false;

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
        public GameObject GameObject { get => gameObject; }
        public Transform Transform { get => transform; }

        // Methods
        public ICellModel Clone()
        {
            return Instantiate(this);
        }
    }
}