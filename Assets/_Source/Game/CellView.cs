using System;
using Core;
using TMPro;
using UnityEngine;

namespace Game
{
   public class CellView : MonoBehaviour
   {
      [SerializeField]
      private TextMeshProUGUI _valueText;

      [SerializeField]
      private SpriteRenderer _spriteRenderer;

      [SerializeField]
      private CellViewSettings _cellViewSettings;
      
      private Cell _cell;

      public void Init(Cell cell)
      {
         _cell = cell;

         _cell.OnPositionChanged += UpdatePosition;
         _cell.OnValueChanged += UpdateValue;
         _cell.OnCellDestroyed += DestroyCell;
         
         UpdatePosition();
         UpdateValue(cell.Value);
      }

      private void UpdateValue(int value)
      {
         _valueText.text = $"{Math.Pow(2, value)}";
         _spriteRenderer.color = Color.Lerp(_cellViewSettings.StartColor, _cellViewSettings.EndColor, value / 10f);
      }

      private void UpdatePosition()
      {
         transform.position =
            new Vector2(_cell.X, _cell.Y);
      }

      private void DestroyCell(Cell cell)
      {
         cell.OnPositionChanged -= UpdatePosition;
         cell.OnValueChanged -= UpdateValue;
         cell.OnCellDestroyed -= DestroyCell;
         
         Destroy(gameObject);
      }
   }
}
