namespace Memory_wfa;

public class Linear2DArray<T>
{
    private readonly T[] _data;
    private readonly int _width;
    private readonly int _height;

    public int Width => _width;
    public int Height => _height;

    public Linear2DArray(int width, int height)
    {
        if (width <= 0 || height <= 0) throw new ArgumentOutOfRangeException("Numbers must be greater than zero");

        _width = width;
        _height = height;
        _data = new T[width * height];
    }

    public T this[int x, int y]
    {
        get
        {
            ValidationConstraints(x, y);
            return _data[x * _height + y];
        }
        set
        {
            ValidationConstraints(x, y);
            _data[x * _height + y] = value;
        }
    }

    public T this[int rawIndex]
    {
        get { return _data[rawIndex]; }
        set { _data[rawIndex] = value; }
    }

    private void ValidationConstraints(int x, int y)
    {
        if (x < 0 || x >= _width || y < 0 || y >= _height) throw new ArgumentOutOfRangeException("Invalid coordinates");
    }

    public void ApplyList(IEnumerable<T> list)
    {
        if (list.Count() != _width * _height)
        {
            throw new IndexOutOfRangeException("Given list is too long");
        }

        for (int i = 0; i < list.Count(); i++)
        {
            _data[i] = list.ToArray()[i];
        }
    }
}