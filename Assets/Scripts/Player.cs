using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{

    private RaycastHit hit;

    public bool selected;
    private bool createPlaceHolder;
    public GameObject ballPlaceHolder;
    private GameObject[] directionObjects; //direction placeholder objects
    private GameManager gameManager;
    private Vector3 initialPos;
        
    private int playerDirection;
    private int walls;
    void Start()
    {
        initialPos = transform.position;
        directionObjects = new GameObject[4]; //left, right, up, down
        playerDirection = this.gameObject.name.Contains("2") ? -1 : 1;
        walls = 10;
        selected = false;
        createPlaceHolder = false;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        string playerName = this.gameObject.name.Replace("(Clone)", "");
        Debug.Log(HasObstacle(new Vector3(0, -1, 0)));
    }
   

    private bool IsMyTurn()
    {
        string playerName = this.gameObject.name.Replace("(Clone)", "");
        return GameObject.Find("PlayerTurn").GetComponent<TextMeshProUGUI>().text.Contains(playerName[playerName.Length -  1]);
    }



    public int ShortestDistanceToGoal()
    {
        return 0;
    }


    public int Heuristic()
    {
        if(this.gameObject.name.Replace("(Clone)", "") == "Player1")
        {
            return (int)((4.5 - transform.position.y));
        }
        else if (this.gameObject.name.Replace("(Clone)", "") == "Player2")
        {
            return (int)((transform.position.y + 3.5));
        }
        return 100;
    }

    public int Heuristic(double y)
    {
        if (this.gameObject.name.Replace("(Clone)", "") == "Player1")
        {
            return (int)((4.5 - y));
        }
        else if (this.gameObject.name.Replace("(Clone)", "") == "Player2")
        {
            return (int)((y + 3.5));
        }
        return 100;
    }







    public List<string> BFS()
    {
        
        Queue<List<string>> liste = new Queue<List<string>>();
        if (!HasObstacle(transform.position, new Vector3(1,0,0)))
        {
            List<string> temp = new List<string>();
            temp.Add("R");
            liste.Enqueue(temp);
        }
        if (!HasObstacle(transform.position, new Vector3(-1,0,0)))
        {
            List<string> temp = new List<string>();
            temp.Add("L");
            liste.Enqueue(temp);
        }
        if (!HasObstacle(transform.position, new Vector3(0,1,0)))
        {
            List<string> temp = new List<string>();
            temp.Add("U");
            liste.Enqueue(temp);
        }
        if (!HasObstacle(transform.position, new Vector3(0,-1,0)))
        {
            List<string> temp = new List<string>();
            temp.Add("D");
            liste.Enqueue(temp);
        }
        while (liste.Count != 0)
        {
            List<string> element = liste.Dequeue();
            List<Vector3> visited = new List<Vector3>();
            var position = transform.position;

            foreach (var v in element)
            {
                visited.Add(position);
                if (v == "R")
                {
                    position.x = position.x + 1;
                }
                if (v == "L")
                {
                    position.x = position.x - 1;
                }
                if (v == "U")
                {
                    position.y = position.y + 1;
                }
                if (v == "D")
                {
                    position.y = position.y - 1;
                }
            }
            //visited.Add(position);

            if (Heuristic(position.y) == 0)
            {
                return element;
            }


            if (visited.Contains(position))
            {
                continue;
            }

            if (!HasObstacle(position, new Vector3( 1,0,0))   )
            {
                List<string> temp = new List<string>(element);
                temp.Add("R");
                liste.Enqueue(temp);
            }
            if (!HasObstacle(position, new Vector3(-1,0,0)))
            {
                List<string> temp = new List<string>(element);
                temp.Add("L");
                liste.Enqueue(temp);
            }
            if (!HasObstacle(position, new Vector3(0, 1,0)))
            {
                List<string> temp = new List<string>(element);
                temp.Add("U");
                liste.Enqueue(temp);
            }
            if (!HasObstacle(position, new Vector3(0,-1,0)))
            {
                List<string> temp = new List<string>(element);
                temp.Add("D");
                liste.Enqueue(temp);
            }



        }

        return new List<string>();
    }



    public List<string> DFS()
    {

        Stack<List<string>> liste = new Stack<List<string>>();
        if (!HasObstacle(transform.position, new Vector3(1, 0, 0)))
        {
            List<string> temp = new List<string>();
            temp.Add("R");
            liste.Push(temp);
        }
        if (!HasObstacle(transform.position, new Vector3(-1, 0, 0)))
        {
            List<string> temp = new List<string>();
            temp.Add("L");
            liste.Push(temp);
        }
        if (!HasObstacle(transform.position, new Vector3(0, 1, 0)))
        {
            List<string> temp = new List<string>();
            temp.Add("U");
            liste.Push(temp);
        }
        if (!HasObstacle(transform.position, new Vector3(0, -1, 0)))
        {
            List<string> temp = new List<string>();
            temp.Add("D");
            liste.Push(temp);
        }
        while (liste.Count != 0)
        {
            List<string> element = liste.Pop();
            List<Vector3> visited = new List<Vector3>();
            var position = transform.position;

            foreach (var v in element)
            {
                visited.Add(position);
                if (v == "R")
                {
                    position.x = position.x + 1;
                }
                if (v == "L")
                {
                    position.x = position.x - 1;
                }
                if (v == "U")
                {
                    position.y = position.y + 1;
                }
                if (v == "D")
                {
                    position.y = position.y - 1;
                }
            }
            //visited.Add(position);

            if (Heuristic(position.y) == 0)
            {
                return element;
            }


            if (visited.Contains(position))
            {
                continue;
            }

            if (!HasObstacle(position, new Vector3(1, 0, 0)))
            {
                List<string> temp = new List<string>(element);
                temp.Add("R");
                liste.Push(temp);
            }
            if (!HasObstacle(position, new Vector3(-1, 0, 0)))
            {
                List<string> temp = new List<string>(element);
                temp.Add("L");
                liste.Push(temp);
            }
            if (!HasObstacle(position, new Vector3(0, 1, 0)))
            {
                List<string> temp = new List<string>(element);
                temp.Add("U");
                liste.Push(temp);
            }
            if (!HasObstacle(position, new Vector3(0, -1, 0)))
            {
                List<string> temp = new List<string>(element);
                temp.Add("D");
                liste.Push(temp);
            }



        }

        return new List<string>();
    }


    public List<string> AStar()
        {
            
            Queue<List<string>> liste = new Queue<List<string>>();
            if (!HasObstacle(transform.position, new Vector3(1,0,0)))
            {
                List<string> temp = new List<string>();
                temp.Add("R");
                liste.Enqueue(temp);
            }
            if (!HasObstacle(transform.position, new Vector3(-1,0,0)))
            {
                List<string> temp = new List<string>();
                temp.Add("L");
                liste.Enqueue(temp);
            }
            if (!HasObstacle(transform.position, new Vector3(0,1,0)))
            {
                List<string> temp = new List<string>();
                temp.Add("U");
                liste.Enqueue(temp);
            }
            if (!HasObstacle(transform.position, new Vector3(0,-1,0)))
            {
                List<string> temp = new List<string>();
                temp.Add("D");
                liste.Enqueue(temp);
            }
            while (liste.Count != 0)
            {
                List<string> element = liste.Dequeue();
                List<Vector3> visited = new List<Vector3>();
                var position = transform.position;

                foreach (var v in element)
                {
                    visited.Add(position);
                    if (v == "R")
                    {
                        position.x = position.x + 1;
                    }
                    if (v == "L")
                    {
                        position.x = position.x - 1;
                    }
                    if (v == "U")
                    {
                        position.y = position.y + 1;
                    }
                    if (v == "D")
                    {
                        position.y = position.y - 1;
                    }
                }
                //visited.Add(position);

                if (Heuristic(position.y) == 0)
                {
                    return element;
                }


                if (visited.Contains(position))
                {
                    continue;
                }


                double hR = 0;
                double hL = 0;
                double hU = 0;
                double hD = 0;

                double minH = 0;

                bool minHeuristicR = false;
                bool minHeuristicL = false;
                bool minHeuristicU = false;
                bool minHeuristicD = false;

                foreach (var v in element)
                {
                    visited.Add(position);
                    if (v == "R")    
                        {hR = Heuristic(position.y);}
                        
                    if (v == "L")    
                        {hL = Heuristic(position.y);}
                            
                    if (v == "U")    
                        {hU = Heuristic(position.y);}
                        
                    if (v == "D")    
                        {hD = Heuristic(position.y);}

                    
                    minH = Math.Min(Math.Min(Math.Min(hR, hL) ,hU), hD);

                    if (minH == hR)    
                        {minHeuristicR = true;}
                        
                    if (minH == hL)    
                        {minHeuristicL = true;}
                            
                    if (minH == hU)    
                        {minHeuristicU = true;}
                        
                    if (minH == hD)    
                        {minHeuristicD = true;}
                
                }
            
                if (!HasObstacle(position, new Vector3( 1,0,0)) && minHeuristicR == true)
                {
                    List<string> temp = new List<string>(element);
                    temp.Add("R");
                    liste.Enqueue(temp);
                }
                if (!HasObstacle(position, new Vector3(-1,0,0)) && minHeuristicL == true)
                {
                    List<string> temp = new List<string>(element);
                    temp.Add("L");
                    liste.Enqueue(temp);
                }
                if (!HasObstacle(position, new Vector3(0, 1,0)) && minHeuristicU == true)
                {
                    List<string> temp = new List<string>(element);
                    temp.Add("U");
                    liste.Enqueue(temp);
                }
                if (!HasObstacle(position, new Vector3(0,-1,0)) && minHeuristicD == true)
                {
                    List<string> temp = new List<string>(element);
                    temp.Add("D");
                    liste.Enqueue(temp);
                }
            }
            return new List<string>();
        }




    void Update()
    {
        //Debug.Log(HasObstacle(new Vector3(initialPos.x, initialPos.y - 1, initialPos.z)));
        


        if (gameManager.GameEnded() ||gameManager.GamePaused())
        {
            return;
        }


        if (Input.GetMouseButtonDown(0) && IsMyTurn())
        {
            bool mousePlayer = MouseOnPlayer();
            if (mousePlayer && !selected)
            {
                selected = true;
                createPlaceHolder = true;
            }

            else if (mousePlayer && selected)
            {
                selected = false;
                ClearPlaceHolders();
            }

            GameObject go = MouseOnPlaceHolder();
            if (go && go.name.StartsWith("BallPlaceHolder") && selected)
            {
                gameManager.ChangeTurn();
                transform.position = go.transform.position;
                ClearPlaceHolders();
                selected = false;
            }
        }

        if (selected && createPlaceHolder)
        {
            
            if (!HasObstacle(Vector3.up * playerDirection))
            {
                directionObjects[0] = Instantiate(ballPlaceHolder, (Vector3.up * playerDirection) + transform.position, Quaternion.identity);
            }
            
            if (!HasObstacle(Vector3.left))
            {   
                directionObjects[1] = Instantiate(ballPlaceHolder, Vector3.left + transform.position, Quaternion.identity);
            }
            if (!HasObstacle(Vector3.right))
            {
                directionObjects[2] = Instantiate(ballPlaceHolder, Vector3.right + transform.position, Quaternion.identity);
            }
            if (!HasObstacle(Vector3.down * playerDirection))
            {
                directionObjects[3] = Instantiate(ballPlaceHolder, (Vector3.down * playerDirection) + transform.position, Quaternion.identity);
            }
            createPlaceHolder = false;
        }



    }

    private bool HasObstacle(Vector3 dir)
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(dir), out hit, 1))
        {
            return true;
        }
        else if (!Physics.Raycast(transform.position + dir, new Vector3(0,0,1), out hit, 1))
        {
            return true;
        }
        return false;
    }

    private bool HasObstacle(Vector3 dir, Vector3 dir1)
    {
        if (Physics.Raycast(dir, transform.TransformDirection(dir1), out hit, 1))
        {
            return true;
        }
        else if (!Physics.Raycast(dir + dir1, new Vector3(0, 0, 1), out hit, 1))
        {
            return true;
        }
        return false;
    }

    private void ClearPlaceHolders()
    {
        for (int i = 0; i < directionObjects.Length; i++)
        {
            Destroy(directionObjects[i]);
        }
    }


    private GameObject MouseOnPlaceHolder()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject;
        }
        return null;
    }


    private bool MouseOnPlayer()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject.name.StartsWith(this.gameObject.name);
        }
        return false;
    }

    public bool IsSelected()
    {
        return this.GetComponent<Player>().selected;
    }

    public int GetWallCount()
    {
        return this.GetComponent<Player>().walls;
    }

    public void DecreaseWall()
    {
        this.GetComponent<Player>().walls -= 1;
    }

    public void MoveLeft()
    {
        transform.position = transform.position + Vector3.left;
        gameManager.ChangeTurn();
    }

    public void MoveUp()
    {
        transform.position = transform.position + Vector3.up * playerDirection;
        gameManager.ChangeTurn();
    }

    public void MoveRight()
    {
        transform.position = transform.position + Vector3.right;
        gameManager.ChangeTurn();
    }

    public void MoveDown()
    {
        transform.position = transform.position + Vector3.down * playerDirection;
    }

    public Vector3 GetPlayerPosition()
    {
        return this.transform.position;
    }

    public bool HasReachedEnd()
    {
        return Vector3.Distance(initialPos, transform.position) >= 8; 
    }



}
