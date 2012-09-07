using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NUnit.Framework;

namespace Gbd.PeriodMatching.Tests
{
  public class InputIntegrityTester : PeriodMatcherTester
  {



    #region Smoke Tests & Functional Tests




    [TestCase(false)]
    [TestCase(true, 12)]
    [TestCase(true, ExpectedException = typeof(AssertionException))]
    [TestCase(true, -1, ExpectedException = typeof(AssertionException))]
    [TestCase(true, MaxT + 1, ExpectedException = typeof(AssertionException))]
    public void ForbiddenCasesSmokeTest(bool enableMaxTimers, int maxTimers = 0)
    {
      if (enableMaxTimers)
      {
        Sandbox.ConstraintMaxTimers = maxTimers;
      }

      Sandbox.Assign();
    }


    [Test]
    [ExpectedException(typeof(InvalidOperationException))]
    public void NoAccessShouldBeGivenBeforeComputationTestsAreDone()
    {
      Sandbox.PeriodsToMatch.Add(100);
      Sandbox.Assign();

      Sandbox.PeriodsToMatch.Add(200);
      int a = Sandbox.TimersAssignment.Count;
      Assert.That(a, Is.Not.Null);
    }


    #endregion

    #region Robustness and limits


    [Test]
    
    public void ForbiddenCasesAllRange(
      [Range(1, MaxT)]      int maxTimers)
    {
      Sandbox.ConstraintMaxTimers = maxTimers;
      Sandbox.PeriodsToMatch = new Collection<long>();
      Sandbox.Assign();
    }

    [Test]
    
    [ExpectedException(typeof(AssertionException))]
    public void ForbiddenCasesRobustness(
      [Values(int.MinValue, int.MaxValue, -1, 0, MaxT + 1)]      int maxTimers)
    {
      Sandbox.ConstraintMaxTimers = maxTimers;

      Sandbox.Assign();
    }


    private static int[] MakeRandomIntArray(int min, int max, int number)
    {
      List<int> data = new List<int>(number);

      for (int i = 0; i<number; i++)
        data.Add(Rnd.Next(min, max));

      return data.ToArray();
    }



    protected static int[] DPCapacityLimitsTestsAllRangeNbPeriods()
    {
      return MakeRandomIntArray(0, MaxP, 10);
    }

    protected static int[] DPCapacityLimitsTestsAllRangeNbTimers()
    {
      return MakeRandomIntArray(0, MaxT, 5);
    }

    protected static int[] DPCapacityLimitsTestsAllRangeValue()
    {
      return MakeRandomIntArray(0, 1000*1000, 5);
    }


    [Test]
    public void CapacityLimitsTestsAllRange(
      [ValueSource("DPCapacityLimitsTestsAllRangeNbPeriods")]          int nbPeriods,
      [ValueSource("DPCapacityLimitsTestsAllRangeNbTimers")]            int nbTimers,
      [ValueSource("DPCapacityLimitsTestsAllRangeValue")]       int periodsValue)
    {
      Sandbox.ConstraintMaxTimers = nbTimers;

      List<long> periods = new List<long>(nbPeriods * 2);
      for (int i = 0; i < nbPeriods; i++)
      {
        periods.Add(periodsValue);
      }
      Sandbox.PeriodsToMatch = periods;

      Sandbox.Assign();
    }

    [TestCase(0, 1, 666)]
    [TestCase(0, MaxT, 666)]
    [TestCase(MaxP, 1, 666)]
    [TestCase(MaxP, MaxT, 666)]
    public void CapacityLimitsAllRange2(int nbPeriods, int nbTimers, int periodsValue)
    {
      Sandbox.ConstraintMaxTimers = nbTimers;

      List<long> periods = new List<long>(nbPeriods * 2);
      for (int i = 0; i < nbPeriods; i++)
      {
        periods.Add(periodsValue);
      }
      Sandbox.PeriodsToMatch = periods;

      Sandbox.Assign();
    }


    [Test]
    
    [ExpectedException(typeof(AssertionException))]
    public void CapacityLimitsRobustness(
      [Values(1, 5, MaxP, MaxP + 1)]                                        int nbPeriods,
      [Values(int.MinValue, -1, 0, 5, MaxT, MaxT + 1, int.MaxValue)]        int nbTimers,
      [Values(int.MinValue, -1, 0, 5, int.MaxValue)]                      int periodsValue)
    {
      Sandbox.ConstraintMaxTimers = nbTimers;

      List<long> periods = new List<long>();
      for (int i = 0; i < nbPeriods; i++)
      {
        periods.Add(periodsValue);
      }
      Sandbox.PeriodsToMatch = periods;

      Sandbox.Assign();

      if (nbPeriods >= 0 && nbPeriods <= MaxP
          && nbTimers > 0 && nbTimers <= MaxT
          && periodsValue >= 0
          )
      {
        //Assert.Ignore("This is not a robustness case and has already been tested by nominal cases");
        Assert.Fail("This is not a robustness case and has already been tested by nominal cases");
      }


    }



    #endregion



  }
}
