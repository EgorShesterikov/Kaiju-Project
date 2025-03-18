using System;
using Game.EnemyBlock.Data;
using Game.EnemyBlock.View;
using Object = UnityEngine.Object;

namespace Game.EnemyBlock.Controllers
{
	public class EnemyFactory : IUnitFactory<EnemyView, EnemyData>
	{
		public EnemyView CreateEnemy(EnemyData enemyData, Action<EnemyView> onDieCallback)
		{
			try
			{
				EnemyView _enemyView = Object.Instantiate(enemyData.Prefab);
				_enemyView.Initialize(enemyData, onDieCallback);
				return _enemyView;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
	}
}