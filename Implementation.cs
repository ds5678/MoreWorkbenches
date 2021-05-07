using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony;
using MelonLoader;
using UnityEngine;

namespace MoreWorkbenches
{
    public class Implementation : MelonMod
    {
		public override void OnApplicationStart()
		{
			Settings.OnLoad();
			Preloader.PreloadingManager.AddToList("AshCanyonRegion_SANDBOX", "Root/Design/Interactive/INTERACTIVE_WorkBenchB");
		}
	}

    [HarmonyPatch(typeof(GameManager),"Awake")]
    internal class MakeWorkbench
	{
        private static void Postfix()
		{
            Settings.MaybeCreateWorkbench();
		}
	}
}
