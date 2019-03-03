using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Speedrunner.Activities
{
    [Obsolete]
    public sealed class ActivityCollection : Collection<Activity>
    {
    }

    public sealed class VariableCollection : Collection<Variable>
    {
        public Variable<T> Get<T>(string name) =>
            (Variable<T>)this.FirstOrDefault(v => v.VariableName == name);
    }
}
