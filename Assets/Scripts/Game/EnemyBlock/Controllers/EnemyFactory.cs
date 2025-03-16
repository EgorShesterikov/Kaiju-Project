using Game.EnemyBlock.Data;
using Game.EnemyBlock.View;
using UnityEngine;

namespace Game.EnemyBlock.Controllers
{
	public class EnemyFactory
	{
		public EnemyView CreateEnemy(EnemyData enemyData)
		{
			GameObject enemyObject = Object.Instantiate(enemyData.Prefab);
			EnemyView _enemyView = enemyObject.GetComponent<EnemyView>(); // Добавление Component UnitView
			_enemyView.Initialize(enemyData);
			return _enemyView;
		}
	}
}