using Game.EnemyBlock.View;
using UnityEngine;

namespace Game.EnemyBlock.Data
{
	[CreateAssetMenu(menuName = "Data/Enemy", fileName = "EnemyData", order = 0)]
	public class EnemyData : ScriptableObject
	{
		[SerializeField] private EnemyType enemyType;
		public EnemyType EnemyType => enemyType;

		[SerializeField] private float damage;
		public float Damage => damage;

		[SerializeField] private Vector2 attackCooldownRange;
		public Vector2 AttackCooldownRange => attackCooldownRange;

		[SerializeField] private Vector2 projectilePerAttackRange;
		public Vector2 ProjectilePerAttackRange => projectilePerAttackRange;

		[SerializeField] private float projectileSpeed;
		public float ProjectileSpeed => projectileSpeed;

		[SerializeField] private Vector2 moveSpeedRange;
		public Vector2 MoveSpeedRange => moveSpeedRange;

		[Header("Кол-во жидкости при поглощении")]
		[SerializeField] private Vector2 energyPerPercentRange;

		public Vector2 EnergyPerPercentRange => energyPerPercentRange;

		[Header("View")]
		[SerializeField] private EnemyView prefab;

		public EnemyView Prefab => prefab;
	}
}