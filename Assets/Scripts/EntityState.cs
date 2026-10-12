using System.Collections.Generic;
using UnityEngine;

// Only save values that can be written to a save file. Do not store Unity objects here.
public sealed class EntityState
{
    private readonly Dictionary<string, double> numbers = new();
    private readonly Dictionary<string, bool> booleans = new();
    private readonly Dictionary<string, string> strings = new();
    private readonly Dictionary<string, Vector3Int> positions = new();

    public bool Has(string key) => numbers.ContainsKey(key) || booleans.ContainsKey(key) || strings.ContainsKey(key) || positions.ContainsKey(key);
    public void SetNumber(string key, double value) => numbers[key] = value;
    public double GetNumber(string key, double fallback) => numbers.TryGetValue(key, out double value) ? value : fallback;
    public void SetBool(string key, bool value) => booleans[key] = value;
    public bool GetBool(string key, bool fallback) => booleans.TryGetValue(key, out bool value) ? value : fallback;
    public void SetString(string key, string value) => strings[key] = value;
    public string GetString(string key, string fallback) => strings.TryGetValue(key, out string value) ? value : fallback;
    public void SetPosition(string key, Vector3Int value) => positions[key] = value;
    public Vector3Int GetPosition(string key, Vector3Int fallback) => positions.TryGetValue(key, out Vector3Int value) ? value : fallback;
}
