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
      [Range(0, int.MaxValue, 178745874)]      int randomSeed,
      [Values(100000)]                        int nbSamples,
      [Values(100, int.MaxValue, long.MaxValue)]   long maxValue )
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
      var deciles = SplitIntoNByValue(10, 0, maxValue, samples.ToList());
      foreach(var decile in deciles)
      {
        Assert.That(decile.Count, Is.AtLeast(minDecileCount));
        Assert.That(decile.Count, Is.AtMost(maxDecileCount));
      }

    }


    [Test]
    public void SplitIntoNByValueSmokeTests(
      [Values(10, 600)]       int sequenceLen,
      [Values(1, 2, 7)]       int nbRepeat,
      [Values(1, 3, 10)]      int nbBuckets)
    {
      long[] samples = new long[sequenceLen*nbRepeat];

      for (int seqPosition = 0; seqPosition<sequenceLen; seqPosition++)
      {
        for (int repeat = 0; repeat<nbRepeat; repeat++)
        {
          samples[seqPosition*nbRepeat + repeat] = seqPosition;
        }
      }

      var split = SplitIntoNByValue(nbBuckets, 0, sequenceLen, samples.ToList());

      Assert.That(split, Is.AssignableTo<IEnumerable<LinkedList<long>>>());
      Assert.That(split, Is.All.Matches<List<long>>( s => s.Count == nbRepeat));
    }

    private IEnumerable<LinkedList<long>> SplitIntoNByValue(int n, long minValue, long maxValue, List<long> datasource)
    {
      long bucketWidth = minValue + (maxValue - minValue)/n;

      LinkedList<long>[] nIles = new LinkedList<long>[n];
      for (int i = 0; i < n; i++)
      {
        nIles[i] = new LinkedList<long>();
      }

      foreach(long data in datasource)
      {
        Assert.That(data, Is.AtMost(maxValue));
        Assert.That(data, Is.AtLeast(minValue));
        int index = (int) ((data-1)/bucketWidth);
        if (index < 0) index = 0;
        Assert.That(index, Is.AtMost(n-1));
        nIles[index].AddLast(data);
      }

      return nIles.ToList();
    }
      
    [Test]
    public void SPlitIntoNIlesTests()
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
