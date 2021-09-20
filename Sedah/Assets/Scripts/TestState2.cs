using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009AB RID: 2475
namespace EntityStates
{
	public class TestState2 : EntityState
	{
		// Token: 0x06003A90 RID: 14992 RVA: 0x000E63A9 File Offset: 0x000E45A9
		public override void OnEnter()
		{
			Debug.LogFormat("{0} Entering TestState2.", new object[]
			{
					base.gameObject
			});
		}

		// Token: 0x06003A91 RID: 14993 RVA: 0x000E63C4 File Offset: 0x000E45C4
		public override void OnExit()
		{
			Debug.LogFormat("{0} Exiting TestState2.", new object[]
			{
					base.gameObject
			});
		}

		// Token: 0x06003A92 RID: 14994 RVA: 0x000E63DF File Offset: 0x000E45DF
		public override void FixedUpdate()
		{
			if (Input.GetButton("Fire2"))
			{
				this.outer.SetNextState(new TestState1());
			}
		}
	}
}