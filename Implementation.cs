using Harmony;
using MelonLoader;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace MoreWorkbenches
{
	public static class BuildInfo
	{
		public const string Name = "MoreWorkbenches"; // Name of the Mod.  (MUST BE SET)
		public const string Description = "Adds a couple more workbenches to the game."; // Description for the Mod.  (Set as null if none)
		public const string Author = "ds5678"; // Author of the Mod.  (MUST BE SET)
		public const string Company = null; // Company that made the Mod.  (Set as null if none)
		public const string Version = "1.1.0"; // Version of the Mod.  (MUST BE SET)
		public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
	}
	public class Implementation : MelonMod
	{
		private static AssetBundle assetBundle = LoadEmbeddedAssetBundle();
		private const string ASSET_PATH = "assets/custom/prefabs/interactive/interactive_workbenchb.prefab";

		public override void OnApplicationStart()
		{
			Debug.Log($"[{Info.Name}] Version {Info.Version} loaded!");
			Settings.OnLoad();
		}

		private static AssetBundle LoadEmbeddedAssetBundle()
		{
			MemoryStream memoryStream;

			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoreWorkbenches.res.workbenchb"))
			{
				memoryStream = new MemoryStream((int)stream.Length);
				stream.CopyTo(memoryStream);
			}
			if (memoryStream.Length == 0)
			{
				throw new System.Exception("No data loaded!");
			}
			return AssetBundle.LoadFromMemory(memoryStream.ToArray());
		}

		internal static GameObject GetNewWorkbenchInstance()
		{
			GameObject workbenchPrefab = assetBundle.LoadAsset<GameObject>(ASSET_PATH);
			if (workbenchPrefab is null)
			{
				MelonLogger.LogError("Workbench prefab is null!");
				return null;
			}
			else return GameObject.Instantiate(workbenchPrefab);
		}
	}

	[HarmonyPatch(typeof(GameManager), "Awake")]
	internal class MakeWorkbench
	{
		private static void Postfix()
		{
			Settings.MaybeCreateWorkbench();
		}
	}
}
