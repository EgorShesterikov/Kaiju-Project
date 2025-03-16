using System.Collections.Generic;
using Game.EnemyBlock.Data;
using Game.EnemyBlock.View;
using UnityEngine;

namespace Game.EnemyBlock.Controllers
{
	public class UnitPool
	{
		private List<EnemyView> _enemyViews = new();

		public bool IsAvailable()
		{
			return _enemyViews.Count > 0;
		}

		public void Add(EnemyView obj)
		{
			_enemyViews.Add(obj);
		}

		public EnemyView Get(EnemyType needEnemyType)
		{
			for (int i = 0; i < _enemyViews.Count; i++)
			{
				var unit = _enemyViews[i];
				if (needEnemyType != unit.Data.EnemyType) continue;

				_enemyViews.Remove(unit);
				return unit;
			}

			Debug.Log($"Enemy not found");
			return null;
		}

		public void Return(EnemyView obj)
		{
			_enemyViews.Add(obj);
		}
	}
}