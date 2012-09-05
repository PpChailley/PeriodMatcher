﻿using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Gbd.PeriodMatching.Matcher
{
  public class PeriodMatcher
  {

    public const int MaxTimersSupported = 100;

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
      AssertStatusIsReadyForComputation();


    }





    private void AssertStatusIsReadyForComputation()
    {
      if (_constraintEnableTimers)
        Assert.That(_constraintMaxTimers, Is.GreaterThan(0));

      Assert.That(_constraintMaxTimers, Is.LessThanOrEqualTo(MaxTimersSupported));
    }
  }
}
