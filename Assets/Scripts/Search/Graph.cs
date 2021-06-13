
using System;
using System.Collections.Generic;

public interface Graph<L>
{
    int GetWeight(L id);
    IEnumerable<L> Neighbors(L id);
}