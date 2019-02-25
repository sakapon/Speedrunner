using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Speedrunner.Activities
{
    public sealed class ActivityCollection : Collection<Activity>
    {
    }

    public sealed class VariableCollection : Collection<Variable>
    {
        public Variable this[string name]
        {
            get => this.FirstOrDefault(v => v.Name == name);
        }
    }
}
