local lazer = vci.assets.GetSubItem("Lazer")
local stick = vci.assets.GetSubItem("Stick")
local lightSource = stick.GetLocalScale().z / 2
function onGrab(use)
     print("Grab : "..use)
     print("SubItemの "..use.." がGrab状態になりました。")
     print("lightSource : "..lightSource)
     
     -- local scale = Vector3.__new(0.005, 0.005, 10)
     -- local pos = Vector3.__new(0, 1, lightSource + 10/2)
     -- lazer.SetLocalScale(scale)
     -- lazer.SetLocalPosition(pos)
end

function onUngrab(use)
     print("Grab : "..use)
     print("SubItemの "..use.." がGrab状態から離れました。")
     -- local scale = Vector3.__new(0.005, 0.005, 0)
     -- local pos = Vector3.__new(0, 0, 0)
     -- lazer.SetLocalScale(scale)
     -- lazer.SetLocalPosition(pos)
end

function onCollisionEnter(item, hit)
     print("Collision Enter")
     print(string.format("%s <= %s", item, hit))
end

function onCollisionExit(item, hit)
     print("Collision Exit")
     print(string.format("%s <= %s", item, hit))
end