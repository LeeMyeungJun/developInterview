using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataFile", menuName = "Scriptable Object/DataFile", order = int.MaxValue)]
public class DataFile : ScriptableObject
{
    [System.Serializable]
    public struct Data
    {
        public string question;
        public string hint;
    }
    public List<Data> datas = new List<Data>();
}
