using System.Collections;
using System.Collections.Generic;

public class RTSGameUnitGamePlayNetworkingData{
	//
	public string unitID;
	public float positionX;
	public float positionY;
	public float positionZ;
	//
	public float eulerX;
	public float eulerY;
	public float eulerZ;
	//
	public string behaviour;
    //
    public override string ToString() {
		//
        string str = "";
		str+="unitID:"+unitID+"\n";
		//
		str+="positionX:"+positionX+"\n";
		str+="positionY:"+positionY+"\n";
		str+="positionZ:"+positionZ+"\n";
		//
		str+="eulerX:"+eulerX+"\n";
		str+="eulerY:"+eulerY+"\n";
		str+="eulerZ:"+eulerZ+"\n";
		//
        return str;
    }
}
