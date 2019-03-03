using System;
using System.Collections.Generic;
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
        public Dictionary<string, Variable> Dictionary { get; } = new Dictionary<string, Variable>();

        protected override void ClearItems()
        {
            base.ClearItems();
            Dictionary.Clear();
        }

        protected override void InsertItem(int index, Variable item)
        {
            base.InsertItem(index, item);
            Dictionary[item.VariableName] = item;
        }

        protected override void RemoveItem(int index)
        {
            Dictionary.Remove(this[index].VariableName);
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, Variable item)
        {
            base.SetItem(index, item);
            Dictionary[item.VariableName] = item;
        }

        public Variable<T> Get<T>(string name)
        {
            if (!Dictionary.ContainsKey(name))
                Add(new Variable<T> { VariableName = name });
            return (Variable<T>)Dictionary[name];
        }

        public VariableCollection ToTyped()
        {
            var c = new VariableCollection();
            foreach (var item in this)
                c.Add(item.ToTyped());
            return c;
        }
    }
}
