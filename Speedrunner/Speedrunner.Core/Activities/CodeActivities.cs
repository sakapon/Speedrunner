using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace Speedrunner.Activities
{
    [DebuggerDisplay(@"\{{GetType().Name}: {Text}\}")]
    public class Expression : Activity
    {
        static readonly Regex ExpressionPattern = new Regex(@"^\s*(.+?)\s*\S?=.+$");

        [DefaultValue("")]
        public string Text { get; set; } = "";

        public override void Execute(WorkflowContext context)
        {
            var match = ExpressionPattern.Match(Text);
            if (!match.Success) throw new FormatException();

            var leftVarName = match.Groups[1].Value;
            var isLeftVarContained = context.Variables.Contains(leftVarName);

            var leftVarIndex = isLeftVarContained ? Array.IndexOf(context.Variables.Dictionary.Keys.ToArray(), leftVarName) : context.Variables.Count;

            var script_vars = string.Join(", ", context.Variables.Select(v => $"{(v.VariableName == leftVarName ? "ref " : "")}{v.Type.FullName} {v.VariableName}"));
            if (!isLeftVarContained) script_vars += $", ref object {leftVarName}";
            var source = $@"
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

            var action = results.CompiledAssembly.GetType("Program").GetMethod("Execute");
            var args = context.Variables.Select(v => v.ActualValue).ToArray();
            if (!isLeftVarContained) args = args.Concat(new object[] { null }).ToArray();
            action.Invoke(null, args);

            var leftVar = context.Variables.Get(leftVarName, args[leftVarIndex].GetType());
            leftVar.ActualValue = args[leftVarIndex];
        }
    }
}
