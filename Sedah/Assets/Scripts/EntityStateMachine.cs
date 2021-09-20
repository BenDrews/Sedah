using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using EntityStates;
public enum InterruptPriority
{
	// Token: 0x04003079 RID: 12409
	Any,
	// Token: 0x0400307A RID: 12410
	Skill,
	// Token: 0x0400307B RID: 12411
	PrioritySkill,
	// Token: 0x0400307C RID: 12412
	Pain,
	// Token: 0x0400307D RID: 12413
	Frozen,
	// Token: 0x0400307E RID: 12414
	Vehicle,
	// Token: 0x0400307F RID: 12415
	Death
}

public class EntityStateMachine : NetworkBehaviour
{
	// Token: 0x17000175 RID: 373
	// (get) Token: 0x06000D4B RID: 3403 RVA: 0x00036A5F File Offset: 0x00034C5F
	// (set) Token: 0x06000D4C RID: 3404 RVA: 0x00036A67 File Offset: 0x00034C67
	public EntityState state { get; private set; }

	// Token: 0x17000177 RID: 375
	// (get) Token: 0x06000D4F RID: 3407 RVA: 0x00036A81 File Offset: 0x00034C81
	// (set) Token: 0x06000D50 RID: 3408 RVA: 0x00036A89 File Offset: 0x00034C89
	public NetworkIdentity networkIdentity { get; private set; }

	// Token: 0x06000D51 RID: 3409 RVA: 0x00036A92 File Offset: 0x00034C92
	public void SetNextState(EntityState newNextState)
	{
		this.nextState = newNextState;
	}

	// Token: 0x06000D52 RID: 3410 RVA: 0x00036A9B File Offset: 0x00034C9B
	public void SetNextStateToMain()
	{
		//this.nextState = EntityStateCatalog.InstantiateState(this.mainStateType);
	}

	// Token: 0x06000D53 RID: 3411 RVA: 0x00036AAE File Offset: 0x00034CAE
	public bool CanInterruptState(InterruptPriority interruptPriority)
	{
		return (this.nextState ?? this.state).GetMinimumInterruptPriority() <= interruptPriority;
	}

	// Token: 0x06000D54 RID: 3412 RVA: 0x00036ACB File Offset: 0x00034CCB
	public bool SetInterruptState(EntityState newNextState, InterruptPriority interruptPriority)
	{
		if (this.CanInterruptState(interruptPriority))
		{
			this.nextState = newNextState;
			return true;
		}
		return false;
	}

	// Token: 0x06000D55 RID: 3413 RVA: 0x00036AE0 File Offset: 0x00034CE0
	public bool HasPendingState()
	{
		return this.nextState != null;
	}

	// Token: 0x06000D56 RID: 3414 RVA: 0x00036AEC File Offset: 0x00034CEC
	public void SetState(EntityState newState)
	{
		this.nextState = null;
		newState.outer = this;
		if (this.state == null)
		{
			Debug.LogErrorFormat("State machine {0} on object {1} does not have a state!", new object[]
			{
					this.customName,
					base.gameObject
			});
		}
		this.state.OnExit();
		this.state = newState;
		this.state.OnEnter();
	}

	// Token: 0x06000D57 RID: 3415 RVA: 0x00036B98 File Offset: 0x00034D98
	private void Awake()
	{
		this.networkIdentity = base.GetComponent<NetworkIdentity>();
		this.commonComponents = new EntityStateMachine.CommonComponentCache(base.gameObject);
		this.state = new Uninitialized();
		this.state.outer = this;
	}

	// Token: 0x06000D58 RID: 3416 RVA: 0x00036BE8 File Offset: 0x00034DE8
	private void Start()
	{
		if (this.nextState != null)
		{
			this.SetState(this.nextState);
			return;
		}
		Type stateType = this.initialStateType.stateType;
		//this.SetState(EntityStateCatalog.InstantiateState(stateType));
	}

	// Token: 0x06000D59 RID: 3417 RVA: 0x00036C64 File Offset: 0x00034E64
	public void Update()
	{
		this.state.Update();
	}

