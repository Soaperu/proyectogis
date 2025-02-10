using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Core.Events;
using ArcGIS.Desktop.Mapping;

namespace Sigcatmin.pro.Application.Interfaces
{
    public interface IMapViewService
    {
        MapView ActiveMapView { get; }
        SubscriptionToken SubscribeToDrawCompleteEvent(Action<EventArgs> callback);
        void UnsubscribeFromDrawCompleteEvent(SubscriptionToken token);
    }
}
