using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Gbd.PeriodMatching.Tools;
using NUnit.Framework;

namespace Gbd.PeriodMatching.Tests
{
  public class ToolsSelfTests 
  {

    [Test]
    public void RandomNextLongTests(
      [Range(0, int.MaxValue, 17874025)]            int randomSeed,
      [Values(10000)]                               int nbSamples,
      [Values(1, 2, int.MaxValue, long.MaxValue)]   long maxValue )
    {
      Random r = new Random(randomSeed);
      ICollection<long> samples = new LinkedList<long>();

      for (int i = 0; i<nbSamples; i++)
      {
        samples.Add(r.NextLong(maxValue));
      }

      double minDecileCount = nbSamples/10D * 0.9D;
      double maxDecileCount= nbSamples/10D * 1.1D;

      // This is the most stupid Assert.That(a, Is.EqualTo(a)) I've ever written
      var deciles = SplitIntoNIles<long>(10, samples.ToList());
      foreach(var decile in deciles)
      {
        Assert.That(decile.Count, Is.AtLeast(minDecileCount));
        Assert.That(decile.Count, Is.AtMost(maxDecileCount));
      }

    }


    [Test]
    private void SPlitIntoNIlesTests()
    {
      throw new NotImplementedException();
    }


    private IList<LinkedList<T>> SplitIntoNIles<T>(int n, List<T> datasource)
    {
      datasource.Sort();

      LinkedList<T>[] nIles = new LinkedList<T>[n];
      for (int i = 0; i < n; i++)
      {
        nIles[i] = new LinkedList<T>();
      }

      for (int i = 0; i < datasource.Count; i++ )
      {
        int index = (i*n)/datasource.Count;
        nIles[index].AddLast(datasource.ElementAt(i));
      }

      return nIles.ToList();
    }


  }
}
