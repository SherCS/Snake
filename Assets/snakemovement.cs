using UnityEngine;
using System.Collections.Generic;

public class snakemovement : MonoBehaviour
{//A Vector can move in both x and y axis, we create the vector to move our snake and name it _direction 
  private Vector2 _direction = Vector2.right;

  private List<Transform> _segments = new List<Transform>(); 
  public Transform segmentPrefab;
  public int initialSize = 3;
  private void Start()
  {
    ResetState();
  }

  private void Update()
  {//We are assigning the directions each key moves the snake
    if(Input.GetKeyDown(KeyCode.W)){
        _direction = Vector2.up;
    } else if(Input.GetKeyDown(KeyCode.A)){
        _direction = Vector2.left;
    }else if(Input.GetKeyDown(KeyCode.S)){
        _direction = Vector2.down;
    }else if(Input.GetKeyDown(KeyCode.D)){
        _direction = Vector2.right;
    }
    
  }
        //this section of code allows us to track the snakes location 
  private void FixedUpdate()
    { //this loop goes in reverse order to arrange the snake, going thru each prefab and making it follow the one before it// 
        for(int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

    this.transform.position = new Vector3(
        //The Mathf will ensure that all our numbers are whole// 
        Mathf.Round(this.transform.position.x) + _direction.x,
        Mathf.Round(this.transform.position.y) + _direction.y,
        0.0f);
    
    }
//creating a growing class to grow the snake each time it eats// 
    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }

    public void ResetState()
    {
        for(int i = 1; i < _segments.Count; i++){
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(this.transform);

        for(int i = 1; i < initialSize; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }

        this.transform.position = Vector3.zero;
    }
//this section of the code is responsible for collisions// 
    private void OnTriggerEnter2D(Collider2D other)
   {
    if(other.tag == "Food"){
        Grow();
        ScoreManager.instance.AddPoint();
    }else if(other.tag == "Obstacle"){
        ResetState();
        ScoreManager.instance.HighScoreTracker();
       
        
    }

   }

}
