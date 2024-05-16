using UnityEngine;

namespace Game
{
  [CreateAssetMenu(fileName = "Cell View Settings", menuName = "SO/new Cell View Settings")]
  public class CellViewSettings : ScriptableObject
  {
    [SerializeField]
    private Color _startColor;

    [SerializeField]
    private Color _endColor;

    public Color StartColor
    {
      get => _startColor;
      set => _startColor = value;
    }
    
    public Color EndColor
    {
      get => _endColor;
      set => _endColor = value;
    }
  }
}