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
glTF_VCAST_vci_joints_Serializer
{


public static void Serialize(JsonFormatter f, glTF_VCAST_vci_joints value)
{
    f.BeginMap();


    if(value.joints!=null&&value.joints.Count>=1){
        f.Key("joints");                
        Serialize_vci_joints(f, value.joints);
    }

    f.EndMap();
}

public static void Serialize_vci_joints(JsonFormatter f, List<glTF_VCAST_vci_joint> value)
{
    f.BeginList();

    foreach(var item in value)
    {
    Serialize_vci_joints_ITEM(f, item);

    }
    f.EndList();
}

public static void Serialize_vci_joints_ITEM(JsonFormatter f, glTF_VCAST_vci_joint value)
{
    f.BeginMap();


    if(!string.IsNullOrEmpty(value.type)){
        f.Key("type");                
        f.Value(value.type);
    }

    if(true){
        f.Key("nodeIndex");                
        f.Value(value.nodeIndex);
    }

    if(value.anchor!=null&&value.anchor.Length>=3){
        f.Key("anchor");                
        Serialize_vci_joints__anchor(f, value.anchor);
    }

    if(value.axis!=null&&value.axis.Length>=3){
        f.Key("axis");                
        Serialize_vci_joints__axis(f, value.axis);
    }

    if(true){
        f.Key("autoConfigureConnectedAnchor");                
        f.Value(value.autoConfigureConnectedAnchor);
    }

    if(value.connectedAnchor!=null&&value.connectedAnchor.Length>=3){
        f.Key("connectedAnchor");                
        Serialize_vci_joints__connectedAnchor(f, value.connectedAnchor);
    }

    if(true){
        f.Key("enableCollision");                
        f.Value(value.enableCollision);
    }

    if(true){
        f.Key("enablePreprocessing");                
        f.Value(value.enablePreprocessing);
    }

    if(true){
        f.Key("massScale");                
        f.Value(value.massScale);
    }

    if(true){
        f.Key("connectedMassScale");                
        f.Value(value.connectedMassScale);
    }

    if(true){
        f.Key("useSpring");                
        f.Value(value.useSpring);
    }

    if(value.spring!=null){
        f.Key("spring");                
        Serialize_vci_joints__spring(f, value.spring);
    }

    if(true){
        f.Key("useLimits");                
        f.Value(value.useLimits);
    }

    if(value.limits!=null){
        f.Key("limits");                
        Serialize_vci_joints__limits(f, value.limits);
    }

    f.EndMap();
}

public static void Serialize_vci_joints__anchor(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void Serialize_vci_joints__axis(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void Serialize_vci_joints__connectedAnchor(JsonFormatter f, Single[] value)
{
    f.BeginList();

    foreach(var item in value)
    {
    f.Value(item);

    }
    f.EndList();
}

public static void Serialize_vci_joints__spring(JsonFormatter f, Spring value)
{
    f.BeginMap();


    if(true){
        f.Key("spring");                
        f.Value(value.spring);
    }

    if(true){
        f.Key("damper");                
        f.Value(value.damper);
    }

    if(true){
        f.Key("minDistance");                
        f.Value(value.minDistance);
    }

    if(true){
        f.Key("maxDistance");                
        f.Value(value.maxDistance);
    }

    if(true){
        f.Key("tolerance");                
        f.Value(value.tolerance);
    }

    if(true){
        f.Key("targetPosition");                
        f.Value(value.targetPosition);
    }

    f.EndMap();
}

public static void Serialize_vci_joints__limits(JsonFormatter f, Limits value)
{
    f.BeginMap();


    if(true){
        f.Key("min");                
        f.Value(value.min);
    }

    if(true){
        f.Key("max");                
        f.Value(value.max);
    }

    if(true){
        f.Key("bounciness");                
        f.Value(value.bounciness);
    }

    if(true){
        f.Key("bounceMinVelocity");                
        f.Value(value.bounceMinVelocity);
    }

    if(true){
        f.Key("contactDistance");                
        f.Value(value.contactDistance);
    }

    f.EndMap();
}

} // VciSerializer
} // VCI
