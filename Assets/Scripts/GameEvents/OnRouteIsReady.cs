namespace GameEvents
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
