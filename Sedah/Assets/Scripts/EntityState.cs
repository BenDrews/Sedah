using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

// Token: 0x02000990 RID: 2448
public class EntityState
{
	// Token: 0x170005F3 RID: 1523
	// (get) Token: 0x060039CD RID: 14797 RVA: 0x000E31C2 File Offset: 0x000E13C2
	// (set) Token: 0x060039CE RID: 14798 RVA: 0x000E31CA File Offset: 0x000E13CA
	protected float age { get; set; }

	// Token: 0x170005F4 RID: 1524
	// (get) Token: 0x060039CF RID: 14799 RVA: 0x000E31D3 File Offset: 0x000E13D3
	// (set) Token: 0x060039D0 RID: 14800 RVA: 0x000E31DB File Offset: 0x000E13DB
	protected float fixedAge { get; set; }

	// Token: 0x060039D1 RID: 14801 RVA: 0x000E31E4 File Offset: 0x000E13E4
	public EntityState()
	{
		//EntityStateCatalog.InitializeStateFields(this);
	}

	// Token: 0x060039D2 RID: 14802 RVA: 0x00004381 File Offset: 0x00002581
	public virtual void OnEnter()
	{
	}

	// Token: 0x060039D3 RID: 14803 RVA: 0x00004381 File Offset: 0x00002581
	public virtual void OnExit()
	{
	}

	// Token: 0x060039D4 RID: 14804 RVA: 0x000E31F2 File Offset: 0x000E13F2
	public virtual void Update()
	{
		this.age += Time.deltaTime;
	}

	// Token: 0x060039D5 RID: 14805 RVA: 0x000E3206 File Offset: 0x000E1406
	public virtual void FixedUpdate()
	{
		this.fixedAge += Time.fixedDeltaTime;
	}

	// Token: 0x060039D6 RID: 14806 RVA: 0x00004381 File Offset: 0x00002581
	public virtual void OnSerialize(NetworkWriter writer)
	{
	}

	// Token: 0x060039D7 RID: 14807 RVA: 0x00004381 File Offset: 0x00002581
	public virtual void OnDeserialize(NetworkReader reader)
	{
	}

	// Token: 0x060039D8 RID: 14808 RVA: 0x0001799C File Offset: 0x00015B9C
	public virtual InterruptPriority GetMinimumInterruptPriority()
	{
		return InterruptPriority.Any;
	}

	// Token: 0x170005F5 RID: 1525
	// (get) Token: 0x060039D9 RID: 14809 RVA: 0x000E321A File Offset: 0x000E141A
	protected GameObject gameObject
	{
		get
		{
			return this.outer.gameObject;
		}
	}

	// Token: 0x060039DA RID: 14810 RVA: 0x000E3227 File Offset: 0x000E1427
	protected static void Destroy(UnityEngine.Object obj)
	{
		UnityEngine.Object.Destroy(obj);
	}

	// Token: 0x060039DB RID: 14811 RVA: 0x000E322F File Offset: 0x000E142F
	protected T GetComponent<T>() where T : Component
	{
		return this.outer.GetComponent<T>();
	}


	// Token: 0x060039DD RID: 14813 RVA: 0x000E324A File Offset: 0x000E144A
	protected Component GetComponent(string type)
	{
		return this.outer.GetComponent(type);
	}

	// Token: 0x170005F9 RID: 1529
	// (get) Token: 0x060039E1 RID: 14817 RVA: 0x000E32B6 File Offset: 0x000E14B6
	protected Transform transform
	{
		get
		{
			return this.outer.commonComponents.transform;
		}
	}

	// Token: 0x170005FA RID: 1530
	// (get) Token: 0x060039E2 RID: 14818 RVA: 0x000E32C8 File Offset: 0x000E14C8
	protected CharacterObject characterObject
	{
		get
		{
			return this.outer.commonComponents.characterObject;
		}
	}


	// Token: 0x170005FD RID: 1533
	// (get) Token: 0x060039E5 RID: 14821 RVA: 0x000E32FE File Offset: 0x000E14FE
	protected Rigidbody rigidbody
	{
		get
		{
			return this.outer.commonComponents.rigidbody;
		}
	}


	// Token: 0x17000601 RID: 1537
	// (get) Token: 0x060039E9 RID: 14825 RVA: 0x000E3346 File Offset: 0x000E1546
	protected ModelLocator modelLocator
	{
		get
		{
			return this.outer.commonComponents.modelLocator;
		}
	}

	// Token: 0x17000603 RID: 1539
	// (get) Token: 0x060039EB RID: 14827 RVA: 0x000E336A File Offset: 0x000E156A
	//protected TeamComponent teamComponent
	//{
	//	get
	//	{
	//		return this.outer.commonComponents.teamComponent;
	//	}
	//}

	// Token: 0x17000604 RID: 1540
	// (get) Token: 0x060039EC RID: 14828 RVA: 0x000E337C File Offset: 0x000E157C
	protected Health healthComponent
	{
		get
		{
			return this.outer.commonComponents.health;
		}
	}


