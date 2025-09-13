// Topic: Indexers — T1.1 IntList (basic)
// Задача: реализовать класс динамического списка целых чисел с индексатором this[int index].
// Требования:
// - Свойство Count — текущее количество элементов.
// - Индексатор get должен бросать ArgumentOutOfRangeException при index < 0 или index >= Count.
// - Индексатор set:
//   * если index в диапазоне [0, Count-1] — заменить значение;
//   * если index == Count — добавить значение в конец (расширение на 1);
//   * если index > Count или index < 0 — бросать ArgumentOutOfRangeException.
// Примечание: это упражнение тренирует базовую работу с индексатором.

namespace App.Topics.Indexers.T1_1_IntList;

public class IntList
{
    private int[] _items;
    private int _count;

    public IntList(int initialCapacity = 4)
    {
        _items = new int[initialCapacity];
        _count = 0;
    }

    // Свойство Count — текущее число элементов
    public int Count => _count;

    // Индексатор с get/set и проверкой границ
    public int this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
                throw new ArgumentOutOfRangeException(nameof(index),
                    $"Индекс {index} выходит за границы списка (0 - {_count - 1})");

            return _items[index];
        }
        set
        {
            if (index == _count)
            {
                // Добавление в конец
                EnsureCapacity(_count + 1);
                _items[_count] = value;
                _count++;
            }
            else if (index >= 0 && index < _count)
            {
                // Замена значения
                _items[index] = value;
            }
            else
            {
                // Недопустимый индекс
                throw new ArgumentOutOfRangeException(nameof(index),
                    $"Индекс {index} выходит за границы списка (0 - {_count})");
            }
        }
    }

    // Вспомогательный метод для обеспечения достаточной емкости
    private void EnsureCapacity(int minCapacity)
    {
        if (_items.Length < minCapacity)
        {
            int newCapacity = _items.Length == 0 ? 4 : _items.Length * 2;
            if (newCapacity < minCapacity) newCapacity = minCapacity;

            int[] newItems = new int[newCapacity];
            Array.Copy(_items, newItems, _count);
            _items = newItems;
        }
    }

    // Дополнительные методы для удобства
    public void Add(int item)
    {
        this[_count] = item;
    }

    public void Clear()
    {
        _count = 0;
        // Можно обнулить массив для экономии памяти, но это не обязательно
        // Array.Clear(_items, 0, _count);
    }

    // Метод для тестирования
    public override string ToString()
    {
        return $"[{string.Join(", ", _items[0.._count])}] (Count = {_count})";
    }
}
