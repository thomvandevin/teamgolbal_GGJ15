using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;

[XmlRoot("enemycollection")]
public class EnemyContainer {

	//public fields
    [XmlArray("enemies")]
    [XmlArrayItem("enemy")]
    public List<Enemy> Enemies = new List<Enemy>();

    public void Save(string path) {
        var serializer = new XmlSerializer(typeof(EnemyContainer));
        using (var stream = new FileStream(path, FileMode.Create)) {
            serializer.Serialize(stream, this);
        }
    }

    public static EnemyContainer Load(string path) {
        var serializer = new XmlSerializer(typeof(EnemyContainer));
        using (var stream = new FileStream(path, FileMode.Open)) {
            return serializer.Deserialize(stream) as EnemyContainer;
        }
    }

    public static EnemyContainer LoadFromText(string text) {
        var serializer = new XmlSerializer(typeof(EnemyContainer));
        return serializer.Deserialize(new StringReader(text)) as EnemyContainer;
    }
	
}
