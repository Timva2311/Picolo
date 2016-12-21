using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Domain;

namespace BL
{
    public interface IRuleManager
    {
        Rule ExecuteGameLogic(ref List<Player> activePlayers, ref List<Rule> activeRules);
    }
}
