// Topic: Indexers — T1.3 Matrix2D (2D indexer)
// Задача: реализовать матрицу фиксированного размера с индексатором this[int row, int col].
// Требования:
// - Конструктор Matrix2D(int rows, int cols): rows>0, cols>0; иначе ArgumentOutOfRangeException.
// - Свойства Rows, Cols — размеры.
// - Хранение можно сделать во внутреннем одномерном массиве длиной rows*cols.
// - Индексатор get/set с проверкой границ (row in [0, Rows), col in [0, Cols)), иначе ArgumentOutOfRangeException.
// - Отображение (row, col) -> index: row * Cols + col.

namespace App.Topics.Indexers.T1_3_Matrix2D;

public class Matrix2D
{
    private readonly double[] _data;

    public int Rows { get; }
    public int Cols { get; }

    public Matrix2D(int rows, int cols)
    {
        if (rows <= 0)
            throw new ArgumentOutOfRangeException(nameof(rows), "Rows must be greater than 0.");

        if (cols <= 0)
            throw new ArgumentOutOfRangeException(nameof(cols), "Cols must be greater than 0.");

        Rows = rows;
        Cols = cols;
        _data = new double[rows * cols];
    }

    public double this[int row, int col]
    {
        get
        {
            ValidateIndices(row, col);
            int index = row * Cols + col;
            return _data[index];
        }
        set
        {
            ValidateIndices(row, col);
            int index = row * Cols + col;
            _data[index] = value;
        }
    }

    private void ValidateIndices(int row, int col)
    {
        if (row < 0 || row >= Rows)
            throw new ArgumentOutOfRangeException($"Row index {row} is out of range.");

        if (col < 0 || col >= Cols)
            throw new ArgumentOutOfRangeException($"Column index {col} is out of range.");
    }
}
