using UnityEngine;

namespace Game.EnemyBlock.View
{
	public class UnitSpawnPointView : MonoBehaviour
	{
		[SerializeField] private UnitSpawnPointView _oppositeSpawnPoint;
		public UnitSpawnPointView OppositeSpawnPoint =>_oppositeSpawnPoint;
	}
}