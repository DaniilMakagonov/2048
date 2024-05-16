namespace  Core
{
  public class Cell
  {
    private int _x;
    private int _y;
    private int _value;

    public event System.Action<int>  OnValueChanged;
    public event System.Action       OnPositionChanged;
    public event System.Action<Cell> OnCellDestroyed; 
    
    public int X
    {
      get => _x;
      set
      {
        _x = value;
        OnPositionChanged?.Invoke();
      }
    }
    
    public int Y
    {
      get => _y;
      set
      {
        _y = value;
        OnPositionChanged?.Invoke();
      }
    }
    
    public int Value
    {
      get => _value;
      set
      {
        _value = value;
        OnValueChanged?.Invoke(_value);
      }
    }

    public Cell(int x, int y, int value)
    {
      _x = x;
      _y = y;
      _value = value;
    }

    public void Destroy()
    {
      OnCellDestroyed?.Invoke(this);
    }
  }
}
