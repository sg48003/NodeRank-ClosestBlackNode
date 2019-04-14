using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NodeRank_ClosestBlackNode
{
    class NodeRank
    {
        public class Node
        {
            public double Rank;
            public List<int> ListOfOutGoingPointers = new List<int>();
            public List<int> ListOfInGoingPointers = new List<int>();

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
            //    var line1 = streamReader.ReadLine().Split();
            //    numberOfNodes = Convert.ToInt32(line1[0]);
            //    accidentalWalkerProbability = Convert.ToDouble(line1[1]);

            //    for (var i = 0; i < numberOfNodes; i++)
            //    {
            //        var node = new Node
            //        {
            //            Rank = (float) 1 / numberOfNodes
            //        };
            //        listOfNodes.Add(node);
            //    }

            //    for (var i = 0; i < numberOfNodes; i++)
            //    {
            //        var listOfEdges = streamReader.ReadLine().Split().Select(int.Parse).ToList();

            //        listOfNodes[i].NumberOfOutGoingPointers = listOfEdges.Count;
            //        foreach (var connectedNode in listOfEdges)
            //        {
            //            listOfNodes[connectedNode].ListOfInGoingPointers.Add(i);
            //        }

            //    }

            //}

            #endregion

            #region Initialization

            var line1 = Console.ReadLine().Split();
            var numberOfNodes = Convert.ToInt32(line1[0]);
            var accidentalWalkerProbability = Convert.ToDouble(line1[1]);

            var listOfNodes = new List<Node>();
            //postavljanje inicijalne vrijednosti ranka svih čvorova
            for (var i = 0; i < numberOfNodes; i++)
            {
                var node = new Node
                {
                    Rank = (double) 1 / numberOfNodes
                };
                listOfNodes.Add(node);
            }

            for (var i = 0; i < numberOfNodes; i++)
            {
                var listOfEdges = Console.ReadLine().Split().Select(int.Parse).ToList();

                listOfNodes[i].ListOfOutGoingPointers = listOfEdges;
                foreach (var connectedNode in listOfEdges)
                {
                    listOfNodes[connectedNode].ListOfInGoingPointers.Add(i);
                }

            }

            #endregion

            #region PageRank

            //može ići do 100 iteracija
            var rank = new double[100 + 1,numberOfNodes];
            //inicijalna vrijednost nulte iteracije svih čvororva
            for (var i = 0; i < numberOfNodes; i++)
            {
                rank[0, i] = (double) 1 / numberOfNodes;
            }

            for (var i = 0; i < 100; i++)
            {
                double S = 0;
                for (var j = 0; j < numberOfNodes; j++)
                {
                    foreach (var pointer in listOfNodes[j].ListOfInGoingPointers)
                    {
                        rank[i + 1,j] += accidentalWalkerProbability * rank[i,pointer] / listOfNodes[pointer].ListOfOutGoingPointers.Count;
                    }
                    S += rank[i + 1, j];
                }

                for (var j = 0; j < numberOfNodes; j++)
                {
                    rank[i + 1, j] += (1 - S) / numberOfNodes;
                }
            }

            #endregion

            #region Output

            var numberOfQueries = Convert.ToInt32(Console.ReadLine());
            for (var i = 0; i < numberOfQueries; i++)
            {
                var queryLine = Console.ReadLine().Split().Select(int.Parse).ToList();
                Console.WriteLine(string.Format("{0:0.0000000000}", rank[queryLine[1], queryLine[0]]));
            }

            #endregion

        }
    }
}
