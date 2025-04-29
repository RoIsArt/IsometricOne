using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CellsDataConfig", menuName = "Cells/GridConfig")]
public class CellsDataConfig : ScriptableObject
{
    [SerializeField] private List<CellData> _cellDatas;

    private Dictionary<CellType, CellData> _datasDictionary;

    public Dictionary<CellType, CellData> CellDatas 
    { 
        get 
        { 
            if(_datasDictionary == null)
            {
                DatasInitialize();
            }

            return _datasDictionary; 
        } 
    }

    private void DatasInitialize()
    {
        _datasDictionary = new Dictionary<CellType, CellData>();
        foreach (var data in _cellDatas)
        {
            _datasDictionary.Add(data.Type, data);
        }
    }
}

