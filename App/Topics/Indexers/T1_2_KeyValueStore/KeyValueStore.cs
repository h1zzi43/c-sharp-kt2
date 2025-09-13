// Topic: Indexers — T1.2 KeyValueStore (overload by parameter type)
// Задача: реализовать класс со СВЯЗАННЫМИ индексаторами по int и string.
// Требования:
// - Индексатор this[int id] и this[string key] возвращают/устанавливают string значение.
// - get: если ключ/ид неизвестен — бросать KeyNotFoundException.
// - set: если ключ/ид отсутствует — добавить; если есть — заменить значение.
// - null-ключи/строки: при попытке доступа по null string — бросать ArgumentNullException.
// Примечание: цель — понять перегрузку индексаторов по разным типам параметров.

namespace App.Topics.Indexers.T1_2_KeyValueStore;

public class KeyValueStore
{
    private readonly Dictionary<int, string> _idToValue;
    private readonly Dictionary<string, int> _keyToId;
    private readonly Dictionary<int, string> _idToKey;
    private int _nextId;

    public KeyValueStore()
    {
        _idToValue = new Dictionary<int, string>();
        _keyToId = new Dictionary<string, int>();
        _idToKey = new Dictionary<int, string>();
        _nextId = 1;
    }

    // Индексатор по int id
    public string this[int id]
    {
        get
        {
            if (_idToValue.TryGetValue(id, out string value))
                return value;

            throw new KeyNotFoundException($"Элемент с ID {id} не найден");
        }
        set
        {
            if (!_idToValue.ContainsKey(id))
                throw new KeyNotFoundException($"Элемент с ID {id} не найден");

            _idToValue[id] = value;
        }
    }

    // Индексатор по string key
    public string this[string key]
    {
        get
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Ключ не может быть null");

            if (_keyToId.TryGetValue(key, out int id))
                return _idToValue[id];

            throw new KeyNotFoundException($"Элемент с ключом '{key}' не найден");
        }
        set
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Ключ не может быть null");

            if (_keyToId.TryGetValue(key, out int id))
            {
                // Замена существующего значения
                _idToValue[id] = value;
            }
            else
            {
                // Добавление нового элемента
                int newId = _nextId++;
                _idToValue[newId] = value;
                _keyToId[key] = newId;
                _idToKey[newId] = key;
            }
        }
    }

    // Методы для удобства работы
    public bool ContainsKey(string key)
    {
        if (key == null)
            throw new ArgumentNullException(nameof(key));

        return _keyToId.ContainsKey(key);
    }

    public bool ContainsId(int id)
    {
        return _idToValue.ContainsKey(id);
    }

    public int GetIdByKey(string key)
    {
        if (key == null)
            throw new ArgumentNullException(nameof(key));

        if (_keyToId.TryGetValue(key, out int id))
            return id;

        throw new KeyNotFoundException($"Ключ '{key}' не найден");
    }

    public string GetKeyById(int id)
    {
        if (_idToKey.TryGetValue(id, out string key))
            return key;

        throw new KeyNotFoundException($"ID {id} не найден");
    }

    public int Count => _idToValue.Count;

    // Метод для удаления элемента
    public bool Remove(string key)
    {
        if (key == null)
            throw new ArgumentNullException(nameof(key));

        if (_keyToId.TryGetValue(key, out int id))
        {
            _keyToId.Remove(key);
            _idToValue.Remove(id);
            _idToKey.Remove(id);
            return true;
        }

        return false;
    }

    public bool Remove(int id)
    {
        if (_idToKey.TryGetValue(id, out string key))
        {
            _keyToId.Remove(key);
            _idToValue.Remove(id);
            _idToKey.Remove(id);
            return true;
        }

        return false;
    }

    // Метод для очистки хранилища
    public void Clear()
    {
        _idToValue.Clear();
        _keyToId.Clear();
        _idToKey.Clear();
        _nextId = 1;
    }

    // Метод для отладки
    public override string ToString()
    {
        var items = new List<string>();
        foreach (var kvp in _idToValue)
        {
            string key = _idToKey.TryGetValue(kvp.Key, out string k) ? k : "null";
            items.Add($"{key}(ID:{kvp.Key})='{kvp.Value}'");
        }
        return $"KeyValueStore: [{string.Join(", ", items)}]";
    }
}
