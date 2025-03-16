using Game.EnemyBlock.Data;
using UnityEngine;

namespace Game.EnemyBlock.Controllers
{
	public class EnemySpawner : MonoBehaviour
	{
		public EnemyData[] enemyDataArray; // Массив ScriptableObject для каждого типа врага
		public float spawnInterval = 2.0f; // Интервал спавна
		private EnemyFactory enemyFactory;

		private void Start()
		{
			enemyFactory = new EnemyFactory();
			InvokeRepeating(nameof(SpawnEnemy), 0, spawnInterval);
		}

		private void SpawnEnemy()
		{
			int randomIndex = Random.Range(0, enemyDataArray.Length);
			enemyFactory.CreateEnemy(enemyDataArray[randomIndex]);
		}
	}
}