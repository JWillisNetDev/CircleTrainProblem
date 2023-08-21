using System.Runtime.CompilerServices;

namespace CircleTrainProblem.Helpers;

internal static class Guard
{
	public static void LessThanZero(int arg, [CallerArgumentExpression(nameof(arg))] string? paramName = null)
	{
		if (arg < 0) throw new ArgumentOutOfRangeException(paramName); 
	}

	public static void LessThanOrEqualToZero(int arg, [CallerArgumentExpression(nameof(arg))] string? paramName = null)
	{
		if (arg <= 0) throw new ArgumentOutOfRangeException(paramName); 
	}
}