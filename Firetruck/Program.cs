/*
    Author: Group 6
    Members:
    1. Abhilash Kundoor
    2. David Moreno-Bautista
    3. Nida Tuz Zohra
    4. Tom Cieslukowski
*/

using System;
using System.Collections.Generic;

namespace Firetruck
{
    class Program
    {
        static Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
        static int caseCount = 0;
        static int routeCount = 0;

        static void Main(string[] args)
        {
            const int FIRESTATION = 1;

            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(@"input.txt");
            while ((line = file.ReadLine()) != null)
            {
                // read first line which is where the fire is at
                int fireCorner = int.Parse(line);

                line = file.ReadLine();
                string[] corners = line.Split(" ");
                while (int.Parse(corners[0]) != 0) //checking for end of list
                {
                    int corner1 = int.Parse(corners[0]);
                    int corner2 = int.Parse(corners[1]);

                    try {
                        // if the first corner already has others adjacent, add this second one
                        graph[corner1].Add(corner2);
                    }
                    catch
                    {
                        // otherwise create a new list for it to hold its adjacent corners
                        List<int> connections = new List<int>() { corner2 };
                        graph.Add(corner1, connections);
                    }

                    try
                    {
                        // if thae second corner already has others adjacent, add this first one
                        graph[corner2].Add(corner1);
                    }
                    catch
                    {
                        // otherwise create a new list for it to hold its adjacent corners
                        List<int> connections = new List<int>() { corner1 };
                        graph.Add(corner2, connections);
                    }

                    // continue the while loop
                    line = file.ReadLine();
                    corners = line.Split(" ");
                }

                // update case count
                caseCount++;
                Console.WriteLine("CASE {0}:",caseCount);

                // create visited list plus one because we start at 1 not 0
                bool[] visited = new bool[graph.Count+1];
                for (int i=0; i < graph.Count+1; i++)
                {
                    visited[i] = false;
                }
                // initialize path list and send all variables to helper function
                List<int> route = new List<int>();
                calcRoutes(visited, route, FIRESTATION, fireCorner);

                // update total routes
                Console.WriteLine("There are {0} routes from the firestation to streetcorner {1}", routeCount, fireCorner);

                // clear global variables for next case
                routeCount = 0;
                graph.Clear();
            }
        }

        /*
         * takes:
         * visited (array of bool): array holding whether each indexed corner has been visited
         * start (int): the starting corner for the search
         * destination (int): the corner we want to get to
         * route (list of int): the current route we've taken from the first start corner to the current corner
         * 
         * returns:
         * void, but calls function to print the routes from the inital start corner to the destination corner
         *
        */
        static void calcRoutes(bool[] visited, List<int> route, int start, int destination)
        {
            // set current corner as visited and add it to the route
            visited[start] = true;
            route.Add(start);

            if (start == destination)
            {
                // if the current corner is the destination corner, print the route
                routeCount++;
                printRoute(route);
            }
            else
            {
                // otherwise grab each next adjacent corner and set it as the new current corner recursively
                foreach (int corner in graph[start])
                {
                    if (visited[corner] == false)
                         calcRoutes(visited, route, corner, destination);
                }

            }
            // remove current corner from route after recursion to clear it from routes of adjacent corners
            visited[start] = false;
            route.RemoveAt(route.Count - 1);
        }

        /*
         * takes:
         * route (list of int): the current route we've taken from the first start corner to the current corner
         * 
         * returns:
         * void, but prints the routes from the inital start corner to the destination corner
         *
        */
        static void printRoute(List<int> route)
        {
            foreach(int corner in route)
            {
                Console.Write("{0}   ",corner);
            }
            Console.WriteLine();
        }
    }
}
