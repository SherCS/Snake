using UnityEngine;

public class food : MonoBehaviour
{
    //We created a Grid using Unitys boxcollider then named it gamegrid//
   public BoxCollider2D gameGrid;  

   private void Start(){
    RandomizePosition();
   }
    //We randomize the generation of food within the grid bounds// 
   private void RandomizePosition(){
    Bounds bounds = this.gameGrid.bounds;

    float x = Random.Range(bounds.min.x, bounds.max.x);
    float y = Random.Range(bounds.min.y, bounds.max.y);

    this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
   }
    //Here we are establishing the regeneration of food after colision with snake// 
   private void OnTriggerEnter2D(Collider2D other)
   {
    if(other.tag == "Player"){
        RandomizePosition();
    }

   }
}
