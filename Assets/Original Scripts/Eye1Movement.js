#pragma strict

function Start () {

}

function Update () {
var translationX : float = Input.GetAxis("Horizontal");
	var translationY : float = Input.GetAxis("Vertical");
	var fastTranslationX : float = 2 * Input.GetAxis("Horizontal");
	var fastTranslationY : float = 2 * Input.GetAxis("Vertical");
	
	if (Input.GetKey(KeyCode.LeftShift))
		{
		transform.Translate(fastTranslationX + fastTranslationY, 0, fastTranslationY - fastTranslationX);
		}
	else
		{
		transform.Translate(translationX + translationY, 0, translationY - translationX); 
		}

	////////////////////
	//mouse scrolling
	
	var mousePosX = Input.mousePosition.x;
	var mousePosY = Input.mousePosition.y;
	var scrollDistance : int = 5;
	var scrollSpeed : float = 70;

	//Horizontal camera movement
	//if (mousePosX < scrollDistance)
		//horizontal, left
		//{ 
		//transform.Translate(-1, 0, 1);
		//} 
	//if (mousePosX >= Screen.width - scrollDistance)
		//horizontal, right
		//{ 
		//transform.Translate(1, 0, -1);
		//} 

	//Vertical camera movement
	//if (mousePosY < scrollDistance)
		//scrolling down
		//{
		//transform.Translate(-1, 0, -1);
		//} 
	//if (mousePosY >= Screen.height - scrollDistance)
		//scrolling up
		//{
		//transform.Translate(1, 0, 1);
		//}
	
	////////////////////
	//zooming
	var Eye : GameObject = GameObject.Find("Eye1");
	
	//
	if (Input.GetAxis("Mouse ScrollWheel") > 0 && Eye.GetComponent.<Camera>().orthographicSize > 4)
		{
		Eye.GetComponent.<Camera>().orthographicSize = Eye.GetComponent.<Camera>().orthographicSize - 0.5;
		}
	
	//
	if (Input.GetAxis("Mouse ScrollWheel") < 0 && Eye.GetComponent.<Camera>().orthographicSize < 80)
		{
		Eye.GetComponent.<Camera>().orthographicSize = Eye.GetComponent.<Camera>().orthographicSize + 0.5;
		}

	//default zoom
	if (Input.GetKeyDown(KeyCode.Mouse2))
		{
		Eye.GetComponent.<Camera>().orthographicSize = 40;
		}
}