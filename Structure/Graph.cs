using System.Collections.Generic;

public class Graph
{
    public List<Vertex> Vertexs = new List<Vertex>();

    public void AddVertex(Vertex v)
    {
        Vertexs.Add(v);
    }

    public int Dijkstra(Vertex v1, Vertex v2)
    {
        v1.MinDistance = 0;
        PriorityQueue<Vertex> queue = new PriorityQueue<Vertex>();
        queue.Enqueue(v1);
        Dictionary<int, Vertex> map = new Dictionary<int, Vertex>();
        while (queue.Count > 0)
        {
            var v = queue.Dequeue();
            foreach (var edge in v.Edges)
            {
                int distance = edge.Distance + v.MinDistance;
                if(edge.To.MinDistance > distance)
                {
                    edge.To.MinDistance = distance;
                    int i;
                    if (queue.Contains(edge.To, out i))
                    {
                        queue.ShiftUp(i);
                    }
                    else
                    {
                        queue.Enqueue(edge.To);
                    }
                }
            }
            map.Add(v.Index, v);
        }
        
        return map[v2.Index].MinDistance;
    }

    public enum State
    {
        NotVisited,
        Visiting,
        Visited
    }

    public class Edge
    {
        public Edge(Vertex from, Vertex to, int distance = 1)
        {
            From = from;
            To = to;
            Distance = distance;
        }

        public Vertex From
        {
            get;
            private set;
        }

        public Vertex To
        {
            get;
            private set;
        }

        public int Distance
        {
            get;
            private set;
        }

        public override string ToString()
        {
            return "From " + From.Index + " To " + To.Index + " Distance = " + Distance;
        }

    }

    public class Vertex : IPriority
    {
        public Vertex(int index)
        {
            Index = index;
            MinDistance = int.MaxValue;
            State = State.NotVisited;
        }

        public List<Edge> Edges = new List<Edge>();

        public void AddEdge(Vertex to, int distance = 1)
        {
            Edges.Add(new Edge(this, to, distance));
        }

        public int MinDistance
        {
            get;
            set;
        }

        public int Index
        {
            get;
            private set;
        }

        public State State
        {
            get;
            set;
        }

        public int Priority { get { return MinDistance; } }

        public override string ToString()
        {
            return Index.ToString() + " " + MinDistance;
        }
    }
}

