using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class jLoadSaveUI : MonoBehaviour
{
	public GameObject player;

	public GameObject playerCamera;

	public GameObject redCube;

	public GameObject greenCube;

	public GameObject blueCube;

	private string lastPlayedPlatform;

	private string lastPlayedTime;

	private int playCount;

	private string error;

	private void OnEnable()
	{
		lastPlayedPlatform = JCloudData.GetString("Last Played Platform", Application.platform.ToString());
		JCloudData.SetString("Last Played Platform", Application.platform.ToString());
		lastPlayedTime = JCloudData.GetString("Last Played Time", DateTime.Now.ToString());
		JCloudData.SetString("Last Played Time", DateTime.Now.ToString());
		playCount = JCloudData.GetInt("Play Count");
		playCount++;
		JCloudData.SetInt("Play Count", playCount);
		JCloudData.Save();
	}

	private void OnApplicationPause(bool pause)
	{
		if (!pause)
		{
			lastPlayedPlatform = JCloudData.GetString("Last Played Platform", Application.platform.ToString());
			JCloudData.SetString("Last Played Platform", Application.platform.ToString());
			lastPlayedTime = JCloudData.GetString("Last Played Time", DateTime.Now.ToString());
			JCloudData.SetString("Last Played Time", DateTime.Now.ToString());
			playCount = JCloudData.GetInt("Play Count");
			playCount++;
			JCloudData.SetInt("Play Count", playCount);
			JCloudData.Save();
		}
	}

	private void OnGUI()
	{
		if (GUILayout.Button("Load Game", GUILayout.Width(320f), GUILayout.Height(40f)))
		{
			StartCoroutine(LoadGame());
		}
		if (GUILayout.Button("Save Game", GUILayout.Width(320f), GUILayout.Height(40f)))
		{
			StartCoroutine(SaveGame());
		}
		if (error != null)
		{
			GUILayout.Label("Error: " + error);
		}
		GUILayout.Label("Last Played Platform: " + lastPlayedPlatform);
		GUILayout.Label("Last Played Time: " + lastPlayedTime);
		GUILayout.Label("Play Count: " + playCount);
	}

	private IEnumerator LoadGame()
	{
		JCloudDocumentOperation operation = JCloudDocument.FileReadAllBytes("Savegames/My saved game.sav");
		while (!operation.finished)
		{
			yield return null;
		}
		if (operation.error.HasValue)
		{
			HandleDocumentError(operation.error.Value);
			yield break;
		}
		error = null;
		if (operation.result is byte[] gameBytes)
		{
			MemoryStream input = new MemoryStream(gameBytes);
			BinaryReader reader = new BinaryReader(input);
			DeserializeTransformFromReader(player.transform, reader);
			DeserializeRigidbodyFromReader(player.GetComponent<Rigidbody>(), reader);
			DeserializeTransformFromReader(playerCamera.transform, reader);
			DeserializeTransformFromReader(redCube.transform, reader);
			DeserializeRigidbodyFromReader(redCube.GetComponent<Rigidbody>(), reader);
			DeserializeTransformFromReader(greenCube.transform, reader);
			DeserializeRigidbodyFromReader(greenCube.GetComponent<Rigidbody>(), reader);
			DeserializeTransformFromReader(blueCube.transform, reader);
			DeserializeRigidbodyFromReader(blueCube.GetComponent<Rigidbody>(), reader);
		}
	}

	private IEnumerator SaveGame()
	{
		if (!player || !playerCamera || !redCube || !greenCube || !blueCube)
		{
			yield break;
		}
		MemoryStream dataStream = new MemoryStream();
		BinaryWriter writer = new BinaryWriter(dataStream);
		SerializeTransformToWriter(player.transform, writer);
		SerializeRigidbodyToWriter(player.GetComponent<Rigidbody>(), writer);
		SerializeTransformToWriter(playerCamera.transform, writer);
		SerializeTransformToWriter(redCube.transform, writer);
		SerializeRigidbodyToWriter(redCube.GetComponent<Rigidbody>(), writer);
		SerializeTransformToWriter(greenCube.transform, writer);
		SerializeRigidbodyToWriter(greenCube.GetComponent<Rigidbody>(), writer);
		SerializeTransformToWriter(blueCube.transform, writer);
		SerializeRigidbodyToWriter(blueCube.GetComponent<Rigidbody>(), writer);
		JCloudDocumentOperation operation = JCloudDocument.DirectoryExists("Savegames");
		while (!operation.finished)
		{
			yield return null;
		}
		if (operation.error.HasValue)
		{
			HandleDocumentError(operation.error.Value);
			yield break;
		}
		if (!(bool)operation.result)
		{
			operation = JCloudDocument.DirectoryCreate("Savegames");
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
		operation = JCloudDocument.FileWriteAllBytes("Savegames/My saved game.sav", dataStream.GetBuffer());
		while (!operation.finished)
		{
			yield return null;
		}
		if (operation.error.HasValue)
		{
			HandleDocumentError(operation.error.Value);
		}
	}

	private void SerializeTransformToWriter(Transform tr, BinaryWriter writer)
	{
		writer.Write(tr.localPosition.x);
		writer.Write(tr.localPosition.y);
		writer.Write(tr.localPosition.z);
		writer.Write(tr.localEulerAngles.x);
		writer.Write(tr.localEulerAngles.y);
		writer.Write(tr.localEulerAngles.z);
		writer.Write(tr.localScale.x);
		writer.Write(tr.localScale.y);
		writer.Write(tr.localScale.z);
	}

	private void DeserializeTransformFromReader(Transform tr, BinaryReader reader)
	{
		tr.localPosition = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
		tr.localEulerAngles = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
		tr.localScale = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
	}

	private void SerializeRigidbodyToWriter(Rigidbody body, BinaryWriter writer)
	{
		writer.Write(body.velocity.x);
		writer.Write(body.velocity.y);
		writer.Write(body.velocity.z);
		writer.Write(body.angularVelocity.x);
		writer.Write(body.angularVelocity.y);
		writer.Write(body.angularVelocity.z);
	}

	private void DeserializeRigidbodyFromReader(Rigidbody body, BinaryReader reader)
	{
		body.velocity = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
		body.angularVelocity = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
	}

	private void HandleDocumentError(JCloudDocumentError documentError)
	{
		switch (documentError)
		{
		case JCloudDocumentError.InvalidPlatform:
			error = "No file access allowed on this platform.";
			break;
		case JCloudDocumentError.PluginError:
		case JCloudDocumentError.InvalidArguments:
		case JCloudDocumentError.NativeError:
			error = "An error ocurred while loading game data. Please retry. Error: " + documentError;
			break;
		case JCloudDocumentError.DocumentNotFound:
			error = "There is no saved game present on this device. Start a new game.";
			break;
		case JCloudDocumentError.DownloadTimeout:
			error = "Could not download the save game data. Please retry.";
			break;
		case JCloudDocumentError.InvalidVersionIdentifier:
		case JCloudDocumentError.InvalidVersionsHash:
			error = "An error occured while handling conflict versions of your save game data. Please retry.";
			break;
		case JCloudDocumentError.OverwriteError:
		case JCloudDocumentError.CloudUnavailable:
			break;
		}
	}
}
