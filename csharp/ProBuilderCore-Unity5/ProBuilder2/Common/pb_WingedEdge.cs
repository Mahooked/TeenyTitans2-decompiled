using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ProBuilder2.Common;

public class pb_WingedEdge : IEquatable<pb_WingedEdge>, IEnumerable
{
	public pb_EdgeLookup edge;

	public pb_Face face;

	public pb_WingedEdge next;

	public pb_WingedEdge previous;

	public pb_WingedEdge opposite;

	public bool Equals(pb_WingedEdge b)
	{
		return b != null && edge.local.Equals(b.edge.local);
	}

	public override bool Equals(object b)
	{
		if (b is pb_WingedEdge b2 && Equals(b2))
		{
			return true;
		}
		if (b is pb_Edge obj && Equals(obj))
		{
			return true;
		}
		return true;
	}

	public override int GetHashCode()
	{
		return edge.local.GetHashCode();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public pb_WingedEdgeEnumerator GetEnumerator()
	{
		return new pb_WingedEdgeEnumerator(this);
	}

	public override string ToString()
	{
		return string.Format("Common: {0}\nLocal: {1}\nOpposite: {2}\nFace: {3}", edge.common.ToString(), edge.local.ToString(), (opposite != null) ? opposite.edge.ToString() : "null", face.ToString());
	}

	public pb_WingedEdge GetAdjacentEdgeWithCommonIndex(int common)
	{
		if (next.edge.common.Contains(common))
		{
			return next;
		}
		if (previous.edge.common.Contains(common))
		{
			return previous;
		}
		return null;
	}

	public static List<pb_Edge> SortEdgesByAdjacency(pb_Face face)
	{
		List<pb_Edge> edges = new List<pb_Edge>(face.edges);
		return SortEdgesByAdjacency(edges);
	}

	public static List<pb_Edge> SortEdgesByAdjacency(List<pb_Edge> edges)
	{
		for (int i = 1; i < edges.Count; i++)
		{
			int y = edges[i - 1].y;
			for (int j = i + 1; j < edges.Count; j++)
			{
				if (edges[j].x == y || edges[j].y == y)
				{
					pb_Edge value = edges[j];
					edges[j] = edges[i];
					edges[i] = value;
				}
			}
		}
		return edges;
	}

	public static Dictionary<int, List<pb_WingedEdge>> GetSpokes(List<pb_WingedEdge> wings)
	{
		Dictionary<int, List<pb_WingedEdge>> dictionary = new Dictionary<int, List<pb_WingedEdge>>();
		List<pb_WingedEdge> value = null;
		for (int i = 0; i < wings.Count; i++)
		{
			if (dictionary.TryGetValue(wings[i].edge.common.x, out value))
			{
				value.Add(wings[i]);
			}
			else
			{
				dictionary.Add(wings[i].edge.common.x, new List<pb_WingedEdge> { wings[i] });
			}
			if (dictionary.TryGetValue(wings[i].edge.common.y, out value))
			{
				value.Add(wings[i]);
				continue;
			}
			dictionary.Add(wings[i].edge.common.y, new List<pb_WingedEdge> { wings[i] });
		}
		return dictionary;
	}

	public static List<int> SortCommonIndicesByAdjacency(List<pb_WingedEdge> wings, HashSet<int> common)
	{
		List<pb_Edge> list = (from y in wings
			where common.Contains(y.edge.common.x) && common.Contains(y.edge.common.y)
			select y.edge.common).ToList();
		if (list.Count != common.Count)
		{
			return null;
		}
		return (from x in SortEdgesByAdjacency(list)
			select x.x).ToList();
	}

	public static List<pb_WingedEdge> GetWingedEdges(pb_Object pb, bool oneWingPerFace = false)
	{
		return GetWingedEdges(pb, pb.faces, oneWingPerFace);
	}

	public static List<pb_WingedEdge> GetWingedEdges(pb_Object pb, IEnumerable<pb_Face> faces, bool oneWingPerFace = false, Dictionary<int, int> sharedIndexLookup = null)
	{
		Dictionary<int, int> dictionary = ((sharedIndexLookup != null) ? sharedIndexLookup : pb.sharedIndices.ToDictionary());
		IEnumerable<pb_Face> enumerable = faces.Distinct();
		List<pb_WingedEdge> list = new List<pb_WingedEdge>();
		Dictionary<pb_Edge, pb_WingedEdge> dictionary2 = new Dictionary<pb_Edge, pb_WingedEdge>();
		int num = 0;
		foreach (pb_Face item in enumerable)
		{
			List<pb_Edge> list2 = SortEdgesByAdjacency(item);
			int count = list2.Count;
			pb_WingedEdge pb_WingedEdge2 = null;
			pb_WingedEdge pb_WingedEdge3 = null;
			for (int i = 0; i < count; i++)
			{
				pb_Edge pb_Edge2 = list2[i];
				pb_WingedEdge pb_WingedEdge4 = new pb_WingedEdge();
				pb_WingedEdge4.edge = new pb_EdgeLookup(dictionary[pb_Edge2.x], dictionary[pb_Edge2.y], pb_Edge2.x, pb_Edge2.y);
				pb_WingedEdge4.face = item;
				if (i < 1)
				{
					pb_WingedEdge2 = pb_WingedEdge4;
				}
				if (i > 0)
				{
					pb_WingedEdge4.previous = pb_WingedEdge3;
					pb_WingedEdge3.next = pb_WingedEdge4;
				}
				if (i == count - 1)
				{
					pb_WingedEdge4.next = pb_WingedEdge2;
					pb_WingedEdge2.previous = pb_WingedEdge4;
				}
				pb_WingedEdge3 = pb_WingedEdge4;
				if (dictionary2.TryGetValue(pb_WingedEdge4.edge.common, out var value))
				{
					value.opposite = pb_WingedEdge4;
					pb_WingedEdge4.opposite = value;
				}
				else
				{
					pb_WingedEdge4.opposite = null;
					dictionary2.Add(pb_WingedEdge4.edge.common, pb_WingedEdge4);
				}
				if (!oneWingPerFace || i < 1)
				{
					list.Add(pb_WingedEdge4);
				}
			}
			num += count;
		}
		return list;
	}
}
