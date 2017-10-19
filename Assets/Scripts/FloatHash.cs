using System;
using System.Collections;

namespace AssemblyCSharp {
public class FloatHash : IEqualityComparer {
  public FloatHash()
  {}

  public int GetHashCode(object time) {
    return (int)Math.Floor((float)time * 1000);
  }

  new public bool Equals(object time1, object time2) {
    float diff = (float)time1 - (float)time2;

    return (diff > -0.001) && (diff < 0.001);
  }
}
}
