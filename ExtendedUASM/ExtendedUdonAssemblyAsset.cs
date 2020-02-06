using System;
using System.Collections.Generic;
using UnityEngine;
using VRC.Udon.EditorBindings;
using VRC.Udon.UAssembly.Assembler;
using VRC.Udon.UAssembly.Interfaces;

[assembly: UdonProgramSourceNewMenu(typeof(ExtendedUdonAssemblyAsset), "Extended Udon Assembly Program Asset")]
[CreateAssetMenu(fileName = "ExtendedUdonAssemblyAsset", menuName = "VRChat/Udon/Extended Udon Assembly Program Asset")]
public class ExtendedUdonAssemblyAsset : UdonAssemblyProgramAsset
{
    TypeResolverGroup typeResolver = new TypeResolverGroup(new List<IUAssemblyTypeResolver>()
    {
        new SystemTypeResolver(),
        new UnityEngineTypeResolver(),
        new VRCSDK2TypeResolver(),
        new UdonTypeResolver(),
        new ExceptionTypeResolver(),
        new UdonBehaviourTypeResolver(),
    });
    
    protected override void DoRefreshProgramActions()
    {
        base.DoRefreshProgramActions();

        if (program != null)
        {
            var lines = udonAssembly.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            bool started = false;
            var stack = new Stack<object>();
            foreach (var line in lines)
            {
                if (!started && line != "#.INIT_START") //Find start
                    continue;
                started = true;

                if (line == "#.INIT_START")
                    continue;
                if (line == "#.INIT_END")
                    break;

                var rawLine = line.TrimStart('#');
                var instructionArgs = rawLine.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                if (instructionArgs.Length < 1)
                    continue;
                var instruction = instructionArgs[0];

                switch (instruction)
                {
                    case "PUSH":
                        HandlePushInstruction(rawLine, instructionArgs, stack, typeResolver);
                        break;
                    case "PUSHENUM":
                        HandlePushEnumInstruction(rawLine, instructionArgs, stack, typeResolver);
                        break;
                    case "CONSTRUCT":
                        HandleConstructInstruction(rawLine, instructionArgs, stack, typeResolver);
                        break;
                    case "SETHEAP":
                        HandleSetHeapInstruction(rawLine, instructionArgs, stack, typeResolver);
                        break;
                    default:
                        throw new InvalidOperationException($"Unknown instruction '{instruction}'");
                }
            }
        }
    }

    private void HandleSetHeapInstruction(string rawLine, string[] instructionArgs, Stack<object> stack,
        TypeResolverGroup typeResolver)
    {
        if (instructionArgs.Length != 2)
            throw new InvalidOperationException(
                $"SETHEAP instruction requires 1 arguments(AssemblyVariableName). EX: 'SETHEAP \"variableName\"'");
        var variableName = instructionArgs[1].Replace("\"", "");
        var addr = program.SymbolTable.GetAddressFromSymbol(variableName);
        var data = stack.Pop();
        if (data is Type)
            program.Heap.SetHeapVariable(addr, data, typeof(Type)); //Prevents .GetType from getting RuntimeType
        else
            program.Heap.SetHeapVariable(addr, data, data.GetType());
    }

    private void HandleConstructInstruction(string rawLine, string[] instructionArgs, Stack<object> stack,
        TypeResolverGroup typeResolver)
    {
        if (instructionArgs.Length != 3)
            throw new InvalidOperationException(
                $"CONSTRUCT instruction requires 2 arguments(TypeName, ArgumentCount). EX: 'CONSTRUCT UnityEngineVector2 2'");
        var typeName = instructionArgs[1];
        var constructorArgumentcount = int.Parse(instructionArgs[2]);

        Stack<object> flipped = new Stack<object>();
        for (int i = 0; i < constructorArgumentcount; i++)
        {
            flipped.Push(stack.Pop());
        }

        object obj = Activator.CreateInstance(typeResolver.GetTypeFromTypeString(typeName), flipped.ToArray());
        stack.Push(obj);
    }

    private void HandlePushInstruction(string rawLine, string[] instructionArgs, Stack<object> stack,
        TypeResolverGroup typeResolver)
    {
        if (instructionArgs.Length < 3)
            throw new InvalidOperationException(
                $"PUSH instruction requires 2 arguments(TypeName, literal). EX: 'PUSH SystemSingle 5'");
        var typeName = instructionArgs[1];
        var literal = rawLine.Substring(rawLine.IndexOf(typeName) + typeName.Length + 1);

        object output = null;
        switch (typeName)
        {
            case "SystemSingle":
                output = Single.Parse(literal);
                break;
            case "SystemDouble":
                output = Double.Parse(literal);
                break;
            case "SystemInt64":
                output = Int64.Parse(literal);
                break;
            case "SystemInt32":
                output = Int32.Parse(literal);
                break;
            case "SystemInt16":
                output = Int16.Parse(literal);
                break;
            case "SystemUInt64":
                output = UInt64.Parse(literal);
                break;
            case "SystemUInt32":
                output = UInt32.Parse(literal);
                break;
            case "SystemUInt16":
                output = UInt16.Parse(literal);
                break;
            case "SystemString":
                output = literal.Trim('"');
                break;
            case "SystemBoolean":
                output = Boolean.Parse(literal);
                break;
            case "SystemChar":
                output = char.Parse(literal);
                break;
            case "SystemByte":
                output = byte.Parse(literal);
                break;
            case "SystemSByte":
                output = sbyte.Parse(literal);
                break;
            case "SystemType":
                output = typeResolver.GetTypeFromTypeString(literal);
                if (output == null)
                    throw new InvalidOperationException(
                        $"Unable to find type '{literal}'.");
                break;
            default:
                throw new InvalidOperationException($"PUSH literal unsupported for type {typeName}");
        }

        stack.Push(output);
    }

    private void HandlePushEnumInstruction(string rawLine, string[] instructionArgs, Stack<object> stack,
        TypeResolverGroup typeResolver)
    {
        if (instructionArgs.Length != 3)
            throw new InvalidOperationException(
                $"PUSHENUM requires 2 arguments(EnumType, EnumValueName). EX: 'PUSHENUM UnityEngineFFTWindow Rectangular'");
        var enumName = instructionArgs[1];
        var enumValueName = instructionArgs[2];

        var enumType = typeResolver.GetTypeFromTypeString(enumName);
        if (enumType == null)
            throw new InvalidOperationException(
                $"Unable to find type '{enumName}'.");
        object output = Enum.Parse(enumType, enumValueName);
        
        stack.Push(output);
    }
}