﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Speedrunner.Activities
{
    [DebuggerDisplay(@"\{{GetType().Name}: {Text}\}")]
    public class Expression : Activity
    {
        static readonly Regex ExpressionPattern = new Regex(@"^\s*(.+?)\s*\S?=.+$");

        [DefaultValue("")]
        public string Text { get; set; } = "";

        MethodInfo action;

        public override void Execute(WorkflowContext context)
        {
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

            return results.CompiledAssembly.GetType("Program").GetMethod("Execute");
        }
    }
}
