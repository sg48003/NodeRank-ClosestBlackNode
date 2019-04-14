using System;
using System.Collections.Generic;
using System.Linq;

namespace ClosestBlackNode
{
    class ClosestBlackNode
    {
        public class Node
        {
            public int Number;
            public Color Color;
            public Boolean Visited;
            public Node ClosestBlackNode;
            public int DistanceToClosestBlackNode;
            public List<Node> ConnectedNodes = new List<Node>();

        }

        public enum Color
        {
            White,
            Black
        }

        static void Main(string[] args)
        {

            #region Testing

            //int numberOfNodes;
            //double accidentalWalkerProbability;
            //var listOfNodes = new List<Node>();

            //const Int32 BufferSize = 128;
            //using (var fileStream = File.OpenRead("S:\\Projekti\\NodeRank_ClosestBlackNode\\NodeRank_ClosestBlackNode\\bin\\Debug\\R.in"))
            //using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            //{

                //var line1 = Console.ReadLine().Split();
                //var numberOfNodes = Convert.ToInt32(line1[0]);
                //var numberOfEdges = Convert.ToInt32(line1[1]);

                //var listOfNodes = new List<Node>();
                //var listOfBlackNodes = new List<Node>();
                //for (var i = 0; i < numberOfNodes; i++)
                //{
                //    var color = Convert.ToInt32(Console.ReadLine());
                //    var node = new Node { Number = i };
                //    if (color == (int)Color.Black)
                //    {
                //        node.Color = Color.Black;
                //        node.DistanceToClosestBlackNode = 0;
                //        node.ClosestBlackNode = node;
                //        listOfBlackNodes.Add(node);
                //    }
                //    else
                //    {
                //        node.Color = Color.White;
                //        node.DistanceToClosestBlackNode = int.MaxValue;
                //    }
                //    listOfNodes.Add(node);
                //}

                //for (var i = 0; i < numberOfEdges; i++)
                //{
                //    var listOfEdges = Console.ReadLine().Split().Select(int.Parse).ToList();
                //    listOfNodes[listOfEdges[0]].ConnectedNodes.Add(listOfNodes[listOfEdges[1]]);
                //    listOfNodes[listOfEdges[1]].ConnectedNodes.Add(listOfNodes[listOfEdges[0]]);
                //}

            //    }

            //}

            #endregion

            #region Initialization

            var line1 = Console.ReadLine().Split();
            var numberOfNodes = Convert.ToInt32(line1[0]);
            var numberOfEdges = Convert.ToInt32(line1[1]);

            var listOfNodes = new List<Node>();
            var listOfBlackNodes = new List<Node>();
            for (var i = 0; i < numberOfNodes; i++)
            {
                var color = Convert.ToInt32(Console.ReadLine());
                var node = new Node { Number = i };
                if (color == (int)Color.Black)
                {
                    node.Color = Color.Black;
                    node.DistanceToClosestBlackNode = 0;
                    node.ClosestBlackNode = node;
                    listOfBlackNodes.Add(node);
                }
                else
                {
                    node.Color = Color.White;
                    node.DistanceToClosestBlackNode = int.MaxValue;
                }
                listOfNodes.Add(node);
            }

            for (var i = 0; i < numberOfEdges; i++)
            {
                var listOfEdges = Console.ReadLine().Split().Select(int.Parse).ToList();
                listOfNodes[listOfEdges[0]].ConnectedNodes.Add(listOfNodes[listOfEdges[1]]);
                listOfNodes[listOfEdges[1]].ConnectedNodes.Add(listOfNodes[listOfEdges[0]]);
            }

            #endregion

            #region Closest Black Node

            while (listOfBlackNodes.Count > 0)
            {
                var node = listOfBlackNodes[0];
                node.Visited = true;
                foreach (var connectedNode in node.ConnectedNodes)
                {
                    if (connectedNode.Color == Color.White && connectedNode.Visited == false)
                    {
                        connectedNode.Visited = true;
                        listOfBlackNodes.Add(connectedNode);
                        if (connectedNode.DistanceToClosestBlackNode >= node.DistanceToClosestBlackNode)
                        {
                            listOfNodes[connectedNode.Number].DistanceToClosestBlackNode = node.DistanceToClosestBlackNode + 1;
                            listOfNodes[connectedNode.Number].ClosestBlackNode = node.ClosestBlackNode;
                        }
                    }
                }
                listOfBlackNodes.Remove(node);

            }

            #endregion

            #region Output

            foreach (var node in listOfNodes)
            {
                if (node.DistanceToClosestBlackNode == int.MaxValue)
                {
                    Console.WriteLine("-1 -1");
                }
                else
                {
                    Console.WriteLine(node.ClosestBlackNode.Number + " " + node.DistanceToClosestBlackNode);
                }
            }

            #endregion

        }
    }
}
