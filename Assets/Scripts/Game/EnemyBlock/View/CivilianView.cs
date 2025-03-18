using EnemyBlock.Interfaces;
using UnityEngine;

namespace Game.EnemyBlock.View
{
	public class CivilianView : EnemyView, IDamageable
	{
		private static readonly int Run = Animator.StringToHash("Run");

		[SerializeField] private float moveSpeed;
		[SerializeField] private Transform[] rootBones;

		private Vector3 _moveVector;
		private bool _isMoved;
		private bool _wasVisible;

		protected override void InitLogic()
		{
			_wasVisible = false;
			_isMoved = false;
			if (_robotTransform.position.x < transform.position.x)
			{
				RunLeft();
				return;
			}

			RunRight();
		}

		private void Update()
		{
			if (!_isMoved) return;

			transform.Translate(_moveVector * (moveSpeed * Time.deltaTime));

			if (!IsVisibleToCamera(transform.position))
			{
				if(!_wasVisible) return;
				_isMoved = false;
				Die();
			}
			else
			{
				_wasVisible = true;
			}
		}

		private void RunLeft()
		{
			_animator.SetBool(Run, true);
			_moveVector = Vector3.left;

			foreach (var _bone in rootBones)
			{
				_bone.localEulerAngles = new Vector3(_bone.localEulerAngles.x, 180, _bone.localEulerAngles.z);
			}

			_isMoved = true;

		}

		private void RunRight()
		{
			_animator.SetBool(Run, true);
			_moveVector = Vector3.right;

			foreach (var _bone in rootBones)
			{
				_bone.localEulerAngles = new Vector3(_bone.localEulerAngles.x, 0, _bone.localEulerAngles.z);
			}

			_isMoved = true;
		}
		
		public void SetDamage(float damage = 0)
		{
			_isMoved = false;
			Die();
		}
		
		
		private static bool IsVisibleToCamera(Vector2 point)
		{
			if (Camera.main.WorldToViewportPoint(point).x + 0.1f < 0 || Camera.main.WorldToViewportPoint(point).x - 0.1f > 1 || Camera.main.WorldToViewportPoint(point).y - 0.1f > +  1 || Camera.main.WorldToViewportPoint(point).y + 0.1f < 0)
				return false;
			return true;
		}
	}
}