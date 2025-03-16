using System.Collections.Generic;
using UnityEngine;

namespace Game.EnemyBlock.Data
{
	[System.Serializable]
	public class EnemyGroupsData
	{
		[SerializeField] private Vector2 additionSpawnCooldownRange;
		public Vector2 AdditionSpawnCooldownRANGE => additionSpawnCooldownRange;

		[SerializeField] private Vector2 additionEnergyPerPercentRange;
		public Vector2 AdditionEnergyPerPercentRange => additionEnergyPerPercentRange;

		[SerializeField] private List<EnemyGroupData> groups;
		public List<EnemyGroupData> Groups => groups;

	}

	[System.Serializable]
	public class EnemyGroupData
	{
		[SerializeField] private List<EnemyGroup> createEnemyData;
		public List<EnemyGroup> CreateEnemyData => createEnemyData;
	}

	[System.Serializable]
	public class EnemyGroup
	{
		[SerializeField] private EnemyType enemyType;
		public EnemyType EnemyType => enemyType;

		[SerializeField] private int count;
		public int Count => count;
	}
}