using System;

namespace Gbd.PeriodMatching.Matcher
{
  internal class Period: IComparable
  {
    public long P;
    private readonly AbstractPeriodMatcher _matcher;


    public Period(AbstractPeriodMatcher matcher)
    {
      _matcher = matcher;
    }


    public bool Matches(Timer t)
    {
      switch(_matcher.Method)
      {
        case AbstractPeriodMatcher.MatchingMethod.PowerOf2:
          return MatchesPowerOfTwo(_matcher.ConstraintMaxMultiplier);
            
        default:
          throw new NotImplementedException();
      }
    }

    private bool MatchesPowerOfTwo(long maxMultiplier)
    {
      //long 

      //if (PowerOfTwoMath.IsAPowerOfTwo())

      throw new NotImplementedException();
    }


    public int CompareTo(object obj)
    {
      throw new NotImplementedException();
    }
  }
}
