using System;
using Game.EnemyBlock.Data;
using UnityEngine;

namespace Game.EnemyBlock.View
{
	public class EnemyView : MonoBehaviour
	{
		private EnemyData _data;
		public EnemyData Data => _data;

		private Action<EnemyView> _onDieCallback;
		private float _bonusEnergy = 0;
		private UnitSpawnPointView _currenSpawnPoint;

		public void Initialize(EnemyData enemyData, Action<EnemyView> onDieCallback)
		{
			_data = enemyData;
			_onDieCallback = onDieCallback;
		}

		public void SetBonusEnergy(float bonus)
			=> _bonusEnergy = bonus;

		public void SetSpawnPoint(UnitSpawnPointView spawnPointView)
		{
			_currenSpawnPoint = spawnPointView;
			transform.position = spawnPointView.transform.position;
		}

		private void Die()
		{
			_onDieCallback?.Invoke(this);
		}
	}
}