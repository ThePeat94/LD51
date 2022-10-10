using System.Collections.Generic;

namespace Nidavellir.Towers.TargetFinding
{
    public interface ITowerTargetStrategy
    {
        public List<EnemyHealthController> FindEnemiesInRange(int count);
    }
}