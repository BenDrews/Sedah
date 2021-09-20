using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000489 RID: 1161
public static class GetComponentsCache<T>
{
	// Token: 0x06001D47 RID: 7495 RVA: 0x00076F2B File Offset: 0x0007512B
	public static void ReturnBuffer(List<T> buffer)
	{
		buffer.Clear();
		GetComponentsCache<T>.buffers.Push(buffer);
	}

	// Token: 0x06001D48 RID: 7496 RVA: 0x00076F3E File Offset: 0x0007513E
	private static List<T> RequestBuffer()
	{
		if (GetComponentsCache<T>.buffers.Count == 0)
		{
			return new List<T>();
		}
		return GetComponentsCache<T>.buffers.Pop();
	}

	// Token: 0x06001D49 RID: 7497 RVA: 0x00076F5C File Offset: 0x0007515C
	public static List<T> GetGameObjectComponents(GameObject gameObject)
	{
		List<T> list = GetComponentsCache<T>.RequestBuffer();
		gameObject.GetComponents<T>(list);
		return list;
	}

	// Token: 0x06001D4A RID: 7498 RVA: 0x00076F78 File Offset: 0x00075178
	public static List<T> GetGameObjectComponentsInChildren(GameObject gameObject, bool includeInactive = false)
	{
		List<T> list = GetComponentsCache<T>.RequestBuffer();
		gameObject.GetComponentsInChildren<T>(includeInactive, list);
		return list;
	}

	// Token: 0x0400192D RID: 6445
	private static readonly Stack<List<T>> buffers = new Stack<List<T>>();
}
