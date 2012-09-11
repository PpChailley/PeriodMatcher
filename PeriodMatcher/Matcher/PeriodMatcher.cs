using System.Collections.Generic;
using NLog;
using NUnit.Framework;

namespace Gbd.PeriodMatching.Matcher
{
  public class PeriodMatcher: AbstractPeriodMatcher
  {
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();


    public override void Assign()
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

      Assert.That(_periodsToMatch, Is.Not.Null);
      Assert.That(_periodsToMatch.Count, Is.LessThanOrEqualTo(MaxPeriodsSupported));

      Assert.That(_periodsToMatch, Is.All.GreaterThanOrEqualTo(0));

    }
  }
}
