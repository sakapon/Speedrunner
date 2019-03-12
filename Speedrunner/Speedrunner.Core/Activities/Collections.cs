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

    public sealed class VariableCollection : Collection<VariableBase>
    {
        public Dictionary<string, VariableBase> Dictionary { get; } = new Dictionary<string, VariableBase>();

        public VariableBase this[string name] => Dictionary[name];

        protected override void ClearItems()
        {
            base.ClearItems();
            Dictionary.Clear();
        }

        protected override void InsertItem(int index, VariableBase item)
        {
            base.InsertItem(index, item);
            Dictionary[item.VariableName] = item;
        }

        protected override void RemoveItem(int index)
        {
            Dictionary.Remove(this[index].VariableName);
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, VariableBase item)
        {
            base.SetItem(index, item);
            Dictionary[item.VariableName] = item;
        }

        public bool Contains(string name) => Dictionary.ContainsKey(name);

        public VariableBase Get(string name, Type type)
        {
            if (!Dictionary.ContainsKey(name))
                Add(VariableBase.CreateTyped(name, type));
            return Dictionary[name];
        }

        public Variable<T> Get<T>(string name)
        {
            if (!Dictionary.ContainsKey(name))
                Add(new Variable<T> { VariableName = name });
            return (Variable<T>)Dictionary[name];
        }

        public static VariableCollection ToTyped(Collection<Variable> variables)
        {
            var c = new VariableCollection();
            foreach (var item in variables)
                c.Add(item.ToTyped());
            return c;
        }
    }
}
