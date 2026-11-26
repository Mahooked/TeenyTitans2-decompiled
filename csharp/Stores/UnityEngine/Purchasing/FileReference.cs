using System;
using System.IO;

namespace UnityEngine.Purchasing;

internal class FileReference
{
	private string m_FilePath;

	private ILogger m_Logger;

	internal static FileReference CreateInstance(string filename, ILogger logger)
	{
		try
		{
			string path = Path.Combine(Application.persistentDataPath, "Unity");
			string path2 = Path.Combine(Application.cloudProjectId, "IAP");
			string text = Path.Combine(path, path2);
			Directory.CreateDirectory(text);
			string text2 = Path.Combine(text, filename);
			Debug.Log("File Path: " + text2);
			return new FileReference(text2, logger);
		}
		catch (Exception message)
		{
			logger.LogError("Unable to createInstance of FileReference Class: ", message);
			return null;
		}
	}

	internal FileReference(string filePath, ILogger logger)
	{
		m_FilePath = filePath;
		m_Logger = logger;
	}

	internal void Save(string payload)
	{
		try
		{
			File.WriteAllText(m_FilePath, payload);
		}
		catch (Exception message)
		{
			m_Logger.LogError("Failed persisting content", message);
		}
	}

	internal string Load()
	{
		try
		{
			return File.ReadAllText(m_FilePath);
		}
		catch
		{
			return null;
		}
	}

	internal void Purge()
	{
		try
		{
			File.Delete(m_FilePath);
		}
		catch (Exception message)
		{
			m_Logger.Log("Failed purging cached content", message);
		}
	}
}
