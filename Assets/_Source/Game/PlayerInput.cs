using Core;
using UnityEngine;

namespace Game
{
  public class PlayerInput : MonoBehaviour
  {
    private GameField _gameField;

    public void Init(GameField gameField)
    {
      _gameField = gameField;
    }

    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.W))
      {
        _gameField.MoveCells(Vector2Int.up);
      }
      
      if (Input.GetKeyDown(KeyCode.S))
      {
        _gameField.MoveCells(Vector2Int.down);
      }
      
      if (Input.GetKeyDown(KeyCode.A))
      {
        _gameField.MoveCells(Vector2Int.left);
      }
      
      if (Input.GetKeyDown(KeyCode.D))
      {
        _gameField.MoveCells(Vector2Int.right);
      }
    }
  }
}