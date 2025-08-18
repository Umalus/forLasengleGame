using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class CharacterStatus_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/MasterData/CharacterStatus.xlsx";
	private static readonly string exportPath = "Assets/MasterData/CharacterStatus.asset";
	private static readonly string[] sheetNames = { "CharacterStatus", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			Entity_CharacterStatus data = (Entity_CharacterStatus)AssetDatabase.LoadAssetAtPath (exportPath, typeof(Entity_CharacterStatus));
			if (data == null) {
				data = ScriptableObject.CreateInstance<Entity_CharacterStatus> ();
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

					Entity_CharacterStatus.Sheet s = new Entity_CharacterStatus.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						Entity_CharacterStatus.Param p = new Entity_CharacterStatus.Param ();
						
					cell = row.GetCell(0); p.ID = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(1); p.CharacterName = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(4); p.Lv = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(5); p.HP = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(6); p.MaxHP = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(7); p.MP = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(8); p.MaxMP = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(9); p.Attack = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(10); p.Defense = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(11); p.Speed = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(12); p.CriticalPower = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(13); p.CriticalRatio = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(14); p.PowerRatio = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(15); p.MaxAddExp = (int)(cell == null ? 0 : cell.NumericCellValue);
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
