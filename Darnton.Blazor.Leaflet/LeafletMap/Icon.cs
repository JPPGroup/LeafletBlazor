using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Darnton.Blazor.Leaflet.LeafletMap
{
    public class Icon : InteropObject
    {
        public IconOptions Options { get; }

        public Icon(IconOptions options)
        {
            Options = options;
        }

        protected override async Task<IJSObjectReference> CreateJsObjectRef()
        {
            var marker = await JSBinder.JSRuntime.InvokeAsync<IJSObjectReference>("L.icon", Options);

            return marker;
        }

        public async Task BuildForMap(Map map)
        {
            if (JSBinder is null)
            {
                await BindJsObjectReference(map.JSBinder);
            }
            GuardAgainstNullBinding("Cannot add icon to map. No JavaScript binding has been set up.");
        }
    }

    public class IconOptions
    {
        public string IconUrl { get; set; }

        public int[] IconAnchor { get; set; } = [24, 48];

        public int[] PopupAnchor { get; set; } = [0, -48];

        public int[] TooltipAnchor { get; set; } = [0, -48];
    }
}
