using System.Collections.Generic;
using Gbd.PeriodMatching.Matcher;
using NUnit.Framework;

namespace Gbd.PeriodMatching.Tests
{

  [TestFixture]
  class PeriodMatcherTester
  {

    [SetUp]
    public void Setup()
    {
    }


    [TearDown]
    public void TearDown()
    {
    }



    [Test]
    public void AssignNoConstraintSmokeTest()
    {
      

    }


    [TestCase(false, ExpectedException=typeof(AssertionException))]
    [TestCase(false, 12, ExpectedException=typeof(AssertionException))]

    public void ForbiddenCasesSmokeTest(bool enableMaxTimers, int maxTimers = 0)
    {
      PeriodMatcher sandbox = new PeriodMatcher {ConstraintMaxTimers = 0};

      sandbox.Assign(new List<long>());


    }



  }
}
