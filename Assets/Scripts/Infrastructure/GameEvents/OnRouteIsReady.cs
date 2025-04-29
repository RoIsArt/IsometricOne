using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GameEvents
{
    public class OnRouteIsReady
    {
        public readonly Route Route;

        public OnRouteIsReady(Route route)
        {
            this.Route = route;
        }
    }
}
