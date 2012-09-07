using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NLog;
using NUnit.Framework;

namespace Gbd.PeriodMatching.Matcher
{
  public class PeriodMatcher
  {

    private static readonly Logger Log = LogManager.GetCurrentClassLogger();

    public const int MaxTimersSupported = 100;
    public const int MaxPeriodsSupported = 50*1000;

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


    private ICollection<long> _periodsToMatch = null;
    public ICollection<long> PeriodsToMatch
    {
      set { _periodsToMatch = value; }
      get
      {
        _timersAssignmentDone = false;
        return _periodsToMatch ?? (_periodsToMatch = new Collection<long>()); 
      }
    }

    private bool _timersAssignmentDone = false;
    private List<long> _timersAssignment;
    public List<long> TimersAssignment
    {
      get
      {
        if (_timersAssignmentDone == false)
          throw new InvalidOperationException("Cannot access the result of the computation before calling Assign()");

        return new List<long>(_timersAssignment);
      }
    }

    public void Assign()
    {
      AssertStatusIsReadyForComputation();

      _timersAssignment = new List<long>(_periodsToMatch.Count);


      _timersAssignmentDone = true;
    }





    private void AssertStatusIsReadyForComputation()
    {
      if (_constraintEnableTimers)
      {
        Assert.That(_constraintMaxTimers, Is.GreaterThan(0));
        Assert.That(_constraintMaxTimers, Is.LessThanOrEqualTo(MaxTimersSupported));
      }

      Assert.That(PeriodsToMatch, Is.Not.Null);
      Assert.That(PeriodsToMatch.Count, Is.LessThanOrEqualTo(MaxPeriodsSupported));

      Assert.That(PeriodsToMatch, Is.All.GreaterThanOrEqualTo(0));

    }
  }
}
