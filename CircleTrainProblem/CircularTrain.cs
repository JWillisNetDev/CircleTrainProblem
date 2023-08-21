using CircleTrainProblem.Helpers;

namespace CircleTrainProblem;

// This method simply providers a wrapper around the internal RootNode node.

public class CircularTrain
{
	private sealed class DefaultRandomBehavior : IRandom
	{
		private static readonly Random Random = new(Convert.ToInt32(DateTime.Now.Ticks % int.MaxValue));
		public bool NextBool() => Random.Next() % 2 == 0;
	}

	private static readonly DefaultRandomBehavior DefaultRandom = new();

	private readonly IRandom _rng;
	private Node Current { get; set; }
	private Node Next => Current.Next;
	private Node Last => Current.Last;

	public CircularTrain(int length, IRandom? rng = null)
	{
		Guard.LessThanOrEqualToZero(length);

		_rng = rng ?? DefaultRandom;

		Current = NewNode();
		for (int i = 1; i < length; i++)
		{
			Current.Add(NewNode());
		}
	}
	
	private Node NewNode() => new(_rng.NextBool());

	public int Count()
	{
		// Start by setting the first two trains to opposite states
		Last.State = !Current.State;
		return Count(Current.State);
	}

	private int Count(bool state, int count = 0)
	{
		while (Last.State != state)
		{
			// Think of this as moving back to the original position because at the end of the loop,  we traverse back to the home train (by counting)
			Current = Current.Move(count);

			// Traverse in one direction and keep count of times traversed until we meet an opposite state.
			while (Current.State == state)
			{
				count++;
				Current = Next;
			}

			// Continue reversing states and moving one direction until the opposite (original) state is met.
			while (Current.State != state)
			{
				count++;
				Current.Flip();
				Current = Next;
			}

			// Move backwards the same amount to see if the known state was switched
			Current = Current.Move(count, true);
		}
		return count;
	}
}