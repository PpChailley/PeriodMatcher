using System.Collections.Generic;
using System.Collections.ObjectModel;
using NUnit.Framework;

namespace Gbd.PeriodMatching.Matcher
{
  public class PeriodMatcher
  {

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
      get { return _periodsToMatch ?? (_periodsToMatch = new Collection<long>()); }
      set { _periodsToMatch = value; }
    }



    public void Assign()
    {
      AssertStatusIsReadyForComputation();



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
