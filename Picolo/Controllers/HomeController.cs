using BL;
using DAL;
using Domain;
using DrinkingGame._Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Picolo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRuleManager _mgr;


        public HomeController()
        {
            _mgr = new RuleManager();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Game(FormCollection form)
        {
            var names = form["name"].Split(char.Parse(","));
            var sex = form["sex"].Split(char.Parse(","));

            var activePlayers = new List<Player>();
            for (int i = 0; i < names.Length; i++)
            {
                if (string.IsNullOrEmpty(names[i]))
                {
                    continue;
                }
                var player = new Player()
                {
                    Name = names[i],
                    Sex = (Sex)Enum.Parse(typeof(Sex), sex[i])
                };
                activePlayers.Add(player);
            }
            activePlayers.Shuffle();
            List<Rule> activeRules = new List<Rule>();
            Rule displayRule = _mgr.ExecuteGameLogic(ref activePlayers, ref activeRules);
            Session["Rules"] = activeRules;
            Session["Players"] = activePlayers;
            return View(displayRule);
        }

        public ActionResult GameResult()
        {
            List<Player> activePlayers = (List<Player>) Session["Players"];
            activePlayers.Shuffle();
            List<Rule> activeRules = (List<Rule>)Session["Rules"];
            Rule displayRule = _mgr.ExecuteGameLogic(ref activePlayers, ref activeRules);
            Session["Rules"] = activeRules;
            Session["Players"] = activePlayers;
            return View(displayRule);
        }

        public ActionResult Rules()
        {
            return View();
        }
    }
}