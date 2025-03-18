using UnityEngine;

namespace Game.EnemyBlock.View
{
	public class CivilianView : EnemyView
	{
		private static readonly int Run = Animator.StringToHash("Run");

		[SerializeField] private float moveSpeed;
		[SerializeField] private Transform[] rootBones;

		private Vector3 _moveVector;
		private bool _isMoved;

		protected override void InitLogic()
		{
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
	}
}