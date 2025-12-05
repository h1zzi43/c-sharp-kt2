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
    // Два словаря для хранения значений по ключам разных типов
    private readonly Dictionary<int, string> _intStore = new Dictionary<int, string>();
    private readonly Dictionary<string, string> _stringStore = new Dictionary<string, string>();

    // Словарь для связи между int и string ключами
    private readonly Dictionary<int, string> _intToStringKey = new Dictionary<int, string>();
    private readonly Dictionary<string, int> _stringToIntKey = new Dictionary<string, int>();

    // Индексатор для доступа по int
    public string this[int id]
    {
        get
        {
            // Получаем значение из словаря int ключей
            if (_intStore.TryGetValue(id, out var value))
                return value;

            throw new KeyNotFoundException($"Key '{id}' not found.");
        }
        set
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "Value cannot be null");

            // Если ключ уже существует, заменяем значение
            if (_intStore.ContainsKey(id))
            {
                _intStore[id] = value;

                // Если есть связанный строковый ключ, обновляем и там
                if (_intToStringKey.TryGetValue(id, out var stringKey))
                {
                    _stringStore[stringKey] = value;
                }
            }
            else
            {
                // Добавляем новый ключ-значение
                _intStore.Add(id, value);
            }
        }
    }

    // Индексатор для доступа по string
    public string this[string key]
    {
        get
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Key cannot be null");

            // Получаем значение из словаря string ключей
            if (_stringStore.TryGetValue(key, out var value))
                return value;

            throw new KeyNotFoundException($"Key '{key}' not found.");
        }
        set
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Key cannot be null");

            if (value == null)
                throw new ArgumentNullException(nameof(value), "Value cannot be null");

            // Если ключ уже существует, заменяем значение
            if (_stringStore.ContainsKey(key))
            {
                _stringStore[key] = value;

                // Если есть связанный int ключ, обновляем и там
                if (_stringToIntKey.TryGetValue(key, out var intKey))
                {
                    _intStore[intKey] = value;
                }
            }
            else
            {
                // Добавляем новый ключ-значение
                _stringStore.Add(key, value);

                // Генерируем новый int ключ для этого string ключа
                // Находим минимальный доступный int ключ
                int newIntKey = 1;
                while (_intStore.ContainsKey(newIntKey))
                {
                    newIntKey++;
                }

                // Связываем ключи
                _intStore.Add(newIntKey, value);
                _intToStringKey.Add(newIntKey, key);
                _stringToIntKey.Add(key, newIntKey);
            }
        }
    }
}