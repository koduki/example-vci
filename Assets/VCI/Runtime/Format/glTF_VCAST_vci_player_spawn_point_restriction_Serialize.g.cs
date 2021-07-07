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
glTF_VCAST_vci_player_spawn_point_restriction_Serializer
{


public static void Serialize(JsonFormatter f, glTF_VCAST_vci_player_spawn_point_restriction value)
{
    f.BeginMap();


    if(value.playerSpawnPointRestriction!=null){
        f.Key("playerSpawnPointRestriction");                
        Serialize_vci_playerSpawnPointRestriction(f, value.playerSpawnPointRestriction);
    }

    f.EndMap();
}

public static void Serialize_vci_playerSpawnPointRestriction(JsonFormatter f, glTF_VCAST_vci_PlayerSpawnPointRestriction value)
{
    f.BeginMap();


    if(!string.IsNullOrEmpty(value.rangeOfMovementRestriction)){
        f.Key("rangeOfMovementRestriction");                
        f.Value(value.rangeOfMovementRestriction);
    }

    if(true){
        f.Key("limitRadius");                
        f.Value(value.limitRadius);
    }

    if(true){
        f.Key("limitRectLeft");                
        f.Value(value.limitRectLeft);
    }

    if(true){
        f.Key("limitRectRight");                
        f.Value(value.limitRectRight);
    }

    if(true){
        f.Key("limitRectForward");                
        f.Value(value.limitRectForward);
    }

    if(true){
        f.Key("limitRectBackward");                
        f.Value(value.limitRectBackward);
    }

    if(!string.IsNullOrEmpty(value.postureRestriction)){
        f.Key("postureRestriction");                
        f.Value(value.postureRestriction);
    }

    if(true){
        f.Key("seatHeight");                
        f.Value(value.seatHeight);
    }

    f.EndMap();
}

} // VciSerializer
} // VCI
