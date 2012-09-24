using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gbd.PeriodMatching.Matcher
{
  public class VoidMatcher : AbstractPeriodMatcher, IPeriodMatcher
  {
    public override void Assign()
    {
      return;
    }
  }
}
