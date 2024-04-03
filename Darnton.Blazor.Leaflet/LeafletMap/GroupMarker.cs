using Microsoft.JSInterop;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Darnton.Blazor.Leaflet.LeafletMap
{
    /// <summary>
    /// A clickable, draggable icon that can be added to a <see cref="Map"/>
    /// <see href="https://leafletjs.com/reference-1.7.1.html#marker"/>
    /// </summary>
    public class GroupMarker : InteractiveLayer
    {
        Map map;

        /// <summary>
        /// Constructs a marker
        /// </summary>
        /// <param name="latlng">The initial position of the marker.</param>
        /// <param name="options">The <see cref="MarkerOptions"/> used to create the marker.</param>
        public GroupMarker(Map m)
        {
            map = m;
        }

        /// <inheritdoc/>
        protected override async Task<IJSObjectReference> CreateJsObjectRef()
        {
            return await JSBinder.JSRuntime.InvokeAsync<IJSObjectReference>("L.markerClusterGroup", new GroupMarkerOptions());
        }

        public async Task AddLayer(Layer layer)
        {
            if (JSBinder is null)
            {
                await BindJsObjectReference(map.JSBinder);
            }
            GuardAgainstNullBinding("Cannot add layer to map. No JavaScript binding has been set up.");
            //await JSBinder.JSRuntime.InvokeAsync<IJSObjectReference>("addLayer");

            await layer.BindJsObjectReference(map.JSBinder);
            await this.JSObjectReference.InvokeVoidAsync("addLayer", layer.JSObjectReference);
        }
    }

    public class GroupMarkerOptions
    {
        public int MaxClusterRadius { get; set; } = 50;

        public int DisableClusteringAtZoom { get; set; } = 14;
    }
}
