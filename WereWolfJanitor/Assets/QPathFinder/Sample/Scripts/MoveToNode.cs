﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace QPathFinder
{
    public class MoveToNode : MonoBehaviour
    {
        public GameObject playerObj;
        public float playerSpeed = 20.0f;
        public bool autoRotateTowardsDestination = true;
        public float playerFloatOffset;     // This is how high the player floats above the ground. 
        public float raycastOriginOffset;   // This is how high above the player u want to raycast to ground. 
        public int raycastDistanceFromOrigin = 40;   // This is how high above the player u want to raycast to ground. 
        public bool thoroughPathFinding = false;    // uses few extra steps in pathfinding to find accurate result. 

        public bool useGroundSnap = false;          // if snap to ground is not used, player goes only through nodes and doesnt project itself on the ground. 


        public QPathFinder.Logger.Level debugLogLevel;
        public float debugDrawLineDuration;


        //starting from here my own variables
        private bool AtStart = false;
        public bool student;
        private List<int> nodesList = new List<int>();
        private int count = 0;

        void Awake()
        {
            QPathFinder.Logger.SetLoggingLevel( debugLogLevel );
            QPathFinder.Logger.SetDebugDrawLineDuration ( debugDrawLineDuration );
        }

        private void Start()
        {
            if (PathFinder.instance == null && PathFinder.instance.graphData != null)
                return;
            {
                int y = 0;
                foreach (var go in PathFinder.instance.graphData.nodes)
                {
                    nodesList.Add(go.autoGeneratedID);
                    y++;
                }
            }
            if (!AtStart&& student)
            {
                MoveTo(PathFinder.instance.graphData.nodes[50]);
                AtStart = true;
                count++;
            }
            if (!student)
            {
                MoveTo(PathFinder.instance.graphData.nodes[50]);
                count++;
            }
        }

        void Update () //everything in update is my own code
        {
            if (count==1)
            {
                StopAllCoroutines();
            }
        /*void OnGUI()
        {
            if ( PathFinder.instance == null && PathFinder.instance.graphData != null ) 
                return;
            {
                int y = 0;
                foreach ( var go in PathFinder.instance.graphData.nodes  )
                {
                    if ( GUI.Button ( new Rect ( Screen.width - 150, y*30, 150, 30), "Node:" + go.autoGeneratedID.ToString() ))
                    {
                        MoveTo( go );
                    }
                    y++;
                }
            }
        }*/

        /*public void TriggerMoveTo(String boolName)
        {
            if (boolName.Equals("colliding1"))
            {
                MoveTo(PathFinder.instance.graphData.nodes[6]);
            }
            if (boolName.Equals("colliding2"))
            {
                MoveTo(PathFinder.instance.graphData.nodes[0]);
            }*/
            
        }

        void MoveTo( Node node )
        {
            {
                PathFinder.instance.FindShortestPathOfPoints( playerObj.transform.position, node.Position,  PathFinder.instance.graphData.lineType, 
                    Execution.Asynchronously,
                    SearchMode.Simple,
                    delegate ( List<Vector3> points ) 
                    { 
                        PathFollowerUtility.StopFollowing( playerObj.transform );
                        if ( useGroundSnap )
                        {
                           FollowThePathWithGroundSnap ( points );
                        }
                        else 
                            FollowThePathNormally ( points );
                    }
                 );
            }
        }

        void FollowThePathWithGroundSnap ( List<Vector3> nodes )
        {
            PathFollowerUtility.FollowPathWithGroundSnap ( playerObj.transform, 
                                                        nodes, 
                                                        playerSpeed, 
                                                        autoRotateTowardsDestination,
                                                        Vector3.down, playerFloatOffset, LayerMask.NameToLayer( PathFinder.instance.graphData.groundColliderLayerName ),
                                                        raycastOriginOffset, raycastDistanceFromOrigin );
        }

        void FollowThePathNormally ( List<Vector3> nodes )
        {
            PathFollowerUtility.FollowPath ( playerObj.transform, nodes, playerSpeed, autoRotateTowardsDestination );
        }
    }
}
