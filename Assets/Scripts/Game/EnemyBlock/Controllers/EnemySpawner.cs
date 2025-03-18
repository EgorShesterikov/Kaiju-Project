using System.Linq;
using Cysharp.Threading.Tasks;
using Game.EnemyBlock.Data;
using Game.EnemyBlock.View;
using UnityEngine;
using Zenject;

namespace Game.EnemyBlock.Controllers
{
	public class EnemySpawner : MonoBehaviour
	{
		[Inject] private IUnitFactory<EnemyView, EnemyData> _enemyFactory;

		[SerializeField] private Transform robotTransform;
		[SerializeField] private SpawnEnemyData _enemyData;

		private UnitPool _pool = new UnitPool();
		private float _delaySpawned;

		private void Start()
		{
			_delaySpawned = _enemyData.BaseSpawnCooldown;
			InitSpawn();
		}

		//TODO NEED USE INFO FROM PLAYER
		private float GetCurrentPlayerHP()
		{
			return Random.Range(0, 20f);
		}

		private void InitSpawn()
		{
			SpawnEnemy().Forget();
		}

		private async UniTaskVoid SpawnEnemy()
		{
			while (true)
			{
				await UniTask.WaitForSeconds(_delaySpawned);
				Spawned();
			}
		}

		private void Spawned()
		{
			var hp = GetCurrentPlayerHP();

			EnemyGroupsData groups = null;
			for (int i = _enemyData.CreatedEnemyGroups.Count - 1; i >= 0; i--)
			{
				var emnemy = _enemyData.CreatedEnemyGroups[i];

				if (hp >= emnemy.PlayerHPRange.x && hp <= emnemy.PlayerHPRange.y)
				{
					groups = emnemy;
					break;
				}
			}
			// var groups = _enemyData.CreatedEnemyGroups.FirstOrDefault(x =>
			// 	x.PlayerHPRange.x >= hp && x.PlayerHPRange.y <= hp);

			if (groups == null)
			{
				Debug.LogError($"[EnemySpawner] group for player hp range {hp} not found");
				return;
			}

			_delaySpawned = _enemyData.BaseSpawnCooldown +
			                Random.Range(groups.AdditionSpawnCooldownRange.x, groups.AdditionSpawnCooldownRange.y);

			float bonusEnergy = Random.Range(groups.AdditionEnergyPerPercentRange.x,
				groups.AdditionEnergyPerPercentRange.y);

			var selectedGroup = groups.Groups[Random.Range(0, groups.Groups.Count)];

			for (int i = 0; i < selectedGroup.CreateEnemyData.Count; i++)
			{
				var spawnData = selectedGroup.CreateEnemyData[i];

				for (int j = 0; j < spawnData.Count; j++)
				{
					EnemyView _enemyView = null;
					if (_pool.IsAvailable())
					{
						if (_pool.TryGet(spawnData.EnemyType, out _enemyView))
						{
							InitEnemy(_enemyView, bonusEnergy);
							continue;
						}
					}

					var createData = _enemyData.AllEnemyData.FirstOrDefault(x => x.EnemyType == spawnData.EnemyType);
					_enemyView = _enemyFactory.CreateEnemy(createData, _pool.Return);
					InitEnemy(_enemyView, bonusEnergy);
				}
			}
		}

		private void InitEnemy(EnemyView enemyView, float bonusEnergy)
		{
			enemyView.SetBonusEnergy(bonusEnergy);
			enemyView.SetSpawnPosition(robotTransform, GetOutOfViewPosition());
		}

		private Vector3 GetOutOfViewPosition()
		{
			var centerScreen = Camera.main.transform.position + Camera.main.transform.forward * 10;

			var height = 2f * 10 * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
			var width = height * Camera.main.aspect;
			var randomX = Random.Range(-width / 2f, width / 2f);
			var outOfViewX = (randomX > 0)
				? (centerScreen.x + width / 2f + Random.Range(1f, 5f))
				: (centerScreen.x - width / 2f - Random.Range(1f, 5f));

			return new Vector3(outOfViewX, centerScreen.y, centerScreen.z);
		}
	}
}