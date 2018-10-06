using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEditor;

public static class FPSEditor {

	[MenuItem("FPSDemo/Test", false, 1)]
	public static void Test()
	{
		var path = EditorUtility.OpenFolderPanel("Choose directory", Application.dataPath, "Views");
		CreateScript(path, "TestForTest");
		AssetDatabase.Refresh();
	}
	
	[MenuItem("FPSDemo/Clear scene", false, 3)]
	public static void ClearScene()
	{
		foreach (var gameObject in GameObject.FindObjectsOfType<GameObject>())
		{
			GameObject.DestroyImmediate(gameObject);
		}
	}

	[MenuItem("FPSDemo/Create/Waypoints", false, 2)]
	public static void CreateWaypoints()
	{
		EditorWindow.GetWindow(typeof(FPSEditorCreateWaypointsWindow));
	}

	[MenuItem("FPSDemo/Create/Weapon", false, 2)]
	public static void CreateWeapon()
	{
		EditorWindow.GetWindow(typeof(FPSEditorCreateWeaponWindow));
	}

	public static string CreateScript(string path, string name, string nameSpace = "FPSDemo")
	{
		var version = -1;
		name = name.Replace(" ","_");
		name = name.Replace("-","_");
		var className = name;
		string creationPath;
		do
		{
			var ext = ".cs";
			if (++version != 0)
			{
				ext = version + ext;
				className = $"{name}{version}";
			}

			creationPath = Path.Combine(path, name + ext);
		} while (File.Exists(creationPath));

		var code = $"using UnityEngine;\nusing UnityEngine;\n\nnamespace {nameSpace}\n{{\n\t public class {className} : MonoBehaviour\n\t{{\n\t}}\n}}";
		using (var fs = new FileStream(creationPath, FileMode.Create))
		{
			var bytes = Encoding.ASCII.GetBytes(code);
			fs.Write(bytes, 0, bytes.Length);
			fs.Close();
		}

		return className;
	}
}
