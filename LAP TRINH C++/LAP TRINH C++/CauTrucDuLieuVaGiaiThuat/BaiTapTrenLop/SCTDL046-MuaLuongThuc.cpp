#include <bits/stdc++.h>
using namespace std;
void survival(int S, int N, int M)
{
	if (S*M > (S-S/7)*N)
		cout << "-1\n";
	else {
		for( int i=1;i<=S;i++){
            if(N*i>=S*M){
                cout<<i<<"\n";
                break;
            }
        }
	}
}
int main()
{
    int T; cin>>T;
    while(T--){
        int S, N, M;
	cin>>N>>S>>M;
	survival(S, N, M);
    }
	return 0;
}