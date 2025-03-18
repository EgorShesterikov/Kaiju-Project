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

		public bool TryGet(EnemyType needEnemyType, out EnemyView enemyView)
		{
			enemyView = null;
			for (int i = 0; i < _enemyViews.Count; i++)
			{
				var unit = _enemyViews[i];
				if (needEnemyType != unit.Data.EnemyType) continue;
				enemyView = unit;
				enemyView.gameObject.SetActive(true);
				_enemyViews.Remove(unit);
				return true;
			}

			Debug.Log($"Enemy not found");
			return false;
		}

		public void Return(EnemyView obj)
		{
			obj.gameObject.SetActive(false);
			_enemyViews.Add(obj);
		}
	}
}