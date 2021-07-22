local lazer = vci.assets.GetSubItem("Lazer")
local stick = vci.assets.GetSubItem("Stick")
local dummyLazer = vci.assets.GetSubItem("DummyLazer")
local lightSource = stick.GetLocalScale().z 

print("ver 0.3")

function onGrab(use)
     -- print("Grab : "..use)
     -- print("SubItemの "..use.." がGrab状態になりました。")
     print("lightSource : "..lightSource)
     print("stick-z : "..stick.GetLocalScale().z)
     print("lazer-z1 : "..dummyLazer.GetLocalPosition().z)

     local scale = Vector3.__new(0.005, 0.005, 10)
     local pos = Vector3.__new(0, 0, 0.52 + 2 * scale.z)
     dummyLazer.SetLocalPosition(pos)
     dummyLazer.SetLocalScale(scale)
     print("lazer-z2 : "..dummyLazer.GetLocalPosition().z)

end

function onUngrab(use)
     -- print("Grab : "..use)
     -- print("SubItemの "..use.." がGrab状態から離れました。")

     local scale = Vector3.__new(0.005, 0.005, 0)
     local pos = Vector3.__new(stick.getLocalPosition().x, stick.getLocalPosition().y, 0)
     dummyLazer.SetLocalPosition(pos)
     dummyLazer.SetLocalScale(scale)
end

---毎フレーム位置同期を行う
function updateAll()
     lazer.SetPosition(dummyLazer.GetPosition())
     lazer.SetRotation(dummyLazer.GetRotation())
     lazer.SetLocalScale(dummyLazer.GetLocalScale())

     -- child.SetLocalScale(ITEMS.PARENT.GetLocalScale()) 
end

function onTriggerEnter(item, hit)
     print("Trigger Enter")
     print(string.format("%s <= %s", item, hit))
end

function onTriggerExit(item, hit)
     print("Trigger Exit")
     print(string.format("%s <= %s", item, hit))
end