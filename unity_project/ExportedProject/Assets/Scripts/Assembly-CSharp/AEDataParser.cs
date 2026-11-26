using System;
using System.Xml;
using UnityEngine;

public static class AEDataParser
{
	private static float frameDuration;

	public static AEAnimationDataNew ParseAnimationData(string data)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(data);
		AEAnimationDataNew aEAnimationDataNew = new AEAnimationDataNew();
		XmlNode xmlNode = xmlDocument.SelectSingleNode("after_affect_animation_doc");
		XmlNode node = xmlNode.SelectSingleNode("meta");
		aEAnimationDataNew.frameDuration = GetFloat(node, "frameDuration");
		aEAnimationDataNew.totalFrames = GetInt(node, "totalFrames");
		aEAnimationDataNew.duration = GetFloat(node, "duration");
		frameDuration = aEAnimationDataNew.frameDuration;
		XmlNode composition = xmlNode.SelectSingleNode("composition");
		aEAnimationDataNew.composition = ParseComposition(composition);
		XmlNode xmlNode2 = xmlNode.SelectSingleNode("sub_items");
		XmlNodeList xmlNodeList = xmlNode2.SelectNodes("composition");
		foreach (XmlNode item in xmlNodeList)
		{
			aEAnimationDataNew.AddComposition(ParseComposition(item));
		}
		return aEAnimationDataNew;
	}

	public static AECompositionNew ParseComposition(XmlNode composition)
	{
		AECompositionNew aECompositionNew = new AECompositionNew();
		aECompositionNew.id = Convert.ToInt32(composition.Attributes.GetNamedItem("id").Value);
		aECompositionNew.width = Convert.ToSingle(composition.Attributes.GetNamedItem("w").Value);
		aECompositionNew.height = Convert.ToSingle(composition.Attributes.GetNamedItem("h").Value);
		XmlNode node = composition.SelectSingleNode("meta");
		aECompositionNew.totalFrames = GetInt(node, "totalFrames");
		aECompositionNew.frameDuration = frameDuration;
		aECompositionNew.duration = aECompositionNew.frameDuration * (float)aECompositionNew.totalFrames;
		XmlNodeList xmlNodeList = composition.SelectNodes("layer");
		foreach (XmlNode item in xmlNodeList)
		{
			AELayer aELayer = new AELayer();
			string value = item.Attributes.GetNamedItem("type").Value;
			if (value.Equals("Composition"))
			{
				aELayer.type = AELayerType.COMPOSITION;
				aELayer.id = Convert.ToInt32(item.Attributes.GetNamedItem("id").Value);
			}
			else if (value.Equals("Footage"))
			{
				aELayer.type = AELayerType.FOOTAGE;
				aELayer.source = item.Attributes.GetNamedItem("source").Value;
			}
			aELayer.index = int.Parse(item.Attributes.GetNamedItem("index").Value);
			if (item.Attributes.GetNamedItem("parent").Value != "none")
			{
				aELayer.parent = int.Parse(item.Attributes.GetNamedItem("parent").Value);
			}
			else
			{
				aELayer.parent = 0;
			}
			aELayer.width = Convert.ToInt32(item.Attributes.GetNamedItem("w").Value);
			aELayer.height = Convert.ToInt32(item.Attributes.GetNamedItem("h").Value);
			aELayer.name = item.Attributes.GetNamedItem("name").Value;
			string value2 = item.Attributes.GetNamedItem("blending").Value;
			aELayer.blending = (AELayerBlendingType)Enum.Parse(typeof(AELayerBlendingType), value2);
			float timeIn = GetFloat(item, "inPoint");
			float timeOut = GetFloat(item, "outPoint");
			aELayer.SetInOutTime(timeIn, timeOut, aECompositionNew);
			XmlNodeList xmlNodeList2 = item.SelectNodes("keyframe");
			AEFrame aEFrame = null;
			AEFrame lastFrame = null;
			foreach (XmlNode item2 in xmlNodeList2)
			{
				aEFrame = new AEFrame();
				aEFrame.index = int.Parse(item2.Attributes.GetNamedItem("frame").Value);
				XmlNode xmlNode3 = item2.SelectSingleNode("source");
				XmlNodeList xmlNodeList3 = xmlNode3.SelectNodes("property");
				foreach (XmlNode item3 in xmlNodeList3)
				{
					switch (item3.Attributes.GetNamedItem("name").Value)
					{
					case "Anchor Point":
						aEFrame.pivot = new Vector3(GetFloat(item3, "x"), GetFloat(item3, "y"), GetFloat(item3, "z"));
						break;
					case "Position":
						aEFrame.SetPosition(new Vector3(GetFloat(item3, "x"), GetFloat(item3, "y"), GetFloat(item3, "z")));
						break;
					case "Scale":
						aEFrame.scale = new Vector3(GetFloat(item3, "x"), GetFloat(item3, "y"), GetFloat(item3, "z")) * 0.01f;
						break;
					case "Rotation":
						aEFrame.rotation = GetFloat(item3, "val");
						break;
					case "Opacity":
						aEFrame.opacity = GetFloat(item3, "val") * 0.01f;
						break;
					}
				}
				aEFrame.isVisible = aEFrame.scale.x != 0f && aEFrame.scale.y != 0f && aEFrame.opacity > 0f;
				aEFrame.CompareToFrame(lastFrame);
				lastFrame = aEFrame;
				aELayer.AddFrame(aEFrame);
			}
			aECompositionNew.AddLayer(aELayer);
		}
		return aECompositionNew;
	}

	public static float GetFloat(XmlNode node, string name)
	{
		return Convert.ToSingle(node.Attributes.GetNamedItem(name).Value);
	}

	public static int GetInt(XmlNode node, string name)
	{
		return Convert.ToInt32(node.Attributes.GetNamedItem(name).Value);
	}
}
