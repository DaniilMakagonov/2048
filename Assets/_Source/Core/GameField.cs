using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
  public class GameField
  {
    private List<Cell>       _cells = new();
    private readonly List<(int, int)> _emptyPosition = new();

    private readonly int _fieldSize;

    public event System.Action<Cell> OnCellCreated;

    public GameField(int fieldSize)
    {
      _fieldSize = fieldSize;
    }

    public void MoveCells(Vector2Int vector)
    {
      _cells = (from cell in _cells
        orderby cell.X * vector.x + cell.Y * vector.y descending
        select cell).ToList();

      bool isCellMoved = false;
      
      for (int i = 0; i < _cells.Count; i++)
      {
        _emptyPosition.Add((_cells[i].X, _cells[i].Y));
        while (CanCellMove(_cells[i], vector))
        {
          _cells[i].X += vector.x;
          _cells[i].Y += vector.y;
          isCellMoved = true;
        }
        _emptyPosition.Remove((_cells[i].X, _cells[i].Y));

        int combinationCellInd = _cells.FindIndex(
            val => 
              val.X == _cells[i].X + vector.x && 
              val.Y == _cells[i].Y + vector.y &&
              val.Value == _cells[i].Value
            );

        if (combinationCellInd < 0) continue;
        
        _cells[combinationCellInd].Value++;
        _cells[i].Destroy();
        isCellMoved = true;
        i--;
        
      }

      if (isCellMoved)
      {
        CreateCell();
      }
    }
    
    public void InitializeField()
    {
      for (int i = 0; i < _fieldSize; i++)
      {
        for (int j = 0; j < _fieldSize; j++)
        {
          _emptyPosition.Add((i, j));
        }
      }
    }

    public void CreateCell()
    {
      (int, int) cellPosition = GetEmptyPosition();

      Cell cell = 
        new Cell(cellPosition.Item1, cellPosition.Item2, CalculateValue());
      
      _cells.Add(cell);
      
      cell.OnCellDestroyed += DestroyCell;
      OnCellCreated?.Invoke(cell);
    }

    private (int, int) GetEmptyPosition()
    {
      int positionPointer =
        Random.Range(0, _emptyPosition.Count);
      
      (int, int) emptyCell = _emptyPosition[positionPointer];

      _emptyPosition.Remove(emptyCell);

      return emptyCell;
    }

    private int CalculateValue()
    {
      int value = Random.Range(0, 101);
      return value <= 10 ? 2 : 1;
    }

    private bool CanCellMove(Cell cell, Vector2Int direction)
    {
      (int, int) newPosition = (cell.X + direction.x, cell.Y + direction.y);
      return _emptyPosition.Contains(newPosition);
    }

    private void DestroyCell(Cell cell)
    {
      _cells.Remove(cell);
      _emptyPosition.Add((cell.X, cell.Y));
      cell.OnCellDestroyed -= DestroyCell;
    }
  }
}