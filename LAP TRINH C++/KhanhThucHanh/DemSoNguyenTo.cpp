#include<bits/stdc++.h>
#define ll long long
using namespace std;
ll f[1000001];
void sang()
{
	f[0]=1;
	f[1]=1;
	f[2]=0;
	for(int i=4;i<=1000000;i=i+2) f[i]=1;
	for(int i=3;i<=sqrt(1000000);i++)
	{
		for(int j=i*i;j<=1000000;j=j+i) f[j]=1;
	}
}
int main () {
	sang();

	int t;
	cin >> t;
	while (t--) {
		long long a, b;
		ll dem=0;
		cin >> a >> b;
		for (long long i=a;i<=b;i++) {
            if(f[i]==0)dem++;
        }
		cout << dem << endl;
		}
		return 0;
	}