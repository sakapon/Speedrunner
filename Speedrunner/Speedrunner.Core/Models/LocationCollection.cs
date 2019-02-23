using System.Collections.ObjectModel;
using System.Linq;

namespace TasSample.Models
{
    public sealed class LocationCollection : Collection<Location>
    {
        public Location this[string name]
        {
            get
            {
                return this.Items.SingleOrDefault(p => p.Name == name);
            }
        }
    }
}
