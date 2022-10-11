#include<bits/stdc++.h>
#include<iostream>

using namespace std;

// double mu(double x) {
//     return x*x;
// }
 
// double LuyThua(double x, int n) {
//     if (n == 0) return 1;
//     else
//         if (n % 2 == 0)
//             return mu(LuyThua(x, n/2));
//         else
//             return x * (mu(LuyThua(x, n/2)));
// }


int main(){
    int t;
    cin >> t;
    while (t--)
    {
        int n , i;
        double x, T, S;
        i = 1;
	    T = 1;
	    S = 0;
        cin >> n;
        cin >> x;
        while(i <= n)
	    {
		    T = T * x;
		    S = sqrt(T + S);
		    i++;
	    }
        cout << fixed << setprecision(3) << S << endl;
    }
    return 0;
}