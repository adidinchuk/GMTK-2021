using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BFSearch
    
{
    public static int Search(Graph<ShipPart> graph, ShipPart start)
    {
        var frontier = new Queue<ShipPart>();
        frontier.Enqueue(start);

        var reached = new HashSet<ShipPart>();
        reached.Add(start);

        int score = graph.GetScore(start);

        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();

            foreach (var next in graph.Neighbors(current))
            {
                if (!reached.Contains(next))
                {
                    score += graph.GetScore(start); 
                    frontier.Enqueue(next);
                    reached.Add(next);
                }
            }
        }

        return score;
    }
}
