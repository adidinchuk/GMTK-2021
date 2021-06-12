
using System;
using System.Collections.Generic;

public interface Graph<L>
{
    int GetScore(L id);
    IEnumerable<L> Neighbors(L id);
}