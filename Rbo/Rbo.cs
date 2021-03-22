using System;
using System.Collections.Generic;
using System.Linq;

namespace Rbo
{
    public class Rbo 
    {

        public static decimal GetScore(List<int> a, List<int> b, decimal p)
        {
            var z = new List<Tuple<int, List<int>>>() {Tuple.Create(a.Count(), a), Tuple.Create(b.Count(), b)}
                .OrderBy(x =>x.Item1)
                .ToList();
		
            var (s, S) = (z[0].Item1, z[0].Item2);
            var (l, L) = (z[1].Item1, z[1].Item2);
		
            if(s == 0) return 0.0m;
		
            var ss = new HashSet<int>();
            var ls = new HashSet<int>();
		
            var x_d = new Dictionary<int, decimal>() { { 0, 0m} };
            decimal sum1 = 0.0m;	
            for(int i=0; i<l;i++) {
                var x = L[i];
                var y = i < s ? S[i] : int.MinValue;
                var d = i + 1;
			
                if( x == y) {
                    x_d[d] = x_d[d-1] + 1.0m;
                } else {
                    ls.Add(x);
                    if (y!=int.MinValue) ss.Add(y);
				
                    x_d[d] = x_d[d-1] + (ss.Contains(x) ? 1.0m : 0.0m) + (ls.Contains(y) ? 1.0m : 0.0m);
                }
			
                sum1 += x_d[d] / d * (decimal)Math.Pow((double)p, (double)d);
			
            }
            decimal sum2 = 0m;
            for(int i=0;  i < l-s; i++) {
                var d = s + i + 1;
                sum2 += x_d[d] * (d-s) / (d*s) * (decimal)Math.Pow((double)p, d);
            }
            var sum3 = ((x_d[l] - x_d[s]) / l + x_d[s] / s) * (decimal)Math.Pow((double)p, l);	
            var rbo_ext = (1 - p) / p * (sum1 + sum2) + sum3;
		
            return decimal.Round(rbo_ext,4);
        }
    }
}