using System.Collections;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;
[ScriptedImporter(1, "euasm")]
[UsedImplicitly]
public class ExtendedUdonAssemblyAssetImporter : ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext ctx)
    {
        ExtendedUdonAssemblyAsset udonAssemblyProgramAsset = ScriptableObject.CreateInstance<ExtendedUdonAssemblyAsset>();
        SerializedObject serializedUdonAssemblyProgramAsset = new SerializedObject(udonAssemblyProgramAsset);
        SerializedProperty udonAssemblyProperty = serializedUdonAssemblyProgramAsset.FindProperty("udonAssembly");
        udonAssemblyProperty.stringValue = File.ReadAllText(ctx.assetPath);
        serializedUdonAssemblyProgramAsset.ApplyModifiedProperties();

        udonAssemblyProgramAsset.RefreshProgram();

        ctx.AddObjectToAsset("Imported Udon Assembly Program", udonAssemblyProgramAsset);
        ctx.SetMainObject(udonAssemblyProgramAsset);
    }
}