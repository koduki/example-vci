﻿
using System;
using System.Collections.Generic;
using UniJSON;
using UnityEngine;
using UniGLTF;
using VCI;
using Object = System.Object;

namespace VCI {

public static class 
glTF_VCAST_vci_audios_Deserializer
{


public static glTF_VCAST_vci_audios Deserialize(JsonNode parsed)
{
    var value = new glTF_VCAST_vci_audios();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="audios"){
            value.audios = glTF_VCAST_vci_audios_Deserializevci_audios(kv.Value);
            continue;
        }

    }
    return value;
}

public static List<VCI.glTF_VCAST_vci_audio> glTF_VCAST_vci_audios_Deserializevci_audios(JsonNode parsed)
{
    var value = new List<glTF_VCAST_vci_audio>();
    foreach(var x in parsed.ArrayItems())
    {
        value.Add(glTF_VCAST_vci_audios_Deserializevci_audios_ITEM(x));
    }
	return value;
}
public static glTF_VCAST_vci_audio glTF_VCAST_vci_audios_Deserializevci_audios_ITEM(JsonNode parsed)
{
    var value = new glTF_VCAST_vci_audio();

    foreach(var kv in parsed.ObjectItems())
    {
        var key = kv.Key.GetString();

        if(key=="name"){
            value.name = kv.Value.GetString();
            continue;
        }

        if(key=="bufferView"){
            value.bufferView = kv.Value.GetInt32();
            continue;
        }

        if(key=="mimeType"){
            value.mimeType = kv.Value.GetString();
            continue;
        }

    }
    return value;
}

} // VciDeserializer
} // VCI
