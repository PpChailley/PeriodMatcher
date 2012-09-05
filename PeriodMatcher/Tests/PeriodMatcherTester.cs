using System.Collections.Generic;
using Gbd.PeriodMatching.Matcher;
using NUnit.Framework;

namespace Gbd.PeriodMatching.Tests
{

  [TestFixture]
  class PeriodMatcherTester
  {


    private PeriodMatcher _sandbox;

    #region Setup And TearDown

    [SetUp]
    public void Setup()
    {
      _sandbox = new PeriodMatcher();
    }


    [TearDown]
    public void TearDown()
    {
    }


    #endregion

    #region Smoke Tests

    [Test]
    public void AssignNoConstraintSmokeTest()
    {
      

    }


    [TestCase(false)]
    [TestCase(true, 12)]
    [TestCase(true, ExpectedException=typeof(AssertionException))]
    [TestCase(true, -1, ExpectedException=typeof(AssertionException))]
    [TestCase(true, PeriodMatcher.MaxTimersSupported+1, ExpectedException=typeof(AssertionException))]
    public void ForbiddenCasesSmokeTest(bool enableMaxTimers, int maxTimers = 0)
    {
      if (enableMaxTimers)
      {
        _sandbox.ConstraintMaxTimers = maxTimers;
      }

      _sandbox.Assign(new List<long>());
    }



    #endregion


  }
}
