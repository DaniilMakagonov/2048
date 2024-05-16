using System;
using Core;
using TMPro;
using UnityEngine;

namespace Game
{
  public class Game : MonoBehaviour
  {
    private int _score = 0;

    [SerializeField] 
    private TextMeshProUGUI _scoreText;
    
    [SerializeField]
    private CellSpawner _cellSpawner;

    [SerializeField]
    private PlayerInput _playerInput;
    private GameField _gameField;

    public void Awake()
    {
      StartGame();
    }

    private void StartGame()
    {
      _gameField = new GameField(4);
      
      _cellSpawner.Init(_gameField);
      _playerInput.Init(_gameField);
      
      _gameField.InitializeField();
      _gameField.OnCellCreated += UpdateScore;
      _gameField.CreateCell();
      _gameField.CreateCell();
    }

    private void UpdateScore(Cell cell)
    {
      _score += (int)Math.Pow(2, cell.Value);
      _scoreText.text = $"Score: {_score}";
    }
  }
}