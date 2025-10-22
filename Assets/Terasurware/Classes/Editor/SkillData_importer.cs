using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class SkillData_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Resources/MasterData/SkillData.xlsx";
	private static readonly string exportPath = "Assets/Resources/MasterData/SkillData.asset";
	private static readonly string[] sheetNames = { "SkillData", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			Entity_SkillData data = (Entity_SkillData)AssetDatabase.LoadAssetAtPath (exportPath, typeof(Entity_SkillData));
			if (data == null) {
				data = ScriptableObject.CreateInstance<Entity_SkillData> ();
				AssetDatabase.CreateAsset ((ScriptableObject)data, exportPath);
				data.hideFlags = HideFlags.NotEditable;
			}
			
			data.sheets.Clear ();
			using (FileStream stream = File.Open (filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
				IWorkbook book = null;
				if (Path.GetExtension (filePath) == ".xls") {
					book = new HSSFWorkbook(stream);
				} else {
					book = new XSSFWorkbook(stream);
				}
				
				foreach(string sheetName in sheetNames) {
					ISheet sheet = book.GetSheet(sheetName);
					if( sheet == null ) {
						Debug.LogError("[QuestData] sheet not found:" + sheetName);
						continue;
					}

					Entity_SkillData.Sheet s = new Entity_SkillData.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						Entity_SkillData.Param p = new Entity_SkillData.Param ();
						
					cell = row.GetCell(0); p.ID = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(1); p.name = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(2); p.Lv = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(3); p.NeedMP = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(4); p.rangeType = (int)(cell == null ? 0 : cell.NumericCellValue);
					p.effectID = new int[4];
					cell = row.GetCell(6); p.effectID[0] = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(7); p.effectID[1] = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(8); p.effectID[2] = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(9); p.effectID[3] = (int)(cell == null ? 0 : cell.NumericCellValue);
						s.list.Add (p);
					}
					data.sheets.Add(s);
				}
			}

			ScriptableObject obj = AssetDatabase.LoadAssetAtPath (exportPath, typeof(ScriptableObject)) as ScriptableObject;
			EditorUtility.SetDirty (obj);
		}
	}
}
