using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity_SkillData : ScriptableObject
{	
	public List<Sheet> sheets = new List<Sheet> ();

	[System.SerializableAttribute]
	public class Sheet
	{
		public string name = string.Empty;
		public List<Param> list = new List<Param>();
	}

	[System.SerializableAttribute]
	public class Param
	{
		
		public int ID;
		public string name;
		public int Lv;
		public int rangeType;
		public int[] effectID;
	}
}

