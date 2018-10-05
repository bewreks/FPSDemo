using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public static class FPSEditor {

	[MenuItem("FPSDemo/Test", false)]
	public static void Test()
	{
		var path = EditorUtility.OpenFolderPanel("Choose directory", Application.dataPath, "Views");
		CreateScript(path, "TestForTest");
	}

	private static void CreateScript(string path, string name, string nameSpace = "FPSDemo")
	{
		var version = 0;
		name = name.Replace(" ","_");
		name = name.Replace("-","_");
		string creationPath;
		do
		{
			var ext = ".cs";
			if (version != 0)
			{
				ext = version++ + ext;
			}
			creationPath = Path.Combine(path, name + ext);
		} while (!File.Exists(creationPath));

		using (var file = new StreamWriter(creationPath))
		{
			file.WriteLine("using UnityEngine;");
			file.WriteLine("using System.Collections;");
			file.WriteLine("");
			file.WriteLine($"namespace {nameSpace} ");
			file.WriteLine("{");
			file.WriteLine("");
			file.WriteLine($"\t public class {name} : MonoBehaviour");
			file.WriteLine("{");
			file.WriteLine("");
			file.WriteLine("}");
			file.WriteLine("}");
		}
	}
}
