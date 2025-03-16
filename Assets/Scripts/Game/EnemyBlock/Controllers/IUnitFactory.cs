using System;

namespace Game.EnemyBlock.Controllers
{
	public interface IUnitFactory<T, C>
	{
		T CreateEnemy(C data, Action<T> onDieCallback);
	}
}