using Xunit;

namespace CircleTrainProblem.Tests;

public class UnitTests
{
	private sealed class NotRandom : IRandom
	{
		private readonly bool _value;
		public NotRandom(bool value)
		{
			_value = value;
		}
		public bool NextBool() => _value;
	}

	private sealed class FirstTrueRestFalseNotRandom : IRandom
	{
		private bool _isFirst = true;
		public bool NextBool()
		{
			if (_isFirst)
			{
				_isFirst = false;
				return true;
			}
			return false;
		}
	}

	[Theory]
	[InlineData(2)]
	[InlineData(10)]
	[InlineData(100)]
	[InlineData(1_000)]
	[InlineData(10_000)]
	[InlineData(12_345)]
	public void Count_ExpectedCount_ReturnsCount(int count)
	{
		// Arrange
		CircularTrain sut = new(count);

		// Act
		int actualCount = sut.Count();

		// Assert
		Assert.Equal(count, actualCount);
	}

	[Theory]
	[InlineData(true, 100)]
	[InlineData(false, 400)]
	[InlineData(false, 1_600)]
	[InlineData(true, 32_000)]
	public void Count_AllTrainsSameState_ReturnsCount(bool state, int count)
	{
		// Arrange
		CircularTrain sut = new(count, new NotRandom(state));

		// Act
		int actualCount = sut.Count();

		// Assert
		Assert.Equal(count, actualCount);
	}

	[Theory]
	[InlineData(100)]
	[InlineData(400)]
	[InlineData(1_600)]
	[InlineData(32_000)]
	public void Count_AllTrainsSameStateExceptFirst_ReturnsCount(int count)
	{
		
		// Arrange
		CircularTrain sut = new(count, new FirstTrueRestFalseNotRandom());

		// Act
		int actualCount = sut.Count();

		// Assert
		Assert.Equal(count, actualCount);
	}
}