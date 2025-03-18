using System.Collections.Generic;
using UnityEngine;

namespace Game.EnemyBlock.Data
{
	[CreateAssetMenu(menuName = "Data/Config", fileName = "SpawnEnemy", order = 1)]
	public class SpawnEnemyData : ScriptableObject
	{
		[SerializeField] private float baseSpawnCooldown;
		public float BaseSpawnCooldown => baseSpawnCooldown;

		[SerializeField] private List<EnemyData> _allEnemyData;
		public List<EnemyData> AllEnemyData => _allEnemyData;
		
		[SerializeField] private List<EnemyGroupsData> createdEnemyGroups;
		public List<EnemyGroupsData> CreatedEnemyGroups => createdEnemyGroups;
	}
}