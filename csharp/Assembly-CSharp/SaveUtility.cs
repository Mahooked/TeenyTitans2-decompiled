using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveUtility : MonoBehaviour
{
	public sealed class VersionDeserializationBinder : SerializationBinder
	{
		public override Type BindToType(string assemblyName, string typeName)
		{
			if (!string.IsNullOrEmpty(assemblyName) && !string.IsNullOrEmpty(typeName))
			{
				Type type = null;
				assemblyName = Assembly.GetExecutingAssembly().FullName;
				return Type.GetType($"{typeName}, {assemblyName}");
			}
			return null;
		}
	}

	private static bool locked = false;

	private static SaveUtility _instance;

	public static string fileNameLocal = "TT2.sav";

	public static string backupFileNameLocal = "TT2.bak";

	private static string fileNameCloud = "Documents & Data";

	public static string saveDirectory = dataPath + "/saves";

	private static string _localPath = string.Empty;

	private static string _backupPath = string.Empty;

	private static string _dataPath = string.Empty;

	private static GameData gameSaveData = new GameData();

	private static Stream stream = null;

	private bool savingToCloud;

	private string error;

	public static SaveUtility instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject gameObject = new GameObject("SaveUtility");
				_instance = gameObject.AddComponent<SaveUtility>();
			}
			return _instance;
		}
	}

	public static string localPath
	{
		get
		{
			if (string.IsNullOrEmpty(_localPath))
			{
				_localPath = saveDirectory + "/" + fileNameLocal;
			}
			return _localPath;
		}
	}

	private static string backupPath
	{
		get
		{
			if (string.IsNullOrEmpty(_backupPath))
			{
				_backupPath = saveDirectory + "/" + backupFileNameLocal;
			}
			return _backupPath;
		}
	}

	public static string dataPath
	{
		get
		{
			if (string.IsNullOrEmpty(_dataPath))
			{
				string text = Application.persistentDataPath;
				try
				{
					IntPtr javaClass = AndroidJNI.FindClass("android/content/ContextWrapper");
					IntPtr methodID = AndroidJNIHelper.GetMethodID(javaClass, "getFilesDir", "()Ljava/io/File;");
					using AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
					using AndroidJavaObject androidJavaObject = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
					IntPtr obj = AndroidJNI.CallObjectMethod(androidJavaObject.GetRawObject(), methodID, new jvalue[0]);
					IntPtr javaClass2 = AndroidJNI.FindClass("java/io/File");
					IntPtr methodID2 = AndroidJNIHelper.GetMethodID(javaClass2, "getAbsolutePath", "()Ljava/lang/String;");
					string text2 = AndroidJNI.CallStringMethod(obj, methodID2, new jvalue[0]);
					if (!string.IsNullOrEmpty(text2))
					{
						text = text2;
						Debug.Log("Got internal path: " + text);
					}
					else
					{
						Debug.Log("Using fallback path");
						text = Application.persistentDataPath;
					}
				}
				catch (Exception)
				{
					text = Application.persistentDataPath;
				}
				if (!Directory.Exists(text))
				{
					text = Application.persistentDataPath;
				}
				_dataPath = text;
			}
			return _dataPath;
		}
	}

	public static GameData GameSaveData
	{
		get
		{
			if (gameSaveData.saveVersion == -1 || gameSaveData == null)
			{
				LoadSaveFile();
			}
			return gameSaveData;
		}
	}

	public static event Action PlayerDataDeserializationFailed;

	private void Awake()
	{
		_instance = this;
	}

	private void Start()
	{
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
	}

	private void OnDestroy()
	{
	}

	public static bool PlayerDataAvailable()
	{
		if (string.IsNullOrEmpty(GameSaveData.GetString("PlayerData")))
		{
			return false;
		}
		return true;
	}

	public static void Save()
	{
		if (locked)
		{
			Debug.Log("[SaveUtility::Save] - LOCKED | Ignoring save");
			return;
		}
		locked = true;
		Debug.Log("[SaveUtility::Save] - Set \"locked\" to TRUE");
		PlayerPrefs.Save();
		SaveLocalFile();
		locked = false;
		Debug.Log("[SaveUtility::Save] - Set \"locked\" to FALSE");
	}

	public static void SavePlayerData(PlayerData playerData, bool save = true)
	{
		SaveData("PlayerData", playerData);
		if (save)
		{
			Save();
		}
	}

	public static PlayerData LoadPlayerData()
	{
		return LoadData<PlayerData>("PlayerData");
	}

	public static void SaveData(string key, object data)
	{
		if (data != null)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			MemoryStream memoryStream = new MemoryStream();
			binaryFormatter.Binder = new VersionDeserializationBinder();
			binaryFormatter.Serialize(memoryStream, data);
			GameSaveData.SetString(key, Convert.ToBase64String(memoryStream.GetBuffer()));
		}
	}

	public static T LoadData<T>(string key) where T : class
	{
		string text = GameSaveData.GetString(key);
		if (!string.IsNullOrEmpty(text))
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Binder = new VersionDeserializationBinder();
			MemoryStream serializationStream = new MemoryStream(Convert.FromBase64String(text));
			try
			{
				return (T)binaryFormatter.Deserialize(serializationStream);
			}
			catch (Exception message)
			{
				Debug.Log(message);
			}
		}
		return (T)null;
	}

	public static T LoadDataFromString<T>(string data) where T : class
	{
		if (!string.IsNullOrEmpty(data))
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			MemoryStream serializationStream = new MemoryStream(Convert.FromBase64String(data));
			return (T)binaryFormatter.Deserialize(serializationStream);
		}
		return (T)null;
	}

	public static void DeleteAll()
	{
		int value = PlayerPrefs.GetInt("ResetDataNumberLocal");
		gameSaveData = new GameData();
		PlayerPrefs.DeleteAll();
		Save();
		PlayerPrefs.SetInt("ResetDataNumberLocal", value);
	}

	public static void LoadSaveFile()
	{
		if (File.Exists(localPath))
		{
			Debug.Log("Local File Found:" + localPath);
			try
			{
				GameData gameData = (GameData)DeserializeObjectFromFile(localPath);
				if (gameData != null)
				{
					gameSaveData = gameData;
				}
				return;
			}
			catch (Exception ex)
			{
				if (stream != null)
				{
					stream.Close();
				}
				Debug.LogFormat("Error Loading File  |  {0}", ex);
				try
				{
					if (File.Exists(backupPath))
					{
						Debug.Log("Backup Found, Attempt To Load: " + backupPath);
						if (File.Exists(localPath))
						{
							File.Delete(localPath);
						}
						File.Move(backupPath, localPath);
						GameData gameData2 = (GameData)DeserializeObjectFromFile(localPath);
						if (gameData2 != null)
						{
							gameSaveData = gameData2;
						}
						else
						{
							gameSaveData = new GameData();
						}
					}
					else
					{
						Debug.Log("Backup File Not Found: " + backupPath);
						gameSaveData = new GameData();
					}
					return;
				}
				catch (Exception ex2)
				{
					Debug.LogFormat("Error loading backup file  |  {0}", ex2);
					if (stream != null)
					{
						stream.Close();
					}
					try
					{
						GameData gameData3 = (GameData)DeserializeObjectFromFile(localPath);
						if (gameData3 != null)
						{
							gameSaveData = gameData3;
						}
						return;
					}
					catch (Exception ex3)
					{
						Debug.LogFormat("Error loading local path one last time  |  {0}", ex3);
						gameSaveData = new GameData();
						return;
					}
				}
			}
		}
		Debug.Log("File Not Found, Creating A New Save File if there's no migration");
		if (!MigrateSaveFile())
		{
			SaveLocalFile(skipBackup: true);
		}
		else
		{
			LoadSaveFile();
		}
	}

	public static void CheckSaveDirectory()
	{
		Debug.Log("[SaveUtility::CheckSaveDirectory] - Check whether or not the save directory exists.");
		if (!Directory.Exists(saveDirectory))
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(saveDirectory);
			if (!directoryInfo.Exists)
			{
				Debug.Log("[SaveUtility::CheckSaveDirectory] - Save directory does NOT exist, so creating it.");
				directoryInfo.Create();
			}
		}
	}

	public static bool MigrateSaveFile()
	{
		string text = Application.persistentDataPath + "/" + fileNameLocal;
		string text2 = Application.persistentDataPath + "/" + backupFileNameLocal;
		CheckSaveDirectory();
		if (File.Exists(text))
		{
			File.Move(text, localPath);
			if (File.Exists(text2))
			{
				File.Move(text2, backupPath);
			}
			return true;
		}
		return false;
	}

	public static void LoadTestGameData(GameData gameData)
	{
		if (gameData != null)
		{
			gameSaveData = gameData.Copy();
		}
	}

	private static void SaveLocalFile(bool skipBackup = false)
	{
		SerializeObjectToFile(gameSaveData, localPath, skipBackup);
	}

	public static void SaveGameDataFile(GameData gameData, string filePath)
	{
		SerializeObjectToFile(gameData, filePath);
	}

	private static bool LocalFileCorrupt()
	{
		object obj = DeserializeObjectFromFile(localPath);
		if (obj == null)
		{
			return true;
		}
		return false;
	}

	public static void SerializeObjectToFile(object serializableObject, string filePath, bool skipBackup = true)
	{
		Debug.LogFormat("[SaveUtility::SerializeObjectToFile] - Start serializing [{0}] to path \"{1}\"", serializableObject, filePath);
		try
		{
			if (!skipBackup && File.Exists(filePath))
			{
				if (!LocalFileCorrupt())
				{
					if (File.Exists(backupPath))
					{
						File.Delete(backupPath);
					}
					File.Move(filePath, backupPath);
					Debug.Log("[SaveUtility::SerializeObjectToFile] - Existing Save File moved to backup" + backupPath);
				}
				else
				{
					Debug.Log("[SaveUtility::SerializeObjectToFile] - Local Save File is corrupt, so not storing as backup.");
				}
			}
			if (stream != null)
			{
				Debug.Log("[SaveUtility::SerializeObjectToFile] - Close Stream");
				stream.Close();
			}
			Debug.LogFormat("[SaveUtility::SerializeObjectToFile] - Open file at path \"{0}\"", filePath);
			stream = File.Open(filePath, FileMode.Create, FileAccess.Write);
			Debug.LogFormat("[SaveUtility::SerializeObjectToFile] - Create new BinaryFormatter instance");
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			Debug.LogFormat("[SaveUtility::SerializeObjectToFile] - Set Binder on new BinaryFormatter instance to a new VersionDeserializationBinder");
			binaryFormatter.Binder = new VersionDeserializationBinder();
			Debug.LogFormat("[SaveUtility::SerializeObjectToFile] - Call Serialize on the BinaryFormatter instance with stream {0} and object [{1}]", stream, serializableObject);
			binaryFormatter.Serialize(stream, serializableObject);
			Debug.LogFormat("[SaveUtility::SerializeObjectToFile] - Close the stream");
			stream.Close();
		}
		catch (Exception ex)
		{
			Debug.LogFormat("[SaveUtility::SerializeObjectToFile] - Something went wrong when serializing object to file.  |  {0}", ex);
			if (stream != null)
			{
				stream.Close();
			}
			if (File.Exists(backupPath))
			{
				if (File.Exists(filePath))
				{
					File.Delete(filePath);
				}
				File.Move(backupPath, filePath);
				Debug.Log("[SaveUtility::SerializeObjectToFile] - Moved Backup File to default Save File path.");
			}
		}
	}

	public static object DeserializeObjectFromFile(string filePath)
	{
		if (!File.Exists(filePath))
		{
			return null;
		}
		try
		{
			if (stream != null)
			{
				stream.Close();
			}
			stream = File.Open(filePath, FileMode.Open);
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Binder = new VersionDeserializationBinder();
			object result = binaryFormatter.Deserialize(stream);
			stream.Close();
			return result;
		}
		catch (Exception ex)
		{
			Debug.LogFormat("Error Deserializing Object From File stream  |  {0}", ex);
			if (stream != null)
			{
				stream.Close();
			}
			return null;
		}
	}

	public IEnumerator SaveGameToCloud()
	{
		Debug.Log("[SaveUtility.cs][SaveGameToCloud()] Save Game to Cloud");
		SaveLocalFile();
		yield break;
	}

	public IEnumerator SaveGameSkipLocalSave()
	{
		yield break;
	}

	public IEnumerator DeleteiCloudDocument()
	{
		savingToCloud = true;
		JCloudDocumentOperation operation = JCloudDocument.FileDelete(fileNameCloud);
		while (!operation.finished)
		{
			yield return null;
		}
		savingToCloud = false;
		if ((bool)operation.result)
		{
			Debug.Log("Deleted file " + fileNameCloud);
		}
		else
		{
			Debug.Log("Did not Delete file " + fileNameCloud);
		}
	}

	public IEnumerator LoadGame()
	{
		Debug.Log("Load iCloud Save");
		JCloudDocumentOperation operation = JCloudDocument.FileHasConflictVersions(fileNameCloud);
		while (!operation.finished)
		{
			yield return null;
		}
		if (operation.error.HasValue && operation.error.Value != JCloudDocumentError.CloudUnavailable)
		{
			Debug.Log("Found error");
			HandleDocumentError(operation.error.Value);
			yield break;
		}
		if (((bool?)operation.result).HasValue && (bool)operation.result)
		{
			Debug.Log("Conflicts Found error");
			operation = JCloudDocument.FileFetchAllVersions(fileNameCloud);
			while (!operation.finished)
			{
				yield return null;
			}
			if (operation.error.HasValue && operation.error.Value != JCloudDocumentError.InvalidVersionIdentifier)
			{
				HandleDocumentError(operation.error.Value);
				yield break;
			}
			JCloudDocumentVersions versions = (JCloudDocumentVersions)operation.result;
			float bestVersionProgress = -1f;
			byte[] bestVersionIdentifier = null;
			Debug.Log("Look for best version");
			JCloudDocumentVersionMetadata[] versionsMetadata = versions.versionsMetadata;
			for (int i = 0; i < versionsMetadata.Length; i++)
			{
				JCloudDocumentVersionMetadata metadata = versionsMetadata[i];
				operation = JCloudDocument.FileReadVersionBytes(fileNameCloud, metadata.uniqueIdentifier);
				while (!operation.finished)
				{
					yield return null;
				}
				if (operation.error.HasValue)
				{
					Debug.Log("Error found looking for best");
					if (operation.error.Value != JCloudDocumentError.InvalidVersionIdentifier)
					{
						HandleDocumentError(operation.error.Value);
						yield break;
					}
				}
				float versionProgress = DeserializeSaveGameProgress((byte[])operation.result);
				Debug.Log("Progress: " + versionProgress + " best " + bestVersionProgress);
				if (versionProgress > bestVersionProgress)
				{
					bestVersionProgress = versionProgress;
					bestVersionIdentifier = metadata.uniqueIdentifier;
				}
			}
			if (bestVersionIdentifier != null)
			{
				Debug.Log("Found version");
				operation = JCloudDocument.FilePickVersion(fileNameCloud, bestVersionIdentifier, versions.versionsHash);
				while (!operation.finished)
				{
					yield return null;
				}
				if (operation.error.HasValue)
				{
					HandleDocumentError(operation.error.Value);
					yield break;
				}
			}
		}
		Debug.Log("Conflicts cleared read file " + fileNameCloud);
		yield return StartCoroutine(ReadFileAtPath(fileNameCloud));
	}

	private float DeserializeSaveGameProgress(byte[] data)
	{
		if (data == null)
		{
			return -1f;
		}
		try
		{
			if (stream != null)
			{
				stream.Close();
			}
			stream = new MemoryStream(data);
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Binder = new VersionDeserializationBinder();
			GameData gamedata = (GameData)binaryFormatter.Deserialize(stream);
			stream.Close();
			return GetTotalCompleteForGameData(gamedata);
		}
		catch (Exception ex)
		{
			Debug.LogFormat("Failed to deserialize save game progress  |  {0}", ex);
			if (stream != null)
			{
				stream.Close();
			}
			return -1f;
		}
	}

	private IEnumerator ReadFileAtPath(string iCloudPath)
	{
		JCloudDocumentOperation operation = JCloudDocument.FileReadAllBytes(iCloudPath);
		while (!operation.finished)
		{
			yield return null;
		}
		if (operation.error.HasValue)
		{
			HandleDocumentError(operation.error.Value);
		}
		else if (operation.result is byte[] saveBytes)
		{
			try
			{
				Stream stream = new MemoryStream(saveBytes);
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Binder = new VersionDeserializationBinder();
				GameData gamedata = (GameData)binaryFormatter.Deserialize(stream);
				stream.Close();
				int totalCompleteForGameData = GetTotalCompleteForGameData(gamedata);
				int totalCompleteForGameData2 = GetTotalCompleteForGameData(gameSaveData);
				Debug.Log("totalCompleteCloudGameData " + totalCompleteForGameData + " totalCompleteLocalData " + totalCompleteForGameData2);
				if (totalCompleteForGameData > totalCompleteForGameData2)
				{
					Debug.Log("Cloud save better than local save");
					gameSaveData = gamedata;
				}
				else
				{
					Debug.Log("Local save better than cloud save");
				}
				SaveLocalFile(skipBackup: true);
			}
			catch (Exception ex)
			{
				Debug.LogFormat("Error Deserializing iCloud save, possibly corrupt  |  {0}", ex);
			}
			error = null;
		}
		else
		{
			error = "There is no saved game present on this device. Start a new game.";
		}
	}

	private int GetTotalCompleteForGameData(GameData gamedata)
	{
		if (gamedata == null)
		{
			return -1;
		}
		return CountTotalCompleted(gamedata);
	}

	public static int CountTotalCompleted(GameData gamedata)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		if (gamedata != null)
		{
			PlayerData playerData = LoadPlayerDataForGameData(gamedata);
			if (playerData != null)
			{
				for (int i = 0; i < playerData.completedMissions.Count; i++)
				{
					Mission mission = GameManager.instance.communicatorDatabase.GetMission(playerData.completedMissions[i].uid);
					if (mission.type == MissionType.Story)
					{
						num++;
					}
					if (mission.type == MissionType.Side)
					{
						num2++;
					}
				}
				num3 = playerData.characters.Count;
			}
		}
		return num * 1000 + num2 * 100 + num3;
	}

	private void HandleDocumentError(JCloudDocumentError documentError)
	{
		Debug.Log("iCloud Document error!: " + documentError);
		switch (documentError)
		{
		case JCloudDocumentError.InvalidPlatform:
			error = "No file access allowed on this platform.";
			break;
		case JCloudDocumentError.PluginError:
		case JCloudDocumentError.InvalidArguments:
		case JCloudDocumentError.NativeError:
			error = "An error ocurred while saving/loading game data. Please retry. Error: " + documentError;
			break;
		case JCloudDocumentError.DocumentNotFound:
			error = "There is no saved game present on this device. Start a new game.";
			break;
		case JCloudDocumentError.DownloadTimeout:
			error = "Could not download the save game data. Please retry.";
			break;
		case JCloudDocumentError.InvalidVersionIdentifier:
			error = "Invalid version Identifier";
			break;
		case JCloudDocumentError.InvalidVersionsHash:
			error = "An error occured while handling conflict versions of your save game data. Please retry.";
			break;
		}
		Debug.Log("Error: " + error);
	}

	public void JCloudDocumentDidChangeExternally(JCloudDocumentExternalChange[] changes)
	{
		Debug.Log("Document did change externally!");
		for (int i = 0; i < changes.Length; i++)
		{
			switch (changes[i].change)
			{
			case JCloudDocumentChangeType.Added:
				Debug.Log("iCloud Document added");
				break;
			case JCloudDocumentChangeType.Changed:
				Debug.Log("iCloud Document changed");
				break;
			case JCloudDocumentChangeType.Removed:
				Debug.Log("iCloud Document remove");
				break;
			}
		}
	}

	public static PlayerData LoadPlayerDataForGameData(GameData gameData)
	{
		if (gameData == null)
		{
			return null;
		}
		PlayerData result = null;
		string text = gameData.GetString("PlayerData");
		if (!string.IsNullOrEmpty(text))
		{
			try
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				MemoryStream serializationStream = new MemoryStream(Convert.FromBase64String(text));
				return (PlayerData)binaryFormatter.Deserialize(serializationStream);
			}
			catch (Exception ex)
			{
				Debug.LogFormat("Failed to load PlayerData from GameData  |  {0}", ex);
				if (PlayerDataDeserializationFailed != null)
				{
					PlayerDataDeserializationFailed();
				}
				return null;
			}
		}
		return result;
	}

	public static GameData LoadGameSaveDataForFilePath(string filePath)
	{
		GameData result = null;
		if (File.Exists(filePath))
		{
			return (GameData)DeserializeObjectFromFile(filePath);
		}
		return result;
	}

	public static void CopySave()
	{
	}

	public static void CopySave(string srcFileName, string destFileName = "")
	{
		UnpackMobileFile(srcFileName, destFileName);
	}

	private static void UnpackMobileFile(string fileName, string destFileName = "")
	{
		if (string.IsNullOrEmpty(destFileName))
		{
			destFileName = fileName;
		}
		string text = Path.Combine(saveDirectory + "/", destFileName);
		string text2 = Path.Combine(Application.streamingAssetsPath, fileName);
		if (text2.Contains("://"))
		{
			WWW wWW = new WWW(text2);
			while (!wWW.isDone)
			{
			}
			if (string.IsNullOrEmpty(wWW.error))
			{
				File.WriteAllBytes(text, wWW.bytes);
			}
			else
			{
				Debug.Log("ERROR: the file DB named " + fileName + " doesn't exist in the StreamingAssets Folder, please copy it there.");
			}
		}
		else if (File.Exists(text2))
		{
			Debug.Log("File.Exists " + text2);
			if (File.Exists(text))
			{
				Debug.Log("File.Delete " + text);
				File.Delete(text);
			}
			File.Copy(text2, text, overwrite: true);
		}
		else
		{
			Debug.Log("ERROR: the file DB named " + fileName + " doesn't exist in the StreamingAssets Folder, please copy it there.");
		}
	}

	public static void SetString(string key, string value, bool save = true)
	{
		GameSaveData.SetString(key, value);
	}

	public static void SetInt(string key, int value, bool save = true)
	{
		GameSaveData.SetInt(key, value);
	}

	public static int GetInt(string key)
	{
		return GameSaveData.GetInt(key);
	}

	public static string GetString(string key)
	{
		return GameSaveData.GetString(key);
	}

	public static string GetString(string key, string defaultValue)
	{
		return GameSaveData.GetString(key, defaultValue);
	}

	public static int GetInt(string key, int defaultValue)
	{
		return GameSaveData.GetInt(key, defaultValue);
	}

	public static void DeleteKey(string key, bool save = true)
	{
		GameSaveData.DeleteKey(key);
	}

	public static bool HasKey(string key)
	{
		return GameSaveData.HasKey(key);
	}
}
