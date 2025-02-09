using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "StationResourceExtraction", menuName = "Scriptable Objects/StationResourceExtraction")]
public class StationResourceExtraction : ScriptableObject
{
    [System.Serializable]
    public class Item
    {
        public string nameStation;
        public string nameForm;
        public int price;
        public Sprite icon; 
    }

    public Item[] items;
}
