using System;
using UnityEngine;
using UnityEngine.Serialization;
using Sedah;

// Token: 0x02000344 RID: 836
[DisallowMultipleComponent]
public class ModelLocator : MonoBehaviour
{
	// Token: 0x17000241 RID: 577
	// (get) Token: 0x060013D1 RID: 5073 RVA: 0x0005279C File Offset: 0x0005099C
	// (set) Token: 0x060013D2 RID: 5074 RVA: 0x000527A4 File Offset: 0x000509A4
	public Transform modelTransform
	{
		get
		{
			return this._modelTransform;
		}
		set
		{
			if (this._modelTransform == value)
			{
				return;
			}
			if (this.modelDestructionNotifier != null)
			{
				this.modelDestructionNotifier.subscriber = null;
				UnityEngine.Object.Destroy(this.modelDestructionNotifier);
				this.modelDestructionNotifier = null;
			}
			this._modelTransform = value;
			if (this._modelTransform)
			{
				this.modelDestructionNotifier = this._modelTransform.gameObject.AddComponent<ModelLocator.DestructionNotifier>();
				this.modelDestructionNotifier.subscriber = this;
			}
			Action<Transform> action = this.onModelChanged;
			if (action == null)
			{
				return;
			}
			action(this._modelTransform);
		}
	}

	// Token: 0x14000052 RID: 82
	// (add) Token: 0x060013D3 RID: 5075 RVA: 0x00052830 File Offset: 0x00050A30
	// (remove) Token: 0x060013D4 RID: 5076 RVA: 0x00052868 File Offset: 0x00050A68
	public event Action<Transform> onModelChanged;

	// Token: 0x060013D5 RID: 5077 RVA: 0x0005289D File Offset: 0x00050A9D
	private void Awake()
	{
		this.playerMovement = base.GetComponent<PlayerMovement>();
	}

	// Token: 0x060013D6 RID: 5078 RVA: 0x000528AB File Offset: 0x00050AAB
	public void Start()
	{
		if (this.modelTransform)
		{
			this.modelParentTransform = this.modelTransform.parent;
			if (!this.dontDetatchFromParent)
			{
				this.modelTransform.parent = null;
			}
		}
	}

	// Token: 0x060013D7 RID: 5079 RVA: 0x000528E0 File Offset: 0x00050AE0
	private void UpdateModelTransform(float deltaTime)
	{
		if (this.modelTransform && this.modelParentTransform)
		{
			Vector3 position = this.modelParentTransform.position;
			Quaternion quaternion = this.modelParentTransform.rotation;
			//this.UpdateTargetNormal();
			this.SmoothNormals(deltaTime);
			quaternion = Quaternion.FromToRotation(Vector3.up, this.currentNormal) * quaternion;
			this.modelTransform.SetPositionAndRotation(position, quaternion);
		}
	}

	// Token: 0x060013D8 RID: 5080 RVA: 0x00052950 File Offset: 0x00050B50
	private void SmoothNormals(float deltaTime)
	{
		this.currentNormal = Vector3.SmoothDamp(this.currentNormal, this.targetNormal, ref this.normalSmoothdampVelocity, 0.1f, float.PositiveInfinity, deltaTime);
	}

	// Token: 0x060013D9 RID: 5081 RVA: 0x0005297C File Offset: 0x00050B7C
	//private void UpdateTargetNormal()
	//{
	//	if (this.normalizeToFloor && this.playerMovement)
	//	{
	//		this.targetNormal = (this.playerMovement.isGrounded ? this.playerMovement.estimatedGroundNormal : Vector3.up);
	//		return;
	//	}
	//	this.targetNormal = Vector3.up;
	//}

	// Token: 0x060013DA RID: 5082 RVA: 0x000529CF File Offset: 0x00050BCF
	public void LateUpdate()
	{
		if (this.autoUpdateModelTransform)
		{
			this.UpdateModelTransform(Time.deltaTime);
		}
	}

	// Token: 0x060013DB RID: 5083 RVA: 0x000529E4 File Offset: 0x00050BE4
	private void OnDestroy()
	{
		if (this.modelTransform)
		{
			if (this.preserveModel)
			{
				if (!this.noCorpse)
				{
					//this.modelTransform.gameObject.AddComponent<Corpse>();
				}
				this.modelTransform = null;
				return;
			}
			UnityEngine.Object.Destroy(this.modelTransform.gameObject);
		}
	}

	// Token: 0x060013DC RID: 5084 RVA: 0x00052A37 File Offset: 0x00050C37
	public void OnDeathStart()
	{
		if (!this.dontReleaseModelOnDeath)
		{
			this.preserveModel = true;
		}
	}

	// Token: 0x04001198 RID: 4504
	[SerializeField]
	[Tooltip("The transform of the child gameobject which acts as the model for this entity.")]
	[FormerlySerializedAs("modelTransform")]
	private Transform _modelTransform;

	// Token: 0x04001199 RID: 4505
	private ModelLocator.DestructionNotifier modelDestructionNotifier;

	// Token: 0x0400119A RID: 4506
	[Tooltip("The transform of the child gameobject which acts as the base for this entity's model. If provided, this will be detached from the hierarchy and positioned to match this object's position.")]
	public Transform modelBaseTransform;

	// Token: 0x0400119B RID: 4507
	[Tooltip("If true, ownership of the model will not be relinquished by death.")]
	public bool dontReleaseModelOnDeath;

	// Token: 0x0400119C RID: 4508
	[Tooltip("Whether or not to update the model transforms automatically.")]
	public bool autoUpdateModelTransform = true;

	// Token: 0x0400119D RID: 4509
	[Tooltip("Forces the model to remain in hierarchy, rather that detaching on start. You usually don't want this for anything that moves.")]
	public bool dontDetatchFromParent;

	// Token: 0x0400119E RID: 4510
	private Transform modelParentTransform;

	// Token: 0x040011A0 RID: 4512
	[Tooltip("Only matters if preserveModel=true. Prevents the addition of a Corpse component to the model when this object is destroyed.")]
	public bool noCorpse;

	// Token: 0x040011A1 RID: 4513
	public bool normalizeToFloor;

	// Token: 0x040011A2 RID: 4514
	private const float normalSmoothdampTime = 0.1f;

	// Token: 0x040011A3 RID: 4515
	private Vector3 normalSmoothdampVelocity;

	// Token: 0x040011A4 RID: 4516
	private Vector3 targetNormal = Vector3.up;

	// Token: 0x040011A5 RID: 4517
	private Vector3 currentNormal = Vector3.up;

	// Token: 0x040011A6 RID: 4518
	private PlayerMovement playerMovement;

	// Token: 0x040011A7 RID: 4519
	[Tooltip("Prevents the model from being destroyed when this object is destroyed. This is rarely used, as character death states are usually responsible for snatching the model away and leaving this ModelLocator with nothing to destroy.")]
	public bool preserveModel;

	// Token: 0x02000345 RID: 837
	private class DestructionNotifier : MonoBehaviour
	{
		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x00052A76 File Offset: 0x00050C76
		// (set) Token: 0x060013DE RID: 5086 RVA: 0x00052A6D File Offset: 0x00050C6D
		public ModelLocator subscriber { private get; set; }

		// Token: 0x060013E0 RID: 5088 RVA: 0x00052A7E File Offset: 0x00050C7E
		private void OnDestroy()
		{
			if (this.subscriber != null)
			{
				this.subscriber.modelTransform = null;
			}
		}
	}
}