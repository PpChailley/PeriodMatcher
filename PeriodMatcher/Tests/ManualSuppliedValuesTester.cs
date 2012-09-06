using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Gbd.PeriodMatching.Tests
{
  public class ManualSuppliedValuesTester : PeriodMatcherTester
  {

    [TestCase(12, 13)]
    //[Test]
    //public void AssignNoConstraintSmokeTest2(
    //  [Values(0, 12, 777777)]     int period,
    //  [Range(0, MaxP, 171)]         int nbPeriods)
    public void AssignNoConstraintSmokeTest(long period, int nbPeriods)
    {
      for (int i = 0; i < nbPeriods; i++)
      {
        Sandbox.PeriodsToMatch.Add(period);
      }

      Sandbox.Assign();

      Assert.That(Sandbox.TimersAssignment, Is.All.EqualTo(period));
    }




    [TestCase(new long[] { 1, 2, 3 }, new long[] { 1, 2, 3 })]
    public void AssignNoConstraintSimpleFuncTest(long[] periods, long[] expectedTimers)
    {
      Sandbox.PeriodsToMatch = periods;
      Sandbox.Assign();

      Assert.That(Sandbox.TimersAssignment, Is.Unique);
      Assert.That(Sandbox.TimersAssignment, Is.EquivalentTo(expectedTimers));
    }






  }
}
