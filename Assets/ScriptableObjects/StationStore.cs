using UnityEngine;

[CreateAssetMenu(fileName = "StationStore", menuName = "Scriptable Objects/StationStore")]
public class StationStore : ScriptableObject
{
    [System.Serializable]
    public class Item
    {
        public string name;
        public int price;
    }

    public Item[] items;
}
