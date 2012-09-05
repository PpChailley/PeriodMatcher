using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Gbd.PeriodMatching.Matcher
{
  public class PeriodMatcher
  {

    private int _constraintMaxTimers = 0;
    private bool _constraintEnableTimers = false;


    public int ConstraintMaxTimers
    {
      set 
      { 
        _constraintMaxTimers = value;
        _constraintEnableTimers = true;
      }
    }


    public void Assign(ICollection<long> periodsToMatch)
    {

      throw new NotImplementedException();

      if (_constraintEnableTimers)
        Assert.That(_constraintMaxTimers, Is.GreaterThan(0));


      

    }

  }
}
