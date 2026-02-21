// using System.Collections.Generic;
// using System.IO;
// using System.Threading.Tasks;
// using UnityEditor;
// using UnityEditor.PackageManager;
// using UnityEditor.PackageManager.Requests;
// using UnityEngine;
//
// namespace BumblebitExtensions
// {
// 	public static class ProjectSetup
// 	{
// 		[MenuItem("Tools/Setup/Import Essential Assets", default, 0)]
// 		private static void ImportEssentials()
// 		{
// 			//Assets.ImportAsset("Odin Inspector and Serializer.unitypackage", "Sirenix/Editor ExtensionsSystem");
// 			//Assets.ImportAsset("Odin Validator.unitypackage", "Sirenix/Editor ExtensionsUtilities");
// 			//Assets.ImportAsset("Editor Console Pro.unitypackage", "FlyingWorm/Editor ExtensionsSystem");
// 			Assets.ImportAsset("Selection History.unitypackage", "StaggartCreations/Editor ExtensionsSystem");
// 			//Assets.ImportAsset("Better Hierarchy.unitypackage", "Toaster Head/Editor ExtensionsSystem");
//
//
//
// 		}
//
// 		[MenuItem("Tools/Setup/Install Essential Packages", default, 1)]
// 		public static void InstallPackages()
// 		{
// 			Packages.InstallPackages(new[]
// 			{
// 			"git+https://github.com/Cysharp/UniTask.git",
// 			"git+https://github.com/adammyhre/Unity-Improved-Timers.git",
// 			"git+https://github.com/Rofubtw/EventBus.git",
// 			//"com.unity.2d.animation",
// 			//"git+https://github.com/adammyhre/Unity-Utils.git",
//
//
//
// 			"com.unity.inputsystem" // Make this last package to install because this package requires a restart
// 		});
// 		}
//
// 		[MenuItem("Tools/Setup/Import External UnityPackage", default, 2)]
// 		public static void ImportExternalPackage()
// 		{
// 			string packagePath = EditorUtility.OpenFilePanel("Select Unity Package", "", "unitypackage");
//
// 			if (!string.IsNullOrEmpty(packagePath))
// 			{
// 				AssetDatabase.ImportPackage(packagePath, true);
// 				Debug.Log($"Imported package from: {packagePath}");
// 			}
// 			else
// 			{
// 				Debug.LogWarning("Package import canceled or invalid path selected.");
// 			}
// 		}
//
// 		[MenuItem("Tools/Setup/Import All UnityPackages From Folder", default, 3)]
// 		public static void ImportAllPackagesFromFolder()
// 		{
// 			string folderPath = EditorUtility.OpenFolderPanel("Select Folder Containing Unity Packages", "", "");
//
// 			if (!string.IsNullOrEmpty(folderPath))
// 			{
// 				string[] packages = Directory.GetFiles(folderPath, "*.unitypackage");
//
// 				foreach (var package in packages)
// 				{
// 					AssetDatabase.ImportPackage(package, false);
// 					Debug.Log($"Imported package: {Path.GetFileName(package)}");
// 				}
// 			}
// 			else
// 			{
// 				Debug.LogWarning("Folder selection canceled or invalid path selected.");
// 			}
// 		}
//
//
// 		[MenuItem("Tools/Setup/Remove Unused Packages", default, 4)]
// 		public static void RemoveUnusedPackages()
// 		{
// 			Packages.RemovePackages(new[]
// 			{
// 			"com.unity.visualscripting",
// 			"com.unity.collab-proxy",
// 			"com.unity.ide.vscode",
// 		});
// 		}
//
//
// 		[MenuItem("Tools/Setup/Create Folders", default, 5)]
// 		public static void CreateFolders()
// 		{
// 			Folders.Create("_Project", "Animation", "Art", "Materials", "Prefabs", "Scripts/Tests", "Scripts/Tests/Editor", "Scripts/Tests/Runtime");
// 			AssetDatabase.Refresh();
// 			Folders.Move("_Project", "Scenes");
// 			Folders.Move("_Project", "Settings");
// 			//Folders.Delete("TutorialInfo");
// 			AssetDatabase.Refresh();
//
// 			const string pathToInputActions = "Assets/InputSystem_Actions.inputactions";
// 			string destination = "Assets/_Project/Settings/InputSystem_Actions.inputactions";
// 			AssetDatabase.MoveAsset(pathToInputActions, destination);
//
// 			const string pathToReadme = "Assets/Readme.asset";
// 			AssetDatabase.DeleteAsset(pathToReadme);
// 			AssetDatabase.Refresh();
// 		}
//
//
//
//
//
// 		private static class Assets
// 		{
// 			public static void ImportAsset(string asset, string folder)
// 			{
// 				string basePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
// 				string assetsFolder = Path.Combine(basePath, "Unity/Asset Store-5.x");
// 				// C:/Users/<username>//Appata/Roaming/Unity/Asset Store-5.x
//
// 				AssetDatabase.ImportPackage(Path.Combine(assetsFolder, folder, asset), false);
// 			}
// 		}
//
// 		private static class Packages
// 		{
// 			private static AddRequest request;
// 			private static RemoveRequest removeRequest;
// 			private static Queue<string> packagesToInstall = new Queue<string>();
// 			private static Queue<string> packagesToRemove = new Queue<string>();
//
// 			private static async void StartNextPackageInstallation()
// 			{
// 				string package = packagesToInstall.Dequeue();
// 				request = Client.Add(package);
//
// 				while (!request.IsCompleted) await Task.Delay(10);
//
// 				if (request.Status == StatusCode.Success) Debug.Log($"Installed: {request.Result.packageId}");
// 				else if (request.Status >= StatusCode.Failure) Debug.LogError(request.Error.message);
//
// 				if (packagesToInstall.Count > 0)
// 				{
// 					await Task.Delay(1000);
// 					StartNextPackageInstallation();
// 				}
// 			}
//
// 			public static void InstallPackages(string[] packages)
// 			{
// 				foreach (string package in packages)
// 				{
// 					packagesToInstall.Enqueue(package);
// 				}
//
// 				if (packagesToInstall.Count > 0)
// 				{
// 					StartNextPackageInstallation();
// 				}
// 			}
//
// 			public static async void RemovePackages(string[] packages)
// 			{
// 				foreach (string package in packages)
// 				{
// 					packagesToRemove.Enqueue(package);
// 				}
//
// 				await ProcessNextRemovePackage();
// 			}
//
// 			private static async Task ProcessNextRemovePackage()
// 			{
// 				if (packagesToRemove.Count == 0) return;
//
// 				string packageName = packagesToRemove.Dequeue();
// 				removeRequest = Client.Remove(packageName);
//
// 				while (!removeRequest.IsCompleted)
// 					await Task.Delay(10);
//
// 				if (removeRequest.Status == StatusCode.Success)
// 					Debug.Log($"Successfully removed: {packageName}");
// 				else
// 					Debug.LogError($"Failed to remove {packageName}: {removeRequest.Error.message}");
//
// 				await Task.Delay(500);
// 				await ProcessNextRemovePackage();
// 			}
// 		}
//
// 		private static class Folders
// 		{
// 			public static void Delete(string folderName)
// 			{
// 				string pathToDelete = $"Assets/{folderName}";
//
// 				if (AssetDatabase.IsValidFolder(pathToDelete))
// 				{
// 					AssetDatabase.DeleteAsset(pathToDelete);
// 				}
// 			}
//
// 			public static void Move(string newParent, string folderName)
// 			{
// 				string sourcePath = $"Assets/{folderName}";
// 				if (AssetDatabase.IsValidFolder(sourcePath))
// 				{
// 					string destinationPath = $"Assets/{newParent}/{folderName}";
// 					string error = AssetDatabase.MoveAsset(sourcePath, destinationPath);
//
// 					if (!string.IsNullOrEmpty(error))
// 					{
// 						Debug.LogError($"Failed to move {folderName}: {error}");
// 					}
// 				}
// 			}
//
// 			private static void CreateSubFolders(string rootPath, string folderHieararchy)
// 			{
// 				var folders = folderHieararchy.Split('/');
// 				var currentPath = rootPath;
//
// 				foreach (var folder in folders)
// 				{
// 					currentPath = Path.Combine(currentPath, folder);
// 					if (!Directory.Exists(currentPath))
// 					{
// 						Directory.CreateDirectory(currentPath);
// 					}
// 				}
// 			}
//
// 			public static void Create(string root, params string[] folders)
// 			{
// 				var fullpath = Path.Combine(Application.dataPath, root);
// 				if (!Directory.Exists(fullpath))
// 				{
// 					Directory.CreateDirectory(fullpath);
// 				}
//
// 				foreach (var folder in folders)
// 				{
// 					CreateSubFolders(fullpath, folder);
// 				}
// 			}
// 		}
// 	}
// }
//
