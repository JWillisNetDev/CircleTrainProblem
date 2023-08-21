using System.Runtime.CompilerServices;
using CircleTrainProblem.Helpers;

namespace CircleTrainProblem;

// This is a node in a circular-linked list
// It has a state which is determined at random. It can be 'on' (true) or 'off' (false)
// The only action we make accessible for mutation is switching this state from on to off, or off to on.

internal sealed class Node
{
	public bool State { get; set; }
	public Node Next { get; private set; }
	public Node Last { get; private set;}

	public Node(bool state)
	{
		State = state;
		Next = this;
		Last = this;
	}

	public void Flip() => State = !State;

	public Node Move(int count, bool moveBackwards = false)
	{
		Guard.LessThanZero(count);

		Node current = this;
		for (int i = 0; i < count; i++)
		{
			current = moveBackwards ? current.Last : current.Next;
		}
		return current;
	}

	#region Circular Linked List Construction
	public void Add(Node node)
	{
		// from
		// This <-> Next
		// into
		// This <-> node <-> Next
		node.Next = Next;
		Next.Last = node;
		node.Last = this;
		Next = node;
	}
	#endregion
}