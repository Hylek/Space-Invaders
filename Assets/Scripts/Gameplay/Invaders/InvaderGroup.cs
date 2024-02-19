using System.Collections.Generic;
using Core;
using UnityEngine;
using Utils;

// Made by Daniel Cumbor in 2024.

namespace Gameplay.Invaders
{
    public class InvaderGroup : MonoBehaviour
    {
        [SerializeField] private ObjectPool invaderPool;
        [SerializeField] private GameObject rowPrefab;

        private List<InvaderRow> _rows;

        private void Start()
        {
            //SetupGroup();
        }

        private void SetupGroup()
        {
            var difficulty = GameManager.Instance.currentDifficulty;
            GenerateRows(difficulty.rowLengths.Count);
            PopulateInvaders(difficulty.rowLengths);
            OrderRows();
        }

        private void GenerateRows(int rowCount)
        {
            _rows = new List<InvaderRow>();
            for (var i = 0; i < rowCount; i++)
            {
                var row = Instantiate(rowPrefab, transform);
                row.name = $"Row{i}";
                _rows.Add(row.GetComponent<InvaderRow>());
            }
        }

        private void PopulateInvaders(IReadOnlyList<int> rowData)
        {
            var rowCount = rowData.Count;
            for (var i = 0; i < rowCount; i++)
            {
                var invaderCount = rowData[i];
                var currentRow = _rows[i];
                for (var j = 0; j < invaderCount; j++)
                {
                    var invader = invaderPool.GetObject();
                    invader.name = $"Invader{j}";
                    var invaderScript = invader.GetComponent<Invader>();
                    currentRow.AddInvader(invaderScript);
                }
            }
        }

        private void OrderRows()
        {
            foreach (var row in _rows)
            {
                row.Order();
            }
        }
    }
}