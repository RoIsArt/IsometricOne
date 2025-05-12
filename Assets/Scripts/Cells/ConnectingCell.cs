using System.Collections.Generic;
using System.Linq;
using DatasAndConfigs;
using GameEvents;
using UnityEngine;

namespace Cells
{
    public class ConnectingCell : Cell
    {
        protected List<ConnectingSide> ConnectingSides;

        public void Construct(ConnectingCellData data, Vector2Int index, IEventBus eventBus)
        {
            base.Construct(data, index, eventBus);
            ConnectingSides = new List<ConnectingSide>();
            ConnectingSides = data.ConnectingSides;
        }

        public ConnectingSide GetConnectingSide(SideName fromSideName) => 
            ConnectingSides.FirstOrDefault(x => x.SideName == fromSideName);

        public ConnectingSide GetUnconnetSide() => 
            ConnectingSides.FirstOrDefault((x => !x.IsConnected));

        public bool ContainSide(SideName sideName)
        {
            foreach (var connectingSide in ConnectingSides)
            {
                if (connectingSide.SideName == sideName) return true;
            }

            return false;
        }
    }
}
