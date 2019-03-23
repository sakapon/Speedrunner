using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Speedrunner.Activities
{
    [DebuggerDisplay(@"\{{GetType().Name}: {Type.Name}.{MethodName}\}")]
    public class InvokeMethod : Activity
    {
        public Type Type { get; set; }
        [DefaultValue("")]
        public string MethodName { get; set; } = "";

        public override void Execute(WorkflowContext context)
        {
            if (Type == null) throw new InvalidOperationException();
            if (string.IsNullOrWhiteSpace(MethodName)) throw new InvalidOperationException();

            var methods = Type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.InvokeMethod)
                .Where(m => m.Name == MethodName)
                .ToArray();

            if (methods.Length != 1) throw new InvalidOperationException();
            var method = methods[0];
            var parameters = method.GetParameters();

            if (parameters.Length == 0)
            {
                method.Invoke(null, null);
            }
            else if (parameters.Length == 1 && parameters[0].ParameterType == typeof(WorkflowContext))
            {
                method.Invoke(null, new[] { context });
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }

    [DebuggerDisplay(@"\{{GetType().Name}: {Text}\}")]
    public class Expression : Activity
    {
        static readonly Regex ExpressionPattern = new Regex(@"^\s*(.+?)\s*[-+*/%&|^<>=!]*=.+$");

        [DefaultValue("")]
        public string Text { get; set; } = "";

        MethodInfo action;

        public override void Execute(WorkflowContext context)
        {
            if (string.IsNullOrWhiteSpace(Text)) return;

            var variables = context.Variables;
            if (action == null) action = CreateAction(variables);

            var parameters = action.GetParameters();
            var args = parameters
                .Select(p => variables.Contains(p.Name) ? variables[p.Name].ActualValue : null)
                .ToArray();
            action.Invoke(null, args);

            var refIndex = Array.FindIndex(parameters, p => p.ParameterType.IsByRef);
            variables.Set(parameters[refIndex].Name, args[refIndex]);
        }

        MethodInfo CreateAction(VariableCollection variables)
        {
            var match = ExpressionPattern.Match(Text);
            if (!match.Success) throw new FormatException();

            var leftVarName = match.Groups[1].Value;
            var script_vars = string.Join(", ", variables.Select(v => $"{(v.VariableName == leftVarName ? "ref " : "")}{v.Type.FullName} {v.VariableName}"));
            if (!variables.Contains(leftVarName)) script_vars += $", ref object {leftVarName}";

            var source = $@"using System;
using System.Collections.Generic;
using System.Linq;

public static class Program
{{
public static void Execute({script_vars})
{{
{Text};
}}
}}";
            var options = new CompilerParameters(new[] { "System.dll", "System.Core.dll" })
            {
                GenerateInMemory = true,
            };
            var provider = CodeDomProvider.CreateProvider("CSharp");
            var results = provider.CompileAssemblyFromSource(options, source);
            if (results.Errors.HasErrors) throw new FormatException();

            return results.CompiledAssembly.GetType("Program").GetMethod("Execute");
        }
    }

    [DebuggerDisplay(@"\{{GetType().Name}: {Condition}\}")]
    public class If : CompositeActivity
    {
        [DefaultValue("")]
        public string Condition { get; set; } = "";

        MethodInfo action;

        public override void Execute(WorkflowContext context)
        {
            if (string.IsNullOrWhiteSpace(Condition)) return;

            var variables = context.Variables;
            if (action == null) action = CreateAction(variables);

            var parameters = action.GetParameters();
            var args = parameters
                .Select(p => variables.Contains(p.Name) ? variables[p.Name].ActualValue : null)
                .ToArray();
            var isSatisfied = (bool)action.Invoke(null, args);

            if (isSatisfied)
                base.Execute(context);
        }

        MethodInfo CreateAction(VariableCollection variables)
        {
            var script_vars = string.Join(", ", variables.Select(v => $"{v.Type.FullName} {v.VariableName}"));

            var source = $@"using System;
using System.Collections.Generic;
using System.Linq;

public static class Program
{{
public static bool Execute({script_vars})
{{
return {Condition};
}}
}}";
            var options = new CompilerParameters(new[] { "System.dll", "System.Core.dll" })
            {
                GenerateInMemory = true,
            };
            var provider = CodeDomProvider.CreateProvider("CSharp");
            var results = provider.CompileAssemblyFromSource(options, source);
            if (results.Errors.HasErrors) throw new FormatException();

            return results.CompiledAssembly.GetType("Program").GetMethod("Execute");
        }
    }
}
