using ModSettings;
using UnityEngine;

namespace MoreWorkbenches
{
	public enum WorkbenchLocation
	{
		PoachersCamp,
		GreyMothers,
		ThompsonsCrossing,
		JackrabbitIsland
	}
	internal class MoreWorkbenchSettings : JsonModSettings
	{
		[Name("Workbench at the Poacher's Camp")]
		public bool poachersCampWorkbench = true;

		[Name("Workbench in Milton")]
		[Description("It is located behind Grey Mother's shed.")]
		public bool miltonWorkbench = true;

		[Name("Workbench in Thompson's Crossing")]
		[Description("It is located inside the house under construction.")]
		public bool pleasantValleyWorkbench = true;

		[Name("Workbench on Jackrabbit Island")]
		public bool jackrabbitWorkbench = true;

		protected override void OnConfirm()
		{
			base.OnConfirm();
			Settings.OnConfirm();
		}
	}
	internal static class Settings
	{
		internal static MoreWorkbenchSettings options;
		private static GameObject workbench;
		internal static void OnLoad()
		{
			options = new MoreWorkbenchSettings();
			options.AddToModSettings("More Workbenches");
		}
		internal static void MaybeCreateWorkbench()
		{
			switch (GameManager.m_ActiveScene)
			{
				case "MountainTownRegion":
					CreateWorkbench(WorkbenchLocation.GreyMothers);
					break;
				case "MarshRegion":
					CreateWorkbench(WorkbenchLocation.PoachersCamp);
					break;
				case "CoastalHouseH":
					CreateWorkbench(WorkbenchLocation.ThompsonsCrossing);
					break;
				case "CoastalRegion":
					CreateWorkbench(WorkbenchLocation.JackrabbitIsland);
					break;
				default:
					workbench = null;
					break;
			}
		}
		private static void CreateWorkbench(WorkbenchLocation workbenchLocation)
		{
			workbench = Implementation.GetNewWorkbenchInstance();
			if (workbench is null) return;

			switch (workbenchLocation)
			{
				case WorkbenchLocation.GreyMothers:
					workbench.transform.position = new Vector3(1142.2f, 267f, 1781f);
					workbench.transform.rotation = Quaternion.Euler(new Vector3(0, 73, 0));
					workbench.SetActive(options.miltonWorkbench);
					break;
				case WorkbenchLocation.PoachersCamp:
					workbench.transform.position = new Vector3(1110.5f, -130f, 976.3f);
					workbench.transform.rotation = Quaternion.Euler(new Vector3(5, 160, 0));
					workbench.SetActive(options.poachersCampWorkbench);
					break;
				case WorkbenchLocation.ThompsonsCrossing:
					workbench.transform.position = new Vector3(-4f, -0.2f, -2.8f);
					workbench.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
					workbench.SetActive(options.pleasantValleyWorkbench);
					break;
				case WorkbenchLocation.JackrabbitIsland:
					workbench.transform.position = new Vector3(-268f, 74.8f, 284.3f);
					workbench.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
					workbench.SetActive(options.jackrabbitWorkbench);
					break;
			}
		}
		internal static void OnConfirm()
		{
			if (workbench is null) return;
			switch (GameManager.m_ActiveScene)
			{
				case "MountainTownRegion":
					workbench.SetActive(options.miltonWorkbench);
					break;
				case "MarshRegion":
					workbench.SetActive(options.poachersCampWorkbench);
					break;
				case "CoastalHouseH":
					workbench.SetActive(options.pleasantValleyWorkbench);
					break;
				case "CoastalRegion":
					workbench.SetActive(options.jackrabbitWorkbench);
					break;
			}
		}
	}
}
