using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BL
{
    public interface IRuleManager
    {
        Rule FindRule(int id);
        Rule FindEndrule(string end);
        int CountRules();
    }
}
