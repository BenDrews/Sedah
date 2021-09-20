using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009AA RID: 2474
namespace EntityStates
{
	public class TestState1 : EntityState
	{
		// Token: 0x06003A8C RID: 14988 RVA: 0x000E634D File Offset: 0x000E454D
		public override void OnEnter()
		{
			Debug.LogFormat("{0} Entering TestState1.", new object[]
			{
					base.gameObject
			});
		}

		// Token: 0x06003A8D RID: 14989 RVA: 0x000E6368 File Offset: 0x000E4568
		public override void OnExit()
		{
			Debug.LogFormat("{0} Exiting TestState1.", new object[]
			{
					base.gameObject
			});
		}

		// Token: 0x06003A8E RID: 14990 RVA: 0x000E6383 File Offset: 0x000E4583
		public override void FixedUpdate()
		{
			if (Input.GetButton("Fire1"))
			{
				this.outer.SetNextState(new TestState2());
			}
		}
	}
}