	// Token: 0x060039F3 RID: 14835 RVA: 0x000E33FA File Offset: 0x000E15FA
	protected Transform GetModelBaseTransform()
	{
		if (!this.modelLocator)
		{
			return null;
		}
		return this.modelLocator.modelBaseTransform;
	}

	// Token: 0x060039F4 RID: 14836 RVA: 0x000E3416 File Offset: 0x000E1616
	protected Transform GetModelTransform()
	{
		if (!this.modelLocator)
		{
			return null;
		}
		return this.modelLocator.modelTransform;
	}


	// Token: 0x060039F6 RID: 14838 RVA: 0x000E3465 File Offset: 0x000E1665
	protected Animator GetModelAnimator()
	{
		if (this.modelLocator && this.modelLocator.modelTransform)
		{
			return this.modelLocator.modelTransform.GetComponent<Animator>();
		}
		return null;
	}

	// Token: 0x060039F9 RID: 14841 RVA: 0x000E3500 File Offset: 0x000E1700
	protected void PlayAnimation(string layerName, string animationStateName, string playbackRateParam, float duration)
	{
		if (duration <= 0f)
		{
			Debug.LogWarningFormat("EntityState.PlayAnimation: Zero duration is not allowed. type={0}", new object[]
			{
					base.GetType().Name
			});
			return;
		}
		Animator modelAnimator = this.GetModelAnimator();
		if (modelAnimator)
		{
			EntityState.PlayAnimationOnAnimator(modelAnimator, layerName, animationStateName, playbackRateParam, duration);
		}
	}

	// Token: 0x060039FA RID: 14842 RVA: 0x000E3550 File Offset: 0x000E1750
	protected static void PlayAnimationOnAnimator(Animator modelAnimator, string layerName, string animationStateName, string playbackRateParam, float duration)
	{
		modelAnimator.speed = 1f;
		modelAnimator.Update(0f);
		int layerIndex = modelAnimator.GetLayerIndex(layerName);
		modelAnimator.SetFloat(playbackRateParam, 1f);
		modelAnimator.PlayInFixedTime(animationStateName, layerIndex, 0f);
		modelAnimator.Update(0f);
		float length = modelAnimator.GetCurrentAnimatorStateInfo(layerIndex).length;
		modelAnimator.SetFloat(playbackRateParam, length / duration);
	}

	// Token: 0x060039FB RID: 14843 RVA: 0x000E35BC File Offset: 0x000E17BC
	protected void PlayCrossfade(string layerName, string animationStateName, string playbackRateParam, float duration, float crossfadeDuration)
	{
		if (duration <= 0f)
		{
			Debug.LogWarningFormat("EntityState.PlayCrossfade: Zero duration is not allowed. type={0}", new object[]
			{
					base.GetType().Name
			});
			return;
		}
		Animator modelAnimator = this.GetModelAnimator();
		if (modelAnimator)
		{
			modelAnimator.speed = 1f;
			modelAnimator.Update(0f);
			int layerIndex = modelAnimator.GetLayerIndex(layerName);
			modelAnimator.SetFloat(playbackRateParam, 1f);
			modelAnimator.CrossFadeInFixedTime(animationStateName, crossfadeDuration, layerIndex);
			modelAnimator.Update(0f);
			float length = modelAnimator.GetNextAnimatorStateInfo(layerIndex).length;
			modelAnimator.SetFloat(playbackRateParam, length / duration);
		}
	}

	// Token: 0x060039FC RID: 14844 RVA: 0x000E365C File Offset: 0x000E185C
	protected void PlayCrossfade(string layerName, string animationStateName, float crossfadeDuration)
	{
		Animator modelAnimator = this.GetModelAnimator();
		if (modelAnimator)
		{
			modelAnimator.speed = 1f;
			modelAnimator.Update(0f);
			int layerIndex = modelAnimator.GetLayerIndex(layerName);
			modelAnimator.CrossFadeInFixedTime(animationStateName, crossfadeDuration, layerIndex);
		}
	}

	// Token: 0x060039FD RID: 14845 RVA: 0x000E36A0 File Offset: 0x000E18A0
	public void PlayAnimation(string layerName, string animationStateName)
	{
		Animator modelAnimator = this.GetModelAnimator();
		if (modelAnimator)
		{
			EntityState.PlayAnimationOnAnimator(modelAnimator, layerName, animationStateName);
		}
	}

	// Token: 0x060039FE RID: 14846 RVA: 0x000E36C4 File Offset: 0x000E18C4
	protected static void PlayAnimationOnAnimator(Animator modelAnimator, string layerName, string animationStateName)
	{
		int layerIndex = modelAnimator.GetLayerIndex(layerName);
		modelAnimator.speed = 1f;
		modelAnimator.Update(0f);
		modelAnimator.PlayInFixedTime(animationStateName, layerIndex, 0f);
	}

	// Token: 0x04003080 RID: 12416
	public EntityStateMachine outer;
}