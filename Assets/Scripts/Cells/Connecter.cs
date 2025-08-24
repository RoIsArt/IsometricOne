using System.Collections.Generic;
using System.Linq;
using DatasAndConfigs;
using NUnit.Framework;

namespace Cells
{
    public class Connecter
    {
        private readonly Dictionary<SideName, Cell> _sides = new();
        public Connecter(CellData data)
        {
            foreach (SideName side in data.Sides)
            {
                _sides.Add(side, null);
            }
        }

        public Dictionary<SideName, Cell> Sides => _sides;
        
        public bool ContainSide(SideName sideName) => 
            _sides.ContainsKey(sideName);

        public void Connect(SideName sideName, Cell forConnect)
        {
            if (ContainSide(sideName))
                _sides[sideName] = forConnect;
        }

        public void Disconnect(SideName sideName)
        {
            if (ContainSide(sideName))
                _sides[sideName] = null;
        }

        public void DisconnectAll()
        {
            foreach (SideName key in _sides.Keys)
            {
                _sides[key] = null;
            }
        }
    }
}