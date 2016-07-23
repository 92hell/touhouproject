#pragma strict
	var cam1 : GameObject;
	var cam2 : GameObject;
	var cam3 : GameObject;
	var cam4 : GameObject;

function Start () {

}

function Update () {
	if(Input.GetKeyDown(KeyCode.Alpha1))
	{
		cam1.active=true;
		cam2.active=false;
		cam3.active=false;
		cam4.active=false;
	}
	else if(Input.GetKeyDown(KeyCode.Alpha2))
	{
		cam1.active=false;
		cam2.active=true;
		cam3.active=false;
		cam4.active=false;
	}
	else if(Input.GetKeyDown(KeyCode.Alpha3))
	{
		cam1.active=false;
		cam2.active=false;
		cam3.active=true;
		cam4.active=false;
	}
	else if(Input.GetKeyDown(KeyCode.Alpha4))
	{
		cam1.active=false;
		cam2.active=false;
		cam3.active=false;
		cam4.active=true;
	}
}