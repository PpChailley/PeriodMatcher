using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gbd.PeriodMatching.Tools;

namespace Gbd.PeriodMatching.Matcher
{
  internal class Period
  {
    public long P;
    private readonly PeriodMatcher _matcher;

    public Period(PeriodMatcher matcher)
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


  }
}
