﻿using System.Collections.Generic;
using RogueSharp.Algorithms;
using System;
using System.Linq;

namespace RogueSharp
{
    /// <summary>
    /// A class which can be used to find shortest path from a source to a destination in a Map
    /// </summary>
    public class PathFinder
    {
        private readonly EdgeWeightedDigraph _graph;
        private readonly IMap _map;
        /// <summary>
        /// Constructs a new PathFinder instance for the specified Map
        /// </summary>
        /// <param name="map">The Map that this PathFinder instance will run shortest path algorithms on</param>
        public PathFinder(IMap map)
        {
            _map = map;
            _graph = new EdgeWeightedDigraph(_map.Width * _map.Height);
            foreach (Cell cell in _map.GetAllCells())
            {
                if (cell.IsWalkable)
                {
                    int v = IndexFor(cell);
                    foreach (Cell neighbor in _map.GetBorderCellsInRadius(cell.X, cell.Y, 1))
                    {
                        if (neighbor.IsWalkable)
                        {
                            int w = IndexFor(neighbor);
                            _graph.AddEdge(new DirectedEdge(v, w, 1.0));
                            _graph.AddEdge(new DirectedEdge(w, v, 1.0));
                        }
                    }
                }
            }
        }

        public PathFinder(IMap map, List<Tuple<int, int>> dynamicObstacles)
        {
            _map = map;
            _graph = new EdgeWeightedDigraph(_map.Width * _map.Height);
            foreach (Cell cell in _map.GetAllCells())
            {
                if (cell.IsWalkable && !IsObstacle(dynamicObstacles, cell.X, cell.Y))
                {
                    int v = IndexFor(cell);
                    foreach (Cell neighbor in _map.GetBorderCellsInRadius(cell.X, cell.Y, 1))
                    {
                        if (neighbor.IsWalkable && !IsObstacle(dynamicObstacles, neighbor.X, neighbor.Y))
                        {
                            int w = IndexFor(neighbor);
                            _graph.AddEdge(new DirectedEdge(v, w, 1.0));
                            _graph.AddEdge(new DirectedEdge(w, v, 1.0));
                        }
                    }
                }
            }
        }

        public bool IsObstacle(List<Tuple<int, int>> dynamicObstacles, int x, int y)
        {
            return dynamicObstacles.FirstOrDefault(e => e.Item1 == x && e.Item2 == y) != null;
        }

        /// <summary>
        /// Returns an ordered IEnumerable of Cells representing the shortest path from a specified source Cell to a destination Cell
        /// </summary>
        /// <param name="source">The Cell which is at the start of the path</param>
        /// <param name="destination">The Cell which is at the end of the path</param>
        /// <returns>Returns an ordered IEnumerable of Cells representing the shortest path from a specified source Cell to a destination Cell</returns>
        public IEnumerable<Cell> ShortestPath(Cell source, Cell destination)
        {
            var dsp = new DijkstraShortestPath(_graph, IndexFor(source));
            IEnumerable<DirectedEdge> path = dsp.PathTo(IndexFor(destination));
            if (path == null)
            {
                yield return null;
            }
            else
            {
                foreach (DirectedEdge edge in path)
                {
                    yield return CellFor(edge.To);
                }
            }
        }
        private int IndexFor(Cell cell)
        {
            return (cell.Y * _map.Width) + cell.X;
        }
        private Cell CellFor(int index)
        {
            int x = index % _map.Width;
            int y = index / _map.Width;

            return _map.GetCell(x, y);
        }
    }
}