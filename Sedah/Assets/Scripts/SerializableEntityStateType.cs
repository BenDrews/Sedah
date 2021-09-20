using System;
using UnityEngine;

// Token: 0x02000991 RID: 2449
[Serializable]
public struct SerializableEntityStateType
{
	// Token: 0x06003A00 RID: 14848 RVA: 0x000E3728 File Offset: 0x000E1928
	public SerializableEntityStateType(string typeName)
	{
		this._typeName = "";
		this.typeName = typeName;
	}

	// Token: 0x06003A01 RID: 14849 RVA: 0x000E373C File Offset: 0x000E193C
	public SerializableEntityStateType(Type stateType)
	{
		this._typeName = "";
		this.stateType = stateType;
	}

	// Token: 0x1700060B RID: 1547
	// (get) Token: 0x06003A02 RID: 14850 RVA: 0x000E3750 File Offset: 0x000E1950
	// (set) Token: 0x06003A03 RID: 14851 RVA: 0x000E3758 File Offset: 0x000E1958
	public string typeName
	{
		get
		{
			return this._typeName;
		}
		private set
		{
			this.stateType = Type.GetType(value);
		}
	}

	// Token: 0x1700060C RID: 1548
	// (get) Token: 0x06003A04 RID: 14852 RVA: 0x000E3768 File Offset: 0x000E1968
	// (set) Token: 0x06003A05 RID: 14853 RVA: 0x000E37A9 File Offset: 0x000E19A9
	public Type stateType
	{
		get
		{
			if (this._typeName == null)
			{
				return null;
			}
			Type type = Type.GetType(this._typeName);
			if (!(type != null) || !type.IsSubclassOf(typeof(EntityState)))
			{
				return null;
			}
			return type;
		}
		set
		{
			this._typeName = ((value != null && value.IsSubclassOf(typeof(EntityState))) ? value.AssemblyQualifiedName : "");
		}
	}

	// Token: 0x04003083 RID: 12419
	[SerializeField]
	private string _typeName;
}