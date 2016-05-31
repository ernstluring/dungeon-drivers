using UnityEngine;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("CardsLibrary")]
public class CardLibrary {

	[XmlArray("GenericCards"), XmlArrayItem("genericCard")]
	public List<Card> genericCards = new List<Card>();

	[XmlArray("Cards"), XmlArrayItem("card")]
	public List<Card> cards = new List<Card>();
	
	public static CardLibrary Load (TextAsset xmlFile) {
		XmlSerializer serializer = new XmlSerializer(typeof(CardLibrary));
		using (StringReader sr = new StringReader (xmlFile.text)) {
			return (CardLibrary)serializer.Deserialize(sr);
		}
	}
}