	// Token: 0x06000D5A RID: 3418 RVA: 0x00036C71 File Offset: 0x00034E71
	public void FixedUpdate()
	{
		if (this.nextState != null)
		{
			this.SetState(this.nextState);
		}
		this.state.FixedUpdate();
	}

	// Token: 0x17000178 RID: 376
	// (get) Token: 0x06000D5B RID: 3419 RVA: 0x00036C92 File Offset: 0x00034E92
	// (set) Token: 0x06000D5C RID: 3420 RVA: 0x00036C9A File Offset: 0x00034E9A
	public bool destroying { get; private set; }

	// Token: 0x06000D5D RID: 3421 RVA: 0x00036CA3 File Offset: 0x00034EA3
	private void OnDestroy()
	{
		this.destroying = true;
		if (this.state != null)
		{
			this.state.OnExit();
			this.state = null;
		}
	}

	// Token: 0x06000D5E RID: 3422 RVA: 0x00036CC8 File Offset: 0x00034EC8
	private void OnValidate()
	{
		if (this.mainStateType.stateType == null)
		{
			//if (this.customName == "Body")
			//{
			//	if (base.GetComponent<CharacterMotor>())
			//	{
			//		this.mainStateType = new SerializableEntityStateType(typeof(GenericCharacterMain));
			//		return;
			//	}
			//	if (base.GetComponent<RigidbodyMotor>())
			//	{
			//		this.mainStateType = new SerializableEntityStateType(typeof(FlyState));
			//		return;
			//	}
			//}
		}
	}

	// Token: 0x06000D5F RID: 3423 RVA: 0x00036D98 File Offset: 0x00034F98
	public static EntityStateMachine FindByCustomName(GameObject gameObject, string customName)
	{
		List<EntityStateMachine> gameObjectComponents = GetComponentsCache<EntityStateMachine>.GetGameObjectComponents(gameObject);
		EntityStateMachine result = null;
		int i = 0;
		int count = gameObjectComponents.Count;
		while (i < count)
		{
			if (string.CompareOrdinal(customName, gameObjectComponents[i].customName) == 0)
			{
				result = gameObjectComponents[i];
				break;
			}
			i++;
		}
		GetComponentsCache<EntityStateMachine>.ReturnBuffer(gameObjectComponents);
		return result;
	}

	// Token: 0x04000CC3 RID: 3267
	private EntityState nextState;

	// Token: 0x04000CC4 RID: 3268
	[Tooltip("The name of this state machine.")]
	public string customName;

	// Token: 0x04000CC5 RID: 3269
	[Tooltip("The type of the state to enter when this component is first activated.")]
	public SerializableEntityStateType initialStateType = new SerializableEntityStateType(typeof(TestState1));

	// Token: 0x04000CC6 RID: 3270
	[Tooltip("The preferred main state of this state machine.")]
	public SerializableEntityStateType mainStateType;

	// Token: 0x04000CC9 RID: 3273
	public EntityStateMachine.CommonComponentCache commonComponents;

	// Token: 0x04000CCA RID: 3274
	public int networkIndex = -1;

	// Token: 0x02000276 RID: 630
	public struct CommonComponentCache
	{
		// Token: 0x06000D61 RID: 3425 RVA: 0x00036E0C File Offset: 0x0003500C
		public CommonComponentCache(GameObject gameObject)
		{
			this.transform = gameObject.transform;
			this.characterObject = gameObject.GetComponent<CharacterObject>();
			this.rigidbody = gameObject.GetComponent<Rigidbody>();
			this.health = gameObject.GetComponent<Health>();
			this.modelLocator = gameObject.GetComponent<ModelLocator>();
		}

		// Token: 0x04000CCC RID: 3276
		public readonly Transform transform;

		// Token: 0x04000CCD RID: 3277
		public readonly CharacterObject characterObject;

		// Token: 0x04000CD0 RID: 3280
		public readonly Rigidbody rigidbody;

		public readonly Health health;

		public readonly ModelLocator modelLocator;

	}
}
