using System;
using Game.EnemyBlock.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.EnemyBlock.View
{
	public abstract class EnemyView : MonoBehaviour
	{
		[SerializeField] protected Animator _animator;
		public EnemyData Data => _data;

		[SerializeField] private Vector3 _spawnOffset;

		protected EnemyData _data;
		protected Action<EnemyView> _onDieCallback;
		protected Transform _robotTransform;

		public float GetEnergy =>
			Random.Range(_data.EnergyPerPercentRange.x, _data.EnergyPerPercentRange.y) + _bonusEnergy;

		private float _bonusEnergy = 0;
		private Vector3 _spawnPosition;

		public virtual void Initialize(EnemyData enemyData, Action<EnemyView> onDieCallback)
		{
			_data = enemyData;
			_onDieCallback = onDieCallback;
		}

		public virtual void SetBonusEnergy(float bonus)
			=> _bonusEnergy = bonus;

		public virtual void SetSpawnPosition(Transform robotTransform, Vector3 spawnPosition)
		{
			_robotTransform = robotTransform;
			_spawnPosition = spawnPosition;
			transform.position = new Vector3(spawnPosition.x, _spawnOffset.y, _spawnOffset.z);
			InitLogic();
		}

		protected abstract void InitLogic();

		protected virtual void Die()
		{
			_onDieCallback?.Invoke(this);
		}
	}
}